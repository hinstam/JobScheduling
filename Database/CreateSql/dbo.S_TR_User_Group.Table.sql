USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_TR_User_Group]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_TR_User_Group](
	[GroupID] [int] NOT NULL,
	[UserUID] [int] NOT NULL,
 CONSTRAINT [PK_S_TR_User_Group] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[UserUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
