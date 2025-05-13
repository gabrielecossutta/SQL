Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports Newtonsoft.Json

Public Module AccessFile




    Public Sub CreateNewFile(name As String, surname As String, age As String)
        Dim NewPerson As New Person()
        NewPerson.Name = name
        NewPerson.Surname = surname
        NewPerson.Age = age

        Dim json As String = JsonConvert.SerializeObject(NewPerson)
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
        Dim person As Person = JsonConvert.DeserializeObject(Of Person)(jsonString)

        Return $"Name: {person.Name}" + "," + $"Surname: {person.Surname}" + "," + $"Age: {person.Age}"
    End Function


    Public Sub WriteToFile()

    End Sub
End Module