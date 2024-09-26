using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages.LifeInfo
{
    public class LifeThreadModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "생활 게시판";
        }
    }
}
