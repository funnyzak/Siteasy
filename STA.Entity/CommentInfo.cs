using System;

namespace STA.Entity
{
    public enum CommentStatus
    {
        待审核 = 0,
        通过 = 1,
        屏蔽 = 2,
        删除 = 3
    }
    /// <summary>
    ///Comment的实体。
    /// </summary>
    [Serializable]
    public class CommentInfo
    {
        #region 变量定义
        private int id = 0;
        private int cid = 0;
        private int ctype = 1;
        private int uid = 0;
        private string username = string.Empty;
        private string title = string.Empty;
        private DateTime addtime = DateTime.Now;
        private DateTime verifytime = DateTime.Now;
        private string userip = string.Empty;
        private CommentStatus status = CommentStatus.待审核; //0待审核 1通过 2屏蔽
        private int diggcount = 0;
        private int stampcount = 0;
        private string contitle = "";
        private string msg = string.Empty;
        private string quote = "";
        private int replay = 0;
        private string city = "";
        private string useragent = "";
        private int star = 0;
        #endregion

        #region 构造函数
        public CommentInfo()
        {
        }

        #endregion

        #region 字段属性

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public int Ctype
        {
            get { return ctype; }
            set { ctype = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Contitle
        {
            get { return contitle; }
            set { contitle = value; }
        }


        public string Useragent
        {
            get { return useragent; }
            set { useragent = value; }
        }

        public int Star
        {
            get { return star; }
            set { star = value; }
        }

        public int Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public DateTime Verifytime
        {
            get { return verifytime; }
            set { verifytime = value; }
        }

        public string Userip
        {
            get { return userip; }
            set { userip = value.Trim(); }
        }

        public CommentStatus Status
        {
            get { return status; }
            set { status = value; }
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

        public string Msg
        {
            get { return msg; }
            set { msg = value.Trim(); }
        }

        public string Quote
        {
            get { return quote; }
            set { quote = value; }
        }

        public int Replay
        {
            get { return replay; }
            set { replay = value; }
        }
        #endregion

        #region 副本
        public CommentInfo Clone()
        {
            return (CommentInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
