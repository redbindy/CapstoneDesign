using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Capstone.Pages
{
    public class MyPageModel : PageModel
    {
        public string? Name { get; private set; }
        public string? DisplayingUserType { get; private set; }

        public MyPageModel()
        {
        }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("UserCookie"))
            {
                return RedirectToPage("Login");
            }

            string? userInfo = Request.Cookies["UserCookie"];
            Debug.Assert(userInfo != null);

            string[] userInfos = userInfo.Split();

            Name = userInfos[0];
            DisplayingUserType = userInfos[1];

            return Page();
        }
    }
}
