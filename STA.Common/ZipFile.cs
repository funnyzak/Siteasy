using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace STA.Common
{
    /// <summary>   
    /// Zip文件压缩、解压   
    /// </summary>   
    public class ZipFile
    {
        /// <summary>   
        /// 解压类型   
        /// </summary>   
        public enum UnzipType
        {
            /// <summary>   
            /// 解压到当前压缩文件所在的目录   
            /// </summary>   
            ToCurrentDirctory,
            /// <summary>   
            /// 解压到与压缩文件名相同的新的目录，如果有多个文件，将为每个文件创建一个目录   
            /// </summary>   
            ToNewDirctory,
            /// <summary>
            /// 解压到其他文件夹
            /// </summary>
            ToOtherDirctory
        }

        #region 压缩
        /// <summary>   
        /// 压缩文件,默认目录为当前目录，文件名为当前目录名，压缩级别为6   
        /// </summary>   
        /// <param name="fileOrDirectory">要压缩的文件或文件夹</param>   
        public static void Zip(params string[] fileOrDirectory)
        {
            Zip(6, fileOrDirectory);
        }

        /// <summary>   
        /// 压缩文件,默认目录为当前目录，文件名为当前目录名   
        /// </summary>   
        /// <param name="zipLevel">压缩的级别</param>   
        /// <param name="fileOrDirectory">要压缩的文件或文件夹</param>   
        public static void Zip(int zipLevel, params string[] fileOrDirectory)
        {
            if (fileOrDirectory == null)
                return;
            else if (fileOrDirectory.Length < 1)
                return;
            else
            {
                string str = fileOrDirectory[0];
                if (str.EndsWith("\\"))
                    str = str.Substring(0, str.Length - 1);
                str += ".zip";
                Zip(str, zipLevel, fileOrDirectory);
            }
        }


        /// <summary>   
        /// 压缩文件,默认目录为当前目录   
        /// </summary>   
        /// <param name="zipedFileName">压缩后的文件</param>   
        /// <param name="zipLevel">压缩的级别</param>   
        /// <param name="fileOrDirectory">要压缩的文件或文件夹</param>   
        public static void Zip(string zipedFileName, int zipLevel, params string[] fileOrDirectory)
        {
            if (fileOrDirectory == null)
                return;
            else if (fileOrDirectory.Length < 1)
                return;
            else
            {
                string str = fileOrDirectory[0];
                if (str.EndsWith("\\"))
                    str = str.Substring(0, str.Length - 1);
                str = str.Substring(0, str.LastIndexOf("\\"));
                Zip(zipedFileName, str, zipLevel, fileOrDirectory);
            }
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="zipedFileName">压缩后的文件</param>   
        /// <param name="zipLevel">压缩的级别</param>   
        /// <param name="currentDirectory">当前所处目录</param>   
        /// <param name="fileOrDirectory">要压缩的文件或文件夹</param>   
        public static void Zip(string zipedFileName, string currentDirectory, int zipLevel, params string[] fileOrDirectory)
        {
            ArrayList AllFiles = new ArrayList();

            //获取所有文件   
            if (fileOrDirectory != null)
            {
                for (int i = 0; i < fileOrDirectory.Length; i++)
                {
                    if (File.Exists(fileOrDirectory[i]))
                        AllFiles.Add(fileOrDirectory[i]);
                    else if (Directory.Exists(fileOrDirectory[i]))
                        GetDirectoryFile(fileOrDirectory[i], AllFiles);
                }
            }

            if (AllFiles.Count < 1)
                return;

            ZipOutputStream zipedStream = new ZipOutputStream(File.Create(zipedFileName));
            zipedStream.SetLevel(zipLevel);

            for (int i = 0; i < AllFiles.Count; i++)
            {
                string file = AllFiles[i].ToString();
                FileStream fs;

                //打开要压缩的文件   
                try
                {
                    fs = File.OpenRead(file);
                }
                catch
                {
                    continue;
                }

                try
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    //新建一个entry   
                    string fileName = file.Replace(currentDirectory, "");
                    if (fileName.StartsWith("\\"))
                        fileName = fileName.Substring(1);
                    ZipEntry entry = new ZipEntry(fileName);
                    entry.DateTime = DateTime.Now;

                    //保存到zip流   
                    fs.Close();
                    zipedStream.PutNextEntry(entry);
                    zipedStream.Write(buffer, 0, buffer.Length);
                }
                catch
                {
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

            zipedStream.Finish();
            zipedStream.Close();
        }


        /// <summary>   
        /// 压缩文件夹   
        /// </summary>   
        /// <param name="curretnDirectory">当前所在的文件夹</param>   
        public static void ZipDirectory(string curretnDirectory)
        {
            if (curretnDirectory == null)
                return;

            string dir = curretnDirectory;
            if (dir.EndsWith("\\"))
                dir = dir.Substring(0, dir.Length - 1);
            string file = dir.Substring(dir.LastIndexOf("\\") + 1) + ".zip";
            dir += "\\" + file;

            Zip(dir, 6, curretnDirectory);
        }


        /// <summary>   
        /// 压缩文件夹   
        /// </summary>   
        /// <param name="curretnDirectory">当前所在的文件夹</param>   
        public static void ZipDirectory(string curretnDirectory, int zipLevel)
        {
            if (curretnDirectory == null)
                return;

            string dir = curretnDirectory;
            if (dir.EndsWith("\\"))
                dir = dir.Substring(0, dir.Length - 1);
            dir += ".zip";

            Zip(dir, zipLevel, curretnDirectory);
        }


        //递归获取一个目录下的所有文件   
        private static void GetDirectoryFile(string parentDirectory, ArrayList toStore)
        {
            string[] files = Directory.GetFiles(parentDirectory);
            for (int i = 0; i < files.Length; i++)
                toStore.Add(files[i]);
            string[] directorys = Directory.GetDirectories(parentDirectory);
            for (int i = 0; i < directorys.Length; i++)
                GetDirectoryFile(directorys[i], toStore);
        }
        #endregion

        #region 解压

        public static ArrayList ZipFiles(string zipname)
        {
            ZipInputStream zipStream;
            ZipEntry entry;
            ArrayList zipfiles = new ArrayList();

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(zipname));
                int loop = 0;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    if (entry.Name.EndsWith("/") || entry.Name == String.Empty)
                        continue;
                    zipfiles.Add("/" + entry.Name);
                    loop++;
                }
                zipStream.Close();
            }
            catch { }
            return zipfiles;
        }

        /// <summary>
        /// 解压文件   
        /// </summary>
        /// <param name="type">解压类型</param>
        /// <param name="dirpath">如何解压类型为其他，否则留空</param>
        /// <param name="zipFile">要解压的文件</param>
        public static void UnZip(UnzipType type, string dirpath, params string[] zipFile)
        {
            ZipInputStream zipStream;
            ZipEntry entry;
            for (int i = 0; i < zipFile.Length; i++)
            {
                zipStream = new ZipInputStream(File.OpenRead(zipFile[i]));
                string directoryName = string.Empty;
                if (type == UnzipType.ToNewDirctory)
                    directoryName = zipFile[i].Substring(0, zipFile[i].LastIndexOf("."));
                else if (type == UnzipType.ToCurrentDirctory)
                    directoryName = zipFile[i].Substring(0, zipFile[i].LastIndexOf("\\"));
                else if (type == UnzipType.ToOtherDirctory && dirpath != string.Empty)
                    directoryName = dirpath;
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);
                directoryName = directoryName.EndsWith("\\") ? directoryName : directoryName + "\\";
                //读出每一个文件   
                try
                {
                    while ((entry = zipStream.GetNextEntry()) != null)
                    {
                        if (entry.Name.EndsWith("/") || entry.Name == String.Empty)
                            continue;
                        string fileName = directoryName + entry.Name.Replace('/', '\\');
                        //创建一个文件   
                        if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                        FileStream streamWriter = File.Create(fileName);

                        //写入文件   
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();

                    }
                    zipStream.Close();
                }
                catch { }
            }
        }


        /// <summary>
        /// 解压文件   
        /// </summary>
        /// <param name="zipFile">要解压的文件,默认解压到新文件夹,每个文件对应生成一个文件夹</param>
        public static void UnZip(params string[] zipFile)
        {
            UnZip(UnzipType.ToNewDirctory, string.Empty, zipFile);
        }
        #endregion
    }
}