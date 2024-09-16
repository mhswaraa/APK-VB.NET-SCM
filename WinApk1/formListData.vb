Imports System.Data.Odbc

Public Class ListData
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
            da = New OdbcDataAdapter("select *from tbl_list_data order by id_customer", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7), row(8), row(9), row(10), row(11))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Sub Combbox11()
        Call koneksi()
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()
        cmd = New OdbcCommand("SELECT nama_cust,kd_kain,nm_produk FROM tbl_customer INNER JOIN tbl_produk ON tbl_customer.id_customer = tbl_produk.id_customer", con)
        Rd = cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item("nama_cust"))
            ComboBox2.Items.Add(Rd.Item("kd_kain"))
            ComboBox3.Items.Add(Rd.Item("nm_produk"))

        Loop
    End Sub
    Sub kondisiAwal()
        ComboBox1.Text = " -- pilih --"
        ComboBox2.Text = " -- pilih --"
        ComboBox3.Text = " -- pilih --"
        ComboBox4.Text = " -- pilih --"
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = "0"
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = "0"
        TextBox8.Text = "0"
        TextBox9.Text = "0"
        TextBox10.Text = ""

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Start"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

    End Sub

    Sub SiapIsi()
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True

    End Sub

    Private Sub FormListData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampil()
        kondisiAwal()
        Combbox11()
        Button3.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            Call koneksi()
            Dim InputData As String = "Insert into tbl_list_data values('" & DateTimePicker1.Value & "','" & TextBox5.Text & "','" & TextBox1.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox11.Text & "','" & ComboBox3.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "')"
            cmd = New OdbcCommand(InputData, con)
            cmd.ExecuteNonQuery()
            MsgBox("Input Data Berhasil")
            kondisiAwal()
            tampil()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim jumdat As Integer
        Dim stok As Single
        Dim total As Single

        jumdat = CInt(TextBox5.Text)
        stok = CSng(TextBox6.Text)

        total = (jumdat + stok)
        TextBox7.Text = total
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call SiapIsi()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            If ComboBox1.Text = "" Or TextBox1.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim DeleteData As String = "Delete from tbl_list_data where id_customer='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(DeleteData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        kondisiAwal()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            Call koneksi()
            Dim InputData As String = "Insert into tbl_stok_kain values('" & ComboBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"
            cmd = New OdbcCommand(InputData, con)
            cmd.ExecuteNonQuery()
            MsgBox("Input Data Berhasil")
            kondisiAwal()
            tampil()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New OdbcCommand("select *from tbl_customer where nama_cust='" & ComboBox1.Text & "'", con)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Kode admin tidak di ketahui")
        Else
            ComboBox1.Text = Rd.Item("nama_cust")
            TextBox1.Text = Rd.Item("id_customer")
            TextBox2.Text = Rd.Item("brand_lbl")
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim query As String = "SELECT * FROM tbl_list_data ORDER BY no_antrian ASC"
        Try
            FormReportListData.DataSetCustomer.Clear()
            FormReportListData.DataSetCustomer.EnforceConstraints = False
            koneksi()
            da = New OdbcDataAdapter(query, con)
            da.Fill(FormReportListData.DataSetCustomer.ListData)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        FormReportListData.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        FormReportListData.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        FormReportListData.ReportViewer1.ZoomPercent = 25
        FormReportListData.ReportViewer1.RefreshReport()
        FormReportListData.Show()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Call koneksi()
        cmd = New OdbcCommand("select *from tbl_kain where kd_kain='" & ComboBox2.Text & "'", con)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Kode admin tidak di ketahui")
        Else
            ComboBox2.Text = Rd.Item("kd_kain")
            TextBox10.Text = Rd.Item("bahan_kain")
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Call koneksi()
        cmd = New OdbcCommand("select *from tbl_produk where nm_produk='" & ComboBox3.Text & "'", con)
        Rd = cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Kode admin tidak di ketahui")
        Else
            TextBox11.Text = Rd.Item("kd_produk")
            TextBox7.Text = Rd.Item("harga")
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Text = "S" Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_produk where nm_produk='" & ComboBox3.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                TextBox6.Text = Rd.Item("S")
            End If

        ElseIf ComboBox4.Text = "M" Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_produk where nm_produk='" & ComboBox3.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                TextBox6.Text = Rd.Item("M")
            End If

        ElseIf ComboBox4.Text = "L" Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_produk where nm_produk='" & ComboBox3.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                TextBox6.Text = Rd.Item("L")
            End If
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim yard As Integer
        Dim pcsroll As Single
        Dim total As Single

        yard = CInt(TextBox3.Text)
        pcsroll = CSng(TextBox6.Text)

        total = (yard / pcsroll)
        TextBox8.Text = total
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim yardt As Integer
        Dim pcsrollt As Single
        Dim total As Single

        yardt = CInt(TextBox7.Text)
        pcsrollt = CSng(TextBox8.Text)

        total = (yardt * pcsrollt)
        TextBox9.Text = total
    End Sub


    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
    End Sub

 
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_list_data where no_antrian ='" & TextBox5.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                ComboBox1.Text = Rd.Item("nama_cust")
            End If

        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class
