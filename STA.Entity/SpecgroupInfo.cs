using System;

namespace STA.Entity
{
	/// <summary>
	///Specgroup的实体。
	/// </summary>
	[Serializable]
	public class SpecgroupInfo
	{
		#region 变量定义
		private int id = 0;  
		private int specid = 0;  
		private string name = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public SpecgroupInfo()
		{			
		}
		
		public SpecgroupInfo(int id, int specid, string name, DateTime addtime, int orderid)
		{
			this.Id = id;
			this.Specid = specid;
			this.Name = name;
			this.Addtime = addtime;
			this.Orderid = orderid;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public int Specid
		{
			get { return specid; }
			set { specid = value; }
		}		

		public string Name
		{
			get { return name; }
			set { name = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public SpecgroupInfo Clone()
        {
            return (SpecgroupInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
