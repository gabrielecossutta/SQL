
Imports System.Data.Entity
Imports System.Net
Imports System.Runtime.Remoting.Contexts
Imports System.Text

Public Class F_Synchronize
    Private Sub B_BackToTotem_Click(sender As Object, e As EventArgs) Handles B_BackToTotem.Click
        SyncronizeBackOfficeOnTotem()

    End Sub

    Sub SyncronizeBackOfficeOnTotem()
        Using contextBackOffice As New DbStructure.BackOfficeDbContext()
            Dim productsToSync = contextBackOffice.Products.ToList()
            Using contextTotem As New DbStructure.TotemDbContext()
                For Each product In productsToSync
                    Dim existingProduct = contextTotem.Products.FirstOrDefault(Function(p) p.ProductName = product.ProductName)
                    If existingProduct IsNot Nothing Then

                        If existingProduct.ProductModificationDate <> Date.Now.Date Then
                            existingProduct.ProductCategory = product.ProductCategory
                            existingProduct.ProductPrice = product.ProductPrice
                            existingProduct.ProductPicture = product.ProductPicture
                            existingProduct.ProductDescription = product.ProductDescription
                            existingProduct.ProductModificationDate = Date.Now.Date
                            existingProduct.ProductModificationUser = product.ProductModificationUser
                        Else


                            existingProduct.ProductModificationDate = Date.Now.Date
                            existingProduct.ProductModificationUser = product.ProductModificationUser
                        End If
                    Else
                        product.ProductModificationDate = Date.Now.Date
                        product.ProductModificationUser = product.ProductInsertUser
                        contextTotem.Products.Add(product)
                    End If
                Next
                contextTotem.SaveChanges()
                Dim productsToCheck = contextTotem.Products.ToList()
                Dim productsToDelete = productsToCheck.Where(Function(ptc) ptc.ProductModificationDate <> Date.Now.Date)
                If productsToDelete.Count > 0 Then
                    contextTotem.Products.RemoveRange(productsToDelete)
                End If
            End Using
        End Using
    End Sub

    Private Sub B_TotemToBackOffice_Click(sender As Object, e As EventArgs) Handles B_TotemToBackOffice.Click
        SyncronizeTotemOnBackOffice()
    End Sub

    Sub SyncronizeTotemOnBackOffice()
        Using contextTotem As New DbStructure.TotemDbContext()
            Dim OrdersToSent = contextTotem.Orders.ToList()
            Dim OrderDetailsToSent = contextTotem.OrderDetails.ToList()
            Dim SummaryToSent = contextTotem.Summaries.ToList()

            Using contextBackOffice As New DbStructure.BackOfficeDbContext()
                For Each order In OrdersToSent
                    Dim OrderToSync As New DbStructure.Orders() With {
                        .OrderCompleted = order.OrderCompleted,
                        .OrderDate = order.OrderDate,
                        .OrderInsertDate = order.OrderInsertDate,
                        .OrderInsertUser = order.OrderInsertUser,
                        .OrderModificationDate = order.OrderModificationDate,
                        .OrderModificationUser = order.OrderModificationUser
                    }
                    contextBackOffice.Orders.Add(OrderToSync)
                    contextBackOffice.SaveChanges()

                    For Each Details In OrderDetailsToSent

                        If order.IdOrders = Details.IdOrder Then

                            Dim OrderDetailsToSync As New DbStructure.OrderDetails() With {
                                .IdOrder = OrderToSync.IdOrders,
                                .IdProduct = Details.IdProduct,
                                .OrderQuantity = Details.OrderQuantity
                            }
                            contextBackOffice.OrderDetails.Add(OrderDetailsToSync)


                        End If
                    Next
                    contextBackOffice.SaveChanges()
                Next
                For Each Summary In SummaryToSent
                    Dim existingSummary = contextBackOffice.Summaries.SingleOrDefault(Function(s) s.IdProduct = Summary.IdProduct AndAlso s.RegistrationDate = Date.Today)

                    If existingSummary IsNot Nothing Then
                        existingSummary.TotalQuantity += Summary.TotalQuantity
                        existingSummary.TotalPrice += Summary.TotalPrice
                    Else
                        Dim newSummary As New DbStructure.Summaries With
                        {
                            .IdProduct = Summary.IdProduct,
                            .RegistrationDate = Date.Now,
                            .TotalQuantity = Summary.TotalQuantity,
                            .TotalPrice = Summary.TotalPrice
                        }
                        contextBackOffice.Summaries.Add(newSummary)
                    End If
                    contextBackOffice.SaveChanges()
                Next


                contextTotem.OrderDetails.RemoveRange(OrderDetailsToSent)
                contextTotem.Orders.RemoveRange(OrdersToSent)
                contextTotem.Summaries.RemoveRange(SummaryToSent)
                contextTotem.SaveChanges()
            End Using


        End Using
        'syncronize the oreder on the back office
        'write all the order and the order details and detele them
        'manda anche il summary of the order
    End Sub

    Private Sub B_SendWebService_Click(sender As Object, e As EventArgs) Handles B_SendWebService.Click



        Dim url As String = "http://localhost:81/OrderReceiver.ashx"
        Dim jsonData As String = "{" &
    """IdOrders"":123," &
    """OrderDate"":""2025-06-17T14:30:00""," &
    """OrderCompleted"":true," &
    """OrderInsertDate"":""2025-06-15T09:00:00""," &
    """OrderInsertUser"":""admin""," &
    """OrderModificationDate"":""2025-06-16T11:20:00""," &
    """OrderModificationUser"":""editor""," &
    """OrderDetailsJSON"":[{" &
        """IdOrder"":123," &
        """IdProduct"":456," &
        """OrderQuantity"":2" &
    "},{" &
        """IdOrder"":123," &
        """IdProduct"":789," &
        """OrderQuantity"":5" &
    "}]" &
"}"

        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            Dim byteData As Byte() = Encoding.UTF8.GetBytes(jsonData)
            request.ContentLength = byteData.Length

            Using stream = request.GetRequestStream()
                stream.Write(byteData, 0, byteData.Length)
            End Using

            Try
                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New IO.StreamReader(response.GetResponseStream())
                    Dim result = reader.ReadToEnd()
                MessageBox.Show("Risposta dal server: " & result)
            End Using
            Catch ex As WebException
                Using reader As New IO.StreamReader(ex.Response.GetResponseStream())
                    Dim errorResult = reader.ReadToEnd()
                MessageBox.Show("Errore: " & errorResult)
            End Using
            End Try

        MessageBox.Show("Premi un tasto per uscire...")
    End Sub


End Class