using System;

namespace STA.Entity
{
	/// <summary>
	///Appconnect的实体。
	/// </summary>
	[Serializable]
	public class AppconnectInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string description = string.Empty;  
		private string identify = string.Empty;  
		private byte isvalid = 0;  
		private string img = string.Empty;  
		private string appid = string.Empty;  
		private string appkey = string.Empty;  
		private int orderid = 0;  
		private string ext = string.Empty;  
		#endregion
		
		#region 构造函数
		public AppconnectInfo()
		{			
		}
		
		public AppconnectInfo(int id, string name, string description, string identify, byte isvalid, string img, string appid, string appkey, int orderid, string ext)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.Identify = identify;
			this.Isvalid = isvalid;
			this.Img = img;
			this.Appid = appid;
			this.Appkey = appkey;
			this.Orderid = orderid;
			this.Ext = ext;
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

		public string Description
		{
			get { return description; }
			set { description = value.Trim(); }
		}		

		public string Identify
		{
			get { return identify; }
			set { identify = value.Trim(); }
		}		

		public byte Isvalid
		{
			get { return isvalid; }
			set { isvalid = value; }
		}		

		public string Img
		{
			get { return img; }
			set { img = value.Trim(); }
		}		

		public string Appid
		{
			get { return appid; }
			set { appid = value.Trim(); }
		}		

		public string Appkey
		{
			get { return appkey; }
			set { appkey = value.Trim(); }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		

		public string Ext
		{
			get { return ext; }
			set { ext = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public AppconnectInfo Clone()
        {
            return (AppconnectInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
