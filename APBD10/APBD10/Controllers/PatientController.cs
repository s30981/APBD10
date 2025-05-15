using Microsoft.AspNetCore.Mvc;
using APBD10.Services;
using APBD10.DTOs;

namespace APBD10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET api/patient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDtos>> GetPatientDetails(int id)
        {
            var patientDetails = await _patientService.GetPatientDetailsAsync(id);

            if (patientDetails == null)
            {
                return NotFound(new { Message = "Patient not found." });
            }

            return Ok(patientDetails);
        }
    }
}