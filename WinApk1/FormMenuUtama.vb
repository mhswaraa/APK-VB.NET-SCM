Public Class Form1

    Sub Terkunci()
        LoginToolStripMenuItem.Enabled = True
        LogoutToolStripMenuItem.Enabled = False
        MasterToolStripMenuItem.Enabled = False
        TransaksiToolStripMenuItem.Enabled = False
        ProdukToolStripMenuItem.Enabled = False
        ListDataToolStripMenuItem.Enabled = False
        TransaksiToolStripMenuItem1.Enabled = False

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Terkunci()
    End Sub

    Private Sub LoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        FormLogin.ShowDialog()
    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Customer.ShowDialog()
    End Sub

    Private Sub TambahUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormInputKain.ShowDialog()
    End Sub

    Private Sub KainDatangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ListData.ShowDialog()
    End Sub

    Private Sub StokKainToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        formTransaksi.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        FormLogin.ShowDialog()
        Terkunci()
    End Sub

    Private Sub BukuBesarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormInputProduk.ShowDialog()
    End Sub

    Private Sub MasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterToolStripMenuItem.Click
        Customer.ShowDialog()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransaksiToolStripMenuItem.Click
        FormInputKain.ShowDialog()
    End Sub

    Private Sub ProdukToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProdukToolStripMenuItem.Click
        FormInputProduk.ShowDialog()
    End Sub

    Private Sub ListDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListDataToolStripMenuItem.Click
        ListData.ShowDialog()
    End Sub

    Private Sub TransaksiToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransaksiToolStripMenuItem1.Click
        formTransaksi.ShowDialog()
    End Sub
End Class
