@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=-Version 1.0.0
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore
tools\nuget.exe restore Conekta.sln -OutputDirectory %cd%\packages -NonInteractive

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Conekta.sln /t:Clean,Build /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
rd /s /q artifacts
mkdir artifacts
mkdir artifacts\nuget
tools\nuget.exe pack Conekta.nuspec -symbols -o artifacts\nuget -p Configuration=%config% %version%

REM Plain assemblies

mkdir artifacts\assemblies
mkdir artifacts\assemblies\net40
mkdir artifacts\assemblies\net45

copy Conekta.NET40\bin\%config%\Conekta*.dll artifacts\assemblies\net40
copy Conekta.NET40\bin\%config%\Conekta*.pdb artifacts\assemblies\net40
copy Conekta.NET45\bin\%config%\Conekta*.dll artifacts\assemblies\net45
copy Conekta.NET45\bin\%config%\Conekta*.pdb artifacts\assemblies\net45
