@echo off
set SERVICE_NAME=TotemService
set EXE_PATH="C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\Es_23.exe"

echo Controllo del servizio %SERVICE_NAME%...
sc query "%SERVICE_NAME%" >nul 2>&1
if %errorlevel%==0 (
    echo Fermando il servizio...
    net stop "%SERVICE_NAME%" >nul
)

echo Disinstallazione del servizio...
"%WINDIR%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" /u %EXE_PATH%

pause
