using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;

namespace STA.Plugin.Translator
{
    [Translator("Google翻译", Version = "1.0", Author = "funnyzak ", DllFileName = "STA.Plugin.dll")]
    /// <summary>
    /// google translate翻译者类[非API，URL访问Google的方式]
    /// </summary>
    public class GoogleTranslator : ITranslator
    {

        //private string UrlTemplate = "http://translate.google.com.hk/?langpair={0}&text={1}";    //google翻译URL模板:GET方式请求
        private string UrlTemplate = "http://translate.google.com.hk/";                            //google翻译URL模板:POST方式请求

        #region 常用语言编码
        private string AutoDetectLanguage = "auto"; //auto google自动判断来源语系
        #endregion

        /// <summary>
        /// 翻译文本[自动检测源语言类型]
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        public string Translate(string sourceText, string targetLanguageCode)
        {
            return Translate(sourceText, AutoDetectLanguage, targetLanguageCode);
        }

        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="sourceLanguageCode">源语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <param name="targetLanguageCode">目标语言类型代码，如：en、zh-CN、zh-TW、ru等</param>
        /// <returns>翻译结果</returns>
        public string Translate(string sourceText, string sourceLanguageCode, string targetLanguageCode)
        {
            if (string.IsNullOrEmpty(sourceText) || Regex.IsMatch(sourceText, @"^\s*$"))
            {
                return sourceText;
            }

            string strReturn = string.Empty;

            #region POST方式实现，无长度限制
            string url = UrlTemplate;

            //组织请求的数据
            string postData = string.Format("langpair={0}&text={1}", HttpUtility.UrlEncode(sourceLanguageCode + "|" + targetLanguageCode), HttpUtility.UrlEncode(sourceText));
            byte[] bytes = Encoding.Default.GetBytes(postData);

            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("ContentLength", postData.Length.ToString());

            byte[] responseData = client.UploadData(url, "POST", bytes);
            string strResult = Encoding.Default.GetString(responseData);    //响应结果 
            #endregion

            #region GET方式实现，有长度限制
            //string url = string.Format(UrlTemplate, HttpUtility.UrlEncode(sourceLanguageCode + "|" + targetLanguageCode), HttpUtility.UrlEncode(sourceText));
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //string strResult = wc.DownloadString(url);                //响应结果            
            #endregion

            string strReg = @"onmouseover=""this.style.backgroundColor='#ebeff9'"" onmouseout=""this.style.backgroundColor='#fff'"">(.*)</span></span></div></div><div id=gt-res-tools>";
            Match match = Regex.Match(strResult, strReg, RegexOptions.None);

            if (match.Success)
            {
                strReturn = match.Groups[1].Value;
                strReturn = strReturn.Replace("<br>", "\r\n");
                strReturn = Regex.Replace(strReturn, @"</span><span title=""[^""]*"" onmouseover=""this.style.backgroundColor='#ebeff9'"" onmouseout=""this.style.backgroundColor='#fff'"">", "", RegexOptions.None);
                //对html 结束标签处理 如< /Div>
                strReturn = Regex.Replace(strReturn, @"&lt;\s*\/\s*([a-zA-Z]+)&gt;", "</$1>", RegexOptions.None);

                strReturn = HttpUtility.HtmlDecode(strReturn);
            }
            return strReturn;
        }
    }
}
