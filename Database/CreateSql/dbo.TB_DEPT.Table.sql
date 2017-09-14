USE [JobScheduling]
GO
/****** Object:  Table [dbo].[TB_DEPT]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_DEPT](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[DeptCode] [varchar](18) NOT NULL,
	[DeptName] [varchar](40) NOT NULL,
	[UpperDeptID] [int] NOT NULL,
	[Remark] [varchar](4000) NULL,
	[Lev] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[InvalidFlag] [varchar](2) NOT NULL,
 CONSTRAINT [PK_TB_DEPT] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
