using System;

namespace STA.Config
{
    /// <summary>
    ///  基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        #region 私有字段
        private string m_webname = "STA"; // 名称
        private string m_webtitle = "Siteasy CMS 内容管理系统"; //网站标题
        private string m_weburl = ""; //网站url地址
        private string m_keywords = string.Empty; //站点默认关键字
        private string m_description = string.Empty; //站点默认描述
        private string m_icp = ""; //网站备案信息
        private string m_extcode = ""; //外部接口(脚本)代码
        private string m_adminmail = "";

        private int m_dynamiced = 1; //网站访问模式 默认动态
        private string m_weblang = "zh-CN";//网站语言
        private int m_opentran = 0;//开启网站翻译器
        private int m_withweburl = 1; //网站所有内容生成的超链接是否加网站地址前缀
        private string m_rewritesuffix = ".aspx";
        private string m_domaincookie = ""; //cookie域
        private int m_listinfocount = 20; //信息页每页默认条数
        private string m_indexlinkname = ""; //主页链接名、用在页面导航中
        private string m_locationseparator = ""; //页面导航分隔符
        private int m_adminlogs = 1; //开启管理日志
        private string m_htmlsavepath = "/html"; //静态html默认保存路径
        private int m_closed = 0; //网站关闭
        private string m_closedreason = ""; //网站关闭提示信息
        private string m_templatesavedirname = "tpl";//模板文件目录名称
        private string m_suffix = "html";
        private int m_debug = 1; //显示程序运行信息

        private string m_attachsavepath = "/files"; //上传文件默认保存路径
        private int m_attachsaveway = 0;
        private int m_attachnameway = 0;
        private string m_attachimgtype = "gif,jpg,jpeg,bmp,png,swf";
        private int m_attachimgmaxsize = 2;
        private string m_attachmediatype = "rm,rmvb,mp3,flv,wav,mid,midi,ra,avi,mpg,mpeg,asf,asx,wma,mov";
        private int m_attachmediamaxsize = 10;
        private string m_attachsofttype = "zip,gz,rar,iso,doc,xsl,ppt,wps";
        private int m_attachsoftmaxsize = 10;
        private string m_attachbigfilepath = "/z";
        private string m_thumbsize = "240,180";

        //水印配置
        private int m_watertype = 1;
        private int m_waterposition = 9;
        private int m_wateropacity = 60;
        private string m_waterlimitsize = "200*200";
        private string m_waterimg = "";
        private int m_waterfontsize = 13;
        private string m_waterfontname = "";
        private string m_watertext = "";
        private int m_waterquality = 80;

        private string m_templatename = "default";

        //性能
        private int m_cacheinterval = 1440;
        private int m_htmlcompress = 0;
        private int m_searchcachetime = 30;
        private int m_searchinterval = 0;
        private int m_contentpage = 1;
        private int m_updateclick = 1;
        private int m_opensearch = 1;
        private int m_opencomment = 1;
        private int m_Reflushinterval = 0;
        private string m_Forbidswords = "";

        //安全
        private string m_Vcodemods = "";

        //互动
        private string m_antispamreplacement; //替换特殊字符
        private int m_commentverify;   //是否验证
        private int m_commentinterval; //评论时间间隔
        private int m_commentlength; //评论长度
        private int m_commentfloor = 30; //评论最大嵌套
        private int m_commentlogin = 0;//是否只能登陆访客投票

        //采集选项
        private int m_colautolink = 1;//是否自动加入内联
        private string m_colclickrange = "0"; //默认点击范围 0,100
        private int m_colseorate = 3;  //seo频率
        private int m_colfilestorage = 0; //采集文件是否入库
        private string m_colseocontent = ""; //seo内容
        private string m_colseolinks = ""; //关键词内联 存在的关键词加链接
        private int m_coltitrplopen = 0;
        private string m_coltitreplace = "";//标题同义词替换
        private int m_coltititpos = 0;//标题随机插入关键词位置 0关闭 1前面 2后面 3随机
        private string m_coltitkeywords = ""; //标题随机关键词列表

