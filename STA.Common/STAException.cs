using System;
using System.Text;

namespace STA.Common
{
    /// <summary>
    /// 自定义异常类。
    /// </summary>
    public class STAException : Exception
    {
        public STAException()
        {
            //
        }


        public STAException(string msg)
            : base(msg)
        {
            //
        }

        // Methods
        public static void WriteError(string path, string msg, Exception ex)
        {
            try
            {
                StringBuilder builder = new StringBuilder("");
                builder.Append("基本信息：");
                builder.Append(msg);
                builder.Append("\r\n");
                builder.Append("出错时间：");
                builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                builder.Append("\r\n");
                builder.Append("出错信息：");
                builder.Append(ex.Message);
                builder.Append("\r\n");
                builder.Append("出错原因：");
                builder.Append(ex.Source);
                builder.Append("\r\n");
                builder.Append("出错位置：");
                builder.Append(ex.StackTrace);
                builder.Append("\r\n");
                builder.Append("出错方法：");
                builder.Append(ex.TargetSite);
                builder.Append("\r\n\r\n\r\n");

                if (!FileUtil.DirExists(path))
                    FileUtil.CreateFolder(path);

                string pathname = path + @"\err_" + DateTime.Now.ToString("yyyy_MM_dd") + ".config";
                FileUtil.WriteFile(pathname, builder.ToString() + FileUtil.ReadFile(pathname));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
