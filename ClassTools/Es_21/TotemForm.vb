Imports System.IO
Imports System.Data.Entity
Imports System.Data.Entity.Core.Metadata.Edm
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common
Public Class F_Totem

    Property TotalPrice As Decimal

    Property ListaItems As New List(Of PrefabItem)

    Public Property TotemForm As F_Totem
    Sub New()

        InitializeComponent()
        TotemForm = Me

    End Sub
    Sub CreateProduct()
        'imagine
        'nome
        'prezzo
        'Dim productControl As New PrefabProduct()
        'popolo
        'FLP_Hamburgers.Controls.Add(productControl)

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
        Using ctx As New MyDbContext()
            Using conn As DbConnection = ctx.Database.Connection
                If conn.State = ConnectionState.Closed Then conn.Open()

                Using transaction = conn.BeginTransaction()
                    Try
                        Dim orderId As Integer

                        ' Inserimento dell'ordine
                        Using cmdInsertOrder As DbCommand = conn.CreateCommand()
                            cmdInsertOrder.Transaction = transaction
                            cmdInsertOrder.CommandText = "INSERT INTO Orders (OrderDate, OrderCompleted, OrderInsertDate, OrderInsertUser) VALUES (@date, @completed, @insertDate, @user); SELECT CAST(SCOPE_IDENTITY() AS INT)"
                            cmdInsertOrder.Parameters.Add(New SqlParameter("@date", Date.Today))
                            cmdInsertOrder.Parameters.Add(New SqlParameter("@completed", False))
                            cmdInsertOrder.Parameters.Add(New SqlParameter("@insertDate", Date.Today))
                            cmdInsertOrder.Parameters.Add(New SqlParameter("@user", "Totem"))

                            orderId = Convert.ToInt32(cmdInsertOrder.ExecuteScalar())
                        End Using

                        ' Inserimento dei dettagli dell'ordine
                        For Each Panel As PrefabItem In ListaItems

                            Using cmdInsertDetail As DbCommand = conn.CreateCommand()
                                cmdInsertDetail.Transaction = transaction
                                cmdInsertDetail.CommandText = "INSERT INTO OrderDetails (IdOrder, IdProduct, OrderQuantity) VALUES (@idOrder, @idProduct, @quantity)"
                                cmdInsertDetail.Parameters.Add(New SqlParameter("@idOrder", orderId))
                                cmdInsertDetail.Parameters.Add(New SqlParameter("@idProduct", Panel.IdProduct))
                                cmdInsertDetail.Parameters.Add(New SqlParameter("@quantity", Panel.ItemQuantity))

                                cmdInsertDetail.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                        MessageBox.Show("Thanks!")

                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Something went wrong: " & ex.Message)
                    End Try
                End Using
            End Using
        End Using
    End Sub


    Private Sub StampaColonneConEntity(nomeTabella As String)
        Using ctx As New MyDbContext()
            Using conn As DbConnection = ctx.Database.Connection
                If conn.State = ConnectionState.Closed Then conn.Open()

                Using cmd As DbCommand = conn.CreateCommand()
                    cmd.CommandText = $"SELECT * FROM [{nomeTabella}]"

                    Using reader As DbDataReader = cmd.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader) ' Carica i dati reali

                        For Each row As DataRow In dataTable.Rows
                            Select Case row(1)
                                Case "Hamburgers"

                                    FLP_Hamburgers.Controls.Add(New PrefabProduct(row(0), row(2), row(3), row(4)))

                                Case "Appetizers"
                                    FLP_Appetizers.Controls.Add(New PrefabProduct(row(0), row(2), row(3), row(4)))

                                Case "Drinks"
                                    FLP_Drinks.Controls.Add(New PrefabProduct(row(0), row(2), row(3), row(4)))

                                Case "Dessert"
                                    FLP_Dessert.Controls.Add(New PrefabProduct(row(0), row(2), row(3), row(4)))

                                Case "Sauce"
                                    FLP_Sauce.Controls.Add(New PrefabProduct(row(0), row(2), row(3), row(4)))

                            End Select

                        Next
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Public Class MyDbContext
        Inherits DbContext

        Public Sub New()
            MyBase.New("Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;")
        End Sub

    End Class

    Private Sub F_Totem_Load(sender As Object, e As EventArgs) Handles Me.Load

        StampaColonneConEntity("Products")

    End Sub

    Private Sub FLP_OrderList_Paint(sender As Object, e As PaintEventArgs) Handles FLP_OrderList.Paint

    End Sub
End Class

