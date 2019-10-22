using System;

namespace STA.Entity
{
	/// <summary>
	///Submail的实体。
	/// </summary>
	[Serializable]
	public class MailsubInfo
	{
		#region 变量定义
		private int m_id = 0;  
		private string m_name = string.Empty;  
		private DateTime m_addtime = DateTime.Now;  
		private string m_mail = string.Empty;  
		private string m_ip = string.Empty;  
		private byte m_status = 1;  
		private string m_safecode = Guid.NewGuid().ToString();  
		private string m_forgroup = string.Empty;  
		#endregion
		
		#region 构造函数
		public MailsubInfo()
		{			
		}
		
		public MailsubInfo(int m_id, string m_name, DateTime m_addtime, string m_mail, string m_ip, byte m_status, string m_safecode, string m_forgroup)
		{
			this.Id = m_id;
			this.Name = m_name;
			this.Addtime = m_addtime;
			this.Mail = m_mail;
			this.Ip = m_ip;
			this.Status = m_status;
			this.Safecode = m_safecode;
			this.Forgroup = m_forgroup;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return m_id; }
			set { m_id = value; }
		}		

		public string Name
		{
			get { return m_name; }
			set { m_name = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return m_addtime; }
			set { m_addtime = value; }
		}		

		public string Mail
		{
			get { return m_mail; }
			set { m_mail = value.Trim(); }
		}		

		public string Ip
		{
			get { return m_ip; }
			set { m_ip = value.Trim(); }
		}		

		public byte Status
		{
			get { return m_status; }
			set { m_status = value; }
		}		

		public string Safecode
		{
			get { return m_safecode; }
			set { m_safecode = value.Trim(); }
		}		

		public string Forgroup
		{
			get { return m_forgroup; }
			set { m_forgroup = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public MailsubInfo Clone()
        {
            return (MailsubInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
