using System.Collections.Generic;

namespace AuthLoginDemo_bnd.Helpers
{
    public class PageResult<T>
    {
        public int Count { get; set;}
        public int PageIndex { get; set;}
        public int PageSize { get; set;}
        public List<T> Items { get; set;}
    }
}