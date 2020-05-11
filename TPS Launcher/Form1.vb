Imports TPS_Launcher.Form1.TPSLauncher
Imports System.Net
Imports System.IO


Public Class Form1
    Public Username As String
    Dim ips(100) As String

    Public Class TPSLauncher
        Public Class MyUtilities
            Shared Sub RunCommandCom(ByVal command As String, ByVal arguments As String, ByVal permanent As Boolean)
                Dim p As Process = New Process()
                Dim pi As ProcessStartInfo = New ProcessStartInfo()
                pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
                pi.FileName = "Lithtech.exe"
                p.StartInfo = pi
                p.Start()
            End Sub
        End Class

    End Class

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        MyUtilities.RunCommandCom("Lithtech.exe", "-cmdfile launchcmds.txt -GOMULTIJOIN 1 +join " + IP.Text + ":" + Port.Text, True)
    End Sub

    Private Sub Join_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Join.Click
        Dim temp() As String = ips(OfficialServerList.SelectedIndex).Split(":")
        MyUtilities.RunCommandCom("Lithtech.exe", "-cmdfile launchcmds.txt -GOMULTIJOIN 1 +join " + temp(0) + ":" + temp(1), True)
    End Sub

    Public Function SendHTTP(ByVal url As String)
        Dim Data As String = "Public"
        Dim request As HttpWebRequest = WebRequest.Create(url)
        request.Method = WebRequestMethods.Http.Post
        request.ContentType = "application/x-www-form-urlencoded"
        request.UserAgent = "text/html"
        request.Proxy = Nothing
        request.ContentLength = Data.Length
        Dim rStream As New StreamWriter(request.GetRequestStream)
        rStream.Write(Data)
        rStream.Flush()
        rStream.Close()
        Dim response As HttpWebResponse = request.GetResponse
        Dim resReader As New StreamReader(response.GetResponseStream)
        Return resReader
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Load User info
        Dim temp As StreamReader
        temp = SendHTTP("http://www.intgamers.net/TPS/main.php?action=VBUser2UID&User=" + Username)
        Dim result As String = temp.ReadLine()
        temp = SendHTTP("http://www.intgamers.net/TPS/main.php?action=stats&UID=" + result)
        result = temp.ReadLine()
        Dim split() As String = result.Split("|")
        Label14.Text = split(0)
        Label17.Text = split(1) + "/" + split(6)
        ProgressBar1.Maximum = split(6)
        ProgressBar1.Value = split(1)
        Label15.Text = split(5)
        Label16.Text = split(2) + " - " + split(3) + " - " + split(4)
        PictureBox1.ImageLocation = split(7)

        'Load server info
        temp = SendHTTP("http://www.intgamers.net/TPS/main.php?action=Servers")
        result = temp.ReadLine()
        Dim penis() As String = result.Split(";")
        Dim counter As Integer
        For Each s As String In penis
            If (s = "" Or s = " ") Then
                Continue For
            End If

            Dim splitception() As String = s.Split("|")
            OfficialServerList.Items.Add(splitception(1))
            ips(counter) = splitception(0)
            counter = counter + 1
        Next

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        MyUtilities.RunCommandCom("Lithtech.exe", " " + Commands.Text + "", True)
    End Sub
End Class
