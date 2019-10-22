using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web;

namespace STA.Control
{
    /// <summary>
    /// 列表框控件
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:ListBox runat=server></{0}:ListBox>")]
    public class ListBox : System.Web.UI.WebControls.ListBox
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListBox()
            : base()
        {
            this.SelectionMode = ListSelectionMode.Multiple;
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="textName">显示的名称</param>
        /// <param name="valueName">值</param>
        /// <param name="extra">额外的值如：fSTA,1:alex,2,否则为null</param>
        public void AddTableData(DataTable dt, string textName, string valueName, string extra)
        {
            this.Items.Clear();
            if (!string.IsNullOrEmpty(extra))
            {
                string[] ms = extra.Split(':');
                foreach (string m in ms)
                {
                    string[] mms = m.Split(',');
                    if (mms.Length == 2)
                    {
                        this.Items.Add(new ListItem(mms[0], mms[1]));
                    }
                }
            }
            foreach (DataRow r in dt.Rows)
            {
                this.Items.Add(new ListItem(r[textName].ToString(), r[valueName].ToString()));
            }
            this.DataBind();
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="sqlstring">查询字符串</param>
        public void BuildTree(DataTable dt, string textName, string valueName)
        {

            this.Items.Clear();
            DataRow[] drs = dt.Select(this.ParentID + "=0");

            foreach (DataRow r in drs)
            {
                this.Items.Add(new ListItem(r[textName].ToString(), r[valueName].ToString()));
                string blank = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");
                BindNode(r[valueName].ToString(), dt, blank, textName, valueName);
            }
            this.DataBind();

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
                this.Items.Add(new ListItem(text, nodevalue));
                string blankNode = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;" + blank);
                BindNode(nodevalue, dt, blankNode, textName, valueName);
            }
        }    
        
        /// <summary>
        /// 父字段名称
        /// </summary>
        private string m_parentid = "parentid";
        [Bindable(true), Category("Appearance"), DefaultValue("parentid")]
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
        /// <summary>
        /// 当某选项被选中后,获取焦点的控件ID(如提交按钮等)
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SetFocusButtonID
        {
            get
            {
                object o = ViewState[this.ClientID + "_SetFocusButtonID"];
                return (o == null) ? "" : o.ToString();
            }
            set
            {
                ViewState[this.ClientID + "_SetFocusButtonID"] = value;
                if (value != "")
                {
                    this.Attributes.Add("onChange", "document.getElementById('" + value + "').focus();");
                }
            }
        }


        /// <summary> 
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }

        /// <summary>
        /// 得到所选项的字符串(格式: 1,2,3)
        /// </summary>
        /// <returns></returns>
        public string GetSelectString()
        {
            return GetSelectString(",");
        }

        /// <summary>
        /// 通过ID绑定选项
        /// </summary>
        /// <param name="selectid">批定ID</param>
        public void SetSelectByID(string selectid)
        {
            selectid = "," + selectid + ",";

            for (int i = 0; i < this.Items.Count; i++)
            {

                if (selectid.IndexOf("," + this.Items[i].Value + ",") >= 0) this.Items[i].Selected = true;

            }
            this.DataBind();
        }

        /// <summary>
        /// 得到按指定分割符的选项字符串
        /// </summary>
        /// <param name="split">指定分割符</param>
        /// <returns></returns>
        public string GetSelectString(string split)
        {
            split = split.Trim();
            string result = "";
            foreach (ListItem li in this.Items)
            {
                if (li.Selected)
                {
                    result = result + li.Value + split;
                }
            }
            return result.TrimEnd(split.ToCharArray());
        }
    }
}
