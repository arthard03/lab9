using lab9.Services;


using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lab9.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsService _patientsService;

        public  PatientsController(PatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientData(int id)
        {
            var patientDto = await _patientsService.GetPatientData(id);

            if (patientDto == null)
                return NotFound();

            return Ok(patientDto);
        }
    }
}
