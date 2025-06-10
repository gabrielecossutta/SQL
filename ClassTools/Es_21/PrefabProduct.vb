Public Class PrefabProduct

    Dim BasePrice As Decimal
    Dim ImageProduct As Image
    Dim NameProduct As String
    Dim TotemForm As F_Totem
    Sub New(NameProduct As String, BasePrice As Decimal, TotemForm As F_Totem) 'NameProduct As String, BasePrice As Decimal, ImageProduct As Image

        InitializeComponent()
        Me.BasePrice = BasePrice
        Me.NameProduct = NameProduct
        Me.TotemForm = TotemForm
        L_PriceProduct.Text = BasePrice
        L_ProductName.Text = NameProduct

    End Sub
    Private Sub PB_ImageProduct_Click(sender As Object, e As EventArgs) Handles PB_ImageProduct.Click

        Dim FindResult() As Control = Me.ParentForm.Controls.Find("FLP_OrderList", True)
        Dim FLP_OrderLIst As FlowLayoutPanel = DirectCast(FindResult(0), FlowLayoutPanel)
        Dim ListOfPanel As List(Of Control) = FLP_OrderLIst.Controls.Cast(Of Control)().ToList()
        Dim needToBeCreated As Boolean = False

        For Each Panel As PrefabItem In ListOfPanel

            If (Panel.ItemName = NameProduct) Then
                Panel.IncreaseQuantityByOne()
                Return
            Else
                needToBeCreated = True
            End If
        Next

        If needToBeCreated Or ListOfPanel.Count < 1 Then
            FLP_OrderLIst.Controls.Add(New PrefabItem(NameProduct, BasePrice, TotemForm))
        End If

    End Sub

    Private Sub P_HamburgersProducts_Paint(sender As Object, e As PaintEventArgs) Handles P_HamburgersProducts.Paint

    End Sub
End Class
