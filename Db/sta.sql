DROP PROC [dbo].[sta_shrinklog]  
GO

CREATE PROCEDURE [dbo].[sta_shrinklog]  
@DBName  nchar(50) 
AS
Begin
	exec('BACKUP LOG ['+@DBName+']  WITH NO_LOG')
	exec('DBCC  SHRINKDATABASE(['+@DBName+'])')
End

GO

DROP PROC sta_pageview
GO

CREATE PROCEDURE [dbo].[sta_pageview]
@tbname     nvarchar(128),             --要分页显示的表名
@FieldKey   nvarchar(50),      --用于定位记录的主键(惟一键)字段
@PageCurrent int=1,              --要显示的页码
@PageSize   int=10,              --每页的大小(记录数)
@FieldShow  nvarchar(1000)='',   --以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段
@FieldOrder  nvarchar(1000)='',  --以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC用于指定排序顺序
@Where     nvarchar(1000)='',    --查询条件
@PageCount  int OUTPUT,          --总页数
@RecordCount int OUTPUT          --记录数
AS
SET NOCOUNT ON

--分页字段检查
IF ISNULL(@FieldKey,N'')=''
BEGIN
    RAISERROR(N'分页处理需要主键（或者惟一键）',1,16)
    RETURN
END

--其他参数检查及规范
IF ISNULL(@PageCurrent,0)<1 SET @PageCurrent=1
IF ISNULL(@PageSize,0)<1 SET @PageSize=10
IF ISNULL(@FieldShow,N'')=N'' SET @FieldShow=N'*'
IF ISNULL(@FieldOrder,N'')=N''
    SET @FieldOrder=N''
ELSE
    SET @FieldOrder=N'ORDER BY '+LTRIM(@FieldOrder)
IF ISNULL(@Where,N'')=N''
    SET @Where=N''
ELSE
    SET @Where=N'WHERE ('+@Where+N')'

--如果@PageCount为NULL值,则计算总页数(这样设计可以只在第一次计算总页数,以后调用时,把总页数传回给存储过程,避免再次计算总页数,对于不想计算总页数的处理而言,可以给@PageCount赋值)
IF @PageCount IS NULL
BEGIN
    DECLARE @sql nvarchar(4000)
    SET @sql=N'SELECT @RecordCount=COUNT(*)'
        +N' FROM '+@tbname
        +N' '+@Where
    EXEC sp_executesql @sql,N'@RecordCount int OUTPUT',@RecordCount OUTPUT
    SET @PageCount=(@RecordCount+@PageSize-1)/@PageSize
END

--计算分页显示的TOPN值
DECLARE @TopN varchar(20),@TopN1 varchar(20)
SELECT @TopN=@PageSize,
    @TopN1=@PageCurrent*@PageSize

--第一页直接显示
IF @PageCurrent=1
    EXEC(N'SELECT TOP '+@TopN
        +N' '+@FieldShow
        +N' FROM '+@tbname
        +N' '+@Where
        +N' '+@FieldOrder)
ELSE
BEGIN
    SELECT @TopN=@TopN1-@PageSize
    --执行查询
    EXEC(N'SET ROWCOUNT '+@TopN1
        +N' SELECT '+@FieldKey + ' as tempfk'
        +N' INTO # FROM '+@tbname
        +N' '+@Where
        +N' '+@FieldOrder
        +N' SET ROWCOUNT '+@TopN
        +N' DELETE FROM #'
        +N' SELECT '+@FieldShow
        +N' FROM '+@tbname
        +N' WHERE EXISTS(SELECT * FROM # WHERE tempfk ='+@FieldKey 
        +N') '+@FieldOrder)
END

GO

--分页联表查询测试
--declare @pagecount int,@recordcount int
--exec sta_pageview 
--	@tbname = N'sta_contents inner join sta_channels on channelid = sta_channels.id',
--	@fieldkey = 'sta_contents.id',
--	@fieldshow = 'parentid,sta_channels.name as nn,*',
--	@pagesize = 100,
--    @pagecount =@pagecount out,
--	@recordcount = @recordcount out
--
--declare @pagecount int,@recordcount int
--exec sta_pageview 
--	@tbname = N'[sta_contents] inner join [sta_extphotos] on [sta_extphotos].[cid] = [sta_contents].[id]',
--	@fieldkey = 'sta_contents.id',
--	@fieldshow = 'id,[sta_contents].typeid,title,ext_imgs',
--	@pagesize = 3,
--	@PageCurrent =1,
--	@fieldorder = 'sta_contents.id asc',
--	@where = N'sta_contents.id > 63',
--    @pagecount =@pagecount out,
--	@recordcount = @recordcount out
--
--
--
--declare @pagecount int,@recordcount int
--exec sta_pageview 
--	@tbname = N'sta_contents inner join sta_contypes on typeid = sta_contypes.id',
--	@fieldkey = 'id',
--	@fieldshow = 'sta_contents.*',
--	@pagesize = 100,
--    @pagecount =@pagecount out,
--	@recordcount = @recordcount out

drop proc sta_specontentpage
GO

 ---查询专题内容
CREATE PROC [dbo].[sta_specontentpage] 
	@specid int, --0则忽略
	@specgroupid int, --0则忽略
	@pagesize int, 
	@pageindex int,
	@fields nvarchar(300), --要获取内容的字段   如：id,newname
	@where nvarchar(300), --搜索内容的条件 没有请留空 
	@OrderBy nvarchar(100), --如何排序  如：id desc,addtime desc 默认按时间降序排序
	@pagecount int out,
	@recordcount int out
AS
BEGIN
	DECLARE @where1 nvarchar(300),@where2 nvarchar(1000),@nottopcount int
	IF ISNULL(@specid,0)!=0
		SET @where1 = N' AND specid=' + CAST(@specid as nvarchar(20)) 
	IF ISNULL(@specgroupid,0) != 0
		SET @where1 = @where1 + N' AND groupid = ' + CAST(@specgroupid as nvarchar(20))
	SET @where1 = ISNULL(@where1,'') 
	IF ISNULL(@OrderBy,N'') = ''
		SET	@OrderBy = 'addtime DESC' 
	IF ISNULL(@where,N'') != ''
		SET @where = ' AND '+ @where 
	SET @where2 =  N' EXISTS (SELECT * FROM [sta_specontents] where [sta_contents].[id] = [sta_specontents].[contentid] ' +@where1+') ' +@where 
	IF @pagecount IS NULL
	BEGIN
		DECLARE @sql nvarchar(4000)
		SET @sql=N'SELECT @recordcount=COUNT(*)'
			+N' FROM [sta_contents] WHERE' + @where2
		EXEC sp_executesql @sql,N'@recordcount int OUTPUT',@recordcount OUTPUT
		SET @pagecount=(@recordcount+@pagesize-1)/@pagesize
	END

	--计算分页显示的TOPN值
	DECLARE @TopN varchar(20),@TopN1 varchar(20)
	SELECT @TopN=@PageSize,
		@TopN1=@Pageindex*@PageSize

	---执行查询
	IF @pageindex = 1
		BEGIN
			EXEC(N'SELECT TOP ' + @TopN + N' '+ @fields 
			+N' FROM [sta_contents] WHERE' + @where2 +' ORDER BY ' +@OrderBy)
		END
	ELSE 
		BEGIN
			SELECT @TopN=@TopN1-@PageSize
			EXEC(N'SET ROWCOUNT '+@TopN1
				+N' SELECT [sta_contents].[id] as tempfk'
				+N' INTO # FROM [sta_contents] WHERE' + @where2 +' ORDER BY ' +@OrderBy
				+N' SET ROWCOUNT '+@TopN
				+N' DELETE FROM #'
				+N' SELECT '+ @fields 
				+N' FROM [sta_contents]'
				+N' WHERE EXISTS(SELECT * FROM # WHERE tempfk = sta_contents.id'
				+N') ORDER BY '+@orderby)
		END
END
GO

--测试
--declare @pagecount int,@recordcount int
--exec sta_specontentpage
--	@specid = 65,
--	@specgroupid = 12,
--	@pagesize = 30, 
--	@pageindex = 1,
--	@fields = 'id', --要获取内容的字段   如：id,newname
--	@where = '', --搜索内容的条件 没有请留空 
--	@OrderBy = 'addtime desc', 
--    @pagecount =@pagecount out,
--	@recordcount = @recordcount out

drop proc sta_createcontent
GO

CREATE PROCEDURE [dbo].[sta_createcontent]  
	@id int,
	@typeid smallint,
	@typename nvarchar(20),
	@channelfamily nchar(200),
	@channelid int,
	@channelname nvarchar(100),
	@ExtChannels nvarchar(100),
	@title nvarchar(255),
	@subtitle nvarchar(255),
	@addtime datetime,
	@updatetime datetime,
	@color char(7),
	@property nchar(50),
	@adduser int,
    @addusername nchar(20),
	@lastedituser int,
	@lasteditusername nchar(20),
	@author nvarchar(20),
	@source nvarchar(20),
	@img nchar(300),
	@url nchar(300),
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@savepath nvarchar(100),
	@filename char(100),
	@template char(50),
	@content ntext,
	@status tinyint,
	@viewgroup nvarchar(200),
	@iscomment tinyint,
	@ishtml tinyint,
	@click int,
	@orderid int,
	@diggcount int,
	@stampcount int,
	@commentcount int,
	@credits int,
	@relates nvarchar(300)
AS
INSERT INTO [sta_contents] ([typeid], [typename], [channelfamily], [channelid], [channelname], [ExtChannels], [title], [subtitle], [addtime], [updatetime], [color], [property], [adduser], [addusername], [lastedituser], [lasteditusername], [author], [source], [img], [url], [seotitle], [seokeywords], [seodescription], [savepath], [filename], [template], [content], [status], [viewgroup], [iscomment], [ishtml], [click], [orderid], [diggcount], [stampcount], [commentcount], [credits], [relates]) VALUES (@typeid, @typename, @channelfamily, @channelid, @channelname, @ExtChannels, @title, @subtitle, @addtime, @updatetime, @color, @property, @adduser, @addusername, @lastedituser, @lasteditusername, @author, @source, @img, @url, @seotitle, @seokeywords, @seodescription, @savepath, @filename, @template, @content, @status, @viewgroup, @iscomment, @ishtml, @click, @orderid, @diggcount, @stampcount, @commentcount, @credits, @relates);SELECT SCOPE_IDENTITY()

GO

--根据文档ID调用相关内容
drop proc [sta_getrelateconlist]
GO

CREATE PROCEDURE [dbo].[sta_getrelateconlist]
	@id int,
	@fields nvarchar(300),
	@count int = 10
