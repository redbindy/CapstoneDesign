using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Capstone.Pages.LifeInfo
{
    public class TourModel : PageModel
    {
        public List<Dummy> Dummies = new List<Dummy>();

        public int PageNumber = 1;

        public TourModel()
        {
            Dummies = new List<Dummy>
            {
                new Dummy("����1", "24.11.01 ~ 24.11.12", "����", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����2", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����3", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����4", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����5", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����6", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����7", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://www.jejunu.ac.kr/"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("����8", "24.11.01 ~ 24.11.12", "������", "ȸ��", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
            };
        }

        public void OnGet()
        {
            ViewData["Title"] = "����";
        }

        public void OnGetOnPageButton(int id)
        {
            Debug.Assert(id > 0);

            PageNumber = id;
        }
    }
}
