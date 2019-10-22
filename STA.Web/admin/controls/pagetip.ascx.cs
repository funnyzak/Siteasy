using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace STA.Web.Admin.UserControls
{
    public partial class pagetip : UserControl
    {
        private IconType _icon = IconType.Information;
        private string _text = "";
        private string _style = "";
        public pagetip()
        {
        }

        /// <summary>
        /// 读取/设置图标
        /// </summary>
        public IconType Icon
        {
            get 
            {
                return _icon;
            }
            set 
            {
                switch (value)
                {
                    case IconType.Information:
                        _icon = IconType.Information;
                        break;
                    case IconType.Warning:
                        _icon = IconType.Warning;
                        break;
                }
            }
        }
        public string Style
        {
            get { return _style; }
            set { _style = value; }
        }
        /// <summary>
        /// 读取/设置提示文字
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// 获取图标图片
        /// </summary>
        /// <returns></returns>
        protected string GetInfoImg()
        {
            switch (_icon)
            {
                case IconType.Information:
                    return "../images/icon/lightbulb.png";
                case IconType.Warning:
                    return "../images/icon/warning.gif";
                default:
                    return "../images/icon/lightbulb.png";
            }
        }

        /// <summary>
        /// 图标类型
        /// </summary>
        public enum IconType
        {
            Information, Warning
        }
    }
}