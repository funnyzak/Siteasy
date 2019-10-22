IF OBJECT_ID('plus_pickerr') IS NOT NULL
DROP TABLE [plus_pickerr]
GO

CREATE TABLE [plus_pickerr](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[title] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ip] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[subtime] [datetime] NOT NULL,
	[errortxt] [nvarchar](2000) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[righttxt] [nvarchar](2000) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_plus_pickerr] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)

GO

DELETE FROM [@tbprefix_variables] WHERE [key] = N'pickerr_type'

GO

INSERT [@tbprefix_variables] ([name], [likeid], [key], [desc], [value], [system]) VALUES (N'内容挑错类型', N'插件.内容挑错', N'pickerr_type', N'系统插件内容挑错的错误类型配置', N'错别字(除的、地、得),成语运用不当,专业术语写法不规则,产品与图片不符,事实年代以及内容错误,技术参数错误,其他', 1)