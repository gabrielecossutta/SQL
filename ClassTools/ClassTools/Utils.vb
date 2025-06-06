Imports System.Runtime.CompilerServices

Public Module Utils

    ''' <summary>
    ''' Writes a log message to a specified file in a specified folder with the current time.
    ''' </summary>
    ''' <param name="message">The message to log.</param>
    ''' <param name="folderName">The name of the folder where the log file is located.</param>
    ''' <param name="fileName">The name of the log file.</param>
    Public Sub WriteLogMessage(message As String, folderName As String, fileName As String)

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.FullName

        'Enter the EXE folder path
        Dim exeFolderPath As String = System.IO.Path.Combine(parentPath, folderName)

        'Create the file path
        Dim filePath As String = $"{exeFolderPath}\{fileName}.txt"

        'Create a stream writer to write on the TXT file
        Dim file As IO.StreamWriter

        'Open the file and write the log message without overwriting
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, True)

        'Write the log message to the file with the current time
        file.WriteLine(My.Computer.Clock.LocalTime + " " + message + ";")

        'Close the file
        file.Close()
        file.Dispose()

    End Sub

    ''' <summary>
    ''' Writes a file with the specified message, folder name, file name, and extension.
    ''' </summary>
    ''' <param name="message">The message to write.</param>
    ''' <param name="folderName">The name of the folder where the file is located.</param>
    ''' <param name="fileName">The name of the log file.</param>
    ''' <param name="extension">extenction</param>
    Public Function WriteAFile(message As String, folderName As String, fileName As String, extension As String, append As Boolean) As String
        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.FullName

        'Enter the EXE folder path
        Dim FolderPath As String = System.IO.Path.Combine(parentPath, folderName)

        'Ensure the folder exists
        If Not System.IO.Directory.Exists(FolderPath) Then
            System.IO.Directory.CreateDirectory(FolderPath)
        End If

        'Create the file path
        Dim filePath As String = $"{FolderPath}\{fileName}.{extension}"

        'Create the file if it does not exist
        If Not System.IO.File.Exists(filePath) Then
            System.IO.File.Create(filePath).Dispose()
        End If

        'Open the file and write the log message
        Dim file As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filePath, append)

        'Write the log message to the file with the current time
        file.WriteLine(message)

        'Close the file
        file.Close()
        file.Dispose()

        'Return the file path
        Return filePath
    End Function

End Module
