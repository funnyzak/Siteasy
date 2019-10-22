IF OBJECT_ID('plus_advisory') IS NOT NULL
DROP TABLE [plus_advisory]
GO

CREATE TABLE [plus_advisory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[uname] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[qtype] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[email] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[phone] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ip] [nvarchar](30) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[addtime] [datetime] NOT NULL,
	[message] [ntext] COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_plus_advisory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) 
GO

DELETE FROM [@tbprefix_variables] WHERE [key] =  N'advisory_msgtype'
GO

INSERT [@tbprefix_variables] ([name], [likeid], [key], [desc], [value], [system]) VALUES (N'留言咨询类型', N'插件.留言建议', N'advisory_msgtype', N'系统插件留言建议的留言类型配置', N'网站建议,内容排版,合作咨询', 1)