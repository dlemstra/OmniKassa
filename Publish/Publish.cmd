@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"

set solution=..\OmniKassa.sln

nuget restore %solution%

msbuild %solution% /m:4 /t:Rebuild /p:Configuration=Release
if %errorlevel% neq 0 goto done

vstest.console /inIsolation ..\tests\OmniKassa.Tests\bin\Release\net45\OmniKassa.Tests.dll
if %errorlevel% neq 0 goto done

cd ..\tests\OmniKassa.Tests\

dotnet test -f netcoreapp1.1
if %errorlevel% neq 0 goto done

cd ..\..\Publish

set projectdir=..\src\OmniKassa

msbuild %projectdir%\OmniKassa.csproj /m:4 /t:Pack /p:Configuration=Release
if %errorlevel% neq 0 goto done

copy %projectdir%\bin\Release\*.nupkg .

:done

pause