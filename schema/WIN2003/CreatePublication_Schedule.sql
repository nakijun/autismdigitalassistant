-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'ADA', @optname = N'merge publish', @value = N'true'
GO

-- Adding the merge publication
use [ADA]
exec sp_addmergepublication @publication = N'Schedule', @description = N'Merge publication of database ''ADA'' from Publisher ''WIN2003''.', @sync_mode = N'character', @retention = 14, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @dynamic_filters = N'false', @conflict_retention = 14, @keep_partition_changes = N'false', @allow_synctoalternate = N'false', @max_concurrent_merge = 0, @max_concurrent_dynamic_snapshots = 0, @use_partition_groups = N'false', @publication_compatibility_level = N'90RTM', @replicate_ddl = 1, @allow_subscriber_initiated_snapshot = N'false', @allow_web_synchronization = N'true', @allow_partition_realignment = N'true', @retention_period_unit = N'days', @conflict_logging = N'both', @automatic_reinitialization_policy = 0
GO


exec sp_addpublication_snapshot @publication = N'Schedule', @frequency_type = 4, @frequency_interval = 14, @frequency_relative_interval = 1, @frequency_recurrence_factor = 0, @frequency_subday = 1, @frequency_subday_interval = 5, @active_start_time_of_day = 500, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'WIN2003\snapshot_agent', @job_password = N'p@ssw0rd', @publisher_security_mode = 0, @publisher_login = N'ada', @publisher_password = N'p@ssw0rd'
exec sp_grant_publication_access @publication = N'Schedule', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'WIN2003\SQLServer2005SQLAgentUser$WIN2003$MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'WIN2003\SQLServer2005MSSQLUser$WIN2003$MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'WIN2003\IUSR_WIN2003'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'distributor_admin'
GO
exec sp_grant_publication_access @publication = N'Schedule', @login = N'ada'
GO

-- Adding the merge articles
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'UserLevel', @source_owner = N'dbo', @source_object = N'UserLevel', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'none', @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'Schedule', @source_owner = N'dbo', @source_object = N'Schedule', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'[IsActive]=''true'' AND ([UserId] IN (SELECT [UserId] FROM [Device] WHERE [DeviceId]=HOST_NAME()) OR [Type]= 2)', @vertical_partition = N'true', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 0, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'ScheduleId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'UserId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'Date', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'Type', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'IsActive', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'SymbolId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'Name', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Schedule', @column = N'rowguid', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'User', @source_owner = N'dbo', @source_object = N'User', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'[UserId] IN (SELECT [UserId] FROM [Device] WHERE [DeviceId]=HOST_NAME())', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 0, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'Device', @source_owner = N'dbo', @source_object = N'Device', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'none', @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'[DeviceId] =HOST_NAME()', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 0, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'Reminder', @source_owner = N'dbo', @source_object = N'Reminder', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'Activity', @source_owner = N'dbo', @source_object = N'Activity', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'[ScheduleId] IN (SELECT [ScheduleId] FROM [Device],[User], [Schedule] WHERE( [Device].[DeviceId]=HOST_NAME() AND [User].[UserId]=[Device].[UserId] AND [User].[UserId]=[Schedule].[UserId] AND [Schedule].IsActive=''true'') OR [Schedule].[Type] =2)', @vertical_partition = N'true', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 0, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'ActivityId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'ScheduleId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'Sequence', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'Name', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'Descripton', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'SymbolId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'StartTime', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'EndTime', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'MiniScheduleId', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'Score', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'ExecutionStart', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'ExecutionEnd', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
exec sp_mergearticlecolumn @publication = N'Schedule', @article = N'Activity', @column = N'rowguid', @operation = N'add', @force_invalidate_snapshot = 1, @force_reinit_subscription = 1
GO
use [ADA]
exec sp_addmergearticle @publication = N'Schedule', @article = N'Activity_Reminder', @source_owner = N'dbo', @source_object = N'Activity_Reminder', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'none', @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO

use [ADA]
exec sp_changemergepublication N'Schedule', N'status', N'active'
GO
