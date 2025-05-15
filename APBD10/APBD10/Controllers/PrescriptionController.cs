using APBD10.DTOs;
using APBD10.Models;
using APBD10.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD10.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] NewPrescriptionRequest request)
    {
        if (request.DueDate < request.Date)
            return BadRequest("DueDate must be >= Date.");

        if (request.Medicaments.Count > 10)
            return BadRequest("Prescription cannot contain more than 10 medicaments.");

        if (!await _service.DoctorExistsAsync(request.IdDoctor))
            return BadRequest("Doctor not found.");

        foreach (var med in request.Medicaments)
        {
            if (!await _service.MedicamentExistsAsync(med.IdMedicament))
                return BadRequest($"Medicament with ID {med.IdMedicament} not found.");
        }

        var patient = await _service.GetOrCreatePatientAsync(request.Patient);

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            PatientId = patient.IdPatient,
            DoctorId = request.IdDoctor
        };

        await _service.AddPrescriptionAsync(prescription);

        var prescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = ""
        });

        await _service.AddPrescriptionMedicamentsAsync(prescriptionMedicaments);

        return StatusCode(201);
    }
}
