' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict Off

Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Class LateBindingTests

    <Theory>
    <MemberData(NameOf(LateCall_TestData))>
    Public Sub LateCall(obj As Object, objType As Type, name As String, args As Object(), paramNames As String(), copyBack As Boolean(), getResult As Func(Of Object, Object), expected As Object)
      LateBinding.LateCall(obj, objType, name, args, paramNames, copyBack)
      Assert.Equal(expected, getResult(obj))
    End Sub

    <Theory>
    <MemberData(NameOf(LateGet_TestData))>
    Public Sub LateGet(obj As Object, objType As Type, name As String, args As Object(), paramNames As String(), copyBack As Boolean(), expected As Object)
      Assert.Equal(expected, LateBinding.LateGet(obj, objType, name, args, paramNames, copyBack))
    End Sub

    <Theory>
    <MemberData(NameOf(LateSet_TestData))>
    Public Sub LateSet(obj As Object, objType As Type, name As String, args As Object(), paramNames As String(), getResult As Func(Of Object, Object), expected As Object)
      LateBinding.LateSet(obj, objType, name, args, paramNames)
      Assert.Equal(expected, getResult(obj))
    End Sub

    <Theory>
    <MemberData(NameOf(LateSetComplex_TestData))>
    Public Sub LateSetComplex(obj As Object, objType As Type, name As String, args As Object(), paramNames As String(), missing As Boolean, valueType As Boolean)
      If missing Then
        Assert.Throws(Of MissingMemberException)(Sub() LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, False))
        Assert.Throws(Of MissingMemberException)(Sub() LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, True))
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, False)
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, True)
      ElseIf valueType Then
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, False)
        Assert.Throws(Of Exception)(Sub() LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, True))
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, False)
        Assert.Throws(Of Exception)(Sub() LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, True))
      Else
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, False)
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, False, True)
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, False)
        LateBinding.LateSetComplex(obj, objType, name, args, paramNames, True, True)
      End If
    End Sub

    <Theory>
    <MemberData(NameOf(LateIndexGet_TestData))>
    Public Sub LateIndexGet(obj As Object, args As Object(), paramNames As String(), expected As Object)
      Assert.Equal(expected, LateBinding.LateIndexGet(obj, args, paramNames))
    End Sub

    <Theory>
    <MemberData(NameOf(LateIndexSet_TestData))>
    Public Sub LateIndexSet(obj As Object, args As Object(), paramNames As String(), getResult As Func(Of Object, Object), expected As Object)
      LateBinding.LateIndexSet(obj, args, paramNames)
      Assert.Equal(expected, getResult(obj))
    End Sub

    <Theory>
    <MemberData(NameOf(LateIndexSet_MissingMember_TestData))>
    Public Sub LateIndexSet_MissingMember(obj As Object, args As Object(), paramNames As String())
      Assert.Throws(Of MissingMemberException)(Sub() LateBinding.LateIndexSet(obj, args, paramNames))
    End Sub

    <Theory>
    <MemberData(NameOf(LateIndexSetComplex_TestData))>
    Public Sub LateIndexSetComplex(obj As Object, args As Object(), paramNames As String(), missing As Boolean, valueType As Boolean)
      If missing Then
        Assert.Throws(Of MissingMemberException)(Sub() LateBinding.LateIndexSetComplex(obj, args, paramNames, False, False))
        Assert.Throws(Of MissingMemberException)(Sub() LateBinding.LateIndexSetComplex(obj, args, paramNames, False, True))
        LateBinding.LateIndexSetComplex(obj, args, paramNames, True, False)
        LateBinding.LateIndexSetComplex(obj, args, paramNames, True, True)
      ElseIf valueType Then
        LateBinding.LateIndexSetComplex(obj, args, paramNames, False, False)
        Assert.Throws(Of Exception)(Sub() LateBinding.LateIndexSetComplex(obj, args, paramNames, False, True))
        LateBinding.LateIndexSetComplex(obj, args, paramNames, True, False)
        Assert.Throws(Of Exception)(Sub() LateBinding.LateIndexSetComplex(obj, args, paramNames, True, True))
      Else
        LateBinding.LateIndexSetComplex(obj, args, paramNames, False, False)
        LateBinding.LateIndexSetComplex(obj, args, paramNames, False, True)
        LateBinding.LateIndexSetComplex(obj, args, paramNames, True, False)
        LateBinding.LateIndexSetComplex(obj, args, paramNames, True, True)
      End If
    End Sub

    Public Shared Iterator Function LateCall_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, GetType(StaticClass), "M", Array.Empty(Of Object)(), Array.Empty(Of String)(), Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(__) StaticClass.Value), 1}
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(__) StaticClass.Value), 2}
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {4, 5}, New String() {"a", "b"}, Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(__) StaticClass.Value), 5}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", Array.Empty(Of Object)(), Array.Empty(Of String)(), Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 1}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 2}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {4, 5}, New String() {"a", "b"}, Array.Empty(Of Boolean)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 5}
    End Function

    Public Shared Iterator Function LateGet_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, GetType(StaticClass), "M", Array.Empty(Of Object)(), Array.Empty(Of String)(), Array.Empty(Of Boolean)(), 1}
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), Array.Empty(Of Boolean)(), 2}
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {4, 5}, New String() {"a", "b"}, Array.Empty(Of Boolean)(), 5}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", Array.Empty(Of Object)(), Array.Empty(Of String)(), Array.Empty(Of Boolean)(), 1}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), Array.Empty(Of Boolean)(), 2}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {4, 5}, New String() {"a", "b"}, Array.Empty(Of Boolean)(), 5}
    End Function

    Public Shared Iterator Function LateSet_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), New Func(Of Object, Object)(Function(__) StaticClass.Value), 2}
      Yield New Object() {Nothing, GetType(StaticClass), "M", New Object() {4, 5}, New String() {"a"}, New Func(Of Object, Object)(Function(__) StaticClass.Value), 5}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {2, 3}, Array.Empty(Of String)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 2}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "M", New Object() {4, 5}, New String() {"a"}, New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 5}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "P", New Object() {6}, Array.Empty(Of String)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 6}
    End Function

    Public Shared Iterator Function LateSetComplex_TestData() As IEnumerable(Of Object())
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "P", New Object() {1}, Array.Empty(Of String)(), False, False}
      Yield New Object() {New InstanceClass, GetType(InstanceClass), "Q", New Object() {2}, Array.Empty(Of String)(), True, False}
      Yield New Object() {New InstanceStruct, GetType(InstanceStruct), "P", New Object() {3}, Array.Empty(Of String)(), False, True}
      Yield New Object() {New InstanceStruct, GetType(InstanceStruct), "Q", New Object() {4}, Array.Empty(Of String)(), True, True}
    End Function

    Public Shared Iterator Function LateIndexGet_TestData() As IEnumerable(Of Object())
      Yield New Object() {New InstanceClass, New Object() {1}, Array.Empty(Of String)(), 2}
      Yield New Object() {New InstanceClass, New Object() {2, 3}, Array.Empty(Of String)(), 5}
      Yield New Object() {New InstanceStruct, New Object() {4}, Array.Empty(Of String)(), 8}
      Yield New Object() {New InstanceStruct, New Object() {5, 6}, Array.Empty(Of String)(), 11}
    End Function

    Public Shared Iterator Function LateIndexSet_TestData() As IEnumerable(Of Object())
      Yield New Object() {New InstanceClass, New Object() {1, 2, 3}, Array.Empty(Of String)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceClass).Value), 6}
      Yield New Object() {New InstanceStruct, New Object() {4, 5, 6}, Array.Empty(Of String)(), New Func(Of Object, Object)(Function(obj) CType(obj, InstanceStruct).Value), 15}
    End Function

    Public Shared Iterator Function LateIndexSet_MissingMember_TestData() As IEnumerable(Of Object())
      Yield New Object() {New StaticClass, New Object() {1}, Array.Empty(Of String)()}
      Yield New Object() {New InstanceClass, New Object() {2, 3}, Array.Empty(Of String)()}
      Yield New Object() {New InstanceStruct, New Object() {5, 6}, Array.Empty(Of String)()}
    End Function

    Public Shared Iterator Function LateIndexSetComplex_TestData() As IEnumerable(Of Object())
      Yield New Object() {New InstanceClass, New Object() {1, 2, 3}, New String() {"x", "y"}, False, False}
      Yield New Object() {New InstanceClass, New Object() {4, 5}, New String() {"i"}, True, False}
      Yield New Object() {New InstanceStruct, New Object() {6, 7, 8}, New String() {"x", "y"}, False, True}
      Yield New Object() {New InstanceStruct, New Object() {9, 10}, New String() {"i"}, True, True}
    End Function

    Private NotInheritable Class StaticClass

      Public Shared Value As Integer

      Public Shared Function M() As Integer
        Value = 1
        Return Value
      End Function

      Public Shared Function M(x As Integer, y As Integer) As Object
        If y <> 0 Then
        End If
        Value = x
        Return Value
      End Function

      Public Shared Function M(a As Object, b As Object) As Object
        If a IsNot Nothing Then
        End If
        Value = CInt(Fix(b))
        Return Value
      End Function

    End Class

    Private NotInheritable Class InstanceClass

      Public Value As Integer

      Public Property P As Integer
        Get
          Return Value
        End Get
        Set(value As Integer)
          Me.Value = value
        End Set
      End Property

      Public ReadOnly Property Q As Integer
        Get
          Return Value
        End Get
      End Property

      Public Function M() As Integer
        Me.Value = 1
        Return Me.Value 'result
      End Function

      Public Function M(x As Integer, y As Integer) As Object
        If y <> 0 Then
        End If
        Me.Value = x
        Return Me.Value
      End Function

      Public Function M(a As Object, b As Object) As Object
        If a IsNot Nothing Then
        End If
        Me.Value = CInt(Fix(b))
        Return Me.Value
      End Function

