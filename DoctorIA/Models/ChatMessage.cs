namespace DoctorIA.Models;

public class ChatMessage
{
    public string Role { get; set; } = default!;
    public string Content { get; set; } = default!;
}