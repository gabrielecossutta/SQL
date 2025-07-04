Imports System.Drawing.Imaging
Imports System.IO
Imports ClassTools
Imports System.Drawing
Imports Es_21.DbStructure
Imports ClosedXML.Excel
Public Class F_BackOffice

    Private Sub PB_Product_Click(sender As Object, e As EventArgs) Handles PB_Product.Click

        Try
            'Create the Dialog to search the file and assign that to the PictureBox
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Immagini|*.png;"
            ofd.Title = "Select a photo"
            If ofd.ShowDialog() = DialogResult.OK Then

                PB_Product.Image = Image.FromFile(ofd.FileName)
                PB_Product.SizeMode = PictureBoxSizeMode.StretchImage
            End If
        Catch ex As Exception
            MessageBox.Show("Choose a PNG image")
        End Try

    End Sub

    Private Sub B_AddNewProduct_Click(sender As Object, e As EventArgs) Handles B_AddNewProduct.Click

        'Check if the fields are not empty
        If CB_Category.Text = "" Then
            MessageBox.Show("Please select Category")
            Return

        ElseIf TB_Name.Text = "" Then
            MessageBox.Show("Please insert a Name")
            Return

        ElseIf TB_Price.Text = "" Then
            MessageBox.Show("Please insert a Price")
            Return

        ElseIf PB_Product.Image Is Nothing Then
            MessageBox.Show("Please select a product image")
            Return

        End If

        Dim img As Image = PB_Product.Image
        Using ms As New MemoryStream()

            ' Save the image to the stream in PNG format (you can change the format)
            img.Save(ms, ImageFormat.Png)

            ' Convert the stream to a byte array
            Dim imageBytes As Byte() = ms.ToArray()
            Using context As New DbStructure.MyDbContext()
                Dim NewProduct As New DbStructure.Products With
                    {
                .ProductCategory = CB_Category.Text,
                .ProductName = TB_Name.Text,
                .ProductPrice = TB_Price.Text,
                .ProductPicture = imageBytes,
                .ProductDescription = TB_Description.Text,
                .ProductInsertDate = Date.Now,
                .ProductInsertUser = "Gabriele"
                }
                context.Products.Add(NewProduct)
                context.SaveChanges()

                CB_Category.Text = ""
                TB_Name.Text = ""
                TB_Price.Text = ""
                TB_Description.Text = ""
                PB_Product.Image = Nothing
            End Using
        End Using

    End Sub

    Private Sub B_StampReport_Click(sender As Object, e As EventArgs) Handles B_StampReport.Click

        ConvertInHtml()
        ConverInExcel()

    End Sub

    Private Sub ConvertInHtml()

        Dim ErrorMessage As String = ""
        Dim IsOperationCompleted As Boolean = False
        Using context As New DbStructure.MyDbContext()

            Dim Summary = context.Summaries.Where(Function(s) s.RegistrationDate = DTP_ReportDate.Value.Date).ToList()
            Dim stringHTML As String = "<html><head><title>Summary</title></head><body><table border='1'><tr>"

            'Create the table header with the properties of the summary class
            Dim props = GetType(DbStructure.Summaries).GetProperties()
            For Each prop In props
                stringHTML += $"<th>{prop.Name}</th>"
            Next
            stringHTML += "</tr>"

            'Create the table rows with the data from the summary
            For Each utente In Summary
                stringHTML += "<tr>"
                For Each prop In props
                    Dim value = prop.GetValue(utente, Nothing)
                    If TypeOf value Is String Then
                        stringHTML += $"<td>{value.ToString().TrimEnd()}</td>"
                    Else
                        stringHTML += $"<td>{value}</td>"
                    End If
                Next
                stringHTML += "</tr>"
            Next
            stringHTML += "</table></body></html>"

            'Write the HTML string to a file in the specified web path
            Utils.WriteAFile(stringHTML, "WEB\McDonald", "Summary", "html", False)
            Process.Start("C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\WEB\McDonald\Summary.html")

        End Using
    End Sub



    Private Sub ConverInExcel()
        Using context As New DbStructure.MyDbContext()

            Dim summaryList = context.Summaries.Where(Function(s) s.RegistrationDate = DTP_ReportDate.Value.Date).ToList()

            Dim filePath = "C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\EXL\Summaries_" & DTP_ReportDate.Value.Day.ToString("00") & "_" & DTP_ReportDate.Value.Month.ToString("00") & "_" & DTP_ReportDate.Value.Year.ToString() & ".xlsx"

            Dim totalPrice As Decimal = 0
            'Create a workbook Excel
            Using wb As New XLWorkbook()

                'Add a page with columns
                Dim ws = wb.Worksheets.Add("Report Venduto")
                ws.Cell(1, 1).Value = "IdProduct"
                ws.Cell(1, 2).Value = "RegistrationDate"
                ws.Cell(1, 3).Value = "TotalQuantity"
                ws.Cell(1, 4).Value = "TotalPrice"

                'Insert the data in the second row
                Dim row = 2
                For Each s In summaryList
                    ws.Cell(row, 1).Value = s.IdProduct
                    ws.Cell(row, 2).Value = s.RegistrationDate.ToShortDateString()
                    ws.Cell(row, 3).Value = s.TotalQuantity
                    ws.Cell(row, 4).Value = s.TotalPrice
                    totalPrice += s.TotalPrice
                    row += 1
                Next

                'TotalPrice
                ws.Cell(row, 3).Value = "Total: "
                ws.Cell(row, 4).Value = totalPrice

                'Auto-fit
                ws.Columns().AdjustToContents()

                'Save the file
                wb.SaveAs(filePath)
            End Using
        End Using

    End Sub

    ''' <summary>
    ''' This method ensure that the user can only enter digits, with a single coma for the decimal separator
    ''' </summary>
    Private Sub TB_Price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TB_Price.KeyPress


        If Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar) Then

        ElseIf (e.KeyChar = "," OrElse e.KeyChar = ".") AndAlso Not TB_Price.Text.Contains(",") AndAlso Not TB_Price.Text.Contains(".") Then
            e.KeyChar = ","c
        Else
            e.Handled = True
        End If

    End Sub

End Class