using System.Text.Json.Serialization;

namespace Clinic.Api.ViewModel;

public class SetAppointmentResponseViewModel
{
    [JsonIgnore]
    public bool IsOk { get; set; }
    public int? AppointmentId { get; set; }
    public DateTime? StartDateTime { get; set; }
    /// <summary>
    /// Error reason
    /// </summary>
    public string? Description { get; set; }
}