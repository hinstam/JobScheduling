USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_Accessright]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_T_Accessright](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [nvarchar](45) NOT NULL,
	[GroupID] [int] NOT NULL,
	[ActionCode] [nvarchar](45) NOT NULL,
	[ActionName] [nvarchar](45) NULL,
	[IsAllow] [tinyint] NULL,
	[CreatedBy] [nvarchar](45) NULL,
	[CreatedTime] [datetime] NULL,
 CONSTRAINT [PK_TB_Accessright] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
