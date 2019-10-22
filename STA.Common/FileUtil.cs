using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace STA.Common
{
    public class FileUtil
    {
        private static string strRootFolder = "";

        //static FileUtil()
        //{
        //    strRootFolder = HttpContext.Current.Request.PhysicalApplicationPath;
        //}

        /// <summary>
        /// 读根目录
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            return strRootFolder;
        }

        /// <summary>
        /// 写根目录
        /// </summary>
        /// <param name="path"></param>
        public static void SetRootPath(string path)
        {
            strRootFolder = path;
        }

        /// <summary>
        /// 读取列表
        /// </summary>
        /// <returns></returns>
        public static List<FileItem> GetItems()
        {
            return GetItems(strRootFolder);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DirExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="name">文件夹名</param>
        /// <param name="parentName">所在文件夹路径</param>
        /// <returns></returns>
        public static bool CreateFolder(string name, string parentName)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(parentName);
                di.CreateSubdirectory(name);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 直接创建文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateFolder(string path)
        {
            try
            {
                if (DirExists(path)) return true;

                Directory.CreateDirectory(path);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 删除文件夹，不包括带有文件的文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFolder(string path)
        {
            return DeleteFolder(path, false);
        }

        /// <summary>
        /// 删除文件和文件夹,不删除带有文件的文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Delete(string path)
        {
            return Delete(path, false);
        }
        /// <summary>
        /// 删除文件和文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isall">是否删除带有文件的文件夹</param>
        /// <returns></returns>
        public static bool Delete(string path, bool isall)
        {
            if (Directory.Exists(path))
                return DeleteFolder(path, isall);
            else
                return DeleteFile(path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="isall">是否删除文件夹的子目录</param>
        /// <returns></returns>
        public static bool DeleteFolder(string path, bool isall)
        {
            try
            {
                Directory.Delete(path, isall);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static bool MoveFolder(string oldPath, string newPath)
        {
            try
            {
                Directory.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateFile(string path, string fileName)
        {
            return CreateFile(path + fileName);
        }

        public static bool CreateFile(string savePath)
        {
            try
            {
                FileStream fs = File.Create(savePath);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateFile(string path, string fileName, byte[] contents)
        {
            return CreateFile(path + fileName, contents);
        }

        public static bool CreateFile(string fileName, byte[] contents)
        {
            try
            {
                FileStream fs = File.Create(fileName);
                fs.Write(contents, 0, contents.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool WriteFile(string filename, string content)
        {
            return WriteFile(filename, content, false, Encoding.UTF8);
        }

        public static bool AppendFile(string filename, string content)
        {
            return WriteFile(filename, content, true, Encoding.UTF8);
        }

        public static bool AppendFile(string filename, string content, Encoding code)
        {
            return WriteFile(filename, content, true, code);
        }

        public static bool WriteFile(string filename, string content, Encoding code)
        {
            return WriteFile(filename, content, false, code);
        }

        /// <summary>
        /// 写入或追加文件内容
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="content">内容</param>
        /// <param name="append">是否追加</param>
        /// <param name="code">文件编码</param>
        /// <returns></returns>
        public static bool WriteFile(string filename, string content, bool append, Encoding code)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename, append, code);
                sw.Write(content);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            return ReadFile(path, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ReadFile(string path, Encoding code)
        {
            string str_content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path, code);
                    str_content = Fso.ReadToEnd();
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                return string.Empty;
            }
            return str_content;
        }

        /// <summary>
        /// 移动文件,如目标文件存在则删除
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static bool MoveFile(string oldPath, string newPath)
        {
            return MoveFile(oldPath, newPath, true);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="over">如果目标文件存在是否覆盖</param>
        /// <returns></returns>
        public static bool MoveFile(string oldPath, string newPath, bool over)
        {
            try
            {
                if (!File.Exists(oldPath) || (FileExists(newPath) && !over))
                    return false;

                if (File.Exists(newPath) && over)
                    DeleteFile(newPath);

                CreateFolder(newPath.Substring(0, newPath.LastIndexOf('\\')));

                File.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static List<FileItem> GetItems(string path, string fileType)
        {
            return GetItems(path, fileType, string.Empty);
        }

        public static FileInfo GetFileInfo(string filename)
        {
            if (!FileExists(filename)) return null;
            return new FileInfo(filename);
        }

        /// <summary>
        /// 读取文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filetype">制定文件格式如：doc,jpg,rar 否则为null</param>
        /// <param name="query">关键字搜索 否则为null</param>
        /// <returns></returns>
        public static List<FileItem> GetItems(string path, string filetype, string query)
        {
            int id = 1;
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            List<FileItem> list = new List<FileItem>();
            foreach (string s in folders)
            {
                FileItem item = new FileItem();
                item.Id = id;
                DirectoryInfo di = new DirectoryInfo(s);
                item.Name = di.Name;
                item.FullName = di.FullName;
                item.CreationDate = di.CreationTime;
                item.LastAccessDate = di.LastAccessTime;
                item.LastWriteDate = di.LastWriteTime;
                list.Add(item);
                id++;
            }
            foreach (string s in files)
            {
                FileItem item = new FileItem();
                FileInfo fi = new FileInfo(s);
                if (filetype != null && filetype != string.Empty && !Utils.InArray(Utils.GetFileExtName(fi.Name).ToLower(), filetype)) continue;
                if (query != null && query != string.Empty && fi.Name.IndexOf(query) < 0) continue;
                item.Id = id;
                item.Name = fi.Name;
                item.FullName = fi.FullName;
                item.CreationDate = fi.CreationTime;
                item.LastWriteDate = fi.LastWriteTime;
                item.LastAccessDate = fi.LastAccessTime;
                item.IsFolder = false;
                item.Size = fi.Length;
                list.Add(item);
                id++;
            }

            if (path.ToLower() != strRootFolder.ToLower())
            {
                FileItem topitem = new FileItem();
                DirectoryInfo topdi = new DirectoryInfo(path).Parent;
                topitem.Name = "[上一级]";
                topitem.FullName = topdi.FullName;
                list.Insert(0, topitem);

                FileItem rootitem = new FileItem();
                DirectoryInfo rootdi = new DirectoryInfo(strRootFolder);
                rootitem.Name = "[根目录]";
                rootitem.FullName = rootdi.FullName;
                list.Insert(0, rootitem);

            }
            return list;
        }


        /// <summary>
        /// 读取列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<FileItem> GetItems(string path)
        {
            return GetItems(path, string.Empty);
        }

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileItem GetItemInfo(string path)
        {
            FileItem item = new FileItem();
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                item.Name = di.Name;
                item.FullName = di.FullName;
                item.CreationDate = di.CreationTime;
                item.IsFolder = true;
                item.LastAccessDate = di.LastAccessTime;
                item.LastWriteDate = di.LastWriteTime;
                item.FileCount = di.GetFiles().Length;
                item.SubFolderCount = di.GetDirectories().Length;
            }
            else
            {
                FileInfo fi = new FileInfo(path);
                item.Name = fi.Name;
                item.FullName = fi.FullName;
                item.CreationDate = fi.CreationTime;
                item.LastAccessDate = fi.LastAccessTime;
                item.LastWriteDate = fi.LastWriteTime;
                item.IsFolder = false;
                item.Size = fi.Length;
            }

            return item;

        }


        public static bool CopyFolder(string source, string destination)
        {
            try
            {
                String[] files;
                if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                    destination += Path.DirectorySeparatorChar;
                if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
                files = Directory.GetFileSystemEntries(source);
                foreach (string element in files)
                {
                    if (Directory.Exists(element))
                        CopyFolder(element, destination + Path.GetFileName(element));
                    else
                        File.Copy(element, destination + Path.GetFileName(element), true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<FileItem> GetFiles(string source)
        {
            return GetFiles(source, string.Empty);
        }

        public static List<FileItem> GetFiles(string source, string filetype)
        {
            return GetFiles(source, filetype, string.Empty);
        }

        /// <summary>
        /// 获取文件夹里的所有文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filetype">文件类型如：rar,jpg,doc,null,empty则所有</param>
        /// <param name="filetype">搜索</param>
        /// <returns></returns>
        public static List<FileItem> GetFiles(string source, string filetype, string query)
        {
            List<FileItem> list = new List<FileItem>();
            if (!DirExists(source)) return list;
            try
            {
                string[] files = Directory.GetFileSystemEntries(source);
                foreach (string element in files)
                {
                    if (!Directory.Exists(element))
                    {
                        FileItem item = GetItemInfo(element);
                        if (query != string.Empty && item.FullName.IndexOf(query) < 0)
                            continue;
                        if (filetype == null || filetype.Trim() == string.Empty)
                        {
                            list.Add(item);
                        }
                        else
                        {
                            if (Utils.InArray(Utils.GetFileExtName(item.Name), filetype))
                                list.Add(item);
                        }
                    }
                    else
                    {
                        list.AddRange(GetFiles(element, filetype, query));
                    }
                }
                return list;
            }
            catch
            {
                return list;
            }
        }

        /// <summary>
        /// 判断是否为安全文件名
        /// </summary>
        /// <param name="str">文件名</param>
        /// <returns></returns>
        public static bool IsSafeName(string filename)
        {
            string strExtension = ".txt";
            filename = filename.ToLower();//变为小写
            if (filename.LastIndexOf(".") >= 0)
                strExtension = filename.Substring(filename.LastIndexOf("."));
            string[] arrExtension = { ".htm", ".html", ".shtml", ".jhtml", ".asp", ".swf", ".aspx", ".ashx", ".asmx", ".asax", ".dll", ".sql", ".php", ".cs", ".config", ".txt", ".js", ".css", ".xml", ".sitemap", ".jpg", ".gif", ".png", ".bmp", ".rar", ".zip", ".xls", ".doc", ".ppt" };
            for (int i = 0; i < arrExtension.Length; i++)
            {
                if (strExtension.Equals(arrExtension[i]))
                    return true;
            }
            return false;
        }
        /// <summary>
        ///  判断是否为不安全文件名
        /// </summary>
        /// <param name="str">文件名、文件夹名</param>
        /// <returns>bool</returns>
        public static bool IsUnsafeName(string strExtension)
        {
            strExtension = strExtension.ToLower();//变为小写
            //得到string的.XXX的文件名后缀 LastIndexOf（得到点的位置） Substring（剪切从X的位置）

            if (strExtension.LastIndexOf(".") >= 0)
            { strExtension = strExtension.Substring(strExtension.LastIndexOf(".")); }
            else
            { strExtension = ".txt"; }//如果没有点 就当成txt文件

            //允许上传的扩展名，可以改成从配置文件中读出 
            string[] arrExtension = { ".", ".asp", ".aspx", ".php", ".cs", ".net", ".dll", ".config", ".ascx", ".master", ".cs", ".asmx", ".asax", ".cd", ".browser", ".rpt", ".ashx", ".xsd", ".mdf", ".resx", ".xsd" };

            for (int i = 0; i < arrExtension.Length; i++)
            {
                if (strExtension.Equals(arrExtension[i]))
                {
                    return true;
                }
            }
            return false;

        }
        /// <summary>
        ///  判断是否为可编辑文件
        /// </summary>
        /// <param name="str">文件名、文件夹名</param>
        /// <returns>bool</returns>
        public static bool IsCanEdit(string strExtension)
        {
            strExtension = strExtension.ToLower();//变为小写
            //得到string的.XXX的文件名后缀 LastIndexOf（得到点的位置） Substring（剪切从X的位置）

            if (strExtension.LastIndexOf(".") >= 0)
            { strExtension = strExtension.Substring(strExtension.LastIndexOf(".")); }
            else
            { strExtension = ".txt"; }//如果没有点 就当成txt文件

            //允许上传的扩展名，可以改成从配置文件中读出 
            string[] arrExtension = { ".htm", ".html", ".shtml", ".config", ".jhtml", ".tpl", ".txt", ".js", ".css", ".xml", ".sitemap", ".sql", ".aspx", ".cs", ".php", ".xml", ".asp", ".ashx", ".asmx", ".asax", };

            for (int i = 0; i < arrExtension.Length; i++)
            {
                if (strExtension.Equals(arrExtension[i]))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }
    }


    public class FileItem
    {
        private int _id = 0;
        private string _Name;
        private string _FullName;
        private string _Other = string.Empty;
        private DateTime _CreationDate;
        private DateTime _LastAccessDate;
        private DateTime _LastWriteDate;

        private bool _IsFolder = true;

        private long _Size;
        private long _FileCount;
        private long _SubFolderCount;

        private string _Version;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
            }
        }

        public string Other
        {
            get { return _Other; }
            set { _Other = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                _CreationDate = value;
            }
        }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return _IsFolder;
            }
            set
            {
                _IsFolder = value;
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime LastAccessDate
        {
            get
            {
                return _LastAccessDate;
            }
            set
            {
                _LastAccessDate = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastWriteDate
        {
            get
            {
                return _LastWriteDate;
            }
            set
            {
                _LastWriteDate = value;
            }
        }

        /// <summary>
        /// 文件数
        /// </summary>
        public long FileCount
        {
            get
            {
                return _FileCount;
            }
            set
            {
                _FileCount = value;
            }
        }

        /// <summary>
        /// 文件夹数
        /// </summary>
        public long SubFolderCount
        {
            get
            {
                return _SubFolderCount;
            }
            set
            {
                _SubFolderCount = value;
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            if (_Version == null)
                _Version = GetType().Assembly.GetName().Version.ToString();

            return _Version;
        }

    }

    #region 文件集合排序
    public class FileCreateCompare : IComparer<FileItem>
    {

        #region IComparer<FileItem> 成员

        public int Compare(FileItem x, FileItem y)
        {
            return y.CreationDate.CompareTo(x.CreationDate);
        }

        #endregion
    }

    public class FileLastAccessCompare : IComparer<FileItem>
    {

        #region IComparer<FileItem> 成员

        public int Compare(FileItem x, FileItem y)
        {
            return y.LastAccessDate.CompareTo(x.LastAccessDate);
        }

        #endregion
    }

    public class FileSizeCompare : IComparer<FileItem>
    {

        #region IComparer<FileItem> 成员

        public int Compare(FileItem x, FileItem y)
        {
            return y.Size.CompareTo(x.Size);
        }

        #endregion
    }

    public class FileNameCompare : IComparer<FileItem>
    {

        #region IComparer<FileItem> 成员

        public int Compare(FileItem x, FileItem y)
        {
            return y.Name.CompareTo(x.Name);
        }

        #endregion
    }
    #endregion
}
