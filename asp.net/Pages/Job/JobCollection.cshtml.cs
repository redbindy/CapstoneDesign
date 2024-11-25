using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Capstone.Pages.Job
{
    public class JobModel : BaseMultiPageModel
    {
        protected override void updateEntities()
        {
            mEntities.Clear();

            string today = DateTime.UtcNow.AddHours(9).ToString("yy'/'MM'/'dd");

            string query = $"select Recruitor, Title, DueDate, Url, Pay from Job where DueDate >= \'{today}\' order by DueDate limit 36 offset {(PageNumber - 1) * CELL_COUNT}";

            Database.Database db = Database.Database.Instance;
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return;
                }

                while (dbReader.Read())
                {
                    string recruitor = dbReader.GetString(0);
                    string title = dbReader.GetString(1);
                    string dueDate = dbReader.GetString(2);
                    string url = dbReader.GetString(3);
                    string pay = dbReader.GetString(4);

                    JobEntity entity = new JobEntity(
                        recruitor: recruitor, 
                        title: title, 
                        dueDate: dueDate, 
                        url: url, 
                        pay: pay);

                    mEntities.Add(entity);
                }
            }
        }

        private class JobEntity : BaseEntity
        {
            private readonly string mRecruitor;
            private readonly string mTitle;
            private readonly string mDueDate;
            private readonly string mUrl;
            private readonly string mPay;

            public JobEntity(string recruitor, string title, string dueDate, string url, string pay)
            {
                Debug.Assert(recruitor != null);
                Debug.Assert(title != null);
                Debug.Assert(dueDate != null);
                Debug.Assert(url != null);
                Debug.Assert(pay != null);

                mRecruitor = recruitor;
                mTitle = title;
                mDueDate = dueDate;
                mUrl = url;
                mPay = pay;
            }

            public override string ShowData()
            {
                StringBuilder sb = new StringBuilder(Program.DEFAULT_CAPACITY);

                sb.AppendLine("<div class=\"grid-item\">");
                sb.AppendLine($"<a href = \"{mUrl}\" class=\"card\">");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{mTitle}</h5>");
                sb.AppendLine($"<p class=\"card-text\">마감일: {mDueDate}</p>");
                sb.AppendLine($"<p class=\"card-text\">회사: {mRecruitor}</p>");
                sb.AppendLine($"<p class=\"card-text\">급여: {mPay}</p>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }
        }
    }
}
