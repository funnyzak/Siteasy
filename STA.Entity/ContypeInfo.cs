using System;

namespace STA.Entity
{
    /// <summary>
    ///Contype的实体。
    /// </summary>
    [Serializable]
    public class ContypeInfo
    {
        #region 变量定义
        private short id = 0;
        private byte open = 0;
        private byte system = 0;
        private string ename = string.Empty;
        private string name = string.Empty;
        private string maintable = string.Empty;
        private string extable = string.Empty;
        private DateTime addtime = DateTime.Now;
        private string fields = string.Empty;
        private string bgaddmod = string.Empty;
        private string bgeditmod = string.Empty;
        private string bglistmod = string.Empty;
        private string addmod = string.Empty;
        private string editmod = string.Empty;
        private string listmod = string.Empty;
        private int orderid = 0;
        #endregion

        #region 构造函数
        public ContypeInfo()
        {
        }

        public ContypeInfo(short id, byte open, byte system, string ename, string name, string maintable, string extable, DateTime addtime, string fields, string bgaddmod, string bgeditmod, string bglistmod, string addmod, string editmod, string listmod)
        {
            this.Id = id;
            this.Open = open;
            this.System = system;
            this.Ename = ename;
            this.Name = name;
            this.Maintable = maintable;
            this.Extable = extable;
            this.Addtime = addtime;
            this.Fields = fields;
            this.Bgaddmod = bgaddmod;
            this.Bgeditmod = bgeditmod;
            this.Bglistmod = bglistmod;
            this.Addmod = addmod;
            this.Editmod = editmod;
            this.Listmod = listmod;
        }
        #endregion

        #region 字段属性
        public short Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }

        public byte Open
        {
            get { return open; }
            set { open = value; }
        }

        public byte System
        {
            get { return system; }
            set { system = value; }
        }

        public string Ename
        {
            get { return ename; }
            set { ename = value.Trim(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public string Maintable
        {
            get { return maintable; }
            set { maintable = value.Trim(); }
        }

        public string Extable
        {
            get { return extable; }
            set { extable = value.Trim(); }
        }

        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        public string Fields
        {
            get { return fields; }
            set { fields = value.Trim(); }
        }

        public string Bgaddmod
        {
            get { return bgaddmod; }
            set { bgaddmod = value.Trim(); }
        }

        public string Bgeditmod
        {
            get { return bgeditmod; }
            set { bgeditmod = value.Trim(); }
        }

        public string Bglistmod
        {
            get { return bglistmod; }
            set { bglistmod = value.Trim(); }
        }

        public string Addmod
        {
            get { return addmod; }
            set { addmod = value.Trim(); }
        }

        public string Editmod
        {
            get { return editmod; }
            set { editmod = value.Trim(); }
        }

        public string Listmod
        {
            get { return listmod; }
            set { listmod = value.Trim(); }
        }
        #endregion

        #region 副本
        public ContypeInfo Clone()
        {
            return (ContypeInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
