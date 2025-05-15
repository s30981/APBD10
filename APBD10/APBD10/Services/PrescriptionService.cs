using APBD10.Data;
using APBD10.DTOs;
using APBD10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly PrescriptionDbContext _context;

    public PrescriptionService(PrescriptionDbContext context)
    {
        _context = context;
    }

    

    public async Task<bool> DoctorExistsAsync(int doctorId)
    {
        return await _context.Doctors.AnyAsync(d => d.IdDoctor == doctorId);
    }

    public async Task<bool> MedicamentExistsAsync(int medicamentId)
    {
        return await _context.Medicaments.AnyAsync(m => m.IdMedicament == medicamentId);
    }

    public async Task<Patient> GetOrCreatePatientAsync(PatientDto patientDto)
    {
        var patient = await _context.Patients.FindAsync(patientDto.IdPatient);
        if (patient != null)
            return patient;

        var newPatient = new Patient
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            BrithDate = patientDto.BirthDate
        };

        _context.Patients.Add(newPatient);
        await _context.SaveChangesAsync();

        return newPatient;
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task AddPrescriptionMedicamentsAsync(IEnumerable<PrescriptionMedicament> meds)
    {
        await _context.PrescriptionMedicaments.AddRangeAsync(meds);
        await _context.SaveChangesAsync();
    }
}