AS
	DECLARE @strcount nvarchar(25),@strid nvarchar(25)
	SELECT @strcount = @count, @strid = @id
    EXEC(N'SELECT TOP '+ @strcount  + N' ' + @fields
        +N' FROM [sta_contents] WHERE EXISTS '
        +N' (SELECT * FROM [sta_contags] WHERE [tagid] in (SELECT [tagid] FROM [sta_contags] WHERE '
        +N' [contentid] = ' + @strid + ') AND ([contentid]>' + @strid + ' or [contentid]<' + @strid + ') AND'
		+N' [contentid] = [sta_contents].[id]) ORDER BY [id] DESC')

GO

DROP PROC [dbo].[sta_deletecontent]

GO

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

GO

drop proc sta_updatecontent
GO

CREATE PROCEDURE [dbo].[sta_updatecontent]
	@id int,
	@typeid smallint,
	@typename nvarchar(20),
	@channelfamily nchar(200),
	@channelid int,
	@channelname nvarchar(100),
	@ExtChannels nvarchar(100),
	@title nvarchar(255),
	@subtitle nvarchar(255),
	@addtime datetime,
	@updatetime datetime,
	@color char(7),
	@property nchar(50),
	@adduser int,
    @addusername nchar(20),
	@lastedituser int,
	@lasteditusername nchar(20),
	@author nvarchar(20),
	@source nvarchar(20),
	@img nchar(300),
	@url nchar(300),
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@savepath nvarchar(100),
	@filename char(100),
	@template char(50),
	@content ntext,
	@status tinyint,
	@viewgroup nvarchar(200),
	@iscomment tinyint,
	@ishtml tinyint,
	@click int,
	@orderid int,
	@diggcount int,
	@stampcount int,
	@commentcount int,
	@credits int,
	@relates nvarchar(300)
AS
UPDATE [sta_comments] SET [contitle] = @title WHERE [cid] = @id
UPDATE [sta_contents]
SET [typeid] = @typeid, [commentcount] = @commentcount, [channelfamily] = @channelfamily, updatetime = @updatetime, [typename] = @typename, [addusername] = @addusername, [lasteditusername] = @lasteditusername, [channelid] = @channelid, [channelname] = @channelname, [ExtChannels] = @ExtChannels, [title] = @title, [subtitle] = @subtitle, [addtime] = @addtime, [color] = @color, [property] = @property, [adduser] = @adduser, [lastedituser] = @lastedituser, [author] = @author, [source] = @source, [img] = @img, [url] = @url, [seotitle] = @seotitle, [seokeywords] = @seokeywords, [seodescription] = @seodescription, [savepath] = @savepath, [filename] = @filename, [template] = @template, [content] = @content, [status] = @status, [viewgroup] = @viewgroup, [iscomment] = @iscomment, [ishtml] = @ishtml, [click] = @click, [orderid] = @orderid, [diggcount] = @diggcount, [stampcount] = @stampcount, [credits] = @credits, [relates] = @relates
WHERE [sta_contents].[id] = @id

GO


DROP PROC [sta_createtag]
GO

CREATE PROCEDURE [dbo].[sta_createtag]  -- 添加内容标签  
	@name nvarchar(20), --标签名
	@cid int --内容Id
AS
DECLARE @tid int
SELECT @tid = ISNULL(id,0) FROM [sta_tags] WHERE [NAME] = @name
IF ISNULL(@tid,0) > 0
	BEGIN
		IF @cid > 0
			BEGIN
				DECLARE @rtagcount int
				SELECT @rtagcount = ISNULL(COUNT(*),0) FROM [sta_contags] WHERE [contentid] = @cid AND tagid = @tid
				IF @rtagcount = 0
					BEGIN
						INSERT INTO [sta_contags] ([contentid], [tagid]) VALUES (@cid, @tid)
						UPDATE [sta_tags] SET [count] = [count] + 1 WHERE id = @tid
					END
			END
	END
ELSE
	BEGIN
		IF @cid > 0
			BEGIN
				INSERT INTO [sta_tags] ([name], [count], [addtime]) VALUES (@name, 1, GETDATE())
				SELECT @tid = ISNULL(id,0) FROM [sta_tags] WHERE [NAME] = @name
				INSERT INTO [sta_contags] ([contentid], [tagid]) VALUES (@cid,@tid )
			END
		ELSE
			INSERT INTO [sta_tags] ([name], [count], [addtime]) VALUES (@name, 0, GETDATE())
	END

GO

DROP PROC [sta_deletetag]
GO

CREATE PROCEDURE [dbo].[sta_deletetag] --直接删除标签
	@name nvarchar(20) --标签名
AS
DECLARE @tid int
SELECT @tid = ISNULL(id,0) FROM [sta_tags] WHERE [NAME] = @name
IF ISNULL(@tid,0) > 0
	BEGIN
		DELETE FROM [sta_contags] WHERE [tagid] = @tid
		DELETE FROM [sta_tags] WHERE [id] = @tid
	END

GO

DROP PROC [sta_deletetagbytid]
GO

CREATE PROCEDURE [dbo].[sta_deletetagbytid] --直接删除标签
	@tid int
AS
BEGIN
	DELETE FROM [sta_contags] WHERE [tagid] = @tid
	DELETE FROM [sta_tags] WHERE [id] = @tid
END

GO

DROP PROCEDURE [sta_deletetagbycid]
GO

CREATE PROCEDURE [dbo].[sta_deletetagbycid] --根据内容删除单个标签
	@name nvarchar(20), 
	@cid int
AS
DECLARE @tid int
SELECT @tid = ISNULL(id,0) FROM [sta_tags] WHERE [name] = @name
IF ISNULL(@tid,0) > 0
	BEGIN
		UPDATE [sta_tags] SET [count] = [count]-1 WHERE [id] = @tid
		DELETE FROM [sta_contags] WHERE [tagid] = @tid AND [contentid] = @cid
	END

GO

DROP PROCEDURE [sta_deletetagsbycid]
GO

CREATE PROCEDURE [dbo].[sta_deletetagsbycid] --根据内容删除标签
	@cid int
AS
DECLARE @tid int
UPDATE [sta_tags] SET [count] = [count]-1 WHERE EXISTS (SELECT * FROM [sta_contags] WHERE [id] = [tagid] AND [contentid] = @cid)
DELETE FROM [sta_contags] WHERE [contentid] = @cid

GO

drop proc [sta_getagsbycid]
GO

CREATE PROCEDURE [dbo].[sta_getagsbycid] --获取内容的所有标签
	@cid int
AS
SELECT [id],[name],[count] FROM [sta_tags] WHERE EXISTS (SELECT * FROM [sta_contags] WHERE [contentid] = @cid AND [id] = [tagid])

GO

drop proc sta_createchannel
GO

CREATE PROCEDURE [dbo].[sta_createchannel]  --添加频道
	@id int,
	@typeid smallint,
	@parentid int,
	@name nvarchar(255),
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
	@listcount int,
	@viewgroup nvarchar(300),
	@viewcongroup nvarchar(300),
	@ipdenyaccess nvarchar(500),
	@ipaccess nvarchar(500)
AS
INSERT INTO [sta_channels] ([typeid], [parentid], [name], [savepath], [filename], [ctype], [img], [addtime], [covertem], [listem], [contem], [conrule], [listrule], [seotitle], [seokeywords], [seodescription], [moresite], [siteurl], [content], [ispost], [ishidden], [orderid], [viewgroup], [viewcongroup], [listcount], [ipdenyaccess], [ipaccess]) VALUES (@typeid, @parentid, @name, @savepath, @filename, @ctype, @img, @addtime, @covertem, @listem, @contem, @conrule, @listrule, @seotitle, @seokeywords, @seodescription, @moresite, @siteurl, @content, @ispost, @ishidden, @orderid, @viewgroup, @viewcongroup, @listcount, @ipdenyaccess, @ipaccess);SELECT SCOPE_IDENTITY()

GO

drop proc sta_updatechannel
GO

CREATE PROCEDURE [dbo].[sta_updatechannel]  --修改频道
	@id int,
	@typeid smallint,
	@parentid int,
	@name nvarchar(255),
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
	@listcount int,
	@viewgroup nvarchar(300),
	@viewcongroup nvarchar(300),
	@ipdenyaccess nvarchar(500),
	@ipaccess nvarchar(500)
AS
UPDATE [sta_contents]
SET [channelname] = @name, [viewgroup] = @viewcongroup WHERE [channelid] = @id

UPDATE [sta_dbcollects]
SET [channelname] = @name WHERE [channelid] = @id

UPDATE [sta_webcollects]
SET [channelname] = @name WHERE [channelid] = @id

UPDATE [sta_channels]
SET [typeid] = @typeid, [listcount] = @listcount, [img] = @img, [parentid] = @parentid, [name] = @name, [savepath] = @savepath, [filename] = @filename, [ctype] = @ctype, [addtime] = @addtime, [covertem] = @covertem, [listem] = @listem, [contem] = @contem, [conrule] = @conrule, [listrule] = @listrule, [seotitle] = @seotitle, [seokeywords] = @seokeywords, [seodescription] = @seodescription, [moresite] = @moresite, [siteurl] = @siteurl, [content] = @content, [ispost] = @ispost, [ishidden] = @ishidden, [orderid] = @orderid, [viewgroup] = @viewgroup,[viewcongroup] = @viewcongroup, [ipdenyaccess] = @ipdenyaccess,[ipaccess] = @ipaccess
WHERE [sta_channels].[id] = @id

GO

drop proc [dbo].[sta_createcontype] 
GO

CREATE PROCEDURE [dbo].[sta_createcontype]  
	@id smallint,
	@open tinyint,
	@system tinyint,
	@ename nchar(20),
	@name nvarchar(20),
	@maintable char(30),
	@extable char(30),
	@addtime datetime,
	@fields nvarchar(1000),
	@bgaddmod nvarchar(50),
	@bgeditmod nvarchar(50),
	@bglistmod nvarchar(50),
	@addmod nvarchar(50),
	@editmod nvarchar(50),
	@listmod nvarchar(50),
	@orderid int
AS
INSERT INTO [sta_contypes] ([orderid], [open], [system], [ename], [name], [maintable], [extable], [addtime], [fields], [bgaddmod], [bgeditmod], [bglistmod], [addmod], [editmod], [listmod]) VALUES (@orderid, @open, @system, @ename, @name, @maintable, @extable, @addtime, @fields, @bgaddmod, @bgeditmod, @bglistmod, @addmod, @editmod, @listmod);SELECT SCOPE_IDENTITY()

GO

drop proc [dbo].[sta_updatecontype]
GO

CREATE PROCEDURE [dbo].[sta_updatecontype]
	@id smallint,
	@open tinyint,
	@system tinyint,
	@ename nchar(20),
	@name nvarchar(20),
	@maintable char(30),
	@extable char(30),
	@addtime datetime,
	@fields nvarchar(1000),
	@bgaddmod nvarchar(50),
	@bgeditmod nvarchar(50),
	@bglistmod nvarchar(50),
	@addmod nvarchar(50),
	@editmod nvarchar(50),
	@listmod nvarchar(50),
	@orderid int
