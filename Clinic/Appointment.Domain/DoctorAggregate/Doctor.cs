namespace Appointment.Domain.DoctorAggregate
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
        /// حداقل زمان ویزیت
        /// </summary>
        public int VisitMinTime { get; set; }
        /// <summary>
        /// حداکثر زمان ویزیت
        /// </summary>
        public int VisitMaxTime { get; set; }

        /// <summary>
        /// ماکزیمم تعداد همپوشانی قرار ملاقات ها
        /// </summary>
        public int MaxOverlap { get; set; }

        //np
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
    }
}
