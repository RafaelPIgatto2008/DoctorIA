namespace DoctorIA.Models;

public class ConsultationRecord : BaseRecord
{
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public float? Temperature { get; set; }
    public string? SymptomsJson { get; set; }
    public string? ListHistoricJson { get; set; }
    public string? AiResponseJson { get; set; }
}