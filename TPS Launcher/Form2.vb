Imports MySql.Data.MySqlClient

Public Class Form2
    
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If vbLogin(Username.Text, Password.Text) Then
            MessageBox.Show("Login Correct! Welcome to TPS!")
            Form1.Username = Username.Text
            Form1.Show()
            Me.Close()
        Else
            MessageBox.Show("Incorrect Login")
        End If
    End Sub
End Class