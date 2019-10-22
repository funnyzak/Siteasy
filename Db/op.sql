
/****** 添加变量表 ******/
CREATE TABLE [dbo].[sta_variables](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[likeid] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[key] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[desc] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[value] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[system] [tinyint] NOT NULL,
 CONSTRAINT [PK_sta_variables] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

go

delete from sta_variables

go

SET IDENTITY_INSERT [dbo].[sta_variables] ON
INSERT [dbo].[sta_variables] ([id], [name], [likeid], [key], [desc], [value], [system]) VALUES (6, N'留言咨询类型', N'插件.留言建议', N'advisory_msgtype', N'系统插件留言建议的留言类型配置', N'网站建议,内容排版,合作咨询', 1)
INSERT [dbo].[sta_variables] ([id], [name], [likeid], [key], [desc], [value], [system]) VALUES (10, N'内容挑错类型', N'插件.内容挑错', N'pickerr_type', N'系统插件内容挑错的错误类型配置', N'错别字(除的、地、得),成语运用不当,专业术语写法不规则,产品与图片不符,事实年代以及内容错误,技术参数错误,其他', 1)
SET IDENTITY_INSERT [dbo].[sta_variables] OFF
go

drop proc [sta_createmenu]  

go
CREATE PROCEDURE [dbo].[sta_createmenu]  
	@id int,
	@name nvarchar(50),
	@pagetype tinyint,
	@parentid int,
	@system tinyint,
	@type tinyint,
	@icon nvarchar(100),
	@url nvarchar(200),
	@target nchar(20),
	@orderid int,
	@identify nvarchar(30)
AS
INSERT INTO [sta_menus] ([name], [pagetype], [parentid], [system], [type], [icon], [url], [target], [orderid], [identify]) VALUES (@name, @pagetype, @parentid, @system, @type, @icon, @url, @target, @orderid, @identify);SELECT SCOPE_IDENTITY()


go

drop proc [sta_updatemenu]

go
CREATE PROCEDURE [dbo].[sta_updatemenu]
	@id int,
	@name nvarchar(50),
	@parentid int,
	@pagetype tinyint,
	@system tinyint,
	@type tinyint,
	@icon nvarchar(100),
	@url nvarchar(200),
	@target nchar(20),
	@orderid int,
	@identify nvarchar(30)
AS
UPDATE [sta_menus]
SET [name] = @name, [pagetype] = @pagetype, [parentid] = @parentid, [system] = @system, [type] = @type, [icon] = @icon, [url] = @url, [target] = @target, [orderid] = @orderid, [identify] = @identify
WHERE [sta_menus].[id] = @id

go

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_useraddress]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_useraddress](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uid] [int] NOT NULL,
	[username] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[title] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[email] [char](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[address] [nvarchar](150) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[postcode] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[phone] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[parms] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_sta_useraddress] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO


CREATE PROC [dbo].[sta_templatespecgroup] 
	@Id int,
	@Template nvarchar(50) output
As 
SELECT @Template = ISNULL(ext_grouptpl,'') FROM sta_extspecials WHERE cid = @Id
IF @Template IS NOT NULL AND @Template = '' 
	SET @Template = 'specgroup_default'

go

DROP PROC [dbo].[sta_deletecontent]

go
CREATE PROCEDURE [dbo].[sta_deletecontent] --删除内容
	@id int
AS
DELETE FROM [sta_specontents] WHERE [sta_specontents].[contentid] = @id OR [sta_specontents].[specid] = @id
DELETE FROM [sta_comments] WHERE [sta_comments].[cid] = @id
DELETE FROM [sta_congroupcons] WHERE [sta_congroupcons].[cid] = @id
DECLARE @typeid int,@typetable nvarchar(30)
SELECT @typeid = typeid FROM [sta_contents] WHERE [sta_contents].[id] = @id
SELECT @typetable = extable FROM [sta_contypes]  WHERE [sta_contypes].[id] = @typeid
IF @typeid = 0
	BEGIN
		DELETE FROM [sta_specgroups] WHERE [sta_specgroups].[specid] = @id
	END
IF OBJECT_ID(@typetable) IS NOT NULL
	EXEC(N'DELETE FROM [' + @typetable + N'] WHERE [' + @typetable + N'].[cid] = ' + @id)
EXEC [sta_deletetagsbycid] @id
DELETE FROM [sta_contents] WHERE [sta_contents].[id] = @id

