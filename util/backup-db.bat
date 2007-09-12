@echo off
sqlcmd -Q "BACKUP DATABASE ADA TO DISK = '%WORKSPACE%\Schema\%COMPUTERNAME%\ADA.bck' WITH FORMAT"
rem sqlcmd -Q "BACKUP DATABASE msdb TO DISK = '%WORKSPACE%\Schema\%COMPUTERNAME%\msdb.bck' WITH FORMAT"
rem sqlcmd -Q "BACKUP DATABASE distribution TO DISK = '%WORKSPACE%\Schema\%COMPUTERNAME%\distribution.bck' WITH FORMAT"
rem sqlcmd -Q "BACKUP DATABASE master TO DISK = '%WORKSPACE%\Schema\%COMPUTERNAME%\master.bck' WITH FORMAT"
