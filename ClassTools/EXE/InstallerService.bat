@echo off
sc create Es23Service binPath= "C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\Es_23.exe" start= auto
net start Es23Service
