Option Explicit On
Option Strict On
Option Infer On

Imports mVB = Microsoft.VisualBasic
Imports cVB = Community.VisualBasic
Imports System.Runtime.InteropServices

Module Program

  Sub Main(args As String())

    Dim a1 = 1.1
    Dim a2 = CInt(Fix(a1))

    Console.Clear()
    Console.WriteLine($"Computer Name: {My.Computer.Name}.")
    Console.WriteLine($"OS Name: {My.Computer.Info.OSFullName}.")
    Console.WriteLine($"OS Type: {My.Computer.Info.OSPlatform}.")
    Console.WriteLine($"OS Version: {My.Computer.Info.OSVersion}.")

    Dim v = mVB.Asc("a"c)

    Dim a3 = "This is a test"
    Dim l1 = Left(a3, 4)

    Dim c = New ClassLibraryTest.Class1()
    c.Test()

    Dim cl = cVB.Command
    Dim cl1 = c.GetCommandLine

    'Console.WriteLine(My.User.Name)

    'Console.WriteLine($"Command Line: {cl}")
    'Console.WriteLine($"Command Line: {cl1}")

    'Environment.SetEnvironmentVariable("OURVARIABLE", "OURVALUE")
    'For Each entry As DictionaryEntry In Environment.GetEnvironmentVariables()
    '  Console.WriteLine($"{entry.Key}={entry.Value}")
    'Next

    'Dim e1 = cVB.Environ("PATH")
    'Dim e2 = c.GetEnviron("PATH")
    'Console.WriteLine($"PATH={e1}")
    'Console.WriteLine($"PATH={e2}")

    If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) AndAlso
       Debugger.IsAttached Then
      Console.WriteLine()
      Console.WriteLine("Press enter to close.")
      Console.ReadLine()
    End If

  End Sub

End Module
