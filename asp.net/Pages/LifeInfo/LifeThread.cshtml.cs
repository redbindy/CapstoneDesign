using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace Capstone.Pages.LifeInfo
{
    public class LifeThreadModel : BaseMultiPageModel
    {
        protected override void updateEntities()
        {
            mEntities.Clear();

            Database.Database db = Database.Database.Instance;

            string query = $"select * from Post order by Time desc limit 36 offset {(PageNumber - 1) * CELL_COUNT}";
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return;
                }

                while (dbReader.Read())
                {
                    LifeThreadEntity entity = new LifeThreadEntity(
                        id: dbReader.GetInt64(0),
                        title: dbReader.GetString(1),
                        time: dbReader.GetString(3),
                        userName: dbReader.GetString(4)
                        );

                    mEntities.Add(entity);
                }
            }
        }

        private class LifeThreadEntity : BaseEntity
        {
            private readonly long mID;
            private readonly string mTitle;
            private readonly string mTime;
            private readonly string mUserName;

            public LifeThreadEntity(long id, string title, string time, string userName)
            {
                Debug.Assert(!string.IsNullOrEmpty(userName));
                Debug.Assert(!string.IsNullOrEmpty(title));

                mID = id;
                mTitle = title;
                mTime = time;
                mUserName = userName;
            }

            public override string ShowData()
            {
                StringBuilder sb = new StringBuilder(Program.DEFAULT_CAPACITY);

                sb.AppendLine("<div class=\"grid-item\">");
                sb.AppendLine($"<a href=\"/LifeInfo/Post?postId={mID}\" class=\"card\">");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{mTitle}</h5>");
                sb.AppendLine($"<p class=\"card-text\">작성자: {mUserName}</p>");
                sb.AppendLine($"<p class=\"card-text\">작성 날짜: {mTime}</p>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }
        }
    }
}
