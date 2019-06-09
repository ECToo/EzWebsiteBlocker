Imports System.Windows.Forms

Public Class adddlg

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try
            If TextBox1.Text <> "" Then
                main.addBlock(TextBox1.Text)
                TextBox1.Text = ""
                main.updateListBox()
                Me.Close()
            Else
                MsgBox("Keine Adresse angegeben!", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub adddlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Select()
    End Sub
End Class
