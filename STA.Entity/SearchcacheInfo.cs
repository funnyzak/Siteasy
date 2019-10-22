using System;

namespace STA.Entity
{
	/// <summary>
	///Searchcache的实体。
	/// </summary>
	[Serializable]
	public class SearchcacheInfo
	{
		#region 变量定义
		private int id = 0;  
		private string keywords = string.Empty;  
		private string searchstring = string.Empty;  
		private DateTime searchtime = DateTime.Now;  
		private DateTime expiration = DateTime.Now;  
		private int scount = 0;  
		private int rcount = 0;  
		private string ids = string.Empty;  
		#endregion
		
		#region 构造函数
		public SearchcacheInfo()
		{			
		}
		
		public SearchcacheInfo(int id, string keywords, string searchstring, DateTime searchtime, DateTime expiration, int scount, int rcount, string ids)
		{
			this.Id = id;
			this.Keywords = keywords;
			this.Searchstring = searchstring;
			this.Searchtime = searchtime;
			this.Expiration = expiration;
			this.Scount = scount;
			this.Rcount = rcount;
			this.Ids = ids;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public string Keywords
		{
			get { return keywords; }
			set { keywords = value.Trim(); }
		}		

		public string Searchstring
		{
			get { return searchstring; }
			set { searchstring = value.Trim(); }
		}		

		public DateTime Searchtime
		{
			get { return searchtime; }
			set { searchtime = value; }
		}		

		public DateTime Expiration
		{
			get { return expiration; }
			set { expiration = value; }
		}		

		public int Scount
		{
			get { return scount; }
			set { scount = value; }
		}		

		public int Rcount
		{
			get { return rcount; }
			set { rcount = value; }
		}		

		public string Ids
		{
			get { return ids; }
			set { ids = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public SearchcacheInfo Clone()
        {
            return (SearchcacheInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
