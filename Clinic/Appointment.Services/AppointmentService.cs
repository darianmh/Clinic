using Appointment.Contract.Repositories;
using Appointment.Contract.Services;
using Appointment.Domain;
using Appointment.Domain.DoctorAggregate;

namespace Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }



        /// <inheritdoc />
        public async Task<bool> CanSetAppointment(SetAppointmentRequestDto request, Doctor doctor, CancellationToken cancellationToken)
        {
            var patientTask = CanSetAppointmentForPatient(request.PatientId, request.StartDateTime.Date, cancellationToken);
            var doctorTask = CanSetAppointmentForDoctor(doctor, request.StartDateTime,
                request.DurationMinutes, cancellationToken);

            var result = await Task.WhenAll(patientTask, doctorTask);

            return result.All(x => x);
        }

        /// <inheritdoc />
        public Task<Domain.AppointmentAggregate.Appointment> FindEarliestAppointment(Doctor doctor, int durationMinutes,
            int patientId, CancellationToken cancellationToken)
        {
            var dateNow = DateTime.Now;

            return FindEarliestAppointment(doctor, dateNow, durationMinutes, patientId, cancellationToken);
        }
        /// <inheritdoc />
        public void AddAppointment(Domain.AppointmentAggregate.Appointment appointment, CancellationToken cancellationToken)
        {
            _repository.Add(appointment);
            _repository.UnitOfWork.SaveChanges();
        }

        /// <inheritdoc />
        public Task<bool> CanSetAppointmentForPatient(int patientId, DateTime startDate, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var patientAppointmentCount = _repository.GetAllAppointmentForPatient(patientId, startDate.Date);
                return patientAppointmentCount.Count < 2;
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> CanSetAppointmentForDoctor(Doctor doctor, DateTime startDateTime, int durationMinutes,
            CancellationToken cancellationToken)
        {

            return Task.Run(() =>
            {
                var doctorSchedules = doctor.DoctorSchedules
                    .FirstOrDefault(x =>
                        //سبق برنامه هفتگی روز هفته را چک میکند
                        x.DayOfWeek == (int)startDateTime.DayOfWeek
                        //برنامه زمانی دکتر قبل از تاریخ انتخابی شروع شده باشد
                        && x.StartTime.TimeOfDay <= startDateTime.TimeOfDay
                        //و بعد از زمان انتخابی به علاوه مدت زمان مدنظر به پایان برسد
                        && x.EndTime.TimeOfDay >= startDateTime.AddMinutes(durationMinutes).TimeOfDay);

                //زمان پایان بازه انتخابی
                var endDateTime = startDateTime.AddMinutes(durationMinutes);
                //اگر برنامه هفتگی وجود نداشته باشد یا برناه دکتر قبل از پایان ملاقات تمام شود
                //امکان ثبت زمان وجود ندارد
                if (doctorSchedules == null
                    || doctorSchedules.EndTime.TimeOfDay < endDateTime.TimeOfDay)
                    return false;


                var doctorAppointments =
                    _repository.GetDoctorAppointments(doctor.Id, startDateTime.Date, x => x.DateTime);
                //انتخاب قرار ویزیت های انتخاب شده در قبل
                // به صورتی که شروع یا پایان آن با بازه ی انتخابی کاربر همپوشانی داشته باشد
                var rangAppointment = doctorAppointments.Where(x =>
                    //شروع آن در بازه زمانی انتخابی باشد
                    (x.DateTime >= startDateTime
                     && x.DateTime <= endDateTime
                    ) ||
                    //یا پایان آن
                    (x.DateTime.AddMinutes(x.DurationMinutes) >= startDateTime
                     && x.DateTime.AddMinutes(x.DurationMinutes) <= endDateTime));
                //اگر زمان های دارای همپوشانی با زمان فعلی بیش از تعداد مجاز باشد امکان ثبت وجود ندارد
                if (rangAppointment.Count() >= doctor.MaxOverlap) return false;

                return true;
            }, cancellationToken);
        }





        /// <summary>
        /// در روز انتخابی اولین قرار ویریت را پیدا می کند
        /// و اگر در این روز قراری یافت نشد به دوباره برای روز بعدی اجرا می شود
        /// </summary>
        /// <param name="doctor">پزشک</param>
        /// <param name="selectedDateTime">روز انتخابی</param>
        /// <param name="durationMinutes">مدت زمان ویزیت</param>
        /// <param name="patientId">بیمار</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Domain.AppointmentAggregate.Appointment> FindEarliestAppointment(Doctor doctor, DateTime selectedDateTime, int durationMinutes, int patientId, CancellationToken cancellationToken)
        {
            //بررسی امکان ثبت قرار ویزیت برای کاربر در روز انتخابی
            var checkPatient = await CanSetAppointmentForPatient(patientId, selectedDateTime, cancellationToken);
            if (checkPatient)
            {
                var appointment = FindEarliestAppointment(doctor, selectedDateTime, durationMinutes);
                if (appointment != null) return appointment;
            }

            //اگر در روز انتخابی امکان ثبت وجود نداشت روز بعد چک می شود
            return await FindEarliestAppointment(doctor, selectedDateTime.AddDays(1), durationMinutes, patientId, cancellationToken);
        }
        /// <summary>
        /// برنامه هفتگی پزشک و قرار های ست شده را دریافت می کند
        /// تا اولین قرار ملاقات خالی را بیابد
        /// </summary>
        /// <param name="doctor">پزشک</param>
        /// <param name="selectedDateTime">روز انتخابی</param>
        /// <param name="durationMinutes">مدت زمان ویزیت</param>
        /// <returns></returns>
        private Domain.AppointmentAggregate.Appointment? FindEarliestAppointment(Doctor doctor, DateTime selectedDateTime, int durationMinutes)
        {
            //برنامه هفتگی پزشک
            var doctorSchedule = doctor.DoctorSchedules
                .Where(x =>
                    //طبق برنامه هفتگی روز هفته را چک میکند
                    x.DayOfWeek == (int)selectedDateTime.DayOfWeek)
                .ToList();
            //قرار های ویزیت های تنظیم شده
            var doctorAppointments =
                _repository.GetDoctorAppointments(doctor.Id, selectedDateTime.Date, x => x.DateTime);
            //اولین قرار ملاقات آزاد
            var appointment = FindEarliestAppointment(doctorSchedule, doctorAppointments, durationMinutes);

            return appointment;
        }
        /// <summary>
        /// براساس برنامه روز پزشک و لیست ملاقات های تعیین شده
        /// اولین قرار ملاقات خالی را پیدا می کند
        /// </summary>
        /// <param name="doctorSchedule">برنامه پزشک در روز انتخابی</param>
        /// <param name="doctorAppointments">لیست قرار ملاقات ها در روز انتخابی</param>
        /// <param name="durationMinutes">مدت زمان قرار ملاقات</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Domain.AppointmentAggregate.Appointment? FindEarliestAppointment(List<DoctorSchedule> doctorSchedule, List<Domain.AppointmentAggregate.Appointment> doctorAppointments, int durationMinutes)
        {
            throw new NotImplementedException();
        }

    }
}
