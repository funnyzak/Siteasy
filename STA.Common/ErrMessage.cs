using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ZAK.Common
{
    public class ErrMessage
    {
        // Methods
        public static void WriteErrFile(Exception es)
        {
            try
            {
                Utils.CreateDir(Utils.GetMapPath("/site/log/"));
                StringBuilder builder = new StringBuilder("");
                builder.Append("出错信息：");
                builder.Append(es.Message);
                builder.Append("\r\n");
                builder.Append("出错原因：");
                builder.Append(es.Source);
                builder.Append("\r\n");
                builder.Append("出错位置：");
                builder.Append(es.StackTrace);
                builder.Append("\r\n");
                builder.Append("出错方法：");
                builder.Append(es.TargetSite);
                builder.Append("\r\n");
                builder.Append("出错时间：");
                builder.Append(DateTime.Now);
                builder.Append("\r\n");
                StreamWriter writer = new StreamWriter(ServerInfo.GetRootPath() + @"\site\log\errinfo_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt", true);
                writer.Write(builder.ToString());
                writer.Flush();
                writer.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }


}
