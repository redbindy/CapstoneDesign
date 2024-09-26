namespace Capstone
{
    public class Dummy
    {
        public string Title { get; }
        public string Date { get; }
        public string Tag { get; }
        public string Company { get; }
        public string Link { get; }

        public Dummy(string title, string data, string tag, string company, string link)
        { 
            Title = title;
            Date = data;
            Tag = tag;
            Company = company;
            Link = link;
        }
    }
}
