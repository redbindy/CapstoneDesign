using System.Diagnostics;
using System.Reflection;

namespace Capstone.Model
{
    public abstract class BaseMultiPageModel : BasePageModel
    {
        protected List<BaseEntity> mEntities = new List<BaseEntity>(Program.DEFAULT_CAPACITY);

        private List<string> mGridItems = new List<string>(Program.DEFAULT_CAPACITY);

        private const int CELL_COUNT = 9;

        protected BaseMultiPageModel()
            : base(1, 1)
        {
            updateEntities();
        }

        public override void OnGet()
        {
            base.OnGet();
            updatePageInfos(1);
            sendItems();
        }

        public void OnGetOnPageButton(int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            updatePageInfos(pageNumber);
            sendItems();
        }

        protected void updatePageInfos(int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            setPageInfos((mEntities.Count + CELL_COUNT - 1) / CELL_COUNT, pageNumber);
        }

        protected abstract void updateEntities();

        private void sendItems()
        {
            mGridItems.Clear();

            int start = (PageNumber - 1) * CELL_COUNT;
            int end = start + CELL_COUNT;
            if (end > mEntities.Count)
            {
                end = mEntities.Count;
            }

            for (int i = start; i < end; ++i)
            {
                string gridItem = mEntities[i].ShowData();
                mGridItems.Add(gridItem);
            }

            ViewData["GridItems"] = mGridItems;
        }
    }
}
