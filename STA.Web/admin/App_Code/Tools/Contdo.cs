using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Entity;
using STA.Core;
using STA.Config;
using System.IO;
using System.Data;

namespace STA.Web.Admin.Tools
{
    public class Contdo : AdminPage
    {
        public Contdo()
        {
            switch (STARequest.GetString("t").ToLower())
            {
                case "fileupload":
                    FileUpload();
                    break;
                case "downloadword":
                    string words = "";
                    foreach (DataRow dr in BanWords.GetBanWordList().Rows)
                    {
                        words += dr[3].ToString() + "=" + dr[4].ToString() + "\r\n";
                    }
                    Utils.ResponseText(words, "banwords.txt", "txt/plain");
                    break;
                default:
                    HttpContext.Current.Response.StatusCode = 404;
                    break;
            }
        }

        public void FileUpload()
        {
            string repstr = "";
            AttachmentInfo[] info = ConUtils.SaveRequestFiles(1, STARequest.GetQueryString("filetype"), STARequest.GetQueryInt("maxsize", 2048),
                                    STARequest.GetQueryString("savedir"), STARequest.GetQueryString("savename"), STARequest.GetQueryInt("overfile", 1),
                                    config.Waterposition, "", STARequest.GetQueryString("filekey"), config);

            if (info != null)
            {
                if (info[0].Noupload == "")
                {
                    info[0].Lastedituid = info[0].Uid = userid;
                    info[0].Lasteditusername = info[0].Username = username;
                    STA.Data.Contents.AddAttachment(info[0]);
                }
                else
                {
                    repstr = info[0].Noupload;
                }
            }
            else
            {
                repstr = "上传失败！";
            }

            if (repstr == "")
            {
                ResponseText("{" + string.Format("'id':'{0}','name':'{1}','path':'{2}','msg':'{3}'", info[0].Id, info[0].Attachment, baseconfig.Sitepath + info[0].Filename, "") + "}");
            }
            else
            {
                ResponseText("{'msg':'" + repstr + "'}");
            }
        }

        private void ResponseText(String text)
        {
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }
    }
}
