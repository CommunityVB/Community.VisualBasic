'Namespace Global.Community.VisualBasic

'  '<HideModuleName()>
'  'Public Module DoesntMatterWhatThisIsCalled

'  '  Public Function Fix(value As Double) As Integer
'  '    Return Math.Floor(value)
'  '  End Function

'  '  Public Function DateAdd(a As Date, b As Integer) As Date
'  '    Return Date.MinValue
'  '  End Function

'  'End Module

'End Namespace

Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.Serialization
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Reflection

Namespace Global.Community.VisualBasic

  ' What appears to be available...
  '
  ' - Microsoft.VisualBasic.CompilerServices.DesignerGeneratedAttribute
  ' - Microsoft.VisualBasic.CompilerServices.IncompleteInitialization
  ' - Microsoft.VisualBasic.CompilerServices.LikeOperator
  ' - Microsoft.VisualBasic.CompilerServices.ObjectFlowControl
  ' - Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute
  ' - Microsoft.VisualBasic.CompilerServices.OptionTextAttribute
  ' - Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute
  ' - Microsoft.VisualBasic.CompilerServices.StaticLocalInitFlag
  ' - Microsoft.VisualBasic.HideModuleNameAttribute
  '
  ' What appears to be (at least) partially available...
  '
  ' - Microsoft.VisualBasic.CompilerServices.Conversions

  Friend NotInheritable Class Application

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property UserAppDataPath As String
      Get
        If OperatingSystem.IsWindows Then
          'Return System.Application.UserAppDataPath
          Dim a = New ApplicationServices.ConsoleApplicationBase
          Dim p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
          Return IO.Path.Combine(p, a.Info.CompanyName, a.Info.ProductName, $"{a.Info.Version.Major}.{a.Info.Version.Minor}.{a.Info.Version.Build}")
        Else
          Return IO.Path.GetDirectoryName(ExecutablePath)
        End If
      End Get
    End Property

    Public Shared ReadOnly Property ExecutablePath As String
      Get
        If OperatingSystem.IsWindows Then
          'Return System.Application.ExecutablePath
          Return Process.GetCurrentProcess.MainModule.FileName
        Else
          'Return AppDomain.CurrentDomain.BaseDirectory ' Recommended as "the right way"... but returns "blank" in WSL2???
          Return Assembly.GetEntryAssembly.Location
        End If
      End Get
    End Property

    Public Shared ReadOnly Property CommonAppDataPath As String
      Get
        If OperatingSystem.IsWindows Then
          'Return Application.CommonAppDataPath
          Dim a = New ApplicationServices.ConsoleApplicationBase
          Dim p = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
          Return IO.Path.Combine(p, a.Info.CompanyName, a.Info.ProductName, $"{a.Info.Version.Major}.{a.Info.Version.Minor}.{a.Info.Version.Build}")
        Else
          Return IO.Path.GetDirectoryName(ExecutablePath)
        End If
      End Get
    End Property

  End Class

  Friend Class SupportedOSPlatform : Inherits Attribute
    Sub New(value As String)
      If value IsNot Nothing Then
      End If
    End Sub
  End Class

End Namespace

Namespace Global.Community.Media

  Friend Class SoundPlayer

    Private ReadOnly m_filename As String
    Private ReadOnly m_stream As IO.Stream

    Sub New()

    End Sub

    Sub New(filename As String)
      m_filename = filename
    End Sub

    Sub New(stream As IO.Stream)
      m_stream = stream
    End Sub

    Public Sub [Stop]()

    End Sub

    Public Sub Play()
      If String.IsNullOrWhiteSpace(m_filename) AndAlso
         m_stream Is Nothing Then
      End If
    End Sub

    Public Sub PlaySync()

    End Sub

    Public Sub PlayLooping()

    End Sub

  End Class

  Public Class SystemSound

    Public Sub Play()

    End Sub

  End Class

End Namespace
