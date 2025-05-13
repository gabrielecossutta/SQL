Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Newtonsoft.Json

Public Module AccessFile
    Public Sub WriteOnFile(par1 As String, par2 As String, par3 As String)

        'Create a new instance of the Connection class
        Dim connection As New Connection()

        'Populate the Class with the values from the textboxes
        connection.ConnectionStrings = par1
        connection.ServicePort = par2
        connection.ProtocolName = par3

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

        'Create the file if it doesn't exist
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)

        'Write the JSON string to the file
        file.WriteLine(JsonString)

        'Close the file
        file.Close()

    End Sub

    Public Function ReadFromFile() As String

        'Get the base path of the application
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        'Backtrack to the executable directory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName

        'Create the file path
        Dim filePath As String = parentPath & "\FILE.json"

        'Check if the file exists
        If Not File.Exists(filePath) Then
            Return "File not found"
        End If

        'Read the JSON string from the file
        Dim jsonString As String = File.ReadAllText(filePath)

        'Deserialize the JSON string to the Connection class
        Dim connection As Connection = JsonConvert.DeserializeObject(Of Connection)(jsonString)

        'Return the values from the class _______________ Remove this line to return the entire json string
        Return $"ConnectionString: {connection.ConnectionStrings}" + "," + $"ServicePort: {connection.ServicePort}" + "," + $"ProtocolName: {connection.ProtocolName}"

    End Function
End Module