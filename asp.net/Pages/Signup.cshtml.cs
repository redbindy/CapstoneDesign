using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Pages
{
    public class SignupModel : PageModel
    {
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string ID { get; set; }

        [StringLength(100, MinimumLength = 16)]
        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "이름을 입력해주세요.")]
        public string Name { get; set; }

        [Required]
        public string UserType { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {

        }
    }
}
