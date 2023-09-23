using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.Contract.Services;
using Appointment.Contract.UseCases;
using Appointment.Domain;

namespace Appointment.UseCases
{
    public class AppointmentManager : IAppointmentManager
    {

        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        public AppointmentManager(IAppointmentService service, IDoctorService doctorService)
        {
            _appointmentService = service;
            _doctorService = doctorService;
        }



        public async Task<SetAppointmentResponseDto> SetAppointment(SetAppointmentRequestDto request, CancellationToken cancellationToken)
        {
            var doctor = _doctorService.GetById(request.DoctorId);
            if (doctor == null) return new SetAppointmentResponseDto("Doctor not found");

            var check = await _appointmentService.CanSetAppointment(request, doctor, cancellationToken);
            if (check)
            {
                var appointment =
                    new Domain.AppointmentAggregate.Appointment(request.DoctorId, request.StartDateTime,
                        request.PatientId, request.DurationMinutes);
                _appointmentService.AddAppointment(appointment, cancellationToken);
                return new SetAppointmentResponseDto(appointment.Id, appointment.DateTime);
            }
            return new SetAppointmentResponseDto("Selected time is invalid");
        }

        public SetAppointmentResponseDto SetEarliestAppointment(SetEarliestAppointmentRequestDto request,
            CancellationToken cancellationToken)
        {
            var doctor = _doctorService.GetById(request.DoctorId);
            if (doctor == null) return new SetAppointmentResponseDto("Doctor not found");

            var earliest = _appointmentService.FindEarliestAppointment(doctor, request.DurationMinutes);
            _appointmentService.AddAppointment(earliest, cancellationToken);
            return new SetAppointmentResponseDto(earliest.Id, earliest.DateTime);
        }
    }
}
