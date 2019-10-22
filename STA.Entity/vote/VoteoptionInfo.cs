using System;

namespace STA.Entity
{
	/// <summary>
	///Voteoption的实体。
	/// </summary>
	[Serializable]
	public class VoteoptionInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string desc = string.Empty;  
		private int topicid = 0;  
		private string topicname = string.Empty;  
		private string img = string.Empty;  
		private int count = 0;  
		private int orderid = 0;  
		#endregion
		
		#region 构造函数
		public VoteoptionInfo()
		{			
		}
		
		public VoteoptionInfo(int id, string name, string desc, int topicid, string topicname, string img, int count, int orderid)
		{
			this.Id = id;
			this.Name = name;
			this.Desc = desc;
			this.Topicid = topicid;
			this.Topicname = topicname;
			this.Img = img;
			this.Count = count;
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

		public string Desc
		{
			get { return desc; }
			set { desc = value.Trim(); }
		}		

		public int Topicid
		{
			get { return topicid; }
			set { topicid = value; }
		}		

		public string Topicname
		{
			get { return topicname; }
			set { topicname = value.Trim(); }
		}		

		public string Img
		{
			get { return img; }
			set { img = value.Trim(); }
		}		

		public int Count
		{
			get { return count; }
			set { count = value; }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public VoteoptionInfo Clone()
        {
            return (VoteoptionInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
