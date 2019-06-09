
Public Class main
    'Liste zum 
    Dim blocks As New List(Of String)
    Dim hostsfile As String = Environment.GetEnvironmentVariable("SystemDrive") & "/Windows/System32/drivers/etc/hosts"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If loadHostsFile() Then
            updateListBox()
        End If

    End Sub

    Private Function loadHostsFile() As Boolean
        Try
            Dim blocksfilestring() As String = IO.File.ReadAllLines(hostsfile)

            For Each file In blocksfilestring
                If file.Contains("#") = False And file.Length > 0 Then
                    blocks.Add(file)
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'Refresh listbox elements
    Public Sub updateListBox()
        'Delete all elements from listbox
        ListBox1.Items.Clear()
        'Wenn Elemente in der internen Liste
        If blocks.Count <> 0 Then
            For Each block In blocks

                ListBox1.Items.Add(block.Split(" ")(1))
            Next
        End If

    End Sub

    Public Function addBlock(ByVal url As String)

        Try
            If url <> "" Then
                blocks.Add("127.0.0.1 " & url)
                blocks.Add("127.0.0.1 " & url.Replace("www.", ""))
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try


    End Function

    Private Function saveToFile() As Boolean
        Try
            Dim hostsfilestring As String = "#Erstellt von Lokaler Webseiten Blocker v. 1.0"
            For Each block In blocks
                hostsfilestring = hostsfilestring & vbCrLf & block
            Next
            My.Computer.FileSystem.WriteAllText(hostsfile, hostsfilestring, False)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Function deleteBlock(ByVal url As String) As Boolean
        Dim a As String

        If url.Contains("www.") Then a = url.Replace("www.", "")
        If url.Contains("www.") = False Then a = "www." & url


        Try
            For Each block In blocks
                If block.Split(" ")(1) = url Then
                    blocks.Remove("127.0.0.1 " & url)
                    blocks.Remove("127.0.0.1 " & a)
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        adddlg.ShowDialog()
    End Sub


    Private Sub DateiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateiToolStripMenuItem.Click
        If saveToFile() Then
            MsgBox("Erfolgreich gespeichert!", MsgBoxStyle.Information)
        Else
            MsgBox("Fehler beim Speichern!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub picbdelete_Click(sender As Object, e As EventArgs) Handles picbdelete.Click

        If ListBox1.SelectedItems.Count > 0 Then

            Try
                If deleteBlock(ListBox1.SelectedItem.ToString) Then
                    updateListBox()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Keine Webseite ausgewählt!", MsgBoxStyle.Exclamation)
        End If
        updateListBox()
    End Sub
End Class
