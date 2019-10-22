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
    /// 文本框控件
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:TextBox runat=server></{0}:TextBox>"), Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        /// <summary>
        /// RequiredFieldValidator控件变量
        /// </summary>
        protected System.Web.UI.WebControls.RequiredFieldValidator CanBeNullRFV = new RequiredFieldValidator();

        /// <summary>
        /// RegularExpressionValidator控件变量
        /// </summary>
        protected System.Web.UI.WebControls.RegularExpressionValidator RequiredFieldTypeREV = new RegularExpressionValidator();

        /// <summary>
        /// RangeValidator控件变量
        /// </summary>
        protected System.Web.UI.WebControls.RangeValidator NumberRV = new System.Web.UI.WebControls.RangeValidator();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TextBox()
            : base()
        {
            base.Attributes.Add("onfocus", "this.className='txt_focus';");
            base.Attributes.Add("onblur", "this.className='txt';");
            base.CssClass = "txt";
            this.AlignX = "center";
            this.AlignY = "top";
            //base.BorderStyle = BorderStyle.Dotted;
            //base.BorderWidth = 1;
        }

        /// <summary>
        /// 添加属性方法
        /// </summary>
        /// <param name="key">键值,如class等</param>
        /// <param name="valuestr">要绑定的字符串</param>
        public void AddAttributes(string key, string valuestr)
        {
            this.Attributes.Add(key, valuestr);
        }

        /// <summary>
        /// 创建子控件
        /// </summary>
        protected override void CreateChildControls()
        {
            //当指定了输入框的最小或最大值时,则加入校验范围项
            if (this.MaximumValue != null || this.MinimumValue != null)
            {
                NumberRV.ControlToValidate = this.ID;
                NumberRV.Type = ValidationDataType.Double;
                NumberRV.CssClass = ErrCssClass;
                if (this.MaximumValue != null && this.MinimumValue != null)
                {
                    NumberRV.MaximumValue = this.MaximumValue;
                    NumberRV.MinimumValue = this.MinimumValue;
                    NumberRV.ErrorMessage = "当前输入数据应在" + this.MinimumValue + "和" + this.MaximumValue + "之间!";
                }
                else
                {
                    if (this.MaximumValue != null)
                    {
                        NumberRV.MaximumValue = this.MaximumValue;
                        NumberRV.MinimumValue = Int32.MinValue.ToString();
                        NumberRV.ErrorMessage = "当前输入数据允许最大值为" + this.MaximumValue;
                    }
                    if (this.MinimumValue != null)
                    {
                        NumberRV.MinimumValue = this.MinimumValue;
                        NumberRV.MaximumValue = Int32.MaxValue.ToString();
                        NumberRV.ErrorMessage = "当前输入数据允许最小值为" + this.MinimumValue;
                    }
                }
                NumberRV.Display = ValidatorDisplay.Static;
                this.Controls.AddAt(0, NumberRV);
            }

            if ((RequiredFieldType != null) && (RequiredFieldType != "") && (RequiredFieldType != "暂无校验"))
            {
                RequiredFieldTypeREV.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                RequiredFieldTypeREV.ControlToValidate = this.ID;
                RequiredFieldTypeREV.CssClass = ErrCssClass;
                switch (RequiredFieldType)
                {
                    case "数据校验":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : "^[-]?\\d+[.]?\\d*$";
                            RequiredFieldTypeREV.ErrorMessage = "数字的格式不正确"; break;
                        }
                    case "电子邮箱":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : (@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                            RequiredFieldTypeREV.ErrorMessage = "邮箱的格式不正确"; break;
                        }
                    case "移动手机":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : "\\d{11}";
                            RequiredFieldTypeREV.ErrorMessage = "手机的位数应为11位!"; break;
                        }
                    case "家用电话":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : "((\\(\\d{3}\\) ?)|(\\d{3}-))?\\d{3}-\\d{4}|((\\(\\d{3}\\) ?)|(\\d{4}-))?\\d{4}-\\d{4}";
                            RequiredFieldTypeREV.ErrorMessage = "请依 (XXX)XXX-XXXX 格式或 (XXX)XXXX-XXXX 输入电话号码！"; break;
                        }
                    case "身份证号码":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : "^\\d{15}$|^\\d{18}$";
                            RequiredFieldTypeREV.ErrorMessage = "请依15或18位数据的身份证号！"; break;
                        }
                    case "网页地址":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的网址"; break;
                        }
                    case "日期":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的日期,如:2006-1-1"; break;
                        }
                    case "日期时间":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的日期,如: 2006-1-1 23:59:59"; break;
                        }
                    case "金额":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : "^([0-9]|[0-9].[0-9]{0-2}|[1-9][0-9]*.[0-9]{0,2})$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的金额"; break;
                        }
                    case "IP地址":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的IP地址"; break;
                        }
                    case "IP地址带端口":
                        {
                            RequiredFieldTypeREV.ValidationExpression = (this.ValidationExpression != null) ? this.ValidationExpression : @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9]):\d{1,5}?$";
                            RequiredFieldTypeREV.ErrorMessage = "请输入正确的带端口的IP地址"; break;
                        }
                    case "自定义":
                        {
                            if (string.IsNullOrEmpty(this.ValidationExpression)) break;
                            RequiredFieldTypeREV.ValidationExpression = this.ValidationExpression;
                            RequiredFieldTypeREV.ErrorMessage = string.IsNullOrEmpty(ErrorMsg) ? "输入格式有误" : ErrorMsg; break;
                        }
                }
                this.Controls.AddAt(0, RequiredFieldTypeREV);
            }

            switch (CanBeNull)
            {
                case "可为空": { break; }
                case "必填":
                    {
                        CanBeNullRFV.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
                        CanBeNullRFV.ControlToValidate = this.ID;
                        CanBeNullRFV.ErrorMessage = "<font color=red>请务必输入内容!</font>";
                        this.Controls.AddAt(0, CanBeNullRFV);
                        break;
                    }
                default: { break; }
            }

        }

        /// <summary>
        /// 获取焦点的控件ID(如提交按钮等)
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
                    this.Attributes.Add("onkeydown", "if(event.keyCode==13){document.getElementById('" + value + "').focus();}");
                }
            }
        }

        /// <summary>
        /// 用户自定义错误提示信息
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ErrorMsg
        {
            get
            {
                object o = ViewState["UserErrorMsg"];
                if (o == null || o.ToString().Trim() == "")
                {
                    return null;
                }
                else
                {
                    return o.ToString().ToLower();
                }
            }
            set
            {
                ViewState["UserErrorMsg"] = value;
            }
        }
        /// <summary>
        /// 设置错误提示的样式名
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("txterror")]
        public string ErrCssClass
        {
            get
            {
                object o = ViewState["ErrCssClass"];
                if (o == null || o.ToString().Trim() == "")
                {
                    return "txterror";
                }
                else
                {
                    return o.ToString().ToLower();
                }
            }
            set
            {
                ViewState["ErrCssClass"] = value;
            }
        }

        /// <summary>
        /// 控件的最大长度属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public override int MaxLength
        {
            get
            {
                object o = ViewState["TextBox_MaxLength"];
                if (o != null)
                {
                    int maxlength = Utils.StrToInt(o.ToString(), 4);
                    AddAttributes("maxlength", maxlength.ToString());
                    return maxlength;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                ViewState["TextBox_MaxLength"] = value;
                AddAttributes("maxlength", value.ToString());
            }
        }


        /// <summary>
        /// 控件的TextMode属性
        /// </summary>
        [Bindable(false), Category("Behavior"), DefaultValue(TextBoxMode.SingleLine), Description("要滚动的对象。")]
        public override TextBoxMode TextMode
        {
            get
            {
                return base.TextMode;
            }
            set
            {
                if (value == TextBoxMode.MultiLine)
                {
                    base.Attributes.Add("onkeyup", "return isMaxLen(this)");
                }

                base.TextMode = value;
            }
        }

        /// <summary>
        /// 要进行校验的表达式
        /// </summary>
        [Bindable(false), Category("Behavior"), DefaultValue(""), TypeConverter(typeof(RequiredFieldTypeControlsConverter)), Description("要滚动的对象。")]
        public string RequiredFieldType
        {
            get
            {
                object o = ViewState["RequiredFieldType"];
                return (o == null) ? "" : o.ToString();
            }
            set
            {
                ViewState["RequiredFieldType"] = value;
            }
        }


        /// <summary>
        /// 要表达式是否可以为空
        /// </summary>
        [Bindable(false), Category("Behavior"), DefaultValue("可为空"), TypeConverter(typeof(CanBeNullControlsConverter)), Description("要滚动的对象。")]
        public string CanBeNull
        {
            get
            {
                object o = ViewState["CanBeNull"];
                return (o == null) ? "" : o.ToString();
            }
            set
            {
                ViewState["CanBeNull"] = value;
            }
        }


        /// <summary>
        /// 是否进行 ' 号替换
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsReplaceInvertedComma
        {
            get
            {
                object o = ViewState["IsReplaceInvertedComma"];
                if (o == null || o.ToString().Trim() == "")
                {
                    return false;
                }
                else
                {
                    return o.ToString().ToLower() == "true" ? true : false;
                }
            }
            set
            {
                ViewState["IsReplaceInvertedComma"] = value;
            }
        }


        /// <summary>
        /// 有效校验表达式
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ValidationExpression
        {
            get
            {
                object o = ViewState["ValidationExpression"];
                if (o == null || o.ToString().Trim() == "")
                {
                    return null;
                }
                else
                {
                    return o.ToString().ToLower();
                }
            }
            set
            {
                ViewState["ValidationExpression"] = value;
            }
        }

        /// <summary>
        /// 文本内容属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public override string Text
        {
            get
            {

                //进行强制日期格式的转换
                if (this.RequiredFieldType == "日期")
                {
                    try
                    {
                        return DateTime.Parse(base.Text).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        return DateTime.Now.ToString("yyyy-MM-dd"); ;
                    }
                }

                //进行强制日期时间格式的转换
                if (this.RequiredFieldType == "日期时间")
                {
                    try
                    {
                        return DateTime.Parse(base.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    catch
                    {
                        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    return IsReplaceInvertedComma ? base.Text.Replace("'", "''").Trim() : base.Text.Trim();
                }
            }
            set
            {
                //进行强制日期格式的转换
                if (this.RequiredFieldType.IndexOf("日期") >= 0)
                {
                    try
                    {
                        base.Text = DateTime.Parse(value).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        base.Text = "";
                    }
                }

                //进行强制日期时间格式的转换
                if (this.RequiredFieldType.IndexOf("日期时间") >= 0)
                {
                    try
                    {
                        base.Text = DateTime.Parse(value).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    catch
                    {
                        base.Text = "";
                    }
                }
                else
                {
                    base.Text = value;
                }
            }

        }

        /// <summary>
        /// 列数属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(30)]
        public int Cols
        {
            get
            {
                return base.Columns;
            }
            set
            {
                base.Columns = value;
            }
        }

        private int _size = 30;
        /// <summary>
        /// 宽度属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(30)]
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }


        private string _maximumValue = null;
        /// <summary>
        /// 最小值属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        public string MaximumValue
        {
            get
            {
                return _maximumValue;
            }
            set
            {
                _maximumValue = value;
            }
        }

        private string _minimumValue = null;
        /// <summary>
        /// 最大值属性
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        public string MinimumValue
        {
            get
            {
                return _minimumValue;
            }
            set
            {
                _minimumValue = value;
            }
        }

        private string _wrapper = "";
        /// <summary>
        /// 外包含,#代表双引号
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Wrapper
        {
            get
            {
                return _wrapper.Replace("#","\"");
            }
            set
            {
                _wrapper = value;
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
        [Bindable(true), Category("Behavior"), DefaultValue("yellowsimple")]
        public string HelpCssClass
        {
            get
            {
                if (_helpCssClass == null || _helpCssClass == string.Empty)
                    return "yellowsimple";
                return base.CssClass;
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
                output.Write("<script type=\"text/javascript\">$(function(){$('#" + this.UniqueID + "').poshytip({className: 'tip-" + this.HelpCssClass + "',alignTo:'target',alignX: '" + this.AlignX + "',alignY: '" + this.AlignY + "', offsetX: " + this.OffsetX + ",offsetY:" + this.OffsetY + " });});</script>");
            }
            //当为TextArea时，maxlength属性可能失效，所以采用js进行长度限制
            if (this.TextMode == TextBoxMode.MultiLine)
            {
                output.WriteLine("<script type=\"text/javascript\">");
                output.WriteLine("function isMaxLen(o){");
                output.WriteLine("var nMaxLen=o.getAttribute? parseInt(o.getAttribute(\"maxlength\")):\"\";");
                output.WriteLine(" if(o.getAttribute && o.value.length>nMaxLen){");
                output.WriteLine(" o.value=o.value.substring(0,nMaxLen)");
                output.WriteLine("}}</script>");

                this.AddAttributes("rows", Rows.ToString());
                this.AddAttributes("cols", Cols.ToString());
                this.Attributes.Add("onfocus", "this.className='txt_focus';");
                this.Attributes.Add("onblur", "this.className='txt';");
                this.Attributes.Add("class", "FormBase");
            }
            else if (this.TextMode == TextBoxMode.Password)
            {
                this.AddAttributes("value", this.Text);
            }
            else
            {
                if (this.Size > 0)
                {
                    this.AddAttributes("size", this.Size.ToString());
                }
            }
            if (this.Wrapper.Trim() != string.Empty)
            {
                output.WriteBeginTag("span " + this.Wrapper + ">");
            }
            this.ToolTip = HelpText;
            base.Render(output);
            RenderChildren(output);
            if (this.Wrapper.Trim() != string.Empty)
            {
                output.WriteEndTag("span");
            }
        }

    }

    /// <summary>
    /// 下拉列表选项转换器
    /// </summary>
    public class RequiredFieldTypeControlsConverter : StringConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RequiredFieldTypeControlsConverter() { }

        /// <summary>
        /// 说明要用下拉列表编辑属性 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 获取标准值列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList controlsArray = new ArrayList();
            controlsArray.Add("暂无校验");
            controlsArray.Add("数据校验");
            controlsArray.Add("电子邮箱");
            controlsArray.Add("移动手机");
            controlsArray.Add("家用电话");
            controlsArray.Add("身份证号码");
            controlsArray.Add("网页地址");
            controlsArray.Add("日期");
            controlsArray.Add("日期时间");
            controlsArray.Add("金额");
            controlsArray.Add("IP地址");
            controlsArray.Add("IP地址带端口");
            controlsArray.Add("自定义");
            return new StandardValuesCollection(controlsArray);

        }

        /// <summary>
        /// return ture的话只能选,return flase可选可填 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }


    /// <summary>
    /// 下拉列表选项转换器
    /// </summary>
    public class CanBeNullControlsConverter : StringConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CanBeNullControlsConverter() { }

        /// <summary>
        /// 下拉列表编辑属性
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 获取标准值列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList controlsArray = new ArrayList();
            controlsArray.Add("可为空");
            controlsArray.Add("必填");

            return new StandardValuesCollection(controlsArray);

        }

        /// <summary>
        /// return ture的话只能选,return flase可选可填 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }

    /// <summary>
    /// 下拉列表选项转换器
    /// </summary>
    public class FormControlsConverter : StringConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormControlsConverter()
        {
        }

        /// <summary>
        /// 下拉列表编辑属性 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        /// <summary>
        /// 获取标准值列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ControlCollection Controls = ((Page)context.Container.Components[0]).Controls;
            ArrayList controlsArray = new ArrayList();
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i] is HtmlTable
                    || Controls[i] is HtmlForm
                    || Controls[i] is HtmlGenericControl
                    || Controls[i] is HtmlImage
                    || Controls[i] is Label
                    || Controls[i] is DataGrid
                    || Controls[i] is DataList
                    || Controls[i] is Table
                    || Controls[i] is Repeater
                    || Controls[i] is Image
                    || Controls[i] is Panel
                    || Controls[i] is PlaceHolder
                    || Controls[i] is Calendar
                    || Controls[i] is AdRotator
                    || Controls[i] is Xml
                    ))
                {
                    controlsArray.Add(Controls[i].ClientID);
                }
            }
            return new StandardValuesCollection(controlsArray);

        }

        /// <summary>
        /// return ture的话只能选,return flase可选可填 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }



}
