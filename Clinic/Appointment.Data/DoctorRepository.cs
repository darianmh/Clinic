using Appointment.Contract;
using Appointment.Contract.Repositories;
using Appointment.Domain.DoctorAggregate;
using Appointment.Frameworks.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppointmentContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public DoctorRepository(AppointmentContext context)
    {
        _context = context;
    }


    public Doctor? GetById(int doctorId)
    {
        return _context.Doctors.Include(x => x.DoctorSchedules)
            .FirstOrDefault(x => x.Id == doctorId);
    }
}