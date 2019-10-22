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
    public partial class editor : UserControl
    {
        public editor()
        {
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return nContent.Value; }
            set { nContent.Value = value; }
        }
    }
}