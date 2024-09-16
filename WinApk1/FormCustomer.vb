Imports System.Data.Odbc
Public Class Customer
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
            da = New OdbcDataAdapter("select *from tbl_customer order by id_customer asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Sub kondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

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
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            Call koneksi()
            Dim InputData As String = "Insert into tbl_customer values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"
            cmd = New OdbcCommand(InputData, con)
            cmd.ExecuteNonQuery()
            MsgBox("Input Data Berhasil")
            Call kondisiAwal()
            tampil()
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            Call koneksi()
            Dim UpdateData As String = "Update tbl_customer set nama='" & TextBox2.Text & "', alamat='" & TextBox3.Text & "',brand_lbl='" & TextBox4.Text & "',no_tlp='" & TextBox5.Text & "' where id_customer='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(UpdateData, con)
            cmd.ExecuteNonQuery()
            MsgBox("Update Data Berhasil")
            Call kondisiAwal()
            tampil()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Silahkan isi nama Field")
        Else
            If (MessageBox.Show("Anda yakin menghapus data dengan kode=" & TextBox1.Text & "...?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
Windows.Forms.DialogResult.OK) Then
                Call koneksi()
                Dim DeleteData As String = "Delete from tbl_customer where id_customer='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(DeleteData, con)
                cmd.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiAwal()
                tampil()
                kondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_customer where id_customer='" & TextBox1.Text & "'", con)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                TextBox1.Text = Rd.Item("id_customer")
                TextBox2.Text = Rd.Item("nama_cust")
                TextBox3.Text = Rd.Item("alamat")
                TextBox4.Text = Rd.Item("brand_lbl")
                TextBox5.Text = Rd.Item("no_tlp")
            End If

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        kondisiAwal()

    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Cetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cetak.Click
        Dim query As String = "SELECT * FROM tbl_customer ORDER BY id_customer ASC"
        Try
            FormReportCustomer.DataSetCustomer.Clear()
            FormReportCustomer.DataSetCustomer.EnforceConstraints = False
            koneksi()
            da = New OdbcDataAdapter(query, con)
            da.Fill(FormReportCustomer.DataSetCustomer.Customer)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        FormReportCustomer.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        FormReportCustomer.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        FormReportCustomer.ReportViewer1.ZoomPercent = 25
        FormReportCustomer.ReportViewer1.RefreshReport()
        FormReportCustomer.Show()
    End Sub

End Class