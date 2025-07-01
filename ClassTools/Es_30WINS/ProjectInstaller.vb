Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

Public Class ProjectInstaller

    Public Sub New()
        ServiceProcessInstaller1.Account = ServiceAccount.LocalSystem

        ServiceInstaller1.ServiceName = "Es23Service"
        ServiceInstaller1.DisplayName = "Es23Service"
        ServiceInstaller1.StartType = ServiceStartMode.Automatic

        Installers.Add(ServiceProcessInstaller1)
        Installers.Add(ServiceInstaller1)

    End Sub

End Class