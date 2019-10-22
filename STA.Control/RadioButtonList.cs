using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using STA.Common;

namespace STA.Control
{
    /// <summary>
    /// RadioButtonList控件。
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:RadioButtonList runat=server></{0}:RadioButtonList>")]
    public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList,  IPostBackDataHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RadioButtonList()
            : base()
        {
            this.BorderWidth = 0;
            this.RepeatColumns = 1;
            this.RepeatDirection = RepeatDirection.Horizontal;
            this.RepeatLayout = RepeatLayout.Flow;
            this.CssClass = "buttonlist";
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
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {

            base.Render(output);
        }
    }
}
