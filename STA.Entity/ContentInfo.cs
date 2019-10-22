using System;
using System.Collections;

namespace STA.Entity
{
    /// <summary>
    ///Content的实体。
    /// </summary>
    [Serializable]
    public class ContentInfo
    {
        #region 变量定义
        private int id = 0;
        private short typeid = 0;
        private string typename = string.Empty;
        private string addusername = string.Empty;
        private string lasteditusername = string.Empty;
        private string channelfamily = string.Empty;
        private int channelid = 0;
        private string channelname = string.Empty;
        private string title = string.Empty;
        private string subtitle = string.Empty;
        private DateTime addtime = DateTime.Now;
        private DateTime updateitme = DateTime.Now;
        private string color = "000000";
        private string property = string.Empty;
        private int addUser = 0;
        private int lastEditUser = 0;
        private string author = string.Empty;
        private string source = string.Empty;
        private string img = string.Empty;
        private string url = string.Empty;
        private string seotitle = string.Empty;
        private string seokeywords = string.Empty;
        private string seodescription = string.Empty;
        private string savepath = string.Empty;
        private string filename = string.Empty;
        private string template = string.Empty;
        private string content = string.Empty;
        private byte status = 0;
        private string viewgroup = string.Empty;
        private byte iscomment = 1;
        private byte ishtml = 0; //0未生成 1已生成 
        private int click = 0;
        private int orderid = 0;
        private int diggcount = 0;
        private int stampcount = 0;
        private int credits = 0;
        private string tags = string.Empty;
        private string extchannels = string.Empty;
        private int commentcount = 0;
        private string relates = string.Empty;
        private Hashtable ext;
        #endregion

        #region 字段属性
        /// <summary>
        /// 扩展字段
        /// </summary>
        public Hashtable Ext
        {
            get { return ext; }
            set { ext = value; }
        }
        public string Relates
        {
            get { return relates; }
            set { relates = value; }
        }
        public int Commentcount
        {
            get { return commentcount; }
            set { commentcount = value; }
        }

        public int Adduser
        {
            get { return addUser; }
            set { addUser = value; }
        }
        public string Lasteditusername
        {
            get { return lasteditusername; }
            set { lasteditusername = value; }
        }
        public string Addusername
        {
            get { return addusername; }
            set { addusername = value; }
        }

        public string Typename
        {
            get { return typename; }
            set { typename = value; }
        }
        public int Lastedituser
        {
            get { return lastEditUser; }
            set { lastEditUser = value; }
        }
        public DateTime Updatetime
        {
            get { return updateitme; }
            set { updateitme = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Extchannels
        {
            get { return extchannels; }
            set { extchannels = value; }
        }
        public short Typeid
        {
            get { return typeid; }
            set { typeid = value; }
        }

        public string Channelfamily
        {
            get { return channelfamily; }
            set { channelfamily = value.Trim(); }
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

        public string Title
        {
            get { return title; }
            set { title = value.Trim(); }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value.Trim(); }
        }

        public string Property
        {
            get { return property; }
            set { property = value.Trim(); }
        }

        public string Author
        {
            get { return author; }
            set { author = value.Trim(); }
        }

        public string Source
        {
            get { return source; }
            set { source = value.Trim(); }
        }

        public string Img
        {
            get { return img; }
            set { img = value.Trim(); }
        }

        public string Url
        {
            get { return url; }
            set { url = value.Trim(); }
        }

        public string Seotitle
        {
            get { return seotitle; }
            set { seotitle = value.Trim(); }
        }

        public string Seokeywords
        {
            get { return seokeywords; }
            set { seokeywords = value.Trim(); }
        }

        public string Seodescription
        {
            get { return seodescription; }
            set { seodescription = value.Trim(); }
        }

        public string Savepath
        {
            get { return savepath; }
            set { savepath = value.Trim(); }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value.Trim(); }
        }

        public string Template
        {
            get { return template; }
            set { template = value.Trim(); }
        }

        public string Content
        {
            get { return content; }
            set { content = value.Trim(); }
        }

        public byte Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Viewgroup
        {
            get { return viewgroup; }
            set { viewgroup = value; }
        }

        public byte Iscomment
        {
            get { return iscomment; }
            set { iscomment = value; }
        }

        public byte Ishtml
        {
            get { return ishtml; }
            set { ishtml = value; }
        }

        public int Click
        {
            get { return click; }
            set { click = value; }
        }

        /// <summary>
        /// <-1000为回收站
        /// </summary>
        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }

        public int Diggcount
        {
            get { return diggcount; }
            set { diggcount = value; }
        }

        public int Stampcount
        {
            get { return stampcount; }
            set { stampcount = value; }
        }

        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        #endregion

        #region 副本
        public ContentInfo Clone()
        {
            return (ContentInfo)this.MemberwiseClone();
        }
        #endregion
    }

    #region 数据参数

    public class DataParmInfo
    {
        private int num = 0;  //调用条数
        private string type = ""; //读取类型 如按频道  按专题 按内容组
        private int ctype = -1; //内容模型ID
        private string id = ""; //相关Id集合 1,3
        private string ext = ""; //模型标识(调用扩展的数据的时候用)
        private string fields = ""; //调用字段
        private int order = 0; //排序方式   0 时间  1权重  
        private int page = 1; //页码
        private int self = 0; //是否只调用本频道
        private string propery = "";
        private int group = 0; //专题组
        private int uid = 0;
        private string cachekey = "";
        private int durdate = 0; //多少天的数据
        private int ordertype = 1; //1降序2升序
        private string likeid = "";

        #region 属性

        public string Likeid
        {
            get { return likeid; }
            set { likeid = value; }
        }

        public int Ctype
        {
            get { return ctype; }
            set { ctype = value; }
        }

        public int Durdate
        {
            get { return durdate; }
            set { durdate = value; }
        }

        public string Propery
        {
            get { return propery; }
            set { propery = value; }
        }

        /// <summary>
        /// 专题组id
        /// </summary>
        public int Group
        {
            get { return group; }
            set { group = value; }
        }

        public int Ordertype
        {
            get { return ordertype; }
            set { ordertype = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        /// <summary>
        /// 缓存键名
        /// </summary>
        public string Cachekey
        {
            get { return cachekey; }
            set { cachekey = value; }
        }

        /// <summary>
        /// 是否只调用本频道
        /// </summary>
        public int Self
        {
            get { return self; }
            set { self = value; }
        }

        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public string Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        #endregion

    }

    #endregion

    public enum ConStatus
    {
        草稿 = 0, 待审核 = 1, 通过 = 2, 退稿 = 3
    }
}
