using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;

using STA.Common;
using STA.Data;

namespace STA.Control
{
	/// <summary>
    /// 下拉树形框控件。
	/// </summary>
	[DefaultProperty("Text"),ToolboxData("<{0}:DropDownTreeList runat=server></{0}:DropDownTreeList>")]
    public class DropDownTreeList : System.Web.UI.WebControls.DropDownList, IPostBackDataHandler
	{
        ///// <summary>
        ///// 下拉列表框控件变量
        ///// </summary>
        //public System.Web.UI.WebControls.DropDownList this=new System.Web.UI.WebControls.DropDownList();
	

        /// <summary>
        /// 构造函数
        /// </summary>
		public DropDownTreeList(): base()
		{
            //this.BorderStyle = BorderStyle.Groove;
            //this.BorderWidth = 1; 
		}

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="sqlstring">查询字符串</param>
        public void BuildTree(DataTable dt, string textName, string valueName)
		{

            string SelectedType = "0";
            this.SelectedValue = SelectedType;
            //DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sqlstring).Tables[0];

            this.Items.Clear();
            //加载树
            this.Items.Add(new ListItem("请选择     ", "0"));
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
		private string m_parentid="parentid";
		[Bindable(true),Category("Appearance"),DefaultValue("parent")]
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


        #region SQL字符串

        /// <summary>
        /// SQL字符串变量
        /// </summary>
        private string sqltext;


        /// <summary>
        /// SQL字符串属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SqlText
        {
            get
            {
                return sqltext;
            }

            set
            {
                sqltext = value;
            }
        }

        #endregion


        /// <summary>
        /// 当某选项被选中后,获取焦点的控件ID(如提交按钮等)
        /// </summary>
		[Bindable(true),Category("Appearance"),DefaultValue("")] 
		public string SetFocusButtonID
		{
			get
			{
				object o = ViewState[this.ClientID+"_SetFocusButtonID"];
				return (o==null)?"":o.ToString(); 
			}
			set
			{
				ViewState[this.ClientID+"_SetFocusButtonID"] = value;
				if(value!="")
				{
					this.Attributes.Add("onChange","document.getElementById('"+value+"').focus();");
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
