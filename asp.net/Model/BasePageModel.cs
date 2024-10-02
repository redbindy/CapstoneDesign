using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Capstone.Model
{
    public abstract class BasePageModel : PageModel
    {
        public int PageNumber { get; protected set; }

        protected BasePageModel(int maxPageCount, int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            PageNumber = pageNumber;
        }

        protected void setPageInfos(int maxPageCount, int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            ViewData["MaxPageCount"] = maxPageCount;
            PageNumber = pageNumber;
        }
    }
}
