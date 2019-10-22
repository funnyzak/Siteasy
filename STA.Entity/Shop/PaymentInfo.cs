using System;

namespace STA.Entity
{
	/// <summary>
	///Payment的实体。
	/// </summary>
	[Serializable]
	public class PaymentInfo
	{
		#region 变量定义
		private int id = 0;  
		private string dll = string.Empty;  
		private string name = string.Empty;  
		private string description = string.Empty;  
		private string pic = string.Empty;  
		private byte isvalid = 0;  
		private string parms = string.Empty;  
		private string version = string.Empty;  
		private string author = string.Empty;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public PaymentInfo()
		{			
		}
		
		public PaymentInfo(int id, string dll, string name, string description, string pic, byte isvalid, string parms, string version, string author, int orderid)
		{
			this.Id = id;
			this.Dll = dll;
			this.Name = name;
			this.Description = description;
			this.Pic = pic;
			this.Isvalid = isvalid;
			this.Parms = parms;
			this.Version = version;
			this.Author = author;
			this.Orderid = orderid;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public string Dll
		{
			get { return dll; }
			set { dll = value.Trim(); }
		}		

		public string Name
		{
			get { return name; }
			set { name = value.Trim(); }
		}		

		public string Description
		{
			get { return description; }
			set { description = value.Trim(); }
		}		

		public string Pic
		{
			get { return pic; }
			set { pic = value.Trim(); }
		}		

		public byte Isvalid
		{
			get { return isvalid; }
			set { isvalid = value; }
		}		

		public string Parms
		{
			get { return parms; }
			set { parms = value.Trim(); }
		}		

		public string Version
		{
			get { return version; }
			set { version = value.Trim(); }
		}		

		public string Author
		{
			get { return author; }
			set { author = value.Trim(); }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public PaymentInfo Clone()
        {
            return (PaymentInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