AS
UPDATE [sta_contents] SET [typename] = @name WHERE [typeid] = @id
UPDATE [sta_contypes]
SET [open] = @open, [system] = @system, [ename] = @ename, [name] = @name, [maintable] = @maintable, [extable] = @extable, [addtime] = @addtime, [fields] = @fields, [bgaddmod] = @bgaddmod, [bgeditmod] = @bgeditmod, [bglistmod] = @bglistmod, [addmod] = @addmod, [editmod] = @editmod, [listmod] = @listmod, [orderid] = @orderid
WHERE [sta_contypes].[id] = @id

GO

drop proc sta_createcontypefield
GO

CREATE PROCEDURE [dbo].[sta_createcontypefield]  
	@id int,
	@cid int,
	@fieldname char(20),
	@fieldtype char(20),
	@length int,
	@isnull tinyint,
	@defvalue nvarchar(200),
	@desctext nvarchar(30),
	@tiptext nvarchar(200),
	@orderid int,
	@vinnertext nvarchar(2000)
AS
INSERT INTO [sta_contypefields] ([cid], [fieldname], [fieldtype], [length], [isnull], [defvalue], [desctext], [tiptext], [orderid], [vinnertext]) VALUES (@cid, @fieldname, @fieldtype, @length, @isnull, @defvalue, @desctext, @tiptext,@orderid, @vinnertext);SELECT SCOPE_IDENTITY()

GO

drop proc sta_updatecontypefield
GO

CREATE PROCEDURE [dbo].[sta_updatecontypefield]
	@id int,
	@cid int,
	@fieldname char(20),
	@fieldtype char(20),
	@length int,
	@isnull tinyint,
	@defvalue nvarchar(200),
	@desctext nvarchar(30),
	@tiptext nvarchar(200),
	@orderid int,
	@vinnertext nvarchar(2000)
AS
UPDATE [sta_contypefields]
SET [cid] = @cid, [tiptext] = @tiptext,[fieldname] = @fieldname, [fieldtype] = @fieldtype, [length] = @length, [isnull] = @isnull, [defvalue] = @defvalue, [desctext] = @desctext, [orderid] = @orderid, [vinnertext] = @vinnertext 
WHERE [sta_contypefields].[id] = @id

GO

drop proc [dbo].[sta_createpage] 
GO

CREATE PROCEDURE [dbo].[sta_createpage]  
	@id int,
	@name nchar(50),
	@alikeid nvarchar(20),
	@addtime datetime,
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@ishtml tinyint,
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@template char(50),
	@content ntext,
	@orderid int
AS
INSERT INTO [sta_pages] ([name], [alikeid], [addtime], [seotitle], [seokeywords], [seodescription], [ishtml], [savepath], [filename], [template], [content], [orderid]) VALUES (@name, @alikeid, @addtime, @seotitle, @seokeywords, @seodescription, @ishtml, @savepath, @filename, @template, @content, @orderid);SELECT SCOPE_IDENTITY()

GO

drop proc [dbo].[sta_updatepage]

GO

CREATE PROCEDURE [dbo].[sta_updatepage]
	@id int,
	@name nchar(50),
	@alikeid nvarchar(20),
	@addtime datetime,
	@seotitle nvarchar(100),
	@seokeywords nvarchar(200),
	@seodescription nvarchar(200),
	@ishtml tinyint,
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@template char(50),
	@content ntext,
	@orderid int
AS
UPDATE [sta_pages]
SET [name] = @name, [orderid] = @orderid, [alikeid] = @alikeid, [addtime] = @addtime, [seotitle] = @seotitle, [seokeywords] = @seokeywords, [seodescription] = @seodescription, [ishtml] = @ishtml, [savepath] = @savepath, [filename] = @filename, [template] = @template, [content] = @content 
WHERE [sta_pages].[id] = @id

GO

drop proc sta_createuser

GO

CREATE PROCEDURE [dbo].[sta_createuser]  
	@id int,
	@username nchar(20),
	@nickname nchar(20),
	@password nchar(32),
	@safecode nchar(32),
	@spaceid int,
	@gender tinyint,
	@birthday datetime,
	@adminid int,
	@admingroupname nvarchar(30),
	@groupid int,
	@groupname nvarchar(30),
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
	@locked tinyint,
	@newpm tinyint,
	@newpmcount int,
	@onlinestate tinyint,
	@Invisible tinyint,
	@showemail tinyint
AS
INSERT INTO [sta_users] ([username], [nickname], [password], [safecode], [spaceid], [gender], [birthday], [adminid], [admingroupname], [groupid], [groupname], [extgroupids], [regip], [addtime], [loginip], [logintime], [lastaction], [money], [credits], [extcredits1], [extcredits2], [extcredits3], [extcredits4], [extcredits5], [email], [ischeck], [locked], [newpm], [newpmcount], [onlinestate], [Invisible], [showemail]) VALUES (@username, @nickname, @password, @safecode, @spaceid, @gender, @birthday, @adminid, @admingroupname, @groupid, @groupname, @extgroupids, @regip, @addtime, @loginip, @logintime, @lastaction, @money, @credits, @extcredits1, @extcredits2, @extcredits3, @extcredits4, @extcredits5, @email, @ischeck, @locked, @newpm, @newpmcount, @onlinestate, @Invisible, @showemail);SELECT SCOPE_IDENTITY()

GO

drop proc sta_updateuser

GO

CREATE PROCEDURE [dbo].[sta_updateuser]
	@id int,
	@username nchar(20),
	@nickname nchar(20),
	@password nchar(32),
	@safecode nchar(32),
	@spaceid int,
	@gender tinyint,
	@birthday datetime,
	@adminid int,
	@admingroupname nvarchar(30),
	@groupid int,
	@groupname nvarchar(30),
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
	@locked tinyint,
	@newpm tinyint,
	@newpmcount int,
	@onlinestate tinyint,
	@Invisible tinyint,
	@showemail tinyint
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
UPDATE [sta_pms] SET msgfrom = @username WHERE msgfromid = @id
UPDATE [sta_pms] SET msgto = @username WHERE msgtoid = @id
UPDATE [sta_mailogs] SET username = @username WHERE userid = @id
UPDATE [sta_words] SET username = @username WHERE uid = @id
UPDATE [sta_userlogs] SET username = @username WHERE uid = @id

UPDATE [sta_users]
SET [username] = @username, [nickname] = @nickname, [password] = @password, [safecode] = @safecode, [spaceid] = @spaceid, [gender] = @gender, [birthday] = @birthday, [adminid] = @adminid, [admingroupname] = @admingroupname, [groupid] = @groupid, [groupname] = @groupname, [extgroupids] = @extgroupids, [regip] = @regip, [addtime] = @addtime, [loginip] = @loginip, [logintime] = @logintime, [lastaction] = @lastaction, [money] = @money, [credits] = @credits, [extcredits1] = @extcredits1, [extcredits2] = @extcredits2, [extcredits3] = @extcredits3, [extcredits4] = @extcredits4, [extcredits5] = @extcredits5, [email] = @email, [ischeck] = @ischeck, [locked] = @locked, [newpm] = @newpm, [newpmcount] = @newpmcount, [onlinestate] = @onlinestate, [Invisible] = @Invisible, [showemail] = @showemail 
WHERE [sta_users].[id] = @id

GO

drop proc [dbo].[sta_deleteuser]
GO

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
UPDATE [sta_pms] SET msgfrom = '已注销' WHERE msgfromid = @id
UPDATE [sta_pms] SET msgto = '已注销' WHERE msgtoid = @id
UPDATE [sta_userlogs] SET username = '已注销' WHERE uid = @id

DELETE FROM [sta_userconnects] WHERE [sta_userconnects].[uid] = @id

DELETE FROM [sta_userfields] WHERE [sta_userfields].[uid] = @id
DELETE FROM [sta_users] WHERE [sta_users].[id] = @id

GO

DROP PROCEDURE [sta_createuserfield] 
GO

CREATE PROCEDURE [dbo].[sta_createuserfield]  
	@Id int,
	@uid int,
	@realname nchar(10),
	@idcard char(30),
	@signature nchar(20),
	@description ntext,
	@areaid int,
	@areaname char(20),
	@address nvarchar(50),
	@postcode char(10),
	@hometel char(20),
	@worktel char(20),
	@mobile char(20),
	@icq char(20),
	@qq char(20),
	@skype nchar(100),
	@msn nchar(100),
	@website nchar(200),
	@ucid int
AS
INSERT INTO [sta_userfields] ([uid], [realname], [idcard], [signature], [description], [areaid], [areaname], [address], [postcode], [hometel], [worktel], [mobile], [icq], [qq], [skype], [msn], [website], [ucid]) VALUES (@uid, @realname, @idcard, @signature, @description, @areaid, @areaname, @address, @postcode, @hometel, @worktel, @mobile, @icq, @qq, @skype, @msn, @website, @ucid);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_updateuserfield]
GO

CREATE PROCEDURE [dbo].[sta_updateuserfield]
	@Id int,
	@uid int,
	@realname nchar(10),
	@idcard char(30),
	@signature nchar(20),
	@description ntext,
	@areaid int,
	@areaname char(20),
	@address nvarchar(50),
	@postcode char(10),
	@hometel char(20),
	@worktel char(20),
	@mobile char(20),
	@icq char(20),
	@qq char(20),
	@skype nchar(100),
	@msn nchar(100),
	@website nchar(200),
	@ucid int
AS
UPDATE [sta_userfields]
SET [uid] = @uid, [realname] = @realname, [idcard] = @idcard, [signature] = @signature, [description] = @description, [areaid] = @areaid, [areaname] = @areaname, [address] = @address, [postcode] = @postcode, [hometel] = @hometel, [worktel] = @worktel, [mobile] = @mobile, [icq] = @icq, [qq] = @qq, [skype] = @skype, [msn] = @msn, [website] = @website, [ucid] = @ucid
WHERE [sta_userfields].[uid] = @uid

GO

DROP PROCEDURE [sta_createusergroup]
GO

CREATE PROCEDURE [dbo].[sta_createusergroup]  
	@id int,
	@system int,
	@name char(50),
	@creditsmax int,
	@creditsmin int,
	@color char(7),
	@avatar nchar(100),
	@star int
AS
INSERT INTO [sta_usergroups] ([system], [name], [creditsmax], [creditsmin], [color], [avatar], [star]) VALUES (@system, @name, @creditsmax, @creditsmin, @color, @avatar, @star);SELECT SCOPE_IDENTITY()

GO

drop proc sta_updateusergroup
GO

