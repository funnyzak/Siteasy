using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

using STA.Common;
using STA.Entity;
using STA.Data;
using STA.Config;
namespace STA.Core.Collect
{
    struct resinfo
    {
        /// <summary>
        /// 完整的原始路径
        /// </summary>
        public string orgurl;
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string orgname;
        /// <summary>
        /// 原始的扩展文件名
        /// </summary>
        public string extname;
        /// <summary>
        /// 新文件名
        /// </summary>
        public string newname;

    }

    public class RemoteResource
    {
        private int SeriesNum;
        private string FileNum;
        private string restype = ".gif|.jpg|.bmp|.png|.jpeg";
        private string _localurl;
        private string _localpath;
        private string _content = "";
        private bool _rename;
        private bool _storage = false;

        public RemoteResource(string Content, string LocalURLDirectory, bool RenameFile, bool Storage)
        {
            _storage = Storage;
            _content = Content;
            _localurl = LocalURLDirectory.Trim();
            _localpath = Utils.GetMapPath(LocalURLDirectory);
            if (_localpath.Equals(""))
                throw new NullReferenceException("本地的物理路径不能为空!");
            _rename = RenameFile;
            SeriesNum = 1;
            FileNum = Rand.Number(6);
            _localpath = _localpath.Replace("/", "\\");
            _localurl = _localurl.Replace("\\", "/");
            _localpath = _localpath.TrimEnd('\\');
            _localurl = _localurl.TrimEnd('/');
            if (!Directory.Exists(_localpath))
                Directory.CreateDirectory(_localpath);
        }

        public string[] FileExtends
        {
            set
            {
                restype = "";
                string[] flexs = value;
                for (int i = 0; i < flexs.Length; i++)
                {
                    if (i > 0)
                        restype += "|";
                    restype += "." + flexs[i].TrimStart('.');
                }
            }
        }
        /// <summary>
        /// 获取远程资源的路径
        /// </summary>
        private IList<resinfo> ObtainResURL()
        {
            IList<resinfo> list = new List<resinfo>();
            String pattern = @"(http|https|ftp|rtsp|mms)://\S+(" + restype.Replace(".", "\\.") + ")";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match m = reg.Match(_content);
            while (m.Success)
            {
                string url = "";
                url = m.Value;
                bool bsame = false;
                foreach (resinfo res in list)
                {
                    if (res.orgurl.Equals(url))
                    {
                        bsame = true;
                        break;
                    }
                }
                if (!bsame)
                {
                    #region 加入资源列表
                    string name = "";
                    string curl = url.Replace("\\", "/").Trim();
                    if (curl.IndexOf("/") >= 0)
                    {
                        name = curl.Substring(curl.LastIndexOf("/") + 1);
                    }
                    else
                    {
                        name = url;
                    }
                    int pos = name.LastIndexOf(".");
                    resinfo r;
                    r.orgurl = url;
                    r.orgname = name.Substring(0, pos);
                    r.extname = name.Substring(pos + 1);
                    r.newname = "";
                    list.Add(r);
                    #endregion 加入资源列表
                }
                m = m.NextMatch();
            }
            return list;
        }

        public void FetchResource()
        {
            WebClient wb = new WebClient();
            IList<resinfo> list = ObtainResURL();
            if (!_localurl.Equals(""))
                _localurl += "/";
            foreach (resinfo r in list)
            {
                try
                {
                    string url = r.orgurl;
                    string newurl = "", newpath = "";
                    if (_rename)
                    {
                        #region 生成新文件名
                        string newname = FileNum + SeriesNum.ToString().PadLeft(3, '0') + "." + r.extname;
                        while (File.Exists(_localpath + "\\" + newname))
                        {
                            SeriesNum++;
                            newname = FileNum + SeriesNum.ToString().PadLeft(3, '0') + "." + r.extname;
                        }
                        newpath = _localpath + "\\" + newname;
                        newurl = _localurl + newname;
                        wb.DownloadFile(url, newpath);
                        #endregion
                    }
                    else
                    {
                        newurl = _localurl + r.orgname + "." + r.extname;
                        wb.DownloadFile(url, _localpath + "\\" + r.orgname + "." + r.extname);
                    }
                    #region 替换文件名
                    _content = _content.Replace(r.orgurl, newurl);
                    #endregion 替换文件名

                    #region 采集内容入库
                    if (_storage)
                    {
                        AttachmentInfo info = new AttachmentInfo();

                        info.Uid = info.Lastedituid = 0;
                        info.Username = info.Lasteditusername = "内容采集";
                        info.Filename = newurl;
                        info.Description = info.Fileext = r.extname;
                        info.Filesize = TypeParse.StrToInt(FileUtil.GetFileInfo(Utils.GetMapPath(newurl)).Length, 0);
                        info.Filetype = Globals.GetContentType(r.extname);
                        info.Attachment = r.orgname;
                        Contents.AddAttachment(info);
                    }
                    #endregion
                    SeriesNum++;
                }
                catch
                { }
            }
            if (wb != null)
                wb.Dispose();
        }
        /// <summary>
        /// 获取内容
        /// </summary>
        public string Content
        {
            get { return _content; }
        } /// <summary>

    }

    public class RemoteFile
    {
        private static string[] deflist = new string[] { "gif", "jpg", "bmp", "png", "jpeg" };

        /// <summary>
        /// 采集远程文件
        /// </summary>
        /// <param name="Content">原内容</param>
        /// <param name="FileExtends">文件类型集合{"gif","jpg"}</param>
        /// <param name="savepath">保存路径如\files</param>
        /// <returns></returns>
        public static String Remote(String Content, string[] FileExtends, string savepath, bool storage)
        {
            try
            {
                String path = savepath + DateTime.Now.Year + "/" + DateTime.Now.Month;
                RemoteResource red = new RemoteResource(Content, path, true, storage);
                red.FileExtends = FileExtends;
                red.FetchResource();
                return red.Content;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static String Remote(string Content, string savepath)
        {
            // "gif", "jpg", "bmp", "ico", "png", "jpeg", "swf", "rar", "zip", "cab", "doc", "rm", "ram", "wav", "mid", "mp3", "avi", "wmv"
            return Remote(Content, deflist, savepath, false);
        }

        public static String Remote(string Content)
        {
            // "gif", "jpg", "bmp", "ico", "png", "jpeg", "swf", "rar", "zip", "cab", "doc", "rm", "ram", "wav", "mid", "mp3", "avi", "wmv"
            return Remote(Content, deflist, "/", false);
        }
    }
}
