Imports System.IO
Imports System.Drawing
Imports System.Runtime.Remoting.Contexts

Public Class PrefabProduct

    Dim BasePrice As Decimal
    Dim ImageProduct As Image
    Dim NameProduct As String
    Dim IdProduct As Integer
    Dim ListOfPanel As List(Of Control)

    Sub New(IdProduct As Integer, NameProduct As String, BasePrice As Decimal, ImageProduct As Byte())

        'Assign all the Base Data
        InitializeComponent()
        Me.BasePrice = BasePrice
        Me.NameProduct = NameProduct
        Me.IdProduct = IdProduct

        'Assign all the visual data
        L_PriceProduct.Text = BasePrice.ToString("F2")
        L_ProductName.Text = NameProduct

        'Convert the byte array into an image and assign it to the PictureBox
        If ImageProduct IsNot Nothing Then
            Dim ImageConverted = ByteArrayToImage(ImageProduct)
            PB_ImageProduct.Image = ImageConverted
            PB_ImageProduct.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    ''' <summary>
    ''' Convert the byte() into image
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns></returns>
    Private Function ByteArrayToImage(bytes() As Byte) As System.Drawing.Image

        Using ms As New MemoryStream(bytes)
            Return System.Drawing.Image.FromStream(ms)
        End Using

    End Function

    'When the user click on the PictureBox, add that item into the cart
    Private Sub PB_ImageProduct_Click(sender As Object, e As EventArgs) Handles PB_ImageProduct.Click
        AddItemIntoCart()
    End Sub

    ''' <summary>
    ''' Add the item into the cart
    ''' </summary>
    Public Sub AddItemIntoCart()

        Dim needToBeCreated As Boolean = False

        'Find all the ItemForm in the FlowLayoutPanel
        Dim FindResult() As Control = Me.ParentForm.Controls.Find("FLP_OrderList", True)
        Dim FLP_OrderLIst As FlowLayoutPanel = DirectCast(FindResult(0), FlowLayoutPanel)
        ListOfPanel = FLP_OrderLIst.Controls.Cast(Of Control)().ToList()

        'Check if there is already that item in the list, if so only increase the quantity
        For Each Panel As PrefabItem In ListOfPanel
            If (Panel.ItemName = NameProduct) Then
                Panel.IncreaseQuantityByOne()
                Return
            Else
                needToBeCreated = True
            End If
        Next

        'Create a new ItemForm
        If needToBeCreated Or ListOfPanel.Count < 1 Then
            Dim NewPrefab As PrefabItem = New PrefabItem(IdProduct, NameProduct, BasePrice)
            FLP_OrderLIst.Controls.Add(NewPrefab)
        End If

    End Sub

End Class