' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Dynamic
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Class NewLateBindingTests

    Private NotInheritable Class OptionalValuesType
      Inherits DynamicObject

#Disable Warning CA1822 ' Mark members as static

      Public Function F1(Of T)(Optional p1 As T = CType(Nothing, T)) As Object
        Return $"{GetType(T)}, {ToString(p1)}"
      End Function

      Public Function F2(Of T)(Optional p1 As T = CType(Nothing, T), Optional p2 As Integer? = 2) As Object
        Return $"{GetType(T)}, {ToString(p1)}, {ToString(p2)}"
      End Function

      Public Function F3(Of T)(p1 As Object, Optional p2 As T = CType(Nothing, T), Optional p3 As Integer? = 3) As Object
        If p1 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p2)}, {ToString(p3)}"
      End Function

      Public Function F4(Of T)(p1 As Object, p2 As Object, Optional p3 As T = CType(Nothing, T), Optional p4 As Integer? = 4) As Object
        If p1 IsNot Nothing OrElse
           p2 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p3)}, {ToString(p4)}"
      End Function

      Public Function F5(Of T)(p1 As Object, p2 As Object, p3 As Object, Optional p4 As T = CType(Nothing, T), Optional p5 As Integer? = 5) As Object
        If p1 IsNot Nothing OrElse
           p2 IsNot Nothing OrElse
           p3 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p4)}, {ToString(p5)}"
      End Function

      Public Function F6(Of T)(p1 As Object, p2 As Object, p3 As Object, p4 As Object, Optional p5 As T = CType(Nothing, T), Optional p6 As Integer? = 6) As Object
        If p1 IsNot Nothing OrElse
           p2 IsNot Nothing OrElse
           p3 IsNot Nothing OrElse
           p4 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p5)}, {ToString(p6)}"
      End Function

      Public Function F7(Of T)(p1 As Object, p2 As Object, p3 As Object, p4 As Object, p5 As Object, Optional p6 As T = CType(Nothing, T), Optional p7 As Integer? = 7) As Object
        If p1 IsNot Nothing OrElse
           p2 IsNot Nothing OrElse
           p3 IsNot Nothing OrElse
           p4 IsNot Nothing OrElse
           p5 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p6)}, {ToString(p7)}"
      End Function

      Public Function F8(Of T)(p1 As Object, p2 As Object, p3 As Object, p4 As Object, p5 As Object, p6 As Object, Optional p7 As T = CType(Nothing, T), Optional p8 As Integer? = 8) As Object
        If p1 IsNot Nothing OrElse
           p2 IsNot Nothing OrElse
           p3 IsNot Nothing OrElse
           p4 IsNot Nothing OrElse
           p5 IsNot Nothing OrElse
           p6 IsNot Nothing Then
        End If
        Return $"{GetType(T)}, {ToString(p7)}, {ToString(p8)}"
      End Function

