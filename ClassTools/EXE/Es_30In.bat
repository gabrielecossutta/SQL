@echo off
sc create Es30Service binPath= "C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\Es_30WINS.exe" start= auto
net start Es30Service 