CREATE PROCEDURE [dbo].[sta_updateusergroup]
	@id int,
	@system int,
	@name char(50),
	@creditsmax int,
	@creditsmin int,
	@color char(7),
	@avatar nchar(100),
	@star int
AS
UPDATE [sta_usergroups]
SET [system] = @system, [name] = @name, [creditsmax] = @creditsmax, [creditsmin] = @creditsmin, [color] = @color, [avatar] = @avatar, [star] = @star 
WHERE [sta_usergroups].[id] = @id
UPDATE [sta_users]
SET [groupname] = @name WHERE [sta_users].[groupid] = @id
UPDATE [sta_users]
SET [admingroupname] = @name WHERE [sta_users].[adminid] = @id

GO

drop proc [sta_createadminlog]  
GO

CREATE PROCEDURE [dbo].[sta_createadminlog]  
	@id int,
	@uid int,
	@username nchar(20),
	@groupid int,
	@groupname char(50),
	@ip char(15),
	@addtime datetime,
	@action nvarchar(100),
	@remark nvarchar(300),
	@admintype int
AS
INSERT INTO [sta_adminlogs] ([uid], [username], [groupid], [groupname], [ip], [addtime], [action], [remark], [admintype]) VALUES (@uid, @username, @groupid, @groupname, @ip, @addtime, @action, @remark, @admintype);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_createlink]  
GO

CREATE PROCEDURE [dbo].[sta_createlink]  
	@id int,
	@typeid int,
	@name nchar(50),
	@url nchar(100),
	@logo nchar(100),
	@email nchar(100),
	@addtime datetime,
	@description nvarchar(500),
	@orderid int,
	@status tinyint
AS
INSERT INTO [sta_links] ([typeid], [name], [url], [logo], [email], [addtime], [description], [orderid], [status]) VALUES (@typeid, @name, @url, @logo, @email, @addtime, @description, @orderid, @status);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updatelink]
GO

CREATE PROCEDURE [dbo].[sta_updatelink]
	@id int,
	@typeid int,
	@name nchar(50),
	@url nchar(100),
	@logo nchar(100),
	@email nchar(100),
	@addtime datetime,
	@description nvarchar(500),
	@orderid int,
	@status tinyint
AS
UPDATE [sta_links]
SET [typeid] = @typeid, [name] = @name, [url] = @url, [logo] = @logo, [email] = @email, [addtime] = @addtime, [description] = @description, [orderid] = @orderid, [status] = @status 
WHERE [sta_links].[id] = @id

GO

drop proc sta_createattachment
GO

CREATE PROCEDURE [dbo].[sta_createattachment]  
	@id int,
	@fileext nchar(10),
	@uid int,
	@username nchar(20),
	@addtime datetime,
	@lastedituid int,
	@lasteditusername nchar(20),
	@lastedittime datetime,
	@filename nchar(200),
	@description nvarchar(300),
	@filetype nchar(100),
	@filesize int,
	@attachment nvarchar(100),
	@width int,
	@height int,
	@downloads int,
	@attachcredits nchar(10)
AS
INSERT INTO [sta_attachments] ([fileext], [username], [uid], [addtime], [lastedituid], [lasteditusername], [lastedittime], [filename], [description], [filetype], [filesize], [attachment], [width], [height], [downloads], [attachcredits]) VALUES (@fileext, @username, @uid, @addtime, @lastedituid, @lasteditusername, @lastedittime, @filename, @description, @filetype, @filesize, @attachment, @width, @height, @downloads, @attachcredits);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updateattachment]
GO

CREATE PROCEDURE [dbo].[sta_updateattachment]
	@id int,
	@fileext nchar(10),
	@uid int,
	@username nchar(20),
	@addtime datetime,
	@lastedituid int,
	@lasteditusername nchar(20),
	@lastedittime datetime,
	@filename nchar(200),
	@description nvarchar(300),
	@filetype nchar(100),
	@filesize int,
	@attachment nvarchar(100),
	@width int,
	@height int,
	@downloads int,
	@attachcredits nchar(10)
AS
UPDATE [sta_attachments]
SET  [fileext] = @fileext, [uid] = @uid, [username] = @username, [addtime] = @addtime, [lastedituid] = @lastedituid, [lasteditusername] = @lasteditusername, [lastedittime] = @lastedittime, [filename] = @filename, [description] = @description, [filetype] = @filetype, [filesize] = @filesize, [attachment] = @attachment, [width] = @width, [height] = @height, [downloads] = @downloads, [attachcredits] = @attachcredits 
WHERE [sta_attachments].[id] = @id

GO

drop proc sta_createad
GO

CREATE PROCEDURE [dbo].[sta_createad]  
	@id int,
	@name nchar(50),
	@status tinyint,
	@filename nvarchar(200),
	@adtype tinyint,
	@addtime datetime,
	@startdate datetime,
	@enddate datetime,
	@click int,
	@paramarray ntext,
	@outdate nvarchar(500)
AS
INSERT INTO [sta_ads] ([name], [status], [filename], [adtype], [addtime], [startdate], [enddate], [click], [paramarray], [outdate]) VALUES (@name, @status, @filename, @adtype, @addtime, @startdate, @enddate, @click, @paramarray, @outdate);SELECT SCOPE_IDENTITY()

GO

drop proc sta_updatead
GO

CREATE PROCEDURE [dbo].[sta_updatead]
	@id int,
	@name nchar(50),
	@status tinyint,
	@filename nvarchar(200),
	@adtype tinyint,
	@addtime datetime,
	@startdate datetime,
	@enddate datetime,
	@click int,
	@paramarray ntext,
	@outdate nvarchar(500)
AS
UPDATE [sta_ads]
SET [name] = @name, [outdate] = @outdate, [status] = @status, [filename] = @filename, [adtype] = @adtype, [addtime] = @addtime, [startdate] = @startdate, [enddate] = @enddate, [click] = @click, [paramarray] = @paramarray
WHERE [sta_ads].[id] = @id

GO

drop proc [sta_commentsummary]
GO

CREATE PROC [dbo].[sta_commentsummary]
	@ctype tinyint,
	@cid int,
	@summary nvarchar(100) output
AS
SELECT @summary = (CAST(COUNT(star) as nvarchar(20)) + ',' + CAST(SUM(star) as nvarchar(20))) FROM sta_comments WHERE ctype = @ctype AND cid = @cid
SELECT @summary = @summary + ',' + CAST(COUNT(star) as nvarchar(20)) FROM sta_comments WHERE ctype = @ctype AND cid = @cid AND star = 1
SELECT @summary = @summary + ',' + CAST(COUNT(star) as nvarchar(20)) FROM sta_comments WHERE ctype = @ctype AND cid = @cid AND star = 2
SELECT @summary = @summary + ',' + CAST(COUNT(star) as nvarchar(20)) FROM sta_comments WHERE ctype = @ctype AND cid = @cid AND star = 3
SELECT @summary = @summary + ',' + CAST(COUNT(star) as nvarchar(20)) FROM sta_comments WHERE ctype = @ctype AND cid = @cid AND star = 4
SELECT @summary = @summary + ',' + CAST(COUNT(star) as nvarchar(20)) FROM sta_comments WHERE ctype = @ctype AND cid = @cid AND star = 5

GO

drop proc [dbo].[sta_createcomment]
GO

CREATE PROCEDURE [dbo].[sta_createcomment]  
	@id int,
	@ctype tinyint,
	@cid int,
	@contitle nvarchar(100),
	@uid int,
	@username nchar(20),
	@title nvarchar(100),
	@addtime datetime,
	@verifytime datetime,
	@userip char(15),
	@status tinyint,
	@diggcount int,
	@stampcount int,
	@msg ntext,
	@quote ntext,
	@replay	int,
	@city nvarchar(50),
	@star int,
	@useragent nvarchar(200)
AS
IF @ctype = 1
	UPDATE [sta_contents] SET [commentcount] = [commentcount]+1 WHERE [id] = @cid
ELSE
	UPDATE [sta_culturalrelics] SET [cmtcount] = [cmtcount]+1 WHERE [id] = @cid

INSERT INTO [sta_comments] ([ctype], [cid], [contitle], [uid], [username], [title], [addtime], [verifytime], [userip], [status], [diggcount], [stampcount], [msg], [quote], [replay], [city], [star], [useragent]) VALUES (@ctype, @cid, @contitle, @uid, @username, @title, @addtime, @verifytime, @userip, @status, @diggcount, @stampcount, @msg, @quote, @replay, @city, @star, @useragent);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updatecomment]
GO

CREATE PROCEDURE [dbo].[sta_updatecomment]
	@id int,
	@ctype tinyint,
	@cid int,
	@contitle nvarchar(100),
	@uid int,
	@username nchar(20),
	@title nvarchar(100),
	@addtime datetime,
	@verifytime datetime,
	@userip char(15),
	@status tinyint,
	@diggcount int,
	@stampcount int,
	@msg ntext,
	@quote ntext,
	@replay	int,
	@city nvarchar(50),
	@star int,
	@useragent nvarchar(200)
AS
UPDATE [sta_comments]
SET [ctype] = @ctype, [cid] = @cid, [replay] = @replay, [city] = @city, [uid] = @uid, [contitle] = @contitle, [username] = @username, [title] = @title, [addtime] = @addtime, [verifytime] = @verifytime, [userip] = @userip, [status] = @status, [diggcount] = @diggcount, [stampcount] = @stampcount, [msg] = @msg , [quote] = @quote, [star] = @star, [useragent] = @useragent WHERE [sta_comments].[id] = @id

GO

drop proc [sta_deletecomment]
GO

CREATE PROCEDURE [dbo].[sta_deletecomment]
	@id int,
	@ctype tinyint
AS
IF ISNULL(@ctype,1) = 1
	UPDATE [sta_contents] SET [commentcount] = [commentcount]-1 WHERE [id] = (SELECT TOP 1 [cid] FROM [sta_comments] WHERE [id] = @id) AND [commentcount] > 0
ELSE
	UPDATE [sta_culturalrelics] SET [cmtcount] = [cmtcount]-1 WHERE [id] = (SELECT TOP 1 [cid] FROM [sta_comments] WHERE [id] = @id) AND [cmtcount] > 0
DELETE FROM [sta_comments] WHERE [sta_comments].[id] = @id

GO

DROP PROCEDURE [sta_createsearchcache]  
GO

CREATE PROCEDURE [dbo].[sta_createsearchcache]  
	@id int,
	@keywords nvarchar(200),
	@searchstring nvarchar(300),
	@searchtime datetime,
	@expiration datetime,
	@scount int,
	@rcount int,
	@ids ntext
AS
INSERT INTO [sta_searchcaches] ([keywords], [searchstring], [searchtime], [expiration], [scount], [rcount], [ids]) VALUES (@keywords, @searchstring, @searchtime, @expiration, @scount, @rcount, @ids);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_updatesearchcache]
GO

