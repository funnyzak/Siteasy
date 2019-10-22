using System;

namespace STA.Entity
{
	/// <summary>
	///Actioncolumn的实体。
	/// </summary>
	[Serializable]
	public class ActioncolumnInfo
	{
		#region 变量定义
		private int id = 0;  
		private string columnname = string.Empty;  
		private byte status = 0;  
		#endregion
		
		#region 构造函数
		public ActioncolumnInfo()
		{			
		}
		
		public ActioncolumnInfo(int id, string columnname, byte status)
		{
			this.Id = id;
			this.Columnname = columnname;
			this.Status = status;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public string Columnname
		{
			get { return columnname; }
			set { columnname = value.Trim(); }
		}		

		public byte Status
		{
			get { return status; }
			set { status = value; }
		}		
		#endregion
		
		#region 副本
        public ActioncolumnInfo Clone()
        {
            return (ActioncolumnInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
