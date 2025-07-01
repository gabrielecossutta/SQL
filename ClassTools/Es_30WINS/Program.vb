Imports System.ServiceProcess
Imports System.Threading

Module Program
    Sub Main()
        If Environment.UserInteractive Then
            Dim service As New MyService()
            service.OnDebug()
            Threading.Thread.Sleep(Timeout.Infinite)
        Else
            Dim ServicesToRun() As ServiceBase = {New MyService()}
            ServiceBase.Run(ServicesToRun)
        End If
    End Sub
End Module
