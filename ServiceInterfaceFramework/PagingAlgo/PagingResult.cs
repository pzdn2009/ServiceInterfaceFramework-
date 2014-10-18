using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpic.Common
{
    public class PagedList<T> : List<T>
    {
        public readonly static PagedList<T> Empty = new PagedList<T>(1, 1, 0, null);

        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public PagedList(int pageIndex, int pageSize, int totalRecords, IList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalRecords;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            if (items != null && items.Count > 0)
            {
                this.AddRange(items);
            }
        }
    }
}
