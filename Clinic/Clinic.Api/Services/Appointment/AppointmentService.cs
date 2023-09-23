using Appointment.Grpc.Protos;
using Clinic.Api.Infrastructure.Appointment;
using Clinic.Api.ViewModel;
using Google.Protobuf.WellKnownTypes;

namespace Clinic.Api.Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly global::Appointment.Grpc.Protos.AppointmentService.AppointmentServiceClient _serviceClient;
    public AppointmentService(global::Appointment.Grpc.Protos.AppointmentService.AppointmentServiceClient serviceClient)
    {
        _serviceClient = serviceClient;
    }


    public async Task<SetAppointmentResponseViewModel> SetAppointment(SetAppointmentRequestViewModel request, CancellationToken cancellationToken)
    {
        var requestDto = Map(request);
        var response = await _serviceClient.SetAppointmentAsync(requestDto, cancellationToken: cancellationToken);
        return Map(response);
    }

    public async Task<SetAppointmentResponseViewModel> SetEarliestAppointment(SetEarliestAppointmentRequestViewModel request, CancellationToken cancellationToken)
    {
        var requestDto = Map(request);
        var response = await _serviceClient.SetEarliestAppointmentAsync(requestDto,
                cancellationToken: cancellationToken);
        return Map(response);
    }




    private SetEarliestAppointmentRequest Map(SetEarliestAppointmentRequestViewModel response)
    {
        return new SetEarliestAppointmentRequest()
        {
            DoctorId = response.DoctorId,
            DurationMinutes = response.DurationMinutes,
            PatientId = response.PatientId
        };
    }
    private SetAppointmentRequest Map(SetAppointmentRequestViewModel response)
    {
        return new SetAppointmentRequest()
        {
            DoctorId = response.DoctorId,
            DurationMinutes = response.DurationMinutes,
            PatientId = response.PatientId,
            StartDateTime = response.StartDateTime.ToUniversalTime().ToTimestamp()
        };
    }

    private SetAppointmentResponseViewModel Map(SetAppointmentResponse response)
    {
        return new SetAppointmentResponseViewModel()
        {
            StartDateTime = response.StartDateTime?.ToDateTime().ToLocalTime(),
            AppointmentId = response.AppointmentId,
            IsOk = response.IsOk
        };
    }
}