using System;

namespace STA.Entity
{
	/// <summary>
	///Diyform的实体。
	/// </summary>
	[Serializable]
	public class DiyformInfo
	{
		#region 变量定义
		private int id = 0;  
		private string name = string.Empty;  
		private string postem = string.Empty;  
		private string viewtem = string.Empty;  
		private string listem = string.Empty;  
		private string tablename = string.Empty;  
		private string fieldset = string.Empty;  
		private string successmsg = string.Empty;  
		private string failmsg = string.Empty;  
		private byte anonymous = 0;  
		private byte ispublic = 0;  
		private byte isvcode = 0;  
		private byte isverify = 0;  
		#endregion
		
		#region 构造函数
		public DiyformInfo()
		{			
		}
		
		public DiyformInfo(int id, string name, string postem, string viewtem, string listem, string tablename, string fieldset, string successmsg, string failmsg, byte anonymous, byte ispublic, byte isvcode, byte isverify)
		{
			this.Id = id;
			this.Name = name;
			this.Postem = postem;
			this.Viewtem = viewtem;
			this.Listem = listem;
			this.Tablename = tablename;
			this.Fieldset = fieldset;
			this.Successmsg = successmsg;
			this.Failmsg = failmsg;
			this.Anonymous = anonymous;
			this.Ispublic = ispublic;
			this.Isvcode = isvcode;
			this.Isverify = isverify;
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

		public string Postem
		{
			get { return postem; }
			set { postem = value.Trim(); }
		}		

		public string Viewtem
		{
			get { return viewtem; }
			set { viewtem = value.Trim(); }
		}		

		public string Listem
		{
			get { return listem; }
			set { listem = value.Trim(); }
		}		

		public string Tablename
		{
			get { return tablename; }
			set { tablename = value.Trim(); }
		}		

		public string Fieldset
		{
			get { return fieldset; }
			set { fieldset = value.Trim(); }
		}		

		public string Successmsg
		{
			get { return successmsg; }
			set { successmsg = value.Trim(); }
		}		

		public string Failmsg
		{
			get { return failmsg; }
			set { failmsg = value.Trim(); }
		}		

		public byte Anonymous
		{
			get { return anonymous; }
			set { anonymous = value; }
		}		

		public byte Ispublic
		{
			get { return ispublic; }
			set { ispublic = value; }
		}		

		public byte Isvcode
		{
			get { return isvcode; }
			set { isvcode = value; }
		}		

		public byte Isverify
		{
			get { return isverify; }
			set { isverify = value; }
		}		
		#endregion
		
		#region 副本
        public DiyformInfo Clone()
        {
            return (DiyformInfo)this.MemberwiseClone();
        }
		#endregion
	}
}
