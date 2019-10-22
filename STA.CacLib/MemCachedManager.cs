namespace STA.CacLib
{
    using STA.Common;
    using STA.Config;
    using System;
    using System.Collections;

    public sealed class MemCachedManager
    {
        private static MemcachedClient mc = null;
        private static MemCachedConfigInfo memCachedConfigInfo = MemCachedConfigs.GetConfig();
        private static SockIOPool pool = null;
        private static string[] serverList = null;

        static MemCachedManager()
        {
            CreateManager();
        }

        private static void CreateManager()
        {
            serverList = Utils.SplitString(memCachedConfigInfo.ServerList, ",");
            pool = SockIOPool.GetInstance(memCachedConfigInfo.PoolName);
            pool.SetServers(serverList);
            pool.InitConnections = memCachedConfigInfo.IntConnections;
            pool.MinConnections = memCachedConfigInfo.MinConnections;
            pool.MaxConnections = memCachedConfigInfo.MaxConnections;
            pool.SocketConnectTimeout = memCachedConfigInfo.SocketConnectTimeout;
            pool.SocketTimeout = memCachedConfigInfo.SocketTimeout;
            pool.MaintenanceSleep = memCachedConfigInfo.MaintenanceSleep;
            pool.Failover = memCachedConfigInfo.FailOver;
            pool.Nagle = memCachedConfigInfo.Nagle;
            pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
            pool.Initialize();
            mc = new MemcachedClient();
            mc.PoolName = memCachedConfigInfo.PoolName;
            mc.EnableCompression = false;
        }

        public static void Dispose()
        {
            if (MemCachedConfigs.GetConfig().ApplyMemCached && (pool != null))
            {
                pool.Shutdown();
            }
        }

        public static string[] GetConnectedSocketHost()
        {
            SockIO connection = null;
            string target = null;
            foreach (string str2 in serverList)
            {
                if (!Utils.StrIsNullOrEmpty(str2))
                {
                    try
                    {
                        connection = SockIOPool.GetInstance(memCachedConfigInfo.PoolName).GetConnection(str2);
                        if (connection != null)
                        {
                            target = Utils.MergeString(str2, target);
                        }
                    }
                    finally
                    {
                        if (connection != null)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return Utils.SplitString(target, ",");
        }

        public static string GetSocketHost(string key)
        {
            string host = "";
            SockIO sock = null;
            try
            {
                sock = SockIOPool.GetInstance(memCachedConfigInfo.PoolName).GetSock(key);
                if (sock != null)
                {
                    host = sock.Host;
                }
            }
            finally
            {
                if (sock != null)
                {
                    sock.Close();
                }
            }
            return host;
        }

        public static ArrayList GetStats()
        {
            ArrayList serverArrayList = new ArrayList();
            foreach (string str in serverList)
            {
                serverArrayList.Add(str);
            }
            return GetStats(serverArrayList, Stats.Default, null);
        }

        public static ArrayList GetStats(ArrayList serverArrayList, Stats statsCommand, string param)
        {
            ArrayList list = new ArrayList();
            param = Utils.StrIsNullOrEmpty(param) ? "" : param.Trim().ToLower();
            string command = "stats";
            switch (statsCommand)
            {
                case Stats.Reset:
                    command = "stats reset";
                    break;

                case Stats.Malloc:
                    command = "stats malloc";
                    break;

                case Stats.Maps:
                    command = "stats maps";
                    break;

                case Stats.Sizes:
                    command = "stats sizes";
                    break;

                case Stats.Slabs:
                    command = "stats slabs";
                    break;

                case Stats.Items:
                    command = "stats";
                    break;

                case Stats.CachedDump:
                {
                    string[] strNumber = Utils.SplitString(param, " ");
                    if ((strNumber.Length == 2) && Utils.IsNumericArray(strNumber))
                    {
                        command = "stats cachedump  " + param;
                    }
                    break;
                }
                case Stats.Detail:
                    if ((string.Equals(param, "on") || string.Equals(param, "off")) || string.Equals(param, "dump"))
                    {
                        command = "stats detail " + param.Trim();
                    }
                    break;

                default:
                    command = "stats";
                    break;
            }
            Hashtable hashtable = CacheClient.Stats(serverArrayList, command);
            foreach (string str2 in hashtable.Keys)
            {
                list.Add(str2);
                Hashtable hashtable2 = (Hashtable) hashtable[str2];
                foreach (string str3 in hashtable2.Keys)
                {
                    list.Add(str3 + ":" + hashtable2[str3]);
                }
            }
            return list;
        }

        public static MemcachedClient CacheClient
        {
            get
            {
                if (mc == null)
                {
                    CreateManager();
                }
                return mc;
            }
        }

        public static string[] ServerList
        {
            get
            {
                return serverList;
            }
            set
            {
                if (value != null)
                {
                    serverList = value;
                }
            }
        }

        public enum Stats
        {
            Default,
            Reset,
            Malloc,
            Maps,
            Sizes,
            Slabs,
            Items,
            CachedDump,
            Detail
        }
    }
}

