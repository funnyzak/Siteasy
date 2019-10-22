namespace STA.CacLib
{
    using STA.Common;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Resources;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Text.RegularExpressions;

    public class MemcachedClient
    {
        private bool _compressEnable;
        private long _compressThreshold;
        private string _defaultEncoding;
        private string _poolName;
        private bool _primitiveAsString;
        private static ResourceManager _resourceManager = new ResourceManager("STA.CacLib.Memcached.StringMessages", typeof(MemcachedClient).Assembly);
        private const string CLIENT_ERROR = "CLIENT_ERROR";
        private const int COMPRESS_THRESH = 0x7800;
        private const string DELETED = "DELETED";
        private const string END = "END";
        private const string ERROR = "ERROR";
        private const int F_COMPRESSED = 2;
        private const int F_SERIALIZED = 8;
        private const string NOTFOUND = "NOT_FOUND";
        private const string NOTSTORED = "NOT_STORED";
        private const string OK = "OK";
        private const string SERVER_ERROR = "SERVER_ERROR";
        private const string STATS = "STAT";
        private const string STORED = "STORED";
        private const string VALUE = "VALUE";

        public MemcachedClient()
        {
            this.Init();
        }

        public bool Add(string key, object value)
        {
            return this.Set("add", key, value, DateTime.MaxValue, null, this._primitiveAsString);
        }

        public bool Add(string key, object value, DateTime expiry)
        {
            return this.Set("add", key, value, expiry, null, this._primitiveAsString);
        }

        public bool Add(string key, object value, int hashCode)
        {
            return this.Set("add", key, value, DateTime.MaxValue, hashCode, this._primitiveAsString);
        }

        public bool Add(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("add", key, value, expiry, hashCode, this._primitiveAsString);
        }

        public long Decrement(string key)
        {
            return this.IncrementOrDecrement("decr", key, 1L, null);
        }

        public long Decrement(string key, long inc)
        {
            return this.IncrementOrDecrement("decr", key, inc, null);
        }

        public long Decrement(string key, long inc, int hashCode)
        {
            return this.IncrementOrDecrement("decr", key, inc, hashCode);
        }

        public bool Delete(string key)
        {
            return this.Delete(key, null, DateTime.MaxValue);
        }

        public bool Delete(string key, DateTime expiry)
        {
            return this.Delete(key, null, expiry);
        }

        public bool Delete(string key, object hashCode, DateTime expiry)
        {
            if (key != null)
            {
                SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
                if (sock == null)
                {
                    return false;
                }
                StringBuilder builder = new StringBuilder("delete ").Append(key);
                if (expiry != DateTime.MaxValue)
                {
                    builder.Append(" " + (GetExpirationTime(expiry) / 0x3e8));
                }
                builder.Append("\r\n");
                try
                {
                    sock.Write(Encoding.UTF8.GetBytes(builder.ToString()));
                    sock.Flush();
                    string str = sock.ReadLine();
                    if ("DELETED" == str)
                    {
                        sock.Close();
                        sock = null;
                        return true;
                    }
                }
                catch
                {
                    try
                    {
                        sock.TrueClose();
                    }
                    catch
                    {
                    }
                    sock = null;
                }
                if (sock != null)
                {
                    sock.Close();
                }
            }
            return false;
        }

        public bool FlushAll()
        {
            return this.FlushAll(null);
        }

        public bool FlushAll(ArrayList servers)
        {
            SockIOPool instance = SockIOPool.GetInstance(this._poolName);
            if (instance == null)
            {
                return false;
            }
            if (servers == null)
            {
                servers = instance.Servers;
            }
            if ((servers == null) || (servers.Count <= 0))
            {
                return false;
            }
            bool flag = true;
            for (int i = 0; i < servers.Count; i++)
            {
                SockIO connection = instance.GetConnection((string) servers[i]);
                if (connection == null)
                {
                    flag = false;
                }
                else
                {
                    string s = "flush_all\r\n";
                    try
                    {
                        connection.Write(Encoding.UTF8.GetBytes(s));
                        connection.Flush();
                        string str2 = connection.ReadLine();
                        flag = ("OK" == str2) ? flag : false;
                    }
                    catch
                    {
                        try
                        {
                            connection.TrueClose();
                        }
                        catch
                        {
                        }
                        flag = false;
                        connection = null;
                    }
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
            return flag;
        }

        public object Get(string key)
        {
            return this.Get(key, null, false);
        }

        public object Get(string key, int hashCode)
        {
            return this.Get(key, hashCode, false);
        }

        public object Get(string key, object hashCode, bool asString)
        {
            SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sock != null)
            {
                try
                {
                    string s = "get " + key + "\r\n";
                    sock.Write(Encoding.UTF8.GetBytes(s));
                    sock.Flush();
                    Hashtable hm = new Hashtable();
                    this.LoadItems(sock, hm, asString);
                    sock.Close();
                    return hm[key];
                }
                catch
                {
                    try
                    {
                        sock.TrueClose();
                    }
                    catch
                    {
                    }
                    sock = null;
                }
                if (sock != null)
                {
                    sock.Close();
                }
            }
            return null;
        }

        public long GetCounter(string key)
        {
            return this.GetCounter(key, null);
        }

        public long GetCounter(string key, object hashCode)
        {
            if (key == null)
            {
                return -1L;
            }
            long num = -1L;
            try
            {
                num = long.Parse((string) this.Get(key, hashCode, true), new NumberFormatInfo());
            }
            catch (ArgumentException)
            {
            }
            return num;
        }

        private static int GetExpirationTime(DateTime expiration)
        {
            if (expiration <= DateTime.Now)
            {
                return 0;
            }
            TimeSpan span = new TimeSpan(0x1d, 0x17, 0x3b, 0x3b);
            if (expiration.Subtract(DateTime.Now) > span)
            {
                return (int) span.TotalSeconds;
            }
            return (int) expiration.Subtract(DateTime.Now).TotalSeconds;
        }

        private static string GetLocalizedString(string key)
        {
            return _resourceManager.GetString(key);
        }

        public Hashtable GetMultiple(string[] keys)
        {
            return this.GetMultiple(keys, null, false);
        }

        public Hashtable GetMultiple(string[] keys, int[] hashCodes)
        {
            return this.GetMultiple(keys, hashCodes, false);
        }

        public Hashtable GetMultiple(string[] keys, int[] hashCodes, bool asString)
        {
            SockIO sock;
            if (keys == null)
            {
                return new Hashtable();
            }
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < keys.Length; i++)
            {
                object hashCode = null;
                if ((hashCodes != null) && (hashCodes.Length > i))
                {
                    hashCode = hashCodes[i];
                }
                sock = SockIOPool.GetInstance(this._poolName).GetSock(keys[i], hashCode);
                if (sock != null)
                {
                    if (!hashtable.ContainsKey(sock.Host))
                    {
                        hashtable[sock.Host] = new StringBuilder();
                    }
                    ((StringBuilder) hashtable[sock.Host]).Append(" " + keys[i]);
                    sock.Close();
                }
            }
            Hashtable hm = new Hashtable();
            ArrayList list = new ArrayList();
            foreach (string str in hashtable.Keys)
            {
                sock = SockIOPool.GetInstance(this._poolName).GetConnection(str);
                try
                {
                    string s = "get" + ((StringBuilder) hashtable[str]) + "\r\n";
                    sock.Write(Encoding.UTF8.GetBytes(s));
                    sock.Flush();
                    this.LoadItems(sock, hm, asString);
                }
                catch
                {
                    list.Add(str);
                    try
                    {
                        sock.TrueClose();
                    }
                    catch
                    {
                    }
                    sock = null;
                }
                if (sock != null)
                {
                    sock.Close();
                }
            }
            foreach (string str in list)
            {
                hashtable.Remove(str);
            }
            return hm;
        }

        public object[] GetMultipleArray(string[] keys)
        {
            return this.GetMultipleArray(keys, null, false);
        }

        public object[] GetMultipleArray(string[] keys, int[] hashCodes)
        {
            return this.GetMultipleArray(keys, hashCodes, false);
        }

        public object[] GetMultipleArray(string[] keys, int[] hashCodes, bool asString)
        {
            if (keys == null)
            {
                return new object[0];
            }
            Hashtable hashtable = this.GetMultiple(keys, hashCodes, asString);
            object[] objArray = new object[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                objArray[i] = hashtable[keys[i]];
            }
            return objArray;
        }

        public long Increment(string key)
        {
            return this.IncrementOrDecrement("incr", key, 1L, null);
        }

        public long Increment(string key, long inc)
        {
            return this.IncrementOrDecrement("incr", key, inc, null);
        }

        public long Increment(string key, long inc, int hashCode)
        {
            return this.IncrementOrDecrement("incr", key, inc, hashCode);
        }

        private long IncrementOrDecrement(string cmdname, string key, long inc, object hashCode)
        {
            SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
            if (sock != null)
            {
                try
                {
                    string s = string.Concat(new object[] { cmdname, " ", key, " ", inc, "\r\n" });
                    sock.Write(Encoding.UTF8.GetBytes(s));
                    sock.Flush();
                    string input = sock.ReadLine();
                    if (new Regex(@"\d+").Match(input).Success)
                    {
                        sock.Close();
                        return long.Parse(input, new NumberFormatInfo());
                    }
                }
                catch
                {
                    try
                    {
                        sock.TrueClose();
                    }
                    catch (IOException)
                    {
                    }
                    sock = null;
                }
                if (sock != null)
                {
                    sock.Close();
                }
            }
            return -1L;
        }

        private void Init()
        {
            this._primitiveAsString = false;
            this._compressEnable = true;
            this._compressThreshold = 0x7800L;
            this._defaultEncoding = "UTF-8";
            this._poolName = GetLocalizedString("default instance");
        }

        public bool KeyExists(string key)
        {
            return (this.Get(key, null, true) != null);
        }

        private void LoadItems(SockIO sock, Hashtable hm, bool asString)
        {
            while (true)
            {
                string str = sock.ReadLine();
                if (str.StartsWith("VALUE"))
                {
                    object obj2;
                    string[] strArray = str.Split(new char[] { ' ' });
                    string newValue = strArray[1];
                    int num = int.Parse(strArray[2], new NumberFormatInfo());
                    byte[] bytes = new byte[int.Parse(strArray[3], new NumberFormatInfo())];
                    sock.Read(bytes);
                    sock.ClearEndOfLine();
                    if ((num & 8) == 0)
                    {
                        if (this._primitiveAsString || asString)
                        {
                            obj2 = Encoding.GetEncoding(this._defaultEncoding).GetString(bytes);
                        }
                        else
                        {
                            try
                            {
                                obj2 = NativeHandler.Decode(bytes);
                            }
                            catch (Exception exception)
                            {
                                throw new IOException(GetLocalizedString("loaditems deserialize error").Replace("$$Key$$", newValue), exception);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            MemoryStream serializationStream = new MemoryStream(bytes);
                            obj2 = new BinaryFormatter().Deserialize(serializationStream);
                        }
                        catch (SerializationException exception2)
                        {
                            throw new IOException(GetLocalizedString("loaditems SerializationException").Replace("$$Key$$", newValue), exception2);
                        }
                    }
                    hm[newValue] = obj2;
                }
                else if ("END" == str)
                {
                    return;
                }
            }
        }

        public bool Replace(string key, object value)
        {
            return this.Set("replace", key, value, DateTime.MaxValue, null, this._primitiveAsString);
        }

        public bool Replace(string key, object value, DateTime expiry)
        {
            return this.Set("replace", key, value, expiry, null, this._primitiveAsString);
        }

        public bool Replace(string key, object value, int hashCode)
        {
            return this.Set("replace", key, value, DateTime.MaxValue, hashCode, this._primitiveAsString);
        }

        public bool Replace(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("replace", key, value, expiry, hashCode, this._primitiveAsString);
        }

        public bool Set(string key, object value)
        {
            return this.Set("set", key, value, DateTime.MaxValue, null, this._primitiveAsString);
        }

        public bool Set(string key, object value, DateTime expiry)
        {
            return this.Set("set", key, value, expiry, null, this._primitiveAsString);
        }

        public bool Set(string key, object value, int hashCode)
        {
            return this.Set("set", key, value, DateTime.MaxValue, hashCode, this._primitiveAsString);
        }

        public bool Set(string key, object value, DateTime expiry, int hashCode)
        {
            return this.Set("set", key, value, expiry, hashCode, this._primitiveAsString);
        }

        private bool Set(string cmdname, string key, object obj, DateTime expiry, object hashCode, bool asString)
        {
            if (expiry < DateTime.Now)
            {
                return true;
            }
            if ((((cmdname != null) && (cmdname.Trim().Length != 0)) && (key != null)) && (key.Length != 0))
            {
                byte[] bytes;
                SockIO sock = SockIOPool.GetInstance(this._poolName).GetSock(key, hashCode);
                if (sock == null)
                {
                    return false;
                }
                if (expiry == DateTime.MaxValue)
                {
                    expiry = new DateTime(0L);
                }
                int num = 0;
                int count = 0;
                if (NativeHandler.IsHandled(obj))
                {
                    if (asString)
                    {
                        if (obj != null)
                        {
                            try
                            {
                                bytes = Encoding.UTF8.GetBytes(obj.ToString());
                                count = bytes.Length;
                            }
                            catch
                            {
                                sock.Close();
                                sock = null;
                                return false;
                            }
                        }
                        else
                        {
                            bytes = new byte[0];
                            count = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            bytes = NativeHandler.Encode(obj);
                            count = bytes.Length;
                        }
                        catch
                        {
                            sock.Close();
                            sock = null;
                            return false;
                        }
                    }
                }
                else if (obj != null)
                {
                    try
                    {
                        MemoryStream serializationStream = new MemoryStream();
                        new BinaryFormatter().Serialize(serializationStream, obj);
                        bytes = serializationStream.GetBuffer();
                        count = (int) serializationStream.Length;
                        num |= 8;
                    }
                    catch
                    {
                        sock.Close();
                        sock = null;
                        return false;
                    }
                }
                else
                {
                    bytes = new byte[0];
                    count = 0;
                }
                try
                {
                    string s = string.Concat(new object[] { cmdname, " ", key, " ", num, " ", GetExpirationTime(expiry), " ", count, "\r\n" });
                    sock.Write(Encoding.UTF8.GetBytes(s));
                    sock.Write(bytes, 0, count);
                    sock.Write(Encoding.UTF8.GetBytes("\r\n"));
                    sock.Flush();
                    string str2 = sock.ReadLine();
                    if ("STORED" == str2)
                    {
                        sock.Close();
                        sock = null;
                        return true;
                    }
                }
                catch
                {
                    try
                    {
                        sock.TrueClose();
                    }
                    catch
                    {
                    }
                    sock = null;
                }
                if (sock != null)
                {
                    sock.Close();
                }
            }
            return false;
        }

        public Hashtable Stats()
        {
            return this.Stats(null, null);
        }

        public Hashtable Stats(ArrayList servers, string command)
        {
            SockIOPool instance = SockIOPool.GetInstance(this._poolName);
            if (instance == null)
            {
                return null;
            }
            if (servers == null)
            {
                servers = instance.Servers;
            }
            if ((servers == null) || (servers.Count <= 0))
            {
                return null;
            }
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < servers.Count; i++)
            {
                SockIO connection = instance.GetConnection((string) servers[i]);
                if (connection == null)
                {
                    continue;
                }
                command = Utils.StrIsNullOrEmpty(command) ? "stats\r\n" : (command + "\r\n");
                try
                {
                    string str;
                    bool flag;
                    connection.Write(Encoding.UTF8.GetBytes(command));
                    connection.Flush();
                    Hashtable hashtable2 = new Hashtable();
                    goto Label_0145;
                Label_00C9:
                    str = connection.ReadLine();
                    if (str.StartsWith("STAT"))
                    {
                        string[] strArray = str.Split(new char[] { ' ' });
                        string str2 = strArray[1];
                        string str3 = strArray[2];
                        hashtable2[str2] = str3;
                    }
                    else if ("END" == str)
                    {
                        goto Label_0168;
                    }
                    hashtable[servers[i]] = hashtable2;
                Label_0145:
                    flag = true;
                    goto Label_00C9;
                }
                catch
                {
                    try
                    {
                        connection.TrueClose();
                    }
                    catch
                    {
                    }
                    connection = null;
                }
            Label_0168:
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return hashtable;
        }

        public bool StoreCounter(string key, long counter)
        {
            return this.Set("set", key, counter, DateTime.MaxValue, null, true);
        }

        public bool StoreCounter(string key, long counter, int hashCode)
        {
            return this.Set("set", key, counter, DateTime.MaxValue, hashCode, true);
        }

        public long CompressionThreshold
        {
            get
            {
                return this._compressThreshold;
            }
            set
            {
                this._compressThreshold = value;
            }
        }

        public string DefaultEncoding
        {
            get
            {
                return this._defaultEncoding;
            }
            set
            {
                this._defaultEncoding = value;
            }
        }

        public bool EnableCompression
        {
            get
            {
                return this._compressEnable;
            }
            set
            {
                this._compressEnable = value;
            }
        }

        public string PoolName
        {
            get
            {
                return this._poolName;
            }
            set
            {
                this._poolName = value;
            }
        }

        public bool PrimitiveAsString
        {
            get
            {
                return this._primitiveAsString;
            }
            set
            {
                this._primitiveAsString = value;
            }
        }
    }
}