CREATE PROCEDURE [dbo].[sta_updatesearchcache]
	@id int,
	@keywords nvarchar(200),
	@searchstring nvarchar(300),
	@searchtime datetime,
	@expiration datetime,
	@scount int,
	@rcount int,
	@ids ntext
AS
UPDATE [sta_searchcaches]
SET [keywords] = @keywords, [searchstring] = @searchstring, [searchtime] = @searchtime, [expiration] = @expiration, [scount] = @scount, [rcount] = @rcount, [ids] = @ids 
WHERE [sta_searchcaches].[id] = @id

GO

drop proc sta_templatecontent
GO
-----查询内容模板
CREATE PROC [dbo].[sta_templatecontent] 
	@Id int,
	@Template nvarchar(50) output
As 
DECLARE @TypeId int,@ChannelId int
SELECT @Template = ISNULL(template,''),@ChannelId = ISNULL(channelid,0),@TypeId = ISNULL(typeid,1) FROM sta_contents WHERE id = @Id
IF @Template IS NOT NULL AND @Template = ''
BEGIN
	IF @Typeid = 0
		SET @Template = 'special_default'
	ELSE
		BEGIN
			SELECT @Template = ISNULL(contem, '') FROM sta_channels WHERE id = @ChannelId
			IF @Template = ''
				SET @Template = 
					(CASE @TypeId
						WHEN 2 THEN 'content_photo'
						WHEN 3 THEN 'content_soft'
						WHEN 4 THEN 'content_product'
						WHEN 5 THEN 'content_info'
						WHEN 6 THEN 'content_video'
						ELSE 'content_default'
					END)
		END
END 

GO

--declare @tem nvarchar(50)
--exec sta_templatecontent
--	 @Id = 1111,
--     @template = @tem out
--print @tem
--
--declare @template nvarchar(50),@te int
--SELECT @te = ISNULL(channelid,1) FROM sta_contents WHERE id = 1444
--print @te

DROP PROCEDURE [sta_templatespecgroup] 
GO

-----查询专题内容组模板
CREATE PROC [dbo].[sta_templatespecgroup] 
	@Id int,
	@Template nvarchar(50) output
As 
SELECT @Template = ISNULL(ext_grouptpl,'') FROM sta_extspecials WHERE cid = @Id
IF @Template IS NOT NULL AND @Template = '' 
	SET @Template = 'specgroup_default'

GO


drop proc [sta_templatespecial] 
GO
-----查询专题模板
CREATE PROC [dbo].[sta_templatespecial] 
	@Id int,
	@Template nvarchar(50) output
As 
SELECT @Template = ISNULL(template,'') FROM sta_contents WHERE id = @Id
IF @Template IS NOT NULL AND @Template = '' 
	SET @Template = 'special_default'

GO

DROP PROCEDURE [sta_templatepage] 
GO
-----查询单页模板
CREATE PROC [dbo].[sta_templatepage] 
	@Id int,
	@Template nvarchar(50) output
As 
SELECT @Template = ISNULL(template,'') FROM sta_pages WHERE id = @Id
IF @Template IS NOT NULL AND @Template = '' 
	SET @Template = 'page_default'

GO

drop proc [sta_templatechannel] 
Go
-----查询频道模板
CREATE PROC [dbo].[sta_templatechannel] 
	@Id int,
	@Template nvarchar(50) output
As 
SET @Template = ''
DECLARE @CoverTem nvarchar(50),@ListTem nvarchar(50),@TypeId int,@ParentId int
SELECT @CoverTem = ISNULL(covertem,''),@ListTem = ISNULL(listem,0),@TypeId = ISNULL(ctype,1),@ParentId = ISNULL(parentid,0) FROM sta_channels WHERE id = @Id
IF @TypeId = 3 OR @TypeId IS NULL RETURN

SET @Template = 
	(CASE @TypeId 
		WHEN 1 THEN @ListTem 
		WHEN 2 THEN @CoverTem
	 END)

IF @Template = ''
	SET @Template = 
		(CASE @TypeId 
			WHEN 1 THEN 'channellist_default' 
			WHEN 2 THEN 'channelindex_default'
		 END)

GO

DROP PROC [sta_setlastexecutescheduledeventdatetime]
GO

CREATE PROCEDURE [dbo].[sta_setlastexecutescheduledeventdatetime]
(
	@key VARCHAR(100),
	@servername VARCHAR(100),
	@lastexecuted DATETIME
)
AS
DELETE FROM [sta_scheduledevents] WHERE ([key]=@key) AND ([lastexecuted] < DATEADD([day], - 7, GETDATE()))

INSERT [sta_scheduledevents] ([key], [servername], [lastexecuted]) Values (@key, @servername, @lastexecuted)

GO

DROP PROCEDURE [sta_getlastexecutescheduledeventdatetime]
GO

CREATE PROCEDURE [dbo].[sta_getlastexecutescheduledeventdatetime]
(
	@key VARCHAR(100),
	@servername VARCHAR(100),
	@lastexecuted DATETIME OUTPUT
)
AS
SELECT @lastexecuted = MAX([lastexecuted]) FROM [sta_scheduledevents] WHERE [key] = @key AND [servername] = @servername

IF(@lastexecuted IS NULL)
BEGIN
	SET @lastexecuted = DATEADD(YEAR,-1,GETDATE())
END

GO

DROP PROCEDURE [sta_createvotecate]
GO

CREATE PROCEDURE [dbo].[sta_createvotecate]  
	@id int,
	@name nvarchar(50),
	@ename nvarchar(50),
	@orderid int
AS
INSERT INTO [sta_votecates] ([name], [ename], [orderid]) VALUES (@name, @ename, @orderid);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_updatevotecate]
GO

CREATE PROCEDURE [dbo].[sta_updatevotecate]
	@id int,
	@name nvarchar(50),
	@ename nvarchar(50),
	@orderid int
AS
UPDATE [sta_votetopics]
SET [catename] = @name WHERE [cateid] = @id
UPDATE [sta_votecates]
SET [name] = @name, [ename] = @ename, [orderid] = @orderid 
WHERE [sta_votecates].[id] = @id

GO

DROP PROCEDURE [sta_deletevotecate]
GO

CREATE PROCEDURE [dbo].[sta_deletevotecate]
	@id int
AS
UPDATE [sta_votetopics]
SET [catename] = '', [cateid] = 0 WHERE [cateid] = @id

DELETE FROM [sta_votecates] WHERE [sta_votecates].[id] = @id

GO

drop proc [sta_createvoteoption] 
GO

CREATE PROCEDURE [dbo].[sta_createvoteoption]  
	@id int,
	@name nvarchar(300),
	@desc ntext,
	@topicid int,
	@topicname nvarchar(300),
	@img nvarchar(200),
	@count int,
	@orderid int
AS
UPDATE [sta_votetopics]
SET [votecount] = ISNULL((SELECT SUM([count]) FROM sta_voteoptions WHERE topicid = @topicid ),0)+@count WHERE [id] = @topicid	

INSERT INTO [sta_voteoptions] ([name], [desc], [topicid], [topicname], [img], [count], [orderid]) VALUES (@name, @desc, @topicid, @topicname, @img, @count, @orderid);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updatevoteoption]
GO

CREATE PROCEDURE [dbo].[sta_updatevoteoption]
	@id int,
	@name nvarchar(300),
	@desc ntext,
	@topicid int,
	@topicname nvarchar(300),
	@img nvarchar(200),
	@count int,
	@orderid int
AS
UPDATE [sta_voteoptions]
SET [name] = @name, [desc] = @desc, [topicid] = @topicid, [topicname] = @topicname, [img] = @img, [count] = @count, [orderid] = @orderid 
WHERE [sta_voteoptions].[id] = @id
EXEC sta_resetvotecount @topicid

GO

drop proc [sta_deletevoteoption]
GO

CREATE PROCEDURE [dbo].[sta_deletevoteoption]
	@id int
AS
DECLARE @tid int
SELECT @tid = ISNULL(topicid, 0) FROM sta_voteoptions WHERE id = @id
DELETE FROM [sta_voteoptions] WHERE [sta_voteoptions].[id] = @id
EXEC sta_resetvotecount @tid

GO

drop proc [sta_createvoterecord]  
GO

CREATE PROCEDURE [dbo].[sta_createvoterecord]  
	@id int,
	@topicid int,
	@topicname nvarchar(300),
	@optionids nvarchar(300),
	@userid int,
	@username nvarchar(20),
	@addtime datetime,
	@userip char(15),
	@realname nvarchar(20),
	@idcard nvarchar(18),
	@phone nvarchar(30)
AS
INSERT INTO [sta_voterecords] ([topicid], [topicname], [optionids], [userid], [username], [addtime], [userip], [realname], [idcard], [phone]) VALUES (@topicid, @topicname, @optionids, @userid, @username, @addtime, @userip, @realname, @idcard, @phone);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_deletevoterecord]
GO

CREATE PROCEDURE [dbo].[sta_deletevoterecord]
	@id int
AS
DELETE FROM [sta_voterecords] WHERE [sta_voterecords].[id] = @id

GO

drop proc [sta_createvotetopic]  
GO

CREATE PROCEDURE [dbo].[sta_createvotetopic]  
	@id int,
	@name nvarchar(300),
	@desc ntext,
	@cateid int,
	@catename nvarchar(50),
	@type tinyint,
	@img nvarchar(200),
	@likeid nvarchar(50),
	@maxvote int,
	@endtime datetime,
	@addtime datetime,
	@endtext ntext,
	@voted nvarchar(2000),
	@votecount int,
	@orderid int,
	@isvcode tinyint,
	@isinfo tinyint,
	@islogin tinyint
AS
INSERT INTO [sta_votetopics] ([name], [desc], [cateid], [catename], [type], [img], [likeid], [maxvote], [endtime], [addtime], [endtext], [voted],  [votecount], [orderid], [isvcode], [isinfo], [islogin]) VALUES (@name, @desc, @cateid, @catename, @type, @img, @likeid, @maxvote, @endtime, @addtime, @endtext, @voted, @votecount, @orderid, @isvcode, @isinfo, @islogin);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_resetvotecount]
GO

CREATE PROCEDURE [dbo].[sta_resetvotecount]
	@id int
AS
UPDATE [sta_votetopics]
SET [votecount] = ISNULL((SELECT SUM([count]) FROM sta_voteoptions WHERE topicid = @id ),0) WHERE [id] = @id	

GO

drop proc [sta_updatevotetopic]
GO

CREATE PROCEDURE [dbo].[sta_updatevotetopic]
	@id int,
	@name nvarchar(300),
	@desc ntext,
	@cateid int,
	@catename nvarchar(50),
	@type tinyint,
	@img nvarchar(200),
	@likeid nvarchar(50),
	@maxvote int,
	@endtime datetime,
	@addtime datetime,
	@endtext ntext,
	@voted nvarchar(2000),
	@votecount int,
	@orderid int,
	@isvcode tinyint,
	@isinfo tinyint,
	@islogin tinyint
