/****** Scripting replication configuration for server WIN2003. Script Date: 16/03/2007 11:06:03 ******/
/****** Please Note: For security reasons, all password parameters were scripted with either NULL or an empty string. ******/

/****** Begin: Script to be run at Publisher: WIN2003 ******/

/****** Installing the server WIN2003 as a Distributor. Script Date: 16/03/2007 11:06:03 ******/
use master
exec sp_adddistributor @distributor = N'WIN2003', @password = N''
GO

-- Adding the agent profiles
-- Updating the agent profile defaults
exec sp_MSupdate_agenttype_default @profile_id = 1
GO
exec sp_MSupdate_agenttype_default @profile_id = 2
GO
exec sp_MSupdate_agenttype_default @profile_id = 4
GO
exec sp_MSupdate_agenttype_default @profile_id = 6
GO
exec sp_MSupdate_agenttype_default @profile_id = 11
GO

-- Adding the distribution databases
use master
exec sp_adddistributiondb @database = N'distribution', @data_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data', @data_file = N'distribution.MDF', @data_file_size = 5, @log_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data', @log_file = N'distribution.LDF', @log_file_size = 2, @min_distretention = 0, @max_distretention = 72, @history_retention = 48, @security_mode = 1
GO

-- Adding the distribution publishers
exec sp_adddistpublisher @publisher = N'WIN2003', @distribution_db = N'distribution', @security_mode = 1, @working_directory = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\ReplData', @trusted = N'false', @thirdparty_flag = 0, @publisher_type = N'MSSQLSERVER'
GO


/****** End: Script to be run at Publisher: WIN2003 ******/


