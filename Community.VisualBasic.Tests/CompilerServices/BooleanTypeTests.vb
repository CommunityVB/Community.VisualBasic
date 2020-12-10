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

  Public Class BooleanTypeTests

    <Fact>
    Public Sub NullData()
      ' Null is valid input for Boolean.FromObject
      Assert.Throws(Of InvalidCastException)(Function() BooleanType.FromString(Nothing))
    End Sub

    <Theory>
    <MemberData(NameOf(InvalidStringData))>
    Public Sub InvalidCastString(value As String)
      Assert.Throws(Of InvalidCastException)(Function() BooleanType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(InvalidStringData))>
    <MemberData(NameOf(InvalidObjectData))>
    Public Sub InvalidCastObject(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() BooleanType.FromObject(value))
    End Sub

    Public Shared ReadOnly Property InvalidStringData As TheoryData(Of String)
      Get
        Return New TheoryData(Of String) From {
            {""},
            {"23&"},
            {"abc"}}
      End Get
    End Property

    Public Shared ReadOnly Property InvalidObjectData As TheoryData(Of Object)
      Get
        Return New TheoryData(Of Object) From {
            {DateTime.Now},
            {"c"c},
            {Guid.Empty}}
      End Get
    End Property

    <Theory>
    <MemberData(NameOf(BoolStringData))>
    Public Sub FromString(expected As Boolean, value As String)
      Assert.Equal(expected, BooleanType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(BoolStringData))>
    <MemberData(NameOf(BoolObjectData))>
    Public Sub FromObject(expected As Boolean, value As Object)
      Assert.Equal(expected, BooleanType.FromObject(value))
    End Sub

    Public Shared ReadOnly Property BoolStringData As TheoryData(Of Boolean, String)
      Get
        Return New TheoryData(Of Boolean, String) From {
            {False, "0"},
            {False, "False"},
            {True, "True"},
            {True, "1"},
            {True, "1" & CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & "2"},
            {True, "2"},
            {True, "-1"},
            {False, "&H00"},
            {False, "&O00"},
            {True, "&H01"},
            {True, "&O01"},
            {True, "9999999999999999999999999999999999999"}}
      End Get
    End Property

    Public Shared ReadOnly Property BoolObjectData As TheoryData(Of Boolean, Object)
      Get
        Return New TheoryData(Of Boolean, Object) From {
            {False, 0},
            {False, Nothing},
            {False, False},
            {True, True},
            {True, 1},
            {True, "1" & CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & "2"},
            {True, 2},
            {True, -1},
            {False, CByte(0)},
            {True, CByte(1)},
            {False, CShort(Fix(0))},
            {True, CShort(Fix(1))},
            {False, CDbl(0)},
            {True, CDbl(1)},
            {False, CDec(0)},
            {True, CDec(1)}}
      End Get
    End Property

  End Class

End Namespace