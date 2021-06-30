using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Models
{
    public class PageResponse<T>
    {
        public List<T> Data { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public Pagination(int _limit, int _page, int _total)
        {
            this._limit = _limit;
            this._page = _page;
            this._total = _total;

        }
        public int _limit { get; set; }
        public int _page { get; set; }
        public int _total { get; set; }

    }

  
}
