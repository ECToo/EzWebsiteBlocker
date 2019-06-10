
Public Class main
    'List of hosts entries
    Dim entries As New List(Of String)
    'Path to hosts file
    Dim hostsfilePath As String = Environment.GetEnvironmentVariable("SystemDrive") & "/Windows/System32/drivers/etc/hosts"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load hosts file and check if it was succesful
        If loadHostsFile() Then
            'Display entries in Listbox
            updateListBox()
        End If

    End Sub

    Private Function loadHostsFile() As Boolean
        Try
            Dim lines() As String = IO.File.ReadAllLines(hostsfilePath)

            For Each line In lines
                'Check if line is a comment(starts with #) or line is empty
                If line.StartsWith("#") = False And line.Length > 0 Then
                    'Add leine to list
                    entries.Add(line)
                End If
            Next
            'Return True if everything completed succesfully
            Return True
        Catch ex As Exception
            'Return False if an error occured
            Return False
        End Try
    End Function
    'Refresh listbox elements
    Public Sub updateListBox()
        'Delete all elements from listbox
        lstbox_entries.Items.Clear()
        'Check if there are elements in the list
        If entries.Count <> 0 Then
            'Add every entry to listbox
            For Each entry In entries
                'entry = "IP Website"
                Dim name As String = entry.Split(" ")(1)
                lstbox_entries.Items.Add(name)
            Next
        End If

    End Sub

    Public Function addEntry(ByVal url As String)

        Try
            'Check if given url parameter is not empty
            If url <> "" Then
                'Add URL with leading "www" and none
                entries.Add("127.0.0.1 " & url)
                entries.Add("127.0.0.1 " & url.Replace("www.", ""))
            End If
            'Return True if everything was done succesfully
            Return True
        Catch ex As Exception
            'Return False if an error occured
            Return False
        End Try


    End Function

    Private Function saveToFile() As Boolean
        Try
            'Prepare final string for hosts file
            Dim hostsfilestring As String = "#Created by EzWebsiteBlocker"
            'Add every entry to string in a seperate line
            For Each block In entries
                hostsfilestring = hostsfilestring & vbCrLf & block
            Next
            'Write final string into hosts file
            My.Computer.FileSystem.WriteAllText(hostsfilePath, hostsfilestring, False)
            'Return True if everything completed succesfully
            Return True
        Catch ex As Exception
            'Return False if an error occured
            Return False
        End Try


    End Function

    Private Function deleteBlock(ByVal url As String) As Boolean
        Dim secondEntry As String

        'Generate second url to remove from list
        If url.Contains("www.") Then
            secondEntry = url.Replace("www.", "")
        ElseIf url.Contains("www.") = False Then
            secondEntry = "www." & url
        End If

        'Delete entries from list
        Try
            For Each entry In entries
                If entry.Split(" ")(1) = url Then
                    entries.Remove("127.0.0.1 " & url)
                    entries.Remove("127.0.0.1 " & secondEntry)
                End If
            Next
            'Return True if everything completed succesfully
            Return True
        Catch ex As Exception
            'Return False if an error occured
            Return False
        End Try

    End Function

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'Show dialog for adding an entry
        adddlg.ShowDialog()
    End Sub


    Private Sub DateiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DateiToolStripMenuItem.Click
        If saveToFile() Then
            MsgBox("Succesfully saved!", MsgBoxStyle.Information)
        Else
            MsgBox("An error occured saving your entries!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub picbdelete_Click(sender As Object, e As EventArgs) Handles picbdelete.Click
        'Check if there is an item selected
        If lstbox_entries.SelectedItems.Count > 0 Then
            Try
                'Check if deletion was succesful
                If deleteBlock(lstbox_entries.SelectedItem.ToString) Then
                    'Refresh listbox
                    updateListBox()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No entry selected!", MsgBoxStyle.Exclamation)
        End If
        updateListBox()
    End Sub
End Class
