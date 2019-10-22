using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Cache;

namespace STA.Core
{
    public class UBB
    {
        private static RegexOptions options = RegexOptions.IgnoreCase;
        private static string IMG_SIGN_SIGNATURE = "<img src=\"$1\" border=\"0\" />";
        private static string IMG_SIGN = "<img src=\"$1\" border=\"0\" onload=\"thumbImg(this)\" />";

        /// <summary>
        /// 处理img标记
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        private static string ParseImg(string sDetail, int Signature)
        {
            if (Signature == 1)
            {
                sDetail = Regex.Replace(sDetail, @"\[img\]\s*([^\[\<\r\n]+?)\s*\[\/img\]", IMG_SIGN_SIGNATURE, options);
                //sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", IMG_SIGN_SIGNATURE, options);
            }
            else
            {
                sDetail = Regex.Replace(sDetail, @"\[img\]\s*([^\[\<\r\n]+?)\s*\[\/img\]", IMG_SIGN, options);

                //sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", IMG_SIGN, options);
            }

            sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*([^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" onload=\"thumbImg(this)\" />", options).Replace("width=\"0\"", "").Replace("height=\"0\"", "");

            //sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" onload=\"thumbImg(this)\" />", options);
            sDetail = Regex.Replace(sDetail, @"\[image\]([\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);

            //sDetail = Regex.Replace(sDetail, @"\[image\](http://[\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);
            return sDetail;
        }

        /// <summary>
        /// 处理B标记
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        private static string parseBold(string sDetail)
        {
            sDetail = Regex.Replace(sDetail, @"\[b(?:\s*)\]", "<b>", options);
            sDetail = Regex.Replace(sDetail, @"\[i(?:\s*)\]", "<i>", options);
            sDetail = Regex.Replace(sDetail, @"\[u(?:\s*)\]", "<u>", options);
            sDetail = Regex.Replace(sDetail, @"\[/b(?:\s*)\]", "</b>", options);
            sDetail = Regex.Replace(sDetail, @"\[/i(?:\s*)\]", "</i>", options);
            sDetail = Regex.Replace(sDetail, @"\[/u(?:\s*)\]", "</u>", options);
            return sDetail;
        }

        /// <summary>
        /// 处理Font标记
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        private static string parseFont(string sDetail)
        {
            sDetail = Regex.Replace(sDetail, @"\[color=([^\[\<]+?)\]", "<font color=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[size=(\d+?)\]", "<font size=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[size=(\d+(\.\d+)?(px|pt|in|cm|mm|pc|em|ex|%)+?)\]", "<font style=\"font-size: $1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[font=([^\[\<]+?)\]", "<font face=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[align=([^\[\<]+?)\]", "<p align=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[float=(left|right)\]", "<br style=\"clear: both\"><span style=\"float: $1;\">", options);
            sDetail = Regex.Replace(sDetail, @"\[/color(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/size(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/font(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/align(?:\s*)\]", "</p>", options);
            sDetail = Regex.Replace(sDetail, @"\[/float(?:\s*)\]", "</span>", options);
            return sDetail;
        }

        /// <summary>
        /// 处理URL标记
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string ParseUrl(string sDetail)
        {
            sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\](www\.(.*?))\[/url(?:\s*)\]", "<a href=\"http://$1\" target=\"_blank\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\]\s*(([^\[""']+?))\s*\[\/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$1</a>", options);

            //sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\]\s*((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent)([^\[""']+?))\s*\[\/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url=www.([^\[""']+?)(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\">$2</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url=(([^\[""']+?))(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$3</a>", options);

            //sDetail = Regex.Replace(sDetail, @"\[url=((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent://)([^\[""']+?))(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$4</a>", options);
            return sDetail;
        }

        /// <summary>
        /// 替换UBB方法
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string ParseSimpleUBB(string sDetail)
        {
            sDetail = ParseImg(sDetail, 0);
            sDetail = parseFont(sDetail);
            sDetail = parseBold(sDetail);
            sDetail = ParseUrl(sDetail);
            return sDetail;
        }

