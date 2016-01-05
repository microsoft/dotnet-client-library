@echo off

::Capture the current dir
set ROOT_DIR=%CD%

::specify the version of the library
set VERSION=8.0.0

::set the configuration (Release or Debug)
set CONFIG=Release

::set the platform (x86)
set PLATFORM=x86

::
::
::DEPLOYR CLIENT LIBRARY CONFIGURATION
::
::

::set the location where referenced assemblies can be found
set CLIENT_LIB_REF_PATH=%ROOT_DIR%\lib

::define output dir
set CLIENT_LIB_OUTDIR=%ROOT_DIR%\release\

::define which version of msbuild we are going to use (Client lib uses .NET 4.5.1)
set DOTNET_4.5_BUILD_EXE=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild 

::specify the location of the source solution file (i.e. deployR.sln)
set CLIENT_LIB_SRC_DIR=%ROOT_DIR%\src

::specify client zip command
set CLIENT_ZIP_CMD=zip DeployR-DotNet-Client-%VERSION%.zip *.dll


::
::
::DEPLOYR CLIENT LIBRARY BUILD
::
::

::delete the output directory
if exist %CLIENT_LIB_OUTDIR% rmdir %CLIENT_LIB_OUTDIR% /s /q

::build the library
cd %CLIENT_LIB_SRC_DIR%
%DOTNET_4.5_BUILD_EXE% /p:Configuration=%CONFIG%;Platform=%PLATFORM%;ReferencePath=%CLIENT_LIB_REF_PATH%;OutDir=%CLIENT_LIB_OUTDIR% /t:Clean;Rebuild
::create the zip file
if exist %CLIENT_LIB_OUTDIR% (
	cd %CLIENT_LIB_OUTDIR%
	%CLIENT_ZIP_CMD%
) 


::
::
::HELP FILE
::
::

::Client lib Sandcastle Help File Builder project file
set CLIENT_LIB_HELP_PROJECT=%ROOT_DIR%\help\deployR.shfbproj

:build client lib help file
cd %CLIENT_LIB_SRC_DIR%
set PLATFORM=
%DOTNET_4.5_BUILD_EXE% /p:Configuration=%CONFIG%;ReferencePath=%CLIENT_LIB_SRC_DIR%;OutDir=%CLIENT_LIB_OUTDIR%;OutputPath=%CLIENT_LIB_OUTDIR% %CLIENT_LIB_HELP_PROJECT%

::reset the directory
cd %ROOT_DIR%

