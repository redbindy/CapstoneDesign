using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages.Job
{
    public class JobPagesModel : BasePageModel
    {
        public JobPagesModel() 
            : base(-1, 1)
        {
        }

        public override void OnGet()
        {
            base.OnGet();
        }
    }
}
