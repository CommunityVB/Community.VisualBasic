' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Threading
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices
Imports System

Namespace Global.Community.IOx

  Public Module StreamExtensions

    <Extension()>
    Public Async Function ReadByteAsync(stream1 As System.IO.Stream, Optional cancellationToken1 As CancellationToken = CType(Nothing, CancellationToken)) As Task(Of Integer)
      Dim buffer As Byte() = New Byte(0) {}
      Dim numBytesRead As Integer = Await stream1.ReadAsync(buffer.AsMemory(0, 1), cancellationToken1)
      If numBytesRead = 0 Then
        Return -1 ' EOF
      End If
      Return buffer(0)
    End Function

  End Module

End Namespace