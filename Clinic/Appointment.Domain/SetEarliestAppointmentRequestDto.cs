namespace Appointment.Domain;

public class SetEarliestAppointmentRequestDto
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int DurationMinutes { get; set; }
}