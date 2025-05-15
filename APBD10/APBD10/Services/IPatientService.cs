

using APBD10.DTOs;

namespace APBD10.Services;

public interface IPatientService
{
    Task<PatientDtos> GetPatientDetailsAsync(int patientId);
}