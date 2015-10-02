@echo off
SET FILE=Conekta
echo WScript.Echo DateDiff("s", "01/01/1970 00:00:00", Now()) > epoch.vbs
for /f "delims=" %%x in ('cscript /nologo epoch.vbs') do set epoch=%%x
del epoch.vbs
SET FULLVERSION=2.1.%epoch%

tools\nuget.exe restore Conekta.sln -OutputDirectory %cd%\packages -NonInteractive

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Clean
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" /t:Rebuild /p:Configuration=Release

del D:\Desktop\NuGet\%FILE%*.nupkg
tools\nuget.exe pack Conekta.nuspec -p Configuration=Release -Version %FULLVERSION%

move *.nupkg D:\Desktop\NuGet\

IF "%1" == "push" GOTO PUSH
GOTO END
:PUSH
tools\nuget push D:\Desktop\NuGet\%FILE%.%FULLVERSION%.nupkg
:END