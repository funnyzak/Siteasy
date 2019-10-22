using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using STA.Common;
using STA.Entity;
using STA.Core;
using STA.Config;
using System.IO;
using STA.Data;
namespace STA.Web.Admin.Tools
{
    public class EditorUpload : AdminPage
    {
        private string fileVirtualPath = string.Empty;
        private string msg = string.Empty;
        private string acceptFileType = string.Empty;
        private int fileMaxLength = 10240; //单位kb
        public EditorUpload()
        {
            string funcNum = STARequest.GetString("CKEditorFuncNum");
            try
            {
                if (!STARequest.IsPost() || HttpContext.Current.Request.Files.Count <= 0) return;
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength / 1024 > fileMaxLength)
                {
                    msg = "上传的文件大小，不是所允许的范围，请控制在" + fileMaxLength.ToString() + "KB之内！";
                }
                else
                {
                    string filename = Path.GetFileName(file.FileName);
                    string fileExt = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
                    bool isAcceptFileType = false;
                    switch (STARequest.GetString("ftype"))
                    {
                        case "image":
                            isAcceptFileType = Utils.IsImgFilename(filename);
                            break;
                        case "flash":
                            isAcceptFileType = fileExt.ToLower().Equals("swf");
                            break;
                        default:
                            break;
                    }
                    if (!isAcceptFileType)
                    {
                        msg = "上传文件类型不是系统所接受的类型，请上传合适的类型！";
                    }
                    else
                    {
                        string savedir = STARequest.GetQueryString("savedir").Trim();
                        if (savedir != "") savedir += "/";
                        AttachmentInfo[] info = ConUtils.SaveRequestFiles(1, "*", 100000, Utils.GetMapPath(sitepath + filesavepath + "/"
                                                            + savedir), "", 1, config.Waterposition, "", "upload", config);
                        if (info != null)
                        {
                            if (info[0].Noupload == "")
                            {
                                fileVirtualPath = info[0].Filename;
                                info[0].Lastedituid = info[0].Uid = userid;
                                info[0].Lasteditusername = info[0].Username = username;
                                Contents.AddAttachment(info[0]);
                            }
                            else
                            {
                                msg = info[0].Noupload;
                            }
                        }
                        else
                        {
                            msg = "上传失败！";
                        }
                    }
                }
                this.ResponseScript(funcNum, fileVirtualPath, msg);
            }
            catch (Exception e)
            {
                this.ResponseScript(funcNum, fileVirtualPath, string.Format("文件上传失败:{0}", e.Message));
            }
        }

        private void ResponseScript(string funcNum, string fileUrl, string msg)
        {
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(" + funcNum + ",'" + fileUrl + "','" + msg + "');</script>");
        }
    }
}
