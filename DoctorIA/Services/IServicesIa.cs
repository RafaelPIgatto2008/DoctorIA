using DoctorIA.Models;

namespace DoctorIA.Services;

public interface IServicesIa
{
    Task<PatientRequest> GetPatientResumeById(Guid id, CancellationToken cancellationToken);
    Task<ConsultationResult> GetConsultationResultById(Guid id, CancellationToken cancellationToken);
}