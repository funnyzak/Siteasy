using System;

namespace STA.Entity
{
    /// <summary>
    ///Congroup的实体。
    /// </summary>
    [Serializable]
    public class CongroupInfo
    {
        #region 变量定义
        private int id = 0;
        private byte type = 0;
        private string name = string.Empty;
        private DateTime addtime = DateTime.Now;
        private string desctext = string.Empty;
        #endregion

        #region 构造函数
        public CongroupInfo()
        {
        }

        public CongroupInfo(int id, byte type, string name, DateTime addtime, string desctext)
        {
            this.Id = id;
            this.Type = type;
            this.Name = name;
            this.Addtime = addtime;
            this.Desctext = desctext;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Desctext
        {
            get { return desctext; }
            set { desctext = value.Trim(); }
        }
        #endregion

        #region 副本
        public CongroupInfo Clone()
        {
            return (CongroupInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
