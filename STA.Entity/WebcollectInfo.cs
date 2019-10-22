using System;

namespace STA.Entity
{
    /// <summary>
    ///Webcollect的实体。
    /// </summary>
    [Serializable]
    public class WebcollectInfo
    {
        #region 变量定义
        public static string ats = "urllist,url,title,source,author,addtime,content,conpage,conpageurl";
        private int id = 0;
        private string name = string.Empty;
        private int channelid = 0;
        private string channelname = string.Empty;
        private byte constatus = 0;
        private DateTime addtime = DateTime.Now;
        private string hosturl = string.Empty;
        private CollectType collecttype = CollectType.单页;
        private string curl = string.Empty;
        private string clisturl = string.Empty;
        private string clistpage = string.Empty;
        private string curls = string.Empty;
        private string encode = string.Empty;
        private string property = string.Empty;
        private string filter = string.Empty;
        private string attrs = string.Empty;
        private string setting = string.Empty;
        private string confilter = string.Empty;
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public int Channelid
        {
            get { return channelid; }
            set { channelid = value; }
        }

        public string Channelname
        {
            get { return channelname; }
            set { channelname = value.Trim(); }
        }

        public string Confilter
        {
            get { return confilter; }
            set { confilter = value; }
        }

        public byte Constatus
        {
            get { return constatus; }
            set { constatus = value; }
        }
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Hosturl
        {
            get { return hosturl; }
            set { hosturl = value.Trim(); }
        }

        public CollectType Collecttype
        {
            get { return collecttype; }
            set { collecttype = value; }
        }

        public string Curl
        {
            get { return curl; }
            set { curl = value.Trim(); }
        }

        public string Clisturl
        {
            get { return clisturl; }
            set { clisturl = value.Trim(); }
        }

        public string Clistpage
        {
            get { return clistpage; }
            set { clistpage = value.Trim(); }
        }

        public string Curls
        {
            get { return curls; }
            set { curls = value.Trim(); }
        }

        public string Encode
        {
            get { return encode; }
            set { encode = value.Trim(); }
        }

        public string Property
        {
            get { return property; }
            set { property = value.Trim(); }
        }

        public string Filter
        {
            get { return filter; }
            set { filter = value.Trim(); }
        }

        public string Attrs
        {
            get { return attrs; }
            set { attrs = value.Trim(); }
        }

        public string Setting
        {
            get { return setting; }
            set { setting = value.Trim(); }
        }
        #endregion

        #region 副本
        public WebcollectInfo Clone()
        {
            return (WebcollectInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum CollectType
    {
        单页 = 3, 索引分页 = 1, 网址集合 = 2
    }
}
