using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STA.Web.Plus.CTAA.KJ.Entity
{
    [Serializable]
    public class Result<T>
    {
        public Result() { }

        public Result(int code = 10000, string msg = "success")
        {
            this.code = code;
            this.msg = msg;
        }

        public int code { get; set; }

        public string msg { get; set; }

        public T result { get; set; }
    }
}