#Disable Warning CA1822 ' Mark members as static

      Default Public ReadOnly Property Item(i As Integer) As Integer
        Get
          Return i * 2
        End Get
      End Property

#Enable Warning CA1822 ' Mark members as static

      Default Public Property Item(x As Object, y As Object) As Integer
        Get
          Return CInt(Fix(x)) + CInt(Fix(y))
        End Get
        Set(value As Integer)
          Me.Value = CInt(Fix(x)) + CInt(Fix(y)) + value
        End Set
      End Property

    End Class

    Private Structure InstanceStruct

      Private m_value As Integer

      Public Property Value As Integer
        Get
          Return m_value
        End Get
        Set(value1 As Integer)
          m_value = value1
        End Set
      End Property

      Public Property P As Integer
        Get
          Return Value
        End Get
        Set(value1 As Integer)
          Value = value1
        End Set
      End Property

      Public ReadOnly Property Q As Integer
        Get
          Return Value
        End Get
      End Property

#Disable Warning CA1822 ' Mark members as static

      Default Public ReadOnly Property Item(i As Integer) As Integer
        Get
          Return i * 2
        End Get
      End Property

#Enable Warning CA1822 ' Mark members as static

      Default Public Property Item(x As Object, y As Object) As Integer
        Get
          Return CInt(Fix(x)) + CInt(Fix(y))
        End Get
        Set(value1 As Integer)
          Value = CInt(Fix(x)) + CInt(Fix(y)) + value1
        End Set
      End Property

    End Structure

  End Class

End Namespace