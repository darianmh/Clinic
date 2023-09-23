using Appointment.Domain;
using Appointment.Domain.DoctorAggregate;

namespace Appointment.Contract.Services;


public interface IAppointmentService
{
    /// <summary>
    /// بررسی اطلاعات دکتر و مریض و تاریخ درخواست شده
    /// برای اینکه امکان افزودن قرار بررسی شود
    /// </summary>
    /// <param name="request">اطلاعات مریض و پزشک و تاریخ درخواستی</param>
    /// <param name="doctor"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> CanSetAppointment(SetAppointmentRequestDto request, Doctor doctor, CancellationToken cancellationToken);

    /// <summary>
    /// دریافت اولین وقت خالی دکتر برای ویزیت مریض با در نظر گرفتن مدت زمان ویزیت
    /// بررسی وقت خالی از اولین روز شروع می شود و اگر زمانبندی ویزیت های بیمار و زمان هفتگی پزشک
    /// ممکن بودنند به دنبال اولین وقت خالی برای ویزیت می گردد در غیر اینصورت روز بعدی بررسی می شود
    /// </summary>
    /// <returns>یک قرار ملاقات معتبر</returns>
    Task<Domain.AppointmentAggregate.Appointment> FindEarliestAppointment(Doctor doctor, int durationMinutes,
        int patientId, CancellationToken cancellationToken);
    /// <summary>
    /// افزودن یک قرار ملاقات جدید به دیتابیس
    /// </summary>
    void AddAppointment(Domain.AppointmentAggregate.Appointment appointment, CancellationToken cancellationToken);

    /// <summary>
    /// بررسی امکان ثبت زمان ویزیت برای بیمار انتخابی
    /// یک بیمار در طول روز نمی تواند بیش از دو ویزیت داشته باشد
    /// </summary>
    /// <param name="startDate">تاریخ مد نظر برای ویزیت</param>
    /// <param name="cancellationToken"></param>
    /// <param name="patientId">شناسه بیمار</param>
    /// <returns></returns>
    Task<bool> CanSetAppointmentForPatient(int patientId, DateTime startDate, CancellationToken cancellationToken);
    /// <summary>
    /// بررسی امکان ثبت زمان ویزیت برای پزشک
    /// یک پزشک تنها در ساعات زمانبندی شده ویزیت انجام می دهند
    /// امکان همپوشانی زمان بیماران نیز برای هر نوع پزشک متفاوت است
    /// </summary>
    /// <returns></returns>
    Task<bool> CanSetAppointmentForDoctor(Doctor doctor, DateTime startDateTime, int durationMinutes,
        CancellationToken cancellationToken);
}