AS
UPDATE [sta_voteoptions]
SET [topicname] = @name WHERE [topicid] = @id

UPDATE [sta_voterecords]
SET [topicname] = @name WHERE [topicid] = @id

UPDATE [sta_votetopics]
SET [name] = @name, [desc] = @desc, [endtext] = @endtext, [voted] = @voted, [cateid] = @cateid, [catename] = @catename, [type] = @type, [img] = @img, [likeid] = @likeid, [maxvote] = @maxvote, [endtime] = @endtime, [addtime] = @addtime, [votecount] = @votecount, [orderid] = @orderid, [isvcode] = @isvcode, [isinfo] = @isinfo, [islogin] = @islogin 
WHERE [sta_votetopics].[id] = @id

GO

drop proc [sta_deletevotetopic]
GO

CREATE PROCEDURE [dbo].[sta_deletevotetopic]
	@id int
AS
DELETE FROM [sta_voteoptions] WHERE [topicid] = @id
DELETE FROM [sta_voterecords] WHERE [topicid] = @id
DELETE FROM [sta_votetopics] WHERE [sta_votetopics].[id] = @id

GO

DROP PROCEDURE [sta_createdbcollect]
GO

CREATE PROCEDURE [dbo].[sta_createdbcollect]  
	@id int,
	@name nvarchar(50),
	@channelid int,
	@channelname nvarchar(50),
	@constatus tinyint,
	@dbtype tinyint,
	@addtime datetime,
	@datasource nvarchar(50),
	@userid nvarchar(50),
	@password nvarchar(50),
	@dbname nvarchar(50),
	@tbname nvarchar(50),
	@primarykey nvarchar(50),
	@orderkey nvarchar(50),
	@repeatkey nvarchar(50),
	@sortby char(4),
	@where nvarchar(1000),
	@matchs nvarchar(4000)
AS
INSERT INTO [sta_dbcollects] ([name], [channelid], [channelname], [constatus], [dbtype], [addtime], [datasource], [userid], [password], [dbname], [tbname], [primarykey], [orderkey], [repeatkey], [sortby], [where], [matchs]) VALUES (@name, @channelid, @channelname, @constatus, @dbtype, @addtime, @datasource, @userid, @password, @dbname, @tbname, @primarykey, @orderkey, @repeatkey, @sortby, @where, @matchs);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_updatedbcollect]
GO

CREATE PROCEDURE [dbo].[sta_updatedbcollect]
	@id int,
	@name nvarchar(50),
	@channelid int,
	@channelname nvarchar(50),
	@constatus tinyint,
	@dbtype tinyint,
	@addtime datetime,
	@datasource nvarchar(50),
	@userid nvarchar(50),
	@password nvarchar(50),
	@dbname nvarchar(50),
	@tbname nvarchar(50),
	@primarykey nvarchar(50),
	@orderkey nvarchar(50),
	@repeatkey nvarchar(50),
	@sortby char(4),
	@where nvarchar(1000),
	@matchs nvarchar(4000)
AS
UPDATE [sta_dbcollects]
SET [name] = @name, [channelid] = @channelid, [channelname] = @channelname, [constatus] = @constatus, [dbtype] = @dbtype, [addtime] = @addtime, [datasource] = @datasource, [userid] = @userid, [password] = @password, [dbname] = @dbname, [tbname] = @tbname, [primarykey] = @primarykey, [orderkey] = @orderkey, [repeatkey] = @repeatkey, [sortby] = @sortby, [where] = @where, [matchs] = @matchs 
WHERE [sta_dbcollects].[id] = @id

GO

drop proc [sta_createwebcollect]  
GO

CREATE PROCEDURE [dbo].[sta_createwebcollect]  
	@id int,
	@name nvarchar(50),
	@addtime datetime,
	@channelid int,
	@channelname nvarchar(50),
	@constatus tinyint,
	@hosturl nvarchar(200),
	@collecttype tinyint,
	@curl nvarchar(200),
	@clisturl nvarchar(200),
	@clistpage nchar(20),
	@curls ntext,
	@encode nchar(20),
	@property nvarchar(200),
	@filter nvarchar(200),
	@attrs nvarchar(300),
	@setting ntext,
	@confilter ntext
AS
INSERT INTO [sta_webcollects] ([name], [addtime], [channelid], [channelname], [constatus], [hosturl], [collecttype], [curl], [clisturl], [clistpage], [curls], [encode], [property], [filter], [attrs], [setting], [confilter]) VALUES (@name, @addtime, @channelid, @channelname, @constatus, @hosturl, @collecttype, @curl, @clisturl, @clistpage, @curls, @encode, @property, @filter, @attrs, @setting, @confilter);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updatewebcollect]
GO

CREATE PROCEDURE [dbo].[sta_updatewebcollect]
	@id int,
	@name nvarchar(50),
	@addtime datetime,
	@channelid int,
	@channelname nvarchar(50),
	@constatus tinyint,
	@hosturl nvarchar(200),
	@collecttype tinyint,
	@curl nvarchar(200),
	@clisturl nvarchar(200),
	@clistpage nchar(20),
	@curls ntext,
	@encode nchar(20),
	@property nvarchar(200),
	@filter nvarchar(200),
	@attrs nvarchar(300),
	@setting ntext,
	@confilter ntext
AS
UPDATE [sta_webcollects]
SET [name] = @name, [channelid] = @channelid, [confilter] = @confilter, [addtime] = @addtime, [channelname] = @channelname, [constatus] = @constatus, [hosturl] = @hosturl, [collecttype] = @collecttype, [curl] = @curl, [clisturl] = @clisturl, [clistpage] = @clistpage, [curls] = @curls, [encode] = @encode, [property] = @property, [filter] = @filter, [attrs] = @attrs, [setting] = @setting 
WHERE [sta_webcollects].[id] = @id

GO

drop proc [sta_createmenu]  
GO

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

GO

drop proc [sta_updatemenu]
GO

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

GO

drop proc [sta_deletemenu]
GO

CREATE PROCEDURE [dbo].[sta_deletemenu]
	@id int
AS
DELETE FROM [sta_menurelations] WHERE [sta_menurelations].[menuid] = @id
DELETE FROM [sta_menus] WHERE [sta_menus].[id] = @id

GO

DROP PROCEDURE [sta_createplugin]
GO

CREATE PROCEDURE [dbo].[sta_createplugin]  
	@id int,
	@name nvarchar(50),
	@email nvarchar(100),
	@author nvarchar(50),
	@pubtime datetime,
	@officesite nvarchar(100),
	@menu nvarchar(500),
	@description ntext,
	@dbcreate ntext,
	@dbdelete ntext,
	@filelist ntext,
	@package nvarchar(200),
	@setup tinyint
AS
INSERT INTO [sta_plugins] ([name], [email], [author], [pubtime], [officesite], [menu], [description], [dbcreate], [dbdelete], [filelist], [package], [setup]) VALUES (@name, @email, @author, @pubtime, @officesite, @menu, @description, @dbcreate, @dbdelete, @filelist, @package, @setup);SELECT SCOPE_IDENTITY()

GO

DROP PROCEDURE [sta_updateplugin]
GO

CREATE PROCEDURE [dbo].[sta_updateplugin]
	@id int,
	@name nvarchar(50),
	@email nvarchar(100),
	@author nvarchar(50),
	@pubtime datetime,
	@officesite nvarchar(100),
	@menu nvarchar(500),
	@description ntext,
	@dbcreate ntext,
	@dbdelete ntext,
	@filelist ntext,
	@package nvarchar(200),
	@setup tinyint
AS
UPDATE [sta_plugins]
SET [name] = @name, [email] = @email, [author] = @author, [pubtime] = @pubtime, [officesite] = @officesite, [menu] = @menu, [description] = @description, [dbcreate] = @dbcreate, [dbdelete] = @dbdelete, [filelist] = @filelist, [package] = @package, [setup] = @setup 
WHERE [sta_plugins].[id] = @id

GO

drop proc [dbo].[sta_createstaticize]  
GO

CREATE PROCEDURE [dbo].[sta_createstaticize]  
	@id int,
	@title nvarchar(50),
	@charset nvarchar(15),
	@url nvarchar(200),
	@addtime datetime,
	@maketime datetime,
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@suffix nvarchar(10)
AS
INSERT INTO [sta_staticizes] ([title], [charset], [url], [addtime], [maketime], [savepath], [filename], [suffix]) VALUES (@title, @charset, @url, @addtime, @maketime, @savepath, @filename, @suffix);SELECT SCOPE_IDENTITY()

GO

drop proc [dbo].[sta_updatestaticize]  
GO

CREATE PROCEDURE [dbo].[sta_updatestaticize]
	@id int,
	@title nvarchar(50),
	@charset nvarchar(15),
	@url nvarchar(200),
	@addtime datetime,
	@maketime datetime,
	@savepath nvarchar(100),
	@filename nvarchar(50),
	@suffix nvarchar(10)
AS
UPDATE [sta_staticizes]
SET [title] = @title, [charset] = @charset, [url] = @url, [addtime] = @addtime, [maketime] = @maketime, [savepath] = @savepath, [filename] = @filename, [suffix] = @suffix 
WHERE [sta_staticizes].[id] = @id

GO

drop proc [sta_createselecttype]  
GO

CREATE PROCEDURE [dbo].[sta_createselecttype]  
	@id int,
	@name nchar(30),
	@ename nchar(20),
	@issign tinyint,
	@system tinyint
AS
IF ((SELECT COUNT(*) FROM [sta_selecttypes] WHERE [ename] = @ename) > 0) RETURN
INSERT INTO [sta_selecttypes] ([name], [ename], [issign], [system]) VALUES (@name, @ename, @issign, @system);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updateselecttype]
GO

CREATE PROCEDURE [dbo].[sta_updateselecttype]
	@id int,
	@name nchar(30),
	@ename nchar(20),
	@issign tinyint,
	@system tinyint
AS
IF ((SELECT COUNT(*) FROM [sta_selecttypes] WHERE [ename] = @ename AND id != @id) > 0) RETURN
UPDATE [sta_selecttypes]
SET [name] = @name, [ename] = @ename, [issign] = @issign, [system] = @system 
WHERE [sta_selecttypes].[id] = @id

GO

drop proc [sta_createselect] 
GO

CREATE PROCEDURE [dbo].[sta_createselect]  
	@id int,
	@name nchar(20),
	@value nchar(30),
	@ename nchar(20),
	@orderid int,
	@issign tinyint
AS
INSERT INTO [sta_selects] ([name], [value], [ename], [orderid], [issign]) VALUES (@name, @value, @ename, @orderid, @issign);SELECT SCOPE_IDENTITY()

