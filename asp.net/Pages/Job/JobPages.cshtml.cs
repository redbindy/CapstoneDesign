using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;

namespace Capstone.Pages.Job
{
    public class JobPagesModel : BaseMultiPageModel
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

                    JobPageEntity entity = new JobPageEntity(
                        recruitor: recruitor,
                        title: title,
                        dueDate: dueDate,
                        url: url,
                        pay: pay);

                    mEntities.Add(entity);
                }
            }
        }

        private class JobPageEntity : BaseEntity
        {
            private readonly string mRecruitor;
            private readonly string mTitle;
            private readonly string mDueDate;
            private readonly string mUrl;
            private readonly string mPay;

            public JobPageEntity(string recruitor, string title, string dueDate, string url, string pay)
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

                sb.AppendLine("<tr>");
                sb.AppendLine("<td>");
                sb.AppendLine($"<a href = \"{mUrl}\">");
                sb.AppendLine($"{mRecruitor}");
                sb.AppendLine("</a>");
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine($"<a href = \"{mUrl}\">");
                sb.AppendLine($"{mTitle}");
                sb.AppendLine("</a>");
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine($"<a href = \"{mUrl}\">");
                sb.AppendLine($"{mPay}");
                sb.AppendLine("</a>");
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine($"<a href = \"{mUrl}\">");
                sb.AppendLine($"{mDueDate}");
                sb.AppendLine("</a>");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");

                return sb.ToString();
            }
        }
    }
}
