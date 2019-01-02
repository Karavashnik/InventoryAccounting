USE [InventoryAccounting]
GO
/****** Object:  Table [dbo].[Acts]    Script Date: 02.01.2019 13:50:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Acts](
	[Id] [int] NOT NULL,
	[CompilationDate] [date] NOT NULL,
	[ContractNumber] [int] NOT NULL,
 CONSTRAINT [PK_Acts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyName]    Script Date: 02.01.2019 13:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyName](
	[UNP] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](80) NULL,
	[DirectorsName] [varchar](30) NOT NULL,
	[DirectorsPhone] [varchar](15) NULL,
 CONSTRAINT [PK_CompanyName] PRIMARY KEY CLUSTERED 
(
	[UNP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 02.01.2019 13:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[ContractNumber] [int] NOT NULL,
	[CompanyUNP] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[ContractNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResponsiblePersons]    Script Date: 02.01.2019 13:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResponsiblePersons](
	[PersonnelNumber] [int] NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[DateOfBirth] [date] NOT NULL,
	[PassportDetails] [varchar](255) NOT NULL,
	[Education] [varchar](50) NULL,
	[DateOfEmployment] [date] NOT NULL,
	[Phone] [varchar](15) NULL,
	[Email] [varchar](30) NULL,
	[Post] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ResponsiblePersons] PRIMARY KEY CLUSTERED 
(
	[PersonnelNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 02.01.2019 13:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[Floor] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Phone] [varchar](15) NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TMC]    Script Date: 02.01.2019 13:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMC](
	[InventoryNumber] [int] NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Description] [varchar](255) NULL,
	[Type] [varchar](50) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[PesponsiblePersonNumber] [int] NOT NULL,
	[FactoryNumber] [int] NOT NULL,
	[WriteOffDate] [date] NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[ActId] [int] NULL,
	[WarrantyDate] [date] NULL,
 CONSTRAINT [PK_InventoryName] PRIMARY KEY CLUSTERED 
(
	[InventoryNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_Contracts] FOREIGN KEY([ContractNumber])
REFERENCES [dbo].[Contracts] ([ContractNumber])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_Contracts]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_CompanyName] FOREIGN KEY([CompanyUNP])
REFERENCES [dbo].[CompanyName] ([UNP])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_CompanyName]
GO
ALTER TABLE [dbo].[TMC]  WITH CHECK ADD  CONSTRAINT [FK_InventoryName_ResponsiblePersons] FOREIGN KEY([PesponsiblePersonNumber])
REFERENCES [dbo].[ResponsiblePersons] ([PersonnelNumber])
GO
ALTER TABLE [dbo].[TMC] CHECK CONSTRAINT [FK_InventoryName_ResponsiblePersons]
GO
ALTER TABLE [dbo].[TMC]  WITH CHECK ADD  CONSTRAINT [FK_TMC_Acts] FOREIGN KEY([ActId])
REFERENCES [dbo].[Acts] ([Id])
GO
ALTER TABLE [dbo].[TMC] CHECK CONSTRAINT [FK_TMC_Acts]
GO
ALTER TABLE [dbo].[TMC]  WITH CHECK ADD  CONSTRAINT [FK_TMC_Rooms] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[TMC] CHECK CONSTRAINT [FK_TMC_Rooms]
GO
