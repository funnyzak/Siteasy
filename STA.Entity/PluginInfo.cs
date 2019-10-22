using System;

namespace STA.Entity
{
	/// <summary>
	///Plugin的实体。
	/// </summary>
	[Serializable]
	public class PluginInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string email = string.Empty;  
		private string author = string.Empty;  
		private DateTime pubtime = DateTime.Now;  
		private string officesite = string.Empty;  
		private string menu = string.Empty;  
		private string description = string.Empty;  
		private string dbcreate = string.Empty;  
		private string dbdelete = string.Empty;  
		private string filelist = string.Empty;  
		private string package = string.Empty;  
		private byte setup = 0;  
		#endregion
		
		#region 构造函数
		public PluginInfo()
		{			
		}
		
		public PluginInfo(int id, string name, string email, string author, DateTime pubtime, string officesite, string menu, string description, string dbcreate, string dbdelete, string filelist, string package, byte setup)
		{
			this.Id = id;
			this.Name = name;
			this.Email = email;
			this.Author = author;
			this.Pubtime = pubtime;
			this.Officesite = officesite;
			this.Menu = menu;
			this.Description = description;
			this.Dbcreate = dbcreate;
			this.Dbdelete = dbdelete;
			this.Filelist = filelist;
			this.Package = package;
			this.Setup = setup;
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

		public string Email
		{
			get { return email; }
			set { email = value.Trim(); }
		}		

		public string Author
		{
			get { return author; }
			set { author = value.Trim(); }
		}		

		public DateTime Pubtime
		{
			get { return pubtime; }
			set { pubtime = value; }
		}		

		public string Officesite
		{
			get { return officesite; }
			set { officesite = value.Trim(); }
		}		

		public string Menu
		{
			get { return menu; }
			set { menu = value.Trim(); }
		}		

		public string Description
		{
			get { return description; }
			set { description = value.Trim(); }
		}		

		public string Dbcreate
		{
			get { return dbcreate; }
			set { dbcreate = value.Trim(); }
		}		

		public string Dbdelete
		{
			get { return dbdelete; }
			set { dbdelete = value.Trim(); }
		}		

		public string Filelist
		{
			get { return filelist; }
			set { filelist = value.Trim(); }
		}		

		public string Package
		{
			get { return package; }
			set { package = value.Trim(); }
		}		

		public byte Setup
		{
			get { return setup; }
			set { setup = value; }
		}		
		#endregion
		
		#region 副本
        public PluginInfo Clone()
        {
            return (PluginInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
