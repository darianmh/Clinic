using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Domain
{
    public class SetAppointmentRequestDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDateTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}
