Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class DbStructure
    Public Class Summaries
        Public Property IdProduct As Integer
        <Column(TypeName:="date")>
        Public Property RegistrationDate As Date
        Public Property TotalQuantity As Integer
        <Column(TypeName:="money")>
        Public Property TotalPrice As Decimal
    End Class

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

    Public Class OrderDetails
        Public Property IdOrder As Integer
        Public Property IdProduct As Integer
        Public Property OrderQuantity As Integer

    End Class

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

    Public Class AppDbContext
        Inherits DbContext

        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Property Products As DbSet(Of Products)
        Public Property Orders As DbSet(Of Orders)
        Public Property OrderDetails As DbSet(Of OrderDetails)
        Public Property Summaries As DbSet(Of Summaries)

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of Summaries)().
            HasKey(Function(s) New With {s.IdProduct, s.RegistrationDate})

            modelBuilder.Entity(Of OrderDetails)().
            HasKey(Function(od) New With {od.IdOrder, od.IdProduct})

        End Sub

    End Class
End Class