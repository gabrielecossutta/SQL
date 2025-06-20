@echo off
set SERVICE_NAME=TotemService
set DISPLAY_NAME=TotemService
set EXE_PATH="C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\Es_23.exe"

echo Gestione del servizio %SERVICE_NAME%...
sc stop "%SERVICE_NAME%" >nul 2>&1
sc delete "%SERVICE_NAME%" >nul 2>&1
timeout /t 1 >nul

echo Installazione del servizio con SC...
sc create "%SERVICE_NAME%" binPath= %EXE_PATH% start= auto DisplayName= "%DISPLAY_NAME%"
if %errorlevel% neq 0 (
    echo ERRORE durante la creazione del servizio.
    pause
    exit /b
)

net start "%SERVICE_NAME%"
echo Servizio avviato.
pause
