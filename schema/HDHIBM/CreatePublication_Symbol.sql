-- Enabling the replication database
use master
exec sp_replicationdboption @dbname = N'ADA', @optname = N'merge publish', @value = N'true'
GO

-- Adding the merge publication
use [ADA]
exec sp_addmergepublication @publication = N'Symbol', @description = N'Merge publication of database ''ADA'' from Publisher ''HDHIBM''.', @sync_mode = N'character', @retention = 0, @allow_push = N'true', @allow_pull = N'true', @allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', @compress_snapshot = N'false', @ftp_port = 21, @ftp_login = N'anonymous', @allow_subscription_copy = N'false', @add_to_active_directory = N'false', @dynamic_filters = N'false', @conflict_retention = 14, @keep_partition_changes = N'false', @allow_synctoalternate = N'false', @max_concurrent_merge = 0, @max_concurrent_dynamic_snapshots = 0, @use_partition_groups = N'false', @publication_compatibility_level = N'90RTM', @replicate_ddl = 1, @allow_subscriber_initiated_snapshot = N'false', @allow_web_synchronization = N'true', @allow_partition_realignment = N'true', @retention_period_unit = N'days', @conflict_logging = N'both', @automatic_reinitialization_policy = 0
GO


exec sp_addpublication_snapshot @publication = N'Symbol', @frequency_type = 4, @frequency_interval = 14, @frequency_relative_interval = 1, @frequency_recurrence_factor = 0, @frequency_subday = 1, @frequency_subday_interval = 5, @active_start_time_of_day = 500, @active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, @job_login = N'hdhibm\snapshot_agent', @job_password = N'p@ssw0rd', @publisher_security_mode = 0, @publisher_login = N'ada', @publisher_password = N'p@ssw0rd'
exec sp_grant_publication_access @publication = N'Symbol', @login = N'sa'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'NT AUTHORITY\SYSTEM'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'BUILTIN\Administrators'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'HDHIBM\SQLServer2005SQLAgentUser$IBM-T60-HDH$MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'HDHIBM\SQLServer2005MSSQLUser$IBM-T60-HDH$MSSQLSERVER'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'distributor_admin'
GO
exec sp_grant_publication_access @publication = N'Symbol', @login = N'ada'
GO

-- Adding the merge articles
use [ADA]
exec sp_addmergearticle @publication = N'Symbol', @article = N'Category', @source_owner = N'dbo', @source_object = N'Category', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'(CategoryId IN
                          (SELECT     CategoryId
                            FROM          Symbol
                            WHERE      (IsActive = 1)))', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Symbol', @article = N'Culture', @source_owner = N'dbo', @source_object = N'Culture', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Symbol', @article = N'Resource', @source_owner = N'dbo', @source_object = N'Resource', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'none', @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'(ResourceId IN
                          (SELECT     r.ResourceId
                            FROM          Resource AS r INNER JOIN
                                                   Culture AS c ON r.ResourceId = c.Language
                            UNION
                            SELECT     r.ResourceId
                            FROM         Resource AS r INNER JOIN
                                                  Symbol AS s ON r.ResourceId = s.Name
                            WHERE     (s.IsActive = 1)
                            UNION
                            SELECT     r.ResourceId
                            FROM         Category AS c INNER JOIN
                                                  Symbol AS s ON c.CategoryId = s.CategoryId INNER JOIN
                                                  Resource AS r ON c.Name = r.ResourceId
                            WHERE     (s.IsActive = 1)))', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO
use [ADA]
exec sp_addmergearticle @publication = N'Symbol', @article = N'Symbol', @source_owner = N'dbo', @source_object = N'Symbol', @type = N'table', @description = N'', @creation_script = N'', @pre_creation_cmd = N'drop', @schema_option = 0x0000000008034FF1, @identityrangemanagementoption = N'auto', @pub_identity_range = 10000, @identity_range = 1000, @threshold = 80, @destination_owner = N'dbo', @force_reinit_subscription = 1, @column_tracking = N'false', @subset_filterclause = N'IsActive = 1', @vertical_partition = N'false', @verify_resolver_signature = 1, @allow_interactive_resolver = N'false', @fast_multicol_updateproc = N'true', @check_permissions = 0, @subscriber_upload_options = 2, @delete_tracking = N'true', @compensate_for_errors = N'false', @stream_blob_columns = N'false', @partition_options = 0
GO

use [ADA]
exec sp_changemergepublication N'Symbol', N'status', N'active'
GO
