using System.Data;
using System.Web.UI;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace STA.Common
{
    /// <summary>
    /// 通过DataTable列表生成select多级下拉菜单
    /// </summary>
    public class SelectTreeList
    {
        private const string format = "<option value='{0}'{2}>{1}</option>";
        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectTreeList()
        {
            items.Add("0", "请选择");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectTreeList(string id, string selectedvalue)
        {
            items.Add("0", "请选择");
            this.id = id;
            this.selectedValue = selectedvalue;
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="sqlstring">查询字符串</param>
        public string BuildTree(DataTable dt, string textName, string valueName)
        {
            DataRow[] drs = dt.Select(this.ParentID + "=0");

            foreach (DataRow r in drs)
            {
                this.Items.Add(r[valueName].ToString(), r[textName].ToString());
                string blank = HttpUtility.HtmlDecode(this.Blank);
                BindNode(r[valueName].ToString(), dt, blank, textName, valueName);
            }

            string ret = string.Format("<select {0}id='{1}' name='{1}'>", this.Multiple == 1 ? "multiple='multiple' " : "", this.Id);
            foreach (KeyValuePair<string, string> item in items)
            {
                ret += StrFormat(item.Key, item.Value);
            }
            return ret + "</select>";

        }

        /// <summary>
        /// 创建树结点
        /// </summary>
        /// <param name="sonparentid">当前数据项</param>
        /// <param name="dt">数据表</param>
        /// <param name="blank">空白符</param>
        private void BindNode(string sonparentid, DataTable dt, string blank, string textName, string valueName)
        {
            DataRow[] drs = dt.Select(this.ParentID + "=" + sonparentid);

            foreach (DataRow r in drs)
            {
                string nodevalue = r[valueName].ToString();
                string text = r[textName].ToString();
                text = blank + text;
                this.Items.Add(nodevalue, text);
                string blankNode = HttpUtility.HtmlDecode(this.Blank + blank);
                BindNode(nodevalue, dt, blankNode, textName, valueName);
            }
        }

        private string StrFormat(string value, string text)
        {
            return string.Format(format, value, text, value == this.selectedValue ? " selected='selected'" : "");
        }


        private Dictionary<string, string> items = new Dictionary<string, string>();

        public Dictionary<string, string> Items
        {
            get { return items; }
            set { items = value; }
        }

        private string id = Rand.Number(10);

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private int m_multiple = 0;

        public int Multiple
        {
            get { return m_multiple; }
            set { m_multiple = value; }
        }
        private string selectedValue = "0";

        public string SelectedValue
        {
            get { return selectedValue; }
            set { selectedValue = value; }
        }

        /// <summary>
        /// 父字段名称
        /// </summary>
        private string m_parentid = "parentid";
        public string ParentID
        {
            get
            {
                return m_parentid;
            }

            set
            {
                m_parentid = value;
            }
        }

        private string m_blank = "&nbsp;&nbsp;&nbsp;&nbsp;";
        /// <summary>
        /// 下级的占位符
        /// </summary>
        public string Blank
        {
            get { return m_blank; }
            set { m_blank = value; }
        }
    }
}
