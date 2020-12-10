' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class InformationTests

    <Theory>
    <InlineData(New Integer() {}, True)>
    <InlineData(Nothing, False)>
    Public Sub IsArray(value As Object, expected As Boolean)
      Assert.Equal(expected, Information.IsArray(value))
    End Sub

    <Fact>
    Public Sub IsDate()
      Assert.[True](Information.IsDate(DateTime.MinValue))
      Assert.[False](Information.IsDate(Nothing))
      Assert.[True](Information.IsDate("2018-01-01"))
      Assert.[False](Information.IsDate("abc"))
      Assert.[False](Information.IsDate(Guid.Empty))
    End Sub

    <Fact>
    Public Sub IsDBNull()
      Assert.[True](Information.IsDBNull(DBNull.Value))
      Assert.[False](Information.IsDBNull(Nothing))
      Assert.[False](Information.IsDBNull("abc"))
    End Sub

    <Theory>
    <InlineData(Nothing, True)>
    <InlineData("abc", False)>
    Public Sub IsNothing(value As Object, expected As Boolean)
      Assert.Equal(expected, Information.IsNothing(value))
    End Sub

    <Fact>
    Public Sub IsError()
      Assert.[False](Information.IsError(Nothing))
      Assert.[True](Information.IsError(New Exception))
      Assert.[False](Information.IsError("abc"))
    End Sub

    <Theory>
    <InlineData(Nothing, True)>
    <InlineData("abc", True)>
    <InlineData(1, False)>
    Public Sub IsReference(value As Object, expected As Boolean)
      Assert.Equal(expected, Information.IsReference(value))
    End Sub

    <Fact>
    Public Sub LBound()
      Assert.Equal(0, Information.LBound(New Integer(0) {}))
      Assert.Equal(5, Information.LBound(Array.CreateInstance("abc".[GetType](), New Integer() {1}, New Integer() {5})))
      Assert.Equal(6, Information.LBound(Array.CreateInstance("abc".[GetType](), New Integer() {1, 1}, New Integer() {5, 6}), 2))
    End Sub

    <Fact>
    Public Sub LBound_Invalid()
      Assert.Throws(Of ArgumentNullException)(Function() Information.LBound(Nothing))
      Assert.Throws(Of RankException)(Function() Information.LBound(Array.Empty(Of Integer)(), 0))
      Assert.Throws(Of RankException)(Function() Information.LBound(Array.Empty(Of Integer)(), 2))
    End Sub

    <Fact>
    Public Sub UBound()
      Assert.Equal(-1, Information.UBound(Array.Empty(Of Integer)()))
      Assert.Equal(0, Information.UBound(New Integer(0) {}))
      Assert.Equal(5, Information.UBound(Array.CreateInstance("abc".[GetType](), New Integer() {1}, New Integer() {5})))
      Assert.Equal(6, Information.UBound(Array.CreateInstance("abc".[GetType](), New Integer() {1, 1}, New Integer() {5, 6}), 2))
    End Sub

    <Fact>
    Public Sub UBound_Invalid()
      Assert.Throws(Of ArgumentNullException)(Function() Information.UBound(Nothing))
      Assert.Throws(Of RankException)(Function() Information.UBound(Array.Empty(Of Integer)(), 0))
      Assert.Throws(Of RankException)(Function() Information.UBound(Array.Empty(Of Integer)(), 2))
    End Sub

    <Theory>
    <InlineData(4, 128)>
    <InlineData(0, 0)>
    <InlineData(15, 16777215)>
    Public Sub QBColor(value As Integer, expected As Integer)
      Assert.Equal(expected, Information.QBColor(value))
    End Sub

    <Fact>
    Public Sub QBColor_Invalid()
      Assert.Throws(Of ArgumentException)(Function() Information.QBColor(-1))
    End Sub

    <Theory>
    <InlineData(0, 0, 0, 0)>
    <InlineData(255, 255, 255, 16777215)>
    <InlineData(300, 400, 500, 16777215)>
    Public Sub RGB(red As Integer, green As Integer, blue As Integer, expected As Integer)
      Assert.Equal(expected, Information.RGB(red, green, blue))
    End Sub

    <Fact>
    Public Sub RGB_Invalid()
      Assert.Throws(Of ArgumentException)(Function() Information.RGB(-1, -1, -1))
      Assert.Throws(Of ArgumentException)(Function() Information.RGB(1, -1, -1))
      Assert.Throws(Of ArgumentException)(Function() Information.RGB(1, 1, -1))
    End Sub

    <Theory>
    <InlineData(Nothing, VariantType.[Object])>
    <InlineData(New Integer() {}, VariantType.Array Or VariantType.[Integer])>
    <InlineData(StringComparison.Ordinal, VariantType.[Integer])>
    <InlineData("abc", VariantType.[String])>
    <InlineData(1S, VariantType.[Short])>
    <InlineData(1L, VariantType.[Long])>
    <InlineData(CSng(1), VariantType.[Single])>
    <InlineData(CDbl(1), VariantType.[Double])>
    <InlineData(True, VariantType.[Boolean])>
    <InlineData(CByte(1), VariantType.[Byte])>
    <InlineData("a"c, VariantType.[Char])>
    Public Sub VarType(value As Object, expected As VariantType)
      Assert.Equal(expected, Information.VarType(value))
    End Sub

    <Theory>
    <InlineData(Nothing, False)>
    <InlineData("a"c, False)>
    <InlineData(1, True)>
    <InlineData("12x", False)>
    <InlineData("123", True)>
    <InlineData("1"c, True)>
    <InlineData("&O123", True)>
    <InlineData("&H123", True)>
    Public Sub IsNumeric(value As Object, expected As Boolean)
      Assert.Equal(expected, Information.IsNumeric(value))
    End Sub

    <Fact>
    Public Sub IsNumeric_Invalid()
      Assert.Throws(Of NullReferenceException)(Function() Information.IsNumeric(New Char() {"1"c, "2"c, "3"c})) ' Bug compatible
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("OBJECT", "System.Object")>
    <InlineData(" OBJECT ", "System.Object")>
    <InlineData("object", "System.Object")>
    <InlineData("custom", Nothing)>
    Public Sub SystemTypeName(value As String, expected As String)
      Assert.Equal(expected, Information.SystemTypeName(value))
    End Sub

    <Theory>
    <MemberData(NameOf(TypeName_TestData))>
    Public Sub TypeName(expression As Object, expected As String)
      Assert.Equal(expected, Information.TypeName(expression))
    End Sub

    Public Shared Iterator Function TypeName_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, "Nothing"}
      Yield New Object() {New Object, "Object"}
      Yield New Object() {CType(Nothing, Boolean), "Boolean"}
      Yield New Object() {CType(Nothing, Char), "Char"}
      Yield New Object() {CType(Nothing, SByte), "SByte"}
      Yield New Object() {CType(Nothing, Byte), "Byte"}
      Yield New Object() {CType(Nothing, Short), "Short"}
      Yield New Object() {CType(Nothing, UShort), "UInt16"}
      Yield New Object() {CType(Nothing, Integer), "Integer"}
      Yield New Object() {CType(Nothing, UInteger), "UInt32"}
      Yield New Object() {CType(Nothing, Long), "Long"}
      Yield New Object() {CType(Nothing, ULong), "UInt64"}
      Yield New Object() {CType(Nothing, Single), "Single"}
      Yield New Object() {CType(Nothing, Double), "Double"}
      Yield New Object() {CType(Nothing, Decimal), "Decimal"}
      Yield New Object() {CType(Nothing, DateTime), "Date"}
      Yield New Object() {"", "String"}
      Yield New Object() {CType(Nothing, Object()), "Nothing"}
      Yield New Object() {Array.Empty(Of Object)(), "Object()"}
      Yield New Object() {New Char(,) {}, "Char(,)"}
      Yield New Object() {CType(Nothing, Integer?), "Nothing"}
      Yield New Object() {CType(0, Integer?), "Integer"}
    End Function

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("System.Object", "Object")>
    <InlineData("Object", "Object")>
    <InlineData(" object ", "Object")>
    <InlineData("custom", Nothing)>
    Public Sub VbTypeName(value As String, expected As String)
      Assert.Equal(expected, Information.VbTypeName(value))
    End Sub

  End Class

End Namespace