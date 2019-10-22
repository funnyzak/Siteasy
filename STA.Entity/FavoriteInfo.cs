using System;

namespace STA.Entity
{
    /// <summary>
    ///Favorite的实体。
    /// </summary>
    [Serializable]
    public class FavoriteInfo
    {
        #region 变量定义
        private int id = 0;
        private int uid = 0;
        private int cid = 0;
        private string likeid = "";
        private byte typeid = 1; //1文档  
        private DateTime favtime = DateTime.Now;
        #endregion

        #region 构造函数
        public FavoriteInfo()
        {
        }

        public FavoriteInfo(int id, int uid, int cid, byte typeid, DateTime favtime)
        {
            this.Id = id;
            this.Uid = uid;
            this.Cid = cid;
            this.Typeid = typeid;
            this.Favtime = favtime;
        }
        #endregion

        #region 字段属性

        public string Likeid
        {
            get { return likeid; }
            set { likeid = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public int Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public byte Typeid
        {
            get { return typeid; }
            set { typeid = value; }
        }

        public DateTime Favtime
        {
            get { return favtime; }
            set { favtime = value; }
        }
        #endregion

        #region 副本
        public FavoriteInfo Clone()
        {
            return (FavoriteInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
