Imports MySql.Data.MySqlClient

Public Class Form1
    Dim StringCon As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=testingform")
    Dim DatabaseReader As MySqlDataReader

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DialogUser.Rows.Clear()

        Try
            StringCon.Open()
            Dim Command As New MySqlCommand("SELECT * FROM users", StringCon)
            DatabaseReader = Command.ExecuteReader
            While DatabaseReader.Read
                DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
            End While
            DatabaseReader.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            StringCon.Close()
        End Try


        Dim editButton As New DataGridViewButtonColumn()
        editButton.HeaderText = "แก้ไข"
        editButton.Name = "Edit"
        editButton.Text = "แก้ไข"
        editButton.UseColumnTextForButtonValue = True
        DialogUser.Columns.Add(editButton)

        Dim deleteButton As New DataGridViewButtonColumn()
        deleteButton.HeaderText = "ลบ"
        deleteButton.Name = "Delete"
        deleteButton.Text = "ลบ"
        deleteButton.UseColumnTextForButtonValue = True
        DialogUser.Columns.Add(deleteButton)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogUser.Rows.Clear()
        Try
            StringCon.Open()
            Dim Command As New MySqlCommand("SELECT * FROM users WHERE userID like '%" & TextBox1.Text & "%' OR userName like '%" & TextBox1.Text & "%'", StringCon)
            DatabaseReader = Command.ExecuteReader
            While DatabaseReader.Read
                DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
            End While
            DatabaseReader.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            StringCon.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.ShowDialog()
    End Sub

    Private Sub DialogUser_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DialogUser.CellClick
        If DialogUser.Columns(e.ColumnIndex).Name = "Delete" Then
            Dim userID As String = DialogUser.Rows(e.RowIndex).Cells(0).Value
            Form3.Label6.Text = DialogUser.Rows(e.RowIndex).Cells(3).Value
            Form3.Label7.Text = DialogUser.Rows(e.RowIndex).Cells(2).Value
            Form3.Label8.Text = DialogUser.Rows(e.RowIndex).Cells(1).Value
            Form3.Label9.Text = DialogUser.Rows(e.RowIndex).Cells(4).Value
            Form3.Label10.Text = userID
            Form3.ShowDialog()
        End If
        If DialogUser.Columns(e.ColumnIndex).Name = "Edit" Then
            Dim userID As String = DialogUser.Rows(e.RowIndex).Cells(0).Value
            Form4.Label6.Text = DialogUser.Rows(e.RowIndex).Cells(1).Value
            Form4.Label7.Text = userID
            Form4.ShowDialog()
        End If
    End Sub
End Class