go

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_payments]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_payments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dll] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[name] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[description] [nvarchar](700) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[pic] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[isvalid] [tinyint] NOT NULL,
	[parms] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[version] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[author] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[orderid] [int] NOT NULL,
 CONSTRAINT [PK_sta_payments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

delete from sta_payments

go

SET IDENTITY_INSERT [dbo].[sta_payments] ON
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (2, N'Alipay', N'支付宝(即时到帐,单笔2%交易费)', N'支付宝即时到帐接口，是支付宝公司针对网上收款、付费、虚拟物品交易而建立的一个安全即时到帐交易平台、无需繁杂的过程即可轻松实现在线交易。', N'/files/pics/pay/alipay.gif', 1, N'silenceace@163.com,btfhyepobkdkemcxtk2m74t,2088002061866935,/files/pics/pay/alipay.gif', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (4, N'Yeepay', N'易宝在线支付', N'YeePay易宝（北京通融通信息技术有限公司）是专业从事多元化电子支付业务一站式服务的领跑者。在立足于网上支付的同时，YeePay易宝不断创新，将互联网、手机、固定电话整合在一个平台上，继短信支付、手机充值之后，首家推出了YeePay易宝电话支付业务，真正实现了离线支付，为更多传统行业搭建了电子支付的高速公路。', N'/files/pics/pay/yeepay.gif', 1, N'12321,sfsdfwfsfsfsdf,/files/pics/pay/yeepay.gif', N'V2.1', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (6, N'Tenpay_Strandard', N'财付通在线支付(担保交易)', N'财付通网站 (www.tenpay.com) 作为功能强大的支付平台，是由中国最早、最大的互联网即时通信软件开发商腾讯公司创办，为最广大的QQ用户群提供安全、便捷、简单的在线支付服务。', N'/files/pics/pay/tenpay.png', 1, N'332488355,sdfsdf,/files/pics/pay/tenpay.png', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (7, N'Alipay_Secured', N'支付宝(担保交易,单笔1.5%交易费)', N'支付宝，是支付宝公司针对网上交易而特别推出的安全付款服务，其运作的实质是以支付宝为信用中介，在买家确认收到商品前，由支付宝替买卖双方暂时保管货款的一种增值服务。', N'/files/pics/pay/alipay.gif', 1, N',,,/files/pics/pay/alipay.gif', N'V1.35', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (8, N'Bank', N'银行汇款/转帐', N'银行汇款/转帐', N'/files/pics/pay/bank.gif', 1, N'/,/files/pics/pay/bank.gif', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (9, N'Tenpay', N'财付通在线支付', N'财付通网站 (www.tenpay.com) 作为功能强大的支付平台，是由中国最早、最大的互联网即时通信软件开发商腾讯公司创办，为最广大的QQ用户群提供安全、便捷、简单的在线支付服务。', N'/files/pics/pay/tenpay.png', 1, N'332488355,sdfsfsrwswrfwre,/files/pics/pay/tenpay.png', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (10, N'Bill99', N'快钱在线支付', N'快钱是支付产品最丰富、覆盖人群最广泛的电子支付企业，其推出的支付产品包括但不限于人民币支付，外卡支付，神州行卡支付，联通充值卡支付，VPOS支付等众多支付产品，支持互联网、手机、电话和 POS等多种终端，满足各类企业和个人的不同支付需求。', N'/files/pics/pay/99bill.gif', 1, N',,/files/pics/pay/99bill.gif', N'V1.0', N'funnyzak', 50)
SET IDENTITY_INSERT [dbo].[sta_payments] OFF


/****** 对象:  Table [dbo].[sta_shopgoods]    脚本日期: 11/29/2012 12:29:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_shopgoods]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_shopgoods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gtype] [tinyint] NOT NULL,
	[cid] [int] NOT NULL,
	[oid] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[uid] [int] NOT NULL,
	[username] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[goodname] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[buynum] [int] NOT NULL,
 CONSTRAINT [PK_sta_shopgoods] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_shoporders]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_shoporders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[oid] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[uid] [int] NOT NULL,
	[username] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[gtype] [tinyint] NOT NULL,
	[did] [int] NOT NULL,
	[pid] [int] NOT NULL,
	[adrid] [int] NOT NULL,
	[cartcount] [int] NOT NULL,
	[dprice] [decimal](18, 2) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[totalprice] [decimal](18, 2) NOT NULL,
	[status] [tinyint] NOT NULL,
	[ip] [nvarchar](24) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[isinvoice] [tinyint] NOT NULL,
	[invoicehead] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[invoicecost] [decimal](18, 2) NOT NULL,
	[addtime] [datetime] NOT NULL,
	[remark] [nvarchar](300) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[parms] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_sta_shoporders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_shopdelivery]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_shopdelivery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ename] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[description] [nvarchar](300) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[fweight] [decimal](18, 2) NOT NULL,
	[iscod] [tinyint] NOT NULL,
	[parms] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[orderid] [int] NOT NULL,
 CONSTRAINT [PK_sta_shopdelivery] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

delete from [sta_shopdelivery]

go

SET IDENTITY_INSERT [dbo].[sta_shopdelivery] ON
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (1, N'EMS特快', N'EMS', N'EMS特快专递业务自1980年开办以来，业务量逐年增长，业务种类不断丰富，服务质量不断提高。除提供国内、国际特快专递服务外，EMS相继推出国内次晨达和次日递、国际承诺服务和限时递等高端服务，同时提供代收货款、收件人付费、鲜花礼仪速递等增值服务。', CAST(0.50 AS Decimal(18, 2)), 0, N'0.5,20,0.5,6,999', 100)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (2, N'普通邮递', N'', N'普通邮递', CAST(1000.00 AS Decimal(18, 2)), 0, N'1000.00,10,0,0,999', 50)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (3, N'申通快递', N'STO', N'申通快递品牌创建于1993年，是国内最早经营快递业务的品牌之一，经过十多年的发展，申通快递在全国范围内形成了完善、流畅的自营速递网络，基本覆盖到全国地市级以上城市和发达地区县级以上城市，尤其是在江浙沪地区，基本实现了派送无盲区。', CAST(0.50 AS Decimal(18, 2)), 0, N'0.50,15,0.5,5,999', 70)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (4, N'顺丰快递', N'', N'顺丰可以提供全国33个省、直辖市、港澳台地区的高水准门到门快递服务。采用标准定价、标准操作流程，各环节均以最快速度进行发运、中转、派送，并对客户进行相对标准承诺。', CAST(0.50 AS Decimal(18, 2)), 0, N'0.50,20,0.5,6,999999', 120)
SET IDENTITY_INSERT [dbo].[sta_shopdelivery] OFF
delete from sta_menus

go



IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_pms]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_pms](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msgfrom] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[msgfromid] [int] NOT NULL,
	[msgto] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[msgtoid] [int] NOT NULL,
	[folder] [tinyint] NOT NULL,
	[new] [int] NOT NULL,
	[subject] [nvarchar](60) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[content] [ntext] COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL,
 CONSTRAINT [PK_sta_pms] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[sta_paylogs]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[sta_paylogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[oid] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[uid] [int] NOT NULL,
	[username] [nchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[title] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[gtype] [tinyint] NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[payid] [int] NOT NULL,
	[payname] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL,
 CONSTRAINT [PK_sta_paylogs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)
END
GO

/****** 重建频道存储过程 ******/
drop proc sta_createchannel

go

CREATE PROCEDURE [dbo].[sta_createchannel]  --添加频道
	@id int,
	@typeid smallint,
	@parentid int,
	@name nvarchar(50),
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@ctype tinyint,
	@img nvarchar(100),
	@addtime datetime,
	@covertem char(50),
	@listem char(50),
	@contem char(50),
	@conrule nvarchar(100),
	@listrule nvarchar(100),
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@moresite tinyint,
	@siteurl nchar(100),
	@content ntext,
	@ispost tinyint,
	@ishidden tinyint,
	@orderid int,
	@viewgroup nvarchar(300),
	@viewcongroup nvarchar(300)
AS
INSERT INTO [sta_channels] ([typeid], [parentid], [name], [savepath], [filename], [ctype], [img], [addtime], [covertem], [listem], [contem], [conrule], [listrule], [seotitle], [seokeywords], [seodescription], [moresite], [siteurl], [content], [ispost], [ishidden], [orderid], [viewgroup], [viewcongroup]) VALUES (@typeid, @parentid, @name, @savepath, @filename, @ctype, @img, @addtime, @covertem, @listem, @contem, @conrule, @listrule, @seotitle, @seokeywords, @seodescription, @moresite, @siteurl, @content, @ispost, @ishidden, @orderid, @viewgroup, @viewcongroup);SELECT SCOPE_IDENTITY()

go

drop proc sta_updatechannel

go

CREATE PROCEDURE [dbo].[sta_updatechannel]  --修改频道
	@id int,
	@typeid smallint,
	@parentid int,
	@name nvarchar(50),
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@ctype tinyint,
	@img nvarchar(100),
	@addtime datetime,
	@covertem char(50),
	@listem char(50),
	@contem char(50),
	@conrule nvarchar(100),
	@listrule nvarchar(100),
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@moresite tinyint,
	@siteurl nchar(100),
	@content ntext,
	@ispost tinyint,
	@ishidden tinyint,
	@orderid int,
	@viewgroup nvarchar(300),
	@viewcongroup nvarchar(300)
AS
UPDATE [sta_contents]
SET [channelname] = @name, [viewgroup] = @viewcongroup WHERE [channelid] = @id

UPDATE [sta_dbcollects]
SET [channelname] = @name WHERE [channelid] = @id

UPDATE [sta_webcollects]
SET [channelname] = @name WHERE [channelid] = @id

UPDATE [sta_channels]
SET [typeid] = @typeid, [img] = @img, [parentid] = @parentid, [name] = @name, [savepath] = @savepath, [filename] = @filename, [ctype] = @ctype, [addtime] = @addtime, [covertem] = @covertem, [listem] = @listem, [contem] = @contem, [conrule] = @conrule, [listrule] = @listrule, [seotitle] = @seotitle, [seokeywords] = @seokeywords, [seodescription] = @seodescription, [moresite] = @moresite, [siteurl] = @siteurl, [content] = @content, [ispost] = @ispost, [ishidden] = @ishidden, [orderid] = @orderid, [viewgroup] = @viewgroup,[viewcongroup] = @viewcongroup
WHERE [sta_channels].[id] = @id

go


/****** 重建用户存储过程 ******/
drop proc sta_updateuser

go

CREATE PROCEDURE [dbo].[sta_updateuser]
	@id int,
	@username nchar(20),
	@nickname nchar(20),
	@password nchar(32),
	@safecode nchar(32),
	@gender tinyint,
	@adminid int,
    @admingroupname nvarchar(30),
	@groupname nvarchar(30),
	@groupid int,
	@extgroupids char(100),
	@regip char(15),
	@addtime datetime,
	@loginip char(15),
	@logintime datetime,
	@lastaction datetime,
	@money decimal(18,0),
	@credits int,
	@extcredits1 int,
	@extcredits2 int,
	@extcredits3 int,
	@extcredits4 int,
	@extcredits5 int,
	@email char(100),
	@ischeck tinyint,
	@locked tinyint
AS
UPDATE [sta_comments] SET username = @username WHERE uid = @id
UPDATE [sta_attachments] SET username = @username WHERE uid = @id
UPDATE [sta_attachments] SET lasteditusername = @username WHERE lastedituid = @id 
UPDATE [sta_adminlogs] SET username = @username WHERE uid = @id 
UPDATE [sta_voterecords] SET username = @username WHERE userid = @id
UPDATE [sta_userauths] SET username = @username WHERE userid = @id
UPDATE [sta_contents] SET addusername = @username WHERE adduser = @id
UPDATE [sta_contents] SET lasteditusername= @username WHERE lastedituser = @id
UPDATE [sta_shoporders] SET username = @username WHERE uid = @id
UPDATE [sta_shopgoods] SET username = @username WHERE uid = @id
UPDATE [sta_paylogs] SET username = @username WHERE uid = @id
UPDATE [dbo.sta_pms] SET msgfrom = @username WHERE msgfromid = @id
UPDATE [dbo.sta_pms] SET msgto = @username WHERE msgtoid = @id

UPDATE [sta_users]
SET [username] = @username, [nickname] = @nickname, [admingroupname] = @admingroupname, [groupname] = @groupname, [password] = @password, [safecode] = @safecode, [gender] = @gender, [adminid] = @adminid, [groupid] = @groupid, [extgroupids] = @extgroupids, [regip] = @regip, [addtime] = @addtime, [loginip] = @loginip, [logintime] = @logintime, [lastaction] = @lastaction, [money] = @money, [credits] = @credits, [extcredits1] = @extcredits1, [extcredits2] = @extcredits2, [extcredits3] = @extcredits3, [extcredits4] = @extcredits4, [extcredits5] = @extcredits5, [email] = @email, [ischeck] = @ischeck ,[locked] = @locked 
WHERE [sta_users].[id] = @id


go




drop proc [dbo].[sta_deleteuser]

go

CREATE PROCEDURE [dbo].[sta_deleteuser]
	@id int
AS
UPDATE [sta_comments] SET username = '已注销' WHERE uid = @id
UPDATE [sta_attachments] SET username = '已注销' WHERE uid = @id
UPDATE [sta_attachments] SET lasteditusername = '已注销' WHERE lastedituid = @id 
UPDATE [sta_adminlogs] SET username = '已注销' WHERE uid = @id 
UPDATE [sta_voterecords] SET username = '已注销' WHERE userid = @id
UPDATE [sta_userauths] SET username = '已注销' WHERE userid = @id
UPDATE [sta_contents] SET addusername = '已注销' WHERE adduser = @id
UPDATE [sta_contents] SET lasteditusername= '已注销' WHERE lastedituser = @id
UPDATE [sta_shoporders] SET username = '已注销' WHERE uid = @id
UPDATE [sta_shopgoods] SET username = '已注销' WHERE uid = @id
UPDATE [sta_paylogs] SET username = '已注销' WHERE uid = @id
UPDATE [dbo.sta_pms] SET msgfrom = '已注销' WHERE msgfromid = @id
UPDATE [dbo.sta_pms] SET msgto = '已注销' WHERE msgtoid = @id

DELETE FROM [sta_userfields] WHERE [sta_userfields].[uid] = @id
DELETE FROM [sta_users] WHERE [sta_users].[id] = @id


go

delete form sta_contypefields
go

SET IDENTITY_INSERT [dbo].[sta_contypefields] ON
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (27, 2, convert(text, N'ext_style           ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 50, 1, N'', N'显示风格', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (28, 2, convert(text, N'ext_imgs            ' collate Chinese_PRC_CI_AS), convert(text, N'ntext               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'图片集合', N'', 3, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (36, 3, convert(text, N'ext_softlevel       ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'等级', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (38, 3, convert(text, N'ext_language        ' collate Chinese_PRC_CI_AS), convert(text, N'nchar               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'语言', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (39, 3, convert(text, N'ext_license         ' collate Chinese_PRC_CI_AS), convert(text, N'char                ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'授权方式', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (40, 3, convert(text, N'ext_environment     ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'运行环境', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (41, 3, convert(text, N'ext_officesite      ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'官方网站', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (42, 3, convert(text, N'ext_demourl         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'演示地址', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (43, 3, convert(text, N'ext_filesize        ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 50, 1, N'', N'文件大小', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (45, 3, convert(text, N'ext_downlinks       ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 2000, 1, N'', N'下载地址', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (46, 3, convert(text, N'ext_downcount       ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'下次次数', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (47, 3, convert(text, N'ext_softtype        ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'类型', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (52, 5, convert(text, N'ext_nativeplace     ' collate Chinese_PRC_CI_AS), convert(text, N'stepselect          ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'地区', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (53, 5, convert(text, N'ext_infotype        ' collate Chinese_PRC_CI_AS), convert(text, N'stepselect          ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'信息类型', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (54, 5, convert(text, N'ext_endtime         ' collate Chinese_PRC_CI_AS), convert(text, N'datetime            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'截止日期', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (55, 5, convert(text, N'ext_linkman         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 30, 1, N'', N'联系人', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (56, 5, convert(text, N'ext_tel             ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'联系电话', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (57, 5, convert(text, N'ext_email           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'电子邮件', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (58, 5, convert(text, N'ext_address         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'联系地址', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (61, 4, convert(text, N'ext_price           ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'市场价', N'请填写数值,可保留小数点两位', 1000, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (62, 4, convert(text, N'ext_vipprice        ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'优惠价', N'请填写数值,可保留小数点两位', 900, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (63, 4, convert(text, N'ext_brand           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 30, 1, N'', N'品牌', N'', 800, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (64, 4, convert(text, N'ext_unit            ' collate Chinese_PRC_CI_AS), convert(text, N'select              ' collate Chinese_PRC_CI_AS), 10, 1, N'瓶 PING,个 GE,台 TAI,箱 XIANG,套 TAO,盒 HE,件 JIAN,本 BEN,部 BU,公斤 KG,克 G', N'计量单位', N'', 500, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (67, 4, convert(text, N'ext_ontime          ' collate Chinese_PRC_CI_AS), convert(text, N'datetime            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'上架时间', N'', 700, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (68, 6, convert(text, N'ext_vfile           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'视频地址', N'', 0, N'<tr>

                                <td class="itemtitle">

                                    ~name~：

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="支持本地视频和外站引用(如优酷、土豆、腾讯、酷6、56等,填写视频链接即可).本地视频只支持flv格式, 如视频文件较大,可使用大文件上传功能上传." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" size="30" style="width:420px;" value="~text~"/>

                                    <span id="selectvideo" class="selectbtn">选择</span>

                                    <a href="javascript:;" id="previewvideo" class="fancybox.iframe">预览</a>
							
                                </td>

                            </tr>')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (69, 6, convert(text, N'ext_copyright       ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'版权方', N'视频的版权方', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (70, 6, convert(text, N'ext_star            ' collate Chinese_PRC_CI_AS), convert(text, N'select              ' collate Chinese_PRC_CI_AS), 20, 1, N'不推荐,一星,二星,三星,四星,五星', N'推荐星级', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (71, 6, convert(text, N'ext_duration        ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'60', N'视频时长', N'视频播放时长,单位为(秒)', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (72, 4, convert(text, N'ext_imgs            ' collate Chinese_PRC_CI_AS), convert(text, N'ntext               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'产品图片', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (73, 4, convert(text, N'ext_storage         ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 4, 1, N'100', N'库存量', N'商品的库存量,只能为整数', 600, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (74, 4, convert(text, N'ext_weight          ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'商品重量', N'商品的重量,KG为单位', 350, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (75, 4, convert(text, N'ext_code            ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 18, 1, N'', N'商品编号', N'商品的编号,可自由输入,或随机生成', 1200, N'<tr>

                                <td class="itemtitle">

                                    ~name~：

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="商品的编号(最长18个字符),可自由输入,或随机生成." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" value="~text~"/>
									生成
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(9));">9位</span>
                                    <span class="selectbtn" onclick="$(''#~field~'').val(RandNum(12));">12位</span>
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(15));">15位</span>
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(18));">18位</span>
							
                                </td>

                            </tr>')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (76, 4, convert(text, N'ext_vfile           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'视频地址', N'', 0, N'<tr>

                                <td class="itemtitle">

                                    ~name~：

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="支持本地视频和外站引用(如优酷、土豆、腾讯、酷6、56等,填写视频链接即可).本地视频只支持flv格式, 如视频文件较大,可使用大文件上传功能上传." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" size="30" style="width:420px;" value="~text~"/>

                                    <span id="selectvideo" class="selectbtn">选择</span>

                                    <a href="javascript:;" id="previewvideo" class="fancybox.iframe">预览</a>
 <script type="text/javascript">RegSelectFilePopWin("selectvideo", "视频文件选择", "root=/files&path=/files&filetype=mp4,flv&fullname=1&cltmed=1&fele=ext_vfile", "click");
        RegPreviewFlv("#ext_vfile", "#previewvideo");
</script>
                                </td>

                            </tr>')
SET IDENTITY_INSERT [dbo].[sta_contypefields] OFF

go



/****** 对象:  Table [dbo].[sta_magazines]    脚本日期: 02/28/2013 15:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sta_magazines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL,
	[updatetime] [datetime] NOT NULL,
	[likeid] [nvarchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ratio] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[cover] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[description] [ntext] COLLATE Chinese_PRC_CI_AS NOT NULL,
	[content] [ntext] COLLATE Chinese_PRC_CI_AS NOT NULL,
	[pages] [int] NOT NULL,
	[orderid] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[click] [int] NOT NULL,
	[parms] [nvarchar](300) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_sta_magazines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



/****** 重置菜单 ******/
delete from sta_menus 
go

SET IDENTITY_INSERT [dbo].[sta_menus] ON
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (1, N'内容管理', 0, 1, 1, 1, N' ', N'global/global_contentlist.aspx?type=1', N'left                ', 500)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (2, N'信息管理', 1, 1, 1, 1, N'content.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (3, N'所有文档列表', 2, 1, 1, 1, N'page_stack.png', N'global/global_contentlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (4, N'待审核文档', 2, 1, 1, 1, N'verify.png', N'global/global_contentverify.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (5, N'用户管理', 0, 1, 1, 1, N'', N'global/global_userlist.aspx', N'left                ', 400)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (6, N'站点设置', 0, 1, 1, 1, N'', N'global/global_siteinfo.aspx', N'left                ', 300)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (7, N'系统扩展', 0, 1, 1, 1, N'', N'global/global_pluginlist.aspx', N'left                ', 300)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (8, N'系统工具', 0, 1, 1, 1, N'', N'global/global_emailsend.aspx', N'left                ', 200)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (9, N'文档快速添加', 2, 1, 1, 1, N'add.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (10, N'文档快速添加', 9, 1, 1, 1, N'', N'$模型文档添加', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (14, N'文档管理', 2, 1, 1, 1, N'archive.gif', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (15, N'文档管理', 14, 1, 1, 1, N'', N'$模型文档管理', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (16, N'频道管理', 2, 1, 1, 1, N'buildings.png', N'', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (17, N'频道列表', 16, 1, 1, 1, N'', N'global/global_channelist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (18, N'频道添加', 16, 1, 1, 1, N'', N'global/global_channeladd.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (19, N'频道批量添加', 16, 1, 1, 1, N'', N'global/global_channelbatchadd.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (20, N'组管理', 2, 1, 1, 1, N'gather.gif', N'', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (21, N'内容组', 20, 1, 1, 1, N'', N'global/global_congroups.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (25, N'频道组', 20, 1, 1, 1, N'', N'global/global_congroupchannels.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (26, N'频道模型', 2, 1, 1, 1, N'page_stack.png', N'', N'main                ', 49920)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (28, N'内容模型管理', 26, 1, 1, 1, N'cmodel.gif', N'global/global_contypes.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (34, N'单页模型管理', 26, 1, 1, 1, N'page.png', N'global/global_pagelist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (38, N'内容维护', 2, 1, 1, 1, N'page_world.png', N'', N'main                ', 49910)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (39, N'内容标签', 38, 1, 1, 1, N'tags.gif', N'global/global_tags.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (40, N'重复文档检查', 38, 1, 1, 1, N'', N'global/global_repeatconcheck.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (41, N'表字段批量替换', 38, 1, 1, 1, N'shading.png', N'global/global_dbfieldreplace.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (42, N'我发布的内容', 2, 1, 1, 1, N'', N'global/global_contentmine.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (43, N'评论管理', 2, 1, 1, 1, N'comment.png', N'global/global_comments.aspx', N'main                ', 49890)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (44, N'内容回收站', 2, 1, 1, 1, N'trash.gif', N'global/global_contentrecycle.aspx', N'main                ', 49880)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (45, N'静态发布', 1, 1, 1, 1, N'publish.gif', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (46, N'首页发布', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?start=yes', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (47, N'频道发布', 45, 1, 1, 1, N'', N'global/global_createchannels.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (48, N'文档发布', 45, 1, 1, 1, N'', N'global/global_createcontents.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (49, N'专题发布', 45, 1, 1, 1, N'', N'global/global_createspecials.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (50, N'单页发布', 45, 1, 1, 1, N'', N'global/global_createpages.aspx', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (51, N'一键发布', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?type=onekey', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (52, N'附件管理', 1, 1, 1, 1, N'file.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (53, N'批量上传', 52, 1, 1, 1, N'', N'global/global_attachmentuploadpre.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (54, N'附件管理', 52, 1, 1, 1, N'', N'global/global_attachments.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (55, N'文件浏览器', 52, 1, 1, 1, N'', N'global/global_filesexplore.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (56, N'功能管理', 1, 1, 1, 1, N'function.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (57, N'投票管理', 56, 1, 1, 1, N'vote.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (58, N'广告管理', 56, 1, 1, 1, N'advertisement.gif', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (59, N'链接管理', 56, 1, 1, 1, N'innerlink.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (60, N'内容采集', 56, 1, 1, 1, N'group.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (61, N'发起投票', 57, 1, 1, 1, N'', N'global/global_voteadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (62, N'投票管理', 57, 1, 1, 1, N'', N'global/global_votelist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (63, N'投票分类', 57, 1, 1, 1, N'', N'global/global_votecates.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (64, N'投票记录', 57, 1, 1, 1, N'', N'global/global_voterecords.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (65, N'广告添加', 58, 1, 1, 1, N'', N'global/global_adadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (66, N'广告管理', 58, 1, 1, 1, N'', N'global/global_adlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (67, N'链接添加', 59, 1, 1, 1, N'', N'global/global_flinkadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (68, N'分类管理', 59, 1, 1, 1, N'', N'global/global_flinkcls.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (69, N'链接管理', 59, 1, 1, 1, N'', N'global/global_flinks.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (70, N'数据库采集', 60, 1, 1, 1, N'', N'global/global_dbcollect.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (71, N'站点信息采集', 60, 1, 1, 1, N'', N'global/global_webcollect.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (72, N'模板管理', 1, 1, 1, 1, N'template.gif', N'', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (73, N'模板管理', 72, 1, 1, 1, N'', N'global/global_templatelist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (74, N'全局配置', 6, 1, 1, 1, N'site.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (75, N'站点信息', 74, 1, 1, 1, N'', N'global/global_siteinfo.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (76, N'基本设置', 74, 1, 1, 1, N'', N'global/global_baseset.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (77, N'性能优化', 74, 1, 1, 1, N'', N'global/global_optimizeset.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (78, N'互动设置', 74, 1, 1, 1, N'', N'global/global_interactset.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (80, N'附件设置', 74, 1, 1, 1, N'', N'global/global_attachmentset.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (81, N'图片水印设置', 74, 1, 1, 1, N'', N'global/global_waterset.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (82, N'邮箱设置', 74, 1, 1, 1, N'', N'global/global_emailset.aspx', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (83, N'投票设置', 74, 1, 1, 1, N'', N'global/global_voteset.aspx', N'main                ', 49920)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (84, N'安全控制', 74, 1, 1, 1, N'', N'global/global_safeset.aspx', N'main                ', 49910)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (85, N'用户管理', 5, 1, 1, 1, N'user.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (86, N'用户添加', 85, 1, 1, 1, N'', N'global/global_useradd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (87, N'用户列表', 85, 1, 1, 1, N'', N'global/global_userlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (88, N'用户组管理', 5, 1, 1, 1, N'group.png', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (89, N'添加用户组', 88, 1, 1, 1, N'', N'global/global_usergroupadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (91, N'用户组管理', 88, 1, 1, 1, N'', N'global/global_usergrouplist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (92, N'系统组管理', 88, 1, 1, 1, N'', N'global/global_sysgrouplist.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (100, N'菜单管理', 5, 1, 1, 1, N'tools.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (102, N'功能菜单', 100, 1, 1, 1, N'', N'global/global_menumanage.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (103, N'菜单管理', 100, 1, 1, 1, N'', N'global/global_menulist.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (104, N'实用工具', 8, 1, 1, 1, N'function.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (105, N'邮件群发', 104, 1, 1, 1, N'email.gif', N'global/global_emailsend.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (106, N'定时任务', 104, 1, 1, 1, N'task.gif', N'global/global_schedulemanage.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (108, N'Sitemap生成', 104, 1, 1, 1, N'innerlink.gif', N'tools/sitemapmake.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (109, N'JS在线压缩', 104, 1, 1, 1, N'script.png', N'global/global_jsmin.aspx', N'main                ', 49890)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (110, N'Html转JS代码', 104, 1, 1, 1, N'html.gif', N'global/global_html2js.aspx', N'main                ', 49840)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (112, N'数据库管理', 8, 1, 1, 1, N'database.png', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (113, N'运行SQL脚本', 112, 1, 1, 1, N'', N'global/global_runsql.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (114, N'数据表结构查看', 112, 1, 1, 1, N'form.gif', N'global/global_dbtableview.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (115, N'备份数据库', 112, 1, 1, 1, N'backup.gif', N'global/global_databasebackup.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (116, N'收缩数据库', 112, 1, 1, 1, N'machine.gif', N'global/global_databaseshrink.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (117, N'运行日志', 8, 1, 1, 1, N'log.gif', N'', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (118, N'操作日志', 117, 1, 1, 1, N'', N'global/global_adminoperatelogs.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (119, N'任务日志', 117, 1, 1, 1, N'', N'global/global_admintasklogs.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (120, N'系统日志', 117, 1, 1, 1, N'', N'global/global_adminlogs.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (121, N'错误日志', 117, 1, 1, 1, N'', N'global/global_errorlogs.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (123, N'系统扩展', 7, 1, 1, 1, N'integration.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (124, N'URL静态化', 123, 1, 1, 1, N'', N'global/global_urlstaticize.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (125, N'扩展管理', 7, 1, 1, 1, N'arrow_inout.png', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (126, N'所有扩展', 125, 1, 1, 1, N'', N'global/global_pluginlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (127, N'扩展创建向导', 125, 1, 1, 1, N'', N'global/global_pluginadd.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (128, N'数据管理', 7, 1, 1, 1, N'data.png', N'', N'main                ', 49900)
GO
print 'Processed 100 total records'
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (129, N'扩展数据管理', 128, 1, 1, 1, N'', N'$扩展数据管理', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (139, N'权限设置', 92, 1, 2, 1, N'', N'global/global_menuauthority.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (141, N'普通文档添加', 10, 1, 2, 1, N'', N'global/global_contentadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (142, N'图集添加', 10, 1, 2, 1, N'', N'global/global_photoadd.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (143, N'软件添加', 10, 1, 2, 1, N'', N'global/global_softadd.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (144, N'专题添加', 10, 1, 2, 1, N'', N'global/global_specialadd.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (145, N'普通文档管理', 15, 1, 2, 1, N'', N'global/global_contentlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (146, N'图集管理', 15, 1, 2, 1, N'', N'global/global_photolist.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (147, N'软件管理', 15, 1, 2, 1, N'', N'global/global_softlist.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (148, N'专题管理', 15, 1, 2, 1, N'', N'global/global_speciallist.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (149, N'专题内容管理', 148, 1, 2, 1, N'', N'global/global_specialcontents.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (150, N'附件编辑', 54, 1, 2, 1, N'', N'global/global_attachmentedit.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (151, N'附件上传', 53, 1, 2, 1, N'', N'global/global_attachmentupload.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (152, N'URL静态化添加', 124, 1, 2, 1, N'', N'global/global_urlstaticizeadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (153, N'投票选项', 61, 1, 2, 1, N'', N'global/global_voteitems.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (154, N'投票分类添加', 63, 1, 2, 1, N'', N'global/global_votecateadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (155, N'数据库采集添加', 70, 1, 2, 1, N'', N'global/global_dbcollectadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (156, N'站点信息采集添加', 71, 1, 2, 1, N'', N'global/global_webcollectadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (157, N'模版文件管理', 73, 1, 2, 1, N'', N'global/global_tplfiles.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (158, N'内容模型添加', 28, 1, 2, 1, N'', N'global/global_contypeadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (159, N'单页模型添加', 34, 1, 2, 1, N'', N'global/global_pageadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (160, N'内容组添加', 21, 1, 2, 1, N'', N'global/global_congroupadd.aspx?type=0', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (161, N'内容组内容维护', 21, 1, 2, 1, N'', N'global/global_congroupconedit.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (162, N'频道组添加', 25, 1, 2, 1, N'', N'global/global_congroupadd.aspx?type=1', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (163, N'菜单添加', 103, 1, 2, 1, N'', N'global/global_menuadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (164, N'联动类型管理', 26, 1, 1, 1, N'doc_shred.png', N'global/global_selects.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (165, N'联动项管理', 164, 1, 2, 1, N'', N'global/global_selectlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (166, N'分类信息添加', 10, 1, 2, 1, N'', N'global/global_infoadd.aspx', N'main                ', 49800)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (167, N'分类信息管理', 15, 1, 2, 1, N'', N'global/global_infolist.aspx', N'main                ', 49800)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (168, N'商品添加', 10, 1, 2, 1, N'', N'global/global_productadd.aspx', N'main                ', 49750)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (169, N'大文件上传', 52, 1, 1, 1, N'', N'global/global_attachmentbigfile.aspx', N'main                ', 49985)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (170, N'视频添加', 10, 1, 2, 1, N'', N'global/global_videoadd.aspx', N'main                ', 49700)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (171, N'地图发布', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?type=sitemap&start=yes', N'main                ', 49955)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (172, N'会员注册', 74, 1, 1, 1, N'', N'global/global_userregset.aspx', N'main                ', 48000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (173, N'简易投票', 123, 1, 1, 1, N'', N'com/vote/votelist.aspx', N'main                ', 60000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (174, N'投票添加', 173, 1, 2, 1, N'', N'com/vote/voteadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (175, N'投票查看', 173, 1, 2, 1, N'', N'com/vote/voteviewaspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (176, N'定时任务维护', 106, 1, 2, 1, N'', N'global/global_scheduleadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (177, N'扩展包安装', 125, 1, 1, 1, N'', N'global/global_pluginzip.aspx', N'main                ', 49955)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (178, N'频道发布状态', 47, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=channel', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (179, N'文档发布状态', 48, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=content', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (180, N'专题发布状态', 49, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=special', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (181, N'单页发布状态', 50, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=page', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (182, N'用户修改', 87, 1, 2, 1, N'', N'global/global_useredit.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (183, N'商品管理', 15, 1, 2, 1, N'', N'global/global_productadd.aspx', N'main                ', 49750)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (184, N'采集选项', 74, 1, 1, 1, N'', N'global/global_collectset.aspx', N'main                ', 49810)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (185, N'其他选项', 74, 1, 1, 1, N'', N'global/global_otherset.aspx', N'main                ', 47760)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (186, N'自定义变量', 123, 1, 1, 1, N'', N'com/ctmvariable/list.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (187, N'自定义变量添加', 186, 1, 2, 1, N'', N'com/ctmvariable/add.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (188, N'支付管理', 5, 1, 1, 1, N'money_yen.png', N'', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (189, N'订单管理', 188, 1, 1, 1, N'', N'pay/orderlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (190, N'支付方式', 188, 1, 1, 1, N'', N'pay/paylist.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (191, N'配送方式', 188, 1, 1, 1, N'', N'pay/deliverylist.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (192, N'配送方式添加', 191, 1, 2, 1, N'', N'pay/deliveryadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (194, N'支付方式配置', 190, 1, 2, 1, N'', N'pay/payset.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (195, N'订单修改', 189, 1, 2, 1, N'', N'pay/orderset.aspx', N'main                ', 50000)
SET IDENTITY_INSERT [dbo].[sta_menus] OFF