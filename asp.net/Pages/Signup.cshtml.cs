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
        [Required(ErrorMessage = "���̵� �Է����ּ���.")]
        public string? ID { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "��й�ȣ�� �Է����ּ���."), MinLength(8, ErrorMessage = "�ּ� 8�ڸ� �Է����ּ���.")]
        public string? Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "�̸��� �Է����ּ���.")]
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
