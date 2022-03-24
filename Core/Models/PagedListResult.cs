using System;
using System.Collections.Generic;

namespace Common.Core.Models
{
    public class PagedListResult<T>
    {
        public int PageNumber { get; set; }
        public int DocumentCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(DocumentCount / (double)PageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
        public List<T> Data { get; set; }
    }
}
