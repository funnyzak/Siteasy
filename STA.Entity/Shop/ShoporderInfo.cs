using System;

namespace STA.Entity
{
    /// <summary>
    ///Shoporder的实体。
    /// </summary>
    [Serializable]
    public class ShoporderInfo
    {
        #region 变量定义
        private int id = 0;
        private string oid = string.Empty;
        private int uid = 0;
        private string username = string.Empty;
        private GoodType gtype = GoodType.实物商品;
        private int did = 0;
        private int pid = 0;
        private int adrid = 0;
        private int cartcount = 0;
        private decimal dprice = 0;
        private decimal price = 0;
        private decimal totalprice = 0;
        private OrderStatus status = OrderStatus.未处理;
        private string ip = string.Empty;
        private byte isinvoice = 0;
        private string invoicehead = string.Empty;
        private decimal invoicecost = 0;
        private DateTime addtime = DateTime.Now;
        private string remark = string.Empty;
        private string parms = string.Empty;
        #endregion

        #region 构造函数
        public ShoporderInfo()
        {
        }

        public ShoporderInfo(int id, string oid, int uid, string username, int did, int pid, int cartcount, decimal dprice, decimal price, decimal totalprice, OrderStatus status, string ip, DateTime addtime, string remark)
        {
            this.Id = id;
            this.Oid = oid;
            this.Uid = uid;
            this.Username = username;
            this.Did = did;
            this.Pid = pid;
            this.Cartcount = cartcount;
            this.Dprice = dprice;
            this.Price = price;
            this.Totalprice = totalprice;
            this.Status = status;
            this.Ip = ip;
            this.Addtime = addtime;
            this.Remark = remark;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public GoodType Gtype
        {
            get { return gtype; }
            set { gtype = value; }
        }

        public byte Isinvoice
        {
            get { return isinvoice; }
            set { isinvoice = value; }
        }
        public string Invoicehead
        {
            get { return invoicehead; }
            set { invoicehead = value; }
        }
        public decimal Invoicecost
        {
            get { return invoicecost; }
            set { invoicecost = value; }
        }
        public int Adrid
        {
            get { return adrid; }
            set { adrid = value; }
        }
        public string Oid
        {
            get { return oid; }
            set { oid = value.Trim(); }
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

        public int Did
        {
            get { return did; }
            set { did = value; }
        }

        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public int Cartcount
        {
            get { return cartcount; }
            set { cartcount = value; }
        }

        public decimal Dprice
        {
            get { return dprice; }
            set { dprice = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public decimal Totalprice
        {
            get { return totalprice; }
            set { totalprice = value; }
        }

        public OrderStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value.Trim(); }
        }

        public string Parms
        {
            get { return parms; }
            set { parms = value; }
        }
        #endregion

        #region 副本
        public ShoporderInfo Clone()
        {
            return (ShoporderInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum OrderStatus
    {
        管理员取消 = 1,
        用户取消 = 2,
        未处理 = 3,
        已付款 = 4,
        已收款 = 5,
        已发货 = 6,
        已收货 = 7,
        已完成 = 8
    }
}
