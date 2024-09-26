using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages
{
    public class InfraStructure : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "인프라구축";
        }
    }
}
