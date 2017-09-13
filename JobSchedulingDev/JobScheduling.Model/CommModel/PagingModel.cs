using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.CommModel
{
    public class PagingModel
    {
        private int _pageIndex = 0;
        private int _pageSize = 1;
        private int _totalCount = 0;

        public int PageIndex
        {
            get { return _pageIndex; ;}
            set { _pageIndex = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        public int TotalPages
        {
            get
            {
                int pages = _totalCount / _pageSize;
                if (_totalCount % PageSize > 0)
                    pages++;
                if (_totalCount == 0)
                    pages++;
                return pages;
            }
        }

        public DataTable DataTable { get; set; }
    }
}
