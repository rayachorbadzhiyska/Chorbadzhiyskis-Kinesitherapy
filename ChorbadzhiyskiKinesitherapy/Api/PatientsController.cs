using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChorbadzhiyskiKinesitherapy.Api
{
    [ApiController]
    [Route("api/patient")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsService _patientsService;

        public PatientsController(PatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet]
        public async Task<List<PatientViewModel>> Get()
        {
            return await _patientsService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientViewModel>> Get(Guid id)
        {
            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PatientViewModel newPatient)
        {
            await _patientsService.CreateAsync(newPatient);

            return CreatedAtAction(nameof(Get), new { id = newPatient.Id }, newPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PatientViewModel updatedPatient)
        {
            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            updatedPatient.Id = patient.Id;

            await _patientsService.UpdateAsync(id, updatedPatient);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            await _patientsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
