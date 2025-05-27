Imports System.Threading

Module MultiThread14

    Dim sharedCounter As Integer = 0
    Dim mutex As New Mutex()
    Private lockObject As New Object()

    Sub Main()

        'Create two threads that will increment the shared counter
        Dim thread1 As New Thread(Sub() IncrementCounter())
        Dim thread2 As New Thread(Sub() IncrementCounter())

        'Start both threads
        thread1.Start()
        thread2.Start()

        'Wait for both threads to complete
        thread1.Join()
        thread2.Join()

    End Sub

    'Method to increment the shared counter using Mutex
    Sub IncrementCounter()

        For i As Integer = 1 To 50

            'Lock the mutex to ensure exclusive access to the shared counter
            mutex.WaitOne()

            sharedCounter += 1
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {sharedCounter}")

            'Release the mutex to allow other threads to access the shared counter
            mutex.ReleaseMutex()

        Next

    End Sub

    'Method to increment the shared counter using synclock
    Sub IncrementCounter(f As Integer)

        For i As Integer = 1 To 50

            'Use SyncLock to ensure exclusive access to the shared counter
            SyncLock lockObject

                sharedCounter += 1
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {sharedCounter}")

            End SyncLock

        Next

    End Sub

End Module