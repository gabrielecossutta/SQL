Imports System.ComponentModel
Imports System.ServiceProcess

<RunInstaller(True)>
Public Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    Private serviceInstaller As New ServiceInstaller()
    Private processInstaller As New ServiceProcessInstaller()

    Public Sub New()
        processInstaller.Account = ServiceAccount.LocalSystem
        serviceInstaller.ServiceName = "TotemService"
        serviceInstaller.DisplayName = "TotemService"
        serviceInstaller.StartType = ServiceStartMode.Automatic

        Installers.Add(processInstaller)
        Installers.Add(serviceInstaller)
    End Sub
End Class
