using lab9.Exceptions;
using lab9.Service.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using lab9.DTO;

namespace lab9.Controller
{


    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDTO prescriptionDTO)
        {
            try
            {
                await _prescriptionService.AddPrescription(prescriptionDTO);
                return Ok("Prescription added successfully.");
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }



   [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientDetails(int patientId)
    {
        try
        {
            var patient = await _prescriptionService.GetPatientDetails(patientId);
            return Ok(patient);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    }
}
