using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibForCore.Database
{
    public class Result<T>
    {
        public T Data { get; set; }

        //public List<T> Datas { get; set; }
        public string RoundCodeOut { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public bool Success { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
    public class SQL_Result
    {
        //public T Data { get; set; }

        //public List<T> Datas { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public bool Success { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
