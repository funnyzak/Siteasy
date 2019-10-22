using System;

namespace STA.Entity
{
	/// <summary>
	///Action的实体。
	/// </summary>
	[Serializable]
	public class ActionInfo
	{
		#region 变量定义
		private int id = 0;  
		private string actionname = string.Empty;  
		private int columnid = 0;  
		private string action = string.Empty;  
		private byte status = 0;  
		#endregion
		
		#region 构造函数
		public ActionInfo()
		{			
		}
		
		public ActionInfo(int id, string actionname, int columnid, string action, byte status)
		{
			this.Id = id;
			this.Actionname = actionname;
			this.Columnid = columnid;
			this.Action = action;
			this.Status = status;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public string Actionname
		{
			get { return actionname; }
			set { actionname = value.Trim(); }
		}		

		public int Columnid
		{
			get { return columnid; }
			set { columnid = value; }
		}		

		public string Action
		{
			get { return action; }
			set { action = value.Trim(); }
		}		

		public byte Status
		{
			get { return status; }
			set { status = value; }
		}		
		#endregion
		
		#region 副本
        public ActionInfo Clone()
        {
            return (ActionInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
