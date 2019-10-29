using STA.Core;
using STA.Page;
using STA.Common;
using System;
using STA.Entity;
using STA.Web.Plus.CTAA.KJ.Core;
using STA.Data;
using STA.Web.Plus.CTAA.KJ.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STA.Web.Plus.CTAA.KJ.Page
{
    public class PhotoUpload : GradeBase
    {

        protected override void PageShow()
        {
            if (ConUtils.IsCrossSitePost()) return;


            AttachmentInfo[] atts = ConUtils.SaveRequestFiles(1, "jpg,jpeg,bmp,png", 2048, Utils.GetMapPath(baseconfig.Sitepath + config.Attachsavepath + "/grade/profile/photo/"), STA.Common.Rand.Str(32).ToLower(), 1, 0, "", "file", config);
            if (atts.Length == 0)
            {
                ResponseJSON(PlusUtils.Result<String>(null, -1, "上传失败"));
            }
            else
            {
                DatabaseProvider.GetInstance().AddAttachment(atts[0]);

                String imgurl = String.Format("http://{0}{1}",STARequest.GetCurrentFullHost(),atts[0].Filename);
                JObject objectJson = new JObject();
                objectJson["imgurl"] = imgurl;

                String result = HttpPost.Post(ApiMethod.IMAGE_DOWNLOAD, JsonConvert.SerializeObject(objectJson));

                resultOuput<UploadFile>(result);
            }

        }
    }
}