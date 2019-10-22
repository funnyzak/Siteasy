using System;

namespace STA.Entity
{
	/// <summary>
	///Dbcollect的实体。
	/// </summary>
	[Serializable]
	public class DbcollectInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private int channelid = 0;  
		private string channelname = string.Empty;  
		private byte constatus = 0;  
		private byte dbtype = 0;  
		private DateTime addtime = DateTime.Now;  
		private string datasource = string.Empty;  
		private string userid = string.Empty;  
		private string password = string.Empty;  
		private string dbname = string.Empty;  
		private string tbname = string.Empty;  
		private string primarykey = string.Empty;  
		private string orderkey = string.Empty;  
		private string repeatkey = string.Empty;  
		private string sortby = string.Empty;  
		private string where = string.Empty;  
		private string matchs = string.Empty;  
		#endregion
		
		#region 构造函数
		public DbcollectInfo()
		{			
		}
		
		public DbcollectInfo(int id, string name, int channelid, string channelname, byte constatus, byte dbtype, DateTime addtime, string datasource, string userid, string password, string dbname, string tbname, string primarykey, string orderkey, string repeatkey, string sortby, string where, string matchs)
		{
			this.Id = id;
			this.Name = name;
			this.Channelid = channelid;
			this.Channelname = channelname;
			this.Constatus = constatus;
			this.Dbtype = dbtype;
			this.Addtime = addtime;
			this.Datasource = datasource;
			this.Userid = userid;
			this.Password = password;
			this.Dbname = dbname;
			this.Tbname = tbname;
			this.Primarykey = primarykey;
			this.Orderkey = orderkey;
			this.Repeatkey = repeatkey;
			this.Sortby = sortby;
			this.Where = where;
			this.Matchs = matchs;
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

		public int Channelid
		{
			get { return channelid; }
			set { channelid = value; }
		}		

		public string Channelname
		{
			get { return channelname; }
			set { channelname = value.Trim(); }
		}		

		public byte Constatus
		{
			get { return constatus; }
			set { constatus = value; }
		}		

		public byte Dbtype
		{
			get { return dbtype; }
			set { dbtype = value; }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public string Datasource
		{
			get { return datasource; }
			set { datasource = value.Trim(); }
		}		

		public string Userid
		{
			get { return userid; }
			set { userid = value.Trim(); }
		}		

		public string Password
		{
			get { return password; }
			set { password = value.Trim(); }
		}		

		public string Dbname
		{
			get { return dbname; }
			set { dbname = value.Trim(); }
		}		

		public string Tbname
		{
			get { return tbname; }
			set { tbname = value.Trim(); }
		}		

		public string Primarykey
		{
			get { return primarykey; }
			set { primarykey = value.Trim(); }
		}		

		public string Orderkey
		{
			get { return orderkey; }
			set { orderkey = value.Trim(); }
		}		

		public string Repeatkey
		{
			get { return repeatkey; }
			set { repeatkey = value.Trim(); }
		}		

		public string Sortby
		{
			get { return sortby; }
			set { sortby = value.Trim(); }
		}		

		public string Where
		{
			get { return where; }
			set { where = value.Trim(); }
		}		

		public string Matchs
		{
			get { return matchs; }
			set { matchs = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public DbcollectInfo Clone()
        {
            return (DbcollectInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
