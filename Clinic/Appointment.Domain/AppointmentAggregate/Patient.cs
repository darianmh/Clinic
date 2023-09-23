namespace Appointment.Domain.AppointmentAggregate;

/// <summary>
/// اطلاعات بیمار
/// </summary>
public class Patient
{
    public int Id { get; set; }
    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// تاریخ تولد
    /// </summary>
    public DateTime BirthDate { get; set; }
}