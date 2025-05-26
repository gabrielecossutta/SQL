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

    Public Function WriteAFile(message As String, folderName As String, fileName As String, extenction As String) As String

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.FullName

        'Enter the EXE folder path
        Dim FolderPath As String = System.IO.Path.Combine(parentPath, folderName)

        'Create the file path
        Dim filePath As String = $"{FolderPath}\{fileName}.{extenction}"

        'Create a stream writer to write on the TXT file
        Dim file As IO.StreamWriter

        'Open the file and write the log message without overwriting
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)

        'Write the log message to the file with the current time
        file.WriteLine(message)

        'Close the file
        file.Close()
        file.Dispose()

        Return filePath

    End Function

End Module
