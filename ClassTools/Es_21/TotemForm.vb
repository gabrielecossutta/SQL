Imports System.IO
Imports System.Data.Entity
Imports System.Data.Entity.Core.Metadata.Edm
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Security.Cryptography
Imports Es_21.DbStructure
Imports System.ComponentModel.DataAnnotations.Schema
Public Class F_Totem

    Property TotalPrice As Decimal

    Property ListaItems As New List(Of PrefabItem)

    Public Property TotemForm As F_Totem
    Sub New()

        InitializeComponent()
        TotemForm = Me

    End Sub

    Public Sub CalculateTotalPrice()

        TotalPrice = 0
        For Each Panel As PrefabItem In ListaItems
            TotalPrice = TotalPrice + Panel.TotalItemPrice
        Next
        L_TotalPrice.Text = $"Total Price: {TotalPrice.ToString("F2")}€"

    End Sub

    Private Sub B_Order_Click(sender As Object, e As EventArgs) Handles B_Order.Click
        If ListaItems.Count < 1 Then
            Return
        End If
        Using context As New DbStructure.AppDbContext("Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;")
            Dim Order As New DbStructure.Orders With
            {
            .OrderDate = DateAndTime.Now,
            .OrderCompleted = False,
            .OrderInsertDate = DateAndTime.Now,
            .OrderInsertUser = "Totem"
             }
            context.Orders.Add(Order)
            context.SaveChanges()

            For Each Item As PrefabItem In ListaItems
                Dim OrderDetails As New DbStructure.OrderDetails With
                  {
                .IdOrder = Order.IdOrders,
                .IdProduct = Item.IdProduct,
                .OrderQuantity = Item.ItemQuantity
                  }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()

                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = Item.IdProduct)

                If existingSummary IsNot Nothing Then
                    existingSummary.TotalQuantity += Item.ItemQuantity
                    existingSummary.TotalPrice += Item.ItemQuantity * Item.Baseprice

                Else
                    Dim newSummary As New DbStructure.Summaries With {
                        .IdProduct = Item.IdProduct,
                        .RegistrationDate = Date.Now,
                        .TotalQuantity = Item.ItemQuantity,
                        .TotalPrice = Item.ItemQuantity * Item.Baseprice
                    }
                    context.Summaries.Add(newSummary)
                End If

                context.SaveChanges()
            Next



        End Using
    End Sub


    Private Sub PopulateForm()
        Using context As New DbStructure.AppDbContext("Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;")
            Dim products = context.Products.ToList()
            For Each product In products
                Select Case product.ProductCategory
                    Case "Hamburgers"
                        FLP_Hamburgers.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Appetizers"
                        FLP_Appetizers.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Drinks"
                        FLP_Drinks.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Dessert"
                        FLP_Dessert.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Sauce"
                        FLP_Sauce.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                End Select
            Next
        End Using
    End Sub

    Private Sub F_Totem_Load(sender As Object, e As EventArgs) Handles Me.Load
        PopulateForm()
    End Sub

End Class

