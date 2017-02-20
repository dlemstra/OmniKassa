@echo off
call "%vs140comntools%vsvars32.bat"

set solution=..\OmniKassa.sln

nuget restore %solution%

msbuild %solution% /m:4 /t:Rebuild /p:Configuration=Release
if %errorlevel% neq 0 goto done

vstest.console /inIsolation ..\tests\OmniKassa.Tests\bin\Release\OmniKassa.Tests.dll
if %errorlevel% neq 0 goto done

nuget pack ..\src\OmniKassa\OmniKassa.csproj -Properties Configuration=Release

:done

pause