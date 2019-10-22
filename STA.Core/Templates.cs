using System;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Cache;
using STA.Entity;
namespace STA.Core
{
    public class Templates : PageTemplate
    {
        public string CreateTpl(string sitePath, string skinName, string tplSaveName, string templateName, string subDir)
        {
            return CreateTpl(sitePath, skinName, tplSaveName, templateName, subDir, 1);
        }

        public string MakeTplAspx(string skinName)
        {
            BaseConfigInfo baseconfig = BaseConfigs.GetBaseConfig();
            GeneralConfigInfo generalconfig = GeneralConfigs.GetConfig();
            string tplpath = Utils.GetMapPath(baseconfig.Sitepath + "/templates/" + skinName + "/" + generalconfig.Templatesavedirname + "/");
            if (!FileUtil.DirExists(tplpath))
                return "模版文件夹不存在，请检查模版目录设置！";
            List<FileItem> flist = FileUtil.GetFiles(tplpath, "htm");
            foreach (FileItem item in flist)
            {
                string templatename = item.Name.Substring(0, item.Name.LastIndexOf('.'));
                string subDir = item.FullName.Replace(tplpath, "").Replace(item.Name, "");
                CreateTpl(baseconfig.Sitepath + "/", skinName, generalconfig.Templatesavedirname, templatename, subDir);
            }
            return "yes";
        }

        public static String ContentTemplate(int id)
        {
            string cacheKey = CacheKeys.TEMPLATE_NAME + "content" + id.ToString();
            string template = Caches.GetObject(cacheKey) as string;
            if (template != null)
                return template;

            template = DatabaseProvider.GetInstance().TemplateContent(id).Trim();
            if (template == string.Empty) return template;
            Caches.AddObject(cacheKey, template);
            return template;
        }

        public static String ChannelTemplate(int id)
        {
            string cacheKey = CacheKeys.TEMPLATE_NAME + "channel" + id.ToString();
            string template = Caches.GetObject(cacheKey) as string;
            if (template != null)
                return template;

            template = DatabaseProvider.GetInstance().TemplateChannel(id).Trim();
            if (template == string.Empty) return template;
            Caches.AddObject(cacheKey, template);
            return template;
        }

        public static String SpecialTemplate(int id)
        {
            string cacheKey = CacheKeys.TEMPLATE_NAME + "special" + id.ToString();
            string template = Caches.GetObject(cacheKey) as string;
            if (template != null)
                return template;

            template = DatabaseProvider.GetInstance().TemplateSpecial(id).Trim();
            if (template == string.Empty) return template;
            Caches.AddObject(cacheKey, template);
            return template;
        }

        public static String PageTemplate(int id)
        {
            string cacheKey = CacheKeys.TEMPLATE_NAME + "page" + id.ToString();
            string template = Caches.GetObject(cacheKey) as string;
            if (template != null)
                return template;

            template = DatabaseProvider.GetInstance().TemplatePage(id).Trim();
            if (template == string.Empty) return template;
            Caches.AddObject(cacheKey, template);
            return template;
        }

        public static String SpecGroupTemplate(int id)
        {
            string cacheKey = CacheKeys.TEMPLATE_NAME + "specgroup" + id.ToString();
            string template = Caches.GetObject(cacheKey) as string;
            if (template != null)
                return template;

            template = DatabaseProvider.GetInstance().TemplateSpecGroup(id).Trim();
            if (template == string.Empty) return template;
            Caches.AddObject(cacheKey, template);
            return template;
        }
    }
}
