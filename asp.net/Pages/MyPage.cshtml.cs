using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Capstone.Pages
{
    public class MyPageModel : PageModel
    {
        public string? Name { get; private set; }
        public string? DisplayingUserType { get; private set; }

        public MyPageModel()
        {
        }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("UserCookie"))
            {
                return RedirectToPage("Login");
            }

            string? userInfo = Request.Cookies["UserCookie"];
            Debug.Assert(userInfo != null);

            string[] userInfos = userInfo.Split();

            Name = userInfos[0];
            DisplayingUserType = userInfos[1];

            Database.Database db = Database.Database.Instance;

            string query = $"select PostID, title from Post where UserInfo = \'{userInfo}\'";
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                List<ListEntity> posts = new List<ListEntity>(Program.DEFAULT_CAPACITY);

                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return Page();
                }

                while (dbReader.Read())
                {
                    int id = dbReader.GetInt32(0);
                    string title = dbReader.GetString(1);

                    ListEntity entity = new ListEntity(id, title);
                    posts.Add(entity);
                }

                ViewData["Posts"] = posts;
            }

            query = $"select PostID, body from Comment where UserInfo = \'{userInfo}\'";
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                List<ListEntity> comments = new List<ListEntity>(Program.DEFAULT_CAPACITY);

                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return Page();
                }

                while (dbReader.Read())
                {
                    int id = dbReader.GetInt32(0);
                    string body = dbReader.GetString(1);

                    ListEntity entity = new ListEntity(id, body);
                    comments.Add(entity);
                }

                ViewData["Comments"] = comments;
            }

            return Page();
        }
    }

    public class ListEntity : BaseEntity
    {
        private readonly int mID;
        private readonly string mTitle;

        public ListEntity(int id, string title)
        {
            Debug.Assert(id >= 0);
            Debug.Assert(!string.IsNullOrEmpty(title));

            mID = id;
            mTitle = title;
        }

        public override string ShowData()
        {
            return $"<li><a href=\"/LifeInfo/Post?postId={mID}\" class=\"card\">{mTitle}</a></li>";
        }
    }
}