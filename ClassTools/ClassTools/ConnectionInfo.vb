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

Public Module Program
    Dim connection As Connections

    Public Function Populate() As Connections

        connection = New Connections() With {
            .ServicePort = "123344",
            .ProtocolName = "Create",
            .ConnectionStrings = New List(Of ConnectionString) From {
                New ConnectionString With {
                    .SQLServerName = "SQLExpress",
                    .DatabaseName = "Northwind",
                    .UserName = "NorthwindUser",
                    .Password = "123"
                },
                New ConnectionString With {
                    .SQLServerName = "SQLServer01",
                    .DatabaseName = "AdventureWorks",
                    .UserName = "AdminUser",
                    .Password = "456"
                },
                New ConnectionString With {
                    .SQLServerName = "RemoteSQL",
                    .DatabaseName = "SalesDB",
                    .UserName = "SalesUser",
                    .Password = "789"
                }
            }
        }
        Return connection
    End Function
End Module