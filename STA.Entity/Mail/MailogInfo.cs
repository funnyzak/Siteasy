using System;

namespace STA.Entity
{
	/// <summary>
	///Mailog的实体。
	/// </summary>
	[Serializable]
	public class MailogInfo
	{
		#region 变量定义
		private int m_id = 0;
        private string m_rgroup = string.Empty;
        private string m_mails = string.Empty;
		private string m_title = string.Empty;  
		private DateTime m_addtime = DateTime.Now;  
		private string m_content = string.Empty;  
		private int m_userid = 0;  
		private string m_username = string.Empty;  
		#endregion
		
		#region 构造函数
		public MailogInfo()
		{			
		}
		
		public MailogInfo(int m_id, string m_title, DateTime m_addtime, string m_content, int m_userid, string m_username)
		{
			this.Id = m_id;
			this.Title = m_title;
			this.Addtime = m_addtime;
			this.Content = m_content;
			this.Userid = m_userid;
			this.Username = m_username;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return m_id; }
			set { m_id = value; }
		}

        public string Rgroup
        {
            get { return m_rgroup; }
            set { m_rgroup = value; }
        }

        public string Mails
        {
            get { return m_mails; }
            set { m_mails = value; }
        }

		public string Title
		{
			get { return m_title; }
			set { m_title = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return m_addtime; }
			set { m_addtime = value; }
		}		

		public string Content
		{
			get { return m_content; }
			set { m_content = value.Trim(); }
		}		

		public int Userid
		{
			get { return m_userid; }
			set { m_userid = value; }
		}		

		public string Username
		{
			get { return m_username; }
			set { m_username = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public MailogInfo Clone()
        {
            return (MailogInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
