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

  Public Class DecimalTypeTests

    <Theory>
    <InlineData(True, -1)>
    <InlineData(False, 0)>
    Public Sub FromBoolean(value As Boolean, expected As Decimal)
      Assert.Equal(expected, DecimalType.FromBoolean(value))
    End Sub

    <Theory>
    <InlineData(Nothing, 0)>
    <InlineData("&H123", 291)>
    <InlineData("&O123", 83)>
    <InlineData("123", 123)>
    Public Sub FromString(value As String, expected As Decimal)
      Assert.Equal(expected, DecimalType.FromString(value))
    End Sub

    <Fact>
    Public Sub FromString_Invalid()
      Assert.Throws(Of OverflowException)(Function() DecimalType.FromString("9999999999999999999999999999999999999"))
      Assert.Throws(Of InvalidCastException)(Function() DecimalType.FromString("abc"))
    End Sub

    <Fact>
    Public Sub FromObject()
      Assert.Equal(0D, DecimalType.FromObject(Nothing))
      Assert.Equal(-1D, DecimalType.FromObject(True))
      Assert.Equal(123D, DecimalType.FromObject(CByte(123)))
      Assert.Equal(123D, DecimalType.FromObject(CShort(Fix(123))))
      Assert.Equal(123D, DecimalType.FromObject(CInt(Fix(123))))
      Assert.Equal(123D, DecimalType.FromObject(CLng(Fix(123))))
      Assert.Equal(123D, DecimalType.FromObject(CSng(123)))
      Assert.Equal(123D, DecimalType.FromObject(CDbl(123)))
      Assert.Equal(123D, DecimalType.FromObject(CDec(123)))
      Assert.Equal(123D, DecimalType.FromObject("123"))
    End Sub

    <Fact>
    Public Sub FromObject_Invalid()
      Assert.Throws(Of InvalidCastException)(Function() DecimalType.FromObject("1"c))
      Assert.Throws(Of InvalidCastException)(Function() DecimalType.FromObject(DateTime.MinValue))
      Assert.Throws(Of InvalidCastException)(Function() DecimalType.FromObject(Guid.Empty))
    End Sub

    <Theory>
    <InlineData("123", 123)>
    <InlineData("¤123", 123)>
    Public Sub Parse(value As String, expected As Decimal)
      Assert.Equal(expected, DecimalType.Parse(value, CultureInfo.InvariantCulture.NumberFormat))
    End Sub

    <Fact>
    Public Sub Parse_Invalid()
      Assert.Throws(Of ArgumentNullException)(Function() DecimalType.Parse(Nothing, Nothing))
      Assert.Throws(Of FormatException)(Function() DecimalType.Parse("abc", CultureInfo.InvariantCulture.NumberFormat))
    End Sub

  End Class

End Namespace