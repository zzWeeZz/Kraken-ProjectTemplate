@echo off
:: Generates Engine.sln. Extra arguments go to Sharpmake, e.g. /fetchupdate.
COLOR
call "%~dp0Scripts\GenerateSolution.bat" %*
if errorlevel 1 (
    COLOR 4F
    echo Generating the solution failed.
)
pause
