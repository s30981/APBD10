using APBD10.DTOs;
using APBD10.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD10.Services;

public interface IPrescriptionService
{
    Task<bool> DoctorExistsAsync(int doctorId);
    Task<bool> MedicamentExistsAsync(int medicamentId);
    Task<Patient> GetOrCreatePatientAsync(PatientDto patientDto);
    Task AddPrescriptionAsync(Prescription prescription);
    Task AddPrescriptionMedicamentsAsync(IEnumerable<PrescriptionMedicament> meds);
}