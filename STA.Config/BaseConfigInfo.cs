using System;

namespace STA.Config
{
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class BaseConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string m_dbconnectstring = ""; //"Data Source=;User ID=sa;Password=123456;Initial Catalog=STA;Pooling=true";	
        private string m_tableprefix = "";		// 数据库中表的前缀
        private string m_sitepath = "";			// 站点路径  /site
        private string m_dbtype = "";
        private int m_founderuid = 0;				// 创始人
        private string m_adminpath = "";          //后台文件夹 /admin
        #endregion

        #region 属性

        public string Dbconnectstring
        {
            get { return m_dbconnectstring; }
            set { m_dbconnectstring = value; }
        }

        public string Adminpath
        {
            get { return m_adminpath; }
            set { m_adminpath = value; }
        }

        /// <summary>
        /// 数据库中表的前缀
        /// </summary>
        public string Tableprefix
        {
            get { return m_tableprefix; }
            set { m_tableprefix = value; }
        }

        /// <summary>
        /// 论坛在站点内的路径
        /// </summary>
        public string Sitepath
        {
            get { return m_sitepath; }
            set { m_sitepath = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string Dbtype
        {
            get { return m_dbtype; }
            set { m_dbtype = value; }
        }

        /// <summary>
        /// 创始人ID
        /// </summary>
        public int Founderuid
        {
            get { return m_founderuid; }
            set { m_founderuid = value; }
        }

        #endregion
    }
}
