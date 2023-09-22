namespace Appointment.Domain
{
    /// <summary>
    /// قرار های ملاقات
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// شناسه پزشک
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// تاریخ
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// شناسه بیمار
        /// </summary>
        public int PatientId { get; set; }


        //np
        public virtual Patient Patient { get; set; } 
    }

    /// <summary>
    /// اطلاعات بیمار
    /// </summary>
    public class Patient
    {
        public int Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
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


        //np
        public virtual Degree Degree { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
    }

    /// <summary>
    /// رتبه پزشک
    /// </summary>
    public class Degree
    {
        public int Id { get; set; }
        /// <summary>
        /// عنوان رتبه
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// حداقل زمان ویزیت
        /// </summary>
        public int VisitMinTime { get; set; }
        /// <summary>
        /// حداکثر زمان ویزیت
        /// </summary>
        public int VisitMaxTime { get; set; }
    }
}
