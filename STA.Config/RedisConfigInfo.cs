using STA.Common;

namespace STA.Config
{
    /// <summary>
    /// Redis配置信息类文件
    /// </summary>
    public class RedisConfigInfo : IConfigInfo
    {
        private bool _applyRedis;
        /// <summary>
        /// 是否应用Redis
        /// </summary>
        public bool ApplyRedis
        {
            get
            {
                return _applyRedis;
            }
            set
            {
                _applyRedis = value;
            }
        }

        private string _writeServerList;
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        public string WriteServerList
        {
            get
            {
                return _writeServerList;
            }
            set
            {
                _writeServerList = value;
            }
        }

        private string _readServerList;
        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        public string ReadServerList
        {
            get
            {
                return _readServerList;
            }
            set
            {
                _readServerList = value;
            }
        }

        private int _maxWritePoolSize;
        /// <summary>
        /// 最大写链接数
        /// </summary>
        public int MaxWritePoolSize
        {
            get
            {
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                _maxWritePoolSize = value;
            }
        }

        private int _maxReadPoolSize;
        /// <summary>
        /// 最大读链接数
        /// </summary>
        public int MaxReadPoolSize
        {
            get
            {
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                _maxReadPoolSize = value;
            }
        }

        private bool _autoStart;
        /// <summary>
        /// 自动重启
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return _autoStart;
            }
            set
            {
                _autoStart = value;
            }
        }
        

        private int _localCacheTime = 30000;
        /// <summary>
        /// 本地缓存到期时间，该设置会与memcached搭配使用，单位:秒
        /// </summary>
        public int LocalCacheTime
        {
            get
            {
                return _localCacheTime;
            }
            set
            {
                _localCacheTime = value;
            }
        }

        private bool _recordeLog = false;
        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        public bool RecordeLog
        {
            get
            {
                return _recordeLog;
            }
            set
            {
                _recordeLog = value;
            }
        }

    }
}
