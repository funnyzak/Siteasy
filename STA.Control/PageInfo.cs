using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

using STA.Common;

namespace STA.Control
{
    /// <summary>
    /// 提示控件
    /// </summary>
    [DefaultProperty("Text"),ValidationProperty("SelectedItem"), ToolboxData("<{0}:PageInfo runat=server/>")]
    public class PageInfo : CompositeControl
    {
        private string _text;
        /// <summary>
        /// 文本内容
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }


        [Bindable(false), Category("Behavior"), DefaultValue("yellowsimple")]
        public override string ID
        {
            get
            {
                if (base.ID == null || base.ID.Trim() == string.Empty)
                    return "pageinfo" + Rand.Number(17);
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        private string _icon;
        /// <summary>
        /// 帮助显示内容
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Icon
        {
            get
            {
                switch (_icon)
                {
                    case "warn":
                        return "../images/icon/warning.gif";
                    default:
                        return "../images/icon/lightbulb.png";
                }
            }
            set
            {
                _icon = value;
            }
        }

        private string _css;
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Css
        {
            get
            {
                return _css;
            }
            set
            {
                _css = value;
            }
        }

        /// <summary> 
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<div class=\"lightip\" id=\"" + this.ID + "\" style=\"" + this.Css + "\">");
            output.Write("<div class=\"close\" title=\"点击关闭\" onclick=\"document.getElementById('" + this.ID + "').style.display='none';\">x</div>");
            output.Write("<div class=\"msg\" style=\"background-image:url('" + this.Icon + "')\">" + this.Text + "</div></div>");
        }

    }
}
