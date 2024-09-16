Imports System.Data.Odbc

Module Module1
    Public Conn As New OdbcConnection
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public CMD As OdbcCommand
    Public Rd As OdbcDataReader
    Public dt As DataTable
    Public MyDB As String

    Public Sub koneksi()
        MyDB = "dsn=db_apk"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

End Module
