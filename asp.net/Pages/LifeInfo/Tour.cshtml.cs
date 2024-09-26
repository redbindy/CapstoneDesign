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
                new Dummy("모집1", "24.11.01 ~ 24.11.12", "개발", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집2", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집3", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집4", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집5", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집6", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집7", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://www.jejunu.ac.kr/"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
                new Dummy("모집8", "24.11.01 ~ 24.11.12", "디자인", "회사", "https://cdn.pixabay.com/photo/2023/08/11/08/29/highland-cattle-8183107_1280.jpg"),
            };
        }

        public void OnGet()
        {
            ViewData["Title"] = "관광";
        }

        public void OnGetOnPageButton(int id)
        {
            Debug.Assert(id > 0);

            PageNumber = id;
        }
    }
}
