using System;

namespace STA.Entity
{
    /// <summary>
    ///Contypefield的实体。
    /// </summary>
    [Serializable]
    public class ContypefieldInfo
    {
        public static readonly string imglistfield = "ext_imgs,ext_style";
        public static readonly string speclistfield = "ext_grouptpl";
        public static readonly string productlistfield = "ext_imgs";
        public static readonly string infolistfield = "ext_nativeplace,ext_infotype"; //ext_endtime,ext_linkman,ext_tel,ext_email,ext_address
        public static readonly string videolistfield = "ext_vfile";
        public static readonly string softlistfield = "ext_softlevel,ext_softtype,ext_language,ext_license,ext_environment,ext_officesite,ext_demourl,ext_filesize,ext_demolink,ext_downcount,ext_downlinks";
        #region 变量定义
        private int id = 0;
        private int cid = 0;
        private string fieldname = string.Empty;
        private string fieldtype = string.Empty;
        private int length = 0;
        private byte isnull = 0;
        private string defvalue = string.Empty;
        private string desctext = string.Empty;
        private int orderid = 0;
        private string vinnertext = string.Empty;
        private string tiptext = string.Empty;
        private string fieldvalue = string.Empty;
        #endregion

        #region 构造函数
        public ContypefieldInfo()
        {

        }

        public ContypefieldInfo(int id, int cid, string fieldname, string fieldtype, int length, byte isnull, string defvalue, string desctext, int orderid, string vinnertext)
        {
            this.Id = id;
            this.Cid = cid;
            this.Fieldname = fieldname;
            this.Fieldtype = fieldtype;
            this.Length = length;
            this.Isnull = isnull;
            this.Defvalue = defvalue;
            this.Desctext = desctext;
            this.Orderid = orderid;
            this.Vinnertext = vinnertext;
        }
        #endregion

        #region 字段属性
        public string Fieldvalue
        {
            get { return fieldvalue; }
            set { fieldvalue = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Tiptext
        {
            get { return tiptext; }
            set { tiptext = value; }
        }
        public int Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public string Fieldname
        {
            get { return fieldname; }
            set { fieldname = value.Trim(); }
        }

        public string Fieldtype
        {
            get { return fieldtype; }
            set { fieldtype = value.Trim(); }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public byte Isnull
        {
            get { return isnull; }
            set { isnull = value; }
        }

        public string Defvalue
        {
            get { return defvalue; }
            set { defvalue = value.Trim(); }
        }

        public string Desctext
        {
            get { return desctext; }
            set { desctext = value.Trim(); }
        }

        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }

        public string Vinnertext
        {
            get { return vinnertext; }
            set { vinnertext = value.Trim(); }
        }
        #endregion

        #region 副本
        public ContypefieldInfo Clone()
        {
            return (ContypefieldInfo)this.MemberwiseClone();
        }
        #endregion
    }
}
