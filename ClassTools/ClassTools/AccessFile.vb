Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Newtonsoft.Json

Public Module AccessFile
    Public Sub WriteOnFile(connection As Connections)

        'Convert the class to a JSON string
        Dim JsonString As String = JsonConvert.SerializeObject(connection)

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName 'poi metterlo nella directory dell'eseguibile

        'Create the file path
        Dim filePath As String = parentPath & "\FILE.json"

        'Create a stream writer to write the JSON string to the file
        Dim file As IO.StreamWriter

        'Open the file and write the JSON string, overwriting any existing content
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)

        'Write the JSON string to the file
        file.WriteLine(JsonString)

        'Close the file
        file.Close()

    End Sub

    Public Sub WriteLogMessage(log As String)

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName 'poi metterlo nella directory dell'eseguibile

        'Create the file path
        Dim filePath As String = parentPath & "\Log.txt"

        'Create a stream writer to write on the TXT file
        Dim file As IO.StreamWriter

        'Open the file and write the log message without overwriting
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, True)

        'Write the log message to the file with the current time
        file.WriteLine(My.Computer.Clock.LocalTime + " " + log + ";")

        'Close the file
        file.Close()

    End Sub

    Public Function ReadFromFile() As Connections

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName

        'Create the file path
        Dim filePath As String = parentPath & "\FILE.json"

        'Check if the file exists
        If Not File.Exists(filePath) Then
            Return Nothing
        End If

        'Read the JSON string from the file
        Dim jsonString As String = File.ReadAllText(filePath)

        'Deserialize the JSON string to the Connection class
        Dim connection As Connections = JsonConvert.DeserializeObject(Of Connections)(jsonString)
        Return connection

    End Function
End Module