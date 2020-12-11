Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Module Extension

  '' Inspired by https://mariusschulz.com/blog/detecting-the-operating-system-in-net-core
  '' Tweaked to be Extension Methods for the existing OperatingSystem class.

  '<Extension>
  'Public Function IsWindows(os As OperatingSystem) As Boolean
  '  If os Is Nothing Then
  '    ' Just to remove the warning for not using the 'os' variable.
  '  End If
  '  Return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
  'End Function

  '<Extension>
  'Public Function IsLinux(os As OperatingSystem) As Boolean
  '  If os Is Nothing Then
  '    ' Just to remove the warning for not using the 'os' variable.
  '  End If
  '  Return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
  'End Function

  '<Extension>
  'Public Function IsOSX(os As OperatingSystem) As Boolean
  '  If os Is Nothing Then
  '    ' Just to remove the warning for not using the 'os' variable.
  '  End If
  '  Return RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
  'End Function

End Module