        //用户注册
        private int m_openreg = 1;
        private int m_emailmultuser = 0;
        private string m_forbiduserwords = "";
        private int m_userverifyway = 2;
        private string m_userverifyemailcontent = "";

        //访问控制
        private string m_ipdenyaccess = ""; //IP禁止访问列表
        private string m_ipaccess = ""; //IP访问列表
        private string m_adminipaccess = ""; //管理员后台IP访问列表

        //其他选项
        private int m_sitemapconcount = 100; //默认生成Sitemap的内容数量
        private int m_rssconcount = 50;//RSS单个栏目生成的内容数量
        private string m_ordernorule = "";//订单号生成规则
        private int m_invoicerate = 0; //发票费率
        private int m_orderbackday = 0;//订单过期时间
        private string m_tranPluginNameSpace; //翻译插件名空间
        private string m_tranDllFileName;  //翻译插件所在的DLL名称

        //第三方集成
        private int m_thirducenter = 0; //是否开启ucenter
        #endregion

        #region 属性

        #region 站点信息
        public string Webname
        {
            get { return m_webname; }
            set { m_webname = value; }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Webtitle
        {
            get { return m_webtitle; }
            set { m_webtitle = value; }
        }

        public string Extcode
        {
            get { return m_extcode; }
            set { m_extcode = value; }
        }

        public string Keywords
        {
            get { return m_keywords; }
            set { m_keywords = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        /// <summary>
        ///  网站url地址
        /// </summary>
        public string Weburl
        {
            get { return m_weburl; }
            set { m_weburl = value; }
        }

        /// <summary>
        /// 网站备案信息
        /// </summary>
        public string Icp
        {
            get { return m_icp; }
            set { m_icp = value; }
        }

        public string Adminmail
        {
            get { return m_adminmail; }
            set { m_adminmail = value; }
        }
        #endregion

        #region 基本配置

        public string Weblang
        {
            get { return m_weblang; }
            set { m_weblang = value; }
        }

        public int Opentran
        {
            get { return m_opentran; }
            set { m_opentran = value; }
        }

        public string Templatename
        {
            get { return m_templatename; }
            set { m_templatename = value; }
        }

        public int Withweburl
        {
            get { return m_withweburl; }
            set { m_withweburl = value; }
        }

        public int Debug
        {
            get { return m_debug; }
            set { m_debug = value; }
        }

        public string Rewritesuffix
        {
            get { return m_rewritesuffix; }
            set { m_rewritesuffix = value; }
        }

        public string Templatesavedirname
        {
            get { return m_templatesavedirname; }
            set { m_templatesavedirname = value; }
        }

        public string Suffix
        {
            get { return m_suffix; }
            set { m_suffix = value; }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public int Closed
        {
            get { return m_closed; }
            set { m_closed = value; }
        }

        /// <summary>
        ///  关闭提示信息
        /// </summary>
        public string Closedreason
        {
            get { return m_closedreason; }
            set { m_closedreason = value; }
        }

        public string Htmlsavepath
        {
            get { return m_htmlsavepath; }
            set { m_htmlsavepath = value; }
        }

        public int Adminlogs
        {
            get { return m_adminlogs; }
            set { m_adminlogs = value; }
        }

        public string Locationseparator
        {
            get { return m_locationseparator; }
            set { m_locationseparator = value; }
        }

        public string Indexlinkname
        {
            get { return m_indexlinkname; }
            set { m_indexlinkname = value; }
        }

        public int Listinfocount
        {
            get { return m_listinfocount; }
            set { m_listinfocount = value; }
        }
        public string Domaincookie
        {
            get { return m_domaincookie; }
            set { m_domaincookie = value; }
        }
        public int Dynamiced
        {
            get { return m_dynamiced; }
            set { m_dynamiced = value; }
        }

        public int Attachsoftmaxsize
        {
            get { return m_attachsoftmaxsize; }
            set { m_attachsoftmaxsize = value; }
        }

        public string Attachsofttype
        {
            get { return m_attachsofttype; }
            set { m_attachsofttype = value; }
        }
        public int Attachmediamaxsize
        {
            get { return m_attachmediamaxsize; }
            set { m_attachmediamaxsize = value; }
        }
        #endregion

        #region 附件配置
        public string Attachmediatype
        {
            get { return m_attachmediatype; }
            set { m_attachmediatype = value; }
        }

        public string Thumbsize
        {
            get { return m_thumbsize; }
            set { m_thumbsize = value; }
        }

        public string Attachbigfilepath
        {
            get { return m_attachbigfilepath; }
            set { m_attachbigfilepath = value; }
        }

        public int Attachimgmaxsize
        {
            get { return m_attachimgmaxsize; }
            set { m_attachimgmaxsize = value; }
        }

        public string Attachimgtype
        {
            get { return m_attachimgtype; }
            set { m_attachimgtype = value; }
        }
        public int Attachnameway
        {
            get { return m_attachnameway; }
            set { m_attachnameway = value; }
        }
        public int Attachsaveway
        {
            get { return m_attachsaveway; }
            set { m_attachsaveway = value; }
        }
        public string Attachsavepath
        {
            get { return m_attachsavepath; }
            set { m_attachsavepath = value; }
        }
        #endregion

        #region 水印配置
        public int Waterquality
        {
            get { return m_waterquality; }
            set { m_waterquality = value; }
        }

        public string Waterimg
        {
            get { return m_waterimg; }
            set { m_waterimg = value; }
        }

        public string Watertext
        {
            get { return m_watertext; }
            set { m_watertext = value; }
        }

        public string Waterfontname
        {
            get { return m_waterfontname; }
            set { m_waterfontname = value; }
        }

        public int Waterfontsize
        {
            get { return m_waterfontsize; }
            set { m_waterfontsize = value; }
        }
        public string Waterlimitsize
        {
            get { return m_waterlimitsize; }
            set { m_waterlimitsize = value; }
        }
        public int Waterposition
        {
            get { return m_waterposition; }
            set { m_waterposition = value; }
        }

        public int Wateropacity
        {
            get { return m_wateropacity; }
            set { m_wateropacity = value; }
        }

        public int Watertype
        {
            get { return m_watertype; }
            set { m_watertype = value; }
        }
        #endregion

        #region 性能选项

        public int Opensearch
        {
            get { return m_opensearch; }
            set { m_opensearch = value; }
        }

        public int Htmlcompress
        {
            get { return m_htmlcompress; }
            set { m_htmlcompress = value; }
        }

        public int Opencomment
        {
            get { return m_opencomment; }
            set { m_opencomment = value; }
        }

        public int Cacheinterval
        {
            get { return m_cacheinterval; }
            set { m_cacheinterval = value; }
        }

        public int Contentpage
        {
            get { return m_contentpage; }
            set { m_contentpage = value; }
        }

        public int Searchinterval
        {
            get { return m_searchinterval; }
            set { m_searchinterval = value; }
        }


        public int Searchcachetime
        {
            get { return m_searchcachetime; }
            set { m_searchcachetime = value; }
        }

        public int Reflushinterval
        {
            get { return m_Reflushinterval; }
            set { m_Reflushinterval = value; }
        }

        public int Updateclick
        {
            get { return m_updateclick; }
            set { m_updateclick = value; }
        }

        public string Forbidswords
        {
            get { return m_Forbidswords; }
            set { m_Forbidswords = value; }
        }

        #endregion

        #region 安全设置
        public string Vcodemods
        {
            get { return m_Vcodemods; }
            set { m_Vcodemods = value; }
        }

        #endregion

        #region 互动设置
        public int Commentlogin
        {
            get { return m_commentlogin; }
            set { m_commentlogin = value; }
        }
        public int Commentfloor
        {
            get { return m_commentfloor; }
            set { m_commentfloor = value; }
        }
        public int Commentverify
        {
            get { return m_commentverify; }
            set { m_commentverify = value; }
        }
        public int Commentinterval
        {
            get { return m_commentinterval; }
            set { m_commentinterval = value; }
        }
        public string Antispamreplacement
        {
            get { return m_antispamreplacement; }
            set { m_antispamreplacement = value; }
        }
        public int Commentlength
        {
            get { return m_commentlength; }
            set { m_commentlength = value; }
        }

        #endregion

        #region 用户注册
        public int Openreg
        {
            get { return m_openreg; }
            set { m_openreg = value; }
        }

        public int Emailmultuser
        {
            get { return m_emailmultuser; }
            set { m_emailmultuser = value; }
        }

        public string Forbiduserwords
        {
            get { return m_forbiduserwords; }
            set { m_forbiduserwords = value; }
        }

        public int Userverifyway
        {
            get { return m_userverifyway; }
            set { m_userverifyway = value; }
        }

        public string Userverifyemailcontent
        {
            get { return m_userverifyemailcontent; }
            set { m_userverifyemailcontent = value; }
        }
        #endregion

        #region 采集选项
        public int Colautolink
        {
            get { return m_colautolink; }
            set { m_colautolink = value; }
        }

        public int Colfilestorage
        {
            get { return m_colfilestorage; }
            set { m_colfilestorage = value; }
        }

        public string Colclickrange
        {
            get { return m_colclickrange; }
            set { m_colclickrange = value; }
        }
        public int Colseorate
        {
            get { return m_colseorate; }
            set { m_colseorate = value; }
        }
        public string Colseocontent
        {
            get { return m_colseocontent; }
            set { m_colseocontent = value; }
        }
        public string Colseolinks
        {
            get { return m_colseolinks; }
            set { m_colseolinks = value; }
        }

        public int Coltitrplopen
        {
            get { return m_coltitrplopen; }
            set { m_coltitrplopen = value; }
        }
        public string Coltitreplace
        {
            get { return m_coltitreplace; }
            set { m_coltitreplace = value; }
        }
        public int Coltititpos
        {
            get { return m_coltititpos; }
            set { m_coltititpos = value; }
        }
        public string Coltitkeywords
        {
            get { return m_coltitkeywords; }
            set { m_coltitkeywords = value; }
        }
        #endregion

        #region 其他选项
        public int Sitemapconcount
        {
            get { return m_sitemapconcount; }
            set { m_sitemapconcount = value; }
        }

        public int Rssconcount
        {
            get { return m_rssconcount; }
            set { m_rssconcount = value; }
        }

        public string Ordernorule
        {
            get { return m_ordernorule; }
            set { m_ordernorule = value; }
        }

        public int Invoicerate
        {
            get { return m_invoicerate; }
            set { m_invoicerate = value; }
        }

        public int Orderbackday
        {
            get { return m_orderbackday; }
            set { m_orderbackday = value; }
        }
        #endregion

        #region 访问控制
        public string TranPluginNameSpace
        {
            get { return m_tranPluginNameSpace; }
            set { m_tranPluginNameSpace = value; }
        }
        public string TranDllFileName
        {
            get { return m_tranDllFileName; }
            set { m_tranDllFileName = value; }
        }


        /// <summary>
        /// IP禁止访问列表
        /// </summary>
        public string Ipdenyaccess
        {
            get { return m_ipdenyaccess; }
            set { m_ipdenyaccess = value; }
        }

        /// <summary>
        /// IP访问列表
        /// </summary>
        public string Ipaccess
        {
            get { return m_ipaccess; }
            set { m_ipaccess = value; }
        }

        /// <summary>
        /// 管理员后台IP访问列表
        /// </summary>
        public string Adminipaccess
        {
            get { return m_adminipaccess; }
            set { m_adminipaccess = value; }
        }
        #endregion

        #region
        public int Thirducenter
        {
            get { return m_thirducenter; }
            set { m_thirducenter = value; }
        }
        #endregion

        #endregion
    }
}