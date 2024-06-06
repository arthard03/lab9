using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using lab9.DTO;
using lab9.Exceptions;
using lab9.Services;
using lab9.Services.ServiceInterfaces;

namespace lab9.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(AddPrescriptionRequestDto request)
        {
            try
            {
                var prescriptionId = await _prescriptionService.AddPrescriptionAsync(request);
                return Ok(new { Id = prescriptionId });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}