namespace Clinic.Api.ViewModel;

public class SetEarliestAppointmentRequestViewModel
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int DurationMinutes { get; set; }
}