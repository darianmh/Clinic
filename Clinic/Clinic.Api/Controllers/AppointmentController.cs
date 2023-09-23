using System.Net;
using Appointment.Grpc.Protos;
using Clinic.Api.Infrastructure.Appointment;
using Clinic.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(SetAppointmentResponseViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetAppointment(SetAppointmentRequestViewModel appointment, CancellationToken cancellationToken)
        {
            var result = await _appointmentService.SetAppointment(appointment, cancellationToken);
            return result.IsOk ? Ok(result) : StatusCode(400, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(SetAppointmentResponseViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetEarliestAppointment(SetEarliestAppointmentRequestViewModel appointment, CancellationToken cancellationToken)
        {
            var result = await _appointmentService.SetEarliestAppointment(appointment, cancellationToken);
            return result.IsOk ? Ok(result) : StatusCode(400, result);
        }

    }
}
