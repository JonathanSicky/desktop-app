Imports MySql.Data.MySqlClient

Public Class Form2
    Dim i As Integer
    Dim StringCon As New MySqlConnection("server=localhost;port=3306;username=root;password=;database=testingform")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                MessageBox.Show("[ระบบทำงานไม่สำเร็จ] กรุณากรอกข้อมูลให้ครบถ้วน คุณไม่ควรเว้นเป็นช่องว่าง")
                Exit Sub
            Else
                StringCon.Open()

                Dim sql As String = "SELECT COUNT(*) FROM users WHERE userName = @userName"
                Dim command As New MySqlCommand(sql, StringCon)
                Dim Command2 As New MySqlCommand("SELECT * FROM users", StringCon)
                Dim DatabaseReader As MySqlDataReader

                command.Parameters.Clear()
                Form1.DialogUser.Rows.Clear()
                command.Parameters.AddWithValue("@userName", TextBox3.Text)

                If command.ExecuteScalar() > 0 Then
                    DatabaseReader = Command2.ExecuteReader
                    While DatabaseReader.Read
                        Form1.DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
                    End While
                    DatabaseReader.Dispose()

                    MessageBox.Show("[ระบบทำงานไม่สำเร็จ] เนื่องจากมีชื่อซ้ำจึงไม่สามารถเพิ่มข้อมูลลงฐานข้อมูลได้")
                Else
                    sql = "INSERT INTO users (userName, userPassword, userFirstname, userPhoneNumber) VALUES (@userName, @userPassword, @userFirstname, @userPhoneNumber)"
                    command = New MySqlCommand(sql, StringCon)
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@userFirstname", TextBox1.Text)
                    command.Parameters.AddWithValue("@userPassword", TextBox2.Text)
                    command.Parameters.AddWithValue("@userName", TextBox3.Text)
                    command.Parameters.AddWithValue("@userPhoneNumber", TextBox4.Text)
                    command.ExecuteNonQuery()

                    DatabaseReader = Command2.ExecuteReader
                    While DatabaseReader.Read
                        Form1.DialogUser.Rows.Add(DatabaseReader.Item("userID"), DatabaseReader.Item("userName"), DatabaseReader.Item("userPassword"), DatabaseReader.Item("userFirstname"), DatabaseReader.Item("userPhoneNumber"))
                    End While
                    DatabaseReader.Dispose()
                    Me.Hide()
                    MessageBox.Show("[ระบบทำงานสำเร็จ] ระบบได้บันทึกข้อมูลสมาชิกลงฐานข้อมูลเรียบร้อยแล้ว", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            StringCon.Close()
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class