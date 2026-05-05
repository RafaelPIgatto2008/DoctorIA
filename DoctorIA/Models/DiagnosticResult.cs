namespace DoctorIA.Models;

public class DiagnosticResult
{
    public List<string> Hyphoteses { get; set; } = new();
    public List<string> Suggestions { get; set; } = new();
    public string DangerLevel { get; set; } = default!;
}