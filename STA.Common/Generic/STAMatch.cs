using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace STA.Common.Generic
{
    public class STAMatch
    {
        public static bool Fetch(string Url,string encode,out string content)
        {
            content = string.Empty;
            if (!Utils.IsURL(Url))
            {
                content = string.Empty;
                return false;
            }
            bool flag = false;
            try
            {
                Uri obj = new Uri(Url);
                content = Utils.GetUrlContent(obj, encode);
                flag = true;
            }
            catch (UriFormatException e)
            {
                throw new Exception(e.Message);
            }
            catch (System.Net.WebException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return flag;
        }

        public static string GetTarget(string input, string pattern, string find)
        {
            string targets = string.Empty;
            Match m = GetMatch(input, pattern, find);
            while (m.Success)
            {
                targets += m.Groups["TARGET"].Value;
                m = m.NextMatch();
            }
            return targets;
        }
        /// <summary>
        /// 获取一个目标的匹配结果
        /// </summary>
        /// <param name="input">要匹配的字符串</param>
        /// <param name="pattern"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static Match GetMatch(string input, string pattern, string find)
        {
            string _pattn = Regex.Escape(pattern);
            _pattn = _pattn.Replace(@"\[变量]", @"[\s\S]*?");
            _pattn = Regex.Replace(_pattn, @"((\\r\\n)|(\\ ))+", @"\s*", RegexOptions.Compiled);
            if (Regex.Match(pattern.TrimEnd(), Regex.Escape(find) + "$", RegexOptions.Compiled).Success)
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+)");
            else
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+?)");
            Regex r = new Regex(_pattn, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = r.Match(input);
            return m;
        }

        public static string FilterScirpt(string html)
        {
            Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            return html;
        }

        public static string FilterFrame(string html)
        {
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            return html;
        }

        public static string FilterObject(string html)
        {
            Regex regex1 = new Regex(@"<object[\s\S]+</object *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); 
            return html;
        }

        public static string FilterStyle(string html)
        {
            Regex regex1 = new Regex(@"<style[\s\S]+</style *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, "");
            return html;
        }

        public static string FilterDiv(string html)
        {
            Regex regex1 = new Regex(@"<div[\s\S]+</div *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, "");
            return html;
        }
    }
}
