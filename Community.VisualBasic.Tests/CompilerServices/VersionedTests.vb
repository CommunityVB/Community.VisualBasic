' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict Off

Imports System.Collections.Generic
Imports Community.VisualBasic.CompilerServices
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class VersionedTests

    <Theory>
    <MemberData(NameOf(CallByName_TestData))>
    Public Sub CallByName(instance As Object, methodName As String, useCallType As CallType, args As Object(), getResult As Func(Of Object, Object), expected As Object)
      Assert.Equal(If(getResult Is Nothing, expected, Nothing), Versioned.CallByName(instance, methodName, useCallType, args))
      If getResult IsNot Nothing Then
        Assert.Equal(expected, getResult(instance)())
      End If
    End Sub

    <Theory>
    <MemberData(NameOf(CallByName_ArgumentException_TestData))>
    Public Sub CallByName_ArgumentException(instance As Object, methodName As String, useCallType As CallType, args As Object())
      Assert.Throws(Of ArgumentException)(Function() Versioned.CallByName(instance, methodName, useCallType, args))
    End Sub

    <Theory>
    <MemberData(NameOf(CallByName_MissingMemberException_TestData))>
    Public Sub CallByName_MissingMemberException(instance As Object, methodName As String, useCallType As CallType, args As Object())
      Assert.Throws(Of MissingMemberException)(Function() Versioned.CallByName(instance, methodName, useCallType, args))
    End Sub

    Public Shared Iterator Function CallByName_TestData() As IEnumerable(Of Object())
      Yield New Object() {New [Class], "Method", CallType.Method, New Object() {1, 2}, Nothing, 3}
      Yield New Object() {New [Class], "Method", CallType.[Get], New Object() {2, 3}, Nothing, 5}
      Yield New Object() {New [Class], "P", CallType.[Get], Array.Empty(Of Object)(), Nothing, 0}
      Yield New Object() {New [Class], "Item", CallType.[Get], New Object() {2}, Nothing, 2}
      Yield New Object() {New [Class], "P", CallType.[Set], New Object() {3}, New Func(Of Object, Object)(Function(obj) CType(obj, [Class]).Value), 3}
      Yield New Object() {New [Class], "Item", CallType.[Let], New Object() {4, 5}, New Func(Of Object, Object)(Function(obj) CType(obj, [Class]).Value), 9}
    End Function

    Public Shared Iterator Function CallByName_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, CType(Nothing, CallType), Array.Empty(Of Object)()}
      Yield New Object() {New [Class], "Method", CType(Nothing, CallType), New Object() {1, 2}}
      Yield New Object() {New [Class], "Method", CType(Integer.MaxValue, CallType), New Object() {1, 2}}
    End Function

    Public Shared Iterator Function CallByName_MissingMemberException_TestData() As IEnumerable(Of Object())
      Yield New Object() {New [Class], "Method", CallType.Method, Array.Empty(Of Object)()}
      Yield New Object() {New [Class], "Q", CallType.[Get], Array.Empty(Of Object)()}
    End Function

    Private NotInheritable Class [Class]

      Public Value As Integer

#Disable Warning CA1822 ' Mark members as static

      Public Function Method(x As Integer, y As Integer) As Integer
        Return x + y
      End Function

#Enable Warning CA1822 ' Mark members as static

      Public Property P As Integer
        Get
          Return Value
        End Get
        Set(value As Integer)
          Me.Value = value
        End Set
      End Property

      Default Public Property Item(index As Object) As Object
        Get
          Return Value + CInt(Fix(index))
        End Get
        Set(value As Object)
          Me.Value = CInt(Fix(value)) + CInt(Fix(index))
        End Set
      End Property

    End Class

    <Theory>
    <InlineData(Nothing, False)>
    <InlineData("a"c, False)>
    <InlineData(1, True)>
    <InlineData("12x", False)>
    <InlineData("123", True)>
    <InlineData("1"c, True)>
    <InlineData("&O123", True)>
    <InlineData("&H123", True)>
    Public Sub IsNumeric(value1 As Object, expected As Boolean)
      Assert.Equal(expected, Versioned.IsNumeric(value1))
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("OBJECT", "System.Object")>
    <InlineData(" OBJECT ", "System.Object")>
    <InlineData("object", "System.Object")>
    <InlineData("custom", Nothing)>
    Public Sub SystemTypeName(value1 As String, expected As String)
      Assert.Equal(expected, Versioned.SystemTypeName(value1))
    End Sub

    <Theory>
    <MemberData(NameOf(TypeName_TestData))>
    Public Sub TypeName(expression As Object, expected As String)
      Assert.Equal(expected, Versioned.TypeName(expression))
    End Sub

    '<SkipOnMono("COM Interop not supported on Mono")>
    '<ConditionalTheory(GetType(PlatformDetection), NameOf(PlatformDetection.IsWindows), NameOf(PlatformDetection.IsNotWindowsNanoServer))>
    <Theory>
    <MemberData(NameOf(TypeName_ComObject_TestData))>
    Public Sub TypeName_ComObject(progId As String, expected As String)
      Dim type1 As Type = Type.GetTypeFromProgID(progId, True)
      Dim expression As Object = Activator.CreateInstance(type1)
      Assert.Equal(expected, Versioned.TypeName(expression))
    End Sub

    Public Shared Iterator Function TypeName_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, "Nothing"}
      Yield New Object() {New Object, "Object"}
      Yield New Object() {CType(Nothing, Boolean), "Boolean"}
      Yield New Object() {CType(Nothing, Char), "Char"}
      Yield New Object() {CType(Nothing, SByte), "SByte"}
      Yield New Object() {CType(Nothing, Byte), "Byte"}
      Yield New Object() {CType(Nothing, Short), "Short"}
      Yield New Object() {CType(Nothing, UShort), "UShort"}
      Yield New Object() {CType(Nothing, Integer), "Integer"}
      Yield New Object() {CType(Nothing, UInteger), "UInteger"}
      Yield New Object() {CType(Nothing, Long), "Long"}
      Yield New Object() {CType(Nothing, ULong), "ULong"}
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

    Public Shared Iterator Function TypeName_ComObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {"ADODB.Stream", "Stream"}
      Yield New Object() {"MSXML2.DOMDocument", "DOMDocument"}
      Yield New Object() {"Scripting.Dictionary", "Dictionary"}
    End Function

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("System.Object", "Object")>
    <InlineData("Object", "Object")>
    <InlineData(" object ", "Object")>
    <InlineData("custom", Nothing)>
    Public Sub VbTypeName(value1 As String, expected As String)
      Assert.Equal(expected, Versioned.VbTypeName(value1))
    End Sub

  End Class

End Namespace