
/****** Object:  Table [dbo].[Apartments]    Script Date: 21-Mar-20 2:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartments](
	[ApartmentId] [int] NOT NULL,
	[HouseId] [int] NOT NULL,
	[RoomsCount] [smallint] NOT NULL,
	[SAll] [decimal](8, 2) NOT NULL,
	[Floor] [smallint] NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_Apartments] PRIMARY KEY CLUSTERED 
(
	[ApartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Districts]    Script Date: 21-Mar-20 2:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[DistrictId] [smallint] NOT NULL,
	[DistrictName] [nvarchar](100) NOT NULL,
	[RegionId] [smallint] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Houses]    Script Date: 21-Mar-20 2:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Houses](
	[HouseId] [int] NOT NULL,
	[StageNumber] [smallint] NOT NULL,
	[HouseNumber] [nvarchar](50) NOT NULL,
	[ComplexName] [nvarchar](100) NOT NULL,
	[DistrictId] [smallint] NOT NULL,
 CONSTRAINT [PK_Houses] PRIMARY KEY CLUSTERED 
(
	[HouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Regions]    Script Date: 21-Mar-20 2:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[RegionId] [smallint] NOT NULL,
	[RegionName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Apartments]  WITH CHECK ADD  CONSTRAINT [FK_Apartments_Houses] FOREIGN KEY([HouseId])
REFERENCES [dbo].[Houses] ([HouseId])
GO
ALTER TABLE [dbo].[Apartments] CHECK CONSTRAINT [FK_Apartments_Houses]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_Districts_Regions] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_Districts_Regions]
GO
ALTER TABLE [dbo].[Houses]  WITH CHECK ADD  CONSTRAINT [FK_Houses_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
GO
ALTER TABLE [dbo].[Houses] CHECK CONSTRAINT [FK_Houses_Districts]
GO

