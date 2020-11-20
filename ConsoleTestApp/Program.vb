Option Explicit On
Option Strict On
Option Infer On

Imports mVB = Microsoft.VisualBasic
Imports cVB = Community.VisualBasic

Module Program

  Sub Main(args As String())

    Dim a1 = 1.1
    Dim a2 = CInt(Fix(a1))

    Console.Clear()
    Console.WriteLine($"Computer Name: {My.Computer.Name}.")
    Console.WriteLine($"OS Name: {My.Computer.Info.OSFullName}.")
    Console.WriteLine($"OS Type: {My.Computer.Info.OSPlatform}.")
    Console.WriteLine($"OS Version: {My.Computer.Info.OSVersion}.")

    Dim a3 = "This is a test"
    Dim l1 = Left(a3, 4)

    If Debugger.IsAttached Then
      Console.WriteLine()
      Console.WriteLine("Press enter to close.")
      Console.ReadLine()
    End If

  End Sub

End Module
