using Capstone.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Capstone.Pages
{
    public class LoginModel : BasePageModel
    {
        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel()
            : base(-1, 1)
        {
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ID)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
                );

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddSeconds(10)
            };
            Response.Cookies.Append("LoginState", "true", cookieOptions);

            return RedirectToPage("/MyPage");

            // return Page();
        }
    }
}
