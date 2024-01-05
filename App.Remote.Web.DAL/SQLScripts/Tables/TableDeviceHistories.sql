CREATE TABLE PI_DeviceHistories(
	[PKDeviceHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[FKPicoID] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[Remark] [nvarchar](100) NULL,
	[FKMachineID] [int] NULL,
 CONSTRAINT [PK_PI_DeviceHistories] PRIMARY KEY CLUSTERED 
(
	[PKDeviceHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
