using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

using STA.Common;

namespace STA.Control
{
    /// <summary>
    /// WebControl接口
    /// </summary>
    public interface IWebControl
    {

    }

    /// <summary>
    /// STA WebControl 基类
    /// </summary>
    public class WebControl : System.Web.UI.WebControls.WebControl, IWebControl
    {
        public WebControl()
        {

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

        protected string GetHelpScript()
        {
            return "<script type=\"text/javascript\">$(function(){$('#" + this.UniqueID + "').poshytip({className: 'tip-" + this.HelpCssClass + "',alignTo:'target',alignX: '" + this.AlignX + "',alignY: '" + this.AlignY + "', offsetX: " + this.OffsetX + ",offsetY:" + this.OffsetY + " });});</script>";
        }
    }
}