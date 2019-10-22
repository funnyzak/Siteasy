using System;

namespace STA.Entity
{
	/// <summary>
	///Magazine的实体。
	/// </summary>
	[Serializable]
	public class MagazineInfo
	{
		#region 变量定义
		private int m_id = 0;  
		private string m_name = string.Empty;  
		private DateTime m_addtime = DateTime.Now;  
		private DateTime m_updatetime = DateTime.Now;  
		private string m_likeid = string.Empty;  
		private string m_ratio = string.Empty;  
		private string m_cover = string.Empty;  
		private string m_description = string.Empty;  
		private string m_content = string.Empty;  
		private int m_pages = 0;  
		private int m_orderid = 0;  
		private byte m_status = 0;  
		private int m_click = 0;  
		private string m_parms = string.Empty;  
		#endregion
		
		#region 构造函数
		public MagazineInfo()
		{			
		}
		
		public MagazineInfo(int m_id, string m_name, DateTime m_addtime, DateTime m_updatetime, string m_likeid, string m_ratio, string m_cover, string m_description, string m_content, int m_pages, int m_orderid, byte m_status, int m_click, string m_parms)
		{
			this.Id = m_id;
			this.Name = m_name;
			this.Addtime = m_addtime;
			this.Updatetime = m_updatetime;
			this.Likeid = m_likeid;
			this.Ratio = m_ratio;
			this.Cover = m_cover;
			this.Description = m_description;
			this.Content = m_content;
			this.Pages = m_pages;
			this.Orderid = m_orderid;
			this.Status = m_status;
			this.Click = m_click;
			this.Parms = m_parms;
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

		public DateTime Updatetime
		{
			get { return m_updatetime; }
			set { m_updatetime = value; }
		}		

		public string Likeid
		{
			get { return m_likeid; }
			set { m_likeid = value.Trim(); }
		}		

		public string Ratio
		{
			get { return m_ratio; }
			set { m_ratio = value.Trim(); }
		}		

		public string Cover
		{
			get { return m_cover; }
			set { m_cover = value.Trim(); }
		}		

		public string Description
		{
			get { return m_description; }
			set { m_description = value.Trim(); }
		}		

		public string Content
		{
			get { return m_content; }
			set { m_content = value.Trim(); }
		}		

		public int Pages
		{
			get { return m_pages; }
			set { m_pages = value; }
		}		

		public int Orderid
		{
			get { return m_orderid; }
			set { m_orderid = value; }
		}		

		public byte Status
		{
			get { return m_status; }
			set { m_status = value; }
		}		

		public int Click
		{
			get { return m_click; }
			set { m_click = value; }
		}		

		public string Parms
		{
			get { return m_parms; }
			set { m_parms = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public MagazineInfo Clone()
        {
            return (MagazineInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
