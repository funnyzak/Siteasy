using System;

namespace STA.Entity
{
	/// <summary>
	///Votecate的实体。
	/// </summary>
	[Serializable]
	public class VotecateInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string ename = string.Empty;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public VotecateInfo()
		{			
		}
		
		public VotecateInfo(int id, string name, string ename, int orderid)
		{
			this.Id = id;
			this.Name = name;
			this.Ename = ename;
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

		public string Ename
		{
			get { return ename; }
			set { ename = value.Trim(); }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public VotecateInfo Clone()
        {
            return (VotecateInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
