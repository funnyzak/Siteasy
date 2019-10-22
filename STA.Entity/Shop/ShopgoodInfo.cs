using System;

namespace STA.Entity
{
	/// <summary>
	///Shopgood的实体。
	/// </summary>
	[Serializable]
	public class ShopgoodInfo
	{
		#region 变量定义
		private int id = 0;
        private GoodType gtype = GoodType.实物商品; //商品类型 1商品 2虚拟商品
		private int cid = 0;  
		private string oid = string.Empty;  
		private int uid = 0;  
		private string username = string.Empty;  
		private string goodname = string.Empty;  
		private decimal price = 0;  
		private int buynum = 0;  
		#endregion
		
		#region 构造函数
		public ShopgoodInfo()
		{			
		}
		
		public ShopgoodInfo(int id, int cid, string oid, int uid, string username, string goodname, decimal price, int buynum)
		{
			this.Id = id;
			this.Cid = cid;
			this.Oid = oid;
			this.Uid = uid;
			this.Username = username;
			this.Goodname = goodname;
			this.Price = price;
			this.Buynum = buynum;
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

		public int Cid
		{
			get { return cid; }
			set { cid = value; }
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

		public string Goodname
		{
			get { return goodname; }
			set { goodname = value.Trim(); }
		}		

		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}		

		public int Buynum
		{
			get { return buynum; }
			set { buynum = value; }
		}		
		#endregion
		
		#region 副本
        public ShopgoodInfo Clone()
        {
            return (ShopgoodInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
