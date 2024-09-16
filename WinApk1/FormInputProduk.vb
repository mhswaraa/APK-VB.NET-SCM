Imports System.Data.Odbc
Public Class FormInputProduk

    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand
    Sub koneksi()
        con = New OdbcConnection
        con.ConnectionString = "dsn=db_apk"
        con.Open()

    End Sub

    Sub tampil()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select *from tbl_produk order by kd_produk asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub
    Sub Combbox11()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New OdbcCommand(" select *from tbl_kain", con)
        Rd = cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item("id_customer"))
            ComboBox2.Items.Add(Rd.Item("kd_kain"))
        Loop
    End Sub

    Sub kondisiAwal()
        ComboBox1.Text = " -- PILIH --"
        ComboBox2.Text = " -- PILIH --"
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""


        Button1.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

    End Sub

    Sub SiapIsi()
        ComboBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
    End Sub

    Private Sub Form_Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kondisiAwal()
        Call tampil()
        Call Combbox11()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            If ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim InputData As String = "Insert into tbl_produk values('" & ComboBox1.Text & "','" & ComboBox2.Text & "', '" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
                cmd = New OdbcCommand(InputData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            If (MessageBox.Show("Anda yakin menghapus data dengan kode=" & TextBox1.Text & "...?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
Windows.Forms.DialogResult.OK) Then
                Call koneksi()
                Dim DeleteData As String = "Delete from tbl_produk where kd_produk='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(DeleteData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiAwal()
                tampil()
                kondisiAwal()
            End If
        End If

    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        kondisiAwal()
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub



    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Cetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cetak.Click
        Dim query As String = "SELECT * FROM tbl_produk ORDER BY kd_produk ASC"
        Try
            FormReportProduk.DataSetCustomer.Clear()
            FormReportProduk.DataSetCustomer.EnforceConstraints = False
            koneksi()
            da = New OdbcDataAdapter(query, con)
            da.Fill(FormReportProduk.DataSetCustomer.Produk)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        FormReportProduk.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        FormReportProduk.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        FormReportProduk.ReportViewer1.ZoomPercent = 25
        FormReportProduk.ReportViewer1.RefreshReport()
        FormReportProduk.Show()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_produk where kd_produk='" & TextBox1.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                ComboBox1.Text = Rd.Item("id_customer")
                TextBox2.Text = Rd.Item("nm_produk")
                TextBox3.Text = Rd.Item("s")
                TextBox4.Text = Rd.Item("m")
                TextBox5.Text = Rd.Item("l")
                TextBox6.Text = Rd.Item("harga")
            End If
        End If
    End Sub
End Class