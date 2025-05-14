Imports System.Windows
Imports Microsoft.VisualBasic.ApplicationServices


Public Class ConnectionString
    Public Property SQLServerName As String
    Public Property DatabaseName As String
    Public Property UserName As String
    Public Property Password As String
End Class

Public Class Connections
    Public Property ServicePort As String
    Public Property ProtocolName As String
    Public Property ConnectionStrings As List(Of ConnectionString)

End Class
