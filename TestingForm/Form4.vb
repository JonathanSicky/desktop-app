Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form4
    Dim StringCon As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=testingform")
    Dim DatabaseReader As MySqlDataReader
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label7.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                MessageBox.Show("[ระบบทำงานไม่สำเร็จ] กรุณากรอกข้อมูลให้ครบถ้วน คุณไม่ควรเว้นเป็นช่องว่าง")
                Exit Sub
            Else
                StringCon.Open()
                Dim Command As New MySqlCommand("UPDATE `users` SET `userName` = @userName, `userPassword` = @userPassword, `userFirstname` = @userFirstname, `userPhoneNumber` = @userPhoneNumber WHERE `userID` = @userID", StringCon)
                Dim Command2 As New MySqlCommand("SELECT * FROM users", StringCon)
                Dim DatabaseReader As MySqlDataReader

                Command.Parameters.Clear()
                Form1.DialogUser.Rows.Clear()
                Command.Parameters.AddWithValue("@userID", Label7.Text)
                Command.Parameters.AddWithValue("@userFirstname", TextBox3.Text)
                Command.Parameters.AddWithValue("@userPassword", TextBox2.Text)
                Command.Parameters.AddWithValue("@userName", Label6.Text)
                Command.Parameters.AddWithValue("@userPhoneNumber", TextBox4.Text)
                Command.ExecuteNonQuery()

                DatabaseReader = Command2.ExecuteReader
                While DatabaseReader.Read
                    Form1.DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
                End While
                DatabaseReader.Dispose()
                Me.Hide()
                MessageBox.Show("[ระบบทำงานสำเร็จ] ระบบได้ทำการอัพเดทข้อมูลเรียบร้อยแล้ว", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            StringCon.Close()
        End Try
    End Sub
End Class