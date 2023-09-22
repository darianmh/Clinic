namespace Clinic.Doctor.Data
{
    /// <summary>
    /// ماهیت پزشک
    /// </summary>
    public class Doctor
    {
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// رتبه
        /// عمومی یا متخصص
        /// </summary>
        public int DegreeId { get; set; }
        /// <summary>
        /// تخصص
        /// </summary>
        public int SpecialtyId { get; set; }


        //np
        public virtual Degree Degree { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
    }
}
