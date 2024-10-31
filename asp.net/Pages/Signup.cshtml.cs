using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Capstone.Pages
{
    public class SignupModel : BaseAccountModel
    {
        [BindProperty]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string? ID { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "비밀번호를 입력해주세요."), MinLength(8, ErrorMessage = "최소 8자리 입력해주세요.")]
        public string? Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "이름을 입력해주세요.")]
        public string? Name { get; set; }

        [BindProperty]
        public string? UserType { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Debug.Assert(Password != null);
            string passwordHash = getPasswordHash(Password);

            Database.Database db = Database.Database.Instance;

            string query = $"insert into User values('{ID}', '{passwordHash}', '{Name}', {UserType})";

            if (!db.Insert(query))
            {
                Console.WriteLine("cannot insert user data to db");
            }

            return RedirectToPage("Login");
        }
    }
}
