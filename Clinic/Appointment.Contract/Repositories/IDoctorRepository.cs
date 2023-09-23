using Appointment.Domain.DoctorAggregate;

namespace Appointment.Contract.Repositories;

public interface IDoctorRepository
{
    IUnitOfWork UnitOfWork { get; }
    Doctor? GetById(int doctorId);
}