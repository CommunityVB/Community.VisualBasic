' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Namespace Global.Community.VisualBasic.Tests

  Public Class ConvertibleWrapper
    Implements IConvertible

    Public ReadOnly Property Value As IConvertible

    Public Sub New(value1 As IConvertible)
      Value = value1
    End Sub

    Public Function GetTypeCode() As TypeCode Implements System.IConvertible.GetTypeCode
      Return Value.GetTypeCode()
    End Function

    Public Function ToBoolean(provider As IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean
      Return Value.ToBoolean(provider)
    End Function

    Public Function ToByte(provider As IFormatProvider) As Byte Implements System.IConvertible.ToByte
      Return Value.ToByte(provider)
    End Function

    Public Function ToChar(provider As IFormatProvider) As Char Implements System.IConvertible.ToChar
      Return Value.ToChar(provider)
    End Function
    Public Function ToDateTime(provider As IFormatProvider) As DateTime Implements System.IConvertible.ToDateTime
      Return Value.ToDateTime(provider)
    End Function
    Public Function ToDecimal(provider As IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal
      Return Value.ToDecimal(provider)
    End Function
    Public Function ToDouble(provider As IFormatProvider) As Double Implements System.IConvertible.ToDouble
      Return Value.ToDouble(provider)
    End Function
    Public Function ToInt16(provider As IFormatProvider) As Short Implements System.IConvertible.ToInt16
      Return Value.ToInt16(provider)
    End Function
    Public Function ToInt32(provider As IFormatProvider) As Integer Implements System.IConvertible.ToInt32
      Return Value.ToInt32(provider)
    End Function
    Public Function ToInt64(provider As IFormatProvider) As Long Implements System.IConvertible.ToInt64
      Return Value.ToInt64(provider)
    End Function
    Public Function ToSByte(provider As IFormatProvider) As SByte Implements System.IConvertible.ToSByte
      Return Value.ToSByte(provider)
    End Function
    Public Function ToSingle(provider As IFormatProvider) As Single Implements System.IConvertible.ToSingle
      Return Value.ToSingle(provider)
    End Function
    Public Function ToString(provider As IFormatProvider) As String Implements System.IConvertible.ToString
      Return Value.ToString(provider)
    End Function
    Public Function ToType(conversionType As Type, provider As IFormatProvider) As Object Implements System.IConvertible.ToType
      Return Value.ToType(conversionType, provider)
    End Function
    Public Function ToUInt16(provider As IFormatProvider) As UShort Implements System.IConvertible.ToUInt16
      Return Value.ToUInt16(provider)
    End Function
    Public Function ToUInt32(provider As IFormatProvider) As UInteger Implements System.IConvertible.ToUInt32
      Return Value.ToUInt32(provider)
    End Function
    Public Function ToUInt64(provider As IFormatProvider) As ULong Implements System.IConvertible.ToUInt64
      Return Value.ToUInt64(provider)
    End Function

  End Class

End Namespace