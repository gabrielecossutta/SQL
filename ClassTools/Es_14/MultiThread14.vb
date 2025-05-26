Imports System.Threading

Module MultiThread14
    Dim sharedCounter As Integer = 0
    Dim mutex As New Mutex()
    Private lockObject As New Object ()

    Sub Main()
        Dim thread1 As New Thread(Sub() IncrementCounter())
        Dim thread2 As New Thread(Sub() IncrementCounter())

        thread1.Start()
        thread2.Start()

        thread1.Join()
        thread2.Join()

        Console.WriteLine("Conteggio finale: " & sharedCounter)
    End Sub

    Sub IncrementCounter()
        For i As Integer = 1 To 50
            mutex.WaitOne()
            sharedCounter += 1
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {sharedCounter}")
            mutex.ReleaseMutex() ' Rilascia il lock
        Next
    End Sub

    Sub IncrementCounter(f As Integer)
        For i As Integer = 1 To 50
            SyncLock lockObject
                sharedCounter += 1
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {sharedCounter}")
            End SyncLock
        Next
    End Sub

End Module