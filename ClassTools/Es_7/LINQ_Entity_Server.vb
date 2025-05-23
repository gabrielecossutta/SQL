Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms

Module LINQ_Entity_Server

    Sub Main()

        'Populate the list of clients using Entity Framework
        PopulateList(ExternalArgumentsLoginCheck())

        SelectFunction()

    End Sub


    Dim db As CustomersDbContext

    Dim connectionString As String = ExternalArgumentsLoginCheck()

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


    ''' <summary>
    ''' Funzione che popola la lista di clienti usando Entity Framework.
    ''' Aggiunto anche il debug per monitorare eventuali problemi.
    ''' </summary>
    Private Sub PopulateList(connectionString As String)

        db = New CustomersDbContext(connectionString)

    End Sub



    ''' <summary>
    ''' Check if the are arguments passed from the command line, if so split the connection string and retrive the SQLServerName, DatabaseName, Username and Password then connect to the SQL Server
    ''' </summary>
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

    Private Sub SelectFunction()

        While True

            Console.WriteLine("0:StampCompleteList - 1:OrderListBy - 2:SearchByPrimaryKey - 3:SearchByValueInProperties - 4:StampSingleProperty - 5:CountNumberOfElements - 6:CheckSpecificValueInProperty - 7:GroupByPropertys - 8:Quit")

            Select Case Console.ReadLine()

                Case 0
                    StampCompleteList()

                Case 1
                    OrderListBy()

                Case 2
                    SearchByPrimaryKey()

                Case 3
                    SearchByValueInProperties()

                Case 4
                    StampSingleProperty()

                Case 5
                    CountNumberOfElements()

                Case 6
                    CheckSpecificValueInProperty()

                Case 7
                    GroupByPropertys()

                Case 8
                    Return

                Case Else
                    Console.WriteLine("Invalid option")

            End Select

            Console.ReadKey()
            Console.Clear()

        End While

    End Sub



#Region "FUNCTIONS"
    '''<summary>
    ''' All the functions are in this region
    ''' </summary>
    Private Sub StampCompleteList()

        For Each client As String In db.Customers.AsEnumerable().Select(Function(c) $"{c.CustomerID},{c.CompanyName},{c.ContactName},{c.ContactTitle},{c.Address},{c.City},{c.Region},{c.PostalCode},{c.Country},{c.Phone},{c.Fax}")

            Console.WriteLine(client)

        Next

    End Sub


    ''' <summary>
    ''' Order the list by the property name using LINQ
    ''' </summary>
    Private Sub OrderListBy()

        'Get the name of the property to order by from the user and check if the property exists
        Console.WriteLine("Order by: CustomerID - CompanyName - ContactName - ContactTitle - Address - City - Region - PostalCode - Country - Phone - Fax")
        Dim criterion As String = Console.ReadLine()
        Dim propertyToSearch = GetType(Client).GetProperty(criterion)

        If propertyToSearch Is Nothing Then

            Console.WriteLine($"Property {criterion} doesn't exist")
            Return

        End If

        'Order the list by the property name
        For Each client As String In db.Customers.AsEnumerable().OrderBy(Function(c) propertyToSearch.GetValue(c)).Select(Function(c) $"{c.CustomerID},{c.CompanyName},{c.ContactName},{c.ContactTitle},{c.Address},{c.City},{c.Region},{c.PostalCode},{c.Country},{c.Phone},{c.Fax}")

            Console.WriteLine(client)

        Next

    End Sub

    ''' <summary>
    ''' Search for a client by primary key and print the client if found
    ''' </summary>
    Private Sub SearchByPrimaryKey()

        'Get the primary key from the user
        Console.WriteLine("Insert the primary key to search for:")
        Dim primaryKey As String = Console.ReadLine()

        'Search for the client with the primary key and Check if the client exists
        Dim client = db.Customers.AsEnumerable().FirstOrDefault(Function(c) c.CustomerID = primaryKey)

        If client IsNot Nothing Then

            For Each eachProperty In client.GetType().GetProperties()

                Console.WriteLine($"{eachProperty.Name}: {eachProperty.GetValue(client)}")

            Next

        Else

            Console.WriteLine("Client not found")

        End If

    End Sub

    ''' <summary>
    ''' Search for a value in the properties of the client and print the client if found
    ''' </summary>
    Private Sub SearchByValueInProperties()

        'Get the value to search for from the user
        Console.WriteLine("Insert the value to search for:")
        Dim valueToSearch = Console.ReadLine()

        'Get the property to search by from the user and check if it exists
        Console.WriteLine("Search value by: CustomerID - CompanyName - ContactName - ContactTitle - Address - City - Region - PostalCode - Country - Phone - Fax")
        Dim criterion As String = Console.ReadLine()
        Dim propertyToSearch = GetType(Client).GetProperty(criterion)
        If propertyToSearch Is Nothing Then

            Console.WriteLine($"Property {criterion} doesn't exist")
            Return

        End If

        'Search for the first client with the value in the property
        Dim client = db.Customers.AsEnumerable().FirstOrDefault(Function(c) c.GetType().GetProperty(criterion).GetValue(c).ToString() = valueToSearch)

        'Check if the client exists
        If client IsNot Nothing Then

            For Each eachProperty In client.GetType().GetProperties()

                Console.WriteLine($"{eachProperty.Name}: {eachProperty.GetValue(client)}")

            Next

        Else

            Console.WriteLine("Client not found")

        End If

    End Sub

    ''' <summary>
    ''' Scroll through the list and print a single property of every element
    ''' </summary>
    Private Sub StampSingleProperty()

        'Get the property to print from the user and check if it exists
        Console.WriteLine("Stamp Single Property: CustomerID - CompanyName - ContactName - ContactTitle - Address - City - Region - PostalCode - Country - Phone - Fax")
        Dim criterion As String = Console.ReadLine()
        Dim propertyToSearch = GetType(Client).GetProperty(criterion)
        If propertyToSearch Is Nothing Then
            Console.WriteLine("Property not found.")
            Return
        End If

        'scoll through the list and print the property
        For Each client In db.Customers

            Console.WriteLine(propertyToSearch.GetValue(client))

        Next

    End Sub

    ''' <summary>
    ''' Count the number of elements in the list using LINQ
    ''' </summary>
    Private Sub CountNumberOfElements()

        Console.WriteLine($"Number of elements in the list: {db.Customers.Count()}")

    End Sub

    ''' <summary>
    ''' Check if there is an element in the list that contains a specific value in a specific property
    ''' </summary>
    Private Sub CheckSpecificValueInProperty()

        'Get the value to search for from the user
        Console.WriteLine("Insert the value to search for:")
        Dim valueToSearch = Console.ReadLine()

        'Get the property to search by from the user and check if it exists
        Console.WriteLine("Search value by: CustomerID - CompanyName - ContactName - ContactTitle - Address - City - Region - PostalCode - Country - Phone - Fax")
        Dim criterion As String = Console.ReadLine()
        Dim propertyToSearch = GetType(Client).GetProperty(criterion)
        If propertyToSearch Is Nothing Then

            Console.WriteLine($"Property {criterion} doesn't exist")

            Return

        End If

        'Search for the first client with the value in the property
        Dim client = db.Customers.AsEnumerable().FirstOrDefault(Function(c) c.GetType().GetProperty(criterion).GetValue(c).ToString() = valueToSearch)

        'check if the client exists
        If client Is Nothing Then

            Console.WriteLine("Client not found")

            Return

        End If

        Console.WriteLine("Client Found")

    End Sub

    ''' <summary>
    ''' Scroll through the grouped list by one or more properties
    ''' </summary>
    Private Sub GroupByPropertys()

        'Get the number of propertys to group by from the user and check if it is between 1 and 11
        Console.WriteLine("Insert the number of propertys to group by:")
        Dim StringNumberOfPropertys As String = Console.ReadLine()
        Dim numberOfPropertys As Integer = 0
        Integer.TryParse(StringNumberOfPropertys, numberOfPropertys)
        If numberOfPropertys < 1 Or numberOfPropertys > 11 Then

            Console.WriteLine("Number of propertys must be between 1 and 11")
            Return

        End If

        'Get the propertys to group by from the user and check if they exist
        Dim propertysToGroupBy As New List(Of String)()
        Dim propertysToGroupByString As String = ""
        For i As Integer = 0 To numberOfPropertys - 1

            Console.WriteLine("Insert the property to group by: CustomerID - CompanyName - ContactName - ContactTitle - Address - City - Region - PostalCode - Country - Phone - Fax")
            Dim propertyToGroupBy As String = Console.ReadLine()
            Dim propertyToSearch = GetType(Client).GetProperty(propertyToGroupBy)
            If propertyToSearch Is Nothing Then

                Console.WriteLine($"Property {propertyToGroupBy} doesn't exist")
                Continue For

            End If

            propertysToGroupBy.Add(propertyToGroupBy)
            propertysToGroupByString += $"{propertyToGroupBy},"

        Next

        'Remove the last comma from the string
        propertysToGroupByString = propertysToGroupByString.TrimEnd(","c)

        'Group the list by the propertys
        Dim groupedList = db.Customers.AsEnumerable().GroupBy(Function(c) String.Join(",", propertysToGroupBy.Select(Function(p) c.GetType().GetProperty(p).GetValue(c))))

        'Print the grouped list
        For Each group In groupedList

            Console.WriteLine(Environment.NewLine)
            Console.WriteLine($"Group: {group.Key}")

            For Each client In group

                Dim clientString = String.Join(",", propertysToGroupBy.Select(Function(p) $"{p}: {client.GetType().GetProperty(p).GetValue(client)}"))
                Console.WriteLine(clientString)

            Next

        Next

    End Sub

#End Region

End Module