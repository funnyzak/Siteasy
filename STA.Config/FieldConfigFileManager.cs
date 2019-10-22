using System;
using System.Text;
using System.Web;
using System.IO;

using STA.Common;
using System.Xml.Serialization;
using System.Xml;


namespace STA.Config
{
    /// <summary>
    /// 论坛基本设置管理类
    /// </summary>
    class FieldConfigFileManager : STA.Config.DefaultConfigFileManager
    {
        private static FieldConfigInfo m_configinfo;


        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;


        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static FieldConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);

            try
            {
                m_configinfo = (FieldConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(FieldConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    ReviseConfig();
                    m_configinfo = (FieldConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(FieldConfigInfo));
                }
            }
        }



        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (FieldConfigInfo)value; }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = Utils.GetMapPath(BaseConfigs.GetSitePath + BaseConfigs.GetAdminDir + "/xml/field.config");
                }

                return filename;
            }

        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static FieldConfigInfo LoadConfig()
        {

            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ReviseConfig();
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as FieldConfigInfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}

