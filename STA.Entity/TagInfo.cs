using System;

namespace STA.Entity
{
	/// <summary>
	///Tag的实体。
	/// </summary>
	[Serializable]
	public class TagInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private int count = 0;  
		private DateTime addtime = DateTime.Now;  
		#endregion
		
		#region 构造函数
		public TagInfo()
		{			
		}
		
		public TagInfo(int id, string name, int count, DateTime addtime)
		{
			this.Id = id;
			this.Name = name;
			this.Count = count;
			this.Addtime = addtime;
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

		public int Count
		{
			get { return count; }
			set { count = value; }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		
		#endregion
		
		#region 副本
        public TagInfo Clone()
        {
            return (TagInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
