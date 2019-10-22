using System;

namespace STA.Entity
{
	/// <summary>
	///Diyform1的实体。
	/// </summary>
	[Serializable]
	public class Diyform1Info
	{
		#region 变量定义
		private int id = 0;  
		private int uid = 0;  
		private DateTime addtime = DateTime.Now;  
		private DateTime verifytime = DateTime.Now;  
		private string sip = string.Empty;  
		private byte status = 0;  
		private string diycolumn = string.Empty;  
		#endregion
		
		#region 构造函数
		public Diyform1Info()
		{			
		}
		
		public Diyform1Info(int id, int uid, DateTime addtime, DateTime verifytime, string sip, byte status, string diycolumn)
		{
			this.Id = id;
			this.Uid = uid;
			this.Addtime = addtime;
			this.Verifytime = verifytime;
			this.Sip = sip;
			this.Status = status;
			this.Diycolumn = diycolumn;
		}
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}		

		public int Uid
		{
			get { return uid; }
			set { uid = value; }
		}		

		public DateTime Addtime
		{
			get { return addtime; }
			set { addtime = value; }
		}		

		public DateTime Verifytime
		{
			get { return verifytime; }
			set { verifytime = value; }
		}		

		public string Sip
		{
			get { return sip; }
			set { sip = value.Trim(); }
		}		

		public byte Status
		{
			get { return status; }
			set { status = value; }
		}		

		public string Diycolumn
		{
			get { return diycolumn; }
			set { diycolumn = value.Trim(); }
		}		
		#endregion
		
		#region 副本
        public Diyform1Info Clone()
        {
            return (Diyform1Info)this.MemberwiseClone();
        }
		#endregion
	}
}
