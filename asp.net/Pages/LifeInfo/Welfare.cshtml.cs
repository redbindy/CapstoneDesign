using Capstone.Model;
using Microsoft.VisualBasic;
using System.Security.Policy;
using System.Text;

namespace Capstone.Pages.LifeInfo
{
    public class WelfareModel : BaseMultiPageModel
    {
        protected override void updateEntities()
        {
            mEntities.Clear();

            string query = $"select Title, Content, Ministry, Period, ServiceType, Contect from Welfare where idx >= 1 and idx <= 90";

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
                    string title = dbReader.GetString(0);
                    string content = dbReader.GetString(1);
                    string ministry = dbReader.GetString(2);
                    string period = dbReader.GetString(3);
                    string serviceType = dbReader.GetString(4);
                    string contect = dbReader.GetString(5);

                    WelfareEntity entity = new WelfareEntity(
                        title: title,
                        content: content,
                        ministry: ministry,
                        period: period,
                        serviceType: serviceType,
                        contect: contect);

                    mEntities.Add(entity);
                }
            }
        }

        public class WelfareEntity : BaseEntity
        {
            private readonly string mTitle;
            private readonly string mContent;
            private readonly string mMinistry;
            private readonly string mPeriod;
            private readonly string mServiceType;
            private readonly string mContect;

            public WelfareEntity(
                string title,
                string content,
                string ministry,
                string period,
                string serviceType,
                string contect
                )
            {
                mTitle = title;
                mContent = content;
                mMinistry = ministry;
                mPeriod = period;
                mServiceType = serviceType;
                mContect = contect;
            }

            public override string ShowData()
            {
                StringBuilder sb = new StringBuilder(Program.DEFAULT_CAPACITY);

                sb.AppendLine("<div class=\"grid-item\">");
                sb.AppendLine($"<a href = \"\" class=\"card\">");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{mTitle}</h5>");
                sb.AppendLine($"<p class=\"card-text\">{mContent}</p>");
                sb.AppendLine($"<p class=\"card-text\">주관 부처: {mMinistry}</p>");
                sb.AppendLine($"<p class=\"card-text\">지원 주기: {mPeriod}</p>");
                sb.AppendLine($"<p class=\"card-text\">지원 유형: {mServiceType}</p>");
                sb.AppendLine($"<p class=\"card-text\">문의처: {mContect}</p>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }
        }
    }
}
