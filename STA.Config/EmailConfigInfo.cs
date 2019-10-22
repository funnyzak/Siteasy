using System;

namespace STA.Config
{
	/// <summary>
	/// Email配置信息类
	/// </summary>
	[Serializable]
    public class EmailConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string smtp; //smtp 地址

		private int port = 25; //端口号

		private string sysemail;  //系统邮件地址

		private string username;  //邮件帐号

		private string password;  //邮件密码

		private string pluginNameSpace; //插件名空间

        private string dllFileName;  //插件所在的DLL名称

        private string emailcontent;

        private string nickname; //发件人昵称

        private string subtitle; //订阅邮件标题


        private string subcont; //订阅邮件内容


        #endregion

        public EmailConfigInfo()
		{
        }

        #region 属性

        /// <summary>
		/// smtp服务器
		/// </summary>
		public string Smtp
		{
			get { return smtp;}
			set { smtp = value;}
		}


        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }


        public string Emailcontent
        {
            get { return emailcontent; }
            set { emailcontent = value; }
        }

		/// <summary>
		/// 端口号
		/// </summary>
		public int Port
		{
			get { return port;}
			set { port = value;}
		}
		

		/// <summary>
		/// 系统Email地址
		/// </summary>
		public string Sysemail
		{
			get { return sysemail;}
			set { sysemail = value;}
		}


		/// <summary>
		/// 用户名
		/// </summary>
		public string Username
		{
			get { return username;}
			set { username = value;}
		}

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get { return password;}
			set { password = value;}
		}

		
		/// <summary>
		/// 所运行的插件的名空间
		/// </summary>
		public string PluginNameSpace
		{
			get { return pluginNameSpace;}
			set { pluginNameSpace = value;}
		}


		/// <summary>
        /// 插件名称 *.dll
		/// </summary>
		public string DllFileName
		{
			get { return dllFileName;}
			set { dllFileName = value;}
        }
        #endregion


        #region 邮件订阅
        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }
        public string Subcont
        {
            get { return subcont; }
            set { subcont = value; }
        }
        #endregion
    }
}
