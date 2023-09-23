namespace Appointment.Domain;

public class SetAppointmentResponseDto
{
    public bool IsOk { get; set; }
    public int? AppointmentId { get; set; }
    public DateTime? StartDateTime { get; set; }
    /// <summary>
    /// Error reason
    /// </summary>
    public string? Description { get; set; }


    public SetAppointmentResponseDto(string description)
    {
        IsOk = false;
        Description = description;
    }

    public SetAppointmentResponseDto(int appointmentId, DateTime startDateTime)
    {
        AppointmentId = appointmentId;
        StartDateTime = startDateTime;
        IsOk = true;
    }
}