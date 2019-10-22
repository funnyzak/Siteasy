using System;

namespace STA.Entity
{
	/// <summary>
	///Usergroup的实体。
	/// </summary>
	[Serializable]
	public class UserGroupInfo
	{
		#region 变量定义
		private int id = 0;  
		private int system = 0;  
		private string name = string.Empty;  
		private int creditsmax = 0;  
		private int creditsmin = 0;  
		private string color = string.Empty;  
		private string avatar = string.Empty;  
		private int star = 0;  
		#endregion
		
		#region 构造函数
		public UserGroupInfo()
		{			
		}
		
		public UserGroupInfo(int id,  int system, string name, int creditsmax, int creditsmin, string color, string avatar, int star)
		{
			this.Id = id;
			this.System = system;
			this.Name = name;
			this.Creditsmax = creditsmax;
			this.Creditsmin = creditsmin;
			this.Color = color;
			this.Avatar = avatar;
			this.Star = star;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}			

		public int System
		{
			get { return system; }
			set { system = value; }
		}		

		public string Name
		{
			get { return name; }
			set { name = value.Trim(); }
		}		

		public int Creditsmax
		{
			get { return creditsmax; }
			set { creditsmax = value; }
		}		

		public int Creditsmin
		{
			get { return creditsmin; }
			set { creditsmin = value; }
		}		

		public string Color
		{
			get { return color; }
			set { color = value.Trim(); }
		}		

		public string Avatar
		{
			get { return avatar; }
			set { avatar = value.Trim(); }
		}		

		public int Star
		{
			get { return star; }
			set { star = value; }
		}		
		#endregion
		
		#region 副本
        public UserGroupInfo Clone()
        {
            return (UserGroupInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
