using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstone.Pages.LifeInfo
{
    public class LifeThreadModel : BasePageModel
    {
        public LifeThreadModel()
            : base(1, 1)
        {
        }

        public void OnGet()
        {
            setPageInfos(1, 1);
        }
    }
}
