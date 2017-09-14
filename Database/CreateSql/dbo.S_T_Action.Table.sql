USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_Action]    Script Date: 2017/9/14 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_T_Action](
	[code] [nvarchar](45) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[showIndex] [int] NULL,
 CONSTRAINT [PK_TB_Action] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
