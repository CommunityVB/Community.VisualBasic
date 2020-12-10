' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Module StructUtilsTestData

    Public Function RecordsAndLength() As TheoryData(Of Object, Integer)
      Return New TheoryData(Of Object, Integer) From {
                  {Nothing, 0},
                  {New Struct_Empty, 0},
                  {New Struct_T(Of Single), 4},
                  {New Struct_T(Of Double), 8},
                  {New Struct_T(Of Short), 2},
                  {New Struct_T(Of Integer), 4},
                  {New Struct_T(Of Byte), 1},
                  {New Struct_T(Of Long), 8},
                  {New Struct_T(Of DateTime), 8},
                  {New Struct_T(Of Boolean), 2},
                  {New Struct_T(Of Decimal), 16},
                  {New Struct_T(Of Char), 2},
                  {New Struct_T(Of String), 4},
                  {New Struct_ArrayT(Of Byte)(elementCount:=10), 4},
                  {New Struct_ArrayT(Of Integer)(elementCount:=10), 4},
                  {New Struct_FixedArrayT10(Of Byte), 10},
                  {New Struct_FixedArrayT10(Of Integer), 40},
                  {New Struct_FixedArrayT10x20(Of Byte), 200},
                  {New Struct_FixedArrayT10x20(Of Integer), 800},
                  {New Struct_FixedString10, 10},
                  {New Struct_PrivateInt, 0},
                  {New Struct_MultipleWithAlignment, 22}}
    End Function

    Public Structure Struct_Empty
    End Structure

    Public Structure Struct_T(Of T)
      Public x As T
    End Structure

    Public Structure Struct_ArrayT(Of T)

      Public Sub New(elementCount As Integer)
        x = New T(elementCount - 1) {}
      End Sub

      Public x As T()

    End Structure

    Public Structure Struct_FixedArrayT10(Of T)
      <VBFixedArray(9)> Public x As T()
    End Structure

    Public Structure Struct_FixedArrayT10x20(Of T)
      <VBFixedArray(9, 19)> Public x As T()
    End Structure

    Public Structure Struct_FixedString10
      <VBFixedString(10)> Public x As String
    End Structure

    Public Structure Struct_PrivateInt
#Disable Warning IDE0051 ' Remove unused private members
      Private ReadOnly x As Integer
#Enable Warning IDE0051 ' Remove unused private members
    End Structure

    Public Structure Struct_MultipleWithAlignment
      Public b As Byte
      Public c As Char
      <VBFixedString(3)> Public s As String
      Public d As Decimal
    End Structure

  End Module

End Namespace