GO

drop proc [sta_updateselect]
GO

CREATE PROCEDURE [dbo].[sta_updateselect]
	@id int,
	@name nchar(20),
	@value nchar(30),
	@ename nchar(20),
	@orderid int,
	@issign tinyint
AS
UPDATE [sta_selects]
SET [name] = @name, [value] = @value, [ename] = @ename, [orderid] = @orderid, [issign] = @issign 
WHERE [sta_selects].[id] = @id

go

drop proc [sta_getpmlist]

go


CREATE PROCEDURE [dbo].[sta_getpmlist]
	@userid int,
	@folder int,
	@pagesize int,
	@pageindex int,
	@inttype int
AS
DECLARE @startRow int,
		@endRow int
SET @startRow = (@pageindex - 1) * @pagesize + 1
SET @endRow = @startRow + @pagesize - 1

IF (@folder <> 0)
BEGIN
	IF (@inttype <> 1)
	BEGIN
		SELECT 
		[PMS].[id],
		[PMS].[msgfrom],
		[PMS].[msgfromid],
		[PMS].[msgto],
		[PMS].[msgtoid],
		[PMS].[folder],
		[PMS].[new],
		[PMS].[subject],
		[PMS].[addtime],
		[PMS].[content]		
		FROM(SELECT ROW_NUMBER() OVER(ORDER BY [id] DESC) AS ROWID,
		[id],
		[msgfrom],
		[msgfromid],
		[msgto],
		[msgtoid],
		[folder],
		[new],
		[subject],
		[addtime],
		[content]		
		FROM [sta_pms]
		WHERE [msgfromid] = @userid AND [folder] = @folder) AS PMS
		WHERE ROWID BETWEEN @startRow AND @endRow
	END
	
	ELSE
	BEGIN
		SELECT 
		[PMS].[id],
		[PMS].[msgfrom],
		[PMS].[msgfromid],
		[PMS].[msgto],
		[PMS].[msgtoid],
		[PMS].[folder],
		[PMS].[new],
		[PMS].[subject],
		[PMS].[addtime],
		[PMS].[content]		
		FROM(SELECT ROW_NUMBER() OVER(ORDER BY [id] DESC) AS ROWID,
		[id],
		[msgfrom],
		[msgfromid],
		[msgto],
		[msgtoid],
		[folder],
		[new],
		[subject],
		[addtime],
		[content]		
		FROM [sta_pms]
		WHERE [msgfromid] = @userid AND [folder] = @folder AND [new] = 1) AS PMS
		WHERE ROWID BETWEEN @startRow AND @endRow
	END
END

ELSE
BEGIN
	IF (@inttype <> 1)
	BEGIN
		SELECT 
		[PMS].[id],
		[PMS].[msgfrom],
		[PMS].[msgfromid],
		[PMS].[msgto],
		[PMS].[msgtoid],
		[PMS].[folder],
		[PMS].[new],
		[PMS].[subject],
		[PMS].[addtime],
		[PMS].[content]		
		FROM(SELECT ROW_NUMBER() OVER(ORDER BY [id] DESC) AS ROWID,
		[id],
		[msgfrom],
		[msgfromid],
		[msgto],
		[msgtoid],
		[folder],
		[new],
		[subject],
		[addtime],
		[content]		
		FROM [sta_pms]
		WHERE [msgtoid] = @userid AND [folder] = @folder) AS PMS
		WHERE ROWID BETWEEN @startRow AND @endRow
	END
	
	ELSE
	BEGIN
		SELECT 
		[PMS].[id],
		[PMS].[msgfrom],
		[PMS].[msgfromid],
		[PMS].[msgto],
		[PMS].[msgtoid],
		[PMS].[folder],
		[PMS].[new],
		[PMS].[subject],
		[PMS].[addtime],
		[PMS].[content]		
		FROM(SELECT ROW_NUMBER() OVER(ORDER BY [id] DESC) AS ROWID,
		[id],
		[msgfrom],
		[msgfromid],
		[msgto],
		[msgtoid],
		[folder],
		[new],
		[subject],
		[addtime],
		[content]		
		FROM [sta_pms]
		WHERE [msgtoid] = @userid AND [folder] = @folder AND [new] = 1) AS PMS
		WHERE ROWID BETWEEN @startRow AND @endRow
	END
END

go



DROP PROC [sta_getpmcount]

go

CREATE PROCEDURE [dbo].[sta_getpmcount]
	@userid int,
	@folder int=0,
	@state int=-1
AS

IF @folder=-1
	BEGIN
	  SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE ([msgtoid] = @userid AND [folder]=0) OR ([msgfromid] = @userid AND [folder] = 1) OR ([msgfromid] = @userid AND [folder] = 2)
	END
ELSE
    BEGIN
		IF @folder=0
			BEGIN
				IF @state=-1
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgtoid] = @userid AND [folder] = @folder
					END
				ELSE IF @state=2
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgtoid] = @userid AND [folder] = @folder AND [new]=1 AND GETDATE()-[addtime]<3
					END
				ELSE
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgtoid] = @userid AND [folder] = @folder AND [new] = @state
					END
			END
		ELSE
			BEGIN
				IF @state=-1
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgfromid] = @userid AND [folder] = @folder
					END
				ELSE IF @state=2
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgfromid] = @userid AND [folder] = @folder AND [new]=1 AND GETDATE()-[addtime]<3
					END
				ELSE
					BEGIN
						SELECT COUNT(id) AS [pmcount] FROM [sta_pms] WHERE [msgfromid] = @userid AND [folder] = @folder AND [new] = @state
					END
			END
	END


GO

DROP PROC [sta_createpm]

go

CREATE PROCEDURE [sta_createpm]
	@id int,
	@msgfrom nchar(20),
	@msgto nchar(20),
	@msgfromid int,
	@msgtoid int,
	@folder smallint=0,
	@new int=0,
	@subject nvarchar(60),
	@addtime datetime,
	@content ntext,
	@savetosentbox smallint = 1
AS

IF @folder<>0
	BEGIN
		SET @msgfrom=@msgto
	END
ELSE
	BEGIN
		UPDATE [sta_users] SET [newpmcount]=ABS(ISNULL([newpmcount],0)*1)+1,[newpm] = 1 WHERE [id]=@msgtoid
	END


INSERT INTO [sta_pms] 
	([msgfrom],[msgfromid],[msgto],[msgtoid],[folder],[new],[subject],[addtime],[content])
VALUES
	(@msgfrom,@msgfromid,@msgto,@msgtoid,@folder,@new,@subject,@addtime,@content)
	
SELECT SCOPE_IDENTITY() AS 'pmid'

IF @savetosentbox=1 AND @folder=0
	BEGIN
		INSERT INTO [sta_pms]
			([msgfrom],[msgfromid],[msgto],[msgtoid],[folder],[new],[subject],[addtime],[content])
		VALUES
			(@msgfrom,@msgfromid,@msgto,@msgtoid,1,@new,@subject,@addtime,@content)
	END





exec [sta_createpm]
	@id = 0,
	@msgfrom = 'admin',
	@msgto = 'zaker',
	@msgfromid = 1,
	@msgtoid = 2,
	@folder=0,
	@new =1,
	@subject = 'test2',
	@addtime = '2012-12-12',
	@content = 'sdfsdfsdf',
	@savetosentbox = 0











select * from sta_scheduledevents


DECLARE @CoverTem nvarchar(50),@ListTem nvarchar(50),@TypeId int,@ParentId int
select @CoverTem = ISNULL(template,'dd') from sta_contents where id = 65
IF @covertem IS NULL
	PRINT 'dsf'
if @covertem = '' AND @covertem is not null
	print ' ddddddddddd'






select * from syscolumns where id =( select id from sysobjects where name='sta_extphotos') 
select * from sta_contypes where id = 0


SELECT * FROM [sta_channels] WHERE [id] = 1

exec [sta_createtag] '你好',0
select * from sta_tags

select [t].[name] from [sta_tags] [t]

update sta_channels set typeid = 1

select * from sta_extphotos

select * from sta_contags

delete from sta_tags where id > 1

declare @t int
select @t = isnull(id,0) from sta_tags where name = '12'
print isnull(@t,0)
select SELECT SCOPE_IDENTITY()
delete from sta_contags
declare @t int
INSERT INTO [sta_contags] ([contentid], [tagid]) VALUES (1, 1)
select SCOPE_IDENTITY() as 'sdf'
SET @t = SCOPE_IDENTITY()
print @t
SELECT SCOPE_IDENTITY()



select * from sta_channels

update sta_channels set typeid=0

select * from sta_tags
select * from sta_contents
select * from sta_extcontents
delete from sta_extcontents
delete from sta_contents

SELECT copyright,time2 FROM sta_extcontents WHERE cid = 8


select [channelname] [cn] from [sta_contents] [s] inner join sta_channels c on s.channelid = c.id where cn = ''

update sta_users set locked = 0


select * from sta_attachments where filesize >= 0 AND addtime >= '2011-11-27 00:00:00' AND addtime <= '2011-12-28 00:00:00'
id > 0 AND addtime >= '2011/9/27 0:00:00' AND addtime <= '2011/12/28 0:00:00' AND logintime >= '2011/12/20 0:00:00' AND logintime <= '2011/12/28 0:00:00'

use siteasy

select * from sta_attachments where addtime > '2011-12-27 00:57:16'

delete from sta_attachments
select * from sta_channels
delete from sta_channels where id>1
select * from sta_contents
select * from sta_contypes
select
select * from sta_users
select * from sta_extcontents
delete from sta_extcontents
delete from sta_extphotos
delete from sta_extsofts
delete from sta_extsofts
delete from sta_extspecials
                                                                                                                                                        
SELECT * FROM [sta_attachments] WHERE [sta_attachments].[filename] like '/files/2011/12/27/index[_]201112270347281962.css%'
SELECT * FROM [sta_attachments] WHERE [sta_attachments].[filename] like '%/files/2011/12/27/正则表达式30分钟入门教程[_]201112271214237763.htm%'


select * from sta_attachments where addtime > '2011-12-26 17:57:17.000'

select * from sta_extcontents
select * from sta_extphotos
select * from sta_extsofts

select * from sta_tags
insert into sta_tags (name) values ('123')

SELECT * FROM syscolumns WHERE id =( SELECT id FROM sysobjects WHERE name= 'sta_extcontents')
ALTER TABLE [sta_extcontents] DROP COLUMN [ext_copyright]

select  * from sta_extphotos
<img id="2576" url="/files/2012/01/02/68_201201021130177145.jpg" name="68"/>  <img id="2577" url="/files/2012/01/02/69_201201021130177790.jpg" name="69"/>  <img id="2607" url="/files/2012/01/02/10_201201021203125827.jpg" name="10"/>  

<soft id="undefined63641255088886448819" url="" name="本地下载"/>  


