USE [JobScheduling]
GO
/****** Object:  Table [dbo].[TB_ProductScheduling]    Script Date: 2017/9/20 星期三 下午 11:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_ProductScheduling](
	[ID] [int] NOT NULL,
	[PolCode] [varchar](12) NOT NULL,
	[OperationTypeCode] [varchar](12) NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[SchedulingDate] [datetime] NOT NULL,
	[IsFirstSchedulingDate] [bit] NOT NULL,
	[ShiftTypeCode] [varchar](12) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_TB_ProductScheduling] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TB_ProductScheduling] ADD  CONSTRAINT [DF_TB_ProductScheduling_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[TB_ProductScheduling] ADD  CONSTRAINT [DF_TB_ProductScheduling_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
