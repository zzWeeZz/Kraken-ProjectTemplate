@echo off
:: Generates Engine.sln and builds it with MSBuild.
:: Usage: Build_Binaries.bat [Debug|Release|All]   (default: Debug)
:: The executables go to bin\Win64_<Configuration>.
setlocal
COLOR

set CONFIGURATIONS=%~1
if "%CONFIGURATIONS%"=="" set CONFIGURATIONS=Debug
if /i "%CONFIGURATIONS%"=="All" set CONFIGURATIONS=Debug Release

call "%~dp0Scripts\GenerateSolution.bat"
if errorlevel 1 goto error

set VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe
if not exist "%VSWHERE%" (
    echo Cannot find vswhere.exe. Is Visual Studio 2022 installed?
    goto error
)
set MSBUILD=
for /f "usebackq tokens=*" %%i in (`"%VSWHERE%" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do set "MSBUILD=%%i"
if not defined MSBUILD (
    echo Cannot find MSBuild. Install the "Desktop development with C++" workload of Visual Studio 2022.
    goto error
)

for %%c in (%CONFIGURATIONS%) do (
    echo Building %%c...
    "%MSBUILD%" "%~dp0Engine.sln" /m /nologo /v:minimal /p:Configuration=%%c /p:Platform=x64
    if errorlevel 1 goto error
)

echo Build succeeded.
pause
exit /b 0

:error
COLOR 4F
echo Build failed.
pause
exit /b 1
