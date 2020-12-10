' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Class ObjectTypeTests

    <Theory>
    <MemberData(NameOf(AddObj_TestData))>
    Public Sub AddObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.AddObj(x, y))
    End Sub

    Public Shared Iterator Function AddObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(BitAndObj_TestData))>
    Public Sub BitAndObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.BitAndObj(x, y))
    End Sub

    Public Shared Iterator Function BitAndObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(BitOrObj_TestData))>
    Public Sub BitOrObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.BitOrObj(x, y))
    End Sub

    Public Shared Iterator Function BitOrObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(BitXorObj_TestData))>
    Public Sub BitXorObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.BitXorObj(x, y))
    End Sub

    Public Shared Iterator Function BitXorObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(DivObj_TestData))>
    Public Sub DivObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.DivObj(x, y))
    End Sub

    Public Shared Iterator Function DivObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, Double.NaN}
      Yield New Object() {0, 0, Double.NaN}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(IDivObj_TestData))>
    Public Sub IDivObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.IDivObj(x, y))
    End Sub

    Public Shared Iterator Function IDivObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {0, 1, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(IDivObj_DivideByZero_TestData))>
    Public Sub IDivObj_DivideByZero(x As Object, y As Object)
      Assert.Throws(Of DivideByZeroException)(Function() ObjectType.IDivObj(x, y))
    End Sub

    Public Shared Iterator Function IDivObj_DivideByZero_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing}
      Yield New Object() {0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(ModObj_TestData))>
    Public Sub ModObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.ModObj(x, y))
    End Sub

    Public Shared Iterator Function ModObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {0, 1, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(ModObj_DivideByZero_TestData))>
    Public Sub ModObj_DivideByZero(x As Object, y As Object)
      Assert.Throws(Of DivideByZeroException)(Function() ObjectType.ModObj(x, y))
    End Sub

    Public Shared Iterator Function ModObj_DivideByZero_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing}
      Yield New Object() {0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(MulObj_TestData))>
    Public Sub MulObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.MulObj(x, y))
    End Sub

    Public Shared Iterator Function MulObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(NegObj_TestData))>
    Public Sub NegObj(obj As Object, expected As Object)
      Assert.Equal(expected, ObjectType.NegObj(obj))
    End Sub

    Public Shared Iterator Function NegObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 0}
      Yield New Object() {0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(NotObj_TestData))>
    Public Sub NotObj(obj As Object, expected As Object)
      Assert.Equal(expected, ObjectType.NotObj(obj))
    End Sub

    Public Shared Iterator Function NotObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, -1}
      Yield New Object() {0, -1}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(PlusObj_TestData))>
    Public Sub PlusObj(obj As Object, expected As Object)
      Assert.Equal(expected, ObjectType.PlusObj(obj))
    End Sub

    Public Shared Iterator Function PlusObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 0}
      Yield New Object() {0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(PowObj_TestData))>
    Public Sub PowObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.PowObj(x, y))
    End Sub

    Public Shared Iterator Function PowObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 1.0}
      Yield New Object() {0, 0, 1.0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(ShiftLeftObj_TestData))>
    Public Sub ShiftLeftObj(obj As Object, amount As Integer, expected As Object)
      Assert.Equal(expected, ObjectType.ShiftLeftObj(obj, amount))
    End Sub

    Public Shared Iterator Function ShiftLeftObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(ShiftRightObj_TestData))>
    Public Sub ShiftRightObj(obj As Object, amount As Integer, expected As Object)
      Assert.Equal(expected, ObjectType.ShiftRightObj(obj, amount))
    End Sub

    Public Shared Iterator Function ShiftRightObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(StrCatObj_TestData))>
    Public Sub StrCatObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.StrCatObj(x, y))
    End Sub

    Public Shared Iterator Function StrCatObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, ""}
      Yield New Object() {0, 0, "00"}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(SubObj_TestData))>
    Public Sub SubObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.SubObj(x, y))
    End Sub

    Public Shared Iterator Function SubObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0}
      Yield New Object() {0, 0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(XorObj_TestData))>
    Public Sub XorObj(x As Object, y As Object, expected As Object)
      Assert.Equal(expected, ObjectType.XorObj(x, y))
    End Sub

    Public Shared Iterator Function XorObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, False}
      Yield New Object() {0, 0, False}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(GetObjectValuePrimitive_TestData))>
    Public Sub GetObjectValuePrimitive(obj As Object, expected As Object)
      Assert.Equal(expected, ObjectType.GetObjectValuePrimitive(obj))
    End Sub

    Public Shared Iterator Function GetObjectValuePrimitive_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing}
      Yield New Object() {0, 0}
      ' Add more...
    End Function

    <Theory>
    <MemberData(NameOf(LikeObj_TestData))>
    Public Sub LikeObj(left As Object, right As Object, expectedBinaryCompare As Object, expectedTextCompare As Object)
      Assert.Equal(expectedBinaryCompare, ObjectType.LikeObj(left, right, CompareMethod.Binary))
      Assert.Equal(expectedTextCompare, ObjectType.LikeObj(left, right, CompareMethod.Text))
    End Sub

    Public Shared Iterator Function LikeObj_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, True, True}
      Yield New Object() {Array.Empty(Of Char)(), Nothing, True, True}
      Yield New Object() {"", Nothing, True, True}
      Yield New Object() {"a3", New Char() {"A"c, "#"c}, False, True}
      Yield New Object() {New Char() {"A"c, "3"c}, "a#", False, True}
      Yield New Object() {"", "*", True, True}
      Yield New Object() {"", "?", False, False}
      Yield New Object() {"a", "?", True, True}
      Yield New Object() {"a3", "[A-Z]#", False, True}
      Yield New Object() {"A3", "[a-a]#", False, True}
    End Function

    <Theory>
    <MemberData(NameOf(LikeObj_NullReference_TestData))>
    Public Sub LikeObj_NullReference(left As Object, right As Object)
      Assert.Throws(Of NullReferenceException)(Function() ObjectType.LikeObj(left, right, CompareMethod.Binary))
      Assert.Throws(Of NullReferenceException)(Function() ObjectType.LikeObj(left, right, CompareMethod.Text))
    End Sub

    Public Shared Iterator Function LikeObj_NullReference_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, New Char() {"*"c}}
      Yield New Object() {Nothing, "*"}
    End Function

    <Theory>
    <MemberData(NameOf(ObjTst_TestData))>
    Public Sub ObjTst(x As Object, y As Object, textCompare As Boolean, expected As Object)
      Assert.Equal(expected, ObjectType.ObjTst(x, y, textCompare))
    End Sub

    Public Shared Iterator Function ObjTst_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, 0, 0}
      Yield New Object() {Nothing, "", 0, 0}
      Yield New Object() {"", Nothing, 0, 0}
      Yield New Object() {Nothing, "a", -1, -1}
      Yield New Object() {"a", Nothing, 1, 1}
      Yield New Object() {"", "a", -97, -1}
      Yield New Object() {"a", "", 97, 1}
      Yield New Object() {"a", "a", 0, 0}
      Yield New Object() {"a", "b", -1, -1}
      Yield New Object() {"b", "a", 1, 1}
      Yield New Object() {"a", "ABC", 32, If(PlatformDetection.IsInvariantGlobalization, -2, -1)}
      Yield New Object() {"ABC", "a", -32, If(PlatformDetection.IsInvariantGlobalization, 2, 1)}
      Yield New Object() {"abc", "ABC", 32, 0}

    End Function

  End Class

End Namespace