namespace Clinic.Doctor.Data;

/// <summary>
/// تخصص پزشک
/// </summary>
public class Specialty
{
    public int Id { get; set; }
    /// <summary>
    /// عنوان تخصص
    /// داخلی یا قلب و ...
    /// </summary>
    public string Title { get; set; }
}