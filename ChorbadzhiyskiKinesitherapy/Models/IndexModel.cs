using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Globalization;

namespace ChorbadzhiyskiKinesitherapy.Models
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<PatientViewModel> Patients { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty]
        public int TotalRecords { get; set; }

        public IndexModel(IEnumerable<PatientViewModel> patients, int pageNo, int pageSize)
        {
            Patients = patients;
            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecords = patients.Count();
        }

        public void SetPatients(IEnumerable<PatientViewModel> patients)
        {
            Patients = patients.Skip((PageNo - 1) * PageSize).Take(PageSize);
            TotalRecords = patients.Count();
        }

        public void OnGet(IEnumerable<PatientViewModel> patients, int page = 1, int size = 10)
        {
            Patients = patients.Skip((page - 1) * size).Take(size);

            TotalRecords = patients.Count();
            PageNo = page;
            PageSize = size;
        }
    }
}
