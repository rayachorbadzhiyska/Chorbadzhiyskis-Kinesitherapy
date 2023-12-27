using ChorbadzhiyskiKinesitherapy.Models;
using ChorbadzhiyskiKinesitherapy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ChorbadzhiyskiKinesitherapy.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientsService patientsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;

        public PatientController(
            PatientsService patientsService,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager
            )
        {
            this.patientsService = patientsService ?? throw new ArgumentNullException(nameof(patientsService));
            this.userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));

            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            // Disable password validation so that the EGN can be used as a password.
            this.userManager.PasswordValidators.Clear();
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            if (page != null && page < 1)
            {
                page = 1;
            }

            var pageSize = 10;

            var patientsData = await patientsService.GetAsync();
            var pagedPatientsData = patientsData.ToPagedList(page ?? 1, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // This is an AJAX request, return the partial view without layout
                return PartialView("Index", pagedPatientsData);
            }
            else
            {
                // This is a regular request, return the full view with layout
                return View(pagedPatientsData);
            }
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

        public IActionResult Add()
        {
            PatientViewModel newPatient = new();

            return View(newPatient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, MobileNumber, EGN, Birthday, Address, Diagnose, FirstAppointment, Notes")] PatientViewModel newPatient)
        {
            if (ModelState.IsValid)
            {
                await patientsService.CreateAsync(newPatient);

                var user = CreateUser();

                await userStore.SetUserNameAsync(user, newPatient.MobileNumber, CancellationToken.None);
                var result = await userManager.CreateAsync(user, newPatient.MobileNumber);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var patient = await patientsService.GetAsync(id);

            return View(patient);
        }

        public async Task<IActionResult> EditPartial(string id)
        {
            var patient = await patientsService.GetAsync(id);

            return PartialView("Edit", patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, [Bind("Name, MobileNumber, EGN, Birthday, Address, Diagnose, FirstAppointment, Notes")] PatientViewModel updatedPatient)
        {
            if (ModelState.IsValid)
            {
                updatedPatient.Id = id;

                await patientsService.UpdateAsync(id, updatedPatient);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var patient = await patientsService.GetAsync(id);

            return View(patient);
        }

        public async Task<IActionResult> DeletePartial(string id)
        {
            var patient = await patientsService.GetAsync(id);

            return PartialView("Delete", patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await patientsService.RemoveAsync(id);

            return RedirectToAction("Index");
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
