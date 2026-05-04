namespace DoctorIA.Models;

public class ConsultationResult : BaseRecord
{
    List<string> Hyphoteses { get; set; }
    List<string> Suggestions { get; set; }
    public string Disclaimer { get; set; }

    public static ConsultationResult Create(List<string> hyphoteses,
        List<string> suggestions)
    {
        var result = new ConsultationResult()
        {
            Hyphoteses = hyphoteses ?? throw new NullReferenceException("Hyphoteses are null"),
            Suggestions = suggestions ?? throw new NullReferenceException("Don´t have a suggestion"),
            Disclaimer =
                "This is an AI-generated suggestion for educational purposes only. It does not replace professional medical evaluation"
        };
        
        return result;
    }
}