        public static string ParseMedia(string type, int width, int height, bool autostart, string url)
        {
            //if (!Utils.InArray(type, "ra,rm,wma,wmv,mp3,mov"))
            //    return "";
            string flv = ParseFlv(url, width, height);
            if (flv != string.Empty)
                return flv;
            url = url.Replace("\\\\", "\\").Replace("<", string.Empty).Replace(">", string.Empty);
            switch (type)
            {
                case "mp3":
                case "wma":
                case "ra":
                case "ram":
                case "wav":
                case "mid":
                    return ParseAudio(autostart ? "1" : "0", url);
                case "rm":
                case "rmvb":
                case "rtsp":
                    Random r = new Random(3);
                    string mediaid = "media_" + r.Next();
                    return string.Format(@"<object classid=""clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"" width=""{0}"" height=""{1}""><param name=""autostart"" value=""{2}"" /><param name=""src"" value=""{3}"" /><param name=""controls"" value=""imagewindow"" /><param name=""console"" value=""{4}_"" /><embed src=""{3}"" type=""audio/x-pn-realaudio-plugin"" controls=""IMAGEWINDOW"" console=""{4}_"" width=""{0}"" height=""{1}""></embed></object><br /><object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""{0}"" height=""32""><param name=""src"" value=""{3}"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""{4}_"" /><embed src=""{3}"" type=""audio/x-pn-realaudio-plugin"" controls=""ControlPanel"" {5} console=""{4}_"" width=""{0}"" height=""32""></embed></object>", width, height, autostart ? 1 : 0, url, mediaid, autostart ? "autostart=\"true\"" : string.Empty);
                case "flv":
                    return string.Format(@"<script type=""text/javascript"" reload=""1"">document.write(AC_FL_RunContent('width', '{0}', 'height', '{1}', 'allowNetworking', 'internal', 'allowScriptAccess', 'never', 'src', '{2}/sta/swf/flvplayer.swf', 'flashvars', 'file={3}', 'quality', 'high', 'wmode', 'transparent', 'allowfullscreen', 'true'));</script>", width, height, BaseConfigs.GetSitePath, Utils.UrlEncode(url));
                case "swf":
                    return string.Format(@"<script type=""text/javascript"" reload=""1"">document.write(AC_FL_RunContent('width', '{0}', 'height', '{1}', 'allowNetworking', 'internal', 'allowScriptAccess', 'never', 'src', '{2}', 'quality', 'high', 'bgcolor', '#ffffff', 'wmode', 'transparent', 'allowfullscreen', 'true'));</script>", width, height, url);
                case "asf":
                case "asx":
                case "wmv":
                case "mms":
                case "avi":
                case "mpg":
                case "mpeg":
                    return string.Format(@"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""{0}"" height=""{1}""><param name=""invokeURLs"" value=""0""><param name=""autostart"" value=""{2}"" /><param name=""url"" value=""{3}"" /><embed src=""{3}"" autostart=""{2}"" type=""application/x-mplayer2"" width=""{0}"" height=""{1}""></embed></object>", width, height, autostart, url);
                case "mov":
                    return string.Format(@"<object classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" width=""{0}"" height=""{1}""><param name=""autostart"" value=""{2}"" /><param name=""src"" value=""{3}"" /><embed src=""{3}"" autostart=""{2}"" type=""video/quicktime"" controller=""true"" width=""{0}"" height=""{1}""></embed></object>", width, height, autostart ? "" : "fale", url);
                default:
                    return string.Format(@"<a href=""{0}"" target=""_blank"">{0}</a>", url);
            }
        }

        private static string ReplaceString(string[] r, string t, string s)
        {
            foreach (string r1 in r)
            {
                s.Replace(r1, t);
            }
            return s;
        }
        private static string ParseFlv(string url, int width, int height)
        {
            string lowerUrl = url.ToLower();
            string flv = "";
            if (lowerUrl != ReplaceString(new string[] { "player.youku.com/player.php/sid/", "tudou.com/v/", "player.ku6.com/refer/" }, "", lowerUrl))
            {
                flv = url;
            }
            else if (lowerUrl.Contains("v.youku.com/v_show/"))
                flv = GetFlvUrl(url, @"http:\/\/v.youku.com\/v_show\/id_([^\/]+)(.html|)", "http://player.youku.com/player.php/sid/{0}/v.swf");
            else if (lowerUrl.Contains("tudou.com/programs/view/"))
                flv = GetFlvUrl(url, @"http:\/\/(www.)?tudou.com\/programs\/view\/([^\/]+)", "http://www.tudou.com/v/{0}", 2);
            else if (lowerUrl.Contains("tudou.com/listplay"))
                flv = GetFlvUrl(url, @"http:\/\/(www.)?tudou.com\/listplay\/([^\/]+)\/([^\/]+)(.html|)", "http://www.tudou.com/v/{0}", 3);
            else if (lowerUrl.Contains("tudou.com/albumplay"))
                flv = GetFlvUrl(url, @"http:\/\/(www.)?tudou.com\/albumplay\/([^\/]+)\/([^\/]+)(.html|)", "http://www.tudou.com/v/{0}", 3);
            else if (lowerUrl.Contains("v.ku6.com/show/"))
                flv = GetFlvUrl(url, @"http:\/\/v.ku6.com\/show\/([^\/]+).html", "http://player.ku6.com/refer/{0}/v.swf");
            else if (lowerUrl.Contains("v.ku6.com/special/show_"))
                flv = GetFlvUrl(url, @"http:\/\/v.ku6.com\/special\/show_\d+\/([^\/]+).html", "http://player.ku6.com/refer/{0}/v.swf");
            else if (lowerUrl.Contains("www.youtube.com/watch?"))
                flv = GetFlvUrl(url, @"http:\/\/www.youtube.com\/watch\?v=([^\/&]+)&?", "http://www.youtube.com/v/{0}&hl=zh_CN&fs=1");
            else if (lowerUrl.Contains("you.video.sina.com.cn/b/"))
                flv = GetFlvUrl(url, @"http:\/\/you.video.sina.com.cn\/b\/(\d+)-(\d+).html", "http://vhead.blog.sina.com.cn/player/outer_player.swf?vid={0}");
            else if (lowerUrl.Contains("video.sina.com.cn"))
            {
                string content = Utils.GetSourceTextByUrl(url, "UTF-8", 7);
                if (content == "") return "";
                MatchCollection mc2 = Regex.Matches(content, @"swfOutsideUrl:'(.+?)',", RegexOptions.IgnoreCase);
                if (mc2.Count > 0)
                    flv = mc2[0].Groups[1].Value;
                else
                    flv = "";
            }
            else if (lowerUrl.Contains("http://v.blog.sohu.com/u/"))
                flv = GetFlvUrl(url, @"http:\/\/v.blog.sohu.com\/u\/[^\/]+\/(\d+)", "http://v.blog.sohu.com/fo/v4/{0}");
            else if (lowerUrl.Contains("tv.sohu.com"))
            {
                string content = Utils.GetSourceTextByUrl(url, "UTF-8", 7);
                if (content == "") return "";
                MatchCollection mc2 = Regex.Matches(content, "<meta property=\\\"og:videosrc\\\" content=\\\"(.+?)\\\" \\/>", RegexOptions.IgnoreCase);
                if (mc2.Count > 0)
                    flv = mc2[0].Groups[1].Value;
                else
                    flv = "";
            }
            else if (lowerUrl.Contains("http://v.qq.com"))
            {
                MatchCollection mc;
                if ((mc = Regex.Matches(url, @"http:\/\/v.qq.com\/play\/(.+?).html", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://static.video.qq.com/TPout.swf?vid={0}", mc[0].Groups[1].Value);
                else if ((mc = Regex.Matches(url, @"http:\/\/v.qq.com/page\/[^\/]+\/[^\/]+\/[^\/]+\/(.+?).html", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://static.video.qq.com/TPout.swf?vid={0}", mc[0].Groups[1].Value);
                else if ((mc = Regex.Matches(url, @"http:\/\/v.qq.com\/[^\/]+\/[^\/]+\/[^\/]+.html\?vid=(\w+)", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://static.video.qq.com/TPout.swf?vid={0}", mc[0].Groups[1].Value);
                else if ((mc = Regex.Matches(url, @"http:\/\/v.qq.com\/[^\/]+\/[^\/]+\/[^\/]+.html", RegexOptions.IgnoreCase)).Count > 0)
                {
                    string content = Utils.GetSourceTextByUrl(url, "UTF-8", 7);
                    if (content == "") return "";
                    MatchCollection mc2 = Regex.Matches(content, "vid:\\\"(.+?)\\\",", RegexOptions.IgnoreCase);
                    flv = string.Format("http://static.video.qq.com/TPout.swf?vid={0}", mc2[0].Groups[1].Value);
                }
                else if ((mc = Regex.Matches(url, @"http:\/\/v.qq.com\/[^\/]+\/[^\/]+\/[^\/]+\/([^\/]+).html", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://static.video.qq.com/TPout.swf?vid={0}", mc[0].Groups[1].Value);
            }
            else if (lowerUrl.Contains("http://www.56.com"))
            {
                MatchCollection mc;
                if ((mc = Regex.Matches(url, @"http:\/\/www.56.com\/\S+\/play_album-aid-(\d+)_vid-(.+?).html", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://player.56.com/v_{0}.swf", mc[0].Groups[2].Value);
                else if ((mc = Regex.Matches(url, @"http:\/\/www.56.com\/\S+\/([^\/]+).html", RegexOptions.IgnoreCase)).Count > 0)
                    flv = string.Format("http://player.56.com/{0}.swf", mc[0].Groups[1].Value);
            }

            if (!string.IsNullOrEmpty(flv) && width != 0 && height != 0)
            {
                return string.Format(@"<script type=""text/javascript"" reload=""1"">document.write(AC_FL_RunContent('width', '{0}', 'height', '{1}', 'allowNetworking', 'internal', 'allowScriptAccess', 'never', 'src', '{2}', 'quality', 'high', 'bgcolor', '#ffffff', 'wmode', 'transparent', 'allowfullscreen', 'true'));</script>", width, height, flv);

            }
            return "";
        }

        private static string GetFlvUrl(string url, string reg, string flvFormat)
        {
            return GetFlvUrl(url, reg, flvFormat, 1);
        }

        private static string GetFlvUrl(string url, string reg, string flvFormat, int groupIndex)
        {
            string flv = "";
            MatchCollection mc = Regex.Matches(url, reg, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                flv = string.Format(flvFormat, mc[0].Groups[groupIndex].Value);
            return flv;
        }

        public static string ParseAudio(string autostart, string url)
        {
            return string.Format(@"<object width=""400"" height=""64"" classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6""><param value=""0"" name=""invokeURLs""><param value=""{0}"" name=""autostart""><param value=""{1}"" name=""url""><embed width=""400"" height=""64"" type=""application/x-mplayer2"" autostart=""{0}"" src=""{1}""></object>", autostart != "" ? "1" : "0", url);
        }

        public static string ParseP(string lineHeight, string textIndent, string textAlign, string content)
        {
            return string.Format(@"<p style=""line-height: {0}px; text-indent: {1}em; text-align: {2};"">{3}</p>", lineHeight, textIndent, textAlign, content);
        }

        public static string ParseFlash(string flashWidth, string flashHeight, string flashUrl)
        {
            flashWidth = flashWidth == "" ? "550" : flashWidth;
            flashHeight = flashHeight == "" ? "400" : flashHeight;
            string randomid = "swf_" + Guid.NewGuid();
            if (Utils.GetFileExtName(flashUrl) != ".flv")
                return string.Format("<span id=\"{0}\"></span><script type=\"text/javascript\" reload=\"1\">$('{0}').innerHTML=AC_FL_RunContent('width', '{1}', 'height', '{2}', 'allowNetworking', 'internal', 'allowScriptAccess', 'none', 'src', '{3}', 'quality', 'high', 'bgcolor', '#ffffff', 'wmode', 'transparent', 'allowfullscreen', 'true');</script>", randomid, flashWidth, flashHeight, flashUrl);
            else
                return string.Format("<span id=\"{0}\"></span><script type=\"text/javascript\" reload=\"1\">$('{0}').innerHTML=AC_FL_RunContent('width', '{1}', 'height', '{2}', 'allowNetworking', 'internal', 'allowScriptAccess', 'none', 'src', '{3}/sta/swf/flvplayer.swf', 'flashvars', 'file={4}', 'quality', 'high', 'wmode', 'transparent', 'allowfullscreen', 'true');</script>", randomid, flashWidth, flashHeight, BaseConfigs.GetSitePath, Utils.UrlEncode(flashUrl));
        }

        /// <summary>
        /// 该方法已被抽到Utils类中
        /// </summary>
        /// <param name="sDetail">内容</param>
        /// <returns>内容</returns>
        public static string ClearUBB(string sDetail)
        {
            return Regex.Replace(sDetail, @"\[[^\]]*?\]", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 清除UBB标签
        /// </summary>
        /// <param name="sDetail">内容</param>
        /// <returns>内容</returns>
        public static string ClearBR(string sDetail)
        {
            return Regex.Replace(sDetail, @"[\r\n]", string.Empty, RegexOptions.IgnoreCase);
        }

    }
}
