using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApi.RestApi.DTO
{
    public class PagedResponse<T> where T:class
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int itemsCount { get; set; }
        public  IEnumerable<T> result { get; set; }

        public PagedResponse(int pageNumber, int pageSize, int itemsCount, IEnumerable<T> result)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.itemsCount = itemsCount;
            this.result = result;
        }
    }
}
