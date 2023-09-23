using System.ComponentModel.DataAnnotations;

namespace Clinic.Api.ViewModel
{
    public class SetAppointmentRequestViewModel
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public int DurationMinutes { get; set; }
    }
}
