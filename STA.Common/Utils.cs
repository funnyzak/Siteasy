using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Net;
using System.Collections;
using System.Data;
using org.in2bits.MyXls;

namespace STA.Common
{
    public class Utils
    {
        public const string ASSEMBLY_VERSION = "1.0.0.1221";

        public const string ASSEMBLY_YEAR = "2012";

        public const string LegalCopyright = "2012, Siteasy Inc.";

        public const string ProductName = "Siteasy CMS";

        public const string OfficeSite = "http://www.stacms.com/";

        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);

        public static Regex RegexFont = new Regex(@"<font color=" + "\".*?\"" + @">([\s\S]+?)</font>", Utils.GetRegexCompiledOptions());

        /// <summary>
        /// 获取内容里的所有图片列表
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string[] GetContentImgList(string content)
        {
            Regex r = new Regex("(?: src=)\\\"([^\\s\'\"]+(.jpg|.png|.gif|.bmp|.jpeg|.JPG|.PNG|.GIF|.BMP|.JPEG))\\\"", RegexOptions.IgnoreCase);
            string imgs = string.Empty;
            foreach (Match m in r.Matches(content))
            {
                imgs += m.Groups[1].ToString() + ",";
            }
            return imgs.Length > 0 ? imgs.Substring(0, imgs.LastIndexOf(',')).Split(',') : new string[0];
        }
        /// <summary>
        /// 给内容里地址加上超链接
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String TextAddLink(string text)
        {
            return Regex.Replace(text, @"([a-zA-z]+://[^\s]*)", "<a href=\"$1\" target=\"_blank\">$1</a>", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 获取图片流
        /// </summary>
        /// <param name="url">图片URL</param>
        /// <returns></returns>
        public static Stream GetImgStreamByUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentLength = 0;
            request.Timeout = 20000;
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return response.GetResponseStream();
            }
            catch
            {
                return null;
            }
        }

        public static int HourToMillisecond(int hour)
        {
            return hour * 60 * 60 * 1000;
        }


