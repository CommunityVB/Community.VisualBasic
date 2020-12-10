' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class VBFixedStringAttributeTests

    <Theory>
    <InlineData(1)>
    <InlineData(32767)>
    Public Sub Ctor_Int(length As Integer)
      Dim attribute As Community.VisualBasic.VBFixedStringAttribute = New VBFixedStringAttribute(length)
      Assert.Equal(length, attribute.Length)
    End Sub

    <Theory>
    <InlineData(-1)>
    <InlineData(0)>
    <InlineData(32768)>
    Public Sub Ctor_InvalidLength_ThrowsArgumentException(length As Integer)
      AssertExtensions.Throws(Of ArgumentException)(Nothing, Function() As Community.VisualBasic.VBFixedStringAttribute
                                                               Return New VBFixedStringAttribute(length)
                                                             End Function)
    End Sub

  End Class

End Namespace