#Enable Warning CA1822 ' Mark members as static

      Private Shared Shadows Function ToString(obj As Object) As String
        Return If(obj?.ToString(), "null")
      End Function

    End Class

    Public Shared Iterator Function LateCall_OptionalValues_Data() As IEnumerable(Of Object())
      ' TODO Check: Local function was replaced with Lambda
      Dim CreateData As Func(Of String, Object(), Type(), String, Object()) = Function(memberName As String, arguments As Object(), typeArguments As Type(), expectedValue As String) As Object()
                                                                                Return New Object() {memberName, arguments, typeArguments, expectedValue}
                                                                              End Function
      ' If System.Type.Missing is used for a parameter with type parameter type,
      ' System.Reflection.Missing is used in type inference. This matches .NET Framework behavior.

      Yield CreateData("F1", New Object() {-1}, Nothing, "System.Int32, -1")
      Yield CreateData("F1", New Object() {Type.Missing}, Nothing, "System.Reflection.Missing, null")
      Yield CreateData("F1", New Object() {Type.Missing}, {GetType(Integer)}, "System.Int32, 0")

      Yield CreateData("F2", New Object() {1, -1}, Nothing, "System.Int32, 1, -1")
      Yield CreateData("F2", New Object() {1, Type.Missing}, Nothing, "System.Int32, 1, 2")
      Yield CreateData("F2", New Object() {Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 2")
      Yield CreateData("F2", New Object() {Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 2")

      Yield CreateData("F3", New Object() {1, 2, -1}, Nothing, "System.Int32, 2, -1")
      Yield CreateData("F3", New Object() {1, 2, Type.Missing}, Nothing, "System.Int32, 2, 3")
      Yield CreateData("F3", New Object() {1, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 3")
      Yield CreateData("F3", New Object() {1, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 3")

      Yield CreateData("F4", New Object() {1, 2, 3, -1}, Nothing, "System.Int32, 3, -1")
      Yield CreateData("F4", New Object() {1, 2, 3, Type.Missing}, Nothing, "System.Int32, 3, 4")
      Yield CreateData("F4", New Object() {1, 2, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 4")
      Yield CreateData("F4", New Object() {1, 2, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 4")

      Yield CreateData("F5", New Object() {1, 2, 3, 4, -1}, Nothing, "System.Int32, 4, -1")
      Yield CreateData("F5", New Object() {1, 2, 3, 4, Type.Missing}, Nothing, "System.Int32, 4, 5")
      Yield CreateData("F5", New Object() {1, 2, 3, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 5")
      Yield CreateData("F5", New Object() {1, 2, 3, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 5")

      Yield CreateData("F6", New Object() {1, 2, 3, 4, 5, -1}, Nothing, "System.Int32, 5, -1")
      Yield CreateData("F6", New Object() {1, 2, 3, 4, 5, Type.Missing}, Nothing, "System.Int32, 5, 6")
      Yield CreateData("F6", New Object() {1, 2, 3, 4, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 6")
      Yield CreateData("F6", New Object() {1, 2, 3, 4, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 6")

      Yield CreateData("F7", New Object() {1, 2, 3, 4, 5, 6, -1}, Nothing, "System.Int32, 6, -1")
      Yield CreateData("F7", New Object() {1, 2, 3, 4, 5, 6, Type.Missing}, Nothing, "System.Int32, 6, 7")
      Yield CreateData("F7", New Object() {1, 2, 3, 4, 5, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 7")
      Yield CreateData("F7", New Object() {1, 2, 3, 4, 5, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 7")

      Yield CreateData("F8", New Object() {1, 2, 3, 4, 5, 6, 7, -1}, Nothing, "System.Int32, 7, -1")
      Yield CreateData("F8", New Object() {1, 2, 3, 4, 5, 6, 7, Type.Missing}, Nothing, "System.Int32, 7, 8")
      Yield CreateData("F8", New Object() {1, 2, 3, 4, 5, 6, Type.Missing, Type.Missing}, Nothing, "System.Reflection.Missing, null, 8")
      Yield CreateData("F8", New Object() {1, 2, 3, 4, 5, 6, Type.Missing, Type.Missing}, {GetType(Integer)}, "System.Int32, 0, 8")

    End Function

    <Theory>
    <MemberData(NameOf(LateCall_OptionalValues_Data))>
    Public Sub LateCall_OptionalValues(memberName As String, arguments As Object(), typeArguments As Type(), expectedValue As String)
      ' NewLateBinding.LateCall() corresponds to a call to the member when using late binding:
      '   Dim instance = New OptionalValuesType()
      '   instance.Member(arguments)
      Dim actualValue As Object = NewLateBinding.LateCall(
          Instance:=New OptionalValuesType,
          Type:=Nothing,
          MemberName:=memberName,
          Arguments:=arguments,
          ArgumentNames:=Nothing,
          TypeArguments:=typeArguments,
          CopyBack:=Nothing,
          IgnoreReturn:=True)
      Assert.Equal(expectedValue, actualValue)
    End Sub

  End Class

End Namespace