' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Globalization
Imports Community.VisualBasic.CompilerServices
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class DoubleTypeTests

    <Theory>
    <InlineData(Nothing, 0)>
    <InlineData("&H123", 291)>
    <InlineData("&O123", 83)>
    <InlineData("123", 123)>
    Public Sub FromString(value As String, expected As Double)
      Assert.Equal(expected, DoubleType.FromString(value))
    End Sub

    <Fact>
    Public Sub FromString_Invalid()
      Assert.Throws(Of InvalidCastException)(Function() DoubleType.FromString("abc"))
    End Sub

    <Fact>
    Public Sub FromObject()
      Assert.Equal(0R, DoubleType.FromObject(Nothing))
      Assert.Equal(-1.0R, DoubleType.FromObject(True))
      Assert.Equal(123.0R, DoubleType.FromObject(CByte(123)))
      Assert.Equal(123.0R, DoubleType.FromObject(CShort(Fix(123))))
      Assert.Equal(123.0R, DoubleType.FromObject(CInt(Fix(123))))
      Assert.Equal(123.0R, DoubleType.FromObject(CLng(Fix(123))))
      Assert.Equal(123.0R, DoubleType.FromObject(CSng(123)))
      Assert.Equal(123.0R, DoubleType.FromObject(CDbl(123)))
      Assert.Equal(123.0R, DoubleType.FromObject(CDec(123)))
      Assert.Equal(123.0R, DoubleType.FromObject("123"))
    End Sub

    <Fact>
    Public Sub FromObject_Invalid()
      Assert.Throws(Of InvalidCastException)(Function() DoubleType.FromObject("1"c))
      Assert.Throws(Of InvalidCastException)(Function() DoubleType.FromObject(DateTime.MinValue))
      Assert.Throws(Of InvalidCastException)(Function() DoubleType.FromObject(Guid.Empty))
    End Sub

    <Theory>
    <InlineData("123", 123)>
    <InlineData("¤123", 123)>
    Public Sub Parse(value As String, expected As Double)
      Assert.Equal(expected, DoubleType.Parse(value, CultureInfo.InvariantCulture.NumberFormat))
    End Sub

    <Fact>
    Public Sub Parse_Invalid()
      Assert.Throws(Of ArgumentNullException)(Function() DoubleType.Parse(Nothing, Nothing))
      Assert.Throws(Of FormatException)(Function() DoubleType.Parse("abc", CultureInfo.InvariantCulture.NumberFormat))
    End Sub

  End Class

End Namespace