@echo off
pushd Vendor
pushd Sharpmake
:: Clear previous run status
COLOR

:: First compile sharpmake to insure we are trying to deploy using an executable corresponding to the code.
dotnet build Sharpmake.sln /p:Configuration=Release /p:Platform="Any CPU"
if %errorlevel% NEQ 0 goto error


echo sharpmake executable is built, fetching executable...
set SHARPMAKE_EXECUTABLE=%~dp0Sharpmake.Application\bin\Release\net6.0\Sharpmake.Application.exe
if not exist %SHARPMAKE_EXECUTABLE% echo Cannot find sharpmake executable in %~dp0Sharpmake.Application\bin\Release\net6.0 & pause & goto error

popd
popd

echo sharpmake building solution...

call %SHARPMAKE_EXECUTABLE% /sources(ProjectDefinitions/Solution.cs)

pause


:error
@COLOR 4F
pause
