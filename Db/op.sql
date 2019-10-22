
/****** ��ӱ����� ******/
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
INSERT [dbo].[sta_variables] ([id], [name], [likeid], [key], [desc], [value], [system]) VALUES (6, N'������ѯ����', N'���.���Խ���', N'advisory_msgtype', N'ϵͳ������Խ����������������', N'��վ����,�����Ű�,������ѯ', 1)
INSERT [dbo].[sta_variables] ([id], [name], [likeid], [key], [desc], [value], [system]) VALUES (10, N'������������', N'���.��������', N'pickerr_type', N'ϵͳ�����������Ĵ�����������', N'�����(���ġ��ء���),�������ò���,רҵ����д��������,��Ʒ��ͼƬ����,��ʵ����Լ����ݴ���,������������,����', 1)
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
CREATE PROCEDURE [dbo].[sta_deletecontent] --ɾ������
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
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (2, N'Alipay', N'֧����(��ʱ����,����2%���׷�)', N'֧������ʱ���ʽӿڣ���֧������˾��������տ���ѡ�������Ʒ���׶�������һ����ȫ��ʱ���ʽ���ƽ̨�����跱�ӵĹ��̼�������ʵ�����߽��ס�', N'/files/pics/pay/alipay.gif', 1, N'silenceace@163.com,btfhyepobkdkemcxtk2m74t,2088002061866935,/files/pics/pay/alipay.gif', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (4, N'Yeepay', N'�ױ�����֧��', N'YeePay�ױ�������ͨ��ͨ��Ϣ�������޹�˾����רҵ���¶�Ԫ������֧��ҵ��һվʽ����������ߡ�������������֧����ͬʱ��YeePay�ױ����ϴ��£������������ֻ����̶��绰������һ��ƽ̨�ϣ��̶���֧�����ֻ���ֵ֮���׼��Ƴ���YeePay�ױ��绰֧��ҵ������ʵ��������֧����Ϊ���ഫͳ��ҵ��˵���֧���ĸ��ٹ�·��', N'/files/pics/pay/yeepay.gif', 1, N'12321,sfsdfwfsfsfsdf,/files/pics/pay/yeepay.gif', N'V2.1', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (6, N'Tenpay_Strandard', N'�Ƹ�ͨ����֧��(��������)', N'�Ƹ�ͨ��վ (www.tenpay.com) ��Ϊ����ǿ���֧��ƽ̨�������й����硢���Ļ�������ʱͨ�������������Ѷ��˾���죬Ϊ�����QQ�û�Ⱥ�ṩ��ȫ����ݡ��򵥵�����֧������', N'/files/pics/pay/tenpay.png', 1, N'332488355,sdfsdf,/files/pics/pay/tenpay.png', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (7, N'Alipay_Secured', N'֧����(��������,����1.5%���׷�)', N'֧��������֧������˾������Ͻ��׶��ر��Ƴ��İ�ȫ���������������ʵ������֧����Ϊ�����н飬�����ȷ���յ���Ʒǰ����֧����������˫����ʱ���ܻ����һ����ֵ����', N'/files/pics/pay/alipay.gif', 1, N',,,/files/pics/pay/alipay.gif', N'V1.35', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (8, N'Bank', N'���л��/ת��', N'���л��/ת��', N'/files/pics/pay/bank.gif', 1, N'/,/files/pics/pay/bank.gif', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (9, N'Tenpay', N'�Ƹ�ͨ����֧��', N'�Ƹ�ͨ��վ (www.tenpay.com) ��Ϊ����ǿ���֧��ƽ̨�������й����硢���Ļ�������ʱͨ�������������Ѷ��˾���죬Ϊ�����QQ�û�Ⱥ�ṩ��ȫ����ݡ��򵥵�����֧������', N'/files/pics/pay/tenpay.png', 1, N'332488355,sdfsfsrwswrfwre,/files/pics/pay/tenpay.png', N'V1.0', N'funnyzak', 0)
INSERT [dbo].[sta_payments] ([id], [dll], [name], [description], [pic], [isvalid], [parms], [version], [author], [orderid]) VALUES (10, N'Bill99', N'��Ǯ����֧��', N'��Ǯ��֧����Ʒ��ḻ��������Ⱥ��㷺�ĵ���֧����ҵ�����Ƴ���֧����Ʒ�����������������֧�����⿨֧���������п�֧������ͨ��ֵ��֧����VPOS֧�����ڶ�֧����Ʒ��֧�ֻ��������ֻ����绰�� POS�ȶ����նˣ����������ҵ�͸��˵Ĳ�֧ͬ������', N'/files/pics/pay/99bill.gif', 1, N',,/files/pics/pay/99bill.gif', N'V1.0', N'funnyzak', 50)
SET IDENTITY_INSERT [dbo].[sta_payments] OFF


/****** ����:  Table [dbo].[sta_shopgoods]    �ű�����: 11/29/2012 12:29:40 ******/
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
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (1, N'EMS�ؿ�', N'EMS', N'EMS�ؿ�ר��ҵ����1980�꿪��������ҵ��������������ҵ�����಻�Ϸḻ����������������ߡ����ṩ���ڡ������ؿ�ר�ݷ����⣬EMS����Ƴ����ڴγ���ʹ��յݡ����ʳ�ŵ�������ʱ�ݵȸ߶˷���ͬʱ�ṩ���ջ���ռ��˸��ѡ��ʻ������ٵݵ���ֵ����', CAST(0.50 AS Decimal(18, 2)), 0, N'0.5,20,0.5,6,999', 100)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (2, N'��ͨ�ʵ�', N'', N'��ͨ�ʵ�', CAST(1000.00 AS Decimal(18, 2)), 0, N'1000.00,10,0,0,999', 50)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (3, N'��ͨ���', N'STO', N'��ͨ���Ʒ�ƴ�����1993�꣬�ǹ������羭Ӫ���ҵ���Ʒ��֮һ������ʮ����ķ�չ����ͨ�����ȫ����Χ���γ������ơ���������Ӫ�ٵ����磬�������ǵ�ȫ�����м����ϳ��кͷ�������ؼ����ϳ��У��������ڽ��㻦����������ʵ����������ä����', CAST(0.50 AS Decimal(18, 2)), 0, N'0.50,15,0.5,5,999', 70)
INSERT [dbo].[sta_shopdelivery] ([id], [name], [ename], [description], [fweight], [iscod], [parms], [orderid]) VALUES (4, N'˳����', N'', N'˳������ṩȫ��33��ʡ��ֱϽ�С��۰�̨�����ĸ�ˮ׼�ŵ��ſ�ݷ��񡣲��ñ�׼���ۡ���׼�������̣������ھ�������ٶȽ��з��ˡ���ת�����ͣ����Կͻ�������Ա�׼��ŵ��', CAST(0.50 AS Decimal(18, 2)), 0, N'0.50,20,0.5,6,999999', 120)
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

/****** �ؽ�Ƶ���洢���� ******/
drop proc sta_createchannel

go

CREATE PROCEDURE [dbo].[sta_createchannel]  --���Ƶ��
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

CREATE PROCEDURE [dbo].[sta_updatechannel]  --�޸�Ƶ��
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


/****** �ؽ��û��洢���� ******/
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
UPDATE [sta_comments] SET username = '��ע��' WHERE uid = @id
UPDATE [sta_attachments] SET username = '��ע��' WHERE uid = @id
UPDATE [sta_attachments] SET lasteditusername = '��ע��' WHERE lastedituid = @id 
UPDATE [sta_adminlogs] SET username = '��ע��' WHERE uid = @id 
UPDATE [sta_voterecords] SET username = '��ע��' WHERE userid = @id
UPDATE [sta_userauths] SET username = '��ע��' WHERE userid = @id
UPDATE [sta_contents] SET addusername = '��ע��' WHERE adduser = @id
UPDATE [sta_contents] SET lasteditusername= '��ע��' WHERE lastedituser = @id
UPDATE [sta_shoporders] SET username = '��ע��' WHERE uid = @id
UPDATE [sta_shopgoods] SET username = '��ע��' WHERE uid = @id
UPDATE [sta_paylogs] SET username = '��ע��' WHERE uid = @id
UPDATE [dbo.sta_pms] SET msgfrom = '��ע��' WHERE msgfromid = @id
UPDATE [dbo.sta_pms] SET msgto = '��ע��' WHERE msgtoid = @id

DELETE FROM [sta_userfields] WHERE [sta_userfields].[uid] = @id
DELETE FROM [sta_users] WHERE [sta_users].[id] = @id


go

delete form sta_contypefields
go

SET IDENTITY_INSERT [dbo].[sta_contypefields] ON
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (27, 2, convert(text, N'ext_style           ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 50, 1, N'', N'��ʾ���', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (28, 2, convert(text, N'ext_imgs            ' collate Chinese_PRC_CI_AS), convert(text, N'ntext               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'ͼƬ����', N'', 3, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (36, 3, convert(text, N'ext_softlevel       ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'�ȼ�', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (38, 3, convert(text, N'ext_language        ' collate Chinese_PRC_CI_AS), convert(text, N'nchar               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'����', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (39, 3, convert(text, N'ext_license         ' collate Chinese_PRC_CI_AS), convert(text, N'char                ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��Ȩ��ʽ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (40, 3, convert(text, N'ext_environment     ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'���л���', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (41, 3, convert(text, N'ext_officesite      ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'�ٷ���վ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (42, 3, convert(text, N'ext_demourl         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'��ʾ��ַ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (43, 3, convert(text, N'ext_filesize        ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 50, 1, N'', N'�ļ���С', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (45, 3, convert(text, N'ext_downlinks       ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 2000, 1, N'', N'���ص�ַ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (46, 3, convert(text, N'ext_downcount       ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'�´δ���', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (47, 3, convert(text, N'ext_softtype        ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'����', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (52, 5, convert(text, N'ext_nativeplace     ' collate Chinese_PRC_CI_AS), convert(text, N'stepselect          ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'����', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (53, 5, convert(text, N'ext_infotype        ' collate Chinese_PRC_CI_AS), convert(text, N'stepselect          ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��Ϣ����', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (54, 5, convert(text, N'ext_endtime         ' collate Chinese_PRC_CI_AS), convert(text, N'datetime            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��ֹ����', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (55, 5, convert(text, N'ext_linkman         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 30, 1, N'', N'��ϵ��', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (56, 5, convert(text, N'ext_tel             ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��ϵ�绰', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (57, 5, convert(text, N'ext_email           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 100, 1, N'', N'�����ʼ�', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (58, 5, convert(text, N'ext_address         ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'��ϵ��ַ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (61, 4, convert(text, N'ext_price           ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'�г���', N'����д��ֵ,�ɱ���С������λ', 1000, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (62, 4, convert(text, N'ext_vipprice        ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'�Żݼ�', N'����д��ֵ,�ɱ���С������λ', 900, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (63, 4, convert(text, N'ext_brand           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 30, 1, N'', N'Ʒ��', N'', 800, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (64, 4, convert(text, N'ext_unit            ' collate Chinese_PRC_CI_AS), convert(text, N'select              ' collate Chinese_PRC_CI_AS), 10, 1, N'ƿ PING,�� GE,̨ TAI,�� XIANG,�� TAO,�� HE,�� JIAN,�� BEN,�� BU,���� KG,�� G', N'������λ', N'', 500, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (67, 4, convert(text, N'ext_ontime          ' collate Chinese_PRC_CI_AS), convert(text, N'datetime            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'�ϼ�ʱ��', N'', 700, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (68, 6, convert(text, N'ext_vfile           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'��Ƶ��ַ', N'', 0, N'<tr>

                                <td class="itemtitle">

                                    ~name~��

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="֧�ֱ�����Ƶ����վ����(���ſᡢ��������Ѷ����6��56��,��д��Ƶ���Ӽ���).������Ƶֻ֧��flv��ʽ, ����Ƶ�ļ��ϴ�,��ʹ�ô��ļ��ϴ������ϴ�." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" size="30" style="width:420px;" value="~text~"/>

                                    <span id="selectvideo" class="selectbtn">ѡ��</span>

                                    <a href="javascript:;" id="previewvideo" class="fancybox.iframe">Ԥ��</a>
							
                                </td>

                            </tr>')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (69, 6, convert(text, N'ext_copyright       ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��Ȩ��', N'��Ƶ�İ�Ȩ��', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (70, 6, convert(text, N'ext_star            ' collate Chinese_PRC_CI_AS), convert(text, N'select              ' collate Chinese_PRC_CI_AS), 20, 1, N'���Ƽ�,һ��,����,����,����,����', N'�Ƽ��Ǽ�', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (71, 6, convert(text, N'ext_duration        ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 20, 1, N'60', N'��Ƶʱ��', N'��Ƶ����ʱ��,��λΪ(��)', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (72, 4, convert(text, N'ext_imgs            ' collate Chinese_PRC_CI_AS), convert(text, N'ntext               ' collate Chinese_PRC_CI_AS), 20, 1, N'', N'��ƷͼƬ', N'', 0, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (73, 4, convert(text, N'ext_storage         ' collate Chinese_PRC_CI_AS), convert(text, N'int                 ' collate Chinese_PRC_CI_AS), 4, 1, N'100', N'�����', N'��Ʒ�Ŀ����,ֻ��Ϊ����', 600, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (74, 4, convert(text, N'ext_weight          ' collate Chinese_PRC_CI_AS), convert(text, N'decimal(18,2)       ' collate Chinese_PRC_CI_AS), 20, 1, N'0.00', N'��Ʒ����', N'��Ʒ������,KGΪ��λ', 350, N'')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (75, 4, convert(text, N'ext_code            ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 18, 1, N'', N'��Ʒ���', N'��Ʒ�ı��,����������,���������', 1200, N'<tr>

                                <td class="itemtitle">

                                    ~name~��

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="��Ʒ�ı��(�18���ַ�),����������,���������." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" value="~text~"/>
									����
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(9));">9λ</span>
                                    <span class="selectbtn" onclick="$(''#~field~'').val(RandNum(12));">12λ</span>
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(15));">15λ</span>
									<span class="selectbtn" onclick="$(''#~field~'').val(RandNum(18));">18λ</span>
							
                                </td>

                            </tr>')
INSERT [dbo].[sta_contypefields] ([id], [cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (76, 4, convert(text, N'ext_vfile           ' collate Chinese_PRC_CI_AS), convert(text, N'nvarchar            ' collate Chinese_PRC_CI_AS), 300, 1, N'', N'��Ƶ��ַ', N'', 0, N'<tr>

                                <td class="itemtitle">

                                    ~name~��

                                </td>

                                <td>

                                    <script type="text/javascript">$(function(){$(''#~field~'').poshytip({className: ''tip-yellowsimple'',alignTo:''target'',alignX: ''center'',alignY: ''top'', offsetX: 5,offsetY:5 });});</script><input name="~field~" type="text" id="~field~" title="֧�ֱ�����Ƶ����վ����(���ſᡢ��������Ѷ����6��56��,��д��Ƶ���Ӽ���).������Ƶֻ֧��flv��ʽ, ����Ƶ�ļ��ϴ�,��ʹ�ô��ļ��ϴ������ϴ�." class="txt" onfocus="this.className=''txt_focus'';" onblur="this.className=''txt'';" size="30" style="width:420px;" value="~text~"/>

                                    <span id="selectvideo" class="selectbtn">ѡ��</span>

                                    <a href="javascript:;" id="previewvideo" class="fancybox.iframe">Ԥ��</a>
 <script type="text/javascript">RegSelectFilePopWin("selectvideo", "��Ƶ�ļ�ѡ��", "root=/files&path=/files&filetype=mp4,flv&fullname=1&cltmed=1&fele=ext_vfile", "click");
        RegPreviewFlv("#ext_vfile", "#previewvideo");
</script>
                                </td>

                            </tr>')
SET IDENTITY_INSERT [dbo].[sta_contypefields] OFF

go



/****** ����:  Table [dbo].[sta_magazines]    �ű�����: 02/28/2013 15:33:32 ******/
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



/****** ���ò˵� ******/
delete from sta_menus 
go

SET IDENTITY_INSERT [dbo].[sta_menus] ON
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (1, N'���ݹ���', 0, 1, 1, 1, N' ', N'global/global_contentlist.aspx?type=1', N'left                ', 500)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (2, N'��Ϣ����', 1, 1, 1, 1, N'content.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (3, N'�����ĵ��б�', 2, 1, 1, 1, N'page_stack.png', N'global/global_contentlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (4, N'������ĵ�', 2, 1, 1, 1, N'verify.png', N'global/global_contentverify.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (5, N'�û�����', 0, 1, 1, 1, N'', N'global/global_userlist.aspx', N'left                ', 400)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (6, N'վ������', 0, 1, 1, 1, N'', N'global/global_siteinfo.aspx', N'left                ', 300)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (7, N'ϵͳ��չ', 0, 1, 1, 1, N'', N'global/global_pluginlist.aspx', N'left                ', 300)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (8, N'ϵͳ����', 0, 1, 1, 1, N'', N'global/global_emailsend.aspx', N'left                ', 200)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (9, N'�ĵ��������', 2, 1, 1, 1, N'add.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (10, N'�ĵ��������', 9, 1, 1, 1, N'', N'$ģ���ĵ����', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (14, N'�ĵ�����', 2, 1, 1, 1, N'archive.gif', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (15, N'�ĵ�����', 14, 1, 1, 1, N'', N'$ģ���ĵ�����', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (16, N'Ƶ������', 2, 1, 1, 1, N'buildings.png', N'', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (17, N'Ƶ���б�', 16, 1, 1, 1, N'', N'global/global_channelist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (18, N'Ƶ�����', 16, 1, 1, 1, N'', N'global/global_channeladd.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (19, N'Ƶ���������', 16, 1, 1, 1, N'', N'global/global_channelbatchadd.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (20, N'�����', 2, 1, 1, 1, N'gather.gif', N'', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (21, N'������', 20, 1, 1, 1, N'', N'global/global_congroups.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (25, N'Ƶ����', 20, 1, 1, 1, N'', N'global/global_congroupchannels.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (26, N'Ƶ��ģ��', 2, 1, 1, 1, N'page_stack.png', N'', N'main                ', 49920)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (28, N'����ģ�͹���', 26, 1, 1, 1, N'cmodel.gif', N'global/global_contypes.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (34, N'��ҳģ�͹���', 26, 1, 1, 1, N'page.png', N'global/global_pagelist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (38, N'����ά��', 2, 1, 1, 1, N'page_world.png', N'', N'main                ', 49910)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (39, N'���ݱ�ǩ', 38, 1, 1, 1, N'tags.gif', N'global/global_tags.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (40, N'�ظ��ĵ����', 38, 1, 1, 1, N'', N'global/global_repeatconcheck.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (41, N'���ֶ������滻', 38, 1, 1, 1, N'shading.png', N'global/global_dbfieldreplace.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (42, N'�ҷ���������', 2, 1, 1, 1, N'', N'global/global_contentmine.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (43, N'���۹���', 2, 1, 1, 1, N'comment.png', N'global/global_comments.aspx', N'main                ', 49890)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (44, N'���ݻ���վ', 2, 1, 1, 1, N'trash.gif', N'global/global_contentrecycle.aspx', N'main                ', 49880)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (45, N'��̬����', 1, 1, 1, 1, N'publish.gif', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (46, N'��ҳ����', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?start=yes', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (47, N'Ƶ������', 45, 1, 1, 1, N'', N'global/global_createchannels.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (48, N'�ĵ�����', 45, 1, 1, 1, N'', N'global/global_createcontents.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (49, N'ר�ⷢ��', 45, 1, 1, 1, N'', N'global/global_createspecials.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (50, N'��ҳ����', 45, 1, 1, 1, N'', N'global/global_createpages.aspx', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (51, N'һ������', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?type=onekey', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (52, N'��������', 1, 1, 1, 1, N'file.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (53, N'�����ϴ�', 52, 1, 1, 1, N'', N'global/global_attachmentuploadpre.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (54, N'��������', 52, 1, 1, 1, N'', N'global/global_attachments.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (55, N'�ļ������', 52, 1, 1, 1, N'', N'global/global_filesexplore.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (56, N'���ܹ���', 1, 1, 1, 1, N'function.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (57, N'ͶƱ����', 56, 1, 1, 1, N'vote.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (58, N'������', 56, 1, 1, 1, N'advertisement.gif', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (59, N'���ӹ���', 56, 1, 1, 1, N'innerlink.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (60, N'���ݲɼ�', 56, 1, 1, 1, N'group.gif', N'', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (61, N'����ͶƱ', 57, 1, 1, 1, N'', N'global/global_voteadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (62, N'ͶƱ����', 57, 1, 1, 1, N'', N'global/global_votelist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (63, N'ͶƱ����', 57, 1, 1, 1, N'', N'global/global_votecates.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (64, N'ͶƱ��¼', 57, 1, 1, 1, N'', N'global/global_voterecords.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (65, N'������', 58, 1, 1, 1, N'', N'global/global_adadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (66, N'������', 58, 1, 1, 1, N'', N'global/global_adlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (67, N'�������', 59, 1, 1, 1, N'', N'global/global_flinkadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (68, N'�������', 59, 1, 1, 1, N'', N'global/global_flinkcls.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (69, N'���ӹ���', 59, 1, 1, 1, N'', N'global/global_flinks.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (70, N'���ݿ�ɼ�', 60, 1, 1, 1, N'', N'global/global_dbcollect.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (71, N'վ����Ϣ�ɼ�', 60, 1, 1, 1, N'', N'global/global_webcollect.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (72, N'ģ�����', 1, 1, 1, 1, N'template.gif', N'', N'main                ', 49960)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (73, N'ģ�����', 72, 1, 1, 1, N'', N'global/global_templatelist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (74, N'ȫ������', 6, 1, 1, 1, N'site.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (75, N'վ����Ϣ', 74, 1, 1, 1, N'', N'global/global_siteinfo.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (76, N'��������', 74, 1, 1, 1, N'', N'global/global_baseset.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (77, N'�����Ż�', 74, 1, 1, 1, N'', N'global/global_optimizeset.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (78, N'��������', 74, 1, 1, 1, N'', N'global/global_interactset.aspx', N'main                ', 49970)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (80, N'��������', 74, 1, 1, 1, N'', N'global/global_attachmentset.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (81, N'ͼƬˮӡ����', 74, 1, 1, 1, N'', N'global/global_waterset.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (82, N'��������', 74, 1, 1, 1, N'', N'global/global_emailset.aspx', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (83, N'ͶƱ����', 74, 1, 1, 1, N'', N'global/global_voteset.aspx', N'main                ', 49920)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (84, N'��ȫ����', 74, 1, 1, 1, N'', N'global/global_safeset.aspx', N'main                ', 49910)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (85, N'�û�����', 5, 1, 1, 1, N'user.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (86, N'�û����', 85, 1, 1, 1, N'', N'global/global_useradd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (87, N'�û��б�', 85, 1, 1, 1, N'', N'global/global_userlist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (88, N'�û������', 5, 1, 1, 1, N'group.png', N'', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (89, N'����û���', 88, 1, 1, 1, N'', N'global/global_usergroupadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (91, N'�û������', 88, 1, 1, 1, N'', N'global/global_usergrouplist.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (92, N'ϵͳ�����', 88, 1, 1, 1, N'', N'global/global_sysgrouplist.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (100, N'�˵�����', 5, 1, 1, 1, N'tools.gif', N'', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (102, N'���ܲ˵�', 100, 1, 1, 1, N'', N'global/global_menumanage.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (103, N'�˵�����', 100, 1, 1, 1, N'', N'global/global_menulist.aspx', N'main                ', 49980)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (104, N'ʵ�ù���', 8, 1, 1, 1, N'function.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (105, N'�ʼ�Ⱥ��', 104, 1, 1, 1, N'email.gif', N'global/global_emailsend.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (106, N'��ʱ����', 104, 1, 1, 1, N'task.gif', N'global/global_schedulemanage.aspx', N'main                ', 49990)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (108, N'Sitemap����', 104, 1, 1, 1, N'innerlink.gif', N'tools/sitemapmake.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (109, N'JS����ѹ��', 104, 1, 1, 1, N'script.png', N'global/global_jsmin.aspx', N'main                ', 49890)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (110, N'HtmlתJS����', 104, 1, 1, 1, N'html.gif', N'global/global_html2js.aspx', N'main                ', 49840)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (112, N'���ݿ����', 8, 1, 1, 1, N'database.png', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (113, N'����SQL�ű�', 112, 1, 1, 1, N'', N'global/global_runsql.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (114, N'���ݱ�ṹ�鿴', 112, 1, 1, 1, N'form.gif', N'global/global_dbtableview.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (115, N'�������ݿ�', 112, 1, 1, 1, N'backup.gif', N'global/global_databasebackup.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (116, N'�������ݿ�', 112, 1, 1, 1, N'machine.gif', N'global/global_databaseshrink.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (117, N'������־', 8, 1, 1, 1, N'log.gif', N'', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (118, N'������־', 117, 1, 1, 1, N'', N'global/global_adminoperatelogs.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (119, N'������־', 117, 1, 1, 1, N'', N'global/global_admintasklogs.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (120, N'ϵͳ��־', 117, 1, 1, 1, N'', N'global/global_adminlogs.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (121, N'������־', 117, 1, 1, 1, N'', N'global/global_errorlogs.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (123, N'ϵͳ��չ', 7, 1, 1, 1, N'integration.gif', N'', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (124, N'URL��̬��', 123, 1, 1, 1, N'', N'global/global_urlstaticize.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (125, N'��չ����', 7, 1, 1, 1, N'arrow_inout.png', N'', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (126, N'������չ', 125, 1, 1, 1, N'', N'global/global_pluginlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (127, N'��չ������', 125, 1, 1, 1, N'', N'global/global_pluginadd.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (128, N'���ݹ���', 7, 1, 1, 1, N'data.png', N'', N'main                ', 49900)
GO
print 'Processed 100 total records'
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (129, N'��չ���ݹ���', 128, 1, 1, 1, N'', N'$��չ���ݹ���', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (139, N'Ȩ������', 92, 1, 2, 1, N'', N'global/global_menuauthority.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (141, N'��ͨ�ĵ����', 10, 1, 2, 1, N'', N'global/global_contentadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (142, N'ͼ�����', 10, 1, 2, 1, N'', N'global/global_photoadd.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (143, N'������', 10, 1, 2, 1, N'', N'global/global_softadd.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (144, N'ר�����', 10, 1, 2, 1, N'', N'global/global_specialadd.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (145, N'��ͨ�ĵ�����', 15, 1, 2, 1, N'', N'global/global_contentlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (146, N'ͼ������', 15, 1, 2, 1, N'', N'global/global_photolist.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (147, N'�������', 15, 1, 2, 1, N'', N'global/global_softlist.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (148, N'ר�����', 15, 1, 2, 1, N'', N'global/global_speciallist.aspx', N'main                ', 49850)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (149, N'ר�����ݹ���', 148, 1, 2, 1, N'', N'global/global_specialcontents.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (150, N'�����༭', 54, 1, 2, 1, N'', N'global/global_attachmentedit.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (151, N'�����ϴ�', 53, 1, 2, 1, N'', N'global/global_attachmentupload.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (152, N'URL��̬�����', 124, 1, 2, 1, N'', N'global/global_urlstaticizeadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (153, N'ͶƱѡ��', 61, 1, 2, 1, N'', N'global/global_voteitems.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (154, N'ͶƱ�������', 63, 1, 2, 1, N'', N'global/global_votecateadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (155, N'���ݿ�ɼ����', 70, 1, 2, 1, N'', N'global/global_dbcollectadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (156, N'վ����Ϣ�ɼ����', 71, 1, 2, 1, N'', N'global/global_webcollectadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (157, N'ģ���ļ�����', 73, 1, 2, 1, N'', N'global/global_tplfiles.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (158, N'����ģ�����', 28, 1, 2, 1, N'', N'global/global_contypeadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (159, N'��ҳģ�����', 34, 1, 2, 1, N'', N'global/global_pageadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (160, N'���������', 21, 1, 2, 1, N'', N'global/global_congroupadd.aspx?type=0', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (161, N'����������ά��', 21, 1, 2, 1, N'', N'global/global_congroupconedit.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (162, N'Ƶ�������', 25, 1, 2, 1, N'', N'global/global_congroupadd.aspx?type=1', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (163, N'�˵����', 103, 1, 2, 1, N'', N'global/global_menuadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (164, N'�������͹���', 26, 1, 1, 1, N'doc_shred.png', N'global/global_selects.aspx', N'main                ', 49940)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (165, N'���������', 164, 1, 2, 1, N'', N'global/global_selectlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (166, N'������Ϣ���', 10, 1, 2, 1, N'', N'global/global_infoadd.aspx', N'main                ', 49800)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (167, N'������Ϣ����', 15, 1, 2, 1, N'', N'global/global_infolist.aspx', N'main                ', 49800)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (168, N'��Ʒ���', 10, 1, 2, 1, N'', N'global/global_productadd.aspx', N'main                ', 49750)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (169, N'���ļ��ϴ�', 52, 1, 1, 1, N'', N'global/global_attachmentbigfile.aspx', N'main                ', 49985)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (170, N'��Ƶ���', 10, 1, 2, 1, N'', N'global/global_videoadd.aspx', N'main                ', 49700)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (171, N'��ͼ����', 45, 1, 1, 1, N'', N'global/global_createprogress.aspx?type=sitemap&start=yes', N'main                ', 49955)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (172, N'��Աע��', 74, 1, 1, 1, N'', N'global/global_userregset.aspx', N'main                ', 48000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (173, N'����ͶƱ', 123, 1, 1, 1, N'', N'com/vote/votelist.aspx', N'main                ', 60000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (174, N'ͶƱ���', 173, 1, 2, 1, N'', N'com/vote/voteadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (175, N'ͶƱ�鿴', 173, 1, 2, 1, N'', N'com/vote/voteviewaspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (176, N'��ʱ����ά��', 106, 1, 2, 1, N'', N'global/global_scheduleadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (177, N'��չ����װ', 125, 1, 1, 1, N'', N'global/global_pluginzip.aspx', N'main                ', 49955)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (178, N'Ƶ������״̬', 47, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=channel', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (179, N'�ĵ�����״̬', 48, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=content', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (180, N'ר�ⷢ��״̬', 49, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=special', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (181, N'��ҳ����״̬', 50, 1, 2, 1, N'', N'global/global_createprogress.aspx?type=page', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (182, N'�û��޸�', 87, 1, 2, 1, N'', N'global/global_useredit.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (183, N'��Ʒ����', 15, 1, 2, 1, N'', N'global/global_productadd.aspx', N'main                ', 49750)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (184, N'�ɼ�ѡ��', 74, 1, 1, 1, N'', N'global/global_collectset.aspx', N'main                ', 49810)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (185, N'����ѡ��', 74, 1, 1, 1, N'', N'global/global_otherset.aspx', N'main                ', 47760)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (186, N'�Զ������', 123, 1, 1, 1, N'', N'com/ctmvariable/list.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (187, N'�Զ���������', 186, 1, 2, 1, N'', N'com/ctmvariable/add.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (188, N'֧������', 5, 1, 1, 1, N'money_yen.png', N'', N'main                ', 49930)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (189, N'��������', 188, 1, 1, 1, N'', N'pay/orderlist.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (190, N'֧����ʽ', 188, 1, 1, 1, N'', N'pay/paylist.aspx', N'main                ', 49950)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (191, N'���ͷ�ʽ', 188, 1, 1, 1, N'', N'pay/deliverylist.aspx', N'main                ', 49900)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (192, N'���ͷ�ʽ���', 191, 1, 2, 1, N'', N'pay/deliveryadd.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (194, N'֧����ʽ����', 190, 1, 2, 1, N'', N'pay/payset.aspx', N'main                ', 50000)
INSERT [dbo].[sta_menus] ([id], [name], [parentid], [system], [pagetype], [type], [icon], [url], [target], [orderid]) VALUES (195, N'�����޸�', 189, 1, 2, 1, N'', N'pay/orderset.aspx', N'main                ', 50000)
SET IDENTITY_INSERT [dbo].[sta_menus] OFF