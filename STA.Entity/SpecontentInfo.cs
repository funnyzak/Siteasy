using System;

namespace STA.Entity
{
	/// <summary>
	///Specontent的实体。
	/// </summary>
	[Serializable]
	public class SpecontentInfo
	{
		#region 变量定义
		private int specid = 0;  
		private int groupid = 0;  
		private int contentid = 0;  
		#endregion
		
		#region 构造函数
		public SpecontentInfo()
		{			
		}
		
		public SpecontentInfo(int specid, int groupid, int contentid)
		{
			this.Specid = specid;
			this.Groupid = groupid;
			this.Contentid = contentid;
		}
		#endregion
		
		#region 字段属性
		public int Specid
		{
			get { return specid; }
			set { specid = value; }
		}		

		public int Groupid
		{
			get { return groupid; }
			set { groupid = value; }
		}		

		public int Contentid
		{
			get { return contentid; }
			set { contentid = value; }
		}		
		#endregion
		
		#region 副本
        public SpecontentInfo Clone()
        {
            return (SpecontentInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
