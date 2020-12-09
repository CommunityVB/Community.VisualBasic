Public Class Form1



  Shared Sub Test()

    'Dim logWriter = My.Application.Log.DefaultFileLogWriter
    'Dim logPath = logWriter.FullLogFileName

    'MsgBox(My.Application.Info.DirectoryPath)

    If My.Application.CommandLineArgs?.Count > 0 Then

    End If

    'My.Application.CommandLineArgs
    'My.Application.Culture
    ' - My.Application.HighDpiMode
    'My.Application.Info
    'My.Application.Log
    ' - My.Application.MinimumSplashScreenDisplayTime
    ' - My.Application.OpenForms
    ' - My.Application.SaveMySettingsOnExit
    ' - My.Application.SplashScreen
    ' - My.Application.UICulture
    'My.Computer.*
    'My.Computer.Network
    ' - My.Forms
    'My.User.IsAuthenticated
    'My.User.Name
    ' - My.WebServices

  End Sub

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    Test()
  End Sub

End Class
