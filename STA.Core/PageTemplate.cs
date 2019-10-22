
using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Cache;
using STA.Entity;

namespace STA.Core
{
    /// <summary>
    /// Template为页面模板类.
    /// </summary>
    public abstract class PageTemplate
    {
        private string defineInt = string.Empty;
        private string loopInt = string.Empty;
        private string tables = ",";
        private string deftable = "";
        public static Regex[] r = new Regex[28];

        static PageTemplate()
        {

            RegexOptions options = Utils.GetRegexCompiledOptions();

            r[0] = new Regex(@"<%include ([^\[\]\{\}\s]+)%>", options);
            r[1] = new Regex(@"<%loop ((\(([a-zA-Z]+)\) )?)([^\[\]\{\}\s]+) ([^\[\]\{\}\s]+)%>", options);
            r[2] = new Regex(@"<%\/loop%>", options);
            r[3] = new Regex(@"{ad (?:\s*)([\w\s]+)}", options);
            r[4] = new Regex(@"<%urltext (?:\s*)([a-zA-Z\d-]+)?(?:\s*)\(([^\s]+)\)(?:\s*)%>");
            r[5] = new Regex(@"<%if (?:\s*)(([^\s]+)((?:\s*)(\|\||\&\&)(?:\s*)([^\s]+))?)(?:\s*)%>", options);
            r[6] = new Regex(@"<%else(( (?:\s*)if (?:\s*)(([^\s]+)((?:\s*)(\|\||\&\&)(?:\s*)([^\s]+))?))?)(?:\s*)%>", options);
            r[7] = new Regex(@"<%\/if%>", options);
            r[8] = new Regex(@"(\{strtoint\(([^\s]+?)\)\})", options);
            r[9] = new Regex(@"(<%urlencode\(([^\s]+?)\)%>)", options);
            r[10] = new Regex(@"(<%datetostr\(([^\s]+?),(.*?)\)%>)", options);
            r[11] = new Regex(@"(\{([^\.\[\]\{\}\s]+)\.([^\[\]\{\}\s]+)\})", options);
            r[12] = new Regex(@"(\{request\[([^\[\]\{\}\s]+)\]\})", options);
            r[13] = new Regex(@"(\{([^\[\]\{\}\s]+)\[([^\[\]\{\}\s]+)\]\})", options);
            r[14] = new Regex(@"(<%sqlstring (?:\s*)(\w+) (?:\s*)([\w\s\*%><,\[\]'\=]+)(?:\s*)%>)", options);
            r[15] = new Regex(@"({([^\[\]/\{\}='\s]+)})", options);
            r[16] = new Regex(@"(([=|>|<|!]=)\\" + "\"" + @"([^\s]*)\\" + "\")", options);
            r[17] = new Regex(@"<%namespace (?:""?)([\s\S]+?)(?:""?)%>", options);
            r[18] = new Regex(@"<%csharp%>([\s\S]+?)<%/csharp%>", options);
            r[19] = new Regex(@"<%set ((\(([a-zA-Z]+)\))?)(?:\s*)\{([^\s]+)\}(?:\s*)=(?:\s*)(.*?)(?:\s*)%>", options);
            r[20] = new Regex(@"(<%getsubstring\(([^\s]+?),(.\d*?),([^\s]+?)\)%>)", options);
            r[21] = new Regex(@"<%for\(([^\s]+?)(?:\s*),(?:\s*)([^\s]+?)\)%>", options);
            r[22] = new Regex(@"<%inherits (?:""?)([\s\S]+?)(?:""?)%>", options);
            r[23] = new Regex(@"<%continue%>");
            r[24] = new Regex(@"<%break%>");
            r[25] = new Regex(@"<%load_data (?:\s*)action=([a-zA-Z]+) (?:\s*)(output=([a-zA-Z\d]+) (?:\s*))?(.+)?%>");
            r[26] = new Regex(@"(\{variable\[([^\[\]\{\}\s]+)\]\})", options);
            r[27] = new Regex(@"<%dbtable (?:\s*)table=(\w+) (?:\s*)(output=([a-zA-Z\d]+) (?:\s*))?(.+)?%>", options);
        }

