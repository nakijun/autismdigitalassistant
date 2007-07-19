@echo off
rem goto restore

net stop SQLSERVERAGENT
net stop mssqlserver

net start mssqlserver
net start SQLSERVERAGENT

:restore
sqlcmd -i %WORKSPACE%\Schema\DisablePublishingDistribution.sql
if errorlevel 1 goto error

sqlcmd -Q "RESTORE DATABASE ADA FROM DISK = '%WORKSPACE%\Schema\%1\ADA.bck' WITH REPLACE"
if errorlevel 1 goto error

sqlcmd -i %WORKSPACE%\Schema\%COMPUTERNAME%\ConfigureDistribution.sql
if errorlevel 1 goto error

sqlcmd -i %WORKSPACE%\Schema\%COMPUTERNAME%\CreatePublication_Symbol.sql
if errorlevel 1 goto error

sqlcmd -i %WORKSPACE%\Schema\%COMPUTERNAME%\CreatePublication_Schedule.sql
if errorlevel 1 goto error

sqlcmd -i %WORKSPACE%\Schema\%COMPUTERNAME%\CreatePublication_Communicator.sql
if errorlevel 1 goto error

sqlmonitor

goto end

:error
echo error!!!

:end