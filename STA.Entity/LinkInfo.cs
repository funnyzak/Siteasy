using System;

namespace STA.Entity
{
	/// <summary>
	///Link的实体。
	/// </summary>
	[Serializable]
	public class LinkInfo
	{
		#region 变量定义
		private int id = 0;  
		private int typeid = 0;  
		private string name = string.Empty;  
		private string url = string.Empty;  
		private string logo = string.Empty;  
		private string email = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private string description = string.Empty;  
		private int orderid = 0;  
		private byte status = 0;  
		#endregion
		
		#region 构造函数
		public LinkInfo()
		{			
		}
		
		public LinkInfo(int id, int typeid, string name, string url, string logo, string email, DateTime addtime, string description, int orderid, byte status)
		{
			this.Id = id;
			this.Typeid = typeid;
			this.Name = name;
			this.Url = url;
			this.Logo = logo;
			this.Email = email;
			this.Addtime = addtime;
			this.Description = description;
			this.Orderid = orderid;
			this.Status = status;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public int Typeid
		{
			get { return typeid; }
			set { typeid = value; }
		}		

		public string Name
		{
			get { return name; }
			set { name = value.Trim(); }
		}		

		public string Url
		{
			get { return url; }
			set { url = value.Trim(); }
		}		

		public string Logo
		{
			get { return logo; }
			set { logo = value.Trim(); }
		}		

		public string Email
		{
			get { return email; }
			set { email = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public string Description
		{
			get { return description; }
			set { description = value.Trim(); }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		

		public byte Status
		{
			get { return status; }
			set { status = value; }
		}		
		#endregion
		
		#region 副本
        public LinkInfo Clone()
        {
            return (LinkInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
