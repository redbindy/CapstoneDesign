using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SQLite;
using System.Diagnostics;
using System.Xml.Linq;

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

        public virtual void OnGet()
        {
            string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string cookie = (string)ViewData["UserCookie"];
            string datetime = DateTime.UtcNow.AddHours(9).ToString();

            string query;
            if (cookie != null)
            {
                query = $"insert into VisitLog (ip, cookie, datetime) values(\'{remoteIpAddress}\', \'{cookie}\', \'{datetime}\')";
            }
            else
            {
                query = $"insert into VisitLog (ip, datetime) values(\'{remoteIpAddress}\', \'{datetime}\')";
            }

            Database.Database db = Database.Database.Instance;

            try
            {
                if (!db.Insert(query))
                {
                    Console.WriteLine("cannot insert user data to db");
                }
            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }
    }
}
