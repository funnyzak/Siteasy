using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using STA.Common;
using STA.Config;
using STA.Data;
using System.Data;

namespace STA.Core.Publish
{
    public class SitemapCreate
    {
        public static void Create(string frequency, string priority, int count)
        {
            try
            {
                Caches.RemoveUrlCache();
  

                GeneralConfigInfo config = GeneralConfigs.GetConfig();

                XmlDocument mapdoc = new XmlDocument();
                XmlDeclaration declareation;
                declareation = mapdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                mapdoc.AppendChild(declareation);

                XmlElement xeRoot = mapdoc.CreateElement("urlset");
                xeRoot.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                mapdoc.AppendChild(xeRoot);
                XmlNode root = mapdoc.SelectSingleNode("urlset");

                root.AppendChild(GetElement(mapdoc, DateTime.Now.ToString("yyyy-MM-dd"), config.Weburl + "/", frequency, priority));

                using (DataTable chldt = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60))
                {
                    foreach (DataRow dr in chldt.Rows)
                    {
                        root.AppendChild(GetElement(mapdoc, TypeParse.StrToDateTime(dr["addtime"]).ToString("yyyy-MM-dd"), config.Weburl + Urls.Channel(TypeParse.StrToInt(dr["id"])), frequency, priority));
                    }
                }

                using (DataTable pgedt = DatabaseProvider.GetInstance().GetPageTableForSiteMap())
                {
                    foreach (DataRow dr in pgedt.Rows)
                    {
                        root.AppendChild(GetElement(mapdoc, TypeParse.StrToDateTime(dr["addtime"]).ToString("yyyy-MM-dd"), config.Weburl + Urls.Page(TypeParse.StrToInt(dr["id"])), frequency, priority));
                    }
                }

                using (DataTable condt = DatabaseProvider.GetInstance().GetContentForSiteMap(count))
                {
                    foreach (DataRow dr in condt.Rows)
                    {
                        root.AppendChild(GetElement(mapdoc, TypeParse.StrToDateTime(dr["updatetime"]).ToString("yyyy-MM-dd"), config.Weburl + Urls.Content(TypeParse.StrToInt(dr["id"]), TypeParse.StrToInt(dr["typeid"]), dr["savepath"].ToString(), dr["filename"].ToString()), frequency, priority));
                    }
                }
                mapdoc.Save(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sitemap.xml"));
            }
            catch (Exception ex)
            {
                STAException.WriteError(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error"), "Sitemap生成错误！", ex);
            }
        }


        private static XmlElement GetElement(XmlDocument xmlparent, string time, string url, string frequency, string priority)
        {
            XmlElement xurl = xmlparent.CreateElement("url");
            XmlElement xloc = xmlparent.CreateElement("loc");
            xloc.InnerText = url;
            xurl.AppendChild(xloc);
            XmlElement xlastmod = xmlparent.CreateElement("lastmod");
            xlastmod.InnerText = time;
            xurl.AppendChild(xlastmod);
            XmlElement xchangefreq = xmlparent.CreateElement("changefreq");
            xchangefreq.InnerText = frequency;
            xurl.AppendChild(xchangefreq);
            XmlElement xpriority = xmlparent.CreateElement("priority");
            xpriority.InnerText = priority;
            xurl.AppendChild(xpriority);
            return xurl;
        }
    }
}
