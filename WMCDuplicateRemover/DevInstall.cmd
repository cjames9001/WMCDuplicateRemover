@ECHO Off
ECHO.Usage: DevInstall.cmd [/u][/debug]
set CompanyName=CTS
set AssemblyName=WMCDuplicateRemover
set RegistrationName=Registration
set ProgramImage=Application.png
set GacUtilPath=%ProgramFiles(x86)%

ECHO Determine whether we are on an 32 or 64 bit machine
if "%PROCESSOR_ARCHITECTURE%"=="x86" if "%PROCESSOR_ARCHITEW6432%"=="" goto x86

ECHO.On an x64 machine
set ProgramFilesPath=%ProgramFiles(x86)%
goto unregister

:x86
    ECHO.On an x86 machine
    set ProgramFilesPath=%ProgramFiles%

:unregister
    ECHO.*** Unregistering and deleting assemblies ***
    ECHO.Unregister and delete previously installed files (which may fail if nothing is registered)

    ECHO.Unregister the application entry points
    %windir%\ehome\RegisterMCEApp.exe /allusers "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\%RegistrationName%.xml" /u
    ECHO.Remove the DLL from the Global Assembly cache
    "%GacUtilPath%\Microsoft SDKs\Windows\v7.0A\bin\gacutil.exe" /u "%AssemblyName%"
    ECHO.Delete the folder containing the DLLs and supporting files (silent if successful)
    rd /s /q "%ProgramFilesPath%\%CompanyName%\%AssemblyName%"
    REM Exit out if the /u uninstall argument is provided, leaving no trace of program files.
    if "%1"=="/u" goto exit

:releasetype
    REM evaluate the second argument
    if "%1"=="/debug" goto debug
    ECHO.Using the release version of the binaries
    set ReleaseType=Release
    goto checkbin

:debug
    ECHO.Using the Debug version of the binaries
    set ReleaseType=Debug

:checkbin
    if exist ".\bin\%ReleaseType%\%AssemblyName%.dll" goto register

    ECHO.Cannot find %ReleaseType% binaries.
    ECHO.Build solution as %ReleaseType% and run script again.
    goto exit

:register
    ECHO.*** Copying and registering assemblies ***
    ECHO.Create the path for the binaries and supporting files (silent if successful)
    md "%ProgramFilesPath%\%CompanyName%\%AssemblyName%"
    ECHO.Copy the binaries to program files
    copy /y ".\bin\%ReleaseType%\%AssemblyName%.dll" "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\"
    ECHO.Copy the registration XML to program files
    copy /y ".\Setup\%RegistrationName%.xml" "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\"
    ECHO.Copy the program image to program files
    copy /y ".\Images\%ProgramImage%" "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\"
    ECHO.Register the DLL with the global assembly cache
    "%GacUtilPath%\Microsoft SDKs\Windows\v7.0A\bin\gacutil.exe" /if "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\%AssemblyName%.dll"
    ECHO.Register the application with Windows Media Center
    %windir%\ehome\RegisterMCEApp.exe /allusers "%ProgramFilesPath%\%CompanyName%\%AssemblyName%\%RegistrationName%.xml"

:exit
