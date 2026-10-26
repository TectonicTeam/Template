IF EXIST nupkg (rmdir nupkg /s /q)
IF %ERRORLEVEL% NEQ 0 EXIT
dotnet clean .
IF %ERRORLEVEL% NEQ 0 EXIT
REM Prompt for user name  

set /p version=Version:   
set PACKAGE_VERSION=%version%

set /p username=Username:   

set /p password=Passowrd:  

dotnet pack --configuration Release --output .\nupkg

@REM dotnet nuget remove source "Nexus-Nuget" || true
@REM dotnet nuget add source "$NUGET_SOURCE" --name "Nexus-Nuget" --username "$NUGET_USERNAME" --password "$NUGET_PASSWORD" --store-password-in-clear-text

dotnet nuget push nupkg\*.nupkg -s "https://rgs.entekhabgroup.com/repository/nuget-deka-host/" -k  --skip-duplicate
PAUSE

