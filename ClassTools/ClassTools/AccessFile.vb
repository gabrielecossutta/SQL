Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Newtonsoft.Json

Public Module AccessFile




    Public Sub CreateNewFile(par1 As String, par2 As String, par3 As String) 'if there is already a file just modify the file in the other function
        Dim connection As New Connection()
        'connectionStrings can contain more than one string
        connection.ConnectionStrings = par1
        connection.ServicePort = par2
        connection.ProtocolName = par3

        Dim json As String = JsonConvert.SerializeObject(connection)
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName 'poi metterlo nella directory dell'eseguibile
        Dim filePath As String = parentPath & "\FILE.json"
        Dim file As IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(filePath, False)
        file.WriteLine(json)
        file.Close()
    End Sub
    Public Function ReadFromFile() As String
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim parentPath As String = System.IO.Directory.GetParent(basePath).Parent.Parent.Parent.FullName
        Dim filePath As String = parentPath & "\FILE.json"
        If Not File.Exists(filePath) Then
            Return "File not found"
        End If

        Dim jsonString As String = File.ReadAllText(filePath)
        Dim connection As Connection = JsonConvert.DeserializeObject(Of Connection)(jsonString)

        Return $"ConnectionString: {connection.ConnectionStrings}" + "," + $"ServicePort: {connection.ServicePort}" + "," + $"ProtocolName: {connection.ProtocolName}"
    End Function


    Public Sub WriteToFile()

    End Sub
End Module