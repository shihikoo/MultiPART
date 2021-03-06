USE [master]
GO
/****** Object:  Database [MULTIPART2]    Script Date: 05/05/2015 10:50:35 ******/
CREATE DATABASE [MULTIPART2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MULTIPART2', FILENAME = N'E:\sqldata\MULTIPART2.mdf' , SIZE = 6144KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MULTIPART2_log', FILENAME = N'F:\sqllog\MULTIPART2_log.ldf' , SIZE = 16512KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MULTIPART2] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MULTIPART2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MULTIPART2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MULTIPART2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MULTIPART2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MULTIPART2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MULTIPART2] SET ARITHABORT OFF 
GO
ALTER DATABASE [MULTIPART2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MULTIPART2] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MULTIPART2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MULTIPART2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MULTIPART2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MULTIPART2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MULTIPART2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MULTIPART2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MULTIPART2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MULTIPART2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MULTIPART2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MULTIPART2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MULTIPART2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MULTIPART2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MULTIPART2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MULTIPART2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MULTIPART2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MULTIPART2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MULTIPART2] SET RECOVERY FULL 
GO
ALTER DATABASE [MULTIPART2] SET  MULTI_USER 
GO
ALTER DATABASE [MULTIPART2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MULTIPART2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MULTIPART2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MULTIPART2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MULTIPART2]
GO
/****** Object:  User [multipart2sysapp]    Script Date: 05/05/2015 10:50:35 ******/
CREATE USER [multipart2sysapp] FOR LOGIN [multipart2sysapp] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[AnimalConditions]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnimalConditions](
	[AnimalConditionID] [int] IDENTITY(1,1) NOT NULL,
	[AnimalConditionName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AnimalConditions] PRIMARY KEY CLUSTERED 
(
	[AnimalConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnimalHusbandries]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnimalHusbandries](
	[AnimalHusbandryID] [int] IDENTITY(1,1) NOT NULL,
	[MultiPARTProjectMultiPARTProjectID] [int] NOT NULL,
	[ResearchgroupID] [int] NOT NULL,
	[StrainID] [int] NOT NULL,
	[AnimalHusbandryLabel] [nvarchar](max) NULL,
	[BeddingMaterialID] [int] NULL,
	[HandlingID] [int] NULL,
	[BiosecurityLevelID] [int] NULL,
	[VentilationID] [int] NULL,
	[WaterAccessID] [int] NULL,
	[CageSizeHeight] [real] NULL,
	[CageSizeWidth] [real] NULL,
	[CageSizeDepth] [real] NULL,
	[NumberOfCageChangePerWeek] [int] NULL,
	[EnrichmentType] [nvarchar](max) NULL,
	[FoodType] [nvarchar](max) NULL,
	[FoodDeprivation] [nvarchar](max) NULL,
	[FoodDeprivationSchedule] [nvarchar](max) NULL,
	[LightingHoursLightPer24hours] [int] NULL,
	[NumberOfExperimentersInteractWithAnimals] [int] NULL,
	[AnimalHousedPerCagePriorToSurgicalProcedure] [int] NULL,
	[NumberOfAnimalsPerCage] [int] NULL,
	[RoomTemperatureMin] [nvarchar](max) NULL,
	[RoomTemperatureMax] [nvarchar](max) NULL,
	[RelativeHumidityMin] [nvarchar](max) NULL,
	[RelativeHumidityMax] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.AnimalHusbandries] PRIMARY KEY CLUSTERED 
(
	[AnimalHusbandryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Animals]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animals](
	[AnimalID] [int] IDENTITY(1,1) NOT NULL,
	[AnimalLabel] [nvarchar](max) NOT NULL,
	[CohortID] [int] NOT NULL,
	[ResearchgroupID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
	[SexID] [int] NULL,
	[Weight] [real] NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_dbo.Animals] PRIMARY KEY CLUSTERED 
(
	[AnimalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnimalSuppliers]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnimalSuppliers](
	[AnimalSupplierID] [int] IDENTITY(1,1) NOT NULL,
	[AnimalSupplierName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AnimalSuppliers] PRIMARY KEY CLUSTERED 
(
	[AnimalSupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Careerhistories]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Careerhistories](
	[CareerhistoryID] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileUserId] [int] NOT NULL,
	[InstitutionInstitutionID] [int] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Position] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Careerhistories] PRIMARY KEY CLUSTERED 
(
	[CareerhistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CohortProcedureAssignments]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CohortProcedureAssignments](
	[CohortProcedureAssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[CohortID] [int] NOT NULL,
	[ProcedureID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.CohortProcedureAssignments] PRIMARY KEY CLUSTERED 
(
	[CohortProcedureAssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cohorts]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cohorts](
	[CohortID] [int] IDENTITY(1,1) NOT NULL,
	[MultiPARTProjectMultiPARTProjectID] [int] NOT NULL,
	[StrainStrainID] [int] NULL,
	[AnimalSupplierAnimalSupplierID] [int] NULL,
	[CohortLabel] [nvarchar](max) NULL,
	[SampleSize] [int] NULL,
	[CategoricalAgeID] [int] NULL,
	[Details] [nvarchar](max) NULL,
	[SexID] [int] NULL,
	[MinAge] [real] NULL,
	[MaxAge] [real] NULL,
	[MinWeight] [real] NULL,
	[MaxWeight] [real] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Cohorts] PRIMARY KEY CLUSTERED 
(
	[CohortID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommercialDrugComponents]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommercialDrugComponents](
	[CommercialDrugComponentID] [int] IDENTITY(1,1) NOT NULL,
	[CommercialDrugID] [int] NOT NULL,
	[DrugID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.CommercialDrugComponents] PRIMARY KEY CLUSTERED 
(
	[CommercialDrugComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommercialDrugs]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommercialDrugs](
	[CommercialDrugID] [int] IDENTITY(1,1) NOT NULL,
	[CommercialDrugName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.CommercialDrugs] PRIMARY KEY CLUSTERED 
(
	[CommercialDrugID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](max) NOT NULL,
	[CountryCode] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiseaseModels]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiseaseModels](
	[DiseaseModelID] [int] IDENTITY(1,1) NOT NULL,
	[HumanConditionHumanConditionID] [int] NOT NULL,
	[HumanConditionTypeID] [int] NOT NULL,
	[AnimalConditionAnimalConditionID] [int] NOT NULL,
	[DiseaseModelDetail] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.DiseaseModels] PRIMARY KEY CLUSTERED 
(
	[DiseaseModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Drugs]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drugs](
	[DrugID] [int] IDENTITY(1,1) NOT NULL,
	[DrugName] [nvarchar](max) NOT NULL,
	[DrugGroup] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Drugs] PRIMARY KEY CLUSTERED 
(
	[DrugID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HumanConditions]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HumanConditions](
	[HumanConditionID] [int] IDENTITY(1,1) NOT NULL,
	[HumanConditionName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.HumanConditions] PRIMARY KEY CLUSTERED 
(
	[HumanConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Institutions]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institutions](
	[InstitutionID] [int] IDENTITY(1,1) NOT NULL,
	[InstitutionName] [nvarchar](max) NOT NULL,
	[CountryCountryID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Institutions] PRIMARY KEY CLUSTERED 
(
	[InstitutionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MultiPARTProjects]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MultiPARTProjects](
	[MultiPARTProjectID] [int] IDENTITY(1,1) NOT NULL,
	[MultiPARTProjectName] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NULL,
	[ProjectStartDate] [datetime] NOT NULL,
	[ProjectCompletionDateExpected] [datetime] NOT NULL,
	[ProjectComletionDate] [datetime] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[ExperimentalQuestion] [nvarchar](max) NULL,
	[Aim] [nvarchar](max) NULL,
	[Logo] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MultiPARTProjects] PRIMARY KEY CLUSTERED 
(
	[MultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OptionFields]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OptionFields](
	[OptionFieldID] [int] IDENTITY(1,1) NOT NULL,
	[OptionFieldName] [nvarchar](max) NULL,
	[TableName] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OptionFields] PRIMARY KEY CLUSTERED 
(
	[OptionFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Options]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Options](
	[OptionID] [int] IDENTITY(1,1) NOT NULL,
	[OptionFieldOptionFieldID] [int] NOT NULL,
	[OptionValue] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Options] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureDetailOptionFields]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureDetailOptionFields](
	[ProcedureDetailOptionFieldID] [int] IDENTITY(1,1) NOT NULL,
	[ProcedurePurposeOrTypeID] [int] NOT NULL,
	[ProcedureDetailFieldName] [nvarchar](max) NULL,
	[ProcedureDetailType] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProcedureDetailOptionFields] PRIMARY KEY CLUSTERED 
(
	[ProcedureDetailOptionFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureDetailOptions]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureDetailOptions](
	[ProcedureDetailOptionID] [int] IDENTITY(1,1) NOT NULL,
	[ProcedureDetailOptionFieldID] [int] NOT NULL,
	[ProcedureDetailOptionName] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProcedureDetailOptions] PRIMARY KEY CLUSTERED 
(
	[ProcedureDetailOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProcedureDetails]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcedureDetails](
	[ProcedureDetailID] [int] IDENTITY(1,1) NOT NULL,
	[ProcedureProcedureID] [int] NOT NULL,
	[ProcedureDetailOptionFieldID] [int] NOT NULL,
	[ProcedureDetailOptionID] [int] NULL,
	[ProcedureDetailValue] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.ProcedureDetails] PRIMARY KEY CLUSTERED 
(
	[ProcedureDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Procedures]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procedures](
	[ProcedureID] [int] IDENTITY(1,1) NOT NULL,
	[MultiPARTProjectMultiPARTProjectID] [int] NOT NULL,
	[ProcedureTypeID] [int] NULL,
	[ProcedurePurposeID] [int] NOT NULL,
	[ProcedureLabel] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NULL,
	[AdministrationTypeID] [int] NULL,
	[AdministrationMethodID] [int] NULL,
	[StartTime] [real] NOT NULL,
	[EndTime] [real] NULL,
	[DurationOfEachAssessment] [real] NULL,
	[NumbersOfAdministrations] [int] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Procedures] PRIMARY KEY CLUSTERED 
(
	[ProcedureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResearchgroupCohortAssignments]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResearchgroupCohortAssignments](
	[ResearchgroupCohortAssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[CohortID] [int] NOT NULL,
	[ResearchgroupID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
	[NumberOfAnimals] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ResearchgroupCohortAssignments] PRIMARY KEY CLUSTERED 
(
	[ResearchgroupCohortAssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResearchgroupInMultiPARTProjects]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResearchgroupInMultiPARTProjects](
	[ResearchgroupInMultiPARTProjectID] [int] IDENTITY(1,1) NOT NULL,
	[MultiPARTProjectMultiPARTProjectID] [int] NOT NULL,
	[ResearchgroupResearchgroupID] [int] NOT NULL,
	[RegistrationDate] [datetime] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
	[ResearchgroupRoleinMultiPARTProjectID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ResearchgroupInMultiPARTProjects] PRIMARY KEY CLUSTERED 
(
	[ResearchgroupInMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Researchgroups]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Researchgroups](
	[ResearchgroupID] [int] IDENTITY(1,1) NOT NULL,
	[ResearchgroupName] [nvarchar](max) NOT NULL,
	[InstitutionInstitutionID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.Researchgroups] PRIMARY KEY CLUSTERED 
(
	[ResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Species]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Species](
	[SpecieID] [int] IDENTITY(1,1) NOT NULL,
	[SpecieName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Species] PRIMARY KEY CLUSTERED 
(
	[SpecieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Strains]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Strains](
	[StrainID] [int] IDENTITY(1,1) NOT NULL,
	[StrainName] [nvarchar](max) NOT NULL,
	[SpecieID] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Strains] PRIMARY KEY CLUSTERED 
(
	[StrainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInResearchgroups]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInResearchgroups](
	[UserInResearchgroupID] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileUserId] [int] NOT NULL,
	[ResearchgroupResearchgroupID] [int] NOT NULL,
	[UserRoleinResearchgroupID] [int] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_dbo.UserInResearchgroups] PRIMARY KEY CLUSTERED 
(
	[UserInResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[ForeName] [nvarchar](max) NULL,
	[SurName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Details] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_dbo.UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProjectAssignments]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProjectAssignments](
	[UserProjectAssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID] [int] NOT NULL,
	[UserInResearchgroupUserInResearchgroupID] [int] NOT NULL,
	[UserRoleinProjectID] [int] NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdatedOn] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserProjectAssignments] PRIMARY KEY CLUSTERED 
(
	[UserProjectAssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 05/05/2015 10:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AnimalConditions] ON 

INSERT [dbo].[AnimalConditions] ([AnimalConditionID], [AnimalConditionName], [Status]) VALUES (1, N'Focal ischemia', N'Current')
SET IDENTITY_INSERT [dbo].[AnimalConditions] OFF
SET IDENTITY_INSERT [dbo].[Animals] ON 

INSERT [dbo].[Animals] ([AnimalID], [AnimalLabel], [CohortID], [ResearchgroupID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [SexID], [Weight], [Age]) VALUES (76, N'animal 1', 17, 1, N'Current', 1, CAST(0x07A914025C75E9390B3C00 AS DateTimeOffset), 1, CAST(0x07D26D195C75E9390B3C00 AS DateTimeOffset), NULL, 0, NULL)
INSERT [dbo].[Animals] ([AnimalID], [AnimalLabel], [CohortID], [ResearchgroupID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [SexID], [Weight], [Age]) VALUES (77, N'animal 2', 16, 1, N'Current', 1, CAST(0x07C22BC58C75E9390B3C00 AS DateTimeOffset), 1, CAST(0x07393DC68C75E9390B3C00 AS DateTimeOffset), NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Animals] OFF
SET IDENTITY_INSERT [dbo].[AnimalSuppliers] ON 

INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (1, N'In-house breeding', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (2, N'Jackson Laboratory', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (3, N'Charles River', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (4, N'Covance', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (5, N'Huntingdon Life Sciences', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (6, N'Harlan', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (7, N'University of Leipzig (experimental breeding farm)', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (8, N'SILABE', N'Current')
INSERT [dbo].[AnimalSuppliers] ([AnimalSupplierID], [AnimalSupplierName], [Status]) VALUES (9, N'Others', N'Current')
SET IDENTITY_INSERT [dbo].[AnimalSuppliers] OFF
SET IDENTITY_INSERT [dbo].[Careerhistories] ON 

INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (1, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A43200000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x0760CD41F68B9C390B0000 AS DateTimeOffset), 1, CAST(0x07F09D45F68B9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (2, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A43200000000 AS DateTime), N'refa', N'Deleted', 1, CAST(0x07808917088C9C390B0000 AS DateTimeOffset), 1, CAST(0x07A0D717088C9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (3, 1, 10, CAST(0x0000A43400000000 AS DateTime), CAST(0x0000A43800000000 AS DateTime), N'asdfasd', N'Deleted', 1, CAST(0x0780B3C8138C9C390B0000 AS DateTimeOffset), 1, CAST(0x07B028C9138C9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (4, 1, 10, CAST(0x0000A43400000000 AS DateTime), CAST(0x0000A43800000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x0780C682A2AD9C390B0000 AS DateTimeOffset), 1, CAST(0x07B03B83A2AD9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (5, 1, 7, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A43300000000 AS DateTime), N'asdf', N'Deleted', 1, CAST(0x07D0311AAFAD9C390B0000 AS DateTimeOffset), 1, CAST(0x0700A71AAFAD9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (6, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A44D00000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x07E08000CEAD9C390B0000 AS DateTimeOffset), 1, CAST(0x0710F600CEAD9C390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (7, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A43300000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x0768DF4CED659D390B0000 AS DateTimeOffset), 1, CAST(0x07384C51ED659D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (8, 1, 5, CAST(0x0000A43500000000 AS DateTime), CAST(0x0000A43000000000 AS DateTime), N'asdfasf', N'Deleted', 1, CAST(0x0758EF6FF7659D390B0000 AS DateTimeOffset), 1, CAST(0x07783D70F7659D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (9, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A44600000000 AS DateTime), N'1', N'Deleted', 1, CAST(0x0788CE4C01669D390B0000 AS DateTimeOffset), 1, CAST(0x07B8434D01669D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (10, 1, 5, CAST(0x0000A41300000000 AS DateTime), CAST(0x0000A43200000000 AS DateTime), N'2', N'Deleted', 1, CAST(0x0788F97D09669D390B0000 AS DateTimeOffset), 1, CAST(0x07B86E7E09669D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (11, 1, 5, CAST(0x0000A43200000000 AS DateTime), NULL, N'sd', N'Current', 1, CAST(0x07A8C22FA3689D390B0000 AS DateTimeOffset), 1, CAST(0x07F88530A3689D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (12, 1, 6, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A44500000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x07889E177F6B9D390B0000 AS DateTimeOffset), 1, CAST(0x0708481B7F6B9D390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (13, 1, 7, CAST(0x0000A2A600000000 AS DateTime), NULL, N're', N'Current', 1, CAST(0x0732E9647A579E390B0000 AS DateTimeOffset), 1, CAST(0x07B292687A579E390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (14, 4, 12, CAST(0x0000A43200000000 AS DateTime), NULL, NULL, N'Current', 1, CAST(0x07733E6D70889F390B0000 AS DateTimeOffset), 1, CAST(0x07FAC07070889F390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (15, 4, 11, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A44D00000000 AS DateTime), NULL, N'Current', 1, CAST(0x07CFC5C676889F390B0000 AS DateTimeOffset), 1, CAST(0x07F113C776889F390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (16, 7, 6, CAST(0x0000A43200000000 AS DateTime), NULL, NULL, N'Current', 1, CAST(0x07753B1884889F390B0000 AS DateTimeOffset), 1, CAST(0x0797891884889F390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (17, 1, 14, CAST(0x0000A13900000000 AS DateTime), CAST(0x0000A2CA00000000 AS DateTime), NULL, N'Current', 1, CAST(0x077B3D393F86A0390B0000 AS DateTimeOffset), 1, CAST(0x07BFD9393F86A0390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (18, 8, 19, CAST(0x0000A44E00000000 AS DateTime), NULL, NULL, N'Current', 8, CAST(0x07FCC1924F6AC0390B0000 AS DateTimeOffset), 8, CAST(0x077CFA934F6AC0390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (19, 1, 19, CAST(0x0000A46500000000 AS DateTime), NULL, NULL, N'Current', 1, CAST(0x07ACFBA8146EC0390B0000 AS DateTimeOffset), 1, CAST(0x076C41AD146EC0390B0000 AS DateTimeOffset))
INSERT [dbo].[Careerhistories] ([CareerhistoryID], [UserProfileUserId], [InstitutionInstitutionID], [StartTime], [EndTime], [Position], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (20, 9, 5, CAST(0x0000A48B00000000 AS DateTime), NULL, NULL, N'Current', 9, CAST(0x0745303F0B70E9390B3C00 AS DateTimeOffset), 9, CAST(0x0740A24A0B70E9390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Careerhistories] OFF
SET IDENTITY_INSERT [dbo].[CohortProcedureAssignments] ON 

INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (58, 16, 2, N'Delete', 9, CAST(0x07828DF98B73E9390B3C00 AS DateTimeOffset), 9, CAST(0x07828DF98B73E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (59, 17, 2, N'Delete', 9, CAST(0x074CC2FE8E74E9390B3C00 AS DateTimeOffset), 9, CAST(0x074CC2FE8E74E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (60, 17, 2, N'Current', 9, CAST(0x07E8085FAF74E9390B3C00 AS DateTimeOffset), 9, CAST(0x07E8085FAF74E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (61, 17, 3, N'Current', 9, CAST(0x07E8085FAF74E9390B3C00 AS DateTimeOffset), 9, CAST(0x07E8085FAF74E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (62, 16, 2, N'Current', 9, CAST(0x07375046B974E9390B3C00 AS DateTimeOffset), 9, CAST(0x07375046B974E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[CohortProcedureAssignments] ([CohortProcedureAssignmentID], [CohortID], [ProcedureID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (63, 16, 4, N'Current', 9, CAST(0x07375046B974E9390B3C00 AS DateTimeOffset), 9, CAST(0x07375046B974E9390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[CohortProcedureAssignments] OFF
SET IDENTITY_INSERT [dbo].[Cohorts] ON 

INSERT [dbo].[Cohorts] ([CohortID], [MultiPARTProjectMultiPARTProjectID], [StrainStrainID], [AnimalSupplierAnimalSupplierID], [CohortLabel], [SampleSize], [CategoricalAgeID], [Details], [SexID], [MinAge], [MaxAge], [MinWeight], [MaxWeight], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (16, 1, 14, 2, N'Cohort 1', 10, NULL, NULL, 33, 0, 0, 0, 0, N'Current', 1, CAST(0x074A8B92436FCE390B3C00 AS DateTimeOffset), 1, CAST(0x076107AC436FCE390B3C00 AS DateTimeOffset))
INSERT [dbo].[Cohorts] ([CohortID], [MultiPARTProjectMultiPARTProjectID], [StrainStrainID], [AnimalSupplierAnimalSupplierID], [CohortLabel], [SampleSize], [CategoricalAgeID], [Details], [SexID], [MinAge], [MaxAge], [MinWeight], [MaxWeight], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (17, 1, 5, 3, N'c2', 10, NULL, NULL, 34, 0, 0, 0, 0, N'Current', 9, CAST(0x0782168E6F74E9390B3C00 AS DateTimeOffset), 9, CAST(0x073DC48F6F74E9390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Cohorts] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (750, N'Afghanistan', N'AF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (751, N'Åland Islands', N'AX', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (752, N'Albania', N'AL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (753, N'Algeria', N'DZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (754, N'American Samoa', N'AS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (755, N'Andorra', N'AD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (756, N'Angola', N'AO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (757, N'Anguilla', N'AI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (758, N'Antarctica', N'AQ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (759, N'Antigua and Barbuda', N'AG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (760, N'Argentina', N'AR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (761, N'Armenia', N'AM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (762, N'Aruba', N'AW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (763, N'Australia', N'AU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (764, N'Austria', N'AT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (765, N'Azerbaijan', N'AZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (766, N'Bahamas', N'BS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (767, N'Bahrain', N'BH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (768, N'Bangladesh', N'BD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (769, N'Barbados', N'BB', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (770, N'Belarus', N'BY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (771, N'Belgium', N'BE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (772, N'Belize', N'BZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (773, N'Benin', N'BJ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (774, N'Bermuda', N'BM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (775, N'Bhutan', N'BT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (776, N'Bolivia', N'BO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (777, N'Bosnia and Herzegovina', N'BA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (778, N'Botswana', N'BW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (779, N'Bouvet Island', N'BV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (780, N'Brazil', N'BR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (781, N'British Indian Ocean Territory', N'IO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (782, N'Brunei Darussalam', N'BN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (783, N'Bulgaria', N'BG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (784, N'Burkina Faso', N'BF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (785, N'Burundi', N'BI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (786, N'Cambodia', N'KH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (787, N'Cameroon', N'CM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (788, N'Canada', N'CA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (789, N'Cape Verde', N'CV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (790, N'Caribbean Netherlands ', N'BQ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (791, N'Cayman Islands', N'KY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (792, N'Central African Republic', N'CF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (793, N'Chad', N'TD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (794, N'Chile', N'CL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (795, N'China', N'CN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (796, N'Christmas Island', N'CX', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (797, N'Cocos (Keeling) Islands', N'CC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (798, N'Colombia', N'CO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (799, N'Comoros', N'KM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (800, N'Congo', N'CG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (801, N'Congo, Democratic Republic of', N'CD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (802, N'Cook Islands', N'CK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (803, N'Costa Rica', N'CR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (804, N'Côte d''Ivoire', N'CI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (805, N'Croatia', N'HR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (806, N'Cuba', N'CU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (807, N'Curaçao', N'CW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (808, N'Cyprus', N'CY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (809, N'Czech Republic', N'CZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (810, N'Denmark', N'DK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (811, N'Djibouti', N'DJ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (812, N'Dominica', N'DM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (813, N'Dominican Republic', N'DO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (814, N'Ecuador', N'EC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (815, N'Egypt', N'EG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (816, N'El Salvador', N'SV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (817, N'English Name', N'Code', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (818, N'Equatorial Guinea', N'GQ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (819, N'Eritrea', N'ER', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (820, N'Estonia', N'EE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (821, N'Ethiopia', N'ET', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (822, N'Falkland Islands', N'FK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (823, N'Faroe Islands', N'FO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (824, N'Fiji', N'FJ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (825, N'Finland', N'FI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (826, N'France', N'FR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (827, N'French Guiana', N'GF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (828, N'French Polynesia', N'PF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (829, N'French Southern Territories', N'TF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (830, N'Gabon', N'GA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (831, N'Gambia', N'GM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (832, N'Georgia', N'GE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (833, N'Germany', N'DE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (834, N'Ghana', N'GH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (835, N'Gibraltar', N'GI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (836, N'Greece', N'GR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (837, N'Greenland', N'GL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (838, N'Grenada', N'GD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (839, N'Guadeloupe', N'GP', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (840, N'Guam', N'GU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (841, N'Guatemala', N'GT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (842, N'Guernsey', N'GG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (843, N'Guinea', N'GN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (844, N'Guinea-Bissau', N'GW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (845, N'Guyana', N'GY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (846, N'Haiti', N'HT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (847, N'Heard and McDonald Islands', N'HM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (848, N'Honduras', N'HN', N'Current')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (849, N'Hong Kong', N'HK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (850, N'Hungary', N'HU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (851, N'Iceland', N'IS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (852, N'India', N'IN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (853, N'Indonesia', N'ID', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (854, N'Iran', N'IR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (855, N'Iraq', N'IQ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (856, N'Ireland', N'IE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (857, N'Isle of Man', N'IM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (858, N'Israel', N'IL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (859, N'Italy', N'IT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (860, N'Jamaica', N'JM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (861, N'Japan', N'JP', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (862, N'Jersey', N'JE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (863, N'Jordan', N'JO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (864, N'Kazakhstan', N'KZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (865, N'Kenya', N'KE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (866, N'Kiribati', N'KI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (867, N'Kuwait', N'KW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (868, N'Kyrgyzstan', N'KG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (869, N'Lao People''s Democratic Republic', N'LA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (870, N'Latvia', N'LV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (871, N'Lebanon', N'LB', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (872, N'Lesotho', N'LS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (873, N'Liberia', N'LR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (874, N'Libya', N'LY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (875, N'Liechtenstein', N'LI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (876, N'Lithuania', N'LT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (877, N'Luxembourg', N'LU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (878, N'Macau', N'MO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (879, N'Macedonia', N'MK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (880, N'Madagascar', N'MG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (881, N'Malawi', N'MW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (882, N'Malaysia', N'MY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (883, N'Maldives', N'MV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (884, N'Mali', N'ML', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (885, N'Malta', N'MT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (886, N'Marshall Islands', N'MH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (887, N'Martinique', N'MQ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (888, N'Mauritania', N'MR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (889, N'Mauritius', N'MU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (890, N'Mayotte', N'YT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (891, N'Mexico', N'MX', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (892, N'Micronesia, Federated States of', N'FM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (893, N'Moldova', N'MD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (894, N'Monaco', N'MC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (895, N'Mongolia', N'MN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (896, N'Montenegro', N'ME', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (897, N'Montserrat', N'MS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (898, N'Morocco', N'MA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (899, N'Mozambique', N'MZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (900, N'Myanmar', N'MM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (901, N'Namibia', N'NA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (902, N'Nauru', N'NR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (903, N'Nepal', N'NP', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (904, N'New Caledonia', N'NC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (905, N'New Zealand', N'NZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (906, N'Nicaragua', N'NI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (907, N'Niger', N'NE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (908, N'Nigeria', N'NG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (909, N'Niue', N'NU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (910, N'Norfolk Island', N'NF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (911, N'North Korea', N'KP', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (912, N'Northern Mariana Islands', N'MP', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (913, N'Norway', N'NO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (914, N'Oman', N'OM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (915, N'Pakistan', N'PK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (916, N'Palau', N'PW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (917, N'Palestinian Territory, Occupied', N'PS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (918, N'Panama', N'PA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (919, N'Papua New Guinea', N'PG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (920, N'Paraguay', N'PY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (921, N'Peru', N'PE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (922, N'Philippines', N'PH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (923, N'Pitcairn', N'PN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (924, N'Poland', N'PL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (925, N'Portugal', N'PT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (926, N'Puerto Rico', N'PR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (927, N'Qatar', N'QA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (928, N'Reunion', N'RE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (929, N'Romania', N'RO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (930, N'Russian Federation', N'RU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (931, N'Rwanda', N'RW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (932, N'Saint Barthélemy', N'BL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (933, N'Saint Helena', N'SH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (934, N'Saint Kitts and Nevis', N'KN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (935, N'Saint Lucia', N'LC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (936, N'Saint Vincent and the Grenadines', N'VC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (937, N'Saint-Martin (France)', N'MF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (938, N'Saint-Martin (Pays-Bas)', N'SX', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (939, N'Samoa', N'WS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (940, N'San Marino', N'SM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (941, N'Sao Tome and Principe', N'ST', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (942, N'Saudi Arabia', N'SA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (943, N'Senegal', N'SN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (944, N'Serbia', N'RS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (945, N'Seychelles', N'SC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (946, N'Sierra Leone', N'SL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (947, N'Singapore', N'SG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (948, N'Slovakia (Slovak Republic)', N'SK', N'Current')
GO
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (949, N'Slovenia', N'SI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (950, N'Solomon Islands', N'SB', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (951, N'Somalia', N'SO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (952, N'South Africa', N'ZA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (953, N'South Georgia and the South Sandwich Islands', N'GS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (954, N'South Korea', N'KR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (955, N'South Sudan', N'SS', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (956, N'Spain', N'ES', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (957, N'Sri Lanka', N'LK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (958, N'St. Pierre and Miquelon', N'PM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (959, N'Sudan', N'SD', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (960, N'Suriname', N'SR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (961, N'Svalbard and Jan Mayen Islands', N'SJ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (962, N'Swaziland', N'SZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (963, N'Sweden', N'SE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (964, N'Switzerland', N'CH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (965, N'Syria', N'SY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (966, N'Taiwan', N'TW', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (967, N'Tajikistan', N'TJ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (968, N'Tanzania', N'TZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (969, N'Thailand', N'TH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (970, N'Netherlands', N'NL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (971, N'Timor-Leste', N'TL', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (972, N'Togo', N'TG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (973, N'Tokelau', N'TK', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (974, N'Tonga', N'TO', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (975, N'Trinidad and Tobago', N'TT', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (976, N'Tunisia', N'TN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (977, N'Turkey', N'TR', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (978, N'Turkmenistan', N'TM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (979, N'Turks and Caicos Islands', N'TC', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (980, N'Tuvalu', N'TV', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (981, N'Uganda', N'UG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (982, N'Ukraine', N'UA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (983, N'United Arab Emirates', N'AE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (984, N'United Kingdom', N'GB', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (985, N'United States', N'US', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (986, N'United States Minor Outlying Islands', N'UM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (987, N'Uruguay', N'UY', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (988, N'Uzbekistan', N'UZ', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (989, N'Vanuatu', N'VU', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (990, N'Vatican', N'VA', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (991, N'Venezuela', N'VE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (992, N'Vietnam', N'VN', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (993, N'Virgin Islands (British)', N'VG', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (994, N'Virgin Islands (U.S.)', N'VI', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (995, N'Wallis and Futuna Islands', N'WF', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (996, N'Western Sahara', N'EH', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (997, N'Yemen', N'YE', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (998, N'Zambia', N'ZM', N'Current')
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [Status]) VALUES (999, N'Zimbabwe', N'ZW', N'Current')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[DiseaseModels] ON 

INSERT [dbo].[DiseaseModels] ([DiseaseModelID], [HumanConditionHumanConditionID], [HumanConditionTypeID], [AnimalConditionAnimalConditionID], [DiseaseModelDetail], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (5, 5, 8, 1, NULL, N'Current', 1, CAST(0x0788F2D90482AB390B0000 AS DateTimeOffset), 1, CAST(0x07C88EDA0482AB390B0000 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[DiseaseModels] OFF
SET IDENTITY_INSERT [dbo].[HumanConditions] ON 

INSERT [dbo].[HumanConditions] ([HumanConditionID], [HumanConditionName], [Status]) VALUES (1, N'AlzheimersDisease', N'Current')
INSERT [dbo].[HumanConditions] ([HumanConditionID], [HumanConditionName], [Status]) VALUES (2, N'HIV', N'Current')
INSERT [dbo].[HumanConditions] ([HumanConditionID], [HumanConditionName], [Status]) VALUES (3, N'Huntington''s Disease', N'Current')
INSERT [dbo].[HumanConditions] ([HumanConditionID], [HumanConditionName], [Status]) VALUES (4, N'Parkinsons Disease', N'Current')
INSERT [dbo].[HumanConditions] ([HumanConditionID], [HumanConditionName], [Status]) VALUES (5, N'Stroke', N'Current')
SET IDENTITY_INSERT [dbo].[HumanConditions] OFF
SET IDENTITY_INSERT [dbo].[Institutions] ON 

INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (5, N'University of Edinburgh', 984, N'Current', 1, CAST(0x0790CC336C869C390B0000 AS DateTimeOffset), 1, CAST(0x07802D476C869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (6, N'The University of Nottingham', 984, N'Current', 1, CAST(0x07A08C057A869C390B0000 AS DateTimeOffset), 1, CAST(0x07D001067A869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (7, N'Florey Neuroscience Institutes', 763, N'Current', 1, CAST(0x073091AC8E869C390B0000 AS DateTimeOffset), 1, CAST(0x07702DAD8E869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (8, N'University of Manchester', 984, N'Current', 1, CAST(0x07E0A70A9C869C390B0000 AS DateTimeOffset), 1, CAST(0x0700F60A9C869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (9, N'Charité', 833, N'Current', 1, CAST(0x07B0120FA3869C390B0000 AS DateTimeOffset), 1, CAST(0x0766DB3F4F719F390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (10, N'Institute for Biomedical Research', 956, N'Current', 1, CAST(0x07B0A80DB4869C390B0000 AS DateTimeOffset), 1, CAST(0x07D0F60DB4869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (11, N'University of Glasgow', 984, N'Current', 1, CAST(0x07500140BB869C390B0000 AS DateTimeOffset), 1, CAST(0x07704F40BB869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (12, N'University of Bern', 964, N'Current', 1, CAST(0x0750C942C3869C390B0000 AS DateTimeOffset), 1, CAST(0x07803E43C3869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (13, N'University Medical Centre', 970, N'Current', 1, CAST(0x07C0E071D0869C390B0000 AS DateTimeOffset), 1, CAST(0x07007D72D0869C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (14, N'Vall d''Hebron Research Institute', 956, N'Current', 1, CAST(0x0750D26A05879C390B0000 AS DateTimeOffset), 4, CAST(0x07408D6F05879C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (15, N'University of Caen Basse Normandie', 826, N'Current', 1, CAST(0x07F046B60E879C390B0000 AS DateTimeOffset), 4, CAST(0x071095B60E879C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (16, N'National Centre for the Replacement, Refinement and Reduction of Animals in Research (NC3Rs)', 984, N'Current', 1, CAST(0x0700173E1A879C390B0000 AS DateTimeOffset), 1, CAST(0x07308C3E1A879C390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (18, N'University of Nottingham - Statistics', 984, N'Current', 4, CAST(0x07551F384E80B3390B0000 AS DateTimeOffset), 4, CAST(0x07C2803A4E80B3390B0000 AS DateTimeOffset))
INSERT [dbo].[Institutions] ([InstitutionID], [InstitutionName], [CountryCountryID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (19, N'Forth Valley Royal Hospital', 984, N'Current', 1, CAST(0x07AC0C502F69C0390B0000 AS DateTimeOffset), 1, CAST(0x071C1E512F69C0390B0000 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Institutions] OFF
SET IDENTITY_INSERT [dbo].[MultiPARTProjects] ON 

INSERT [dbo].[MultiPARTProjects] ([MultiPARTProjectID], [MultiPARTProjectName], [Details], [ProjectStartDate], [ProjectCompletionDateExpected], [ProjectComletionDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy], [ExperimentalQuestion], [Aim], [Logo]) VALUES (1, N'Multicentre Preclinical Trials ', NULL, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A56100000000 AS DateTime), NULL, N'Current', 1, CAST(0x0700000000000000000000 AS DateTimeOffset), CAST(0x07120C061B56A3390B0000 AS DateTimeOffset), 1, N'Does glyceryl trinitrate (GTN), a nitrous oxide donor, 1) improve neurological score, 2) affect grip strength, 3) affect infarct volume (TTC staining)?', N'The primary aim of this beta-test is to assess whether multicentre preclinical trials of a test compound for stroke are feasible. Secondary aims are to determine whether glyceryl trinitrate (GTN) administered at a clinically relevant dose and time reduces infarct volume and/or improves neurological score assessed at 45 h after middle cerebral artery occlusion (MCAo) in the mouse.', NULL)
INSERT [dbo].[MultiPARTProjects] ([MultiPARTProjectID], [MultiPARTProjectName], [Details], [ProjectStartDate], [ProjectCompletionDateExpected], [ProjectComletionDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy], [ExperimentalQuestion], [Aim], [Logo]) VALUES (3, N'Short duration hypothermia', NULL, CAST(0x0000A45700000000 AS DateTime), CAST(0x0000A58700000000 AS DateTime), NULL, N'Current', 1, CAST(0x07E6ACE11C63B2390B0000 AS DateTimeOffset), CAST(0x07801CF71C63B2390B0000 AS DateTimeOffset), 1, N'Does transient hypothermia as induced by ice cold saline IP confer neuroprotection', N'Explore efficacy of short duration hypothermia', NULL)
INSERT [dbo].[MultiPARTProjects] ([MultiPARTProjectID], [MultiPARTProjectName], [Details], [ProjectStartDate], [ProjectCompletionDateExpected], [ProjectComletionDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy], [ExperimentalQuestion], [Aim], [Logo]) VALUES (4, N'Mindfulness in Multiple Sclerosis', NULL, CAST(0x0000A44E00000000 AS DateTime), CAST(0x0000A50600000000 AS DateTime), NULL, N'Current', 1, CAST(0x07B86A00A382B6390B0000 AS DateTimeOffset), CAST(0x0719B20EA382B6390B0000 AS DateTimeOffset), 1, N'Does a mindfulness program improve outcome in patients with MS who have anxiety or depresion?', N'To test a program of mindfulness therapy in patients with MS', NULL)
INSERT [dbo].[MultiPARTProjects] ([MultiPARTProjectID], [MultiPARTProjectName], [Details], [ProjectStartDate], [ProjectCompletionDateExpected], [ProjectComletionDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy], [ExperimentalQuestion], [Aim], [Logo]) VALUES (5, N'Murray Randomization', NULL, CAST(0x0000A46500000000 AS DateTime), CAST(0x0000A48A00000000 AS DateTime), NULL, N'Deleted', 1, CAST(0x074CDFDDDE68C0390B0000 AS DateTimeOffset), CAST(0x07FC32EEDE68C0390B0000 AS DateTimeOffset), 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MultiPARTProjects] OFF
SET IDENTITY_INSERT [dbo].[OptionFields] ON 

INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (1, N'ResearchgroupRoleinMultiPARTProjectID', N'ResearchgroupInMultiPARTProjects', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (2, N'UserRoleInResearchgroupID', N'UserInResearchgroups', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (3, N'UserRoleinProjectID', N'UserProjectAssignments', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (4, N'HumanConditionTypeID', N'', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (5, N'SexID', N'Cohorts', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (6, N'CategoricalAgeID', N'Cohorts', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (7, N'ProcedureTypeID', N'Procedures', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (8, N'ProcedurePurposeID', N'Procedures', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (9, N'AdminstrationTypeID', N'Procedures', N'Current')
INSERT [dbo].[OptionFields] ([OptionFieldID], [OptionFieldName], [TableName], [Status]) VALUES (10, N'AdministrationMethodID', N'Procedures', N'Current')
SET IDENTITY_INSERT [dbo].[OptionFields] OFF
SET IDENTITY_INSERT [dbo].[Options] ON 

INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (1, 1, N'Experiment Group', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (2, 1, N'Analysis Group', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (3, 1, N'Coordinating Group', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (4, 2, N'Group Leader', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (5, 2, N'Group Member', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (6, 3, N'Experimenter', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (7, 3, N'Analyst', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (8, 4, N'Diesease', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (9, 4, N'Pain', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (12, 3, N'PI', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (13, 7, N'Surgical', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (14, 7, N'Pharmacologic', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (15, 7, N'Transgene', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (16, 7, N'Implantation', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (17, 7, N'Developmental', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (18, 8, N'Disease Model Induction', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (19, 8, N'Comorbidity Induction', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (20, 8, N'Treatment', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (21, 8, N'Post-Operative Analgesia', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (22, 8, N'Anaesthesia', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (23, 9, N'Single', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (24, 9, N'Multiple', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (25, 9, N'Continuous', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (26, 10, N'Intravenous', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (27, 10, N'Intraperitoneal', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (28, 10, N'Subcutaneous', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (29, 8, N'Outcome Accessment', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (33, 5, N'Female', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (34, 5, N'Male', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (35, 5, N'Both', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (36, 6, N'Adult', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (37, 6, N'Juvenile', N'Current')
INSERT [dbo].[Options] ([OptionID], [OptionFieldOptionFieldID], [OptionValue], [Status]) VALUES (38, 6, N'Mature', N'Current')
SET IDENTITY_INSERT [dbo].[Options] OFF
SET IDENTITY_INSERT [dbo].[ProcedureDetailOptionFields] ON 

INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (1, 13, N'Surgery Name', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (2, 13, N'Vertilatin Method', N'lookup ProcedureDetailOptions', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (3, 13, N'Site Of Surgery', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (4, 13, N'Control Temperature', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (5, 13, N'Method Of Wound Closure', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (6, 13, N'Monitored Blood Pressure', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (7, 13, N'Monitored Blood Gases', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (8, 13, N'Fasted Before Surgery', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (9, 13, N'PhysiologicalVariables', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (10, 14, N'DrugID', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (11, 14, N'CommercialDrug', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (12, 14, N'CommercialDrugSupplier', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (13, 14, N'DrugDose', N'number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (14, 14, N'DrugUnits', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (15, 14, N'DeliveryRoute', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (16, 14, N'DrugTargetReceptor', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (17, 14, N'DrugReceptorLevel', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (18, 14, N'Mechanism', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (19, 15, N'Transgene', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (20, 15, N'Promoter', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (21, 15, N'Approved Gene', N'lookup HumanGemon', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (22, 15, N'StateGeneCopyNumber', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (23, 15, N'StateExpressionLevels', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (24, 15, N'CompareCarebralVasculature', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (25, 15, N'StateNumberOfBackCrosses', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (26, 15, N'LitterMate', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (27, 15, N'CellLineOrigin', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (28, 16, N'NumberOfImplantedCells', N'Number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (29, 16, N'VolumnOfImplantedCells', N'Number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (30, 16, N'ImplantationSite', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (31, 16, N'CellType', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (32, 16, N'ImplantType', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (33, 16, N'TypeOfCultureMedium', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (34, 16, N'FreshOrFronzen', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (35, 16, N'NumberOfPassages', N'Number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (36, 16, N'TransfectionOrSelection', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (37, 16, N'StemCellOrigin', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (38, 16, N'StemCellDetail', N'Free Text', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (39, 18, N'Human Condition', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (40, 18, N'Animal Condition', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (41, 18, N'Severity Quantification Method', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (42, 18, N'Confirmed By lase Doppler Flowmetry', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (43, 18, N'Confirmed By Behavioural Testing', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (44, 21, N'Analgesics', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (45, 21, N'AnalgesiaDose', N'Number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (46, 22, N'Anesthetics', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (47, 22, N'Marked Intrinic Neuroprotective Activity', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (48, 22, N'Alias Drug', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (49, 22, N'AnestheticsDose', N'Number', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (50, 22, N'AnestheticsPurpose', N'lookup', N'Current')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (51, 18, N' ', N'lookup', N'no')
INSERT [dbo].[ProcedureDetailOptionFields] ([ProcedureDetailOptionFieldID], [ProcedurePurposeOrTypeID], [ProcedureDetailFieldName], [ProcedureDetailType], [Status]) VALUES (52, 18, N' ', N'lookup', N'no')
SET IDENTITY_INSERT [dbo].[ProcedureDetailOptionFields] OFF
SET IDENTITY_INSERT [dbo].[ProcedureDetailOptions] ON 

INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (1, 1, N'Intrulumina filament/suture', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (2, 1, N'Permanent ischemia ', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (3, 2, N'Spontaneous', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (4, 2, N'Mechanical', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (5, 3, N'Femur Bone', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (6, 3, N'
Sciatic Nerve', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (7, 4, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (8, 4, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (9, 5, N'Staples', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (10, 5, N'Suture', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (11, 6, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (12, 6, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (13, 7, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (14, 7, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (15, 8, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (16, 8, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (17, 9, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (18, 9, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (19, 17, N'Primary', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (20, 17, N'Secondary', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (21, 17, N'Other', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (22, 15, N'Oral', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (23, 15, N'Foot Pad', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (24, 15, N'Injection', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (25, 15, N'Intracardial', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (27, 39, N'Alzheimers Disease', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (28, 39, N'Parkinsons Disease', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (31, 39, N'Stroke', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (32, 40, N'Focal ischemia', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (33, 42, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (34, 42, N'No', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (35, 43, N'Yes', N'Current')
INSERT [dbo].[ProcedureDetailOptions] ([ProcedureDetailOptionID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionName], [Status]) VALUES (36, 43, N'No', N'Current')
SET IDENTITY_INSERT [dbo].[ProcedureDetailOptions] OFF
SET IDENTITY_INSERT [dbo].[ProcedureDetails] ON 

INSERT [dbo].[ProcedureDetails] ([ProcedureDetailID], [ProcedureProcedureID], [ProcedureDetailOptionFieldID], [ProcedureDetailOptionID], [ProcedureDetailValue], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (15, 2, 1, 1, NULL, N'Current', 1, CAST(0x079C0E866B25D2390B3C00 AS DateTimeOffset), 1, CAST(0x0734B8896B25D2390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[ProcedureDetails] OFF
SET IDENTITY_INSERT [dbo].[Procedures] ON 

INSERT [dbo].[Procedures] ([ProcedureID], [MultiPARTProjectMultiPARTProjectID], [ProcedureTypeID], [ProcedurePurposeID], [ProcedureLabel], [Details], [AdministrationTypeID], [AdministrationMethodID], [StartTime], [EndTime], [DurationOfEachAssessment], [NumbersOfAdministrations], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (1, 3, NULL, 18, N'1', NULL, NULL, NULL, 0, NULL, NULL, NULL, N'Current', 1, CAST(0x07F02DEDD572C8390B3C00 AS DateTimeOffset), 1, CAST(0x079DE70ED672C8390B3C00 AS DateTimeOffset))
INSERT [dbo].[Procedures] ([ProcedureID], [MultiPARTProjectMultiPARTProjectID], [ProcedureTypeID], [ProcedurePurposeID], [ProcedureLabel], [Details], [AdministrationTypeID], [AdministrationMethodID], [StartTime], [EndTime], [DurationOfEachAssessment], [NumbersOfAdministrations], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (2, 1, 13, 18, N'disease model induction 1', NULL, NULL, NULL, 0, NULL, NULL, NULL, N'Current', 1, CAST(0x0733A40A8475C8390B3C00 AS DateTimeOffset), 1, CAST(0x07FEC20E8475C8390B3C00 AS DateTimeOffset))
INSERT [dbo].[Procedures] ([ProcedureID], [MultiPARTProjectMultiPARTProjectID], [ProcedureTypeID], [ProcedurePurposeID], [ProcedureLabel], [Details], [AdministrationTypeID], [AdministrationMethodID], [StartTime], [EndTime], [DurationOfEachAssessment], [NumbersOfAdministrations], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (3, 1, 14, 20, N't1', NULL, 23, 26, 0, NULL, NULL, NULL, N'Current', 9, CAST(0x07A718709F74E9390B3C00 AS DateTimeOffset), 9, CAST(0x070D03719F74E9390B3C00 AS DateTimeOffset))
INSERT [dbo].[Procedures] ([ProcedureID], [MultiPARTProjectMultiPARTProjectID], [ProcedureTypeID], [ProcedurePurposeID], [ProcedureLabel], [Details], [AdministrationTypeID], [AdministrationMethodID], [StartTime], [EndTime], [DurationOfEachAssessment], [NumbersOfAdministrations], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (4, 1, 14, 20, N't2', NULL, 23, 26, 0, NULL, NULL, NULL, N'Current', 9, CAST(0x07A73D45AA74E9390B3C00 AS DateTimeOffset), 9, CAST(0x07DAB245AA74E9390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Procedures] OFF
SET IDENTITY_INSERT [dbo].[ResearchgroupCohortAssignments] ON 

INSERT [dbo].[ResearchgroupCohortAssignments] ([ResearchgroupCohortAssignmentID], [CohortID], [ResearchgroupID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [NumberOfAnimals]) VALUES (34, 16, 1, N'Current', 9, CAST(0x070F88EDDA70E9390B3C00 AS DateTimeOffset), 9, CAST(0x0797C0EEDA70E9390B3C00 AS DateTimeOffset), 20)
INSERT [dbo].[ResearchgroupCohortAssignments] ([ResearchgroupCohortAssignmentID], [CohortID], [ResearchgroupID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [NumberOfAnimals]) VALUES (35, 17, 2, N'Current', 9, CAST(0x07AEFDBB8474E9390B3C00 AS DateTimeOffset), 9, CAST(0x07475DBD8474E9390B3C00 AS DateTimeOffset), 10)
INSERT [dbo].[ResearchgroupCohortAssignments] ([ResearchgroupCohortAssignmentID], [CohortID], [ResearchgroupID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [NumberOfAnimals]) VALUES (36, 17, 1, N'Current', 1, CAST(0x0792002E3175E9390B3C00 AS DateTimeOffset), 1, CAST(0x074A8C363175E9390B3C00 AS DateTimeOffset), 10)
SET IDENTITY_INSERT [dbo].[ResearchgroupCohortAssignments] OFF
SET IDENTITY_INSERT [dbo].[ResearchgroupInMultiPARTProjects] ON 

INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (4, 1, 1, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07582E4B7B7CA3390B0000 AS DateTimeOffset), 1, CAST(0x0756C24F7B7CA3390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (5, 1, 4, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07DFE80C1E7EA3390B0000 AS DateTimeOffset), 1, CAST(0x07DD7C111E7EA3390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (7, 1, 8, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07F0081E118CA3390B0000 AS DateTimeOffset), 1, CAST(0x07DD7522118CA3390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (8, 1, 9, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x0718E784FE6CAB390B0000 AS DateTimeOffset), 1, CAST(0x07E8E286FE6CAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (9, 1, 7, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07F81E6C366DAB390B0000 AS DateTimeOffset), 1, CAST(0x0728946C366DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (10, 1, 3, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07E825E13D6DAB390B0000 AS DateTimeOffset), 1, CAST(0x0728C2E13D6DAB390B0000 AS DateTimeOffset), 2)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (11, 1, 10, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07886E5D476DAB390B0000 AS DateTimeOffset), 1, CAST(0x07B8E35D476DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (12, 1, 12, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x072825B64C6DAB390B0000 AS DateTimeOffset), 1, CAST(0x07589AB64C6DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (13, 1, 11, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07C8D7D9536DAB390B0000 AS DateTimeOffset), 1, CAST(0x070874DA536DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (14, 1, 5, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x0778E698586DAB390B0000 AS DateTimeOffset), 1, CAST(0x07B88299586DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (15, 1, 2, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07A863C75E6DAB390B0000 AS DateTimeOffset), 1, CAST(0x07E8FFC75E6DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (16, 1, 6, CAST(0x0000A44E00000000 AS DateTime), N'Current', 1, CAST(0x07E82B79636DAB390B0000 AS DateTimeOffset), 1, CAST(0x0718A179636DAB390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (17, 3, 1, CAST(0x0000A45800000000 AS DateTime), N'Current', 1, CAST(0x07B54A726B6DB3390B0000 AS DateTimeOffset), 1, CAST(0x070A1A8A6B6DB3390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[ResearchgroupInMultiPARTProjects] ([ResearchgroupInMultiPARTProjectID], [MultiPARTProjectMultiPARTProjectID], [ResearchgroupResearchgroupID], [RegistrationDate], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [ResearchgroupRoleinMultiPARTProjectID]) VALUES (18, 5, 14, CAST(0x0000A46500000000 AS DateTime), N'Current', 1, CAST(0x071CDC9AE069C0390B0000 AS DateTimeOffset), 1, CAST(0x07FCFE9CE069C0390B0000 AS DateTimeOffset), 1)
SET IDENTITY_INSERT [dbo].[ResearchgroupInMultiPARTProjects] OFF
SET IDENTITY_INSERT [dbo].[Researchgroups] ON 

INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (1, N'CAMARADES', 5, N'Current', 1, CAST(0x07521D7113779E390B0000 AS DateTimeOffset), 1, CAST(0x07B25CCC13779E390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (2, N'Florey', 7, N'Current', 1, CAST(0x07022777597B9E390B0000 AS DateTimeOffset), 1, CAST(0x07D2937B597B9E390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (3, N'NC3Rs', 16, N'Current', 1, CAST(0x07421AC20D809E390B0000 AS DateTimeOffset), 1, CAST(0x07B29CC50D809E390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (4, N'Charité', 9, N'Current', 1, CAST(0x07C6BCC0CF4C9F390B0000 AS DateTimeOffset), 1, CAST(0x073621C9CF4C9F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (5, N'Stroke, University of Nottingham', 6, N'Current', 1, CAST(0x07E6770628659F390B0000 AS DateTimeOffset), 1, CAST(0x07C6ED0F28659F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (6, N'UMC Utrecht', 13, N'Current', 1, CAST(0x0766753733659F390B0000 AS DateTimeOffset), 1, CAST(0x07A6113833659F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (7, N'Institute of Neuroscience and Psychology', 11, N'Current', 1, CAST(0x076633234D659F390B0000 AS DateTimeOffset), 1, CAST(0x07B6D8284D659F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (8, N'Division of Animal Welfare', 12, N'Current', 1, CAST(0x0756F72457739F390B0000 AS DateTimeOffset), 1, CAST(0x07163D2957739F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (9, N'Department of Brain Ischemia and Neurodegeneration', 10, N'Current', 1, CAST(0x07B68B6F4E749F390B0000 AS DateTimeOffset), 1, CAST(0x075683734E749F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (10, N'Neuroscience Research Institute', 8, N'Current', 1, CAST(0x0796BF4993789F390B0000 AS DateTimeOffset), 1, CAST(0x07D65B4A93789F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (11, N'VHRI', 14, N'Current', 1, CAST(0x0756D8E5FE799F390B0000 AS DateTimeOffset), 1, CAST(0x07E64DE6FE799F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (12, N'Inserm UMR-S U919 - SP2U', 15, N'Current', 1, CAST(0x0716157C927A9F390B0000 AS DateTimeOffset), 4, CAST(0x076A8A7C927A9F390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (13, N'University of Nottingham - Statistics', 18, N'Current', 4, CAST(0x07A815417281B3390B0000 AS DateTimeOffset), 4, CAST(0x07FDD9457281B3390B0000 AS DateTimeOffset))
INSERT [dbo].[Researchgroups] ([ResearchgroupID], [ResearchgroupName], [InstitutionInstitutionID], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (14, N'Anne Rowling Regenerative Neurology Clinic', 19, N'Current', 1, CAST(0x073C3C51D769C0390B0000 AS DateTimeOffset), 1, CAST(0x07CC9B52D769C0390B0000 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Researchgroups] OFF
SET IDENTITY_INSERT [dbo].[Species] ON 

INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (1, N'Mouse', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (2, N'Baboon', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (3, N'Caenorhabditis', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (4, N'Cat', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (5, N'Chicken', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (6, N'Cow', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (7, N'Dog', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (8, N'Drosophila', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (9, N'Ewe', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (10, N'Frog', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (11, N'Gerbil', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (12, N'Guinea Pig', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (13, N'Hamster', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (14, N'Marmoset', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (15, N'Monkey', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (16, N'Mouse', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (17, N'Mpouse', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (18, N'Pig', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (19, N'Rabbit', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (20, N'Rat', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (21, N'Rodent', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (22, N'Sheep', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (23, N'Zebrafish', N'Current')
INSERT [dbo].[Species] ([SpecieID], [SpecieName], [Status]) VALUES (24, N'Sheep', N'Current')
SET IDENTITY_INSERT [dbo].[Species] OFF
SET IDENTITY_INSERT [dbo].[Strains] ON 

INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (4, N'Lewis', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (5, N'SHR', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (7, N'SHRSP', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (8, N'Sprague Dawley', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (9, N'Wistar', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (10, N'Wistar-Kyoto', 20, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (11, N'BALB/c', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (12, N'C57BL', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (13, N'C57BL/6', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (14, N'CD-1', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (15, N'DBA', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (16, N'ob/ob', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (17, N'SCID', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (18, N'Swiss', 1, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (19, N'Merino', 24, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (20, N'blackhead', 24, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (21, N'Rhesus ', 15, N'Current')
INSERT [dbo].[Strains] ([StrainID], [StrainName], [SpecieID], [Status]) VALUES (22, N'macaca', 15, N'Current')
SET IDENTITY_INSERT [dbo].[Strains] OFF
SET IDENTITY_INSERT [dbo].[UserInResearchgroups] ON 

INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (2, 1, 1, 4, CAST(0x0000A43900000000 AS DateTime), CAST(0x0000A44500000000 AS DateTime), N'Deleted', 1, CAST(0x0745EFA21D8C9F390B0000 AS DateTimeOffset), 1, CAST(0x070880071E8C9F390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (3, 1, 1, 4, CAST(0x0000A2A600000000 AS DateTime), CAST(0x0000A41300000000 AS DateTime), N'Current', 1, CAST(0x07DDF84C2C8C9F390B0000 AS DateTimeOffset), 1, CAST(0x07106E4D2C8C9F390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (4, 1, 1, 4, CAST(0x0000A43200000000 AS DateTime), NULL, N'Current', 1, CAST(0x070C97BA99AB9F390B0000 AS DateTimeOffset), 1, CAST(0x073F0CBB99AB9F390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (5, 1, 1, 5, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A44600000000 AS DateTime), N'Deleted', 1, CAST(0x0748A4C39584A0390B0000 AS DateTimeOffset), 1, CAST(0x078AD4C89584A0390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (6, 4, 8, 4, CAST(0x0000A43200000000 AS DateTime), CAST(0x0000A61800000000 AS DateTime), N'Current', 1, CAST(0x0759C43D2E8CA3390B0000 AS DateTimeOffset), 1, CAST(0x07F2233F2E8CA3390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (7, 8, 14, 4, CAST(0x0000A44E00000000 AS DateTime), NULL, N'Current', 1, CAST(0x074C751D5F6DC0390B0000 AS DateTimeOffset), 1, CAST(0x07DC45215F6DC0390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (8, 1, 14, 5, CAST(0x0000A46500000000 AS DateTime), NULL, N'Current', 1, CAST(0x07CC4CB11D6EC0390B0000 AS DateTimeOffset), 1, CAST(0x074C85B21D6EC0390B0000 AS DateTimeOffset))
INSERT [dbo].[UserInResearchgroups] ([UserInResearchgroupID], [UserProfileUserId], [ResearchgroupResearchgroupID], [UserRoleinResearchgroupID], [StartTime], [EndTime], [Status], [CreatedBy], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn]) VALUES (9, 9, 1, 4, CAST(0x0000A48B00000000 AS DateTime), NULL, N'Current', 9, CAST(0x079950AE9D70E9390B3C00 AS DateTimeOffset), 9, CAST(0x07FF3AAF9D70E9390B3C00 AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[UserInResearchgroups] OFF
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (1, N'admin', N'admin', N'admin', N'jing.liao@ed.ac.uk', N'camarades', N'Current', CAST(0x0700000000009C390B0000 AS DateTimeOffset), CAST(0x0700000000009C390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (4, N'MultiPART', N'multipart', N'multipart', N'Multipart.team@ed.ac.uk', NULL, N'Current', CAST(0x07708B1CD7899C390B0000 AS DateTimeOffset), CAST(0x07708B1CD7899C390B0000 AS DateTimeOffset), 4)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (5, N'testuser', N'testuser', N'testuser', N'multipart.team@ed.ac.uk', NULL, N'Current', CAST(0x0750E267FA899C390B0000 AS DateTimeOffset), CAST(0x0750E267FA899C390B0000 AS DateTimeOffset), 5)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (7, N'tester2', N'asdf', NULL, N'jing.liao@ed.ac.uk', NULL, N'Current', CAST(0x0716F7286A6F9F390B0000 AS DateTimeOffset), CAST(0x0716F7286A6F9F390B0000 AS DateTimeOffset), 7)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (8, N'KatyMurray', N'Katy', N'Murray', N'katy.murray@nhs.net', NULL, N'Current', CAST(0x077CD539E35FC0390B0000 AS DateTimeOffset), CAST(0x077CD539E35FC0390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (9, N'csena', N'Christopher', N'Sena', N'chrissena87@gmail.com', NULL, N'Current', CAST(0x07230FD3F85BE9390B3C00 AS DateTimeOffset), CAST(0x077CD539E35FC0390B0000 AS DateTimeOffset), NULL)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (10, N'test1', N'Testy', N'McTesterson', N'test1@test.com', NULL, N'Current', CAST(0x0706030E375FE9390B3C00 AS DateTimeOffset), CAST(0x078019FBE25FC0390B0000 AS DateTimeOffset), NULL)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (11, N'test34', N'Test', N'McTesterson', N'testy23@test.com', NULL, N'Current', CAST(0x07978642E77BE9390B3C00 AS DateTimeOffset), CAST(0x07978642E77BE9390B3C00 AS DateTimeOffset), NULL)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [ForeName], [SurName], [Email], [Details], [Status], [CreatedOn], [LastUpdatedOn], [LastUpdatedBy]) VALUES (12, N'Test431', N'Test', N'McTesterson', N'testy23@test.com', NULL, N'Current', CAST(0x072E9558297EE9390B3C00 AS DateTimeOffset), CAST(0x072E9558297EE9390B3C00 AS DateTimeOffset), NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
SET IDENTITY_INSERT [dbo].[UserProjectAssignments] ON 

INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (3, 4, 4, 6, N'Current', CAST(0x0702F9742480A6390B0000 AS DateTimeOffset), 1, CAST(0x0782F57F2480A6390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (4, 7, 6, 12, N'Current', CAST(0x07A220F83484A6390B0000 AS DateTimeOffset), 1, CAST(0x0782B4FC3484A6390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (5, 4, 4, 6, N'Deleted', CAST(0x07476AC04B80AE390B0000 AS DateTimeOffset), 1, CAST(0x0727FEC44B80AE390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (6, 7, 6, 6, N'Deleted', CAST(0x07DC1A1CDC63B2390B0000 AS DateTimeOffset), 1, CAST(0x0789DC20DC63B2390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (7, 18, 7, 6, N'Current', CAST(0x078C52FE656DC0390B0000 AS DateTimeOffset), 1, CAST(0x070C8BFF656DC0390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (8, 18, 8, 6, N'Current', CAST(0x078C3908246EC0390B0000 AS DateTimeOffset), 1, CAST(0x070C7209246EC0390B0000 AS DateTimeOffset), 1)
INSERT [dbo].[UserProjectAssignments] ([UserProjectAssignmentID], [ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID], [UserInResearchgroupUserInResearchgroupID], [UserRoleinProjectID], [Status], [CreatedOn], [LastUpdatedBy], [LastUpdatedOn], [CreatedBy]) VALUES (9, 4, 9, 12, N'Current', CAST(0x07380282A570E9390B3C00 AS DateTimeOffset), 9, CAST(0x07C03A83A570E9390B3C00 AS DateTimeOffset), 9)
SET IDENTITY_INSERT [dbo].[UserProjectAssignments] OFF
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A44100ED6C83 AS DateTime), NULL, 1, CAST(0x0000A46500D833EC AS DateTime), 0, N'ANwFMWSrO658Oz7CwyHYelE+3A9nUEL7PEexgiCkSpsYlYoKXUpP6zEzeE8tXLmS/A==', CAST(0x0000A44300ACFDB9 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (4, CAST(0x0000A441010F015C AS DateTime), NULL, 1, NULL, 0, N'ANFpFmiLdFvH/LHRk6ATABpm3hIt5s/MfOFbnMy5lBzzxm5UIiOvlfzn9iI5ushwuw==', CAST(0x0000A441010F015C AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (5, CAST(0x0000A441010F46B2 AS DateTime), NULL, 1, NULL, 0, N'ACGN+4Chdv+wuGWT43+Qi8eZgbd3YyG5pZ5/g+w+XbZXcPQkoPJAdV2m0Oi4GIcTLA==', CAST(0x0000A441010F46B2 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (7, CAST(0x0000A44400DB0D7D AS DateTime), NULL, 1, NULL, 0, N'ADFpfGVLZQk5SGRTJHAb1s3J+yXtcJHUwvV/XC8e2+A1u8z0DKj9SJ2Q77gfABjO5A==', CAST(0x0000A44400DB0D7D AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (8, CAST(0x0000A46500BC85E2 AS DateTime), NULL, 1, NULL, 0, N'AJRb9ROjhQMYeLxv4PIpaB0J1jjUrVSmz5gDG0cruRUo9PtoZ0501w3UgjfMwOqtnw==', CAST(0x0000A46500BC85E2 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (9, CAST(0x0000A48E00B4D30B AS DateTime), NULL, 1, NULL, 0, N'AA1Oi3dNWrTfSofvJfWJvwFQ1jbs6qIjepqBydW2a2yezQ3xn3sYA46UugyXvBJAcA==', CAST(0x0000A48E00B4D30B AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (10, CAST(0x0000A48E00BB3356 AS DateTime), NULL, 1, NULL, 0, N'ACJv5iZs0FZ2cYY/wUXwGRbtGhuMzuAZmeeMn6kk8DZ3D89v97cuVAOlP2sEpelgGQ==', CAST(0x0000A48E00BB3356 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (11, CAST(0x0000A48E00F39AA3 AS DateTime), NULL, 1, NULL, 0, N'AHSZ+ZErxe8X/fn18Bv4RYcQOSiogP2RsG0T0g0lOaQ1/DQP8W/WkZ7EqlIQa54YqQ==', CAST(0x0000A48E00F39AA3 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (12, CAST(0x0000A48E00F80B31 AS DateTime), NULL, 1, NULL, 0, N'AIOYMTcECX6/osJeQF462qvtU4amcAdYh2QTrGsWjsxlJIk1cGhef56y4jMDG3gk+w==', CAST(0x0000A48E00F80B31 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 

INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'Superuser')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (4, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (5, 3)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (8, 3)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (9, 1)
/****** Object:  Index [IX_AnimalHusbandries]    Script Date: 05/05/2015 10:50:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AnimalHusbandries] ON [dbo].[AnimalHusbandries]
(
	[ResearchgroupID] ASC,
	[StrainID] ASC,
	[MultiPARTProjectMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultiPARTProjectMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_MultiPARTProjectMultiPARTProjectID] ON [dbo].[AnimalHusbandries]
(
	[MultiPARTProjectMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupID] ON [dbo].[AnimalHusbandries]
(
	[ResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StrainID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_StrainID] ON [dbo].[AnimalHusbandries]
(
	[StrainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CohortID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CohortID] ON [dbo].[Animals]
(
	[CohortID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupID] ON [dbo].[Animals]
(
	[ResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SexID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_SexID] ON [dbo].[Animals]
(
	[SexID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstitutionInstitutionID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_InstitutionInstitutionID] ON [dbo].[Careerhistories]
(
	[InstitutionInstitutionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserProfileUserId]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_UserProfileUserId] ON [dbo].[Careerhistories]
(
	[UserProfileUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CohortID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CohortID] ON [dbo].[CohortProcedureAssignments]
(
	[CohortID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureID] ON [dbo].[CohortProcedureAssignments]
(
	[ProcedureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AnimalSupplierAnimalSupplierID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_AnimalSupplierAnimalSupplierID] ON [dbo].[Cohorts]
(
	[AnimalSupplierAnimalSupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoricalAgeID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CategoricalAgeID] ON [dbo].[Cohorts]
(
	[CategoricalAgeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultiPARTProjectMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_MultiPARTProjectMultiPARTProjectID] ON [dbo].[Cohorts]
(
	[MultiPARTProjectMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SexID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_SexID] ON [dbo].[Cohorts]
(
	[SexID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StrainStrainID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_StrainStrainID] ON [dbo].[Cohorts]
(
	[StrainStrainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommercialDrugID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CommercialDrugID] ON [dbo].[CommercialDrugComponents]
(
	[CommercialDrugID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DrugID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_DrugID] ON [dbo].[CommercialDrugComponents]
(
	[DrugID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AnimalConditionAnimalConditionID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_AnimalConditionAnimalConditionID] ON [dbo].[DiseaseModels]
(
	[AnimalConditionAnimalConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HumanConditionHumanConditionID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_HumanConditionHumanConditionID] ON [dbo].[DiseaseModels]
(
	[HumanConditionHumanConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HumanConditionType]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_HumanConditionType] ON [dbo].[DiseaseModels]
(
	[HumanConditionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CountryCountryID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CountryCountryID] ON [dbo].[Institutions]
(
	[CountryCountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OptionFieldID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_OptionFieldID] ON [dbo].[Options]
(
	[OptionFieldOptionFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureDetailOptionFieldID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureDetailOptionFieldID] ON [dbo].[ProcedureDetailOptions]
(
	[ProcedureDetailOptionFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureDetailOptionFieldID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureDetailOptionFieldID] ON [dbo].[ProcedureDetails]
(
	[ProcedureDetailOptionFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureDetailOptionID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureDetailOptionID] ON [dbo].[ProcedureDetails]
(
	[ProcedureDetailOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureProcedureID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureProcedureID] ON [dbo].[ProcedureDetails]
(
	[ProcedureProcedureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdministrationMethodID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_AdministrationMethodID] ON [dbo].[Procedures]
(
	[AdministrationMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdministrationTypeID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_AdministrationTypeID] ON [dbo].[Procedures]
(
	[AdministrationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultiPARTProjectMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_MultiPARTProjectMultiPARTProjectID] ON [dbo].[Procedures]
(
	[MultiPARTProjectMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedurePurposeID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedurePurposeID] ON [dbo].[Procedures]
(
	[ProcedurePurposeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProcedureTypeID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProcedureTypeID] ON [dbo].[Procedures]
(
	[ProcedureTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CohortID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_CohortID] ON [dbo].[ResearchgroupCohortAssignments]
(
	[CohortID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupID] ON [dbo].[ResearchgroupCohortAssignments]
(
	[ResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MultiPARTProjectMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_MultiPARTProjectMultiPARTProjectID] ON [dbo].[ResearchgroupInMultiPARTProjects]
(
	[MultiPARTProjectMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupResearchgroupID] ON [dbo].[ResearchgroupInMultiPARTProjects]
(
	[ResearchgroupResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupRoleinMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupRoleinMultiPARTProjectID] ON [dbo].[ResearchgroupInMultiPARTProjects]
(
	[ResearchgroupRoleinMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InstitutionInstitutionID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_InstitutionInstitutionID] ON [dbo].[Researchgroups]
(
	[InstitutionInstitutionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Strains]    Script Date: 05/05/2015 10:50:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Strains] ON [dbo].[Strains]
(
	[StrainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupResearchgroupID] ON [dbo].[UserInResearchgroups]
(
	[ResearchgroupResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserProfileUserId]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_UserProfileUserId] ON [dbo].[UserInResearchgroups]
(
	[UserProfileUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoleinResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoleinResearchgroupID] ON [dbo].[UserInResearchgroups]
(
	[UserRoleinResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID] ON [dbo].[UserProjectAssignments]
(
	[ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserInResearchgroupUserInResearchgroupID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_UserInResearchgroupUserInResearchgroupID] ON [dbo].[UserProjectAssignments]
(
	[UserInResearchgroupUserInResearchgroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoleinProjectID]    Script Date: 05/05/2015 10:50:35 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoleinProjectID] ON [dbo].[UserProjectAssignments]
(
	[UserRoleinProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__webpages__8A2B61601E0C7710]    Script Date: 05/05/2015 10:50:35 ******/
ALTER TABLE [dbo].[webpages_Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
