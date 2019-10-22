using System;

namespace STA.Entity
{
	/// <summary>
	///Contag的实体。
	/// </summary>
	[Serializable]
	public class ContagInfo
	{
		#region 变量定义
		private int contentid = 0;  
		private int tagid = 0;  
		#endregion
		
		#region 构造函数
		public ContagInfo()
		{			
		}
		
		public ContagInfo(int contentid, int tagid)
		{
			this.Contentid = contentid;
			this.Tagid = tagid;
		}
		#endregion
		
		#region 字段属性
		public int Contentid
		{
			get { return contentid; }
			set { contentid = value; }
		}		

		public int Tagid
		{
			get { return tagid; }
			set { tagid = value; }
		}		
		#endregion
		
		#region 副本
        public ContagInfo Clone()
        {
            return (ContagInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
