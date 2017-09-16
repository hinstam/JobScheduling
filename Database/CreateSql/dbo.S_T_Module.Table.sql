USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_Module]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_T_Module](
	[Code] [nvarchar](45) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[FatherCode] [nvarchar](45) NULL,
	[IsFatherNode] [tinyint] NULL,
	[ModuleIndex] [int] NULL,
	[ModuleController] [nvarchar](45) NULL,
	[ModuleAction] [nvarchar](45) NULL,
	[ModuleBelong] [nvarchar](45) NULL,
	[IsShow] [tinyint] NULL,
	[SystemID] [nvarchar](45) NULL,
	[CreatedBy] [nvarchar](45) NULL,
	[CreatedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](45) NULL,
	[LastModifiedTime] [datetime] NULL,
	[IsDeleted] [tinyint] NULL,
	[DeletedBy] [nvarchar](45) NULL,
	[DeletedTime] [datetime] NULL,
 CONSTRAINT [PK_S_T_Module] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[S_T_Module] ADD  CONSTRAINT [DF_S_T_Module_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
