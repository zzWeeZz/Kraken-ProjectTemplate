@echo off
:: Builds Sharpmake from Vendor\Sharpmake, then generates Engine.sln at the repository root.
:: Arguments are passed to Sharpmake (for instance /generateDebugSolution or /fetchupdate).
:: Called by the Build_*.bat scripts; returns a non-zero errorlevel on failure.
setlocal

set ROOT=%~dp0..
set SHARPMAKE_DIR=%ROOT%\Vendor\Sharpmake
set SHARPMAKE_EXECUTABLE=%SHARPMAKE_DIR%\Sharpmake.Application\bin\Release\net6.0\Sharpmake.Application.exe

pushd "%ROOT%"

:: The repository may have been cloned without --recursive.
if not exist "%SHARPMAKE_DIR%\Sharpmake.sln" (
    echo Fetching the Sharpmake submodule...
    git submodule update --init --recursive
    if errorlevel 1 goto error
)

:: Always build: it is incremental, so it is quick when nothing changed, and the executable
:: then always matches the Sharpmake sources (after a submodule update, for instance).
echo Building Sharpmake...
dotnet build "%SHARPMAKE_DIR%\Sharpmake.Application\Sharpmake.Application.csproj" -c Release --nologo -v q
if errorlevel 1 goto error

if not exist "%SHARPMAKE_EXECUTABLE%" (
    echo Cannot find the Sharpmake executable: %SHARPMAKE_EXECUTABLE%
    goto error
)

echo Generating the solution...
"%SHARPMAKE_EXECUTABLE%" "/sources('ProjectDefinitions/SharpmakeEntrypoint/main.sharpmake.cs')" %*
if errorlevel 1 goto error

popd
exit /b 0

:error
popd
exit /b 1
