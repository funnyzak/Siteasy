using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Plugin.Translator
{
    /// <summary>
    /// 语言翻译者接口
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode);

        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        string Translate(string sourceText, string targetLanguageCode);
    }
}