        public virtual string CreateTpl(string webPath, string skinName, string tplname, string templateName, int nest)
        {
            return CreateTpl(webPath, skinName, tplname, templateName, "", nest);
        }

        /// <summary>
        /// 获得模板字符串.
        /// </summary>
        /// <param name="webPath">网站虚拟路径如/</param>
        /// <param name="skinName">模版名(路径)</param>
        /// <param name="tplName">模版文件所在文件夹</param>
        /// <param name="templateName">模版名称</param>
        /// <param name="templateSubDirectory">模版所在子目录</param>
        /// <param name="nest">嵌套次数</param>
        /// <returns></returns>
        public virtual string CreateTpl(string webPath, string skinName, string tplName, string templateName, string templateSubDirectory, int nest)
        {
            StringBuilder strReturn = new StringBuilder(220000);
            if (nest < 1)
                nest = 1;
            else if (nest > 5)
                return "";

            if (templateName.StartsWith("_") && nest == 1) //不生成以_开头的模版文件
                return "";

            string extNamespace = "";
            if (templateSubDirectory != string.Empty && !templateSubDirectory.EndsWith("\\"))
            {
                templateSubDirectory += "\\";
            }

            string htmlPathFormatStr = "{0}\\{1}\\{2}\\{3}{4}.htm";
            string createFilePath;

            if (nest > 1
                && templateName.Split('/').Length > 1
                && File.Exists(string.Format(htmlPathFormatStr, Utils.GetMapPath(webPath + "templates"), skinName, tplName, "", templateName.Replace("/", "\\"))))
            //通过include嵌套的绝对路径模板 如：common/_header
            {
                createFilePath = string.Format(htmlPathFormatStr, Utils.GetMapPath(webPath + "templates"), skinName, tplName, "", templateName.Replace("/", "\\"));
            }
            else
            {
                if (File.Exists(string.Format(htmlPathFormatStr, Utils.GetMapPath(webPath + "templates"), skinName, tplName, templateSubDirectory, templateName)))
                    createFilePath = string.Format(htmlPathFormatStr, Utils.GetMapPath(webPath + "templates"), skinName, tplName, templateSubDirectory, templateName);
                else
                    return "";
            }
            //模版类
            string inherits = "STA.Page.";
            if (templateSubDirectory.ToLower().EndsWith("plus\\"))
            {
                inherits += "Plus." + templateName.Substring(0, 1).ToUpper() + templateName.Substring(1).ToLower();
            }
            else if (templateSubDirectory.ToLower().EndsWith("user\\"))
            {
                inherits += "User." + templateName.Substring(0, 1).ToUpper() + templateName.Substring(1).ToLower();
            }
            else if (templateSubDirectory != "")
            {
                inherits += "PageBase";
            }
            else
            {
                XmlDocument ihtdoc = XMLUtil.LoadDocument(Utils.GetMapPath(webPath + "sta/config/tpl.config"));
                string tmpname = templateName;
                if (tmpname.IndexOf('_') >= 0)
                    tmpname = tmpname.Substring(0, tmpname.IndexOf('_') + 1) + "*";
                XmlNode itemnode = ihtdoc.SelectSingleNode("tpl/page/item[@filename='" + tmpname + "']");
                if (itemnode != null)
                    inherits = itemnode.Attributes["inherits"].Value;
                else
                    inherits += "PageBase";
            }

            using (StreamReader objReader = new StreamReader(createFilePath, Encoding.UTF8))
            {
                StringBuilder textOutput = new StringBuilder(70000);

                textOutput.Append(objReader.ReadToEnd());
                objReader.Close();

                //处理命名空间
                if (nest == 1)
                {
                    //命名空间
                    foreach (Match m in r[17].Matches(textOutput.ToString()))
                    {
                        extNamespace += "\r\n<%@ Import namespace=\"" + m.Groups[1] + "\" %>";
                        textOutput.Replace(m.Groups[0].ToString(), string.Empty);
                    }

                    //inherits
                    foreach (Match m in r[22].Matches(textOutput.ToString()))
                    {
                        inherits = m.Groups[1].ToString();
                        textOutput.Replace(m.Groups[0].ToString(), string.Empty);
                        break;
                    }
                    if ("\"".Equals(inherits))
                    {
                        inherits = "STA.Page.PageBase";
                    }

                }
                //处理Csharp语句
                foreach (Match m in r[18].Matches(textOutput.ToString()))
                {
                    textOutput.Replace(m.Groups[0].ToString(), m.Groups[0].ToString().Replace("\r\n", "\r\t\r"));
                }

                foreach (Match m in r[3].Matches(textOutput.ToString()))
                {
                    AdInfo adinfo = Contents.GetAd(m.Groups[1].Value);
                    textOutput.Replace(m.Groups[0].ToString(), adinfo == null ? "广告不存在" :
                        string.Format("<script type=\"text/javascript\" src=\"{0}?{1}\"></script>", "{siteurl}" + adinfo.Filename, adinfo.Id));
                }

                textOutput.Replace("\r\n", "\r\r\r");
                textOutput.Replace("<%", "\r\r\n<%");
                textOutput.Replace("%>", "%>\r\r\n");
                textOutput.Replace("<%csharp%>\r\r\n", "<%csharp%>").Replace("\r\r\n<%/csharp%>", "<%/csharp%>");
                string[] strlist = Utils.SplitString(textOutput.ToString(), "\r\r\n");
                int count = strlist.GetUpperBound(0);

                for (int i = 0; i <= count; i++)
                {
                    if (strlist[i] == "")
                        continue;
                    strReturn.Append(ConvertTags(nest, webPath, skinName, tplName, templateSubDirectory, strlist[i], skinName));
                }
            }
            if (nest == 1)
            {
                string template = string.Format("<%@ Page language=\"c#\" AutoEventWireup=\"false\" EnableViewState=\"false\" Inherits=\"{0}\" %>\r\n<%@ Import namespace=\"System.Data\" %>\r\n<%@ Import namespace=\"STA.Common\" %>\r\n<%@ Import namespace=\"STA.Data\" %>\r\n<%@ Import namespace=\"STA.Core\" %>\r\n<%@ Import namespace=\"STA.Entity\" %>\r\n<%@ Import namespace=\"STA.Config\" %>\r\n{1}\r\n<script runat=\"server\">\r\noverride protected void OnInit(EventArgs e)\r\n{{\r\n\r\n\t/* \r\n\t\tThis page was created by Siteasy CMS Template Engine at {2}.\r\n\t\t本页面代码由Siteasy CMS模板引擎生成于 {2}. \r\n\t*/\r\n\r\n\tbase.OnInit(e);\r\n\r\n{3}\r\n\r\n{4}\r\n\r\n\ttemplateBuilder.Capacity = {5};\r\n{6}\r\n\tResponse.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());\r\n}}\r\n</script>\r\n", inherits, extNamespace, DateTime.Now, this.defineInt, this.deftable, strReturn.Capacity, Regex.Replace(strReturn.ToString(), @"\r\n\s*templateBuilder\.Append\(""""\);", ""));

                string pageDir = Utils.GetMapPath(webPath + "sta/aspx/" + skinName + "/" + templateSubDirectory + "/");
                if (!Directory.Exists(pageDir))
                    Utils.CreateDir(pageDir);

                string outputPath = pageDir + templateName + ".aspx";

                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = Encoding.UTF8.GetBytes(template);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

            }

            return strReturn.ToString();
        }

        private void TableDef(string str)
        {
            str = DefTableName(str);
            if (("," + this.tables).IndexOf("," + str + ",") == -1)
            {
                this.deftable = this.deftable + "\r\n\tDataTable " + str + ";";
                this.tables = this.tables + str + ",";
            }
        }

        private string DefTableName(string str)
        {
            if (str == "") return "list";
            return str;
        }

        private void LoopIntDef(string str)
        {
            if (("," + this.loopInt).IndexOf("," + str + ",") == -1)
            {
                this.defineInt = this.defineInt + "\r\n\tint " + str + "__loop__id=0;";
                this.loopInt = this.loopInt + str + ",";
            }
        }

        private string VarReplace(string parms, string name)
        {
            Match match = Regex.Match(parms, name + "=([\\w\\.,]+)");
            string valmatch = match.Groups[1].Value;
            string valtarget = "";
            if (valmatch != "")
            {
                foreach (string s in valmatch.Split(','))
                {
                    if (s == "") continue;
                    if (Regex.IsMatch(s, @"[a-z]+"))
                    {
                        valtarget += s + ".ToString() + \",\" +";
                    }
                    else
                    {
                        valtarget += " \"" + s + "\" + \",\" +";
                    }
                }
                valtarget = valtarget.Substring(0, valtarget.Length - 5);
                parms = parms.Replace(match.Groups[0].Value, name + "=\" + " + valtarget + "\"");
            }
            //if (match.Groups[1].Value != "" && Regex.IsMatch(match.Groups[1].Value, @"[a-z]+"))
            //    parms = parms.Replace(match.Groups[0].Value, name + "=\"+" + match.Groups[1].Value + ".ToString()+\"");
            return parms;
        }

        private string ConvertTags(int nest, string webPath, string skinName, string tplName, string templateSubDirectory, string inputStr, string skinname)
        {
            string strReturn = "";
            string strTemplate;
            strTemplate = inputStr.Replace("\\", "\\\\");
            strTemplate = strTemplate.Replace("\"", "\\\"");
            strTemplate = strTemplate.Replace("</script>", "</\");\r\n\ttemplateBuilder.Append(\"script>");
            bool IsCodeLine = false;

            foreach (Match m in r[0].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "\r\n" + CreateTpl(webPath, skinName, tplName, m.Groups[1].ToString(), templateSubDirectory, nest + 1) + "\r\n");
            }
            foreach (Match m in r[1].Matches(strTemplate))
            {
                IsCodeLine = true;
                if (m.Groups[3].ToString() == "")
                {
                    LoopIntDef(m.Groups[4].ToString());
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        string.Format("\r\n\t{0}__loop__id=0;\r\n\tforeach(DataRow {0} in {1}.Rows)\r\n\t{{\r\n\t\t{0}__loop__id++;\r\n", m.Groups[4], m.Groups[5]));
                }
                else
                {
                    LoopIntDef(m.Groups[4].ToString());
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        string.Format("\r\n\t{1}__loop__id=0;\r\n\tforeach({0} {1} in {2})\r\n\t{{\r\n\t\t{1}__loop__id++;\r\n", m.Groups[3], m.Groups[4], m.Groups[5]));
                }
            }

            foreach (Match m in r[4].Matches(strTemplate))
            {
                IsCodeLine = true;
                string encode = m.Groups[1].Value;
                encode = encode.Trim() == "" ? "utf-8" : encode;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\r\n\ttemplateBuilder.Append(Utils.GetPageContent(new Uri(\"{0}\"), \"{1}\"));\r\n", m.Groups[2], encode));
            }

            foreach (Match m in r[14].Matches(strTemplate))
            {
                IsCodeLine = true;
                TableDef(m.Groups[2].ToString());
                string sql = m.Groups[3].Value;
                foreach (string str in sql.Split(' '))
                {
                    if (str.Trim() == "") continue;
                    if (str.StartsWith("variable_"))
                    {
                        sql = sql.Replace(str, string.Format("\" + {0}.ToString() +\"", str.Substring(9)));
                    }
                }
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\r\n\t{0} = GetSqlTable(\"{1}\");\r\n", DefTableName(m.Groups[2].Value), sql));
            }

            foreach (Match m in r[25].Matches(strTemplate))
            {
                IsCodeLine = true;
                TableDef(m.Groups[3].ToString());

                string parms = m.Groups[4].Value;
                parms = VarReplace(parms, "id");
                parms = VarReplace(parms, "num");
                parms = VarReplace(parms, "page");
                parms = VarReplace(parms, "group");
                parms = VarReplace(parms, "uid");
                parms = VarReplace(parms, "order");
                parms = VarReplace(parms, "ordertype");
                parms = VarReplace(parms, "cache");
                parms = VarReplace(parms, "self");
                parms = VarReplace(parms, "durdate");

                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\r\n\t{0} = GetTable(\"{1}\", \"{2}\");\r\n", DefTableName(m.Groups[3].ToString()), m.Groups[1], parms));
            }


            foreach (Match m in r[27].Matches(strTemplate))
            {
                IsCodeLine = true;
                TableDef(m.Groups[3].ToString());

                string parms = m.Groups[4].Value;
                parms = VarReplace(parms, "num");
                parms = VarReplace(parms, "cache");

                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\r\n\t{0} = GetDbTable(\"{1}\",\"{2}\");\r\n", DefTableName(m.Groups[3].ToString()), m.Groups[1], parms));
            }

            foreach (Match m in r[2].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    "\r\n\t}\t//end loop\r\n");
            }

            foreach (Match m in r[5].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    "\r\n\tif (" + m.Groups[1].ToString().Replace("\\\"", "\"") + ")\r\n\t{\r\n");
            }

            foreach (Match m in r[6].Matches(strTemplate))
            {
                IsCodeLine = true;
                if (m.Groups[1].ToString() == string.Empty)
                {
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    "\r\n\t}\r\n\telse\r\n\t{\r\n");
                }
                else
                {
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        "\r\n\t}\r\n\telse if (" + m.Groups[3].ToString().Replace("\\\"", "\"") + ")\r\n\t{\r\n");
                }
            }

            foreach (Match m in r[7].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    "\r\n\t}\t//end if\r\n");
            }

            foreach (Match m in r[19].Matches(strTemplate))
            {
                IsCodeLine = true;
                string type = "";
                if (m.Groups[3].ToString() != string.Empty)
                {
                    type = m.Groups[3].ToString();
                }
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    string.Format("\t{0} {1} = {2};\r\n\t", type, m.Groups[4], m.Groups[5]).Replace("\\\"", "\"")
                    );
            }

            foreach (Match m in r[21].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                                                 "\tfor (int i = 0; i < " + m.Groups[2] + "; i++)\r\n\t{\r\n\t\ttemplateBuilder.Append(" + m.Groups[1].ToString().Replace("\\\"", "\"").Replace("\\\\", "\\") + ");\r\n\t}\r\n");
            }

            foreach (Match m in r[23].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "\tcontinue;\r\n");
            }

            foreach (Match m in r[24].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "\tbreak;\r\n");
            }

            foreach (Match m in r[8].Matches(strTemplate))
            {
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                    "Utils.StrToInt(" + m.Groups[2] + ", 0)");
            }

            foreach (Match m in r[9].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "templateBuilder.Append(Utils.UrlEncode(" + m.Groups[2] + "));");
            }

            foreach (Match m in r[10].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                              string.Format("\ttemplateBuilder.Append(Convert.ToDateTime({0}).ToString(\"{1}\"));", m.Groups[2], m.Groups[3].ToString().Replace("\\\"", string.Empty)));
            }

            foreach (Match m in r[20].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                              string.Format("\ttemplateBuilder.Append(Utils.GetUnicodeSubString({0},{1},\"{2}\"));", m.Groups[2], m.Groups[3], m.Groups[4].ToString().Replace("\\\"", string.Empty)));
            }

            foreach (Match m in r[11].Matches(strTemplate))
            {
                if (IsCodeLine)
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        string.Format("{0}.{1}{2}", m.Groups[2], m.Groups[3].ToString().Substring(0, 1).ToUpper(), m.Groups[3].ToString().Substring(1, m.Groups[3].ToString().Length - 1)));
                else
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        string.Format("\");\r\n\ttemplateBuilder.Append({0}.{1}.ToString().Trim());\r\n\ttemplateBuilder.Append(\"", m.Groups[2], m.Groups[3].ToString()));
            }

            foreach (Match m in r[12].Matches(strTemplate))
            {
                if (IsCodeLine)
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "STARequest.GetString(\"" + m.Groups[2] + "\")");
                else
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\" + STARequest.GetString(\"{0}\") + \"", m.Groups[2]));
            }

            foreach (Match m in r[26].Matches(strTemplate))
            {
                if (IsCodeLine)
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(), "Variable(\"" + m.Groups[2] + "\")");
                else
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\" + Variable(\"{0}\") + \"", m.Groups[2]));
            }

            foreach (Match m in r[13].Matches(strTemplate))
            {
                if (IsCodeLine)
                {
                    if (Utils.IsNumeric(m.Groups[3].ToString()))
                        strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[2] + "[" + m.Groups[3] + "].ToString().Trim()");
                    else
                    {
                        if (m.Groups[3].ToString() == "_id")
                            strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[2] + "__loop__id");
                        else
                            strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[2] + "[\"" + m.Groups[3] + "\"].ToString().Trim()");
                    }
                }
                else
                {
                    if (Utils.IsNumeric(m.Groups[3].ToString()))
                        strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\" + {0}[{1}].ToString().Trim() + \"", m.Groups[2], m.Groups[3]));
                    else
                    {
                        if (m.Groups[3].ToString() == "_id")
                            strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\" + {0}__loop__id.ToString() + \"", m.Groups[2]));
                        else
                            strTemplate = strTemplate.Replace(m.Groups[0].ToString(), string.Format("\" + {0}[\"{1}\"].ToString().Trim() + \"", m.Groups[2], m.Groups[3]));
                    }
                }
            }

            foreach (Match m in r[15].Matches(strTemplate))
            {
                if (IsCodeLine)
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[2].ToString());
                else
                    strTemplate = strTemplate.Replace(m.Groups[0].ToString(),
                        string.Format("\");\r\n\ttemplateBuilder.Append({0}.ToString());\r\n\ttemplateBuilder.Append(\"", m.Groups[2].ToString().Trim()));
            }

            foreach (Match m in r[16].Matches(strTemplate))
            {
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[2] + "\"" + m.Groups[3] + "\"");
            }

            foreach (Match m in r[18].Matches(strTemplate))
            {
                IsCodeLine = true;
                strTemplate = strTemplate.Replace(m.Groups[0].ToString(), m.Groups[1].ToString().Replace("\r\t\r", "\r\n\t").Replace("\\\"", "\""));
            }

            if (IsCodeLine)
            {
                strReturn = strTemplate + "\r\n";
            }
            else
            {
                if (strTemplate.Trim() != "")
                {
                    strReturn = "\ttemplateBuilder.Append(\"" + strTemplate.Replace("\r\r\r", "\\r\\n") + "\");";
                    strReturn = strReturn.Replace("\\r\\n<?xml", "<?xml");
                    strReturn = strReturn.Replace("\\r\\n<!DOCTYPE", "<!DOCTYPE");
                }
            }
            return strReturn;
        }
    }
}