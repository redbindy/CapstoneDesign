using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Capstone.Pages.LifeInfo
{
    public class PostModel : PageModel
    {
        public long ID { get; private set; }
        public string? Title { get; private set; }
        public string? Body { get; private set; }
        public string? UserInfo { get; private set; }
        public List<Comment> Comments { get; private set; }

        public PostModel()
        {
            Comments = new List<Comment>(Program.DEFAULT_CAPACITY);
        }

        public void OnGet(long postId)
        {
            ID = postId;

            string query = $"select Title, Body, UserInfo from Post where PostID={postId}";

            Database.Database db = Database.Database.Instance;
            using (System.Data.SQLite.SQLiteDataReader? dbReader = db.Select(query))
            {
                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return;
                }

                if (!dbReader.Read())
                {
                    return;
                }

                Title = dbReader.GetString(0);
                Body = dbReader.GetString(1);
                UserInfo = dbReader.GetString(2);
            }

            query = $"select CommentID, Body, UserInfo from Comment where PostID={postId}";
            using (var dbReader = db.Select(query))
            {
                if (dbReader == null)
                {
                    Console.WriteLine("cannot read user data from db");
                    return;
                }

                Comments.Clear();
                while (dbReader.Read())
                {
                    Comment comment = new Comment(
                        id: dbReader.GetInt64(0),
                        body: dbReader.GetString(1),
                        userInfo: dbReader.GetString(2)
                        );

                    Comments.Add(comment);
                }
            }
        }

        [BindProperty]
        [Required(ErrorMessage = "내용을 입력해주세요.")]
        public string CommentBody { get; set; }
        public void OnPostOnCommentSubmit(long postId, string userInfo)
        {
            string query = $"insert into Comment (Body, UserInfo, PostID) values('{CommentBody}', '{userInfo}', {postId})";

            Database.Database db = Database.Database.Instance;

            if (!db.Insert(query))
            {
                Console.WriteLine("cannot insert user data to db");
            }

            OnGet(postId);
        }

        public class Comment
        {
            public long ID { get; }
            public readonly string UserInfo;
            public string Body { get; private set; }

            public Comment(long id, string body, string userInfo)
            {
                Debug.Assert(!string.IsNullOrEmpty(userInfo));
                Debug.Assert(!string.IsNullOrEmpty(body));

                ID = id;
                UserInfo = userInfo;
                Body = body;
            }
        }
    }
}
