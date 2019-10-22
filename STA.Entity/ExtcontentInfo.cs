using System;

namespace STA.Entity
{
	/// <summary>
	///Extcontent的实体。
	/// </summary>
	[Serializable]
	public class ExtcontentInfo
	{
		#region 变量定义
		private int cid = 0;  
		private short typeid = 0;  
		#endregion
		
		#region 构造函数
		public ExtcontentInfo()
		{			
		}
		
		public ExtcontentInfo(int cid, short typeid)
		{
			this.Cid = cid;
			this.Typeid = typeid;
		}
		#endregion
		
		#region 字段属性
		public int Cid
		{
			get { return cid; }
			set { cid = value; }
		}		

		public short Typeid
		{
			get { return typeid; }
			set { typeid = value; }
		}		
		#endregion
		
		#region 副本
        public ExtcontentInfo Clone()
        {
            return (ExtcontentInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
