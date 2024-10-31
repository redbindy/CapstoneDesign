using Capstone.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;

namespace Capstone.Pages
{
    public class LoginModel : BaseAccountModel
    {
        [BindProperty]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string? ID { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
        public string? Password { get; set; }

        public override void OnGet()
        {
            base.OnGet();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Database.Database db = Database.Database.Instance;

            string query = $"select * from User where ID='{ID}'";
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return Page();
                }

                if (!dbReader.Read())
                {
                    ModelState.AddModelError("ID", "서비스 가입이 필요합니다.");
                    return Page();
                }

                string id = dbReader.GetString(0);
                string passwordHash = dbReader.GetString(1);

                Debug.Assert(Password != null);
                string inputPasswordHash = getPasswordHash(Password);

                if (ID != id || inputPasswordHash != passwordHash)
                {
                    ModelState.AddModelError("ID", "ID 혹은 비밀번호를 확인해주세요.");
                    return Page();
                }

                string name = dbReader.GetString(2);
                EUserType eUserType = (EUserType)dbReader.GetInt32(3);

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, name)
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
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };

                string userTypeStr = "";
                switch (eUserType)
                {
                    case EUserType.LOCAL:
                        userTypeStr = "(도민)";
                        break;

                    case EUserType.NON_LOCAL:
                        userTypeStr = "(도외민)";
                        break;

                    case EUserType.SELF_EMPLOYED:
                        userTypeStr = "(자영업자)";
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }

                Response.Cookies.Append("UserCookie", $"{name} {userTypeStr}", cookieOptions);
            }

            return RedirectToPage("/MyPage");
        }
    }
}
