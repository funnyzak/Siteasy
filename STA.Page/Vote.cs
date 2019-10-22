using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class Vote : PageBase
    {
        public VotetopicInfo info;
        protected override void PageShow()
        {
            if (!ConUtils.IsCrossSitePost())
            {
                string retstr;
                bool success = ConUtils.SetRequestVote(out retstr);
                if (STARequest.GetString("action") == "ajax")
                {
                    ResponseText(string.Format("{message:\"{0}\", status: \"{1}\"}", retstr, success ? 1 : 0));
                }
                else
                {
                    PageInfo(retstr, "");
                }
            }
            else
            {
                votelist = vtype == "like" ? Votes.GetVoteByLikeid(relval) : Votes.GetVoteByIds(relval);
                seotitle = "投票结果展示 - " + config.Webtitle;
            }
        }
    }
}
