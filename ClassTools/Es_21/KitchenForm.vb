Public Class F_Kitchen
    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint_1(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub B_OrderComplete_Click(sender As Object, e As EventArgs) Handles B_OrderComplete.Click

        ' Ottiene il controllo Panel che contiene il bottone
        Dim bottone As Button = DirectCast(sender, Button)
        Dim pannello As Panel = TryCast(bottone.Parent, Panel)

        If pannello IsNot Nothing Then
            ' Rimuove il pannello dal suo contenitore (ad es. il Form)
            pannello.Parent.Controls.Remove(pannello)
            pannello.Dispose()
        End If
    End Sub

End Class