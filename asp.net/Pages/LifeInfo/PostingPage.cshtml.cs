using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Capstone.Pages.LifeInfo
{
    public class PostingPageModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "제목을 입력해주세요.")]
        public string? Title { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "내용을 입력해주세요.")]
        public string? Body { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Random random = new Random(2024);
            long id = random.NextInt64();

            string? userInfo = Request.Cookies["UserCookie"];
            Debug.Assert(userInfo != null);

            string query = $"insert into Post (Title, Body, Time, UserInfo) values('{Title}', '{Body}', '{DateTime.UtcNow.AddHours(9).ToString()}','{userInfo}')";

            Database.Database db = Database.Database.Instance;

            if (!db.Insert(query))
            {
                Console.WriteLine("cannot insert user data to db");
            }

            return RedirectToPage("LifeThread");
        }
    }
}
