Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Net

Public Class Form1

    'FTP server URL
    Dim ftpUrl As String = "ftp://192.168.3.177/"

    'User credentials
    Dim username As String = "Gabriele Cossutta"
    Dim password As String = "3663"

    'Folder where files are going to be downloaded
    Dim DestinationFolder As String = "C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\DWL\"

    Private Sub B_Download_Click(sender As Object, e As EventArgs) Handles B_Download.Click

        DownloadFiles()

    End Sub

    Private Sub DownloadFiles()

        Try
            'Create an FTP request
            Dim request As FtpWebRequest = CType(WebRequest.Create(ftpUrl), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(username, password)

            'Populate the list of file names
            Dim ListFileNamesWeb As New List(Of String)
            Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    While Not reader.EndOfStream
                        ListFileNamesWeb.Add(reader.ReadLine())
                    End While
                End Using
            End Using

            'List of file size
            Dim ListFileSizesWeb As New List(Of Long)

            'Download every file and store the size
            For Each fileName As String In ListFileNamesWeb

                Dim fileUrl As String = ftpUrl & fileName
                Dim localPath As String = Path.Combine(DestinationFolder, fileName)

                'Create the request to get the file size
                Dim sizeRequest As FtpWebRequest = CType(WebRequest.Create(fileUrl), FtpWebRequest)
                sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize
                sizeRequest.Credentials = New NetworkCredential(username, password)

                'Store the file size
                Using sizeResponse As FtpWebResponse = CType(sizeRequest.GetResponse(), FtpWebResponse)
                    ListFileSizesWeb.Add(sizeResponse.ContentLength)
                End Using

                'Create the request to get che file
                Dim downloadRequest As FtpWebRequest = CType(WebRequest.Create(fileUrl), FtpWebRequest)
                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile
                downloadRequest.Credentials = New NetworkCredential(username, password)

                'Download and save the every file
                Using response As FtpWebResponse = CType(downloadRequest.GetResponse(), FtpWebResponse)
                    Using responseStream As Stream = response.GetResponseStream()
                        Using outputFileStream As New FileStream(localPath, FileMode.Create)
                            responseStream.CopyTo(outputFileStream)
                        End Using
                    End Using
                End Using

            Next

            TB_DownloadCompleted.Text = "Download completed"

            'Populare the list of name and size of the files
            Dim ListFileNamesDB As New List(Of String)
            Dim ListFileSizesDB As New List(Of Long)
            For Each file As String In Directory.GetFiles(DestinationFolder)
                Dim fileInfo As New FileInfo(file)
                ListFileNamesDB.Add(fileInfo.Name)
                ListFileSizesDB.Add(fileInfo.Length)
            Next

            'Bool to check if all names and size coincide
            Dim allNamesMatch As Boolean = True
            Dim allSizesMatch As Boolean = True

            'Check if all files have been downloaded and sizes match
            For index = 1 To ListFileNamesWeb.Count - 1

                If ListFileSizesDB(index) <> ListFileSizesWeb(index) Then
                    allSizesMatch = False
                End If

                If ListFileNamesDB(index) <> ListFileNamesWeb(index) Then
                    allNamesMatch = False
                End If

            Next

            'Update Textboxes
            If allNamesMatch Then
                TB_NumberOfFileDownloaded.Text = "All files have been downloaded"
            Else
                TB_NumberOfFileDownloaded.Text = "Not all files have been downloaded"
            End If

            If allSizesMatch Then
                TB_SameDimension.Text = "All files have the same size as remote"
            Else
                TB_SameDimension.Text = "Not all files have the same size as remote"
            End If

        Catch ex As Exception

            TB_DownloadCompleted.Text = "Files not downloaded"

        End Try

    End Sub

End Class
