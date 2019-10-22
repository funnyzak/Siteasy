using System;

namespace STA.Entity
{
	/// <summary>
	///Staticize的实体。
	/// </summary>
	[Serializable]
	public class StaticizeInfo
	{
		#region 变量定义
		private int id = 0;  
		private string title = string.Empty;  
		private string charset = string.Empty;  
		private string url = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private DateTime maketime = DateTime.Now;  
		private string savepath = string.Empty;  
		private string filename = string.Empty;  
		private string suffix = string.Empty;  
		#endregion
		
		#region 构造函数
		public StaticizeInfo()
		{			
		}
		
		public StaticizeInfo(int id, string title, string charset, string url, DateTime addtime, DateTime maketime, string savepath, string filename, string suffix)
		{
			this.Id = id;
			this.Title = title;
			this.Charset = charset;
			this.Url = url;
			this.Addtime = addtime;
			this.Maketime = maketime;
			this.Savepath = savepath;
			this.Filename = filename;
			this.Suffix = suffix;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public string Title
		{
			get { return title; }
			set { title = value.Trim(); }
		}		

		public string Charset
		{
			get { return charset; }
			set { charset = value.Trim(); }
		}		

		public string Url
		{
			get { return url; }
			set { url = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public DateTime Maketime
		{
			get { return maketime; }
			set { maketime = value; }
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

		public string Suffix
		{
			get { return suffix; }
			set { suffix = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public StaticizeInfo Clone()
        {
            return (StaticizeInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
