Imports System.Threading

Module MultiThreadEs13

    Sub StampaNumeri(i As Integer, j As Integer)
        For index = i To j
            Console.WriteLine(index)
            Thread.Sleep(100)

        Next

    End Sub

    Sub Main()
        Dim thread1 As New Thread(Sub() StampaNumeri(1, 10))
        Dim thread2 As New Thread(Sub() StampaNumeri(11, 20))

        thread1.Start()
        thread2.Start()

        Console.WriteLine("Code still going")
        thread1.Join()
        thread2.Join()
        Console.WriteLine("Code Finished")

    End Sub
End Module