using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Data
{
    public class DbFields
    {
        public const string SPEC_CONTENT = "id, typeid, click, typename, channelid, channelname, title, subtitle, addtime, color,adduser, addusername, lastedituser, lasteditusername, author, source, img";
        public const string User = "[id]，[username]，[nickname]，[password]，[safecode]，[spaceid]，[gender]，[birthday]，[adminid]，[admingroupname]，[groupid]，[groupname]，[extgroupids]，[regip]，[addtime]，[loginip]，[logintime]，[lastaction]，[money]，[credits]，[extcredits1]，[extcredits2]，[extcredits3]，[extcredits4]，[extcredits5]，[email]，[ischeck]，[locked]，[newpm]，[newpmcount]，[onlinestate]，[Invisible]，[showemail]";
        public const string WORDS = "[id],[uid],[username],[find],[replacement]";
        public const string PMS = "[id],[msgfrom],[msgfromid],[msgto],[msgtoid],[folder],[new],[subject],[addtime],[content]";
        public const string Content = "[id], [typeid], [typename], [channelfamily], [channelid], [channelname], [extchannels], [title], [subtitle], [addtime], [updatetime], [color], [property], [adduser], [addusername], [lastedituser], [lasteditusername], [author], [source], [img], [url], [seotitle], [seokeywords], [seodescription], [savepath], [filename], [template], [content], [status], [viewgroup], [iscomment], [ishtml], [click], [orderid], [diggcount], [stampcount], [commentcount], [credits], [relates]";
    }
}
