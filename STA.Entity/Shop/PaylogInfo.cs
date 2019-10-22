using System;

namespace STA.Entity
{
    /// <summary>
    ///Paylog的实体。
    /// </summary>
    [Serializable]
    public class PaylogInfo
    {
        #region 变量定义
        private int m_id = 0;
        private string m_oid = string.Empty;
        private int m_uid = 0;
        private string m_username = string.Empty;
        private string m_title = string.Empty;
        private GoodType m_gtype = GoodType.实物商品;
        private decimal m_amount = 0;
        private int m_payid = 0;
        private string m_payname = string.Empty;
        private DateTime m_addtime = DateTime.Now;
        #endregion

        #region 构造函数
        public PaylogInfo()
        {
        }

        public PaylogInfo(int m_id, string m_oid, int m_uid, string m_username, string m_title, GoodType m_gtype, decimal m_amount, int m_payid, string m_payname, DateTime m_addtime)
        {
            this.Id = m_id;
            this.Oid = m_oid;
            this.Uid = m_uid;
            this.Username = m_username;
            this.Title = m_title;
            this.Gtype = m_gtype;
            this.Amount = m_amount;
            this.Payid = m_payid;
            this.Payname = m_payname;
            this.Addtime = m_addtime;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public string Oid
        {
            get { return m_oid; }
            set { m_oid = value.Trim(); }
        }

        public int Uid
        {
            get { return m_uid; }
            set { m_uid = value; }
        }

        public string Username
        {
            get { return m_username; }
            set { m_username = value.Trim(); }
        }

        public string Title
        {
            get { return m_title; }
            set { m_title = value.Trim(); }
        }

        public GoodType Gtype
        {
            get { return m_gtype; }
            set { m_gtype = value; }
        }

        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        public int Payid
        {
            get { return m_payid; }
            set { m_payid = value; }
        }

        public string Payname
        {
            get { return m_payname; }
            set { m_payname = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return m_addtime; }
            set { m_addtime = value; }
        }
        #endregion

        #region 副本
        public PaylogInfo Clone()
        {
            return (PaylogInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum GoodType
    {
        实物商品 = 1,
        购物卡 = 2,
        点卡 = 3
    }
}
