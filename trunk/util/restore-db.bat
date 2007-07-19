@echo off
goto restore

net stop SQLSERVERAGENT
net stop mssqlserver

net start mssqlserver
net start SQLSERVERAGENT

:restore
sqlcmd -Q "RESTORE DATABASE ADA FROM DISK = '%WORKSPACE%\Schema\%COMPUTERNAME%\ADA.bck' WITH REPLACE"
