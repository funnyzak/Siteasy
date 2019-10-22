using System;

namespace STA.Entity
{
	/// <summary>
	///Word的实体。
	/// </summary>
	[Serializable]
	public class WordInfo
	{
		#region 变量定义
		private int id = 0;  
		private int uid = 0;  
		private string username = string.Empty;  
		private string find = string.Empty;  
		private string replacement = string.Empty;  
		#endregion
		
		#region 构造函数
		public WordInfo()
		{			
		}
		
		public WordInfo(int id, int uid, string username, string find, string replacement)
		{
			this.Id = id;
			this.Uid = uid;
			this.Username = username;
			this.Find = find;
			this.Replacement = replacement;
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

		public string Username
		{
			get { return username; }
			set { username = value.Trim(); }
		}		

		public string Find
		{
			get { return find; }
			set { find = value.Trim(); }
		}		

		public string Replacement
		{
			get { return replacement; }
			set { replacement = value.Trim(); }
		}		
		#endregion
		
	}
}
