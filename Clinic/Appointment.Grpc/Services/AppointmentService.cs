using Appointment.Contract.UseCases;
using Appointment.Domain;
using Appointment.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Appointment.Grpc.Services
{
    public class AppointmentService : Protos.AppointmentService.AppointmentServiceBase
    {
        private readonly IAppointmentManager _appointmentManager;

        public AppointmentService(IAppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager;
        }


        public override async Task<SetAppointmentResponse> SetAppointment(SetAppointmentRequest request, ServerCallContext context)
        {
            var requestDto = Map(request);
            var response = await _appointmentManager.SetAppointment(requestDto, cancellationToken: context.CancellationToken);
            return Map(response);
        }

        public override Task<SetAppointmentResponse> SetEarliestAppointment(SetEarliestAppointmentRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var requestDto = Map(request);
                var response =
                    _appointmentManager.SetEarliestAppointment(requestDto,
                        cancellationToken: context.CancellationToken);
                return Map(response);
            });
        }






        private SetEarliestAppointmentRequestDto Map(SetEarliestAppointmentRequest response)
        {
            return new SetEarliestAppointmentRequestDto()
            {
                DoctorId = response.DoctorId,
                DurationMinutes = response.DurationMinutes,
                PatientId = response.PatientId
            };
        }
        private SetAppointmentRequestDto Map(SetAppointmentRequest response)
        {
            return new SetAppointmentRequestDto()
            {
                DoctorId = response.DoctorId,
                DurationMinutes = response.DurationMinutes,
                PatientId = response.PatientId,
                StartDateTime = response.StartDateTime!.ToDateTime().ToLocalTime()
            };
        }

        private SetAppointmentResponse Map(SetAppointmentResponseDto response)
        {
            return new SetAppointmentResponse()
            {
                StartDateTime = response.StartDateTime?.ToUniversalTime().ToTimestamp(),
                AppointmentId = response.AppointmentId,
                IsOk = response.IsOk
            };
        }
    }
}
