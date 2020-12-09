' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports Microsoft.Win32.SafeHandles
Imports Xunit

Namespace Global.Community

  Public Module AdminHelpers

    ''' <summary>
    ''' Runs the given command as sudo (for Unix).
    ''' </summary>
    ''' <param name="commandLine">The command line to run as sudo</param>
    ''' <returns> Returns the process exit code (0 typically means it is successful)</returns>
    Public Function RunAsSudo(commandLine As String) As Integer
      Dim startInfo As New ProcessStartInfo() With {
        .FileName = "sudo",
        .Arguments = commandLine}
      Using process1 As Process = Process.Start(startInfo)
        Assert.[True](process1.WaitForExit(30000))
        Return process1.ExitCode
      End Using
    End Function

    Public Function IsProcessElevated() As Boolean
      If Not RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
        Dim userId As UInteger = Interop.Sys.GetEUid()
        Return (userId = 0)
      End If

      Dim token As SafeAccessTokenHandle = Nothing
      If Not Interop.Advapi32.OpenProcessToken(Interop.Kernel32.GetCurrentProcess(), TokenAccessLevels.Read, token) Then
        Throw New Win32Exception(Marshal.GetLastWin32Error(), "Open process token failed")
      End If

      Using token
        Dim elevation As New Interop.Advapi32.TOKEN_ELEVATION
        Dim ignore As UInteger
        If Not Interop.Advapi32.GetTokenInformation(token,
                                                    Interop.Advapi32.TOKEN_INFORMATION_CLASS.TokenElevation,
                                                    elevation,
                                                    CUInt(Len(New Interop.Advapi32.TOKEN_ELEVATION())),
                                                    ignore) Then
          Throw New Win32Exception(Marshal.GetLastWin32Error(), "Get token information failed")
        End If

        Return elevation.TokenIsElevated <> Interop.BOOL.[FALSE]
      End Using
    End Function

  End Module

End Namespace
