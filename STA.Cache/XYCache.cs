using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace ZAK.Cache
{
    public class XYCache
    {
        private static readonly System.Web.Caching.Cache _cache;
        private const string CACHE_CLASSINFO_PATH = "/cache/classinfo/";
        private static readonly string CACHE_EXTENSION = "zak";
        private const string CACHE_LABEL_PATH = "/cache/label/";
        private static readonly string CACHE_PREFIX = "cak";
        private static object lockHelper = new object();
        
        static XYCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                _cache = current.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        private static string CacheObjectByFile(string fileName, string body, string path)
        {
            string str3;
            string str = HttpContext.Current.Request.MapPath(path);
            string str2 = Path.Combine(str, fileName);
            ReaderWriterLock @lock = new ReaderWriterLock();
            Stream serializationStream = null;
            try
            {
                @lock.AcquireWriterLock(0x5dc);
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
                if (!File.Exists(str2))
                {
                    serializationStream = File.Create(str2);
                }
                else
                {
                    serializationStream = File.Open(str2, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                new BinaryFormatter().Serialize(serializationStream, body);
                serializationStream.Close();
                str3 = str2;
            }
            finally
            {
                if (serializationStream != null)
                {
                    serializationStream.Close();
                }
                @lock.ReleaseWriterLock();
            }
            return str3;
        }

        public static void Clear()
        {
            foreach (string str in GetKeys())
            {
                _cache.Remove(str);
            }
        }

        public static void Clear(CacheContentType contentType)
        {
            foreach (string str in GetKeys())
            {
                if (str.StartsWith(GetKeyFlag(contentType)))
                {
                    Remove(str);
                }
            }
        }

        public static object Get(string key, CacheContentType contentType)
        {
            if (_cache[GetKeyFlag(contentType) + key] == null)
            {
                return null;
            }
            string fileName = CACHE_PREFIX + Utils.JSEscape(key) + CACHE_EXTENSION;
            if (((contentType == CacheContentType.ContentLabel) || (contentType == CacheContentType.ClassLabel)) || (contentType == CacheContentType.SystemLabel))
            {
                return GetCacheObjectForFile(fileName, "/cache/label/");
            }
            if (contentType == CacheContentType.ClassInfoStat)
            {
                return GetCacheObjectForFile(fileName, "/cache/classinfo/");
            }
            return _cache[GetKeyFlag(contentType) + key];
        }

        private static string GetCacheObjectForFile(string fileName, string path)
        {
            string str2 = Path.Combine(HttpContext.Current.Request.MapPath(path), fileName);
            if (File.Exists(str2))
            {
                ReaderWriterLock @lock = new ReaderWriterLock();
                Stream serializationStream = null;
                Stream stream2 = null;
                try
                {
                    @lock.AcquireReaderLock(0x5dc);
                    serializationStream = File.Open(str2, FileMode.Open, FileAccess.Read);
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (string)formatter.Deserialize(serializationStream);
                }
                catch
                {
                    @lock.ReleaseReaderLock();
                    @lock.AcquireReaderLock(0x5dc);
                    if (File.Exists(fileName.Replace(XYECOM.Configuration.Cache.Instance.Extension, XYECOM.Configuration.Cache.Instance.Extension + ".temp")))
                    {
                        stream2 = File.Open(str2.Replace(XYECOM.Configuration.Cache.Instance.Extension, XYECOM.Configuration.Cache.Instance.Extension + ".temp"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        BinaryFormatter formatter2 = new BinaryFormatter();
                        return (string)formatter2.Deserialize(serializationStream);
                    }
                }
                finally
                {
                    if (serializationStream != null)
                    {
                        serializationStream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                    @lock.ReleaseReaderLock();
                }
            }
            return null;
        }

        private static string GetKeyFlag(CacheContentType type)
        {
            if (type == CacheContentType.ContentLabel)
            {
                return "content_";
            }
            if (type == CacheContentType.ClassLabel)
            {
                return "class_";
            }
            if (type == CacheContentType.SystemLabel)
            {
                return "system_";
            }
            if (type == CacheContentType.ClassInfoStat)
            {
                return "classinfo_";
            }
            return "";
        }

        private static ArrayList GetKeys()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key);
            }
            return list;
        }

        private static int GetRandSeconds()
        {
            Random random = new Random();
            return (int)(random.NextDouble() * 100.0);
        }

        public static void Insert(string key, object obj, int minutes, CacheContentType contentType)
        {
            if (minutes != 0)
            {
                string filename = "";
                filename = CacheObjectByFile(CACHE_PREFIX + "sdf." + CACHE_EXTENSION, obj.ToString(), "/cache/label/");
                CacheDependency dependencies = new CacheDependency(filename);
                CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(XYCache.OnRemove);
                if (!filename.Equals(""))
                {
                    lock (lockHelper)
                    {
                        _cache.Insert(GetKeyFlag(contentType) + key, filename, dependencies, DateTime.Now.AddSeconds((double)((minutes * 60) + GetRandSeconds())), TimeSpan.Zero, CacheItemPriority.High, onRemoveCallback);
                    }
                }
            }
        }

        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public static void OnRemove(string key, object obj, CacheItemRemovedReason reason)
        {
            string path = obj.ToString();
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
            }
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}

