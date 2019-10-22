using System;

namespace STA.Entity
{
	/// <summary>
	///Channel的实体。
	/// </summary>
	[Serializable]
	public class ChannelInfo
	{
		#region 变量定义
		private int id = 0;  
		private short typeid = 0;  
		private int parentid = 0;  
		private string name = string.Empty;  
		private string savepath = string.Empty;  
		private string filename = string.Empty;  
		private byte ctype = 0;
        private string img = string.Empty;
		private DateTime addtime = DateTime.Now;  
		private string covertem = string.Empty;  
		private string listem = string.Empty;  
		private string contem = string.Empty;  
		private string conrule = string.Empty;  
		private string listrule = string.Empty;  
		private string seotitle = string.Empty;  
		private string seokeywords = string.Empty;  
		private string seodescription = string.Empty;  
		private byte moresite = 0;  
		private string siteurl = string.Empty;  
		private string content = string.Empty;  
		private byte ispost = 0;  
		private byte ishidden = 0;  
		private int orderid = 0;
        private int listcount = 0;
		private string viewgroup = string.Empty;
        private string viewcongroup = string.Empty;
        private string ipaccess = "";
        private string ipdenyaccess = "";

		#endregion
		
		#region 构造函数
		public ChannelInfo()
		{			
		}
		
		public ChannelInfo(int id, short typeid, int parentid, string name, string savepath, string filename, byte ctype, DateTime addtime, string covertem, string listem, string contem, string conrule, string listrule, string seotitle, string seokeywords, string seodescription, byte moresite, string siteurl, string content, byte ispost, byte ishidden, int orderid, string viewgroup)
		{
			this.Id = id;
			this.Typeid = typeid;
			this.Parentid = parentid;
			this.Name = name;
			this.Savepath = savepath;
			this.Filename = filename;
			this.Ctype = ctype;
			this.Addtime = addtime;
			this.Covertem = covertem;
			this.Listem = listem;
			this.Contem = contem;
			this.Conrule = conrule;
			this.Listrule = listrule;
			this.Seotitle = seotitle;
			this.Seokeywords = seokeywords;
			this.Seodescription = seodescription;
			this.Moresite = moresite;
			this.Siteurl = siteurl;
			this.Content = content;
			this.Ispost = ispost;
			this.Ishidden = ishidden;
			this.Orderid = orderid;
			this.Viewgroup = viewgroup;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
        public string Ipaccess
        {
            get { return ipaccess; }
            set { ipaccess = value; }
        }
        public string Ipdenyaccess
        {
            get { return ipdenyaccess; }
            set { ipdenyaccess = value; }
        }
        public int Listcount
        {
            get { return listcount; }
            set { listcount = value; }
        }

        public string Img
        {
            get { return img; }
            set { img = value; }
        }
        public string Viewcongroup
        {
            get { return viewcongroup; }
            set { viewcongroup = value; }
        }

		public short Typeid
		{
			get { return typeid; }
			set { typeid = value; }
		}		

		public int Parentid
		{
			get { return parentid; }
			set { parentid = value; }
		}		

		public string Name
		{
			get { return name; }
			set { name = value.Trim(); }
		}		

		public string Savepath
		{
			get { return savepath; }
			set { savepath = value.Trim(); }
		}		

		public string Filename
		{
			get { return filename; }
			set { filename = value.Trim(); }
		}		

		public byte Ctype
		{
			get { return ctype; }
			set { ctype = value; }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public string Covertem
		{
			get { return covertem; }
			set { covertem = value.Trim(); }
		}		

		public string Listem
		{
			get { return listem; }
			set { listem = value.Trim(); }
		}		

		public string Contem
		{
			get { return contem; }
			set { contem = value.Trim(); }
		}		

		public string Conrule
		{
			get { return conrule; }
			set { conrule = value.Trim(); }
		}		

		public string Listrule
		{
			get { return listrule; }
			set { listrule = value.Trim(); }
		}		

		public string Seotitle
		{
			get { return seotitle; }
			set { seotitle = value.Trim(); }
		}		

		public string Seokeywords
		{
			get { return seokeywords; }
			set { seokeywords = value.Trim(); }
		}		

		public string Seodescription
		{
			get { return seodescription; }
			set { seodescription = value.Trim(); }
		}		

		public byte Moresite
		{
			get { return moresite; }
			set { moresite = value; }
		}		

		public string Siteurl
		{
			get { return siteurl; }
			set { siteurl = value.Trim(); }
		}		

		public string Content
		{
			get { return content; }
			set { content = value.Trim(); }
		}		

		public byte Ispost
		{
			get { return ispost; }
			set { ispost = value; }
		}		

		public byte Ishidden
		{
			get { return ishidden; }
			set { ishidden = value; }
		}		

		public int Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}		

		public string Viewgroup
		{
			get { return viewgroup; }
			set { viewgroup = value; }
		}		
		#endregion
		
		#region 副本
        public ChannelInfo Clone()
        {
            return (ChannelInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
