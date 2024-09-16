Imports System.Data.Odbc

Public Class formTransaksi
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
            da = New OdbcDataAdapter("select *from tbl_transaksi order by no_transaksi", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub
    Sub kondisiAwal()
        ComboBox1.Text = " -- pilih --"
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox7.Text = "0"
        TextBox8.Text = "0"
        TextBox9.Text = "0"

        Button1.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

    End Sub
    Sub Combbox11()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New OdbcCommand(" select *from tbl_list_data", con)
        Rd = cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item("id_customer"))
        Loop
    End Sub

    Private Sub FormListData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampil()
        kondisiAwal()
        Combbox11()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New OdbcCommand("select *from tbl_list_data where id_customer='" & ComboBox1.Text & "'", con)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Kode admin tidak di ketahui")
        Else
            ComboBox1.Text = Rd.Item("id_customer")
            TextBox1.Text = Rd.Item("nama_cust")
            TextBox2.Text = Rd.Item("no_antrian")
            TextBox3.Text = Rd.Item("kd_kain")
            TextBox4.Text = Rd.Item("kd_produk")
            TextBox5.Text = Rd.Item("jumlah_total")
            TextBox6.Text = Rd.Item("total_bayar")
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim query As String = "SELECT * FROM tbl_transaksi ORDER BY no_transaksi ASC"
        Try
            FormReportTransaksi.DataSetCustomer.Clear()
            FormReportTransaksi.DataSetCustomer.EnforceConstraints = False
            koneksi()
            da = New OdbcDataAdapter(query, con)
            da.Fill(FormReportTransaksi.DataSetCustomer.Transaksi)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        FormReportTransaksi.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        FormReportTransaksi.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        FormReportTransaksi.ReportViewer1.ZoomPercent = 25
        FormReportTransaksi.ReportViewer1.RefreshReport()
        FormReportTransaksi.Show()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            Dim jumawal As Integer
            Dim total As Single

            jumawal = CInt(TextBox6.Text)

            total = (jumawal - 100000)
            TextBox8.Text = "Rp." & total
            TextBox7.Text = TextBox5.Text
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Report"
            Button3.Enabled = False
            Button4.Text = "Batal"
        Else
            If ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim InputData As String = "Insert into tbl_transaksi values('" & DateTimePicker1.Value & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox10.Text & "')"
                cmd = New OdbcCommand(InputData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                'kondisiAwal()
                tampil()
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
        Else
            If ComboBox1.Text = "" Or TextBox1.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim DeleteData As String = "Delete from tbl_transaksi where no_transaksi='" & TextBox2.Text & "'"
                cmd = New OdbcCommand(DeleteData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ComboBox1.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox7.Text = "0"
        TextBox8.Text = "0"
        TextBox9.Text = "0"
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Start"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Dim jumawal As Integer
            Dim total As Single

            jumawal = CInt(TextBox6.Text)

            total = (jumawal - 150000)
            TextBox8.Text = "Rp." & total
            TextBox7.Text = TextBox5.Text
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            Dim jumawal As Integer
            Dim total As Single

            jumawal = CInt(TextBox6.Text)

            total = (jumawal - 150000)
            TextBox8.Text = "Rp." & total
            TextBox7.Text = TextBox5.Text
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        TextBox8.Text = "Rp." & TextBox6.Text
        TextBox7.Text = TextBox5.Text
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class