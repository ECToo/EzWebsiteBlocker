Imports System.Windows.Forms

Public Class adddlg

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try
            If txtbox_url.Text <> "" Then
                main.addEntry(txtbox_url.Text)
                main.updateListBox()
                Me.Close()
            Else
                MsgBox("No website URL entered!", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub adddlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Select()
    End Sub
End Class
