using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChorbadzhiyskiKinesitherapy.Controllers
{
	[ApiController]
	[Route("api/patient")]
	public class PatientsController : ControllerBase
	{
		private readonly PatientsService _patientsService;

		public PatientsController(PatientsService patientsService) =>
			_patientsService = patientsService;

		[HttpGet]
		public async Task<List<Patient>> Get() =>
			await _patientsService.GetAsync();

		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult<Patient>> Get(string id)
		{
			var patient = await _patientsService.GetAsync(id);

			if (patient is null)
			{
				return NotFound();
			}

			return patient;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Patient newPatient)
		{
			await _patientsService.CreateAsync(newPatient);

			return CreatedAtAction(nameof(Get), new { id = newPatient.Id }, newPatient);
		}

		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> Update(string id, Patient updatedPatient)
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

		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> Delete(string id)
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
