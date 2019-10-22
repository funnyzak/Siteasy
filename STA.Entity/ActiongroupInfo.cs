using System;

namespace STA.Entity
{
	/// <summary>
	///Actiongroup的实体。
	/// </summary>
	[Serializable]
	public class ActiongroupInfo
	{
		#region 变量定义
		private int id = 0;  
		private int gid = 0;  
		private int action = 0;  
		private string adminname = string.Empty;  
		private DateTime addtime = DateTime.Now;  
		#endregion
		
		#region 构造函数
		public ActiongroupInfo()
		{			
		}
		
		public ActiongroupInfo(int id, int gid, int action, string adminname, DateTime addtime)
		{
			this.Id = id;
			this.Gid = gid;
			this.Action = action;
			this.Adminname = adminname;
			this.Addtime = addtime;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public int Gid
		{
			get { return gid; }
			set { gid = value; }
		}		

		public int Action
		{
			get { return action; }
			set { action = value; }
		}		

		public string Adminname
		{
			get { return adminname; }
			set { adminname = value.Trim(); }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		
		#endregion
		
		#region 副本
        public ActiongroupInfo Clone()
        {
            return (ActiongroupInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
