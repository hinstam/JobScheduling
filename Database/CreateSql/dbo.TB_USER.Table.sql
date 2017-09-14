USE [JobScheduling]
GO
/****** Object:  Table [dbo].[TB_USER]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_USER](
	[UserID] [int] NOT NULL,
	[LoginName] [varchar](30) NOT NULL,
	[Password] [varchar](40) NOT NULL,
	[UserName] [varchar](30) NOT NULL,
	[WorkNo] [varchar](30) NULL,
	[DeptID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[InvalidFlag] [varchar](2) NOT NULL,
	[Remark] [varchar](4000) NULL,
 CONSTRAINT [PK_TB_USER] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
