CREATE TABLE PI_Devices(
	[PKPicoID] [int] IDENTITY(1,1) NOT NULL,
	[FKMachineID] [int] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Remark] [nvarchar](1000) NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Available] [bit] NULL,
 CONSTRAINT [PK_PI_Devices] PRIMARY KEY CLUSTERED 
(
	[PKPicoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PI_Devices] ADD  CONSTRAINT [DF_PI_Devices_Available]  DEFAULT ((1)) FOR [Available]
GO