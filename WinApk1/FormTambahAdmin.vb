Imports System.Data.Odbc
Public Class FormTambahAdmin
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
            da = New OdbcDataAdapter("select *from tbl_user order by kode_user asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3))
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
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.Items.Add("OWNER")
        ComboBox1.Items.Add("ADMIN1")
        ComboBox1.Items.Add("ADMIN2")
        ComboBox1.Items.Add("ADMIN3")
    End Sub

    Private Sub Form_Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kondisiAwal()
        Call tampil()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim InputData As String = "Insert into tbl_user values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "')"
                cmd = New OdbcCommand(InputData, con)
                CMD.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim UpdateData As String = "Update tbl_user set nama_user='" & TextBox2.Text & "', password='" & TextBox3.Text & "',level_user='" & ComboBox1.Text & "' where kode_user='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(UpdateData, con)
                CMD.ExecuteNonQuery()
                MsgBox("Update Data Berhasil")
                Call kondisiAwal()
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
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi nama Field")
            Else
                Call koneksi()
                Dim DeleteData As String = "Delete from tbl_user where kode_user='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(DeleteData, con)
                CMD.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiAwal()
                tampil()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select *from tbl_user where kode_user='" & TextBox1.Text & "'", con)
            Rd = CMD.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Kode admin tidak di ketahui")
            Else
                TextBox1.Text = Rd.Item("kode_user")
                TextBox2.Text = Rd.Item("nama_user")
                TextBox3.Text = Rd.Item("password")
                ComboBox1.Text = Rd.Item("level_user")
            End If

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            kondisiAwal()
        End If

    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

End Class