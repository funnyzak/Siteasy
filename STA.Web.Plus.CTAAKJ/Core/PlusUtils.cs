using System;
using System.Text;
using System.Data;
using STA.Common;

using STA.Web.Plus.CTAA.KJ.Entity;
using STA.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace STA.Web.Plus.CTAA.KJ.Core
{
    public partial class PlusUtils
    {
        public static string Result<T>(T data, int code = 0, string emsg = "")
        {
            Result<T> jsonResult = new Result<T>(code, emsg) { result = data };
            string result = JsonConvert.SerializeObject(jsonResult);

            LogProvider.Logger.InfoFormat("{0}{1}\r\n\r\nAPI Response:{2}\r\n\r\n\r\n\r\n", STARequest.PrintRequestData(), STARequest.GetRequestHeader(), result);

            return result;
        }


    }
}