using Capstone.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Capstone.Pages
{
    public class JobModel : BaseMultiPageModel
    {
        protected override void updateEntities()
        {
            mEntities.Clear();

            mEntities.AddRange(new List<BaseEntity>
            {
                new JobEntity("����1", "24.11.01 ~ 24.11.12", "����", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����2", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����3", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����4", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����5", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����6", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����7", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new JobEntity("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
            });
        }

        private class JobEntity : BaseEntity
        {
            private readonly string mTitle;
            private readonly string mDate;
            private readonly string mTag;
            private readonly string mCompany;
            private readonly string mUrl;

            public JobEntity(string title, string data, string tag, string company, string url)
            {
                Debug.Assert(title != null);
                Debug.Assert(data != null);
                Debug.Assert(tag != null);
                Debug.Assert(company != null);
                Debug.Assert(url != null);

                mTitle = title;
                mDate = data;
                mTag = tag;
                mCompany = company;
                mUrl = url;
            }

            public override string ShowData()
            {
                StringBuilder sb = new StringBuilder(Program.DEFAULT_CAPACITY);

                sb.AppendLine("<div class=\"grid-item\">");
                sb.AppendLine($"<a href = \"{mUrl}\" class=\"card\">");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{mTitle}</h5>");
                sb.AppendLine($"<p class=\"card-text\">���� �Ⱓ: {mDate}</p>");
                sb.AppendLine($"<p class=\"card-text\">�з�: {mTag}</p>");
                sb.AppendLine($"<p class=\"card-text\">ȸ��: {mCompany}</p>");
                sb.AppendLine("</div></a></div>");

                return sb.ToString();
            }
        }
    }
}
