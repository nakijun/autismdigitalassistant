@echo off
nant -buildfile:%BUILDFILE% commit %*
call backup-svn.bat
