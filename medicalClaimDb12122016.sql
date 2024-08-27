USE [master]
GO
/****** Object:  Database [MedicalClaim]    Script Date: 12/13/2016 12:38:45 PM ******/
CREATE DATABASE [MedicalClaim]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MedicalClaim', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MedicalClaim.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MedicalClaim_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MedicalClaim_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MedicalClaim] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MedicalClaim].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MedicalClaim] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MedicalClaim] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MedicalClaim] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MedicalClaim] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MedicalClaim] SET ARITHABORT OFF 
GO
ALTER DATABASE [MedicalClaim] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MedicalClaim] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MedicalClaim] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MedicalClaim] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MedicalClaim] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MedicalClaim] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MedicalClaim] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MedicalClaim] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MedicalClaim] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MedicalClaim] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MedicalClaim] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MedicalClaim] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MedicalClaim] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MedicalClaim] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MedicalClaim] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MedicalClaim] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MedicalClaim] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MedicalClaim] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MedicalClaim] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MedicalClaim] SET  MULTI_USER 
GO
ALTER DATABASE [MedicalClaim] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MedicalClaim] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MedicalClaim] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MedicalClaim] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MedicalClaim]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 12/13/2016 12:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Pasword] [varchar](50) NULL,
	[isDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MedicalClaim]    Script Date: 12/13/2016 12:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MedicalClaim](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[ClaimType] [varchar](50) NULL,
	[ClaimAmount] [varchar](50) NULL,
	[Hospital] [varchar](100) NULL,
	[ClaimDate] [datetime] NULL,
	[Details] [varchar](200) NULL,
	[Status] [varchar](50) NULL,
	[isDelete] [bit] NOT NULL,
	[actionById] [int] NULL,
	[actionDate] [datetime] NULL,
	[lastUpdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MedicalQuota]    Script Date: 12/13/2016 12:38:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalQuota](
	[MedicalQuotaId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[QuotaAmount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicalQuotaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeId], [Name], [Username], [Pasword], [isDelete]) VALUES (1, N'Munam Yousuf', N'munam', N'123', 0)
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Username], [Pasword], [isDelete]) VALUES (2, N'Tanveer Ahmed', N'tanveer', N'123', 1)
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Username], [Pasword], [isDelete]) VALUES (3, N'Ehtesham Arif', N'ehtesham', N'123', 0)
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Username], [Pasword], [isDelete]) VALUES (4, N'Khalil Ahmed', N'khalil', N'123', 0)
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Username], [Pasword], [isDelete]) VALUES (5, N'Salman Bahadur', N'salman', N'123', 0)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[MedicalClaim] ON 

INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (3, 1, N'Operation', N'10000', N'Liaquat National Hospital', CAST(0x0000A6BF01082BBC AS DateTime), N'Liaquat National Hospital', N'Approved', 0, 1, CAST(0x0000A6C200C08796 AS DateTime), CAST(0x0000A6C20137A974 AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (137, 1, N'dfg', N'3', N'gd', CAST(0x0000A6CF0134E5C0 AS DateTime), N'dfg', N'Pending', 1, 1, CAST(0x0000A6D100DD2AE9 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (138, 1, N'a', N'3', N'af', CAST(0x0000A6CF01418DF7 AS DateTime), N'fa', N'Pending', 1, 1, CAST(0x0000A6D100C1FA84 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (139, 1, N'b', N'33', N'bb', CAST(0x0000A6CF0141C6A2 AS DateTime), N'cc', N'Pending', 1, 1, CAST(0x0000A6D100C2080B AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (140, 1, N'sd', N'sd', N'ds', CAST(0x0000A6D100DD3B31 AS DateTime), N'sd', N'Pending', 1, 1, CAST(0x0000A6D100DD46F5 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (141, 1, N's', N'ds', N'sd', CAST(0x0000A6D100DD66DD AS DateTime), N'sd', N'Pending', 1, 1, CAST(0x0000A6D100DE5C95 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (142, 1, N'ds', N's', N'sd', CAST(0x0000A6D100E37B40 AS DateTime), N'sd', N'Pending', 1, 1, CAST(0x0000A6D100EB9EA5 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (143, 1, N'sd', N'sd', N'sd', CAST(0x0000A6D100E38E2C AS DateTime), N'sd', N'Pending', 1, 1, CAST(0x0000A6D100EBE9A1 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (144, 1, N'a', N'a', N'a', CAST(0x0000A6D100E396AA AS DateTime), N'a', N'Pending', 1, 1, CAST(0x0000A6D100ED8AE4 AS DateTime), CAST(0x0000A6D100E3C16F AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (145, 1, N'b', N'b', N'b', CAST(0x0000A6D100E39F20 AS DateTime), N'b', N'Pending', 1, 1, CAST(0x0000A6D100ED30C9 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (146, 1, N'c', N'c', N'c', CAST(0x0000A6D100E3A845 AS DateTime), N'c', N'Pending', 1, 1, CAST(0x0000A6D100EB4794 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (147, 1, N'd', N'd', N'd', CAST(0x0000A6D100E3B1A6 AS DateTime), N'd', N'Pending', 1, 1, CAST(0x0000A6D100E6D5A6 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (148, 1, N'a', N'a', N'a', CAST(0x0000A6D100EE6060 AS DateTime), N'a', N'Pending', 1, 1, CAST(0x0000A6D100F43121 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (149, 1, N'b', N'b', N'b', CAST(0x0000A6D100EE673C AS DateTime), N'b', N'Pending', 1, 1, CAST(0x0000A6D100F30F17 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (150, 1, N'c', N'c', N'c', CAST(0x0000A6D100EE6DAD AS DateTime), N'c', N'Pending', 1, 1, CAST(0x0000A6D100F20ABE AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (151, 1, N'd', N'd', N'd', CAST(0x0000A6D100EE75C6 AS DateTime), N'd', N'Pending', 1, 1, CAST(0x0000A6D100EF1F3C AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (152, 1, N'e', N'e', N'e', CAST(0x0000A6D100EE7CF1 AS DateTime), N'e', N'Pending', 1, 1, CAST(0x0000A6D100EEF9AE AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (153, 1, N'f', N'f', N'f', CAST(0x0000A6D100EE842D AS DateTime), N'f', N'Pending', 1, 1, CAST(0x0000A6D100EE8FC6 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (154, 1, N'a', N'a', N'a', CAST(0x0000A6D100F49F5B AS DateTime), N'a', N'Pending', 1, 1, CAST(0x0000A6D101336094 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (155, 1, N'b', N'b', N'b', CAST(0x0000A6D100F4A81E AS DateTime), N'b', N'Pending', 1, 1, CAST(0x0000A6D101335D09 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (156, 1, N'c', N'c', N'c', CAST(0x0000A6D100F4AEE8 AS DateTime), N'c', N'Pending', 1, 1, CAST(0x0000A6D101335A67 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (157, 1, N'd', N'd', N'd', CAST(0x0000A6D100F4B6BB AS DateTime), N'd', N'Pending', 1, 1, CAST(0x0000A6D1013357CD AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (158, 1, N'e', N'e', N'e', CAST(0x0000A6D100F4BD83 AS DateTime), N'e', N'Pending', 1, 1, CAST(0x0000A6D100F7321E AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (159, 1, N'f', N'f', N'f', CAST(0x0000A6D100F4CB00 AS DateTime), N'f', N'Pending', 1, 1, CAST(0x0000A6D100F65675 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (160, 1, N'g', N'g', N'g', CAST(0x0000A6D100F4D260 AS DateTime), N'g', N'Pending', 1, 1, CAST(0x0000A6D100F51BD3 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (161, 1, N'type', N'amount', N'hospital', CAST(0x0000A6D1010C54CF AS DateTime), N'details', N'Pending', 1, 1, CAST(0x0000A6D1013355C9 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (162, 1, N'sd', N'sd', N'sd', CAST(0x0000A6D1011C79A3 AS DateTime), N'sd', N'Pending', 1, 1, CAST(0x0000A6D101335357 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (163, 1, N'a type', N'a amount', N'a hospital', CAST(0x0000A6D1011DFC40 AS DateTime), N'a details', N'Pending', 1, 1, CAST(0x0000A6D1013350CB AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (164, 1, N'bb ', N'bb', N'bb', CAST(0x0000A6D1011F183A AS DateTime), N'bb', N'Pending', 1, 1, CAST(0x0000A6D101334ED0 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (165, 1, N'ca', N'cb', N'cc', CAST(0x0000A6D10121C851 AS DateTime), N'cd', N'Pending', 1, 1, CAST(0x0000A6D101334C75 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (166, 1, N'da', N'db', N'dc', CAST(0x0000A6D10132D88C AS DateTime), N'dd', N'Pending', 1, 1, CAST(0x0000A6D101334A6C AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (167, 1, N'aaa', N'aaa', N'aaa', CAST(0x0000A6D1013369DD AS DateTime), N'aaa', N'Pending', 0, NULL, NULL, CAST(0x0000A6DB00CE376E AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (168, 1, N'b', N'b', N'b', CAST(0x0000A6D101338102 AS DateTime), N'b', N'Pending', 0, NULL, NULL, NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (169, 1, N'c', N'c', N'c', CAST(0x0000A6D10133CB2D AS DateTime), N'c', N'Pending', 0, NULL, NULL, NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (170, 1, N'd', N'd', N'd', CAST(0x0000A6D10133DB62 AS DateTime), N'd', N'Pending', 1, 1, CAST(0x0000A6D101526465 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (171, 1, N'e', N'e', N'e', CAST(0x0000A6D1013423D8 AS DateTime), N'e', N'Pending', 1, 1, CAST(0x0000A6D10152604F AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (172, 1, N'f', N'f', N'f', CAST(0x0000A6D101351049 AS DateTime), N'f', N'Pending', 1, 1, CAST(0x0000A6D101525E81 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (173, 1, N'g', N'g', N'g', CAST(0x0000A6D10136D172 AS DateTime), N'g', N'Pending', 1, 1, CAST(0x0000A6D101525CB6 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (174, 1, N'h', N'h', N'h', CAST(0x0000A6D1013D2B27 AS DateTime), N'h', N'Pending', 1, 1, CAST(0x0000A6D1013F20AD AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (175, 1, N'i', N'i', N'i', CAST(0x0000A6D1013F1966 AS DateTime), N'i', N'Pending', 1, 1, CAST(0x0000A6D101525ABB AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (176, 1, N'j', N'j', N'j', CAST(0x0000A6D1013FC11E AS DateTime), N'j', N'Pending', 1, 1, CAST(0x0000A6D10152584A AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (177, 1, N'k', N'k', N'k', CAST(0x0000A6D101437250 AS DateTime), N'k', N'Pending', 1, 1, CAST(0x0000A6D1015255BF AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (178, 1, N'l', N'l', N'l', CAST(0x0000A6D101438CA9 AS DateTime), N'l', N'Pending', 1, 1, CAST(0x0000A6D101525398 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (179, 1, N'm', N'm', N'm', CAST(0x0000A6D10144BBB5 AS DateTime), N'm', N'Pending', 1, 1, CAST(0x0000A6D101525124 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (180, 1, N'n', N'n', N'n', CAST(0x0000A6D101453CFD AS DateTime), N'n', N'Pending', 1, 1, CAST(0x0000A6D101524F21 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (181, 1, N'o', N'o', N'o', CAST(0x0000A6D10145690D AS DateTime), N'o', N'Pending', 1, 1, CAST(0x0000A6D101524D25 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (182, 1, N'p', N'p', N'p', CAST(0x0000A6D10145BD46 AS DateTime), N'p', N'Pending', 1, 1, CAST(0x0000A6D101524B79 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (183, 1, N'q', N'q', N'q', CAST(0x0000A6D1014CACCC AS DateTime), N'q', N'Pending', 1, 1, CAST(0x0000A6D101524875 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (184, 1, N'r', N'r', N'r', CAST(0x0000A6D1014D06D8 AS DateTime), N'r', N'Pending', 1, 1, CAST(0x0000A6D101524686 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (185, 1, N'aaaaaa', N'aaaa', N'aaaaa', CAST(0x0000A6D1014D3851 AS DateTime), N'aaaaa', N'Pending', 1, 1, CAST(0x0000A6D1015244C3 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (186, 1, N'aa', N'aa', N'aa', CAST(0x0000A6D1014D4706 AS DateTime), N'aa', N'Pending', 1, 1, CAST(0x0000A6D1015242B9 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (187, 1, N'aaaaaaaaaaaa', N'aaaaaaaaaaa', N'aaaaaaaaaaa', CAST(0x0000A6D1014D6EDE AS DateTime), N'aaaaaaaaaaaa', N'Pending', 1, 1, CAST(0x0000A6D1015240CE AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (188, 1, N'bbbbb', N'bbbb', N'bbbb', CAST(0x0000A6D1014D7BAB AS DateTime), N'bbbb', N'Pending', 1, 1, CAST(0x0000A6D101523F08 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (189, 1, N'bb', N'bb', N'bb', CAST(0x0000A6D1014DBD85 AS DateTime), N'bb', N'Pending', 1, 1, CAST(0x0000A6D101523D83 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (190, 1, N'cc', N'cc', N'cc', CAST(0x0000A6D1014DC52A AS DateTime), N'cc', N'Pending', 1, 1, CAST(0x0000A6D10152387C AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (191, 1, N'ff', N'ff', N'ff', CAST(0x0000A6D1014DDFEE AS DateTime), N'ff', N'Pending', 1, 1, CAST(0x0000A6D101523651 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (192, 1, N'1', N'1', N'1', CAST(0x0000A6D1014E29C8 AS DateTime), N'1', N'Pending', 1, 1, CAST(0x0000A6D101523490 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (193, 1, N'2', N'2', N'2', CAST(0x0000A6D1014E4EA9 AS DateTime), N'2', N'Pending', 1, 1, CAST(0x0000A6D1015232DD AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (194, 1, N'3', N'3', N'3', CAST(0x0000A6D1014F035B AS DateTime), N'3', N'Pending', 1, 1, CAST(0x0000A6D10152310B AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (195, 1, N'4', N'4', N'4', CAST(0x0000A6D1014F0A5C AS DateTime), N'4', N'Pending', 1, 1, CAST(0x0000A6D101522F57 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (196, 1, N'5', N'5', N'5', CAST(0x0000A6D1014F11F5 AS DateTime), N'5', N'Pending', 1, 1, CAST(0x0000A6D101522CB4 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (197, 1, N'6', N'6', N'6', CAST(0x0000A6D1014F1DE1 AS DateTime), N'6', N'Pending', 1, 1, CAST(0x0000A6D101522A80 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (198, 1, N'abdul', N'abdul', N'abdul', CAST(0x0000A6D1014F477A AS DateTime), N'zzzzzz', N'Pending', 1, 1, CAST(0x0000A6D101522865 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (199, 1, N'9', N'9', N'9', CAST(0x0000A6D1014FC2E7 AS DateTime), N'9', N'Pending', 1, 1, CAST(0x0000A6D10152267F AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (200, 1, N'10', N'10', N'10', CAST(0x0000A6D101507CA3 AS DateTime), N'10', N'Pending', 1, 1, CAST(0x0000A6D1015220B2 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (201, 1, N'11', N'11', N'11', CAST(0x0000A6D10150F96D AS DateTime), N'11', N'Pending', 1, 1, CAST(0x0000A6D101521E5E AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (202, 1, N'12', N'12', N'12', CAST(0x0000A6D101511C8A AS DateTime), N'12', N'Pending', 1, 1, CAST(0x0000A6D10151AEA7 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (203, 1, N'13', N'13', N'13', CAST(0x0000A6D101514284 AS DateTime), N'13', N'Pending', 1, 1, CAST(0x0000A6D10151AB3A AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (204, 1, N'111', N'111', N'111', CAST(0x0000A6D10151B656 AS DateTime), N'111', N'Pending', 1, 1, CAST(0x0000A6D101521B09 AS DateTime), CAST(0x0000A6D10151D6C4 AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (205, 1, N'd', N'd', N'd', CAST(0x0000A6D10152D2D3 AS DateTime), N'd', N'Pending', 0, NULL, NULL, NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (206, 1, N'fff', N'fff', N'fff', CAST(0x0000A6D10152D956 AS DateTime), N'fff', N'Pending', 0, NULL, NULL, CAST(0x0000A6D300BD595B AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (207, 1, N'f', N'f', N'f', CAST(0x0000A6D300BA7CDC AS DateTime), N'f', N'Pending', 1, 1, CAST(0x0000A6DB00CEBA01 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1207, 1, N'ggm', N'ggm', N'ggm', CAST(0x0000A6D300BD669A AS DateTime), N'ggm', N'Pending', 1, 1, CAST(0x0000A6DB00CEA962 AS DateTime), CAST(0x0000A6D300BFEFD9 AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1208, 1, N't', N't', N't', CAST(0x0000A6D300BD974D AS DateTime), N't', N'Pending', 1, 1, CAST(0x0000A6D300BFE70C AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1209, 1, N'mmmm', N'mmmm', N'mmm', CAST(0x0000A6D300BDEB46 AS DateTime), N'mmm', N'Pending', 1, 1, CAST(0x0000A6D300BFE32B AS DateTime), CAST(0x0000A6D300BFD67B AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1210, 1, N'dfg', N'df', N'd', CAST(0x0000A6DB00C602BE AS DateTime), N'd', N'Pending', 1, 1, CAST(0x0000A6DB00CDC55E AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1211, 1, N'test3', N'test3', N'test3', CAST(0x0000A6DB00C6106D AS DateTime), N'test3', N'Pending', 1, 1, CAST(0x0000A6DB00C6283A AS DateTime), CAST(0x0000A6DB00C61B15 AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1212, 1, N'test', N'test', N'test', CAST(0x0000A6DB00CD7536 AS DateTime), N'test', N'Pending', 1, 1, CAST(0x0000A6DB00CDA468 AS DateTime), NULL)
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1213, 1, N'esss33', N'ess33', N'ess33', CAST(0x0000A6DB00CE1E94 AS DateTime), N'ess33', N'Pending', 1, 1, CAST(0x0000A6DB00CE5403 AS DateTime), CAST(0x0000A6DB00CE435F AS DateTime))
INSERT [dbo].[MedicalClaim] ([ClaimId], [EmployeeId], [ClaimType], [ClaimAmount], [Hospital], [ClaimDate], [Details], [Status], [isDelete], [actionById], [actionDate], [lastUpdate]) VALUES (1214, 1, N'te', N'te', N'te', CAST(0x0000A6DB00CECA20 AS DateTime), N'te', N'Pending', 0, NULL, NULL, CAST(0x0000A6DB00CED1C8 AS DateTime))
SET IDENTITY_INSERT [dbo].[MedicalClaim] OFF
SET IDENTITY_INSERT [dbo].[MedicalQuota] ON 

INSERT [dbo].[MedicalQuota] ([MedicalQuotaId], [EmployeeId], [QuotaAmount]) VALUES (1, 1, 50000)
INSERT [dbo].[MedicalQuota] ([MedicalQuotaId], [EmployeeId], [QuotaAmount]) VALUES (2, 2, 60000)
INSERT [dbo].[MedicalQuota] ([MedicalQuotaId], [EmployeeId], [QuotaAmount]) VALUES (3, 3, 70000)
INSERT [dbo].[MedicalQuota] ([MedicalQuotaId], [EmployeeId], [QuotaAmount]) VALUES (4, 4, 80000)
INSERT [dbo].[MedicalQuota] ([MedicalQuotaId], [EmployeeId], [QuotaAmount]) VALUES (5, 5, 90000)
SET IDENTITY_INSERT [dbo].[MedicalQuota] OFF
ALTER TABLE [dbo].[MedicalClaim] ADD  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[MedicalClaim]  WITH CHECK ADD FOREIGN KEY([actionById])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[MedicalClaim]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[MedicalQuota]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
USE [master]
GO
ALTER DATABASE [MedicalClaim] SET  READ_WRITE 
GO
