using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using System.Web.Caching;
using ZAK.Common;
using ZAK.Config;
namespace ZAK.Cache
{

    public class FileCache
    {
        private static readonly System.Web.Caching.Cache _cache;
        private static readonly string CACHE_EXTENSION = ".cache";
        private static readonly string CACHE_PREFIX = "zcache";
        private static readonly string CACHE_BASEPATH = "/site/cache/";
        private static object lockHelper = new object();

        static FileCache()
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
            string str = Utils.GetMapPath(path);
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
                    File.Copy(str2, str2.Replace(CACHE_EXTENSION, CACHE_EXTENSION + ".temp"), true);
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

        public static void Clear(string key)
        {
            Remove(key);
        }

        public static object Get(string key)
        {
            if (_cache[key] == null)
                return null;
            string fileName = CACHE_PREFIX + key + CACHE_EXTENSION;
            return GetCacheObjectForFile(fileName, string.Format("{0}/{1}/",CACHE_BASEPATH,BaseConfigs.TemplateName));
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
                    if (File.Exists(fileName.Replace(CACHE_EXTENSION, CACHE_EXTENSION + ".temp")))
                    {
                        stream2 = File.Open(str2.Replace(CACHE_EXTENSION, CACHE_EXTENSION + ".temp"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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

        public static void Insert(string key, object obj, int minutes)
        {
            if (minutes != 0)
            {
                string filename = "";
                filename = CacheObjectByFile(CACHE_PREFIX + key + CACHE_EXTENSION, obj.ToString(), string.Format("{0}/{1}/", CACHE_BASEPATH, BaseConfigs.TemplateName));
                CacheDependency dependencies = new CacheDependency(filename);
                CacheItemRemovedCallback onRemoveCallback = new CacheItemRemovedCallback(FileCache.OnRemove);
                if (!filename.Equals(""))
                {
                    lock (lockHelper)
                    {
                        _cache.Insert(key, filename, dependencies, DateTime.Now.AddSeconds((double)((minutes * 60) + GetRandSeconds())),TimeSpan.Zero, CacheItemPriority.High, onRemoveCallback);
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

        public static bool Contains(string key)
        {
            return _cache[key] == null ? false : true;
        }
    }
}

