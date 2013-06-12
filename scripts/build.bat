@echo OFF
setlocal

SET InnoSetupDir=C:\Program Files (x86)\Inno Setup 5

REM
REM This file is called by a post build event when in release mode
REM It copies the bin\Release files to %~dp0\..\.builds\
REM

cd %~dp0

echo Copying files....
rmdir /s /Q "..\.builds
xcopy /S /I /R "..\src\MutoMark\bin\Release" "..\.builds"

REM
REM Compile if InnoSetup is available
REM
if not exist "%InnoSetupDir%" goto end

echo Compiling latest build...
SET PATH=%PATH%;"%InnoSetupDir%\"
call iscc.exe /Q "setup-script.iss"

REM We're done
:end