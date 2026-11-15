@echo off
:: Generates Engine.sln plus a solution to debug the Sharpmake scripts themselves.
COLOR
call "%~dp0Scripts\GenerateSolution.bat" /generateDebugSolution %*
if errorlevel 1 (
    COLOR 4F
    echo Generating the solution failed.
)
pause
