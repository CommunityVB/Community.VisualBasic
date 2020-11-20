Public Class Class1

  Public Sub Test()

    Dim a1 = $"Computer Name: {My.Computer.Name}."
    Dim a2 = $"OS Name: {My.Computer.Info.OSFullName}."
    Dim a3 = $"OS Type: {My.Computer.Info.OSPlatform}."
    Dim a4 = $"OS Version: {My.Computer.Info.OSVersion}."

    Dim f1 = 1.1
    Dim i1 = CInt(Fix(f1))

    Dim aa1 = "This is a test"
    'Dim leftSide = Microsoft.VisualBasic.Strings.Left(aa1, 4)
    Dim leftSide = Left(aa1, 4)

    ' In Microsoft.VisualBasic.Strings
    '  - AscW
    '  - ChrW

  End Sub

End Class
