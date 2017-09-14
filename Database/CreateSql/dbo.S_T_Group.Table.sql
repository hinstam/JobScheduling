USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_Group]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_T_Group](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](100) NULL,
	[Operation] [nvarchar](45) NULL,
	[SystemID] [nvarchar](45) NULL,
	[Description] [nvarchar](400) NULL,
	[NumOfUsers] [int] NULL,
	[CreatedBy] [nvarchar](45) NULL,
	[CreatedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](45) NULL,
	[LastModifiedTime] [datetime] NULL,
	[IsDeleted] [tinyint] NULL,
	[DeletedBy] [nvarchar](45) NULL,
	[DeletedTime] [datetime] NULL,
 CONSTRAINT [PK_TB_Group] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[S_T_Group] ADD  CONSTRAINT [DF_TB_Group_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
