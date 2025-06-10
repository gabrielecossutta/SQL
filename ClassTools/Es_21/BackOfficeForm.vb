Public Class F_BackOffice
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
        ' Crea un OpenFileDialog
        Dim ofd As New OpenFileDialog()

        ' Imposta il filtro per visualizzare solo immagini comuni
        ofd.Filter = "Immagini|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofd.Title = "Select an image"

        ' Mostra la finestra di dialogo
        If ofd.ShowDialog() = DialogResult.OK Then
            ' Se l'utente ha scelto un file, caricalo nel PictureBox
            Dim Image As Image = Image.FromFile(ofd.FileName)
        End If
    End Sub

    Private Sub B_StampReport_Click(sender As Object, e As EventArgs) Handles B_StampReport.Click

    End Sub

    Private Sub DTP_ReportDate_ValueChanged(sender As Object, e As EventArgs) Handles DTP_ReportDate.ValueChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub P_AddProduct_Paint(sender As Object, e As PaintEventArgs) Handles P_AddProduct.Paint

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub L_PanelAddProduct_Click(sender As Object, e As EventArgs) Handles L_PanelAddProduct.Click

    End Sub

    Private Sub F_BackOffice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Crea un OpenFileDialog
        Dim ofd As New OpenFileDialog()

        ' Imposta il filtro per visualizzare solo immagini comuni
        ofd.Filter = "Immagini|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofd.Title = "Seleziona una foto"

        ' Mostra la finestra di dialogo
        If ofd.ShowDialog() = DialogResult.OK Then
            ' Se l'utente ha scelto un file, caricalo nel PictureBox
            'PB_ImageProduct.Image = Image.FromFile(ofd.FileName)
            'PB_ImageProduct.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub
End Class