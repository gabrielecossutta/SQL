Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports ClassTools
Module Module1

    Sub Main()

        Dim Client1 As New Overriding.Client With {
        .CustomerID = "aaa",
        .CompanyName = "bbb",
        .ContactName = "ccc",
        .ContactTitle = "ddd",
        .Address = "eee",
        .City = "fff",
        .Region = "ggg",
        .PostalCode = "hhh",
        .Country = "iii",
        .Phone = 3,
        .Fax = 4
            }
        Dim Client2 As New Overriding.Client With {
        .CustomerID = "aaa",
        .CompanyName = "lll",
        .ContactName = "mmm",
        .ContactTitle = "nnn",
        .Address = "zzz",
        .City = "xxx",
        .Region = "ccc",
        .PostalCode = "vvv",
        .Country = "bbb",
        .Phone = 1,
        .Fax = 2
            }
        Dim Client3 As New Overriding.Client With {
        .CustomerID = "bbb",
        .CompanyName = "bbb",
        .ContactName = "ccc",
        .ContactTitle = "ddd",
        .Address = "eee",
        .City = "fff",
        .Region = "ggg",
        .PostalCode = "hhh",
        .Country = "iii",
        .Phone = 3,
        .Fax = 4
            }


        Console.WriteLine(Client1.ToString())
        Console.ReadKey()

        Console.WriteLine(Client1.Equals(Client2))
        Console.ReadKey()

        Console.WriteLine(Client1.Equals(Client3))
        Console.ReadKey()

        Console.WriteLine(Client2.Equals(Client3))
        Console.ReadKey()

    End Sub

End Module

Public Class Overriding

    'List of clients
    Public ClientList As List(Of Client) = New List(Of Client)()

    'DBContext for Entity Framework
    Public Class CustomersDbContext
        Inherits DbContext

        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Property Customers As DbSet(Of Client)

    End Class

    'Name of the table in the database
    <Table("Customers")>
    Public Class Client
        <Key> 'Primary key
        Public Property CustomerID As String
        Public Property CompanyName As String
        Public Property ContactName As String
        Public Property ContactTitle As String
        Public Property Address As String
        Public Property City As String
        Public Property Region As String
        Public Property PostalCode As String
        Public Property Country As String
        Public Property Phone As String
        Public Property Fax As String

        'Override method to String
        Public Overrides Function ToString() As String
            Dim properties = Me.GetType().GetProperties()
            Return String.Join(";", properties.Select(Function(p) p.GetValue(Me)?.ToString()))

        End Function

        'Override method Equal
        Public Overrides Function Equals(obj As Object) As Boolean

            Return Me.CustomerID = obj.CustomerID

        End Function

    End Class
End Class