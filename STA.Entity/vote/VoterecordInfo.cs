using System;

namespace STA.Entity
{
	/// <summary>
	///Voterecord的实体。
	/// </summary>
	[Serializable]
	public class VoterecordInfo
	{
		#region 变量定义
		private int id = 0;  
		private int topicid = 0;  
		private string topicname = string.Empty;  
		private string optionids = "";  
		private int userid = 0;  
		private string username = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private string userip = string.Empty;  
		private string realname = string.Empty;  
		private string idcard = string.Empty;  
		private string phone = string.Empty;  
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public int Topicid
		{
			get { return topicid; }
			set { topicid = value; }
		}		

		public string Topicname
		{
			get { return topicname; }
			set { topicname = value.Trim(); }
		}		

		public string Optionids
		{
			get { return optionids; }
			set { optionids = value; }
		}		

		public int Userid
		{
			get { return userid; }
			set { userid = value; }
		}		

		public string Username
		{
			get { return username; }
			set { username = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public string Userip
		{
			get { return userip; }
			set { userip = value.Trim(); }
		}		

		public string Realname
		{
			get { return realname; }
			set { realname = value.Trim(); }
		}		

		public string Idcard
		{
			get { return idcard; }
			set { idcard = value.Trim(); }
		}		

		public string Phone
		{
			get { return phone; }
			set { phone = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public VoterecordInfo Clone()
        {
            return (VoterecordInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
