' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

' TODO: Skipped Null-able Directive enable 
Imports System.Globalization

Namespace Global.Community.Tests

  Public NotInheritable Class ThreadCultureChange
    Implements IDisposable

    Private ReadOnly _origCulture As CultureInfo = CultureInfo.CurrentCulture
    Private ReadOnly _origUICulture As CultureInfo = CultureInfo.CurrentUICulture

    Public Sub New(cultureName As String)
      'MyBase.New(If(cultureName IsNot Nothing, New CultureInfo(cultureName), Nothing))
      If cultureName IsNot Nothing Then
        _origCulture = CultureInfo.CurrentCulture
        CultureInfo.CurrentCulture = New CultureInfo(cultureName)
      End If
    End Sub

    Public Sub New(newCulture As CultureInfo)
      'MyBase.New(newCulture, nothing)
      If newCulture IsNot Nothing Then
        _origCulture = CultureInfo.CurrentCulture
        CultureInfo.CurrentCulture = newCulture
      End If
    End Sub

    Public Sub New(newCulture As CultureInfo, newUICulture As CultureInfo)
      If newCulture IsNot Nothing Then
        _origCulture = CultureInfo.CurrentCulture
        CultureInfo.CurrentCulture = newCulture
      End If

      If newUICulture IsNot Nothing Then
        _origUICulture = CultureInfo.CurrentUICulture
        CultureInfo.CurrentUICulture = newUICulture
      End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
      CultureInfo.CurrentCulture = _origCulture
      CultureInfo.CurrentUICulture = _origUICulture
    End Sub

  End Class

End Namespace