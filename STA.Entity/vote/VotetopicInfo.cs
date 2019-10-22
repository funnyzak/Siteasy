using System;

namespace STA.Entity
{
	/// <summary>
	///Votetopic的实体。
	/// </summary>
	[Serializable]
	public class VotetopicInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string desc = string.Empty;  
		private int cateid = 0;  
		private string catename = string.Empty;  
		private byte type = 0;  
		private string img = string.Empty;  
		private string likeid = string.Empty;  
		private int maxvote = 0;  
		private DateTime endtime = DateTime.Now;  
		private DateTime addtime = DateTime.Now;
        private string endtext = "";
        private string voted = "";
		private int votecount = 0;  
		private int orderid = 0;
        private int islogin = 2;

        public int Islogin
        {
            get { return islogin; }
            set { islogin = value; }
        }
        private int isinfo = 2;

        public int Isinfo
        {
            get { return isinfo; }
            set { isinfo = value; }
        }
        private int isvcode = 2;

        public int Isvcode
        {
            get { return isvcode; }
            set { isvcode = value; }
        }
		#endregion
		
		#region 构造函数
		public VotetopicInfo()
		{			
		}
		
		public VotetopicInfo(int id, string name, string desc, int cateid, string catename, byte type, string img, string likeid, int maxvote, DateTime endtime, DateTime addtime, int votecount, int orderid)
		{
			this.Id = id;
			this.Name = name;
			this.Desc = desc;
			this.Cateid = cateid;
			this.Catename = catename;
			this.Type = type;
			this.Img = img;
			this.Likeid = likeid;
			this.Maxvote = maxvote;
			this.Endtime = endtime;
			this.Addtime = addtime;
			this.Votecount = votecount;
			this.Orderid = orderid;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        public string Endtext
        {
            get { return endtext; }
            set { endtext = value; }
        }

        public string Voted
        {
            get { return voted; }
            set { voted = value; }
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

		public int Cateid
		{
			get { return cateid; }
			set { cateid = value; }
		}		

		public string Catename
		{
			get { return catename; }
			set { catename = value.Trim(); }
		}		

		public byte Type
		{
			get { return type; }
			set { type = value; }
		}		

		public string Img
		{
			get { return img; }
			set { img = value.Trim(); }
		}		

		public string Likeid
		{
			get { return likeid; }
			set { likeid = value.Trim(); }
		}		

		public int Maxvote
		{
			get { return maxvote; }
			set { maxvote = value; }
		}		

		public DateTime Endtime
		{
			get { return endtime; }
			set { endtime = value; }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public int Votecount
		{
			get { return votecount; }
			set { votecount = value; }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		
		#endregion
		
		#region 副本
        public VotetopicInfo Clone()
        {
            return (VotetopicInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
