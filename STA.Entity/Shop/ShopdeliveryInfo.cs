using System;

namespace STA.Entity
{
	/// <summary>
	///Shopdelivery的实体。
	/// </summary>
	[Serializable]
	public class ShopdeliveryInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string ename = string.Empty;  
		private string description = string.Empty;  
		private decimal fweight = 0;  
		private byte iscod = 0;  
		private string parms = string.Empty;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public ShopdeliveryInfo()
		{			
		}
		
		public ShopdeliveryInfo(int id, string name, string ename, string description, decimal fweight, byte iscod, string parms, int orderid)
		{
			this.Id = id;
			this.Name = name;
			this.Ename = ename;
			this.Description = description;
			this.Fweight = fweight;
			this.Iscod = iscod;
			this.Parms = parms;
			this.Orderid = orderid;
		}
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

		public string Ename
		{
			get { return ename; }
			set { ename = value.Trim(); }
		}		

		public string Description
		{
			get { return description; }
			set { description = value.Trim(); }
		}		

		public decimal Fweight
		{
			get { return fweight; }
			set { fweight = value; }
		}		

		public byte Iscod
		{
			get { return iscod; }
			set { iscod = value; }
		}		

		public string Parms
		{
			get { return parms; }
			set { parms = value.Trim(); }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public ShopdeliveryInfo Clone()
        {
            return (ShopdeliveryInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
