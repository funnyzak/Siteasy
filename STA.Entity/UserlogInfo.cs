using System;

namespace STA.Entity
{
	/// <summary>
	///Userlog的实体。
	/// </summary>
	[Serializable]
	public class UserlogInfo
	{
		#region 变量定义
		private int id = 0;  
		private int uid = 0;  
		private string username = string.Empty;  
		private string ip = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private string action = string.Empty;  
		private int m_value = 0;  
		private string remark = string.Empty;  
		private string identify = string.Empty;  
		#endregion
		
		#region 构造函数
		public UserlogInfo()
		{			
		}
		
		public UserlogInfo(int id, int uid, string username, string ip, DateTime addtime, string action, int value, string remark, string identify)
		{
			this.Id = id;
			this.Uid = uid;
			this.Username = username;
			this.Ip = ip;
			this.Addtime = addtime;
			this.Action = action;
			this.Value = value;
			this.Remark = remark;
			this.Identify = identify;
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

		public string Action
		{
			get { return action; }
			set { action = value.Trim(); }
		}		

		public int Value
		{
			get { return m_value; }
			set { m_value = value; }
		}		

		public string Remark
		{
			get { return remark; }
			set { remark = value.Trim(); }
		}		

		public string Identify
		{
			get { return identify; }
			set { identify = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public UserlogInfo Clone()
        {
            return (UserlogInfo)this.MemberwiseClone();
        }
		#endregion
	}
}