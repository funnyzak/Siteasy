using System;
using System.Text;

namespace STA.Common
{
    /// <summary>
    /// �Զ����쳣�ࡣ
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
                builder.Append("������Ϣ��");
                builder.Append(msg);
                builder.Append("\r\n");
                builder.Append("����ʱ�䣺");
                builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                builder.Append("\r\n");
                builder.Append("������Ϣ��");
                builder.Append(ex.Message);
                builder.Append("\r\n");
                builder.Append("����ԭ��");
                builder.Append(ex.Source);
                builder.Append("\r\n");
                builder.Append("����λ�ã�");
                builder.Append(ex.StackTrace);
                builder.Append("\r\n");
                builder.Append("��������");
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
