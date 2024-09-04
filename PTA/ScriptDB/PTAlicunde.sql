CREATE DATABASE [PTAlicunde]
GO

USE [PTAlicunde]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DistributionSystemOperator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodingScheme] [nvarchar](10) NULL,
	[Country] [nvarchar](5) NULL,
	[DsoCode] [nvarchar](30) NULL,
	[DsoName] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](56) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](56) NULL,
 CONSTRAINT [PK_DistributionSystemOperator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO