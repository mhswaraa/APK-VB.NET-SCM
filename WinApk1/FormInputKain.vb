Imports System.Data.Odbc
Public Class FormInputKain

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
            da = New OdbcDataAdapter("select *from tbl_kain order by kd_kain asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub
    Sub Combbox11()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New OdbcCommand("SELECT * FROM tbl_customer", con)
        Rd = cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item("id_customer"))
        Loop
    End Sub

    Sub kondisiAwal()
        ComboBox1.Text = " -- pilih --"
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Text = ""
        TextBox5.Text = ""
        ComboBox1.Enabled = True
        ComboBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox1.Enabled = True

        Button1.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
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
            If TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim InputData As String = "Insert into tbl_kain values('" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox5.Text & "','" & TextBox7.Text & "')"
                cmd = New OdbcCommand(InputData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            Call koneksi()
            Dim DeleteData As String = "Delete from tbl_kain where kd_kain='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(DeleteData, con)
            cmd.ExecuteNonQuery()
            MsgBox("Delete Data Berhasil")
            Call kondisiAwal()
            tampil()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_kain where kd_kain='" & TextBox1.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                ComboBox1.Text = Rd.Item("id_customer")
                TextBox2.Text = Rd.Item("bahan_kain")
                TextBox3.Text = Rd.Item("warna_kain")
                TextBox5.Text = Rd.Item("jumlah_kain")
                TextBox7.Text = Rd.Item("diskripsi")
            End If

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            kondisiAwal()
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Cetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cetak.Click
        Dim query As String = "SELECT * FROM tbl_kain ORDER BY kd_kain ASC"
        Try
            FormReportKain.DataSetCustomer.Clear()
            FormReportKain.DataSetCustomer.EnforceConstraints = False
            koneksi()
            da = New OdbcDataAdapter(query, con)
            da.Fill(FormReportKain.DataSetCustomer.Kain)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        FormReportKain.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        FormReportKain.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        FormReportKain.ReportViewer1.ZoomPercent = 25
        FormReportKain.ReportViewer1.RefreshReport()
        FormReportKain.Show()
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        kondisiAwal()

    End Sub
End Class