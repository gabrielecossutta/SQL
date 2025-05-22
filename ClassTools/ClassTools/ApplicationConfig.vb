Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Module ApplicationConfig

#Region "CONFIG"
    ''' <summary>
    ''' Write on a JSON file all the information contained in the ConnectionInfoClass
    ''' </summary>
    Public Sub WriteOnConfigFile(connection As Connections)

        'Convert the class to a JSON string
        Dim JsonString As String = JsonConvert.SerializeObject(connection)

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.FullName 'poi metterlo nella directory dell'eseguibile

        'Enter the EXE folder path
        Dim exeFolderPath As String = System.IO.Path.Combine(parentPath, "EXE")

        'Create the file path
        Dim filePath As String = exeFolderPath & "\FILE.json"

        'Create a stream writer to write the JSON string to the file
        Dim file As IO.StreamWriter

        'Open the file and write the JSON string, overwriting any existing content
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)

        'Write the JSON string to the file
        file.WriteLine(JsonString)

        'Close the file
        file.Close()
        file.Dispose()
        file = Nothing

        WriteLogMessage("Data saved on Json File", "EXE", "Log")

    End Sub

    ''' <summary>
    ''' Read the JSON file and deserialize it to the Connections class to retrieve the connection information
    ''' </summary>
    Public Function ReadFromConfigFile() As Connections

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.FullName

        'Enter the EXE folder path
        Dim exeFolderPath As String = System.IO.Path.Combine(parentPath, "EXE")

        'Create the file path
        Dim filePath As String = exeFolderPath & "\FILE.json"

        'Check if the file exists
        If Not File.Exists(filePath) Then
            Return Nothing
        End If

        'Read the JSON string from the file
        Dim jsonString As String = File.ReadAllText(filePath)

        'Deserialize the JSON string to the Connection class
        Dim connection As Connections = JsonConvert.DeserializeObject(Of Connections)(jsonString)

        WriteLogMessage("Data Loaded from Json File", "EXE", "Log")

        Return connection

    End Function

    ''' <summary>
    ''' Class to store service port and protocol name and a list of strings that contain the connection information
    ''' </summary>
    Public Class Connections
        Public Property ServicePort As String
        Public Property ProtocolName As String
        Public Property ConnectionStrings As List(Of ConnectionString)

    End Class

    ''' <summary>
    ''' Containt the connection information, username, password, server name and database name
    ''' </summary>
    Public Class ConnectionString
        Public Property SQLServerName As String
        Public Property DatabaseName As String
        Public Property UserName As String
        Public Property Password As String
    End Class

#End Region

End Module