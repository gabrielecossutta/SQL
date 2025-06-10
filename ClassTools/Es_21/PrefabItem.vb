Imports System.Diagnostics.Eventing.Reader
Imports System.Linq.Expressions

Public Class PrefabItem

    Dim f As F_Totem

    Public Property ItemName As String
    Public Property ItemQuantity As Integer
    Public Property ItemBaseprice As Decimal
    Public Property TotalItemPrice As Decimal
    Sub New(ItemName As String, BasePrice As Decimal, TotemForm As F_Totem)

        InitializeComponent()
        
        ItemBaseprice = BasePrice ' prezzo esterno
        TotalItemPrice = BasePrice
        Me.ItemName = ItemName
        L_ItemName.Text = ItemName
        ItemQuantity = 1
        UpdatePrice()

        'controllare se esiste già un item uaguale, se si imprementarlo senza crearne un'altro
    End Sub

    Private Sub B_Add_Click(sender As Object, e As EventArgs) Handles B_Add.Click

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub

    Private Sub B_Remove_Click(sender As Object, e As EventArgs) Handles B_Remove.Click

        ItemQuantity = ItemQuantity - 1

        If Integer.Parse(ItemQuantity) < 1 Then
            UpdatePrice()
            Me.Dispose()
            Return
        End If

        UpdatePrice()

    End Sub

    Private Sub PrefabItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdatePrice()
    End Sub

    Sub IncreaseQuantityByOne()

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub

    Sub UpdatePrice()

        L_ProductQuantity.Text = ItemQuantity
        TotalItemPrice = ItemBaseprice * ItemQuantity
        L_ProductPrice.Text = $"Price: {TotalItemPrice.ToString("F2")}€"
        f = TryCast(Me.ParentForm(), F_Totem)
        'perchè non funziona la prima volta
        If f IsNot Nothing Then
            f.CalculateTotalPrice()
        End If

    End Sub

    Private Sub P_Order_Paint(sender As Object, e As PaintEventArgs) Handles P_Order.Paint

    End Sub
End Class
