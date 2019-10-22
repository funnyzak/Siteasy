using System;

namespace STA.Entity
{
    /// <summary>
    ///Useractivation的实体。
    /// </summary>
    [Serializable]
    public class UserauthInfo
    {
        #region 变量定义
        private int id = 0;
        private int userid = 0;
        private string username = string.Empty;
        private string email = string.Empty;
        private DateTime addtime = DateTime.Now;
        private int expirs = 24 * 60 * 3;
        private string code = string.Empty;
        private AuthType atype = AuthType.用户激活;
        #endregion

        #region 字段属性

        public AuthType Atype
        {
            get { return atype; }
            set { atype = value; }
        }

        public int Expirs
        {
            get { return expirs; }
            set { expirs = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

        public string Email
        {
            get { return email; }
            set { email = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value.Trim(); }
        }
        #endregion

        #region 副本
        public UserauthInfo Clone()
        {
            return (UserauthInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum AuthType
    {
        用户激活 = 1, 重置密码 = 2
    }
}
