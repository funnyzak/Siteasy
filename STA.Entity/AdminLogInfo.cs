using System;

namespace STA.Entity
{
    /// <summary>
    ///Adminlog的实体。
    /// </summary>
    [Serializable]
    public class AdminLogInfo
    {
        #region 变量定义
        private int id = 0;
        private int uid = 0;
        private string username = string.Empty;
        private int groupid = 0;
        private string groupname = string.Empty;
        private string ip = string.Empty;
        private DateTime addtime = DateTime.Now;
        private string action = string.Empty;
        private string remark = string.Empty;
        private int admintype = 1;
        #endregion

        #region 构造函数
        public AdminLogInfo()
        {
        }

        public AdminLogInfo(int admintype, int uid, string username, int groupid, string groupname, string ip, DateTime addtime, string action, string remark)
        {
            this.admintype = admintype;
            this.Uid = uid;
            this.Username = username;
            this.Groupid = groupid;
            this.Groupname = groupname;
            this.Ip = ip;
            this.Addtime = addtime;
            this.Action = action;
            this.Remark = remark;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Admintype
        {
            get { return admintype; }
            set { admintype = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
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

        public string Ip
        {
            get { return ip; }
            set { ip = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value.Trim(); }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value.Trim(); }
        }
        #endregion

        #region 副本
        public AdminLogInfo Clone()
        {
            return (AdminLogInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
