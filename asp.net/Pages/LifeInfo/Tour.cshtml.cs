using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;

namespace Capstone.Pages.LifeInfo
{
    public class TourModel : BaseMultiPageModel
    {
        protected override void updateEntities()
        {
            mEntities.Clear();

            string query = $"select Name, ImgUrl, Url from Attraction where idx >= 1 and idx <= 90";

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
                    string name = dbReader.GetString(0);
                    string imgUrl = dbReader.GetString(1);
                    string url = dbReader.GetString(2);

                    TourEntity entity = new TourEntity(
                        name: name,
                        imageUrl: imgUrl,
                        url: url);

                    mEntities.Add(entity);
                }
            }
        }

        private class TourEntity : BaseEntity
        {
            private readonly string mName;
            private readonly string mUrl;
            private readonly string mImageUrl;

            public TourEntity(string name, string url, string imageUrl)
            {
                Debug.Assert(name != null);
                Debug.Assert(url != null);
                Debug.Assert(imageUrl != null);

                mName = name;
                mUrl = url;
                mImageUrl = imageUrl;
            }

            public override string ShowData()
            {
                StringBuilder sb = new StringBuilder(Program.DEFAULT_CAPACITY);

                sb.AppendLine("<div class=\"grid-item\">");
                sb.AppendLine($"<a href=\"{mUrl}\" class=\"card\">");
                sb.AppendLine($"<img class=\"card-image\" src=\"{mImageUrl}\" />");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{mName}</h5>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }
        }
    }
}
