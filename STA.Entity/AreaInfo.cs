using System;

namespace STA.Entity
{
	/// <summary>
	///Area的实体。
	/// </summary>
	[Serializable]
	public class AreaInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private int parentid = 0;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public AreaInfo()
		{			
		}
		
		public AreaInfo(int id, string name, int parentid, int orderid)
		{
			this.Id = id;
			this.Name = name;
			this.Parentid = parentid;
			this.Orderid = orderid;
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

		public int Parentid
		{
			get { return parentid; }
			set { parentid = value; }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public AreaInfo Clone()
        {
            return (AreaInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