        public static string HtmlToJs(string str, string blank)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("\"", "\\\"");
            str = str.Replace("/", "\\/");
            Regex re = new Regex(@"\r\n", RegexOptions.None);
            str = re.Replace(str, "\");\r\n" + blank + "document.writeln(\"");
            return blank + "document.writeln(\"" + str + "\");";
        }

        public static string HtmlToJs(string str)
        {
            return HtmlToJs(str, "");
        }

        public static string IntArrayToString(int[] array, string split)
        {
            if (array.Length <= 0) return string.Empty;
            string temp = string.Empty;
            foreach (int i in array)
                temp += i.ToString() + split;
            return temp.Substring(0, temp.Length - split.Length);
        }

        /// <summary>
        /// 移除html标签(包含标签内容)
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string html, string tag)
        {
            Regex regex1 = new Regex("<" + tag + "[\\s\\S]+</" + tag + " *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, "");
            return html;
        }

        /// <summary>
        /// 过滤html标签(不包含标签内容)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string FilterHtmlTag(string html, string tag)
        {
            Regex regex1 = new Regex(@"<" + tag + "[^>]*>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, "");
            regex1 = new Regex(@"</" + tag + "\\s*>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, "");
            return html;
        }

        public static string StringArrayToString(string[] array, string split)
        {
            if (array.Length <= 0) return string.Empty;
            string temp = string.Empty;
            foreach (string s in array)
                temp += s + split;
            return temp.Substring(0, temp.Length - split.Length);
        }

        /// <summary>
        /// 批量替换文件夹下的文件内容
        /// </summary>
        /// <param name="dirpath">文件夹路径</param>
        /// <param name="filetype">文件类型,如：.txt,.html,为空则全部替换</param>
        /// <param name="oldstr">旧内容</param>
        /// <param name="newstr">新内容</param>
        /// <returns></returns>
        public static bool ReplaceFilesStr(string dirpath, string filetype, string oldstr, string newstr)
        {
            return ReplaceFilesStr(dirpath, filetype, oldstr, newstr, Encoding.UTF8);
        }

        /// <summary>
        /// 批量替换文件夹下的文件内容
        /// </summary>
        /// <param name="dirpath">文件夹路径</param>
        /// <param name="filetype">文件类型,如：.txt,.html,为空则全部替换</param>
        /// <param name="oldstr">旧内容</param>
        /// <param name="newstr">新内容</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static bool ReplaceFilesStr(string dirpath, string filetype, string oldstr, string newstr, Encoding code)
        {
            try
            {
                if (!Directory.Exists(dirpath)) return false;
                foreach (FileItem f in FileUtil.GetFiles(dirpath))
                {
                    bool isreplace = false;
                    if (filetype.Length > 0)
                    {
                        foreach (string t in filetype.Split(','))
                        {
                            if (f.Name.EndsWith(t))
                            {
                                isreplace = true;
                                break;
                            }
                        }
                    }
                    if (isreplace == false && filetype.Length > 0) continue;
                    string content = FileUtil.ReadFile(f.FullName, code);
                    if (content.Trim().Length == 0) continue;
                    FileUtil.WriteFile(f.FullName, content.Replace(oldstr, newstr), code);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 指定文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool PathExist(string path)
        {
            return Directory.Exists(GetMapPath(path));
        }

        /// <summary>
        /// 添加文件名称尾部添加字符
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Tail"></param>
        /// <returns></returns>
        public static string AddFileLastStr(String FileName, String Tail)
        {
            int dotex = FileName.LastIndexOf('.');
            if (dotex < 0 || Tail.Length == 0) return FileName;
            return FileName.Substring(0, dotex) + Tail + FileName.Substring(dotex);
        }
        /// <summary>
        /// html格式化成可存到xml的格式
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string StrSaveToXml(String Str)
        {
            return "<![CDATA[" + Str + "]]>";
        }
        /// <summary>
        /// 从xml里对数据进行处理输出
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string StrGetFromXml(String Str)
        {
            return (!String.IsNullOrEmpty(Str) && Str.IndexOf("<![CDATA[") >= 0) ? Str.Substring(9, Str.Length - 12) : String.Empty;
        }
        /// <summary>
        /// 压缩Html文件
        /// </summary>
        /// <param name="html">Html文件</param>
        /// <returns></returns>
        public static string CompressHtml(string Html)
        {

            Html = Regex.Replace(Html, @">\s+?<", "><");//去除Html中的空白字符.
            Html = Regex.Replace(Html, @"\r\n\s*", "");
            Html = Regex.Replace(Html, @"<body([\s|\S]*?)>([\s|\S]*?)</body>", @"<body$1>$2</body>", RegexOptions.IgnoreCase);
            return Html;
        }
        /// <summary>
        /// 去掉字符中括号,适用于简单替换
        /// </summary>
        /// <param name="str">字符如1[ksjf],3[sdf]</param>
        /// <returns></returns>
        public static string RemoveBracket(string str)
        {
            string Rstr = string.Empty;
            if (String.IsNullOrEmpty(str)) return Rstr;
            foreach (string s in str.Split(','))
            {
                int start = s.IndexOf('[');
                int end = s.LastIndexOf(']');
                if (!(start > 0 && end > 0) || end <= start) continue;
                Rstr += s.Replace(s.Substring(start, (end - start + 1)), String.Empty) + ",";
            }
            return Rstr;
        }

        /// <summary>
        /// 指定开始位置截取指定长度的字符串 Code By Funnyzak
        /// </summary>
        /// <param name="Str">要截取的字符串</param>
        /// <param name="Index">开始位置</param>
        /// <param name="Num">长度，0为到尾部</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Index, int Num)
        {
            if (String.IsNullOrEmpty(Str) || Index > Str.Length) { return String.Empty; }
            if (Num == 0) { return Str.Substring(Index); }
            return Str.Substring(Index).Length < Num ? Str.Substring(Index) : Str.Substring(Index, Num);
        }

        public static string GetSubString(string Str, char StartChar, char EndChar)
        {
            if (String.IsNullOrEmpty(Str)) { return String.Empty; }
            int startindex = Str.IndexOf(StartChar);
            int endindex = Str.IndexOf(EndChar);
            if (startindex == -1 || endindex == -1 || (endindex - startindex - 1) <= 0) return Str;
            return GetSubString(Str, startindex + 1, (endindex - startindex - 1));
        }
        /// <summary>
        /// datatable 转Sting
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Rs">要处理的列eg:id,name</param>
        /// <param name="sc">每个列的分割符 eg:,</param>
        /// <param name="rc">每个行的分割符 eg: :</param>
        /// <returns>Eg:1,你好:3,你好啊</returns>
        public static string DataTableToString(DataTable dt, string Rs, string sc, string rc)
        {
            string rstr = string.Empty;
            if (dt == null || dt.Rows.Count <= 0) return rstr;
            foreach (DataRow dr in dt.Rows)
            {
                string rows = String.Empty;
                foreach (string s in Rs.Split(','))
                {
                    if (s == "") continue;
                    rows += dr[s].ToString() + sc;
                }
                rstr += (rows == "" ? rows : rows.Substring(0, rows.Length - sc.Length)) + rc;
            }
            return rstr == "" ? rstr : rstr.Substring(0, rstr.Length - rc.Length);
        }
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (stringarray == "" || stringarray == null)
            {
                return false;
            }

            str = str.ToLower();
            string[] stringArray = Utils.SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                //string t1 = str;
                //string t2 = stringArray[i];
                if (str.IndexOf(stringArray[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        /// <summary>
        /// 返回父目录的子目录数组
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string[] GetSubDir(string dir)
        {
            return Directory.GetDirectories(GetMapPath(dir));
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }


        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }


        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            //Regex r = null;
            Match m = null;

            //r = new Regex(@"(\r\n)",RegexOptions.IgnoreCase);
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }


            return str;
        }

        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex > (length - 1)) return "";
            if (startIndex <= 0) { return str; }
            return str.Substring(startIndex);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        public enum EnumDateCompare
        {
            year = 1,
            month = 2,
            day = 3,
            hour = 4,
            minute = 5,
            second = 6
        }
        /// <summary>
        /// 日期相差
        /// </summary>
        /// <param name="howtocompare"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="IsDeal">如果小于0是否转换为0</param>
        /// <returns></returns>
        public static double DateDiff(EnumDateCompare howtocompare, System.DateTime startDate, System.DateTime endDate, bool IsDeal)
        {
            double diff = 0;
            System.TimeSpan TS = new System.TimeSpan(endDate.Ticks - startDate.Ticks);

            switch (howtocompare)
            {
                case EnumDateCompare.year:
                    diff = Convert.ToDouble(TS.TotalDays / 365);
                    break;
                case EnumDateCompare.month:
                    diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                    break;
                case EnumDateCompare.day:
                    diff = Convert.ToDouble(TS.TotalDays);
                    break;
                case EnumDateCompare.hour:
                    diff = Convert.ToDouble(TS.TotalHours);
                    break;
                case EnumDateCompare.minute:
                    diff = Convert.ToDouble(TS.TotalMinutes);
                    break;
                case EnumDateCompare.second:
                    diff = Convert.ToDouble(TS.TotalSeconds);
                    break;
            }
            return diff < 0 && IsDeal ? 0 : diff;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(1).Trim();
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }



        /// <summary>
        /// 以指定的ContentType输出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // 需要读的数据长度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 输出文本
        /// </summary>
        /// <param name="Text"></param>
        public static void ResponseText(string Text)
        {
            ResponseText(Text, Rand.RamTime() + ".txt", "text/HTML");
        }
        /// <summary>
        /// 输出Word
        /// </summary>
        /// <param name="Text"></param>
        public static void ResponseWord(string Text)
        {
            ResponseText(Text, Rand.RamTime() + ".doc", "application/ms-word");
        }
        /// <summary>
        /// 输出制定内容
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static void ResponseText(string Text, string FileName, string contenttype)
        {
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = contenttype;
            HttpContext.Current.Response.Write(Text);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }

        public static bool IsImgHttp(string Input)
        {
            string pattern = @"((?:http|https|ftp|mms|rtsp)://(&(?=amp;)|[A-Za-z0-9\./=\?%_~@#:;\+\-])+(gif|jpg|png|jpeg|bmp|JPEG|GIF|BMP|PNG))";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(Input);
        }

        /// <summary>
        /// 检测含中文字符串实际长度
        /// </summary>
        /// <param name="str">待检测的字符串</param>
        /// <returns>返回正整数</returns>
        public static int CharNum(string Input)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(Input);
            int l = 0;
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)//判断是否为汉字或全脚符号
                {
                    l++;
                }
                l++;
            }
            return l;
        }

        /// <summary>
        /// 字符串简单加密解密
        /// </summary>
        /// <param name="Input">待加密字符串</param>
        /// <param name="Encrypt">真为加密反之为解密</param>
        /// <returns></returns>
        public static string StrEncrypt(string Input, bool Encrypt)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = Encrypt ? _chartemp[i] + 1 : _chartemp[i] - 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }
        /// <summary>
        /// 添加Session，调动有效期为60分钟
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void AddSessionValue(string strSessionName, string strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = 60;
        }
        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void ClearSessionValue(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static string GetSessionValue(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
            //return GetSubStrings(p_SrcString, p_Length*2, p_TailString);
        }

        /// <summary>
        /// 根据新闻标题的属性设置返回设置后的标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="color">标题颜色</param>
        /// <param name="isB">是否粗体</param>
        /// <param name="isI">是否斜体</param>
        /// <param name="titleNum">返回标题字节数,0为全部</param>
        /// /// <param name="tail"></param>
        /// <returns>返回设置后的标题</returns>
        public static string GetColorTitle(string title, string color, bool isB, bool isI, int titleNum, string tail)
        {
            if (title == null || title == "") return title;

            title = GetUnicodeSubString(title, titleNum, tail);
            if (isB)
            {
                title = "<b>" + title + "</b>";
            }
            if (isI)
            {
                title = "<i>" + title + "</i>";
            }
            if (color != "")
            {
                title = "<font style=\"color:" + color + ";\">" + title + "</font>";
            }
            return title;
        }
        /// <summary>
        /// 高亮字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="keywords">高亮关键字</param>
        /// <param name="color">颜色代码 如：000</param>
        /// <param name="isB">是否粗体</param>
        /// <param name="size">大小 如 14</param>
        /// <returns></returns>
        public static string HightLightStr(string str, string keywords, string color, bool isB, string size)
        {
            string oldwd = keywords;
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(str)) return str;
            keywords = isB ? "<b>" + keywords + "</b>" : keywords;
            keywords = "<font style=\"font-size:" + size + "px;color:#" + color + "\">" + keywords + "</font>";
            return Regex.Replace(str, oldwd, keywords, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 截取一个关键字前后N个字符,用于搜索结果处理
        /// </summary>
        /// <param name="input">原始文本</param>
        /// <param name="keywords">关键字列表 如果找不到第一个则继续搜寻下一个知道找到为止 都找不到则截取长度的2倍</param>
        /// <param name="length">前后多少个长度</param>
        /// <param name="tail">省略的字符尾部 如...</param>
        /// <returns>截取结果</returns>
        public static string GetInputAroundString(string input, string[] keywords, int length, string tail)
        {
            string content = string.Empty;
            foreach (string k in keywords)
            {
                if (GetInputAroundString(input, k, length, tail, out content)) return content;
            }
            return content;
        }

        /// 截取一个关键字前后N个字符,用于搜索结果处理
        /// </summary>
        /// <param name="input">文本</param>
        /// <param name="keywords">关键字 如果找不到第一个则继续搜寻下一个知道找到为止</param>
        /// <param name="tail">省略的字符尾部 如...</param>
        /// <param name="length">前后多少个字符长度</param>
        /// <param name="content">返回的结果</param>
        /// <returns>是否成功</returns>
        public static bool GetInputAroundString(string input, string keywords, int length, string tail, out string content)
        {
            int strLen = input.IndexOf(keywords, StringComparison.OrdinalIgnoreCase);
            int inputlength = input.Length;
            if (strLen == -1 || keywords == string.Empty)
            {
                //content = GetUnicodeSubString(input, length, tail);
                content = length * 2 < input.Length ? input.Substring(0, length * 2) + tail : input.Substring(0);
                return false;
            }
            int startlen = strLen - length;
            int endlen = strLen + length;
            if (startlen < 0) startlen = 0;
            if (endlen > inputlength - 1)
                content = input.Substring(startlen);
            else
                content = (startlen == 0 ? input.Substring(startlen, strLen + length) : input.Substring(startlen, length * 2)) + tail;
            return true;
        }

        ///// 截取一个关键字前后N个字符,用于搜索结果处理
        ///// </summary>
        ///// <param name="input">文本</param>
        ///// <param name="keywords">关键字 如果找不到第一个则继续搜寻下一个知道找到为止</param>
        ///// <param name="length">前后多少个字符长度</param>
        ///// <returns></returns>
        //public static string GetInputAroundString(string input, string keywords, int length)
        //{
        //    int strLen = input.IndexOf(keywords);
        //    int inputlength = input.Length;
        //    if (strLen == -1) return length * 2 < input.Length ? input.Substring(0, length * 2) : input.Substring(0);
        //    int startlen = strLen - length;
        //    int endlen = strLen + length;
        //    if (startlen < 0) startlen = 0;
        //    if (endlen > inputlength - 1)
        //        return input.Substring(startlen);
        //    else
        //        return startlen == 0 ? input.Substring(startlen, strLen + length) : input.Substring(startlen, length * 2);
        //}
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos) + p_TailString;
            }
            else
                result = str;

            return result;
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                    {
                        return "";
                    }
                    else
                    {
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                    }
                }
            }


            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            //return Regex.IsMatch(strEmail, @"^[A-Za-z0-9-_]+@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
            return Regex.IsMatch(strEmail, @"^[\w\.]+@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }

        /// <summary>
        /// 根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 替换回车换行符为P标记
        /// </summary>
        public static string StrFormatPtag(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = "<p>" + str + "</p>";
                str = str.Replace("\r\n", "</p><p>");
                str = str.Replace("\n", "</p><p>");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;

        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }


        public static string GetRealIP()
        {
            string ip = STARequest.GetIP();

            return ip;
        }

        /// <summary>
        /// 改正sql语句中的转义字符
        /// </summary>
        public static string mashSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ChkSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                str2 = str;
            }
            return str2;
        }


        /// <summary>
        /// 转换为静态html
        /// </summary>
        public static void transHtml(string path, string outpath)
        {
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(path, writer);
            FileStream fs;
            if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
            {
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            byte[] bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }


        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!Utils.StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                {
                    string[] tmp = { strContent };
                    return tmp;
                }
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
            {
                return new string[0] { };
            }
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];

            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] PadStringArray(string[] strArray, int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                int t = maxLength;
                maxLength = minLength;
                minLength = t;
            }

            int iMiniStringCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                    continue;
                }
                if (strArray[i].Length > maxLength)
                {
                    strArray[i] = strArray[i].Substring(0, maxLength);
                }
                iMiniStringCount++;
            }

            string[] result = new string[iMiniStringCount];
            for (int i = 0, j = 0; i < strArray.Length && j < result.Length; i++)
            {
                if (strArray[i] != null && strArray[i] != string.Empty)
                {
                    result[j] = strArray[i];
                    j++;
                }
            }


            return result;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            return ignoreRepeatItem ? DistinctStringArray(result, maxElementLength) : result;
        }

        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int minElementLength, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            if (ignoreRepeatItem)
            {
                result = DistinctStringArray(result);
            }
            return PadStringArray(result, minElementLength, maxElementLength);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return SplitString(strContent, strSplit, ignoreRepeatItem, 0);
        }

        /// <summary>
        /// 打乱数组顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] RandomArray<T>(T[] array)
        {
            int len = array.Length;
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
            T[] ret = new T[len];
            Random rand = new Random();
            int i = 0;
            while (list.Count < len)
            {
                int iter = rand.Next(0, len);
                if (!list.Contains(iter))
                {
                    list.Add(iter);
                    ret[i] = array[iter];
                    i++;
                }

            }
            return ret;
        }

        /// <summary>
        /// 词批量替换
        /// </summary>
        /// <param name="source">要处理的文本</param>
        /// <param name="sms">同义词列表 瘦|细</param>
        /// <returns></returns>
        public static string ReplaceWordList(string source, string[] sms)
        {
            if (source == "" || sms == null || sms.Length < 1) return source;

            foreach (string sm in sms)
            {
                string[] m = sm.Split('|');
                if (m.Length < 2 || m.Length > 2 || m[0] == "") continue;
                source = source.Replace(m[0], m[1]);
            }

            return source;
        }

        /// <summary>
        /// 关键词加链接
        /// </summary>
        /// <param name="source">要处理的文本</param>
        /// <param name="links">百度|http://baidu.com</param>
        /// <returns></returns>
        public static string AddConLinkList(string source, string[] links)
        {
            if (source == "" || links == null || links.Length < 1) return source;

            foreach (string link in links)
            {
                string[] m = link.Split('|');
                if (m.Length < 2 || m.Length > 2 || m[0] == "") continue;

                source = source.Replace(m[0], string.Format("<a href=\"{0}\" title=\"{1}\">{1}</a>", m[1], m[0]));
            }

            return source;
        }

        /// <summary>
        /// 文本中插入关键词
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pos">1前 2后 3随机</param>
        /// <param name="kws">关键词列表 你好</param>
        /// <param name="kwcount">添加次数</param>
        /// <returns></returns>
        public static string InsertWordList(string source, int pos, string[] kws, int kwcount)
        {
            if (source == "" || kws == null || kws.Length < 1 || kwcount < 1) return source;

            kws = Utils.RandomArray(kws);

            System.Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0, j = 0; i < kwcount; i++, j++)
            {
                if (j == kws.Length)
                    j = 0;

                string kw = kws[j];

                int insertidx = 0;
                if (pos == 2)
                    insertidx = source.Length;
                else if (pos == 3)
                    insertidx = random.Next(0, source.Length);

                if (insertidx == 0)
                    source = kw + source;
                else if (insertidx == source.Length)
                    source = source + kw;
                else
                    source = source.Substring(0, insertidx) + kw + source.Substring(insertidx + 1);
            }
            return source;
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            string[] result = new string[h.Count];

            h.Keys.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        /// <summary>
        /// 替换html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }



        //public static string ClearHtml(string strHtml)
        //{
        //    if (strHtml != "")
        //    {

        //        r = Regex.Replace(@"<\/?[^>]*>",RegexOptions.IgnoreCase);
        //        for (m = r.Match(strHtml); m.Success; m = m.NextMatch()) 
        //        {
        //            strHtml = strHtml.Replace(m.Groups[0].ToString(),"");
        //        }
        //    }
        //    return strHtml;
        //}


        /// <summary>
        /// 进行指定的替换(脏字过滤)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            string text1 = "";
            string text2 = "";
            string[] textArray1 = SplitString(bantext, "\r\n");
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                text1 = textArray1[num1].Substring(0, textArray1[num1].IndexOf("="));
                text2 = textArray1[num1].Substring(textArray1[num1].IndexOf("=") + 1);
                str = str.Replace(text1, text2);
            }
            return str;
        }




        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }


        /// <summary>
        /// 返回指定目录下的非 UTF8 字符集文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns>文件名的字符串数组</returns>
        public static string[] FindNoUTF8File(string Path)
        {
            //System.IO.StreamReader reader = null;
            StringBuilder filelist = new StringBuilder();
            DirectoryInfo Folder = new DirectoryInfo(Path);
            //System.IO.DirectoryInfo[] subFolders = Folder.GetDirectories(); 
            /*
            for (int i=0;i<subFolders.Length;i++) 
            { 
                FindNoUTF8File(subFolders[i].FullName); 
            }
            */
            FileInfo[] subFiles = Folder.GetFiles();
            for (int j = 0; j < subFiles.Length; j++)
            {
                if (subFiles[j].Extension.ToLower().Equals(".htm"))
                {
                    FileStream fs = new FileStream(subFiles[j].FullName, FileMode.Open, FileAccess.Read);
                    bool bUtf8 = IsUTF8(fs);
                    fs.Close();
                    if (!bUtf8)
                    {
                        filelist.Append(subFiles[j].FullName);
                        filelist.Append("\r\n");
                    }
                }
            }
            return Utils.SplitString(filelist.ToString(), "\r\n");

        }

        //0000 0000-0000 007F - 0xxxxxxx  (ascii converts to 1 octet!)
        //0000 0080-0000 07FF - 110xxxxx 10xxxxxx    ( 2 octet format)
        //0000 0800-0000 FFFF - 1110xxxx 10xxxxxx 10xxxxxx (3 octet format)

        /// <summary>
        /// 判断文件流是否为UTF8字符集
        /// </summary>
        /// <param name="sbInputStream">文件流</param>
        /// <returns>判断结果</returns>
        private static bool IsUTF8(FileStream sbInputStream)
        {
            int i;
            byte cOctets;  // octets to go in this UTF-8 encoded character 
            byte chr;
            bool bAllAscii = true;
            long iLen = sbInputStream.Length;

            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                chr = (byte)sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        }
                        while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            if (bAllAscii)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }

        /// <summary>
        /// 将long型数值转换为Int32类型
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            string strNum = objNum.ToString();
            if (IsNumeric(strNum))
            {

                if (strNum.ToString().Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
                return Int32.Parse(strNum);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalHours;
        }

        /// <summary>
        /// 建立文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return Utils.MakeSureDirectoryPathExists(name);
        }

        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }


        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }



        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {

            string[] userip = Utils.SplitString(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = Utils.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i] || tmpip[i] == "*")
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        /// <summary>
        /// 是否为有效域
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            if (host.IndexOf(".") == -1)
                return false;

            return new Regex(@"^\d+$").IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }

        /// <summary>
        /// 将文件字节变为接近的衡量单位表示
        /// </summary>
        /// <param name="bytes">字节大小</param>
        /// <returns></returns>
        public static string ConvertFileSize(long bytes)
        {
            if (bytes == 0) return "0 Bytes";
            string[] sizename = ("Bytes,KB,MB,GB,TB,PB,EB,ZB,YB").Split(',');
            double i = Math.Floor(Math.Log(bytes) / Math.Log(1024));
            int p = bytes > 1 ? 2 : 0;
            return (bytes / Math.Pow(1024, Math.Floor(i))).ToString("N") + " " + sizename[TypeParse.StrToInt(i)];
        }

        public static void WriteCookie(string cookieName, string strName, string strValue)
        {
            WriteCookie(cookieName, strName, strValue, string.Empty, 60 * 24 * 180);
        }

        public static void WriteCookie(string cookieName, string strValue)
        {
            WriteCookie(cookieName, cookieName, strValue);
        }

        public static void WriteCookie(string cookieName, string strName, string strValue, string cookiedomain)
        {
            WriteCookie(cookieName, strName, strValue, cookiedomain, 60 * 24 * 180);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string cookieName, string strName, string strValue, string domain, int defexpires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie.Values[strName] = UrlEncode(strValue);
            }
            else
            {
                cookie.Values[strName] = UrlEncode(strValue);

            }

            if (defexpires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(defexpires);
            }

            string cookieDomain = domain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void ClearCookie(string cookieName)
        {
            ClearCookie(cookieName, string.Empty);
        }

        public static void ClearCookie(string cookieName, string cookiedomain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-2);
            string cookieDomain = cookiedomain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="strName">cookie键</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null && HttpContext.Current.Request.Cookies[cookieName][strName] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[cookieName][strName].ToString());

            return "";
        }

        public static string GetCookie(string cookieName)
        {
            return GetCookie(cookieName, cookieName);
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase).Replace("&nbsp;", string.Empty);
        }

        public static string RemoveLine(string content)
        {
            return content.Replace("\r\n", " ");
        }
        /// <summary>
        /// 去掉字符串首字符
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>

        public static string RemoveFirstChar(string str, char cr)
        {
            if (string.IsNullOrEmpty(str) || str.Length == 0) return "";
            return str.IndexOf(cr) == 0 ? str.Substring(1, str.Length - 1) : str;
        }        /// <summary>
        /// 去掉最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="cr"></param>
        /// <returns></returns>
        public static string RemoveLastChar(string str, char cr)
        {
            if (string.IsNullOrEmpty(str) || str.Length == 0) return "";
            return str.LastIndexOf(cr) == (str.Length - 1) ? str.Substring(0, str.Length - 1) : str;
        }
        /// <summary>
        /// 去掉首尾字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="cr"></param>
        /// <returns></returns>
        public static string RemoveFirstAndLastChar(string str, char cr)
        {
            return RemoveFirstChar(RemoveLastChar(str, cr), cr);
        }

        public static string RemoveUnsafeStr(string input)
        {
            return RemoveUnsafeSqlStr(RemoveUnsafeHtml(input));
        }
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = RemoveUnsafeSqlStr(content);
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// 过滤字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string RemoveUnsafeSqlStr(string sInput)
        {
            if (sInput == null || sInput == "")
                return "";
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }


        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            return Validator.IsNumeric(Expression);
        }
        /// <summary>
        /// 从HTML中获取文本,保留br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return regEx.Replace(HTML, "");
        }

        /// <summary>
        /// 把半角逗号隔开的邮件列表不是邮件格式的过滤
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public static string GetEmailList(string emails)
        {
            if (emails.Trim() != string.Empty)
            {
                string ret = string.Empty;
                foreach (string mail in emails.Split('，', ' ', ','))
                {
                    if (mail.Trim() == "" || !Validator.IsEmail(mail.Trim())) continue;
                    ret += mail + ",";
                }
                return ret.EndsWith(",") ? ret.Substring(0, ret.Length - 1) : ret;
            }
            return "";
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            return TypeParse.StrToBool(expression, defValue);
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            return TypeParse.StrToBool(expression, defValue);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object expression, int defValue)
        {
            return TypeParse.ObjectToInt(expression, defValue);
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            return TypeParse.StrToInt(expression, defValue);
        }

        /// <summary>
        /// Object型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }

        /// <summary>
        /// 字符串加密  进行位移操作
        /// </summary>
        /// <param name="str">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public static string StrEncrypt(string Input)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] + 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str">待解密数据</param>
        /// <returns>解密成功后的数据</returns>
        public static string StrNcy(string Input)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] - 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            return Validator.IsNumericArray(strNumber);
        }


        public static string AdDeTime(int times)
        {
            string newtime = (DateTime.Now).AddMinutes(times).ToString();
            return newtime;

        }
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {

            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry str in NewHash)
            {

                try
                {
                    string[] single = SplitString(str.Value.ToString(), "\r\n");

                    foreach (string strs in single)
                    {
                        if (strs != "")


                            switch (ruletype.Trim().ToLower())
                            {
                                case "email":
                                    if (IsValidDoEmail(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "ip":
                                    if (IsIPSect(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "timesect":
                                    string[] splitetime = strs.Split('-');
                                    if (Utils.IsTime(splitetime[1].ToString()) == false || Utils.IsTime(splitetime[0].ToString()) == false)
                                        throw new Exception();
                                    break;

                            }

                    }


                }
                catch
                {
                    key = str.Key.ToString();
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            else
                return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        /// 取得网页的内容
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="sDocument">返回的网页内容或者是异常</param>
        /// <returns>有异常返回false</returns>
        public static string GetPageContent(Uri Url, Encoding encoding)
        {
            WebClient webclient = new WebClient();
            try
            {
                webclient.Encoding = encoding;
                return webclient.DownloadString(Url);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                webclient.Dispose();
            }
        }

        /// <summary>
        /// 取得网页的内容
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="sEncode">编码名称</param>
        /// <param name="sDocument">返回的网页内容或者是异常</param>
        /// <returns>有异常返回false</returns>
        public static string GetPageContent(Uri Url, string sEncode)
        {
            try
            {

                Encoding encoding = System.Text.Encoding.GetEncoding(sEncode);
                return GetPageContent(Url, encoding);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static System.Drawing.Color ToColor(string color)
        {
            int red, green, blue = 0;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return System.Drawing.Color.FromArgb(red, green, blue);
                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return System.Drawing.Color.FromArgb(red, green, blue);
                default:
                    return System.Drawing.Color.FromName(color);

            }
        }

        /// <summary>
        /// 得到网站的真实路径
        /// </summary>
        /// <returns></returns>
        public static string GetTrueSitePath()
        {
            string sitePath = HttpContext.Current.Request.Path;
            if (sitePath.LastIndexOf("/") != sitePath.IndexOf("/"))
                sitePath = sitePath.Substring(sitePath.IndexOf("/"), sitePath.LastIndexOf("/") + 1);
            else
                sitePath = "/";

            return sitePath;
        }

        /// <summary>
        /// 转换长文件名为短文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="repstring"></param>
        /// <param name="leftnum"></param>
        /// <param name="rightnum"></param>
        /// <param name="charnum"></param>
        /// <returns></returns>
        public static string ConvertSimpleFileName(string fullname, string repstring, int leftnum, int rightnum, int charnum)
        {
            string simplefilename = "", leftstring = "", rightstring = "", filename = "";

            string extname = GetFileExtName(fullname);
            if (extname == "" || extname == null)
            {

                throw new Exception("字符串不含有扩展名信息");
            }
            int filelength = 0, dotindex = 0;

            dotindex = fullname.LastIndexOf('.');
            filename = fullname.Substring(0, dotindex);
            filelength = filename.Length;
            if (dotindex > charnum)
            {
                leftstring = filename.Substring(0, leftnum);
                rightstring = filename.Substring(filelength - rightnum, rightnum);
                if (repstring == "" || repstring == null)
                {
                    simplefilename = leftstring + rightstring + "." + extname;
                }
                else
                {
                    simplefilename = leftstring + repstring + rightstring + "." + extname;
                }
            }
            else
            {

                simplefilename = fullname;
            }
            return simplefilename;

        }

        public static string GetFileExtName(string filename)
        {
            string[] array = filename.Trim().Split('.');
            Array.Reverse(array);
            return array[0].ToString();
        }


        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
            {
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号
            }
            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号
            }

            if (dt_dispose)
            {
                dt.Dispose();
            }
            return stringBuilder.Append("\r\n]");
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            //#if NET1
            if (str == null || str.Trim() == "")
            {
                return true;
            }
            //#else
            //            if (string.IsNullOrEmpty(str))
            //            {
            //                return true;
            //            }
            //#endif

            return false;
        }

        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string sitePath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host.ToString(),
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 sitePath);
        }

        /// <summary>
        /// 是否为数值串列表，各数值间用","间隔
        /// </summary>
        /// <param name="numList"></param>
        /// <returns></returns>
        public static bool IsNumericList(string numList)
        {
            if (numList == "")
                return false;
            foreach (string num in numList.Split(','))
            {
                if (!IsNumeric(num))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 检查颜色值是否为3/6位的合法颜色
        /// </summary>
        /// <param name="color">待检查的颜色</param>
        /// <returns></returns>
        public static bool CheckColorValue(string color)
        {
            if (StrIsNullOrEmpty(color))
            {
                return false;
            }

            color = color.Trim().Trim('#');

            if (color.Length != 3 && color.Length != 6)
            {
                return false;
            }
            //不包含0-9  a-f以外的字符
            if (!Regex.IsMatch(color, "[^0-9a-f]", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 动态页码
        /// </summary>
        /// <param name="defUrl">第一页</param>
        /// <param name="urlFormat">页格式如/list.aspx?page={0}</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">页数</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageNCount">显示最大页数</param>
        /// <param name="isSelect">是否生成select</param>
        /// <returns></returns>
        public static StringBuilder GetDynamicPageNumber(String defUrl, String urlFormat, int pageIndex, int pageCount, int recordCount, int pageNCount, bool isSelect)
        {
            StringBuilder PageResult = new StringBuilder(3000);
            int PageStart = 0; //页码开始数
            int PageOther = 5;//点击几页开始换页码导航 此为点击当前页码的第4个 换另一个页码数 当前页码数不可以小于此
            int PageStatus = 0; //当前页码的第几页
            //共16页/461条 首页 上一页 12 3 4 5 6 7 下一页 末页

            if (pageCount > 0)
                PageResult.Append(" 共" + pageCount.ToString() + "页");
            if (recordCount > 0)
                PageResult.Append("/" + recordCount.ToString() + "条");
            if (defUrl == "")
                defUrl = string.Format(urlFormat, 1);
            if (pageNCount <= 1)
                pageNCount = 7;

            PageResult.Append("<a href='" + (pageIndex == 1 ? "#" : defUrl) + "'>首页</a>");
            if (pageCount < 2 || pageIndex == 1) { PageResult.Append("<a href='#'>上一页</a>"); }
            else { PageResult.Append("<a href='" + (pageIndex == 2 ? defUrl : string.Format(urlFormat, (pageIndex - 1).ToString())) + "'>上一页</a>"); }
            PageStatus = pageIndex % PageOther;
            PageStart = pageIndex - PageStatus;
            if (PageStart <= 0) { PageStart = 1; }
            for (int i = PageStart; i < PageStart + pageNCount; i++)
            {
                if (i > pageCount) { break; }
                PageResult.Append("<a" + (i == pageIndex ? " class=\"on\"" : string.Empty) + " href='" + (i == pageIndex ? "#" : (i == 1 ? defUrl : string.Format(urlFormat, i.ToString()))) + "'>" + i.ToString() + "</a>");
            }
            if (pageIndex < pageCount)
            {
                PageResult.Append("<a href='" + string.Format(urlFormat, (pageIndex + 1).ToString()) + "'>下一页</a>");
            }
            else { PageResult.Append("<a href='#'>下一页</a>"); }
            PageResult.Append("<a href='" + (pageIndex == pageCount ? "#" : string.Format(urlFormat, pageCount.ToString())) + "'>末页</a>");
            if (!isSelect) return PageResult;
            PageResult.Append(" 跳到<select name='pageselect' id='pageselect' onchange='location.href=this.options[this.selectedIndex].value;'>");
            for (int j = 1; j <= pageCount; j++)
            {
                String Selected = "";
                if (pageIndex == j) { Selected = "selected=\"selected\""; }
                PageResult.Append("<option value='" + (j == 1 ? defUrl : string.Format(urlFormat, j.ToString())) + "' " + Selected + ">" + j.ToString() + "</option>");
            }
            return PageResult.Append("</select>页");
        }

        /// <summary>
        /// 返回分页导航 共407条记录,共41页,当前第1页首页上一页下一页尾页
        /// </summary>
        /// <param name="recordcount"></param>
        /// <param name="pagecount"></param>
        /// <param name="pageindex"></param>
        /// <param name="urlformat">/page/{0}.html</param>
        /// <returns></returns>
        public static string GetPageNumber(int recordcount, int pagecount, int pageindex, string urlformat)
        {
            string linkformat = "<a href=\"{0}\" target=\"_self\">{1}</a>";
            StringBuilder Sbb = new StringBuilder();
            Sbb.Append("共" + recordcount.ToString() + "条记录,共" + pagecount.ToString() + "页，当前第" + pageindex.ToString() + "页");
            int PrevPage, NextPage, indexpage, lastpage;
            PrevPage = pageindex <= 1 ? 0 : pageindex - 1;
            NextPage = pageindex >= pagecount ? 0 : pageindex + 1;
            indexpage = pageindex <= 1 ? 0 : 1;
            lastpage = pageindex >= pagecount ? 0 : pagecount;
            Sbb.Append(string.Format(linkformat, (indexpage == 0 ? "#" : string.Format(urlformat, "1")), "首页"));
            Sbb.Append(string.Format(linkformat, (PrevPage == 0 ? "#" : string.Format(urlformat, PrevPage.ToString())), "上一页"));
            Sbb.Append(string.Format(linkformat, (NextPage == 0 ? "#" : string.Format(urlformat, NextPage.ToString())), "下一页"));
            Sbb.Append(string.Format(linkformat, (lastpage == 0 ? "#" : string.Format(urlformat, pagecount.ToString())), "尾页"));
            return Sbb.ToString();
        }


        /// <summary>
        /// 随机页码上一页 12 3 4 5 6 7 下一页
        /// </summary>
        /// <param name="Pformat"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageNCount"></param>
        /// <returns></returns>
        public static StringBuilder GetRandomPageNumber(String Pformat, int PageIndex, int PageNCount)
        {
            StringBuilder PageResult = new StringBuilder(500);
            int PageStart = 0; //页码开始数
            int PageOther = 5;//点击几页开始换页码导航 此为点击当前页码的第4个 换另一个页码数 当前页码数不可以小于此
            int PageStatus = 0; //当前页码的第几页
            //上一页 12 3 4 5 6 7 下一页
            PageResult.Append("  <div class=\"pageguide\">");
            if (PageIndex > 1) { PageResult.Append("<a href='" + string.Format(Pformat, (PageIndex - 1).ToString()) + "'>上一页</a>"); }
            PageStatus = PageIndex % PageOther;
            PageStart = PageIndex - PageStatus;
            if (PageStart <= 0) { PageStart = 1; }
            for (int i = PageStart; i < PageStart + PageNCount; i++)
            {
                PageResult.Append("<a" + (i == PageIndex ? " class=\"on\"" : string.Empty) + " href='" + string.Format(Pformat, i.ToString()) + "'>" + i.ToString() + "</a>");
            }
            PageResult.Append("<a href='" + string.Format(Pformat, (PageIndex + 1).ToString()) + "'>下一页</a>");
            return PageResult.Append("</div>");
        }

        /// <summary>
        /// 为内容分页
        /// </summary>
        /// <param name="source">源内容</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="defaulturl">默认连接如zak.htm</param>
        /// <param name="urlformat">连接格式如zak-{0}.htm</param>
        /// <param name="curcontent">当然页的内容</param>
        /// <param name="spacemarks">内容分割符号,支持多个</param>
        /// <returns></returns>
        public static string GetContentPageNumber(string source, int pageindex, string defaulturl, string urlformat, out string curcontent, params string[] spacemarks)
        {
            curcontent = string.Empty;
            if (source == null || source.Trim().Length == 0)
                return string.Empty;
            if (pageindex <= 0)
                pageindex = 1;
            string[] rstr = source.Split(spacemarks, StringSplitOptions.RemoveEmptyEntries);
            string pstr = string.Empty;
            if (string.Compare(rstr[0], source) == 0)
            {
                curcontent = source;
                return string.Empty;
            }
            else
            {
                int pagecount = rstr.Length;
                if (pageindex > pagecount)
                {
                    curcontent = rstr[0];
                }
                else
                {
                    curcontent = rstr[pageindex - 1];
                }
                string _self = "target=\"_self\"";
                pstr += pageindex == 2 ? "<a href=\"" + defaulturl + "\" " + _self + ">上一页</a>" : string.Empty;
                pstr += pageindex > 2 ? "<a href=\"" + string.Format(urlformat, (pageindex - 1).ToString()) + "\" " + _self + ">上一页</a>" : string.Empty;
                for (int i = 1; i <= pagecount; i++)
                {
                    string url = i == 1 ? defaulturl : string.Format(urlformat, i.ToString());
                    url = i == pageindex ? "#" : url;
                    string cssclass = i == pageindex ? "class=\"on\"" : string.Empty;
                    pstr += "<a href=\"" + url + "\" " + cssclass + " " + _self + ">" + i.ToString() + "</a>";
                }
                return pageindex < pagecount ? pstr + "<a href=\"" + string.Format(urlformat, (pageindex + 1).ToString()) + "\" " + _self + ">下一页</a>" : pstr;
            }
        }

        /// <summary>
        /// 为内容分页带分页标题 [Z:PAGE=你好第三方] [Z:PAGE]
        /// </summary>
        /// <param name="source">内容</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="defaulturl">默认页 第一页</param>
        /// <param name="urlformat">分页格式</param>
        /// <param name="curcontent">当前内容</param>
        /// <param name="curtitle">当前标题</param>
        /// <returns></returns>
        public static string GetContentPageNumber(string source, int pageindex, string defaulturl, string urlformat, out string curcontent, out string curtitle)
        {
            curcontent = string.Empty;
            curtitle = string.Empty;
            if (source == null || source.Trim().Length == 0)
                return string.Empty;
            if (pageindex <= 0)
                pageindex = 1;
            string pattern = "(\\[STA:PAGE(=([^\\s]*))?\\])";
            string[] rstr = Regex.Replace(source, pattern, "[STA:PAGE]").Split(new string[] { "[STA:PAGE]" }, StringSplitOptions.None);
            string pstr = string.Empty;
            if (string.Compare(rstr[0], source) == 0)
            {
                curcontent = source;
                return string.Empty;
            }
            else
            {
                if (pageindex > 1)
                {
                    int loop = 0;
                    foreach (Match m in Regex.Matches(source, pattern))
                    {
                        string s = m.Groups[0].ToString();
                        if ((pageindex - 2) == loop && s != "[STA:PAGE]")
                            curtitle = m.Groups[3].ToString();
                        loop++;
                    }
                }
                int pagecount = rstr.Length;
                if (pageindex > pagecount)
                {
                    curcontent = rstr[0];
                }
                else
                {
                    curcontent = rstr[pageindex - 1];
                }
                string _self = "target=\"_self\"";
                pstr += pageindex == 2 ? "<a href=\"" + defaulturl + "\" " + _self + ">上一页</a>" : string.Empty;
                pstr += pageindex > 2 ? "<a href=\"" + string.Format(urlformat, (pageindex - 1).ToString()) + "\" " + _self + ">上一页</a>" : string.Empty;
                for (int i = 1; i <= pagecount; i++)
                {
                    string url = i == 1 ? defaulturl : string.Format(urlformat, i.ToString());
                    url = i == pageindex ? "#" : url;
                    string cssclass = i == pageindex ? "class=\"on\"" : string.Empty;
                    pstr += "<a href=\"" + url + "\" " + cssclass + " " + _self + ">" + i.ToString() + "</a>";
                }
                return pageindex < pagecount ? pstr + "<a href=\"" + string.Format(urlformat, (pageindex + 1).ToString()) + "\" " + _self + ">下一页</a>" : pstr;
            }
        }

        public static string GetSourceTextByUrl(string url)
        {
            return GetSourceTextByUrl(url, string.Empty, 7);
        }

        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encode">编码</param>
        /// <param name="timeOut">几秒超时</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url, string encode, int timeOut)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = timeOut * 1000;
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, Encoding.GetEncoding(encode == string.Empty ? "UTF-8" : encode));
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

         /// <summary>
        /// 执行URL获取页面内容
        /// </summary>
        public static string UrlExecute(string urlPath)
        {
            if (string.IsNullOrEmpty(urlPath))
            {
                return "error";
            }
            StringWriter sw = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(urlPath, sw);
                return sw.ToString();
            }
            catch (Exception)
            {
                return "error";
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (Utils.StrIsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        }

        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }


        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }


        #region 获取相应扩展名的ContentType类型
        private static string GetContentType(string fileextname)
        {
            switch (fileextname)
            {
                #region 常用文件类型
                case "jpeg": return "image/jpeg";
                case "jpg": return "image/jpeg";
                case "js": return "application/x-javascript";
                case "jsp": return "text/html";
                case "gif": return "image/gif";
                case "htm": return "text/html";
                case "html": return "text/html";
                case "asf": return "video/x-ms-asf";
                case "avi": return "video/avi";
                case "bmp": return "application/x-bmp";
                case "asp": return "text/asp";
                case "wma": return "audio/x-ms-wma";
                case "wav": return "audio/wav";
                case "wmv": return "video/x-ms-wmv";
                case "ra": return "audio/vnd.rn-realaudio";
                case "ram": return "audio/x-pn-realaudio";
                case "rm": return "application/vnd.rn-realmedia";
                case "rmvb": return "application/vnd.rn-realmedia-vbr";
                case "xhtml": return "text/html";
                case "png": return "image/png";
                case "ppt": return "application/x-ppt";
                case "tif": return "image/tiff";
                case "tiff": return "image/tiff";
                case "xls": return "application/x-xls";
                case "xlw": return "application/x-xlw";
                case "xml": return "text/xml";
                case "xpl": return "audio/scpls";
                case "swf": return "application/x-shockwave-flash";
                case "torrent": return "application/x-bittorrent";
                case "dll": return "application/x-msdownload";
                case "asa": return "text/asa";
                case "asx": return "video/x-ms-asf";
                case "au": return "audio/basic";
                case "css": return "text/css";
                case "doc": return "application/msword";
                case "exe": return "application/x-msdownload";
                case "mp1": return "audio/mp1";
                case "mp2": return "audio/mp2";
                case "mp2v": return "video/mpeg";
                case "mp3": return "audio/mp3";
                case "mp4": return "video/mpeg4";
                case "mpa": return "video/x-mpg";
                case "mpd": return "application/vnd.ms-project";
                case "mpe": return "video/x-mpeg";
                case "mpeg": return "video/mpg";
                case "mpg": return "video/mpg";
                case "mpga": return "audio/rn-mpeg";
                case "mpp": return "application/vnd.ms-project";
                case "mps": return "video/x-mpeg";
                case "mpt": return "application/vnd.ms-project";
                case "mpv": return "video/mpg";
                case "mpv2": return "video/mpeg";
                case "wml": return "text/vnd.wap.wml";
                case "wsdl": return "text/xml";
                case "xsd": return "text/xml";
                case "xsl": return "text/xml";
                case "xslt": return "text/xml";
                case "htc": return "text/x-component";
                case "mdb": return "application/msaccess";
                case "zip": return "application/zip";
                case "rar": return "application/x-rar-compressed";
                #endregion

                case "*": return "application/octet-stream";
                case "001": return "application/x-001";
                case "301": return "application/x-301";
                case "323": return "text/h323";
                case "906": return "application/x-906";
                case "907": return "drawing/907";
                case "a11": return "application/x-a11";
                case "acp": return "audio/x-mei-aac";
                case "ai": return "application/postscript";
                case "aif": return "audio/aiff";
                case "aifc": return "audio/aiff";
                case "aiff": return "audio/aiff";
                case "anv": return "application/x-anv";
                case "awf": return "application/vnd.adobe.workflow";
                case "biz": return "text/xml";
                case "bot": return "application/x-bot";
                case "c4t": return "application/x-c4t";
                case "c90": return "application/x-c90";
                case "cal": return "application/x-cals";
                case "cat": return "application/vnd.ms-pki.seccat";
                case "cdf": return "application/x-netcdf";
                case "cdr": return "application/x-cdr";
                case "cel": return "application/x-cel";
                case "cer": return "application/x-x509-ca-cert";
                case "cg4": return "application/x-g4";
                case "cgm": return "application/x-cgm";
                case "cit": return "application/x-cit";
                case "class": return "java/*";
                case "cml": return "text/xml";
                case "cmp": return "application/x-cmp";
                case "cmx": return "application/x-cmx";
                case "cot": return "application/x-cot";
                case "crl": return "application/pkix-crl";
                case "crt": return "application/x-x509-ca-cert";
                case "csi": return "application/x-csi";
                case "cut": return "application/x-cut";
                case "dbf": return "application/x-dbf";
                case "dbm": return "application/x-dbm";
                case "dbx": return "application/x-dbx";
                case "dcd": return "text/xml";
                case "dcx": return "application/x-dcx";
                case "der": return "application/x-x509-ca-cert";
                case "dgn": return "application/x-dgn";
                case "dib": return "application/x-dib";
                case "dot": return "application/msword";
                case "drw": return "application/x-drw";
                case "dtd": return "text/xml";
                case "dwf": return "application/x-dwf";
                case "dwg": return "application/x-dwg";
                case "dxb": return "application/x-dxb";
                case "dxf": return "application/x-dxf";
                case "edn": return "application/vnd.adobe.edn";
                case "emf": return "application/x-emf";
                case "eml": return "message/rfc822";
                case "ent": return "text/xml";
                case "epi": return "application/x-epi";
                case "eps": return "application/x-ps";
                case "etd": return "application/x-ebx";
                case "fax": return "image/fax";
                case "fdf": return "application/vnd.fdf";
                case "fif": return "application/fractals";
                case "fo": return "text/xml";
                case "frm": return "application/x-frm";
                case "g4": return "application/x-g4";
                case "gbr": return "application/x-gbr";
                case "gcd": return "application/x-gcd";

                case "gl2": return "application/x-gl2";
                case "gp4": return "application/x-gp4";
                case "hgl": return "application/x-hgl";
                case "hmr": return "application/x-hmr";
                case "hpg": return "application/x-hpgl";
                case "hpl": return "application/x-hpl";
                case "hqx": return "application/mac-binhex40";
                case "hrf": return "application/x-hrf";
                case "hta": return "application/hta";
                case "htt": return "text/webviewhtml";
                case "htx": return "text/html";
                case "icb": return "application/x-icb";
                case "ico": return "application/x-ico";
                case "iff": return "application/x-iff";
                case "ig4": return "application/x-g4";
                case "igs": return "application/x-igs";
                case "iii": return "application/x-iphone";
                case "img": return "application/x-img";
                case "ins": return "application/x-internet-signup";
                case "isp": return "application/x-internet-signup";
                case "IVF": return "video/x-ivf";
                case "java": return "java/*";
                case "jfif": return "image/jpeg";
                case "jpe": return "application/x-jpe";
                case "la1": return "audio/x-liquid-file";
                case "lar": return "application/x-laplayer-reg";
                case "latex": return "application/x-latex";
                case "lavs": return "audio/x-liquid-secure";
                case "lbm": return "application/x-lbm";
                case "lmsff": return "audio/x-la-lms";
                case "ls": return "application/x-javascript";
                case "ltr": return "application/x-ltr";
                case "m1v": return "video/x-mpeg";
                case "m2v": return "video/x-mpeg";
                case "m3u": return "audio/mpegurl";
                case "m4e": return "video/mpeg4";
                case "mac": return "application/x-mac";
                case "man": return "application/x-troff-man";
                case "math": return "text/xml";
                case "mfp": return "application/x-shockwave-flash";
                case "mht": return "message/rfc822";
                case "mhtml": return "message/rfc822";
                case "mi": return "application/x-mi";
                case "mid": return "audio/mid";
                case "midi": return "audio/mid";
                case "mil": return "application/x-mil";
                case "mml": return "text/xml";
                case "mnd": return "audio/x-musicnet-download";
                case "mns": return "audio/x-musicnet-stream";
                case "mocha": return "application/x-javascript";
                case "movie": return "video/x-sgi-movie";
                case "mpw": return "application/vnd.ms-project";
                case "mpx": return "application/vnd.ms-project";
                case "mtx": return "text/xml";
                case "mxp": return "application/x-mmxp";
                case "net": return "image/pnetvue";
                case "nrf": return "application/x-nrf";
                case "nws": return "message/rfc822";
                case "odc": return "text/x-ms-odc";
                case "out": return "application/x-out";
                case "p10": return "application/pkcs10";
                case "p12": return "application/x-pkcs12";
                case "p7b": return "application/x-pkcs7-certificates";
                case "p7c": return "application/pkcs7-mime";
                case "p7m": return "application/pkcs7-mime";
                case "p7r": return "application/x-pkcs7-certreqresp";
                case "p7s": return "application/pkcs7-signature";
                case "pc5": return "application/x-pc5";
                case "pci": return "application/x-pci";
                case "pcl": return "application/x-pcl";
                case "pcx": return "application/x-pcx";
                case "pdf": return "application/pdf";
                case "pdx": return "application/vnd.adobe.pdx";
                case "pfx": return "application/x-pkcs12";
                case "pgl": return "application/x-pgl";
                case "pic": return "application/x-pic";
                case "pko": return "application/vnd.ms-pki.pko";
                case "pl": return "application/x-perl";
                case "plg": return "text/html";
                case "pls": return "audio/scpls";
                case "plt": return "application/x-plt";
                case "pot": return "application/vnd.ms-powerpoint";
                case "ppa": return "application/vnd.ms-powerpoint";
                case "ppm": return "application/x-ppm";
                case "pps": return "application/vnd.ms-powerpoint";
                case "pr": return "application/x-pr";
                case "prf": return "application/pics-rules";
                case "prn": return "application/x-prn";
                case "prt": return "application/x-prt";
                case "ps": return "application/x-ps";
                case "ptn": return "application/x-ptn";
                case "pwz": return "application/vnd.ms-powerpoint";
                case "r3t": return "text/vnd.rn-realtext3d";
                case "ras": return "application/x-ras";
                case "rat": return "application/rat-file";
                case "rdf": return "text/xml";
                case "rec": return "application/vnd.rn-recording";
                case "red": return "application/x-red";
                case "rgb": return "application/x-rgb";
                case "rjs": return "application/vnd.rn-realsystem-rjs";
                case "rjt": return "application/vnd.rn-realsystem-rjt";
                case "rlc": return "application/x-rlc";
                case "rle": return "application/x-rle";
                case "rmf": return "application/vnd.adobe.rmf";
                case "rmi": return "audio/mid";
                case "rmj": return "application/vnd.rn-realsystem-rmj";
                case "rmm": return "audio/x-pn-realaudio";
                case "rmp": return "application/vnd.rn-rn_music_package";
                case "rms": return "application/vnd.rn-realmedia-secure";
                case "rmx": return "application/vnd.rn-realsystem-rmx";
                case "rnx": return "application/vnd.rn-realplayer";
                case "rp": return "image/vnd.rn-realpix";
                case "rpm": return "audio/x-pn-realaudio-plugin";
                case "rsml": return "application/vnd.rn-rsml";
                case "rt": return "text/vnd.rn-realtext";
                case "rtf": return "application/msword";
                case "rv": return "video/vnd.rn-realvideo";
                case "sam": return "application/x-sam";
                case "sat": return "application/x-sat";
                case "sdp": return "application/sdp";
                case "sdw": return "application/x-sdw";
                case "sit": return "application/x-stuffit";
                case "slb": return "application/x-slb";
                case "sld": return "application/x-sld";
                case "slk": return "drawing/x-slk";
                case "smi": return "application/smil";
                case "smil": return "application/smil";
                case "smk": return "application/x-smk";
                case "snd": return "audio/basic";
                case "sol": return "text/plain";
                case "sor": return "text/plain";
                case "spc": return "application/x-pkcs7-certificates";
                case "spl": return "application/futuresplash";
                case "spp": return "text/xml";
                case "ssm": return "application/streamingmedia";
                case "sst": return "application/vnd.ms-pki.certstore";
                case "stl": return "application/vnd.ms-pki.stl";
                case "stm": return "text/html";
                case "sty": return "application/x-sty";
                case "svg": return "text/xml";
                case "tdf": return "application/x-tdf";
                case "tg4": return "application/x-tg4";
                case "tga": return "application/x-tga";
                case "tld": return "text/xml";
                case "top": return "drawing/x-top";
                case "tsd": return "text/xml";
                case "txt": return "text/plain";
                case "uin": return "application/x-icq";
                case "uls": return "text/iuls";
                case "vcf": return "text/x-vcard";
                case "vda": return "application/x-vda";
                case "vdx": return "application/vnd.visio";
                case "vml": return "text/xml";
                case "vpg": return "application/x-vpeg005";
                case "vsd": return "application/vnd.visio";
                case "vss": return "application/vnd.visio";
                case "vst": return "application/vnd.visio";
                case "vsw": return "application/vnd.visio";
                case "vsx": return "application/vnd.visio";
                case "vtx": return "application/vnd.visio";
                case "vxml": return "text/xml";
                case "wax": return "audio/x-ms-wax";
                case "wb1": return "application/x-wb1";
                case "wb2": return "application/x-wb2";
                case "wb3": return "application/x-wb3";
                case "wbmp": return "image/vnd.wap.wbmp";
                case "wiz": return "application/msword";
                case "wk3": return "application/x-wk3";
                case "wk4": return "application/x-wk4";
                case "wkq": return "application/x-wkq";
                case "wks": return "application/x-wks";
                case "wm": return "video/x-ms-wm";
                case "wmd": return "application/x-ms-wmd";
                case "wmf": return "application/x-wmf";
                case "wmx": return "video/x-ms-wmx";
                case "wmz": return "application/x-ms-wmz";
                case "wp6": return "application/x-wp6";
                case "wpd": return "application/x-wpd";
                case "wpg": return "application/x-wpg";
                case "wpl": return "application/vnd.ms-wpl";
                case "wq1": return "application/x-wq1";
                case "wr1": return "application/x-wr1";
                case "wri": return "application/x-wri";
                case "wrk": return "application/x-wrk";
                case "ws": return "application/x-ws";
                case "ws2": return "application/x-ws";
                case "wsc": return "text/scriptlet";
                case "wvx": return "video/x-ms-wvx";
                case "xdp": return "application/vnd.adobe.xdp";
                case "xdr": return "text/xml";
                case "xfd": return "application/vnd.adobe.xfd";
                case "xfdf": return "application/vnd.adobe.xfdf";
                case "xq": return "text/xml";
                case "xql": return "text/xml";
                case "xquery": return "text/xml";
                case "xwd": return "application/x-xwd";
                case "x_b": return "application/x-x_b";
                case "x_t": return "application/x-x_t";
            }
            return "application/octet-stream";
        }
        #endregion
        /// <summary>
        /// 下载远程文件
        /// </summary>
        /// <param name="RI"></param>
        /// <returns></returns>
        public static bool DownFile(string orgUrl, string savePath, string fileName)
        {
            bool IsOk = false;
            try
            {
                CreateDir(savePath);
                WebClient WebClient = new WebClient();
                WebClient.DownloadFile(orgUrl, savePath + fileName);
                WebClient.Dispose();
                IsOk = true;
            }
            catch (Exception)
            {
                IsOk = false;
                throw;
            }
            return IsOk;
        }

        ///// <summary> 
        ///// word转成html 
        ///// </summary> 
        ///// <param name="wordFileName"></param> 
        ///// <param name="htmlpath"></param> 
        //public static bool WordToHtml(object wordFileName, string htmlpath)
        //{
        //    try
        //    {
        //        Word.ApplicationClass word = new Word.ApplicationClass();
        //        Type wordType = word.GetType();
        //        Word.Documents docs = word.Documents;
        //        //打开文件 
        //        Type docsType = docs.GetType();
        //        Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });
        //        //转换格式，另存为 
        //        Type docType = doc.GetType();
        //        docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { htmlpath, Word.WdSaveFormat.wdFormatFilteredHTML });
        //        docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
        //        //退出 Word 
        //        wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        ErrMessage.WriteErrFile(exception);
        //        return false;
        //    }
        //}

        #region URL请求数据
        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(param);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }
        #endregion

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// 把url请求的内容保存为html
        /// </summary>
        /// <param name="strsavepath">保存路径\bb\</param>
        /// <param name="strhttpurl">url地址</param>
        /// <param name="strfilename">1.html</param>
        /// <returns></returns>
        public static bool CreateHTMLPage(string strsavepath, string strhttpurl, string strfilename)
        {
            bool flag = false;
            try
            {
                StreamWriter writer;
                if (!Directory.Exists(strsavepath))
                {
                    CreateDir(strsavepath);
                }
                if (System.IO.File.Exists(strsavepath + strfilename))
                {
                    System.IO.File.Delete(strsavepath + strfilename);
                }
                string str = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strhttpurl);
                request.Method = WebRequestMethods.Http.Get;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 2.0.50727; .NET CLR 2.0.50727)";
                request.Accept = "*/*";
                request.ContentType = "text/html";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Encoding encoding = Encoding.UTF8;
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);
                str = reader.ReadToEnd();
                response.Close();
                reader.Close();
                writer = new StreamWriter(System.IO.File.OpenWrite(strsavepath + strfilename), Encoding.UTF8);
                writer.WriteLine(str);
                writer.Flush();
                writer.Close();
                flag = true;
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(strsavepath + @"\" + strfilename))
                {
                    System.IO.File.Delete(strsavepath + @"\" + strfilename);
                }
                throw new Exception(e.Message);
            }
            return flag;
        }

        #region 取单个字符的拼音声母
        /// <summary> 
        /// 取单个汉字的首个字母
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "G";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }
        #endregion

        #region 把汉字转化成全拼音
        private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

        private static string[] pyName = new string[]
        {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertE(string hzString)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }
        #endregion

        #region datatable 导出Execl
        /// <summary>
        /// datatable 导出Execl
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name">sheet名</param>
        /// <param name="filename">文件名 如：data</param>
        public static void CreateExecl(DataTable table, string name, string filename)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = filename;
            int rowIndex = 1;
            int colIndex = 0;

            Worksheet sheet = xls.Workbook.Worksheets.Add(name);
            Cells cells = sheet.Cells;
            foreach (DataColumn col in table.Columns)
            {
                colIndex++;
                Cell cell = cells.AddValueCell(1, colIndex, col.ColumnName);
                cell.Font.Bold = true;
                cell.Font.FontName = "微软雅黑";

            }

            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                colIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colIndex++;
                    Cell cell = cells.AddValueCell(rowIndex, colIndex, row[col.ColumnName].ToString());
                    cell.Font.FontName = "微软雅黑"; //字体
                }
            }
            xls.Send();
        }
        #endregion

        /// <summary>
        /// 得到正则编译参数设置
        /// </summary>
        /// <returns>参数设置</returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }
    }

}