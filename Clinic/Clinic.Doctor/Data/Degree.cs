namespace Clinic.Doctor.Data;

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