Imports System.Drawing.Imaging
Imports System.IO
Imports ClassTools
Imports System.Drawing
Imports ClosedXML.Excel
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Rendering.IO
Imports GrapeCity.ActiveReports.Export.Pdf.Page
Imports GrapeCity.ActiveReports
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

        CreateReport()
        ConverInExcel()
        ConvertInHtml()

    End Sub

    Private Sub ConvertInHtml()

        Dim ErrorMessage As String = ""
        Dim IsOperationCompleted As Boolean = False
        Using context As New DbStructure.MyDbContext()

            Dim Summary = context.Summaries.Where(Function(s) s.RegistrationDate >= DTP_Start.Value.Date AndAlso s.RegistrationDate <= DTP_End.Value.Date).ToList()
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
            Process.Start("C:\Users\Gabriele\Desktop\SQL\ClassTools\WEB\McDonald\Summary.html")

        End Using
    End Sub



    Private Sub ConverInExcel()
        Using context As New DbStructure.MyDbContext()

            Dim summaryList = context.Summaries.Where(Function(s) s.RegistrationDate >= DTP_Start.Value.Date AndAlso s.RegistrationDate <= DTP_End.Value.Date).ToList()

            Dim filePath = "C:\Users\Gabriele\Desktop\SQL\ClassTools\EXE\EXL\" & $"From-{DTP_Start.Value.Day}-{DTP_Start.Value.Month}-{DTP_Start.Value.Year}-To-{DTP_End.Value.Day}-{DTP_End.Value.Month}-{DTP_End.Value.Year}.xlsx"

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
                Dim PrevData As Integer
                Dim skip = False
                For Each s In summaryList

                    'If it's the first row assign the preData
                    If row = 2 Then
                        PrevData = s.IdProduct
                        skip = True
                    End If

                    'If it's a different product skip a row 
                    If Not PrevData = s.IdProduct Then
                        PrevData = s.IdProduct
                        skip = True
                        row += 1
                    End If

                    ws.Cell(row, 1).Value = ""
                    ws.Cell(row, 2).Value = s.RegistrationDate.ToShortDateString()
                    ws.Cell(row, 3).Value = s.TotalQuantity
                    ws.Cell(row, 4).Value = s.TotalPrice

                    '
                    If skip = True Then
                        ws.Cell(row, 1).Value = s.IdProduct
                        skip = False
                    End If

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

                Process.Start(filePath)
            End Using
        End Using

    End Sub

    ''' <summary>
    ''' Create the report rdlx and rdx
    ''' </summary>
    Private Sub CreateReport()

        'Load the report in the viewer
        Dim report As New SectionReport(DTP_Start.Value.Date.ToString("dd/MM/yyyy"), DTP_End.Value.Date.ToString("dd/MM/yyyy"))
        Viewer1.LoadDocument(report)
        Viewer1.Dock = DockStyle.Fill


        Dim reportPath As String = "C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_28\Report.rdlx"
        Dim report2 As New PageReport(New FileInfo(reportPath))
        report2.Report.ReportParameters.Item(0).DefaultValue.Values.Clear()
        report2.Report.ReportParameters.Item(0).DefaultValue.Values.Add(DTP_Start.Value.Date.ToString("MM/dd/yyyy"))
        report2.Report.ReportParameters.Item(1).DefaultValue.Values.Clear()
        report2.Report.ReportParameters.Item(1).DefaultValue.Values.Add(DTP_End.Value.Date.ToString("MM/dd/yyyy"))

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

    Private Sub DTP_End_ValueChanged(sender As Object, e As EventArgs) Handles DTP_End.ValueChanged

    End Sub

    Private Sub DTP_Start_ValueChanged(sender As Object, e As EventArgs) Handles DTP_Start.ValueChanged

    End Sub

    Private Sub Viewer1_Load(sender As Object, e As EventArgs) Handles Viewer1.Load

    End Sub
End Class