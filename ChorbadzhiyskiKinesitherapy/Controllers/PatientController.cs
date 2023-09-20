using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChorbadzhiyskiKinesitherapy.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientsService patientsService;

        public PatientController(PatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        public async Task<IActionResult> Index()
        {
            var patientsData = await patientsService.GetAsync();
            return View(patientsData);
        }

        public IActionResult AddPatient()
        {
            return PartialView(new PatientViewModel());
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await patientsService.GetAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, MobileNumber, Birthday, Address, Diagnose, FirstAppointment")] PatientViewModel newPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await patientsService.CreateAsync(newPatient);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, [Bind("Name, MobileNumber, Birthday, Address, Diagnose, FirstAppointment")] PatientViewModel updatedPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            updatedPatient.Id = id;

            await patientsService.UpdateAsync(id, updatedPatient);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await patientsService.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}
