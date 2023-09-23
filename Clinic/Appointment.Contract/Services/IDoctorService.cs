using Appointment.Domain.DoctorAggregate;

namespace Appointment.Contract.Services;

public interface IDoctorService
{
    Doctor? GetById(int doctorId);
}