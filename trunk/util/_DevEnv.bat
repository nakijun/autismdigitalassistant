@ECHO OFF
CD/D %~DP0%..
SET WORKSPACE=%CD%
SET PATH=%WORKSPACE%\util;%PATH%;
SET BUILDFILE="%WORKSPACE%\util\default.build"

TITLE DevEnv Command Prompt @ %WORKSPACE%
CMD
:END
