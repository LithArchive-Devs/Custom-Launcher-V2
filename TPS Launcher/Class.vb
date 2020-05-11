Imports System.Security.Cryptography
Imports System.Text
Imports System.Net
Imports System.IO
Module vbloginsystem
    Public Function vbLogin(ByVal Username As String, ByVal Password As String)
        Password = MD5(Password)
        Dim valid As Boolean = False
        Dim data As String = "vb_login_username=" & Username & "&vb_login_password=&s=&do=login&vb_login_md5password=" & Password & "&vb_login_md5password_utf=" & Password
        Try
            Dim request As HttpWebRequest = WebRequest.Create("http://www.intgamers.net/forum/login.php?do=login")
            request.Method = WebRequestMethods.Http.Post
            request.ContentType = "application/x-www-form-urlencoded"
            request.UserAgent = "-- vBulletin Vaidation  --"
            request.ContentLength = data.Length
            Dim rStream As New StreamWriter(request.GetRequestStream)
            rStream.Write(data)
            rStream.Flush()
            rStream.Close()
            Dim response As HttpWebResponse = request.GetResponse
            Dim resReader As New StreamReader(response.GetResponseStream)
            Dim str As String = resReader.ReadToEnd
            If str.Contains("Thank you for logging in") Then
                valid = True
            Else
                valid = False
            End If
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error intgamers.net - Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return valid
    End Function
    Public Function MD5(ByVal number As String) As String
        Dim ASCIIenc As New ASCIIEncoding
        Dim strReturn As String = String.Empty
        Dim ByteSourceText() As Byte = ASCIIenc.GetBytes(number)
        Dim Md5Hash As New MD5CryptoServiceProvider
        Dim ByteHash() As Byte = MD5Hash.ComputeHash(ByteSourceText)
        For Each b As Byte In ByteHash
            strReturn &= b.ToString("x2")
        Next
        Return strReturn
    End Function
End Module