Imports System.Diagnostics.Eventing.Reader
Imports System.Linq.Expressions

Public Class PrefabItem

    Dim TotemForm As F_Totem


    Public Property IdProduct As String
    Public Property ItemName As String
    Public Property ItemQuantity As Integer
    Public Property Baseprice As Decimal

    'TotalPrice BasePrice * ItemQuantity
    Public Property TotalItemPrice As Decimal

    Sub New(IdProduct As Integer, ItemName As String, BasePrice As Decimal)

        'Assign all the base data
        InitializeComponent()
        Me.IdProduct = IdProduct
        Me.ItemName = ItemName
        Me.Baseprice = BasePrice
        TotalItemPrice = BasePrice
        L_ItemName.Text = ItemName
        ItemQuantity = 1

    End Sub


    'Increment the quantity of the item
    Private Sub B_Add_Click(sender As Object, e As EventArgs) Handles B_Add.Click

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub


    'Decrease the quantity of the item
    Private Sub B_Remove_Click(sender As Object, e As EventArgs) Handles B_Remove.Click

        ItemQuantity = ItemQuantity - 1

        'Check if the quantity is less thah 1, if so delete the item from the list and destroy the ItemForm
        If Integer.Parse(ItemQuantity) < 1 Then
            UpdatePrice()
            TotemForm.ListaItems.Remove(Me)
            Me.Dispose()
            Return
        End If

        UpdatePrice()

    End Sub

    'When loaded 
    Private Sub PrefabItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Add the itemForm in the TotemForm
        TotemForm = TryCast(Me.ParentForm(), F_Totem)
        TotemForm.ListaItems.Add(Me)
        UpdatePrice()

    End Sub

    ''' <summary>
    ''' Increment quantity by one when there is already that product in the list
    ''' </summary>
    Sub IncreaseQuantityByOne()

        ItemQuantity = ItemQuantity + 1
        UpdatePrice()

    End Sub

    ''' <summary>
    ''' Update the total price of the Item
    ''' </summary>
    Sub UpdatePrice()

        L_ProductQuantity.Text = ItemQuantity
        TotalItemPrice = Baseprice * ItemQuantity
        L_ProductPrice.Text = $"Price: {TotalItemPrice.ToString("F2")}€"

        'Update the total price in the TotemForm
        TotemForm.CalculateTotalPrice()

    End Sub

End Class
