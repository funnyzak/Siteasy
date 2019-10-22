using System;

namespace STA.Entity
{
    /// <summary>
    ///Select的实体。
    /// </summary>
    [Serializable]
    public class SelectInfo
    {
        #region 变量定义
        private int id = 0;
        private string name = string.Empty;
        private string xvalue = "";
        private string ename = string.Empty;
        private int orderid = 0;
        private byte issign = 0;
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

        public string Value
        {
            get { return xvalue; }
            set { xvalue = value; }
        }

        public string Ename
        {
            get { return ename; }
            set { ename = value.Trim(); }
        }

        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }

        public byte Issign
        {
            get { return issign; }
            set { issign = value; }
        }
        #endregion

        #region 副本
        public SelectInfo Clone()
        {
            return (SelectInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
