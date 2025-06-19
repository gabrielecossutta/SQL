Public Module JsonStructure
    Public Class Order
        Public Property IdOrders As Integer
        Public Property OrderDate As Date
        Public Property OrderCompleted As Boolean
        Public Property OrderInsertDate As Date
        Public Property OrderInsertUser As String
        Public Property OrderModificationDate As Date
        Public Property OrderModificationUser As String
        Public Property Details As List(Of OrderDetails)

    End Class

    ''' <summary>
    ''' Containt the connection information, username, password, server name and database name
    ''' </summary>
    Public Class OrderDetails
        Public Property IdOrder As Integer
        Public Property IdProduct As Integer
        Public Property OrderQuantity As Integer
    End Class

End Module
