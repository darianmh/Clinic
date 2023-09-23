using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.Contract;
using Appointment.Domain.AppointmentAggregate;
using Appointment.Domain.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Frameworks.EfCore
{
    public class AppointmentContext : DbContext, IUnitOfWork
    {
        public virtual DbSet<Domain.AppointmentAggregate.Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options)
        {

        }
    }
}
