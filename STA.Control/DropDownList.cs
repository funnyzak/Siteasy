using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using STA.Common;
using STA.Data;

namespace STA.Control
{
    /// <summary>
    /// 下拉列表控件。
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList, IPostBackDataHandler
    {

        public void AddTableData(DataTable dt, string textName, string valueName)
        {
            this.AddTableData(dt, textName, valueName, "请选择,0");
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
                this.Items.Add(new ListItem(r[textName].ToString().Trim(), r[valueName].ToString().Trim()));
            }
            this.DataBind();
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

        public override string ID
        {
            get
            {
                if (base.ID == null || base.ID.Trim() == string.Empty)
                    return "id" + Rand.Number(17);
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        private string _helpCssClass;
        [Bindable(false), Category("Behavior"), DefaultValue("yellowsimple")]
        public string HelpCssClass
        {
            get
            {
                if (_helpCssClass == null || _helpCssClass == string.Empty)
                    return "yellowsimple";
                return _helpCssClass;
            }
            set
            {
                _helpCssClass = value;
            }
        }

        /// <summary>
        /// 帮助显示内容
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HelpText
        {
            get
            {
                return ViewState["helptext"] == null ? "" : ViewState["helptext"].ToString();
            }
            set
            {
                ViewState["helptext"] = value;
            }
        }

        private string _alignX;
        [Bindable(true), Category("Appearance"), DefaultValue("right")]
        public string AlignX
        {
            get
            {
                if (_alignX == null || _alignX.Trim() == string.Empty)
                    return "right";
                return _alignX;
            }
            set
            {
                _alignX = value;
            }
        }

        private string _alignY;
        [Bindable(true), Category("Appearance"), DefaultValue("center")]
        public string AlignY
        {
            get
            {
                if (_alignY == null || _alignY.Trim() == string.Empty)
                    return "center";
                return _alignY;
            }
            set
            {
                _alignY = value;
            }
        }

        private int _offsetY;
        [Bindable(true), Category("Appearance"), DefaultValue(5)]
        public int OffsetY
        {
            get
            {
                if (_offsetY <= 0)
                    return 5;
                return _offsetY;
            }
            set
            {
                _offsetY = value;
            }
        }

        private int _offsetX;
        [Bindable(true), Category("Appearance"), DefaultValue(5)]
        public int OffsetX
        {
            get
            {
                if (_offsetX <= 0)
                    return 5;
                return _offsetX;
            }
            set
            {
                _offsetX = value;
            }
        }

        /// <summary> 
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.HelpText.Length > 0)
            {
                output.Write("<script type=\"text/javascript\">$(function(){$('#" + this.ID + "').poshytip({className: 'tip-" + this.HelpCssClass + "',alignTo:'target',alignX: '" + this.AlignX + "',alignY: '" + this.AlignY + "', offsetX: " + this.OffsetX + ",offsetY:" + this.OffsetY + " });});</script>");
            }
            this.ToolTip = this.HelpText;
            base.Render(output);
        }

    }

}
