/****** Scripting replication configuration for server HDHIBM. Script Date: 14/03/2007 16:36:24 ******/
/****** Please Note: For security reasons, all password parameters were scripted with either NULL or an empty string. ******/

/****** Installing the server HDHIBM as a Distributor. Script Date: 14/03/2007 16:36:25 ******/
use master
exec sp_adddistributor @distributor = N'HDHIBM', @password = N''
GO
exec sp_adddistributiondb @database = N'distribution', @data_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data', @log_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data', @log_file_size = 2, @min_distretention = 0, @max_distretention = 72, @history_retention = 48, @security_mode = 1
GO

use [distribution] 
if (not exists (select * from sysobjects where name = 'UIProperties' and type = 'U ')) 
	create table UIProperties(id int) 
if (exists (select * from ::fn_listextendedproperty('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', null, null))) 
	EXEC sp_updateextendedproperty N'SnapshotFolder', N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\ReplData', 'user', dbo, 'table', 'UIProperties' 
else 
	EXEC sp_addextendedproperty N'SnapshotFolder', 'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\ReplData', 'user', dbo, 'table', 'UIProperties'
GO

exec sp_adddistpublisher @publisher = N'HDHIBM', @distribution_db = N'distribution', @security_mode = 0, @login = N'ada', @password = N'', @working_directory = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\ReplData', @trusted = N'false', @thirdparty_flag = 0, @publisher_type = N'MSSQLSERVER'
GO
