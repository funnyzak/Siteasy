namespace STA.CacLib
{
    using STA.Cache;
    using STA.Common;
    using STA.Config;
    using STA.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    public class MemCachedStrategy : DefaultCacheStrategy
    {
        public override void AddObject(string objId, object o)
        {
            base.AddObject(objId, o);
            MemCachedManager.CacheClient.Set(objId, o);
            this.RecordLog(objId, "set");
        }

        public override void AddObjectWithDepend(string objId, object o, string[] dependKey)
        {
        }

        public override void AddObjectWithFileChange(string objId, object o, string[] files)
        {
        }

        private void RecordLog(string objId, string opName)
        {
            if (MemCachedConfigs.GetConfig().RecordeLog)
            {
                DbParameter[] commandParameters = new DbParameter[] { DbHelper.MakeInParam("@cachekey", DbType.Int64, 200, objId), DbHelper.MakeInParam("@opname", DbType.Int64, 10, opName), DbHelper.MakeInParam("@postdatetime", DbType.Currency, 8, Utils.GetDateTime()) };
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO memcachedlogs (cachekey, opname, postdatetime) Values (@cachekey, @opname, @postdatetime)", commandParameters);
            }
        }

        public override void RemoveObject(string objId)
        {
            if (base.RetrieveObject(objId) != null)
            {
                base.RemoveObject(objId);
            }
            if (MemCachedManager.CacheClient.KeyExists(objId))
            {
                MemCachedManager.CacheClient.Delete(objId);
            }
            SyncCache.SyncRemoteCache(objId);
        }

        public override object RetrieveObject(string objId)
        {
            object obj2 = base.RetrieveObject(objId);
            if (obj2 == null)
            {
                obj2 = MemCachedManager.CacheClient.Get(objId);
                if (obj2 != null)
                {
                    base.AddObject(objId, obj2);
                }
                this.RecordLog(objId, "get");
            }
            return obj2;
        }

        public override int TimeOut
        {
            get
            {
                return MemCachedConfigs.GetConfig().LocalCacheTime;
            }
        }
    }
}

