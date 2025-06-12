Imports System.IO
Imports System.Drawing

Public Class PrefabProduct

    Dim BasePrice As Decimal
    Dim ImageProduct As Image
    Dim NameProduct As String
    Dim IdProduct As Integer
    Sub New(IdProduct As Integer, NameProduct As String, BasePrice As Decimal, ImageProduct As Byte()) 'NameProduct As String, BasePrice As Decimal, ImageProduct As Image

        InitializeComponent()
        Me.BasePrice = BasePrice
        Me.NameProduct = NameProduct
        Me.IdProduct = IdProduct
        L_PriceProduct.Text = BasePrice.ToString("F2")
        L_ProductName.Text = NameProduct
        If ImageProduct IsNot Nothing Then
            Dim _image = ByteArrayToImage(ImageProduct)
            PB_ImageProduct.Image = _image
            PB_ImageProduct.SizeMode = PictureBoxSizeMode.StretchImage

        End If
    End Sub
    Private Function ByteArrayToImage(bytes() As Byte) As System.Drawing.Image
        Using ms As New MemoryStream(bytes)
            Return System.Drawing.Image.FromStream(ms)
        End Using
    End Function
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
            FLP_OrderLIst.Controls.Add(New PrefabItem(IdProduct, NameProduct, BasePrice))
        End If

    End Sub

    Private Sub P_HamburgersProducts_Paint(sender As Object, e As PaintEventArgs) Handles P_HamburgersProducts.Paint

    End Sub
End Class
