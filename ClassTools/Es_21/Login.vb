Public Class Login
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Get the arguments from the command line
        Dim args As String() = Environment.GetCommandLineArgs()

        'Check if the arguments are more than 1
        If args.Count > 1 Then

            Dim x = 0

            For Each arg As String In args

                If Integer.TryParse(arg, x) Then

                    Select Case x
                        Case 1
                            Dim TotemForm As New F_Totem
                            TotemForm.ShowDialog()
                        Case 2
                            Dim KitchenForm As New F_Kitchen
                            KitchenForm.ShowDialog()

                        Case 3
                            Dim BackOfficeForm As New F_BackOffice
                            BackOfficeForm.ShowDialog()

                    End Select


                    Exit For

                End If

            Next

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles B_Totem.Click
        Dim TotemForm As New F_Totem
        TotemForm.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles B_Kitchen.Click
        Dim KitchenForm As New F_Kitchen
        KitchenForm.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles B_BackOffice.Click
        Dim BackOfficeForm As New F_BackOffice
        BackOfficeForm.ShowDialog()
    End Sub
End Class