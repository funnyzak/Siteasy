using System;
using System.Reflection;
using System.Text;
using System.Data;
using System.Threading;

using STA.Common;
using STA.Data;
using STA.Plugin.Translator;
using STA.Config;
using STA.Entity;

namespace STA.Core
{
    /// <summary>
    /// 语言翻译的调用封装类
    /// </summary>
    public class Translators
    {
        protected static GeneralConfigInfo configinfo = GeneralConfigs.GetConfig();

        protected static ITranslator tran;


        static Translators()
        {
            if (configinfo.TranDllFileName.ToLower().IndexOf(".dll") <= 0)
                configinfo.TranDllFileName = configinfo.TranDllFileName + ".dll";
            LoadTranslatorPlugin();
        }

        //重设置当前翻译的实例对象
        public static void ReSetITranslator()
        {
            configinfo = GeneralConfigs.GetConfig();
            LoadTranslatorPlugin();
        }

        /// <summary>
        /// 加载Translator插件
        /// </summary>
        private static void LoadTranslatorPlugin()
        {
            try
            {
                //读取相应的DLL信息
                Assembly asm = Assembly.LoadFrom(System.Web.HttpRuntime.BinDirectory + configinfo.TranDllFileName);
                tran = (ITranslator)Activator.CreateInstance(asm.GetType(configinfo.TranPluginNameSpace, false, true));
            }
            catch
            {
                try
                {
                    //读取相应的DLL信息
                    Assembly asm = Assembly.LoadFrom(Utils.GetMapPath("/bin/" + configinfo.TranDllFileName));
                    tran = (ITranslator)Activator.CreateInstance(asm.GetType(configinfo.TranPluginNameSpace, false, true));
                }
                catch
                {
                    tran = new GoogleTranslator();
                }
            }
        }

        public static string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {
            return Translate(sourceText, sourceLanguageCode, targetLanguageCode, sourceText);
        }

        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <param name="defText">如果未翻译成功返回的默认文本</param>
        /// <returns>翻译结果</returns>
        public static string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode, string defText)
        {
            string tranrlt = tran.Translate(sourceText, sourceLanguageCode, targetLanguageCode);
            return tranrlt == "" ? defText : tranrlt;
        }

        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        public static string Translate(string sourceText, string targetLanguageCode)
        {
            string tranrlt = tran.Translate(sourceText, targetLanguageCode);
            return tranrlt == "" ? sourceText : tranrlt;
        }
    }
}
