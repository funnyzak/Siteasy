# 站易内容管理系统

开发于2013年之前


一些说明：
* 基于vs 2015开发
* 数据库使用SqlServer 2005 Above
* IIS 7.0  4.0 integration


## Template Lan Intro:

全局变量：

字符串变量
seotitle,seokeywords,seodescription 默认seo变量
userid 当前用户ID
meta meta标签,放到页头
link link标签、添加css文件,放到页头
script 脚本,放到页头
errortxt 当前页面的错误提示
location 站点导航
ispost bool值 当前是否POST请求
webname 网站名称
weburl 网站地址
cururl 当前URL

siteurl 网站绝对地址（如网站安装到二级目录）
sitedir 网站安装的目录地址（一般为空）

tempdir 模板目录地址
tempurl 模板绝对地址


oluser 当前在线用户数据
baseconfig 系统配置
config 系统细胞配置


config常用参数详解 //调用如 {config.Webname}

Webname  网站名称
Webtitle  网站seo标题
Extcode 第三方代码
Keywords Seo关键字
Description Seo描述

Weburl  //网站地址
Icp  //网站备案信息
Adminmain  //网站备案信息

Suffix //网站静态后缀 如:.html
Locationseparator //站点导航分隔符
Indexlinkname //站点导航首页名称
Listinfocount //默认列表页每页数量

oluser 当前在线用户数据参数详解
Userid //用户ID
Username //用户名
Adminid //管理组ID
Groupid //用户组ID
Nickname //昵称
Groupname //用户组名称
admingroupname //管理组名称

扩展表字段说明
软件   3

等级	 ext_softlevel	
语言	 ext_language	 
授权方式	 ext_license
运行环境	 ext_environment	
官方网站	 ext_officesite	 
演示地址	 ext_demourl	 
文件大小	 ext_filesize	 
下载地址	 ext_downlinks	 
下次次数	 ext_downcount
类型	 ext_softtype

商品   4

商品编号	 ext_code		
市场价	 ext_price	 
优惠价	 ext_vipprice	
品牌	 ext_brand	
上架时间	 ext_ontime	
库存量	 ext_storage	
计量单位	 ext_unit	
商品重量	 ext_weight	
产品图片	 ext_imgs	
视频地址	 ext_vfile	

分类信息   5 

地区	 ext_nativeplace
信息类型	 ext_infotype
截止日期	 ext_endtime
联系人	 ext_linkman	 
联系电话	 ext_tel	 
电子邮件	 ext_email	 
联系地址	 ext_address

视频

视频地址	 ext_vfile	 
版权方	 ext_copyright	 
推荐星级	 ext_star	 
视频时长	 ext_duration

图集

图片集合	 ext_imgs	
显示风格	 ext_style	


招聘


名称	 字段名	 
职位名称	 ext_position		
所属机构	 ext_company		
学历要求	 ext_education	
工作地区	 ext_nativeplace	
职位性质	 ext_worktype	
性别要求	 ext_gender	 
截止日期	 ext_enddate		
薪资待遇	 ext_wage	
工作经验	 ext_experience	
语言要求	 ext_language	
招聘人数	 ext_headcount




DatabaseProvider.GetInstance().ContentCount() 内容数量,不包括专题


模板变量修饰器：

自定义模版类：
<%inherits STA.Page.album%>

引用命名空间：
<%namespace STA.Config%>

引用页面：
<%include _albumheader%>
<%include user/_albumheader%> 引用绝对路径tpl下user目录下模板

调用广告：
{ad 广告名}

