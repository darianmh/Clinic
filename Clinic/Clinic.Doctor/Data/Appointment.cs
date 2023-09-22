namespace Clinic.Doctor.Data;

/// <summary>
/// قرار های ملاقات
/// </summary>
public class Appointment
{
    /// <summary>
    /// شناسه پزشک
    /// </summary>
    public int DoctorId { get; set; }
    /// <summary>
    /// تاریخ
    /// </summary>
    public DateTime DateTime { get; set; }
}