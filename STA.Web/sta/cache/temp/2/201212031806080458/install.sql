IF OBJECT_ID('plus_slideimgs') IS NOT NULL
DROP TABLE [plus_slideimgs]
GO

CREATE TABLE [plus_slideimgs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[img] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[thumb] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[text] [nvarchar](300) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[url] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[likeid] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[orderid] [int] NOT NULL,
 CONSTRAINT [PK_plus_slideimgs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
)