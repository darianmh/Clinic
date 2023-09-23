
using System.Linq.Expressions;

namespace Appointment.Contract.Repositories;

public interface IAppointmentRepository
{
    IUnitOfWork UnitOfWork { get; }
    void Add(Domain.AppointmentAggregate.Appointment appointment);

    /// <summary>
    /// دریافت هه قرارهای ویزیت برای یک بیمار در یک تاریخ مشخص
    /// </summary>
    /// <param name="patientId">شناسه بیمار</param>
    /// <param name="startDate">تاریخ مدنظر</param>
    /// <param name="order">مرتب سازی</param>
    /// <returns></returns>
    List<Domain.AppointmentAggregate.Appointment> GetAllAppointmentForPatient(int patientId, DateTime startDate, Expression<Func<Domain.AppointmentAggregate.Appointment, object>>? order = null);

    /// <summary>
    /// دریافت همه ویزیت های تنظیم شده برای یک دکتر در یک تاریخ
    /// </summary>
    /// <param name="doctorId">شناسه پزشک</param>
    /// <param name="date">تاریخ مد نظر</param>
    /// <param name="order">مرتب سازی</param>
    /// <returns></returns>
    List<Domain.AppointmentAggregate.Appointment> GetDoctorAppointments(int doctorId, DateTime date, Expression<Func<Domain.AppointmentAggregate.Appointment, object>>? order = null);
}