using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using STA.Common;

namespace STA.Config
{
	/// <summary>
	/// 论坛基本设置类
	/// </summary>
    public class FieldConfigs
	{
		private static object lockHelper = new object();

        private static System.Timers.Timer FieldConfigTimer = new System.Timers.Timer(15000);

        private static FieldConfigInfo m_configinfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static FieldConfigs()
        {
            m_configinfo = FieldConfigFileManager.LoadConfig();

            FieldConfigTimer.AutoReset = true;
            FieldConfigTimer.Enabled = true;
            FieldConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            FieldConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }


        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = FieldConfigFileManager.LoadConfig();
        }

		public static FieldConfigInfo GetConfig()
		{
            return m_configinfo;
  		}

        public static int MaxFavouriteMenuCount
        {
            get
            {
                return GetConfig().MaxFavouriteMenuCount;
            }
        }

        public static string ContentAuthor
        {
            get
            {
                return GetConfig().ContentAuthor;
            }
        }

        public static string ContentSource
        {
            get
            {
                return GetConfig().ContentSource;
            }
        }

        public static int PopWinOverlay
        {
            get
            {
                return GetConfig().PopWinOverlay;
            }
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="FieldConfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(FieldConfigInfo FieldConfiginfo)
        {
            FieldConfigFileManager gcf = new FieldConfigFileManager();
            FieldConfigFileManager.ConfigInfo = FieldConfiginfo;
            return gcf.SaveConfig();
        }


		#region Helper

		/// <summary>
		/// 序列化配置信息为XML
		/// </summary>
		/// <param name="configinfo">配置信息</param>
		/// <param name="configFilePath">配置文件完整路径</param>
		public static FieldConfigInfo Serialiaze(FieldConfigInfo configinfo, string configFilePath)
		{
			lock(lockHelper) 
			{
				SerializationHelper.Save(configinfo, configFilePath);
			}
			return configinfo;
		}


		public static FieldConfigInfo Deserialize(string configFilePath)
		{
			return (FieldConfigInfo)SerializationHelper.Load(typeof(FieldConfigInfo), configFilePath);
		}

		#endregion



	}
}
