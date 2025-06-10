Imports System.IO

Public Class F_Totem

    Property TotalPrice As Decimal

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
        Dim FindResult() As Control = Me.Controls.Find("FLP_OrderList", True)
        Dim FLP_OrderLIst As FlowLayoutPanel = DirectCast(FindResult(0), FlowLayoutPanel)
        Dim ListOfPanel As List(Of Control) = FLP_OrderLIst.Controls.Cast(Of Control)().ToList()

        For Each Panel As PrefabItem In ListOfPanel
            TotalPrice = TotalPrice + Panel.TotalItemPrice
        Next

        L_TotalPrice.Text = $"Total Price: {TotalPrice.ToString("F2")}€"

    End Sub

    Private Sub B_Order_Click(sender As Object, e As EventArgs) Handles B_Order.Click

        Dim productControl As New PrefabProduct("aaaaaaa", 1D, TotemForm)
        FLP_Hamburgers.Controls.Add(productControl)
        FLP_Hamburgers.Controls.Add(New PrefabProduct("bbbbbbb", 2D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("ccccc", 3D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("ddddd", 4D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("eeeeee", 5D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("ffffff", 6D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("gggggg", 7D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("hhhhhhh", 8D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("iiii", 9D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("lll1lll", 10D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("mm2mmm", 11D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("nn1nnn", 12D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("oo1ooo", 13D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("pp3ppp", 14D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("hh2hhhhh", 8D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("ii4ii", 9D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("ll4llll", 10D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("mm5mmm", 11D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("nn6nnn", 12D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("oo7ooo", 13D, TotemForm))
        FLP_Hamburgers.Controls.Add(New PrefabProduct("pp7ppp", 14D, TotemForm))

    End Sub

    Private Sub FLP_Hamburgers_Paint(sender As Object, e As PaintEventArgs) Handles FLP_Hamburgers.Paint

    End Sub
End Class

