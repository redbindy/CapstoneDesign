using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages
{
    public class MyPageModel : BasePageModel
    {
        public MyPageModel()
            : base(-1, 1)
        {
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["LoginState"] != "true")
            {
                return RedirectToPage("Login");
            }

            return Page();
        }
    }
}
