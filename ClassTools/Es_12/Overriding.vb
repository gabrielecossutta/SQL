Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Linq
Module Module1
    Sub Main()
        Dim overring As New Overriding()

        'Populate the list of clients using Entity Framework
        overring.PopulateList(overring.ExternalArgumentsLoginCheck())

        Console.WriteLine(overring.ToString())
        Console.ReadKey()

        Console.WriteLine(overring.ClientList.Any(Function(c) c.CustomerID = "ALFKI"))
        Console.ReadKey()

        Console.WriteLine(overring.ClientList.Any(Function(c) c.CustomerID = "ALFK"))
        Console.ReadKey()
    End Sub
End Module

Public Class Overriding

    'List of clients
    Public ClientList As List(Of Client) = New List(Of Client)()

    Public Overrides Function ToString() As String
        Return String.Join(Environment.NewLine, ClientList.Select(Function(c) $"{c.CustomerID},{c.CompanyName},{c.ContactName},{c.ContactTitle},{c.Address},{c.City},{c.Region},{c.PostalCode},{c.Country},{c.Phone},{c.Fax}"))
    End Function

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

    End Class


    Public Overrides Function Equals(obj As Object) As Boolean
        Dim other As String = TryCast(obj, String)
        For Each customId In Me.ClientList.Select(Of String)(Function(c) c.CustomerID)
            If customId = other Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Funzione che popola la lista di clienti usando Entity Framework.
    ''' Aggiunto anche il debug per monitorare eventuali problemi.
    ''' </summary>
    Public Sub PopulateList(connectionString As String)

        Try
            'Instantiate the DbContext with the connection string
            Using db As New CustomersDbContext(connectionString)

                'retrive the data from the database
                ClientList = db.Customers.ToList()

            End Using

        Catch ex As Exception

            ' Gestione errori
            Console.WriteLine($"Error during che population of the list with Entity Framework: {ex.Message}")

        End Try
    End Sub



    ''' <summary>
    ''' Check if the are arguments passed from the command line, if so split the connection string and retrive the SQLServerName, DatabaseName, Username and Password then connect to the SQL Server
    ''' </summary>
    Public Function ExternalArgumentsLoginCheck()

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

End Class