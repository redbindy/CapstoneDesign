using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages
{
    public class MyPageModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "마이페이지";
        }
    }
}
