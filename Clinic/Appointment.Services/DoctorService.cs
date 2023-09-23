using Appointment.Contract.Repositories;
using Appointment.Contract.Services;
using Appointment.Domain.DoctorAggregate;

namespace Appointment.Services;

public class DoctorService : IDoctorService
{
    private  readonly IDoctorRepository _repository;
    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public Doctor? GetById(int doctorId)
    {
        return _repository.GetById(doctorId);
    }
}