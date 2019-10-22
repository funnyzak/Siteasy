using System;

namespace STA.Entity
{
	/// <summary>
	///Userconnect的实体。
	/// </summary>
	[Serializable]
	public class UserconnectInfo
	{
		#region 变量定义
		private int id = 0;  
		private int uid = 0;  
		private string appidentify = string.Empty;  
		private string openid = string.Empty;  
		private string token = string.Empty;  
		private string secret = string.Empty;  
		private string ext = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		#endregion
		
		#region 构造函数
		public UserconnectInfo()
		{			
		}
		
		public UserconnectInfo(int id, int uid, string appidentify, string openid, string token, string secret, string ext, DateTime addtime)
		{
			this.Id = id;
			this.Uid = uid;
			this.Appidentify = appidentify;
			this.Openid = openid;
			this.Token = token;
			this.Secret = secret;
			this.Ext = ext;
			this.Addtime = addtime;
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

		public string Appidentify
		{
			get { return appidentify; }
			set { appidentify = value.Trim(); }
		}		

		public string Openid
		{
			get { return openid; }
			set { openid = value.Trim(); }
		}		

		public string Token
		{
			get { return token; }
			set { token = value.Trim(); }
		}		

		public string Secret
		{
			get { return secret; }
			set { secret = value.Trim(); }
		}		

		public string Ext
		{
			get { return ext; }
			set { ext = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		
		#endregion
		
		#region 副本
        public UserconnectInfo Clone()
        {
            return (UserconnectInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
