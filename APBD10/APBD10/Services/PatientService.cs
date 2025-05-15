using APBD10.Data;
using APBD10.DTOs;
using Microsoft.EntityFrameworkCore;


namespace APBD10.Services;

public class PatientService : IPatientService
{
    private readonly PrescriptionDbContext _context;

    public PatientService(PrescriptionDbContext context)
    {
        _context = context;
    }
    public async Task<PatientDtos?> GetPatientDetailsAsync(int patientId)
    {
        Models.Patient? patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
            return null;

        return new PatientDtos
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BrithDate,

            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionDtos
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDtos
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Dose = pm.Dose ?? 0
                    }).ToList(),
                    Doctor = new DoctorDtos
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName
                    }
                }).ToList()
        };
    }

}
