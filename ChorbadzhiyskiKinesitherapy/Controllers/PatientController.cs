using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ChorbadzhiyskiKinesitherapy.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientsService patientsService;

        public PatientController(PatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var pageSize = 20;

            var patientsData = await patientsService.GetAsync();
            var pagedPatientsData = patientsData.ToPagedList(page ?? 1, pageSize);

            return View(pagedPatientsData);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Get(string id)
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
        public async Task<IActionResult> Update(string id, [Bind("Name, MobileNumber, Birthday, Address, Diagnose, FirstAppointment")] PatientViewModel updatedPatient)
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
        public async Task<IActionResult> Delete(string id)
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
