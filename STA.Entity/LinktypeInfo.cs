using System;

namespace STA.Entity
{
	/// <summary>
	///Linktype的实体。
	/// </summary>
	[Serializable]
	public class LinktypeInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public LinktypeInfo()
		{			
		}
		
		public LinktypeInfo(int id, string name, int orderid)
		{
			this.Id = id;
			this.Name = name;
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

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public LinktypeInfo Clone()
        {
            return (LinktypeInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
