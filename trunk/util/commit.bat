@echo off
nant -buildfile:%BUILDFILE% commit %*
rem call backup-svn.bat
