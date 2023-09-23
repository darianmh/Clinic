using Appointment.Domain.DoctorAggregate;

namespace Appointment.Domain.AppointmentAggregate;

/// <summary>
/// قرار های ملاقات
/// </summary>
public class Appointment
{
    public int Id { get; set; }
    /// <summary>
    /// شناسه پزشک
    /// </summary>
    public int DoctorId { get; set; }
    /// <summary>
    /// تاریخ
    /// </summary>
    public DateTime DateTime { get; set; }
    /// <summary>
    /// شناسه بیمار
    /// </summary>
    public int PatientId { get; set; }
    /// <summary>
    /// مدت زمان بر اساس دقیقه
    /// </summary>
    public int DurationMinutes { get; set; }


    //np
    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }


    public Appointment(int doctorId, DateTime dateTime, int patientId, int durationMinutes)
    {
        DoctorId = doctorId;
        DateTime = dateTime;
        PatientId = patientId;
        DurationMinutes = durationMinutes;
    }
    
}