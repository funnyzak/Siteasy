using System;

namespace STA.Entity
{
	/// <summary>
	///Selecttype的实体。
	/// </summary>
	[Serializable]
	public class SelecttypeInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string ename = string.Empty;  
		private byte issign = 0;  
		private byte system = 0;  
		#endregion
		
		#region 构造函数
		public SelecttypeInfo()
		{			
		}
		
		public SelecttypeInfo(int id, string name, string ename, byte issign, byte system)
		{
			this.Id = id;
			this.Name = name;
			this.Ename = ename;
			this.Issign = issign;
			this.System = system;
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

		public byte Issign
		{
			get { return issign; }
			set { issign = value; }
		}		

		public byte System
		{
			get { return system; }
			set { system = value; }
		}		
		#endregion
		
		#region 副本
        public SelecttypeInfo Clone()
        {
            return (SelecttypeInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
