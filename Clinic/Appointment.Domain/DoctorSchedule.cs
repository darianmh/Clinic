namespace Appointment.Domain;

/// <summary>
/// زمان بندی هفتگی حضور دکتر در درمانگاه
/// </summary>
public class DoctorSchedule
{
    public int Id { get; set; }
    /// <summary>
    /// شناسه دکتر
    /// </summary>
    public int DoctorId { get; set; }
    /// <summary>
    /// شماره روزهفته
    /// </summary>
    public int DayOfWeek { get; set; }
    /// <summary>
    /// ساعت حضور
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// ساعت پایان حضور
    /// </summary>
    public DateTime EndTime { get; set; }
}