--Create Devices table
CREATE TABLE PI_Settings(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
	[Available] [bit] NULL,
    [IsPico] [bit] NULL,
 CONSTRAINT [PK_PI_Settings_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PI_Settings] ADD  CONSTRAINT [DF_PI_Settings_Available]  DEFAULT ((1)) FOR [Available]
GO
--Done Create Settings table

--Seed basic settings
USE [ME_Attrition]
GO
--API-KEY
INSERT INTO [dbo].[PI_Settings]
           ([Name]
           ,[Value]
           ,[Available]
           ,[IsPico])
     VALUES
           ('api-key','n8UkpZTgVXU1cXqkOtl8UAq3ZNg7UtYCEBHofdMMh0iPd1ZuYnj6Ry4gC7lFqX4h',1,0)
           ,('OnlineTimeOutMinutes','10',1,1)
           ,('TicketsApiCheckTimeOutMinutes','5',1,1)
           ,('TicketsValidDays','1',1,1)
           ,('CloseTicketURL','192.168.1.3:8081/Pages/ScreenAttrition.aspx',1,1)
           ,('CloseTicketParameterGidzl','70pPSROhh3mV4jrolZ2kFnSJrt2WOFKyMqU1Tw5Zzse0JDKahsYZRbKQsYMkElmv1Hk89JUP5q1ujYkXF0',1,1)
           ,('CloseTicketParameterProcess','Attrition',1,1)
           ,('CloseTicketParameterProcesId','2',1,1);
GO
--Done Seed basic settings