using System;

namespace STA.Entity.Plus
{
	/// <summary>
	///Variable的实体。
	/// </summary>
	[Serializable]
	public class VariableInfo
	{
		#region 变量定义
		private int id = 0;  
		private string m_name = string.Empty;  
		private string m_likeid = string.Empty;  
		private string m_key = string.Empty;  
		private string m_desc = string.Empty;  
		private string m_kvalue = string.Empty;
        private byte m_system = 0;
		#endregion
		
		#region 字段属性
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

        public byte System
        {
            get { return m_system; }
            set { m_system = value; }
        }

		public string Name
		{
			get { return m_name; }
			set { m_name = value.Trim(); }
		}		

		public string Likeid
		{
			get { return m_likeid; }
			set { m_likeid = value.Trim(); }
		}		

		public string Key
		{
			get { return m_key; }
			set { m_key = value.Trim(); }
		}		

		public string Desc
		{
			get { return m_desc; }
			set { m_desc = value.Trim(); }
		}		

		public string KValue
		{
			get { return m_kvalue; }
			set { m_kvalue = value.Trim(); }
		}		
		#endregion
		
	}
}
