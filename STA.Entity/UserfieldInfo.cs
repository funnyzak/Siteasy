using System;

namespace STA.Entity
{
    /// <summary>
    ///Userfield的实体。
    /// </summary>
    [Serializable]
    public class UserfieldInfo
    {
        #region 变量定义
        private int id = 0;
        private int uid = 0;
        private string realname = string.Empty;
        private string idcard = string.Empty;
        private string signature = string.Empty;
        private string description = string.Empty;
        private int areaid = 0;
        private string areaname = string.Empty;
        private string address = string.Empty;
        private string postcode = string.Empty;
        private string hometel = string.Empty;
        private string worktel = string.Empty;
        private string mobile = string.Empty;
        private string icq = string.Empty;
        private string qq = string.Empty;
        private string skype = string.Empty;
        private string msn = string.Empty;
        private string website = string.Empty;
        private int ucid = 0;
        #endregion

        #region 构造函数
        public UserfieldInfo()
        {
        }

        public UserfieldInfo(int uid, string realname, string idcard, string signature, string description, int areaid, string areaname, string address, string postcode, string hometel, string worktel, string mobile, string icq, string qq, string skype, string msn, string website)
        {
            this.Uid = uid;
            this.Realname = realname;
            this.Idcard = idcard;
            this.Signature = signature;
            this.Description = description;
            this.Areaid = areaid;
            this.Areaname = areaname;
            this.Address = address;
            this.Postcode = postcode;
            this.Hometel = hometel;
            this.Worktel = worktel;
            this.Mobile = mobile;
            this.Icq = icq;
            this.Qq = qq;
            this.Skype = skype;
            this.Msn = msn;
            this.Website = website;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Realname
        {
            get { return realname; }
            set { realname = value.Trim(); }
        }
        public int Ucid
        {
            get { return ucid; }
            set { ucid = value; }
        }
        public string Idcard
        {
            get { return idcard; }
            set { idcard = value.Trim(); }
        }

        public string Signature
        {
            get { return signature; }
            set { signature = value.Trim(); }
        }

        public string Description
        {
            get { return description; }
            set { description = value.Trim(); }
        }

        public int Areaid
        {
            get { return areaid; }
            set { areaid = value; }
        }

        public string Areaname
        {
            get { return areaname; }
            set { areaname = value.Trim(); }
        }

        public string Address
        {
            get { return address; }
            set { address = value.Trim(); }
        }

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value.Trim(); }
        }

        public string Hometel
        {
            get { return hometel; }
            set { hometel = value.Trim(); }
        }

        public string Worktel
        {
            get { return worktel; }
            set { worktel = value.Trim(); }
        }

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value.Trim(); }
        }

        public string Icq
        {
            get { return icq; }
            set { icq = value.Trim(); }
        }

        public string Qq
        {
            get { return qq; }
            set { qq = value.Trim(); }
        }

        public string Skype
        {
            get { return skype; }
            set { skype = value.Trim(); }
        }

        public string Msn
        {
            get { return msn; }
            set { msn = value.Trim(); }
        }

        public string Website
        {
            get { return website; }
            set { website = value.Trim(); }
        }
        #endregion

        #region 副本
        public UserfieldInfo Clone()
        {
            return (UserfieldInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
