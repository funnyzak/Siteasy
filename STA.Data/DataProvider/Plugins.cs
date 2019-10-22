using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;

namespace STA.Data
{
    public class Plugins
    {
        public static int AddPlugin(PluginInfo info)
        {
            return DatabaseProvider.GetInstance().AddPlugin(info);
        }

        public static bool DelPlugin(int id)
        {
            return DatabaseProvider.GetInstance().DelPlugin(id) > 0;
        }

        public static bool EditPlugin(PluginInfo info)
        {
            return DatabaseProvider.GetInstance().EditPlugin(info) > 0;
        }

        public static PluginInfo GetPlugin(int id)
        {
            PluginInfo info = null;
            using (IDataReader reader = DatabaseProvider.GetInstance().GetPlugin(id))
            {
                if (reader.Read())
                {
                    info = new PluginInfo();
                    info.Id = TypeParse.StrToInt(reader["id"]);
                    info.Name = reader["name"].ToString();
                    info.Email = reader["email"].ToString();
                    info.Author = reader["author"].ToString();
                    info.Pubtime = TypeParse.StrToDateTime(reader["pubtime"]);
                    info.Officesite = reader["officesite"].ToString();
                    info.Menu = reader["menu"].ToString();
                    info.Description = reader["description"].ToString();
                    info.Dbcreate = reader["dbcreate"].ToString();
                    info.Dbdelete = reader["dbdelete"].ToString();
                    info.Filelist = reader["filelist"].ToString();
                    info.Package = reader["package"].ToString();
                    info.Setup = byte.Parse(reader["setup"].ToString());
                }
            }
            return info;
        }

        public static DataTable GetPluginDataPage(int pagecurrent, int pagesize, out int pagecount, out int recordcount)
        {
            return DatabaseProvider.GetInstance().GetPluginDataPage(pagecurrent, pagesize, out pagecount, out recordcount);
        }

        public static DataTable GetPluginTable(int setup)
        {
            return DatabaseProvider.GetInstance().GetPluginTable(setup);
        }
    }
}
