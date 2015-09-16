@echo off
echo WScript.Echo DateDiff("s", "01/01/1970 00:00:00", Now()) > epoch.vbs
for /f "delims=" %%x in ('cscript /nologo epoch.vbs') do set epoch=%%x
del epoch.vbs
echo EPOCH: %epoch%

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" /t:Clean
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" /t:Rebuild /p:Configuration=Release
tools\nuget pack Conekta.nuspec -Version 2.1.%epoch% -Verbosity detailed

move *.nupkg D:\Desktop\NuGet\