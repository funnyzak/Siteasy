using System;

namespace STA.Entity
{
    /// <summary>
    ///User的实体。
    /// </summary>
    [Serializable]
    public class OnlineUserInfo
    {
        #region 变量定义
        private int id = 0;
        private int userid = 0;
        private string username = string.Empty;
        private string safecode = string.Empty;
        private string nickname = string.Empty;
        private string password = string.Empty;
        private int adminid = 0;
        private int groupid = 0;
        private string admingroupname = string.Empty;
        private string groupname = string.Empty;
        private string lastsearchtime = "1900-1-1 00:00:00";
        private string ip = "";
        private int expires = 0;
        #endregion

        #region 字段属性

        public int Expires
        {
            get { return expires; }
            set { expires = value; }
        }
        public string Safecode
        {
            get { return safecode; }
            set { safecode = value; }
        }
        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Lastsearchtime
        {
            get { return lastsearchtime; }
            set { lastsearchtime = value; }
        }

        public string Groupname
        {
            get { return groupname; }
            set { groupname = value; }
        }
        public string Admingroupname
        {
            get { return admingroupname; }
            set { admingroupname = value; }
        }
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

        public int Adminid
        {
            get { return adminid; }
            set { adminid = value; }
        }

        public int Groupid
        {
            get { return groupid; }
            set { groupid = value; }
        }

        #endregion

    }
}
