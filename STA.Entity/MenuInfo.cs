using System;

namespace STA.Entity
{
    /// <summary>
    ///Menu的实体。
    /// </summary>
    [Serializable]
    public class MenuInfo
    {
        #region 变量定义
        private int id = 0;
        private string name = string.Empty;
        private int parentid = 0;
        private byte system = 0;
        private byte type = 1;
        private PageType pagetype = PageType.菜单页;
        private string icon = string.Empty;
        private string url = string.Empty;
        private string target = string.Empty;
        private int orderid = 0;
        private string identify = "";
        #endregion

        #region 构造函数
        public MenuInfo()
        {
        }

        public MenuInfo(int id, string name, int parentid, byte system, byte type, string icon, string url, string target, int orderid)
        {
            this.Id = id;
            this.Name = name;
            this.Parentid = parentid;
            this.System = system;
            this.Type = type;
            this.Icon = icon;
            this.Url = url;
            this.Target = target;
            this.Orderid = orderid;
        }
        #endregion

        #region 字段属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Identify
        {
            get { return identify; }
            set { identify = value; }
        }

        public PageType Pagetype
        {
            get { return pagetype; }
            set { pagetype = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public int Parentid
        {
            get { return parentid; }
            set { parentid = value; }
        }

        public byte System
        {
            get { return system; }
            set { system = value; }
        }

        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Icon
        {
            get { return icon; }
            set { icon = value.Trim(); }
        }

        public string Url
        {
            get { return url; }
            set { url = value.Trim(); }
        }

        public string Target
        {
            get { return target; }
            set { target = value.Trim(); }
        }

        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }
        #endregion

        #region 副本
        public MenuInfo Clone()
        {
            return (MenuInfo)this.MemberwiseClone();
        }
        #endregion
    }

    public enum PageType
    {
        菜单页 = 1, 流程页 = 2, 对话框 = 3
    }
}
