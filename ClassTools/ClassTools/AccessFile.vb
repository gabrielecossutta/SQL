Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Newtonsoft.Json

Public Module AccessFile
    Public Sub WriteOnFile(par1 As String, par2 As String, par3 As String)

        '-----------------------qua passerai il connection lo serializzi e lo scrivi nel file----------------
        'Create a new instance of the Connection class
        Dim connection As New Connections()

        'Populate the Class with the values from the textboxes
        'connection.ConnectionStrings = par1
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



        '---------------------------------------qua ritorna tutto il json che gestirai in unaltra funzione--magari anche popolare---------------------
        Return connection

    End Function
End Module