using System.Linq.Expressions;
using Appointment.Contract;
using Appointment.Contract.Repositories;
using Appointment.Frameworks.EfCore;

namespace Appointment.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentContext _context;
        public IUnitOfWork UnitOfWork => _context;


        public void Add(Domain.AppointmentAggregate.Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }

        public List<Domain.AppointmentAggregate.Appointment> GetAllAppointmentForPatient(int patientId, DateTime startDate, Expression<Func<Domain.AppointmentAggregate.Appointment, object>>? order = null)
        {
            var query = _context.Appointments.Where(x => x.PatientId == patientId
            && x.DateTime.Date == startDate);

            if (order != null)
                query = query.OrderByDescending(order);

            return query.ToList();
        }

        public List<Domain.AppointmentAggregate.Appointment> GetDoctorAppointments(int doctorId, DateTime date,
            Expression<Func<Domain.AppointmentAggregate.Appointment, object>>? order = null)
        {
            var query = _context.Appointments
                   .Where(x => x.DoctorId == doctorId && x.DateTime.Date == date);

            if (order != null)
                query = query.OrderByDescending(order);

            return query.ToList();
        }

        public AppointmentRepository(AppointmentContext context)
        {
            _context = context;
        }




    }
}
