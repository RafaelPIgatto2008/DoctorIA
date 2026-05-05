namespace DoctorIA.Models;

public class PatientRequest
{
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public float? Temperature { get; set; }
    public List<string>? Symptoms { get; set; }
    public List<string>? ListHistoric { get; set; }
}