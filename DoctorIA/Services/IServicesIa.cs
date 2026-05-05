using DoctorIA.Models;

namespace DoctorIA.Services;

public interface IServicesIa
{
    Task<string> GetDiagnosticsAsync(int userId, PatientRequest request, CancellationToken cancellationToken);
    Task<string> GetHistoricAsync(int userId, CancellationToken cancellationToken);
}