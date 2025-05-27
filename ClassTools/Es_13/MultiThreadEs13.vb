Imports System.Threading

Module MultiThreadEs13

    Sub Main()

        ' Create two threads to print numbers from 1 to 10 and from 11 to 20
        Dim thread1 As New Thread(Sub() StampaNumeri(1, 10))
        Dim thread2 As New Thread(Sub() StampaNumeri(11, 20))

        ' Start the threads
        thread1.Start()
        thread2.Start()

        'Writelines to show that the code is still running
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")
        Console.WriteLine("Code still going")

        ' Wait for both threads to finish, Code will not continue until both threads have completed
        thread1.Join()
        thread2.Join()

    End Sub

    'Method to print numbers from i to j with a delay
    Sub StampaNumeri(i As Integer, j As Integer)

        For index = i To j

            Console.WriteLine(index)
            Thread.Sleep(10)

        Next

    End Sub



End Module