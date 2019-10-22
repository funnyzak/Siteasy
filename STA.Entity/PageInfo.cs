using System;

namespace STA.Entity
{
	/// <summary>
	///Page的实体。
	/// </summary>
	[Serializable]
	public class PageInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string alikeid = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private string seotitle = string.Empty;  
		private string seokeywords = string.Empty;  
		private string seodescription = string.Empty;  
		private byte ishtml = 0;  
		private string savepath = string.Empty;  
		private string filename = string.Empty;  
		private string template = string.Empty;  
		private string content = string.Empty;
        private int orderid = 0;
		#endregion
		
		#region 构造函数
		public PageInfo()
		{			
		}
		
		public PageInfo(int id, string name, string alikeid, DateTime addtime, string seotitle, string seokeywords, string seodescription, byte ishtml, string savepath, string filename, string template, string content)
		{
			this.Id = id;
			this.Name = name;
			this.Alikeid = alikeid;
			this.Addtime = addtime;
			this.Seotitle = seotitle;
			this.Seokeywords = seokeywords;
			this.Seodescription = seodescription;
			this.Ishtml = ishtml;
			this.Savepath = savepath;
			this.Filename = filename;
			this.Template = template;
			this.Content = content;
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

		public string Alikeid
		{
			get { return alikeid; }
			set { alikeid = value.Trim(); }
		}


        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public string Seotitle
		{
			get { return seotitle; }
			set { seotitle = value.Trim(); }
		}		

		public string Seokeywords
		{
			get { return seokeywords; }
			set { seokeywords = value.Trim(); }
		}		

		public string Seodescription
		{
			get { return seodescription; }
			set { seodescription = value.Trim(); }
		}		

		public byte Ishtml
		{
			get { return ishtml; }
			set { ishtml = value; }
		}		

		public string Savepath
		{
			get { return savepath; }
			set { savepath = value.Trim(); }
		}		

		public string Filename
		{
			get { return filename; }
			set { filename = value.Trim(); }
		}		

		public string Template
		{
			get { return template; }
			set { template = value.Trim(); }
		}		

		public string Content
		{
			get { return content; }
			set { content = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public PageInfo Clone()
        {
            return (PageInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
