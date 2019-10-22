using System;
using System.Text;

namespace STA.Config
{
    /// <summary>
    ///  Vote配置类
    /// </summary>
    public class VoteConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static VoteConfigInfo GetConfig()
        {
            return VoteConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="Voteconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(VoteConfigInfo Voteconfiginfo)
        {
            VoteConfigFileManager ecfm = new VoteConfigFileManager();
            VoteConfigFileManager.ConfigInfo = Voteconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
