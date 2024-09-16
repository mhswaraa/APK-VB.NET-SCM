Imports System.Data.Odbc
Public Class FormLogin

    Sub terbuka()
        Form1.LoginToolStripMenuItem.Enabled = False
        Form1.LogoutToolStripMenuItem.Enabled = True
        Form1.MasterToolStripMenuItem.Enabled = True
        Form1.TransaksiToolStripMenuItem.Enabled = True
        Form1.ProdukToolStripMenuItem.Enabled = True
        Form1.ListDataToolStripMenuItem.Enabled = True
        Form1.TransaksiToolStripMenuItem1.Enabled = True

    End Sub
    Sub kondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Enabled = True
        TextBox2.Enabled = True

    End Sub
    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text = "mahes" And TextBox2.Text = "1234" Then
            Form1.Show()
            Me.Hide()
            Call terbuka()

        Else
            MsgBox("Username dan Password yang anda masukkan salah !!", MsgBoxStyle.MsgBoxHelp, "Exit")

        End If
    End Sub

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        Else
            CheckBox1.Checked = False
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class