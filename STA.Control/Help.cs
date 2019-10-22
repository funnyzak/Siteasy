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
    [DefaultProperty("Text"), ValidationProperty("SelectedItem")]
    public class Help : WebControl
    {
        public Help()
            : base()
        {

        }

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

        private string _imgUrl;
        [Bindable(true), Category("Appearance"), DefaultValue("../images/icon/help.png")]
        public string ImgUrl
        {
            get
            {
                if (_imgUrl == null || _imgUrl.Trim() == string.Empty)
                    return "../images/icon/help.png";
                return _imgUrl;
            }
            set
            {
                _imgUrl = value;
            }
        }

        /// <summary> 
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            output.Write(base.GetHelpScript());
            output.Write("<img src=\"" + this.ImgUrl + "\" id=\"" + this.UniqueID + "\" style=\"border: 0; cursor: pointer; margin: 0 2px 0 0;\" title=\"" + (this.HelpText == string.Empty ? this.Text : this.HelpText) + "\" />" + this.Text);
        }

    }
}
