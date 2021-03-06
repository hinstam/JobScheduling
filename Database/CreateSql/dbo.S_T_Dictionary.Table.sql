USE [JobScheduling]
GO
/****** Object:  Table [dbo].[S_T_Dictionary]    Script Date: 2017/9/20 星期三 下午 11:01:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[S_T_Dictionary](
	[DicCode] [varchar](12) NOT NULL,
	[DicName] [nvarchar](50) NOT NULL CONSTRAINT [DF_S_T_Dictionary_DicName]  DEFAULT (''),
	[ParentDicCode] [varchar](12) NOT NULL CONSTRAINT [DF_S_T_Dictionary_ParentDicCode]  DEFAULT (''),
	[Index] [int] NOT NULL,
	[Remark] [nvarchar](100) NULL,
	[IsAllow] [bit] NOT NULL CONSTRAINT [DF_Table_1_IsAllowed]  DEFAULT ((1)),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_S_T_Dictionary_CreateDate]  DEFAULT (getdate()),
	[EditDate] [datetime] NULL,
	[IsDel] [bit] NOT NULL CONSTRAINT [DF_S_T_Dictionary_IsDel]  DEFAULT ((0)),
	[Extend1] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001', N'生产线', N'', 1, NULL, 1, CAST(N'2017-09-20 14:52:49.930' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001001', N'Pot1', N'001', 1, NULL, 1, CAST(N'2017-09-20 14:54:24.510' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001002', N'Pol2', N'001', 2, NULL, 1, CAST(N'2017-09-20 14:54:42.543' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002', N'班次', N'', 2, NULL, 1, CAST(N'2017-09-20 14:55:26.903' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002001', N'夜', N'002', 1, NULL, 1, CAST(N'2017-09-20 14:55:52.470' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002002', N'日', N'002', 2, NULL, 1, CAST(N'2017-09-20 14:56:04.320' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002003', N'中', N'002', 3, NULL, 1, CAST(N'2017-09-20 14:56:15.807' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003', N'操作类型', N'', 3, NULL, 1, CAST(N'2017-09-20 14:58:18.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003001', N'BRG', N'003', 1, NULL, 1, CAST(N'2017-09-20 15:00:04.100' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003002', N'BRP
', N'003', 2, NULL, 1, CAST(N'2017-09-20 15:00:33.870' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003003', N'CAW', N'003', 3, NULL, 1, CAST(N'2017-09-20 15:00:41.577' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003004', N'CAG', N'003', 4, NULL, 1, CAST(N'2017-09-20 15:00:51.080' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003005', N'DBL', N'003', 5, NULL, 1, CAST(N'2017-09-20 15:00:59.447' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003006', N'FW/O', N'003', 6, NULL, 1, CAST(N'2017-09-20 15:01:09.543' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003007', N'GT', N'003', 7, NULL, 1, CAST(N'2017-09-20 15:01:18.583' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003008', N'HES', N'003', 8, NULL, 1, CAST(N'2017-09-20 15:01:27.960' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003009', N'HBW', N'003', 9, NULL, 1, CAST(N'2017-09-20 15:01:39.337' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003010', N'HFP', N'003', 10, NULL, 1, CAST(N'2017-09-20 15:01:48.760' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003011', N'HTD', N'003', 11, NULL, 1, CAST(N'2017-09-20 15:01:55.920' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003012', N'HNW', N'003', 12, NULL, 1, CAST(N'2017-09-20 15:02:03.190' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003013', N'HNG', N'003', 13, NULL, 1, CAST(N'2017-09-20 15:02:11.807' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003014', N'HHW', N'003', 14, NULL, 1, CAST(N'2017-09-20 15:02:19.750' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003015', N'HRG', N'003', 15, NULL, 1, CAST(N'2017-09-20 15:02:27.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003016', N'KFP', N'003', 16, NULL, 1, CAST(N'2017-09-20 15:02:35.503' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003017', N'NGT', N'003', 17, NULL, 1, CAST(N'2017-09-20 15:02:45.553' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003018', N'NBW', N'003', 18, NULL, 1, CAST(N'2017-09-20 15:02:56.017' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003019', N'PC', N'003', 19, NULL, 1, CAST(N'2017-09-20 15:03:05.440' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003020', N'PSO', N'003', 20, NULL, 1, CAST(N'2017-09-20 15:03:12.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003021', N'PSL', N'003', 21, NULL, 1, CAST(N'2017-09-20 15:03:21.797' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003022', N'PSM', N'003', 22, NULL, 1, CAST(N'2017-09-20 15:03:31.277' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003023', N'PW', N'003', 23, NULL, 1, CAST(N'2017-09-20 15:35:27.240' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003024', N'PB', N'003', 24, NULL, 1, CAST(N'2017-09-20 15:35:38.570' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003025', N'QW/O ', N'003', 25, NULL, 1, CAST(N'2017-09-20 15:35:49.593' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003026', N'RW/O', N'003', 26, NULL, 1, CAST(N'2017-09-20 15:35:59.857' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003027', N'SW', N'003', 27, NULL, 1, CAST(N'2017-09-20 15:36:08.303' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003028', N'W/O', N'003', 28, NULL, 1, CAST(N'2017-09-20 15:36:20.000' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003029', N'WT', N'003', 29, NULL, 1, CAST(N'2017-09-20 15:36:32.170' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003030', N'WS', N'003', 30, NULL, 1, CAST(N'2017-09-20 15:36:42.400' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003031', N'WG', N'003', 31, NULL, 1, CAST(N'2017-09-20 15:36:52.473' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001', N'生产线', N'', 1, NULL, 1, CAST(N'2017-09-20 14:52:49.930' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001001', N'Pot1', N'001', 1, NULL, 1, CAST(N'2017-09-20 14:54:24.510' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'001002', N'Pol2', N'001', 2, NULL, 1, CAST(N'2017-09-20 14:54:42.543' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002', N'班次', N'', 2, NULL, 1, CAST(N'2017-09-20 14:55:26.903' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002001', N'夜', N'002', 1, NULL, 1, CAST(N'2017-09-20 14:55:52.470' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002002', N'日', N'002', 2, NULL, 1, CAST(N'2017-09-20 14:56:04.320' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'002003', N'中', N'002', 3, NULL, 1, CAST(N'2017-09-20 14:56:15.807' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003', N'操作类型', N'', 3, NULL, 1, CAST(N'2017-09-20 14:58:18.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003001', N'BRG', N'003', 1, NULL, 1, CAST(N'2017-09-20 15:00:04.100' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003002', N'BRP
', N'003', 2, NULL, 1, CAST(N'2017-09-20 15:00:33.870' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003003', N'CAW', N'003', 3, NULL, 1, CAST(N'2017-09-20 15:00:41.577' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003004', N'CAG', N'003', 4, NULL, 1, CAST(N'2017-09-20 15:00:51.080' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003005', N'DBL', N'003', 5, NULL, 1, CAST(N'2017-09-20 15:00:59.447' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003006', N'FW/O', N'003', 6, NULL, 1, CAST(N'2017-09-20 15:01:09.543' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003007', N'GT', N'003', 7, NULL, 1, CAST(N'2017-09-20 15:01:18.583' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003008', N'HES', N'003', 8, NULL, 1, CAST(N'2017-09-20 15:01:27.960' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003009', N'HBW', N'003', 9, NULL, 1, CAST(N'2017-09-20 15:01:39.337' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003010', N'HFP', N'003', 10, NULL, 1, CAST(N'2017-09-20 15:01:48.760' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003011', N'HTD', N'003', 11, NULL, 1, CAST(N'2017-09-20 15:01:55.920' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003012', N'HNW', N'003', 12, NULL, 1, CAST(N'2017-09-20 15:02:03.190' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003013', N'HNG', N'003', 13, NULL, 1, CAST(N'2017-09-20 15:02:11.807' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003014', N'HHW', N'003', 14, NULL, 1, CAST(N'2017-09-20 15:02:19.750' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003015', N'HRG', N'003', 15, NULL, 1, CAST(N'2017-09-20 15:02:27.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003016', N'KFP', N'003', 16, NULL, 1, CAST(N'2017-09-20 15:02:35.503' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003017', N'NGT', N'003', 17, NULL, 1, CAST(N'2017-09-20 15:02:45.553' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003018', N'NBW', N'003', 18, NULL, 1, CAST(N'2017-09-20 15:02:56.017' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003019', N'PC', N'003', 19, NULL, 1, CAST(N'2017-09-20 15:03:05.440' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003020', N'PSO', N'003', 20, NULL, 1, CAST(N'2017-09-20 15:03:12.880' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003021', N'PSL', N'003', 21, NULL, 1, CAST(N'2017-09-20 15:03:21.797' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003022', N'PSM', N'003', 22, NULL, 1, CAST(N'2017-09-20 15:03:31.277' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003023', N'PW', N'003', 23, NULL, 1, CAST(N'2017-09-20 15:35:27.240' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003024', N'PB', N'003', 24, NULL, 1, CAST(N'2017-09-20 15:35:38.570' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003025', N'QW/O ', N'003', 25, NULL, 1, CAST(N'2017-09-20 15:35:49.593' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003026', N'RW/O', N'003', 26, NULL, 1, CAST(N'2017-09-20 15:35:59.857' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003027', N'SW', N'003', 27, NULL, 1, CAST(N'2017-09-20 15:36:08.303' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003028', N'W/O', N'003', 28, NULL, 1, CAST(N'2017-09-20 15:36:20.000' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003029', N'WT', N'003', 29, NULL, 1, CAST(N'2017-09-20 15:36:32.170' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003030', N'WS', N'003', 30, NULL, 1, CAST(N'2017-09-20 15:36:42.400' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[S_T_Dictionary] ([DicCode], [DicName], [ParentDicCode], [Index], [Remark], [IsAllow], [CreateDate], [EditDate], [IsDel], [Extend1]) VALUES (N'003031', N'WG', N'003', 31, NULL, 1, CAST(N'2017-09-20 15:36:52.473' AS DateTime), NULL, 0, NULL)
