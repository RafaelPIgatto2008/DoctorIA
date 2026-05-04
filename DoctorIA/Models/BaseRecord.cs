namespace DoctorIA.Models;

public abstract class BaseRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateRequest { get; set; } = DateTime.UtcNow;
}