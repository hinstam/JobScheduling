USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_TR_Module_Action]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_TR_Module_Action](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [nvarchar](45) NULL,
	[ActionCode] [nvarchar](45) NULL,
 CONSTRAINT [PK_S_TR_Module_Action] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
