CREATE DATABASE [ADA]
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'ADA', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ADA].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [ADA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ADA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ADA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ADA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ADA] SET ARITHABORT OFF 
GO
ALTER DATABASE [ADA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ADA] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ADA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ADA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ADA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ADA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ADA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ADA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ADA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ADA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ADA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ADA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ADA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ADA] SET  READ_WRITE 
GO
ALTER DATABASE [ADA] SET RECOVERY FULL 
GO
ALTER DATABASE [ADA] SET  MULTI_USER 
GO
ALTER DATABASE [ADA] SET PAGE_VERIFY CHECKSUM  
GO
USE [ADA]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [ADA] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
