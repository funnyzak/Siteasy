using System;

namespace STA.Entity
{
    /// <summary>
    ///User的实体。
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        #region 变量定义
        private int id = 0;
        private string username = string.Empty;
        private string nickname = string.Empty;
        private string password = string.Empty;
        private string safecode = string.Empty;
        private int spaceid = 0;
        private byte gender = 1;
        private DateTime birthday = DateTime.Parse("1990-01-01");
        private int adminid = 0;
        private string admingroupname = string.Empty;
        private int groupid = 0;
        private string groupname = string.Empty;
        private string extgroupids = string.Empty;
        private string regip = string.Empty;
        private DateTime addtime = DateTime.Now;
        private string loginip = string.Empty;
        private DateTime logintime = DateTime.Now;
        private DateTime lastaction = DateTime.Now;
        private decimal money = 0;
        private int credits = 0;
        private int extcredits1 = 0;
        private int extcredits2 = 0;
        private int extcredits3 = 0;
        private int extcredits4 = 0;
        private int extcredits5 = 0;
        private string email = string.Empty;
        private byte ischeck = 0;
        private byte locked = 0;
        private byte newpm = 0;
        private int newpmcount = 0;
        private byte onlinestate = 0;
        private byte invisible = 0;
        private byte showemail = 0;
        #endregion

        #region 构造函数
        public UserInfo()
        {
        }

        public UserInfo(int id, string username, string nickname, string password, string safecode, int spaceid, byte gender, DateTime birthday, int adminid, string admingroupname, int groupid, string groupname, string extgroupids, string regip, DateTime addtime, string loginip, DateTime logintime, DateTime lastaction, decimal money, int credits, int extcredits1, int extcredits2, int extcredits3, int extcredits4, int extcredits5, string email, byte ischeck, byte locked, byte newpm, int newpmcount, byte onlinestate, byte invisible, byte showemail)
        {
            this.Id = id;
            this.Username = username;
            this.Nickname = nickname;
            this.Password = password;
            this.Safecode = safecode;
            this.Spaceid = spaceid;
            this.Gender = gender;
            this.Birthday = birthday;
            this.Adminid = adminid;
            this.Admingroupname = admingroupname;
            this.Groupid = groupid;
            this.Groupname = groupname;
            this.Extgroupids = extgroupids;
            this.Regip = regip;
            this.Addtime = addtime;
            this.Loginip = loginip;
            this.Logintime = logintime;
            this.Lastaction = lastaction;
            this.Money = money;
            this.Credits = credits;
            this.Extcredits1 = extcredits1;
            this.Extcredits2 = extcredits2;
            this.Extcredits3 = extcredits3;
            this.Extcredits4 = extcredits4;
            this.Extcredits5 = extcredits5;
            this.Email = email;
            this.Ischeck = ischeck;
            this.Locked = locked;
            this.Newpm = newpm;
            this.Newpmcount = newpmcount;
            this.Onlinestate = onlinestate;
            this.Invisible = invisible;
            this.Showemail = showemail;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value.Trim(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value.Trim(); }
        }

        public string Safecode
        {
            get { return safecode; }
            set { safecode = value.Trim(); }
        }

        public int Spaceid
        {
            get { return spaceid; }
            set { spaceid = value; }
        }

        public byte Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public int Adminid
        {
            get { return adminid; }
            set { adminid = value; }
        }

        public string Admingroupname
        {
            get { return admingroupname; }
            set { admingroupname = value.Trim(); }
        }

        public int Groupid
        {
            get { return groupid; }
            set { groupid = value; }
        }

        public string Groupname
        {
            get { return groupname; }
            set { groupname = value.Trim(); }
        }

        public string Extgroupids
        {
            get { return extgroupids; }
            set { extgroupids = value.Trim(); }
        }

        public string Regip
        {
            get { return regip; }
            set { regip = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Loginip
        {
            get { return loginip; }
            set { loginip = value.Trim(); }
        }

        public DateTime Logintime
        {
            get { return logintime; }
            set { logintime = value; }
        }

        public DateTime Lastaction
        {
            get { return lastaction; }
            set { lastaction = value; }
        }

        public decimal Money
        {
            get { return money; }
            set { money = value; }
        }

        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        public int Extcredits1
        {
            get { return extcredits1; }
            set { extcredits1 = value; }
        }

        public int Extcredits2
        {
            get { return extcredits2; }
            set { extcredits2 = value; }
        }

        public int Extcredits3
        {
            get { return extcredits3; }
            set { extcredits3 = value; }
        }

        public int Extcredits4
        {
            get { return extcredits4; }
            set { extcredits4 = value; }
        }

        public int Extcredits5
        {
            get { return extcredits5; }
            set { extcredits5 = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value.Trim(); }
        }

        public byte Ischeck
        {
            get { return ischeck; }
            set { ischeck = value; }
        }

        public byte Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        public byte Newpm
        {
            get { return newpm; }
            set { newpm = value; }
        }

        public int Newpmcount
        {
            get { return newpmcount; }
            set { newpmcount = value; }
        }

        public byte Onlinestate
        {
            get { return onlinestate; }
            set { onlinestate = value; }
        }

        public byte Invisible
        {
            get { return invisible; }
            set { invisible = value; }
        }

        public byte Showemail
        {
            get { return showemail; }
            set { showemail = value; }
        }
        #endregion

        #region 副本
        public UserInfo Clone()
        {
            return (UserInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public class LikesetInfo
    {
        private int uid = 0;
        private string systemstyle = "grey";
        private int managelistcount = 20;
        private int msgtip = 1;
        private int overlay = 60;
        private int fastmenucount = 15;
        private string popwinbgcolor = "white";

        public string Popwinbgcolor
        {
            get { return popwinbgcolor; }
            set { popwinbgcolor = value; }
        }

        public int Fastmenucount
        {
            get { return fastmenucount; }
            set { fastmenucount = value; }
        }

        public int Overlay
        {
            get { return overlay; }
            set { overlay = value; }
        }

        public int Msgtip
        {
            get { return msgtip; }
            set { msgtip = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Systemstyle
        {
            get { return systemstyle; }
            set { systemstyle = value; }
        }

        public int Managelistcount
        {
            get { return managelistcount; }
            set { managelistcount = value; }
        }
    }
}
