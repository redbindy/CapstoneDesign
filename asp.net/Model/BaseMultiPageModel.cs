using System.Diagnostics;
using System.Reflection;

namespace Capstone.Model
{
    public abstract class BaseMultiPageModel : BasePageModel
    {
        protected List<BaseEntity> mEntities = new List<BaseEntity>(Program.DEFAULT_CAPACITY);

        private List<string> mGridItems = new List<string>(Program.DEFAULT_CAPACITY);

        protected const int CELL_COUNT = 9;

        protected BaseMultiPageModel()
            : base(1, 1)
        {
            updateEntities();
        }

        public override void OnGet()
        {
            base.OnGet();
            updatePageInfos(1);
            updateEntities();
            updatePageInfos(1);
            sendItems();
        }

        public void OnGetOnPageButton(int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            updatePageInfos(pageNumber);
            updateEntities();
            updatePageInfos(pageNumber);
            sendItems();
        }

        protected void updatePageInfos(int pageNumber)
        {
            Debug.Assert(pageNumber > 0);

            int maxPageNumber = (int)(pageNumber + Math.Round(mEntities.Count / (double)CELL_COUNT));

            setPageInfos(maxPageNumber, pageNumber);
        }

        protected abstract void updateEntities();

        private void sendItems()
        {
            mGridItems.Clear();

            int end = (int)Math.Min(CELL_COUNT, mEntities.Count);
            for (int i = 0; i < end; ++i)
            {
                string gridItem = mEntities[i].ShowData();
                mGridItems.Add(gridItem);
            }

            ViewData["GridItems"] = mGridItems;
        }
    }
}