delete from sta_adminlogs

select * from sta_ads

SELECT     col.name, col.column_id, st.name AS type, col.max_length AS length, col.is_nullable,col.[precision], col.scale, col.is_identity, defCst.definition FROM         sys.columns AS col LEFT OUTER JOIN sys.types AS st ON st.user_type_id = col.user_type_id LEFT OUTER JOIN sys.types AS bt ON bt.user_type_id = col.system_type_id LEFT OUTER JOIN sys.objects AS robj ON robj.object_id = col.rule_object_id AND robj.type = 'R' LEFT OUTER JOIN sys.objects AS dobj ON dobj.object_id = col.default_object_id AND dobj.type = 'D' LEFT OUTER JOIN sys.default_constraints AS defCst ON defCst.parent_object_id = col.object_id AND defCst.parent_column_id = col.column_id LEFT OUTER JOIN sys.identity_columns AS idc ON idc.object_id = col.object_id AND idc.column_id = col.column_id LEFT OUTER JOIN sys.computed_columns AS cmc ON cmc.object_id = col.object_id AND cmc.column_id = col.column_id LEFT OUTER JOIN sys.fulltext_index_columns AS ftc ON ftc.object_id = col.object_id AND ftc.column_id = col.column_id LEFT OUTER JOIN sys.xml_schema_collections AS xmlcoll ON xmlcoll.xml_collection_id = col.xml_collection_id WHERE     (col.object_id = OBJECT_ID(N'dbo.sta_contents')) ORDER BY col.column_id

/*查询重复记录*/
select * from sta_contents where id>0 and id >5

select * from sta_contents where title in (select title from sta_contents  where typeid = 1 group by title having count(title) > 1 ) and typeid =1

SELECT TOP 1 addtime FROM [{0}adminlogs] WHERE id not in SELECT [{0}adminlogs].[uid] = {1} AND [{0}adminlogs].[action] = '系统登录' ORDER BY ID DESC


SELECT TOP 1 addtime FROM [sta_adminlogs] WHERE id not in (SELECT TOP 1 id FROM [sta_adminlogs] WHERE [sta_adminlogs].[uid] = 6 AND [sta_adminlogs].[action] = '系统登录' ORDER BY ID DESC) AND [sta_adminlogs].[uid] = 6 AND [sta_adminlogs].[action] = '系统登录' ORDER BY ID DESC

update sta_channels set conrule = '{@channelpath}/{@year02}/{@month}/{@day}/{@hour}/{@contentid}',listrule = '{@channelpath}/list_{@channelid}_{@page}'

select (savepath+'/'+filename) as htmlname from sta_contents


exec sp_configure 'show advanced options',1   
reconfigure   
exec sp_configure 'Ad Hoc Distributed Queries',1   
reconfigure   


insert into sta_selects(name,value,ename,orderid,issign) SELECT name,value,ename,orderid,issign FROM OPENROWSET('MICROSOFT.JET.OLEDB.4.0'
,'Excel 5.0;HDR=YES;DATABASE=C:\Users\Administrator\Desktop\dede_sys_enum.xls',dede_sys_enum$)

/*使用完成后，关闭Ad Hoc Distributed Queries*/  
exec sp_configure 'Ad Hoc Distributed Queries',0   
reconfigure   
exec sp_configure 'show advanced options',0   
reconfigure  

select * from sta_selecttypes where id = 12

delete from sta_selecttypes where id = 12



select * from sta_selects where ename = 'infotype' and cast([value] as nvarchar(300) like '%.%'

select * from sta_selects where ename = 'nativeplace' order by cast([value] as float) asc

select * from sta_selects where [value] > 2501

delete from sta_selects

drop table sta_selects


select * from sta_selects where id > 3357


delete from sta_selects  where id > 3357

select * from sta_selects where name = '商品' 


select * from sta_contypefields

select * from sta_extvideos

select top 1 * from sta_contents where id > 13 order by id asc

select top 1 * from sta_contents where id < 13 order by id desc

select * from sta_comments where id%500 = 0

select COUNT(*) as ct,[value] from sta_selects where ename = 'nativeplace'  group by [value] order by count(*) desc

select * from sta_selects where ename = 'nativeplace' and [value] = '11003.1'

select title + ',' + channelname from sta_contents


--迁移数据库
exec sp_configure 'show advanced options',1   
reconfigure   
exec sp_configure 'Ad Hoc Distributed Queries',1   
reconfigure   

select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_contents

--复制频道
delete from sta_channels
go
SET IDENTITY_INSERT [dbo].[sta_channels] ON
insert into sta_channels (id, typeid, parentid, name, savepath, filename, ctype, img, addtime, covertem, listem, contem, conrule, listrule, seotitle, seokeywords, seodescription, moresite, siteurl, content, ispost, ishidden, orderid, listcount,viewgroup,viewcongroup)  
select id, typeid, parentid, name, savepath, filename, ctype, img, addtime, covertem, listem, contem, conrule, listrule, seotitle, seokeywords, seodescription, moresite, siteurl, content, ispost, ishidden, orderid, 0, '', '' from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_channels
SET IDENTITY_INSERT [dbo].[sta_channels] OFF
go

--复制文档
delete from sta_contents
go
SET IDENTITY_INSERT [dbo].[sta_contents] ON
insert into sta_contents (id, typeid, typename, channelfamily, channelid, channelname, extchannels, title, subtitle, addtime, updatetime, color, property, adduser, addusername, lastedituser, lasteditusername, author, source, img, url, seotitle, seokeywords, seodescription, savepath, filename, template, content, status, viewgroup, iscomment, ishtml, click, orderid, diggcount, stampcount, commentcount, credits)  
select id, typeid, typename, channelfamily, channelid, channelname, extchannels, title, subtitle, addtime, updatetime, color, property, adduser, addusername, lastedituser, lasteditusername, author, source, img, url, seotitle, seokeywords, seodescription, savepath, filename, template, content, status, viewgroup, iscomment, ishtml, click, orderid, diggcount, stampcount, commentcount, credits from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_contents
SET IDENTITY_INSERT [dbo].[sta_contents] OFF
go

delete from sta_extcontents
go
insert into sta_extcontents (cid,typeid) select id,typeid from sta_contents where typeid = 1 
go
delete from sta_extspecials
go
insert into sta_extspecials (cid,typeid,ext_grouptpl,ext_banner) select id,typeid,'','' from sta_contents where typeid = 0
go

--复制单页
delete from sta_pages
go
SET IDENTITY_INSERT [dbo].[sta_pages] ON
insert into sta_pages (id, name, alikeid, addtime, seotitle, seokeywords, seodescription, ishtml, savepath, filename, template, content, orderid) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_pages
SET IDENTITY_INSERT [dbo].[sta_pages] OFF
go

--专题组复制 
delete from sta_specgroups
go
SET IDENTITY_INSERT [dbo].[sta_specgroups] ON
insert into sta_specgroups (id, specid, name, addtime, orderid) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_specgroups
SET IDENTITY_INSERT [dbo].[sta_specgroups] OFF
go
delete from sta_specontents
go
insert into sta_specontents (specid, groupid, contentid) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_specontents
go

--复制广告表  
delete from sta_ads
go
SET IDENTITY_INSERT [dbo].[sta_ads] ON
insert into sta_ads (id, name, status, filename, adtype, addtime, startdate, enddate, click, paramarray, outdate) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_ads
SET IDENTITY_INSERT [dbo].[sta_ads] OFF
go
--复制附件表 
delete from sta_attachments
go
SET IDENTITY_INSERT [dbo].[sta_attachments] ON
insert into sta_attachments (id, uid, username, lastedituid, lasteditusername, lastedittime, addtime, filename, description, filetype, fileext, filesize, attachment, width, height, downloads, attachcredits) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_attachments
SET IDENTITY_INSERT [dbo].[sta_attachments] OFF
go
--复制用户  
delete from sta_users
go
SET IDENTITY_INSERT [dbo].[sta_users] ON
insert into sta_users (id, username, nickname, password, safecode, gender, adminid, admingroupname, groupid, groupname, extgroupids, regip, addtime, loginip, logintime, lastaction, money, credits, extcredits1, extcredits2, extcredits3, extcredits4, extcredits5, email, ischeck, locked) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_users
SET IDENTITY_INSERT [dbo].[sta_users] OFF
go

delete from sta_userfields
go
SET IDENTITY_INSERT [dbo].[sta_userfields] ON
insert into sta_userfields (Id, uid, realname, idcard, signature, description, areaid, areaname, address, postcode, hometel, worktel, mobile, icq, qq, skype, msn, website) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_userfields
SET IDENTITY_INSERT [dbo].[sta_userfields] OFF
go

--复制标签
delete from sta_tags
go
SET IDENTITY_INSERT [dbo].[sta_tags] ON
insert into sta_tags (id, name, count, addtime) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_tags
SET IDENTITY_INSERT [dbo].[sta_tags] OFF
go
delete from sta_contags
go
insert into sta_contags (contentid, tagid) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_contags
go
--复制简易投票
delete from sta_plusvotes
go
SET IDENTITY_INSERT [dbo].[sta_plusvotes] ON
insert into sta_plusvotes (Id, Title, StartDate, EndDate, IsMore, IsView, IsEnable, Interval, Items) select * from OPENDATASOURCE('SQLNCLI','Data Source=.;Integrated Security=SSPI').ctaa3.dbo.sta_plusvotes
SET IDENTITY_INSERT [dbo].[sta_plusvotes] OFF
go
/*使用完成后，关闭Ad Hoc Distributed Queries*/  
exec sp_configure 'Ad Hoc Distributed Queries',0   
reconfigure   
exec sp_configure 'show advanced options',0   
reconfigure  


--循环更改频道 内容 路径

select * from sta_channels where savepath like '/a/%'
select * from sta_contents where savepath like '/a/%'
select * from sta_pages where savepath like '/a/%'
update sta_channels set savepath = substring(savepath,3,200) where savepath like '/a/%'
update sta_contents set savepath = substring(savepath,3,200) where savepath like '/a/%'
update sta_pages set savepath = substring(savepath,3,200) where savepath like '/a/%'


SELECT a.*
FROM OPENROWSET('SQLNCLI', 'Server=.;Trusted_Connection=yes;',
     'SELECT *
      FROM ctaa3.sta_contents') AS a;

--循环操作
declare @count int,@cid int,@i int,@temp int
select @count = (select count(id) from sta_channels)
set @i=1
set @temp = 1
while @i<=@count
begin 
	declare @tempid int
	select @tempid = (select top 1 id from sta_channels where id not in (select top (@i-1) id from news order by id asc) order by id asc)
	update news set orderid = @temp where id = @tempid
	set @temp =@i*5
	set @i = @i+1
end 

update sta_channels set ipdenyaccess = '',ipaccess=''

select * from sta_userfields