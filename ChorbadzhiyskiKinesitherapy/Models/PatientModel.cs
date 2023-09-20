using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChorbadzhiyskiKinesitherapy.Models
{
    public class PatientModel : PageModel
    {
        [BindProperty]
        public PatientViewModel Patient { get; set; }

        private void test()
        {
            var testinn = Url;
        }
    }
}
