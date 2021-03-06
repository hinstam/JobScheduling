USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_ActionLog]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[S_T_ActionLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](100) NULL,
	[OPType] [varchar](20) NULL,
	[OPContent] [varchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_S_T_ActionLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
