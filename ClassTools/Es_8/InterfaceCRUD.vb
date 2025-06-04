#Region "INTERACE"
Imports System.Reflection
Imports Microsoft.SqlServer
Imports Microsoft.VisualBasic.ApplicationServices
Imports ClassTools
Imports System.Data.SqlClient
Imports Microsoft.Identity.Client

''' <summary>
''' Interface for CRUD operations
''' </summary>
Public Interface ICRUD

    Sub Create()

    Sub Read()

    Sub Update()

    Sub Delete()

End Interface

#End Region

#Region "BASE CLASS"
''' <summary>
''' Base class that implements the ICRUD interface, cant be instantiated directly.
''' It provides default implementations for Create, Read, Update, and Delete methods.
''' Derived classes can override these methods to provide specific functionality.
''' </summary>
Public Class Base
    Implements ICRUD

    Protected tableName As String
    Protected connectionString As String

    Public Sub New(name As String, connectionString As String)
        tableName = name
        Me.connectionString = connectionString
    End Sub

    Public Function GetSQLColumnsAndValues() As (String, String)
        Dim properties As PropertyInfo() = Me.GetType().GetProperties()
        Dim columnNames As String = String.Join(",", properties.Select(Function(p) p.Name))
        Dim paramNames As String = String.Join(",", properties.Select(Function(p) $"'{p.GetValue(Me)}'"))
        Return (columnNames, paramNames)
    End Function

    Public Sub Create() Implements ICRUD.Create
        Dim result = GetSQLColumnsAndValues()
        Dim columns As String = result.Item1
        Dim values As String = result.Item2
        Dim sql As String = $"INSERT INTO {tableName} ({columns}) VALUES ({values});"
        'Crud.CreateRow(sql, Crud.ConnectToTheServer(connectionString), tableName, "")
        Console.WriteLine("Query SQL di INSERT:")
        Console.WriteLine(sql)
    End Sub

    Public Sub Read() Implements ICRUD.Read
        Dim properties As PropertyInfo() = Me.GetType().GetProperties()
        Dim columnNames As String = String.Join(",", properties.Select(Function(p) p.Name))
        Dim sql As String = $"SELECT {columnNames} FROM {tableName};"
        Crud.ReadRow(sql, Crud.ConnectToTheServer(connectionString))
        Console.WriteLine("Query SQL di SELECT:")
        Console.WriteLine(sql)
    End Sub

    Public Sub Update() Implements ICRUD.Update
        Dim properties As PropertyInfo() = Me.GetType().GetProperties()
        Dim updates As String = String.Join(",", properties.Select(Function(p) $"{p.Name}='{p.GetValue(Me)}'"))
        Dim sql As String = $"UPDATE {tableName} SET {updates} WHERE RegionID = 1 ;"
        Crud.UpdateCell(New List(Of String) From {sql}, Crud.ConnectToTheServer(connectionString), tableName, "")
        Console.WriteLine("Query SQL di UPDATE:")
        Console.WriteLine(sql)
    End Sub

    Public Overridable Sub Delete() Implements ICRUD.Delete
        Dim sql As String = $"DELETE FROM {tableName} WHERE ShipperID='100';"
        Crud.DeleteRows(sql, Crud.ConnectToTheServer(connectionString), "")
        Console.WriteLine("Query SQL di DELETE:")
        Console.WriteLine(Sql)
    End Sub
End Class

#End Region

Public Class Tabella1
    Inherits Base

    Public Property ShipperID As String
    Public Property CompanyName As String
    Public Property Phone As String

    Public Sub New(connectionString As String)

        MyBase.New("Shippers", connectionString)
        ShipperID = "100"
        CompanyName = "FakeCompany"
        Phone = "123 456 7890"

    End Sub
End Class

Public Class Tabella2
    Inherits Base
    Public Property RegionID As String
    Public Property RegionDescription As String

    Public Sub New(connectionString As String)
        MyBase.New("Region", connectionString)
        RegionID = "100"
        RegionDescription = "Ugly"
    End Sub

    Public Overrides Sub Delete()
        Dim sql As String = $"DELETE FROM {tableName} WHERE RegionID='100';"
        MyBase.Delete()
    End Sub
End Class

Module InterfaceCRUD

    Sub Main()
        Dim t1 As New Tabella1(ExternalArgumentsLoginCheck())
        Dim t2 As New Tabella2(ExternalArgumentsLoginCheck())

        Console.WriteLine("Generazione query CRUD per Tabella1:")
        t1.Create()
        t1.Read()
        t1.Update()
        t1.Delete()

        Console.WriteLine(vbCrLf & "Generazione query CRUD per Tabella2:")
        t2.Create()
        t2.Read()
        t2.Update()
        t2.Delete()

        Console.WriteLine(vbCrLf & "Premi un tasto per terminare...")
        Console.ReadKey()
    End Sub

    Private Function ExternalArgumentsLoginCheck()

        'Get the arguments from the command line
        Dim args As String() = Environment.GetCommandLineArgs()

        'connection for the SQL Server
        Dim connection As SqlConnection = Nothing

        'Check if the arguments are more than 1
        If args.Count > 1 Then

            Return args(1)

        End If

        Return connection

    End Function
End Module




