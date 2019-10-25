using System;
using System.Runtime.Serialization;

namespace STA.Entity.Common
{
    [DataContract]
    public class JsonResult<T>
    {
        public JsonResult() { }

        public JsonResult(int code = 10000, string msg = "success")
        {
            Code = code;
            Msg = msg;
        }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "msg")]
        public string Msg { get; set; }

        [DataMember(Name = "result")]
        public T Result { get; set; }
    }
}