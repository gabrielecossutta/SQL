Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.IO
Imports Newtonsoft.Json

Public Class DbStructure
    ''' <summary>
    ''' Reppresents the summary of products sold, including total quantity and price.
    ''' </summary>
    Public Class Summaries
        Public Property IdProduct As Integer
        <Column(TypeName:="date")>
        Public Property RegistrationDate As Date
        Public Property TotalQuantity As Integer
        <Column(TypeName:="money")>
        Public Property TotalPrice As Decimal
    End Class

    ''' <summary>
    ''' Represents an order in the system
    ''' </summary>
    Public Class Orders
        <Key>
        Public Property IdOrders As Integer
        <Column(TypeName:="date")>
        Public Property OrderDate As Date
        Public Property OrderCompleted As Boolean
        <Column(TypeName:="date")>
        Public Property OrderInsertDate As Date
        Public Property OrderInsertUser As String
        <Column(TypeName:="date")>
        Public Property OrderModificationDate As Date?
        Public Property OrderModificationUser As String
    End Class

    ''' <summary>
    ''' Represents the details of an order
    ''' </summary>
    Public Class OrderDetails
        Public Property IdOrder As Integer
        Public Property IdProduct As Integer
        Public Property OrderQuantity As Integer

    End Class

    ''' <summary>
    ''' Represents a copy of an order, used for tracking the cart
    ''' </summary>
    Public Class CopyOrders
        <Key>
        Public Property IdOrders As Integer
        <Column(TypeName:="date")>
        Public Property OrderDate As Date
        Public Property OrderCompleted As Boolean
        <Column(TypeName:="date")>
        Public Property OrderInsertDate As Date
        Public Property OrderInsertUser As String
        <Column(TypeName:="date")>
        Public Property OrderModificationDate As Date?
        Public Property OrderModificationUser As String
    End Class

    ''' <summary>
    ''' Represents the details of a copied order, used for tracking the cart items
    ''' </summary>
    Public Class CopyOrderDetails
        Public Property IdOrder As Integer
        Public Property IdProduct As Integer
        Public Property OrderQuantity As Integer

    End Class

    ''' <summary>
    ''' Represents a product in the system
    ''' </summary>
    Public Class Products
        <Key>
        Public Property IdProduct As Integer
        <Column("ProductCaterogy")>
        Public Property ProductCategory As String
        Public Property ProductName As String
        <Column(TypeName:="money")>
        Public Property ProductPrice As Decimal
        <Column(TypeName:="varbinary(max)")>
        Public Property ProductPicture As Byte()
        Public Property ProductDescription As String
        <Column(TypeName:="date")>
        Public Property ProductInsertDate As Date
        <Column(TypeName:="date")>
        Public Property ProductModificationDate As Date?
        <Column("ProductInserUser")>
        Public Property ProductInsertUser As String
        Public Property ProductModificationUser As String
    End Class

    ''' <summary>
    ''' Database context for the application
    ''' </summary>
    Public Class MyDbContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(GetConnectionString())
        End Sub

        ' Retrieves the connection string from a JSON file
        Private Shared Function GetConnectionString() As String
            Dim jsonPath As String = "Es21.json"
            Dim jsonText As String = File.ReadAllText(jsonPath)
            Dim config As ConnectionString = JsonConvert.DeserializeObject(Of ConnectionString)(jsonText)

            Return $"Server={config.SQLServerName};Database={config.DatabaseName};User={config.UserName};Password={config.Password};"
        End Function

        'Db sets for the database tables
        Public Property Products As DbSet(Of Products)
        Public Property Orders As DbSet(Of Orders)
        Public Property OrderDetails As DbSet(Of OrderDetails)
        Public Property CopyOrders As DbSet(Of CopyOrders)
        Public Property CopyOrderDetails As DbSet(Of CopyOrderDetails)
        Public Property Summaries As DbSet(Of Summaries)

        'Configure the keys for the tables
        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of Summaries)().HasKey(Function(s) New With {s.IdProduct, s.RegistrationDate})

            modelBuilder.Entity(Of OrderDetails)().HasKey(Function(od) New With {od.IdOrder, od.IdProduct})

            modelBuilder.Entity(Of CopyOrderDetails)().HasKey(Function(cod) New With {cod.IdOrder, cod.IdProduct})

        End Sub

    End Class
End Class

''' <summary>
''' Represents the connection string configuration
''' </summary>
Public Class ConnectionString
    Public Property SQLServerName As String
    Public Property DatabaseName As String
    Public Property UserName As String
    Public Property Password As String
End Class