读取URL内容(默认utf-8)：
<%urltext gb2312 (http://www.baidu.com)%>
<%urltext (http://www.baidu.com)%>

获取子频道
<%set {list}=SubChlList(0)%>

所有频道
<%set {list}=Caches.GetChannelTable()%>

是否压缩输出页面
<%set {iscompress}=false%>


获取内容类型
<%set {list}=Caches.GetContypeTable()%>

获取图片缩略图
<%set (string){img}=ImgThumb({item[img]})%>

变量声明调用：
<%set (int){cid}=TypeParse.StrToInt({item[id]})%>

语言翻译
<%set (string){txt} = Translate(info.Content,"en")%>  自动检测翻译为英语

<%set (string){txt} = Translate(webname,"zh-CN","en")%>

<%set (string){ab}=""%>

<%set (string){vstr}=UBB.ParseMedia("flv", 980, 625, true, {info.Ext[ext_vfile]})%>

<%set {list} = SpecConGroup()%> //获取专题内容组 id,name

<%set (ContentInfo){pinfo}=SimpleContent(id)%> //获取内容简单信息 id,title,img,typeid,channelid,filename,savepath

<%set (ContentInfo){pinfo}=PrevCon(true, "prev")%>  //内容上一篇  true 是否只限于本频道

<%set (ContentInfo){pinfo}=PrevCon(true, "")%>  //内容下一篇

<%set (string){ab}=ConUtils.HumanizeTime({d[addtime]})%> //人性化时间

<%set (string){html}=Utils.RemoveLine(Utils.RemoveHtml(item["content"].ToString())).Replace(" ","")%>	//变量去掉HTMl

<%set (string){title}=TitleFormat({item[title]},{item[property]},"#333",82,"..")%>	//格式化内容标题

<%set {list}=SoftList({info.Ext[ext_downlinks]})%>  //调用文档类型为软件的 文件下载列表  id,name,url

<%set {list}=MagazineList({info.Content})%>  //调用杂志内容列表 name,url,orderid,attid

<%set (string){durl}=GetDownloadUrl({item[url]},info.Id)%> //生成文档软件下载地址

<%set {list}=PhotoList({info.Ext[ext_imgs]})%>  //调用文档类型为图片的 图片列表  id,name,url

<%set (SelectInfo){val}=SelectByVal("联动标识","联动值")%>  //根据联动标识、联动值 获取联动text

<%set {list} = SelectSubList("nativeplace",native)%>  //获取联动的下级列表

<%set (SelectInfo){s} = SelectParentNode("联动标识","联动值")%>  //根据联动值获取上级节点  >>select

<%set {list}=RelateConList(内容ID,数量,"字段列表")%>  //根据内容ID获取内容相关列表

<%set {list}=SubChlList(父频道ID)%> //获取子频道列表 

<%set {list}=Contents.GetTagsByCid(内容ID)%> //获取内容标签

<%set {list}=RelateConList(id,count,fields)%> //获取内容 相关内容 

<%set {str}=SelectName(string ename, string value,string split)%> //如果扩展字段为联动字段,根据值获取名称    fields:联动标识(不包括ext_)、联动值  

 <%set (string){title}=TitleFormat({item[title]},{item[property]},(item["color"].ToString().Trim() == "000000"? defcolor: {item[color]}),36,"..")%> //格式化内容标题
 
 获取内容URL
 {Urls.Contnet(id)}
<%set (int){cid}=TypeParse.StrToInt({item[id]})%>
<%set (int){tid}=TypeParse.StrToInt({item[typeid]})%>
<%set (string){curl}=Urls.Content(cid,tid,{item[savepath]},{item[filename]})%>


生成页面导航 1
<%set (string){pguide}=PageNumber("第一页地址,可不填", "地址格式如：list.aspx?page=@page", 1[当前页码], 3[页数], 0[记录数,可填0], 7[页码导航链接数量], false[是否生成select跳转])%> 
生成页面导航 2
<%set (string){pguide}=PageNumber("地址格式如：list.aspx?page=@page", 2[当前页码], 7[页码导航链接数量])%> 
生成页面导航 3
<%set (string){pguide}=PageNumber("地址格式如：list.aspx?page=@page", 7[页码导航链接数量])%> 
生成页面导航 4
<%set (string){pguide}=PageNumber("地址格式如：list.aspx?page=@page")%> 
生成页面导航 5
<%set (string){pguide}=PageNumber()%> 



调用自定义变量:
方法1：{variable[vblname]}
方法2：<%set (string){vbl}=Variable("vblname")%>

当前URL Request变量值:
{request[uid]}

URL编码
<%urlencode(变量)%>;

//加入引用文件
<%csharp%>
AddLinkCss("地址");
AddLinkCss("地址","id");
AddLinkRss("地址","描述"); //订阅
AddScript("脚本代码");
AddLinkScript("脚本地址");
<%/csharp%>

重复内容33次：
<%for("<td>&nbsp;</td>", 33)%>

列表遍历:
<%loop (ModelInfo) info GetList()%>
<%loop subnav datatable%> table遍历
{info.id}
<%/loop%>

<%continue%>
<%break%>


按字节截取字符串
<%getsubstring({abc[dd]},25,"...")%>


条件语句：
<%if {infloat}==0%>

<%else if b=5%>

<%else%>
sdfdsf
<%/if%>


代码:
<%csharp%>
//代码
<%/csharp%>

日期转换:
<%datetostr({item[addtime]}, "yyyy-MM-dd HH:mm")%>

string转int:
{strtoint(变量)};

读取配置变量：
{config.webname}



数据调用：
uid 发布的用户
action: content channel link page tag comment vote

action 为必填项
output 可不填,默认为list
cache 1开启缓存 0关闭缓存  默认开启
order 0时间 1ID 2权重 3点击 4顶 5踩 6评论数  7更新时间 默认按时间
ordertype 1降序 2升序 默认降序

调用内容
action:content
type 有 channel special congroup tag  分别按频道、专题、内容组，标签 调用内容 都必须设置ID，如不设置则调用所有
id 相关ID 当type为channel,id可设置多个
uid 发布内容的用户ID
ctype 内容模型ID （系统模型：0专题(special) 1普通内容(content) 2图片集(photo) 3软件(soft) 4商品(product) 5分类信息(info) 6视频(video))
ext 模型标识，使用此属性 必须type为channel并设置id(调用扩展表字段时使用) 字段typeid已有不要加
type 如何调用
property 头条[h]  推荐[r]  幻灯[f]  特荐[a]  滚动[s]  加粗[b]  斜体[i]  图片[p]  跳转[j]
durdate 多少天内容的内容
self 是否只调用本频道数据  使用此属性 必须type为channel 且 设置id
page 调用第几页
num 调用数量
self 是否只调用本频道内容,不调用扩展频道和子频道内容
fields 所要调用的表字段  id, typeid, typename, channelfamily, channelid, channelname, extchannels, title, subtitle, addtime, updatetime, color, property, adduser, addusername, lastedituser, lasteditusername, author, source, img, url, seotitle, seokeywords, seodescription, savepath, filename, template, content, status, viewgroup, iscomment, ishtml, click, orderid, diggcount, stampcount, commentcount, credits, relates
group 专题内容组ID 配合type为special使用调用  
<%load_data action=content output=nihao type=channel ctype=1 ext=photo uid=2 num=12 id=cid fields=id,name,tilte,ext_date order=5 ordertype=1 group=0 page=12 property=j,f,p self=1 durdate=30%>

 durdate 调用30天内的数据
<%load_data action=content type=channel id=79,343 num=30 uid=1 fields=id,title,img order=0 property=r cache=1%>

<%load_data action=content type=special id=specid num=2 fields=id,title,img order=0 property=r,p cache=1%>

<%load_data action=content type=congroup id=3 num=4 fields=id,typeid,savepath,filename,img,title order=0%>

如果只写action 或output 后面必须留空如：<%load_data action=content %> <%load_data action=content output=23 %>
<%load_data action=content type=channel id=3 num=5 ext=soft ctype=3 fields=id,savepath,filename,title,ext_%>

调用频道
action:channel
id 为父ID  当type为chlgroup id为频道组ID
type chlgroup 调用频道组时使用
order 0时间 1ID 2权重 3点击 4顶 5踩 6评论数  7更新时间 默认按时间
fields  id, typeid, parentid, name, savepath, filename, ctype, img, addtime, seotitle, seokeywords, seodescription, moresite, siteurl, content, ispost, ishidden, orderid, viewgroup, viewcongroup
<%load_data action=channel output=nihao id=2 num=12 fields=id,name order=1%>

<%load_data action=channel output=nihao type=chlgroup id=3 num=12 fields=id,name order=1%>

 <%load_data action=channel type=chlgroup id=2 num=10 fields=id,name order=2%>

调用链接
action:link
fields id, typeid, name, url, logo, email, addtime, description, orderid, status
<%load_data action=link output=nihao num=12 id=32 fields=id,name,tilte order=1%>

调用杂志
action:magazine
fields id, name, addtime, updatetime, likeid, ratio, cover, description, content, pages, orderid, status, click, parms
<%load_data action=magazine output=list num=12 likeid=你 fields=id,name,tilte order=1%>


调用链接类型
<%load_data action=linktype output=nihao num=12 fields=id,name order=1%>

<%sqlstring list select id,name from tbprefix_linktypes order by orderid desc%>

调用单页
action:page
<%load_data action=page output=nihao num=12 likeid=about fields=id,name,tilte order=1%>


调用标签
action:tag
<%load_data action=tag output=nihao num=12 fields=id,name,tilte order=1 ordertype=desc%>
其中order 2是为按内容数排序


调用评论：
action:comment
order 0时间 1ID 2顶 3踩
ctype 评论类型 1内容 2其他
<%load_data action=comment output=nihao ctype=1 id=3 num=12 fields=id,name,tilte uid=1 durdate=10 order=1 ordertype=desc cache=0%>
id为文档ID


调用投票：
action:vote
<%load_data action=vote output=nihao num=12 durdate=30 likeid=dfd fields=id,name order=1%>
order 0时间 1ID 2权重 3投票数

调用表数据
<%dbtable table=sta_voteoptions output=nihao num=12 where=id>8_and_id<100 fields=id,name,tilte order=id_desc cache=0%>

sql语句(data为变量声明[必填])：
tbprefix_ 为表前缀
variable_ 表示list为变量
<%sqlstring data select variable_list * from tbprefix_contents%>
<%sqlstring list select title,img,thumb,text,url from plus_slideimgs where likeid = 'df' order by orderid desc%>

频道分页：
self //是否调用只调用本频道内容1是 0否
fields //字段 typeid默认存在,不可写
orderBy //排序
pageSize //页大小
pageN //显示页码
isext //是否调用扩展表
showselect //页面导航是否显示select
datas //返回结构集
Paging(int self, string fields, string orderBy, int pageSize, int pageN, bool isext, bool showselect, out DataTable datas)

<%set (DataTable){datas}=null%>
<%set (string){pageguide}=Paging(1,"id,typeid,savepath,filename,title,addtime","orderid desc,addtime desc",config.Listinfocount,7,out datas)%>


ajax调用 //t=动作名称
conclick //获取内容浏览数 id内容id  back是否更新并返回点击数
getconclick //获取内容浏览数 id
magclick //获取杂志浏览数 id内容id  back是否更新并返回点击数
getmagclick //获取杂志浏览数 id
getsoftdowncount //获取软件下载数量 id
diggstamp //获取内容顶踩数 id  >12,32
confieldval //获取扩展表字段值 ext 扩展标识 cid文档ID field获取的字段  >值
condigg //顶一下 id >bool
constamp //踩一下 id >bool
comdigg //评论顶 id   >bool
comstamp //评论踩 id  >bool
subcomment //提交评论 vcode验证码 cname cookie名称 cid内容ID contitle内容标题 uid相关用户 username用户名 title评论标题 msg评论内容 quote replay
concomcount //内容评论数 id
getconcomment //cid相关ID  fields要调用的字段 ctype相关类型 1内容 order 排序字段(diggcount,id,addtime,stampcount) page页码 pagesize每页调用数量 >json数据 {pagecount:页数,recordcount:总条数,content:评论数组}
getcontentlist //返回内容列表 num数量 fields字段 typeid内容类型 addusers用户名列表 channelid频道ID property属性 startdate enddate keywords关键字 返回JSON数据
userlogin //检查用户登录 username password vcode expires  >返回错误信息 空字符则成功
getuseraddress //获取用户地址列表 uid
edituseraddress //编辑用户地址信息  id,uid,username,title,email,address,postcode,phone,parms
getprodsbyids //根据商品ID列表获取商品列表 fields 字段列表 ids产品id列表
mailsubcribe //邮件订阅添加  name 订阅人 mail 邮件 group 分组
translate //语言翻译  text 文本  sl源语言(auto) tl目标语言 def默认结果
usernameexist //用户名是否存在 username    >字符0则不存在
useremailexist //邮件是否存在 email >同上

rss订阅；
count 显示数量
chl 频道ID
cache 是否缓存数据
order 排序方式
peri 更新频率
/tools/rss.aspx?count=200&chl=0&cache=0&order=0&peri=80

rss静态生成地址
/sta/data/rss/频道id.xml



搜索页面 (search.aspx)

page 页码
persize 页容量
chlid 频道ID
typeid 模型id  如果模型ID>-1 可调用扩展表字段
searchid 缓存ID
order 0时间 1ID 2权重 3点击 4顶 5踩 6评论数 7更新时间 默认按时间
ordertype 1降序 2升序 默认降序
durday 搜索几天内的内容
query 关键字
searchtype 搜索类型  1标题  0,2或其他模糊搜素

搜素分页：
fields //字段  typeid默认存在,不可写
pageN //显示页码
datas //返回结构集
Paging(string fields, int pageN, out DataTable condata, out int pagecount, out int recordcount)

<%set (DataTable){datas}=null%>
<%set (int){pagecount}=0%>
<%set (int){recordcount}=0%>
<%set (string){pageguide}=Paging("id,savepath,filename,title,addtime,content,click,channelid,channelname",20,out datas,out pagecount,out recordcount)%>


//JSON格式字符串转JSON
function ToJson(data) {
    return eval('(' + data + ')');
};


其他：
/vote.aspx 投票功能展示
参数 
vtype:获取方式  ids集合(ids)  标识列(like)
relval：相关值
display：返回方式  html、js

/plus/vote.aspx 扩展投票
id: 投票ID
utype 
display html,其他

后台扩展：

数据库安装   @tbprefix_ 表前缀

							
							
							
plus
生成条码
sta/plus/barimg.aspx?width=100&height=100&format=QR_CODE&txt=内容
