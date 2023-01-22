Imports MySql.Data.MySqlClient

Public Class Form3
    Dim StringCon As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=testingform")
    Dim DatabaseReader As MySqlDataReader
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("ยินยันการลบข้อมูลสมาชิก ?", "Alert", MessageBoxButtons.OKCancel)

        If result = DialogResult.OK Then
            Try
                StringCon.Open()
                Dim Command As New MySqlCommand("DELETE FROM users WHERE userID='" & Label10.Text & "'", StringCon)
                Command.ExecuteNonQuery()
                Form1.DialogUser.Rows.Clear()
                Dim Command2 As New MySqlCommand("SELECT * FROM users", StringCon)
                DatabaseReader = Command2.ExecuteReader
                While DatabaseReader.Read
                    Form1.DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
                End While
                DatabaseReader.Dispose()
                Me.Hide()
                MessageBox.Show("[ระบบทำงานสำเร็จ] คุณได้ลบข้อมูลคุณ " & Label6.Text & " เรียบร้อยแล้ว", "Alert", MessageBoxButtons.OK)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                StringCon.Close()
            End Try
        End If
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label10.Visible = False
    End Sub
End Class