USE [HRM]
GO
/****** Object:  Table [dbo].[HRM_TR_TIMEOT]    Script Date: 18/03/2024 10:28:36 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HRM_TR_TIMEOT]') AND type in (N'U'))
DROP TABLE [dbo].[HRM_TR_TIMEOT]
GO
/****** Object:  Table [dbo].[HRM_TR_TIMEOT]    Script Date: 18/03/2024 10:28:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HRM_TR_TIMEOT](
	[COMPANY_CODE] [varchar](5) NOT NULL,
	[WORKER_CODE] [varchar](15) NOT NULL,
	[TIMEOT_ID] [bigint] NOT NULL,
	[TIMEOT_DOC] [varchar](30) NULL,
	[TIMEOT_WORKDATE] [datetime] NOT NULL,
	[TIMEOT_BEFOREMIN] [int] NULL,
	[TIMEOT_NORMALMIN] [int] NULL,
	[TIMEOT_AFTERMIN] [int] NULL,
	[TIMEOT_NOTE] [varchar](100) NULL,
	[REASON_CODE] [varchar](10) NULL,
	[LOCATION_CODE] [varchar](10) NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[MODIFIED_BY] [varchar](20) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[FLAG] [bit] NOT NULL,
	[TIMEOT_BREAK] [int] NULL,
 CONSTRAINT [PK_HRM_TR_TIMEOT] PRIMARY KEY CLUSTERED 
(
	[COMPANY_CODE] ASC,
	[WORKER_CODE] ASC,
	[TIMEOT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[HRM_TR_TIMEOT] ([COMPANY_CODE], [WORKER_CODE], [TIMEOT_ID], [TIMEOT_DOC], [TIMEOT_WORKDATE], [TIMEOT_BEFOREMIN], [TIMEOT_NORMALMIN], [TIMEOT_AFTERMIN], [TIMEOT_NOTE], [REASON_CODE], [LOCATION_CODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [FLAG], [TIMEOT_BREAK]) VALUES (N'APT', N'DRV0000022', 1, N'11', CAST(N'2024-03-15T00:00:00.000' AS DateTime), 180, 180, 180, N'3', N'01', N'00001', N'Admin', CAST(N'2024-03-15T19:02:39.080' AS DateTime), NULL, NULL, 0, 60)
INSERT [dbo].[HRM_TR_TIMEOT] ([COMPANY_CODE], [WORKER_CODE], [TIMEOT_ID], [TIMEOT_DOC], [TIMEOT_WORKDATE], [TIMEOT_BEFOREMIN], [TIMEOT_NORMALMIN], [TIMEOT_AFTERMIN], [TIMEOT_NOTE], [REASON_CODE], [LOCATION_CODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [FLAG], [TIMEOT_BREAK]) VALUES (N'APT', N'DRV0000022', 2, N'123', CAST(N'2024-01-01T00:00:00.000' AS DateTime), 180, 180, 180, N'4', N'01', N'00000', N'Admin', CAST(N'2024-03-18T09:21:50.940' AS DateTime), NULL, NULL, 0, 60)
INSERT [dbo].[HRM_TR_TIMEOT] ([COMPANY_CODE], [WORKER_CODE], [TIMEOT_ID], [TIMEOT_DOC], [TIMEOT_WORKDATE], [TIMEOT_BEFOREMIN], [TIMEOT_NORMALMIN], [TIMEOT_AFTERMIN], [TIMEOT_NOTE], [REASON_CODE], [LOCATION_CODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [FLAG], [TIMEOT_BREAK]) VALUES (N'APT', N'DRV0000022', 3, N'8', CAST(N'2024-01-02T00:00:00.000' AS DateTime), 0, 0, 9, N'8', N'01', N'00001', N'Admin', CAST(N'2024-03-18T09:58:09.447' AS DateTime), NULL, NULL, 0, 0)
INSERT [dbo].[HRM_TR_TIMEOT] ([COMPANY_CODE], [WORKER_CODE], [TIMEOT_ID], [TIMEOT_DOC], [TIMEOT_WORKDATE], [TIMEOT_BEFOREMIN], [TIMEOT_NORMALMIN], [TIMEOT_AFTERMIN], [TIMEOT_NOTE], [REASON_CODE], [LOCATION_CODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [FLAG], [TIMEOT_BREAK]) VALUES (N'APT', N'DRV0000022', 4, N'77', CAST(N'2024-01-03T00:00:00.000' AS DateTime), 0, 0, 9, N'i', N'01', N'00001', N'Admin', CAST(N'2024-03-18T09:59:39.690' AS DateTime), NULL, NULL, 0, 0)
INSERT [dbo].[HRM_TR_TIMEOT] ([COMPANY_CODE], [WORKER_CODE], [TIMEOT_ID], [TIMEOT_DOC], [TIMEOT_WORKDATE], [TIMEOT_BEFOREMIN], [TIMEOT_NORMALMIN], [TIMEOT_AFTERMIN], [TIMEOT_NOTE], [REASON_CODE], [LOCATION_CODE], [CREATED_BY], [CREATED_DATE], [MODIFIED_BY], [MODIFIED_DATE], [FLAG], [TIMEOT_BREAK]) VALUES (N'APT', N'DRV0000022', 5, N'77', CAST(N'2024-01-04T00:00:00.000' AS DateTime), 0, 0, 0, N'30', N'01', N'00001', N'Admin', CAST(N'2024-03-18T10:05:34.407' AS DateTime), NULL, NULL, 0, 0)
GO
