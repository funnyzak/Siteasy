using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Entity.Plus;

namespace STA.Page.Plus
{
    public class Vote : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public List<VoteItem> votelist;
        public VoteInfo info;
        private string cookiename = "stavote";

        protected override void PageShow()
        {
            if (ispost && !STA.Core.ConUtils.IsCrossSitePost())
            {
                SetVote();
            }
            else
            {
                info = STA.Data.Plus.GetStaVote(id);
                if (info != null)
                {
                    if (info.IsView == 1)
                    {
                        votelist = info.VoteList;
                        votelist.Sort(new VoteCompare());
                    }
                    else
                    {
                        PageInfo("该投票禁止查看结果！", STARequest.GetUrlReferrer());
                    }
                }
                else
                {
                    PageInfo("参数错误！", STARequest.GetUrlReferrer());
                }
            }
        }

        protected int BarWid(int count, int basewidth, out string prec)
        {
            int total = info.VoteTotalCount;
            double precent = Convert.ToDouble(count) / Convert.ToDouble(total);
            prec = (precent * 100).ToString("#0.0");
            return Convert.ToInt32(Convert.ToDouble(basewidth) / (votelist[0].Count / Convert.ToDouble(total)) * precent);
        }

        private void SetVote()
        {
            if (id != 0)
            {
                info = STA.Data.Plus.GetStaVote(id);
                if (info.IsEnable == 0)
                {
                    PageInfo("该投票未启用！", STARequest.GetUrlReferrer());
                    return;
                }
                if (info.EndDate < DateTime.Now)
                {
                    PageInfo("该投票已过期！", STARequest.GetUrlReferrer());
                    return;
                }
                string items = STARequest.GetFormString("item");
                if (items == string.Empty)
                {
                    PageInfo("请选择至少一项！", STARequest.GetUrlReferrer());
                }
                else
                {
                    string strname = "vote_" + id.ToString();

                    if (info.Interval > 0 && Utils.GetCookie(cookiename, strname) != string.Empty)
                    {
                        PageInfo("您已经投过票了！", STARequest.GetUrlReferrer());
                    }
                    else
                    {
                        foreach (string item in items.Split(','))
                        {
                            int vid = TypeParse.StrToInt(item, 0);
                            STA.Data.Plus.UpdateStaVoteCount(id, vid);
                        }
                        if (info.Interval > 0)
                        {
                            Utils.WriteCookie(cookiename, strname, STARequest.GetIP(), config.Domaincookie, 60 * 24 * info.Interval);
                        }
                        PageInfo("投票成功！", STARequest.GetUrlReferrer());
                    }
                }
            }
            else
            {
                PageInfo("参数错误！", STARequest.GetUrlReferrer());
            }
        }

    }

}
