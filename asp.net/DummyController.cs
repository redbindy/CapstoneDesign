using Microsoft.AspNetCore.Mvc;

namespace Capstone
{
    public static class DummyController
    {
        public static List<Dummy> Dummies = new List<Dummy>
        { 
            new Dummy("모집", "24.11.01 ~ 24.11.12", "개발"), 
            new Dummy("모집2", "24.11.01 ~ 24.11.12", "디자인"), 
        };
    }
}
