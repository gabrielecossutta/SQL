Imports System.Diagnostics.Eventing.Reader
Imports System.Linq.Expressions

Public Class PrefabItem

    Dim TotemForm As F_Totem

    Public Property IdProduct As String
    Public Property ItemName As String
    Public Property ItemQuantity As Integer
    Public Property Baseprice As Decimal
    Public Property TotalItemPrice As Decimal
    Sub New(IdProduct As Integer, ItemName As String, BasePrice As Decimal)

        InitializeComponent()
        Me.IdProduct = IdProduct
        Me.ItemName = ItemName
        Me.Baseprice = BasePrice
        TotalItemPrice = BasePrice
        L_ItemName.Text = ItemName
        ItemQuantity = 1

    End Sub

    Private Sub B_Add_Click(sender As Object, e As EventArgs) Handles B_Add.Click

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub

    Private Sub B_Remove_Click(sender As Object, e As EventArgs) Handles B_Remove.Click

        ItemQuantity = ItemQuantity - 1

        If Integer.Parse(ItemQuantity) < 1 Then
            UpdatePrice()
            TotemForm.ListaItems.Remove(Me)

            Me.Dispose()
            Return
        End If

        UpdatePrice()

    End Sub

    Private Sub PrefabItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TotemForm = TryCast(Me.ParentForm(), F_Totem)
        TotemForm.ListaItems.Add(Me)
        UpdatePrice()
    End Sub

    Sub IncreaseQuantityByOne()

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub

    Sub UpdatePrice()

        L_ProductQuantity.Text = ItemQuantity
        TotalItemPrice = Baseprice * ItemQuantity
        L_ProductPrice.Text = $"Price: {TotalItemPrice.ToString("F2")}€"
        TotemForm.CalculateTotalPrice()

    End Sub

    Private Sub P_Order_Paint(sender As Object, e As PaintEventArgs) Handles P_Order.Paint

    End Sub
End Class
