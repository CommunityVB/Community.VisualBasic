' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class VBFixedArrayAttributeTests

    <Theory>
    <InlineData(0, 1)>
    <InlineData(1, 2)>
    Public Sub Ctor_Int(upperBound As Integer, expectedLength As Integer)
      Dim attribute As Community.VisualBasic.VBFixedArrayAttribute = New VBFixedArrayAttribute(upperBound)
      Assert.Equal(New Integer() {upperBound}, attribute.Bounds)
      Assert.Equal(expectedLength, attribute.Length)
    End Sub

    <Theory>
    <InlineData(0, 0, 1)>
    <InlineData(1, 0, 2)>
    <InlineData(0, 1, 2)>
    <InlineData(1, 2, 6)>
    Public Sub Ctor_Int_Int(upperBound1 As Integer, upperBound2 As Integer, expectedLength As Integer)
      Dim attribute As Community.VisualBasic.VBFixedArrayAttribute = New VBFixedArrayAttribute(upperBound1, upperBound2)
      Assert.Equal(New Integer() {upperBound1, upperBound2}, attribute.Bounds)
      Assert.Equal(expectedLength, attribute.Length)
    End Sub

    <Fact>
    Public Sub Ctor_NegativeUpperBound1_ThrowsArgumentException()
      AssertExtensions.Throws(Of ArgumentException)(Nothing, Function() As Community.VisualBasic.VBFixedArrayAttribute
                                                               Return New VBFixedArrayAttribute(-1)
                                                             End Function)
      AssertExtensions.Throws(Of ArgumentException)(Nothing, Function() As Community.VisualBasic.VBFixedArrayAttribute
                                                               Return New VBFixedArrayAttribute(-1, 0)
                                                             End Function)
    End Sub

    <Fact>
    Public Sub Ctor_NegativeUpperBound2_ThrowsArgumentException()
      AssertExtensions.Throws(Of ArgumentException)(Nothing, Function() As Community.VisualBasic.VBFixedArrayAttribute
                                                               Return New VBFixedArrayAttribute(0, -1)
                                                             End Function)
    End Sub

  End Class

End Namespace