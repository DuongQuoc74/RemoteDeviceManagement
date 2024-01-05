CREATE TABLE PI_ApiLogs(
	[PKApiLogsID] [int] IDENTITY(1,1) NOT NULL,
	[Api] [nvarchar](50) NOT NULL,
	[Request] [nvarchar](1000) NOT NULL,
	[Response] [nvarchar](max) NOT NULL,
	[FKMachineId] [int] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[DeviceGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PI_ApiLogs_Id] PRIMARY KEY CLUSTERED 
(
	[PKApiLogsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
