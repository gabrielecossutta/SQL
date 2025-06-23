Imports System.ServiceProcess

Module Program
    Sub Main()

        'This module is the entry poin for the window service
        Dim ServicesToRun() As ServiceBase
        ServicesToRun = New ServiceBase() {New MyService()}
        ServiceBase.Run(ServicesToRun)

    End Sub
End Module
