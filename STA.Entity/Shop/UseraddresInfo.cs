using System;

namespace STA.Entity
{
	/// <summary>
	///Useraddres的实体。
	/// </summary>
	[Serializable]
	public class UseraddressInfo
	{
		#region 变量定义
		private int id = 0;  
		private int uid = 0;  
		private string username = string.Empty;  
		private string title = string.Empty;  
		private string email = string.Empty;  
		private string address = string.Empty;  
		private string postcode = string.Empty;  
		private string phone = string.Empty;  
		private string parms = string.Empty;  
		#endregion
		
		#region 构造函数
		public UseraddressInfo()
		{			
		}
		
		public UseraddressInfo(int id, int uid, string username, string title, string email, string address, string postcode, string phone, string parms)
		{
			this.Id = id;
			this.Uid = uid;
			this.Username = username;
			this.Title = title;
			this.Email = email;
			this.Address = address;
			this.Postcode = postcode;
			this.Phone = phone;
			this.Parms = parms;
		}
		#endregion
		
		#region 字段属性
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

		public string Username
		{
			get { return username; }
			set { username = value.Trim(); }
		}		

		public string Title
		{
			get { return title; }
			set { title = value.Trim(); }
		}		

		public string Email
		{
			get { return email; }
			set { email = value.Trim(); }
		}		

		public string Address
		{
			get { return address; }
			set { address = value.Trim(); }
		}		

		public string Postcode
		{
			get { return postcode; }
			set { postcode = value.Trim(); }
		}		

		public string Phone
		{
			get { return phone; }
			set { phone = value.Trim(); }
		}		

        /// <summary>
        /// 500,1  省份,性别
        /// </summary>
		public string Parms
		{
			get { return parms; }
			set { parms = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public UseraddressInfo Clone()
        {
            return (UseraddressInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
