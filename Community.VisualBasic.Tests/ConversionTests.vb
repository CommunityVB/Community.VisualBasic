' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class ConversionTests
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Byte_TestData))>
    Public Sub CTypeDynamic_Byte(expression As Object, expected As Object)
      CTypeDynamic(Of Byte)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_SByte_TestData))>
    Public Sub CTypeDynamic_SByte(expression As Object, expected As Object)
      CTypeDynamic(Of SByte)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_UShort_TestData))>
    Public Sub CTypeDynamic_UShort(expression As Object, expected As Object)
      CTypeDynamic(Of UShort)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Short_TestData))>
    Public Sub CTypeDynamic_Short(expression As Object, expected As Object)
      CTypeDynamic(Of Short)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_UInteger_TestData))>
    Public Sub CTypeDynamic_UInteger(expression As Object, expected As Object)
      CTypeDynamic(Of UInteger)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Integer_TestData))>
    Public Sub CTypeDynamic_Integer(expression As Object, expected As Object)
      CTypeDynamic(Of Integer)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_ULong_TestData))>
    Public Sub CTypeDynamic_ULong(expression As Object, expected As Object)
      CTypeDynamic(Of ULong)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Long_TestData))>
    Public Sub CTypeDynamic_Long(expression As Object, expected As Object)
      CTypeDynamic(Of Long)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Single_TestData))>
    Public Sub CTypeDynamic_Single(expression As Object, expected As Object)
      CTypeDynamic(Of Single)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Double_TestData))>
    Public Sub CTypeDynamic_Double(expression As Object, expected As Object)
      CTypeDynamic(Of Double)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Decimal_TestData))>
    Public Sub CTypeDynamic_Decimal(expression As Object, expected As Object)
      CTypeDynamic(Of Decimal)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_Char_TestData))>
    Public Sub CTypeDynamic_Char(expression As Object, expected As Object)
      CTypeDynamic(Of Char)(expression, expected)
    End Sub
    <Theory>
    <MemberData(NameOf(CTypeDynamic_String_TestData))>
    Public Sub CTypeDynamic_String(expression As Object, expected As Object)
      CTypeDynamic(Of String)(expression, expected)
    End Sub
    Private Shared Sub CTypeDynamic(Of T)(expression As Object, expected As Object)
      Assert.Equal(expected, Conversion.CTypeDynamic(expression, GetType(T)))
      Assert.Equal(expected, Conversion.CTypeDynamic(Of T)(expression))
    End Sub
    Public Shared Function CTypeDynamic_Byte_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToByte_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_SByte_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToSByte_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_UShort_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToUShort_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Short_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToShort_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_UInteger_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToUInteger_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Integer_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToInteger_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_ULong_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToULong_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Long_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToLong_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Single_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToSingle_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Double_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToDouble_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Decimal_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToDecimal_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_Char_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToChar_Object_TestData()
    End Function
    Public Shared Function CTypeDynamic_String_TestData() As IEnumerable(Of Object())
      Return ConversionsTests.ToString_Object_TestData()
    End Function
    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("", Nothing)>
    Public Sub CTypeDynamic_ArgumentException(expression As Object, targetType As Type)
      Assert.Throws(Of ArgumentException)(Function() Conversion.CTypeDynamic(expression, targetType))
    End Sub
    <Fact>
    Public Sub ErrorToString()
      Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(New IO.FileNotFoundException)
      Assert.NotNull(Conversion.ErrorToString())
    End Sub
    <Theory>
    <InlineData(0)>
    <InlineData(-1)>
    <InlineData(7)>
    <InlineData(Integer.MinValue)>
    Public Sub ErrorToString_WithErrorNumber(errorNumber As Integer)
      Assert.NotNull(Conversion.ErrorToString(errorNumber))
    End Sub
    <Theory>
    <InlineData(Integer.MaxValue)>
    Public Sub ErrorToString_ArgumentException(errorNumber As Integer)
      Assert.Throws(Of ArgumentException)(Function() Conversion.ErrorToString(errorNumber))
    End Sub
    <Theory>
    <InlineData(True, 1)>
    <InlineData(False, 0)>
    <InlineData("0", 0.0)>
    <InlineData("1", 1.0)>
    <InlineData("-1", -1.0)>
    <MemberData(NameOf(Fix_Short_TestData))>
    <MemberData(NameOf(Fix_Integer_TestData))>
    <MemberData(NameOf(Fix_Long_TestData))>
    <MemberData(NameOf(Fix_Single_TestData))>
    <MemberData(NameOf(Fix_Double_TestData))>
    <MemberData(NameOf(Fix_Decimal_TestData))>
    Public Sub Fix_Object(value As Object, expected As Object)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Short_TestData))>
    Public Sub Fix_Short(value As Short, expected As Short)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Integer_TestData))>
    Public Sub Fix_Int(value As Integer, expected As Integer)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Long_TestData))>
    Public Sub Fix_Long(value As Long, expected As Long)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Single_TestData))>
    Public Sub Fix_Float(value As Single, expected As Single)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Double_TestData))>
    Public Sub Fix_Double(value As Double, expected As Double)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Fix_Decimal_TestData))>
    Public Sub Fix_Decimal(value As Decimal, expected As Decimal)
      Assert.Equal(expected, Conversion.Fix(value))
    End Sub
    <Theory>
    <InlineData(Char.MinValue)>
    <InlineData(Char.MaxValue)>
    <MemberData(NameOf(Various_ArgumentException_TestData))>
    Public Sub Fix_ArgumentException(value As Object)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Fix(value))
    End Sub
    <Theory>
    <InlineData(Nothing)>
    Public Sub Fix_ArgumentNullException(value As Object)
      Assert.Throws(Of ArgumentNullException)(Function() Conversion.Fix(value))
    End Sub
    Public Shared Iterator Function Fix_Short_TestData() As IEnumerable(Of Object())
      Yield New Object() {Short.MinValue, Short.MinValue}
      Yield New Object() {CShort(Fix(-1)), CShort(Fix(-1))}
      Yield New Object() {CShort(Fix(0)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(1)), CShort(Fix(1))}
      Yield New Object() {Short.MaxValue, Short.MaxValue}
    End Function
    Public Shared Iterator Function Fix_Integer_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue, Integer.MinValue}
      Yield New Object() {-1, -1}
      Yield New Object() {0, 0}
      Yield New Object() {1, 1}
      Yield New Object() {Integer.MaxValue, Integer.MaxValue}
    End Function
    Public Shared Iterator Function Fix_Long_TestData() As IEnumerable(Of Object())
      Yield New Object() {Long.MinValue, Long.MinValue}
      Yield New Object() {-1L, -1L}
      Yield New Object() {0L, 0L}
      Yield New Object() {1L, 1L}
      Yield New Object() {Long.MaxValue, Long.MaxValue}
    End Function
    Public Shared Iterator Function Fix_Single_TestData() As IEnumerable(Of Object())
      Yield New Object() {Single.MinValue, Single.MinValue}
      Yield New Object() {-999.999F, -999.0F}
      Yield New Object() {-1.9F, -1.0F}
      Yield New Object() {0.0F, 0.0F}
      Yield New Object() {1.9F, 1.0F}
      Yield New Object() {999.999F, 999.0F}
      Yield New Object() {Single.MaxValue, Single.MaxValue}
    End Function
    Public Shared Iterator Function Fix_Double_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.MinValue, Double.MinValue}
      Yield New Object() {-999.999, -999.0}
      Yield New Object() {-1.9, -1.0}
      Yield New Object() {0.0, 0.0}
      Yield New Object() {1.9, 1.0}
      Yield New Object() {999.999, 999.0}
      Yield New Object() {Double.MaxValue, Double.MaxValue}
      Yield New Object() {Math.E, CDbl(2)}
      Yield New Object() {Math.PI, CDbl(3)}
    End Function
    Public Shared Iterator Function Fix_Decimal_TestData() As IEnumerable(Of Object())
      Yield New Object() {Decimal.MinValue, Decimal.MinValue}
      Yield New Object() {CDec(-999.999), CDec(-999.0)}
      Yield New Object() {CDec(-1.9), CDec(-1.0)}
      Yield New Object() {CDec(0), CDec(0)}
      Yield New Object() {CDec(1.9), CDec(1.0)}
      Yield New Object() {CDec(999.999), CDec(999.0)}
      Yield New Object() {Decimal.MaxValue, Decimal.MaxValue}
    End Function
    <Theory>
    <InlineData(-1, "FFFFFFFF")> ' expected for long overload: "FFFFFFFFFFFFFFFF"
    <InlineData("9223372036854775807", "7FFFFFFFFFFFFFFF")> ' long
    <InlineData("18446744073709551615", "FFFFFFFFFFFFFFFF")> ' ulong
    <MemberData(NameOf(Hex_Byte_TestData))>
    <MemberData(NameOf(Hex_SByte_TestData))>
    <MemberData(NameOf(Hex_UShort_TestData))>
    <MemberData(NameOf(Hex_Short_TestData))>
    <MemberData(NameOf(Hex_UInteger_TestData))>
    <MemberData(NameOf(Hex_Integer_TestData))>
    <MemberData(NameOf(Hex_ULong_TestData))>
    <MemberData(NameOf(Hex_Long_TestData))>
    <MemberData(NameOf(Hex_Single_TestData))>
    <MemberData(NameOf(Hex_Double_TestData))>
    <MemberData(NameOf(Hex_Decimal_TestData))>
    Public Sub Hex_Object(value As Object, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Byte_TestData))>
    Public Sub Hex_Byte(value As Byte, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_SByte_TestData))>
    Public Sub Hex_Sbyte(value As SByte, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_UShort_TestData))>
    Public Sub Hex_Ushort(value As UShort, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Short_TestData))>
    Public Sub Hex_Short(value As Short, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_UInteger_TestData))>
    Public Sub Hex_Uint(value As UInteger, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Integer_TestData))>
    Public Sub Hex_Int(value As Integer, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_ULong_TestData))>
    Public Sub Hex_Ulong(value As ULong, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <InlineData(-1, "FFFFFFFFFFFFFFFF")> ' expected for object overload: "FFFFFFFF"
    <MemberData(NameOf(Hex_Long_TestData))>
    Public Sub Hex_Long(value As Long, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Single_TestData))>
    Public Sub Hex_Float(value As Single, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Double_TestData))>
    Public Sub Hex_Double(value As Double, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Hex_Decimal_TestData))>
    Public Sub Hex_Decimal(value As Decimal, expected As String)
      Assert.Equal(expected, Conversion.Hex(value))
    End Sub
    <Theory>
    <InlineData(True)>
    <InlineData(False)>
    <InlineData(Char.MinValue)>
    <MemberData(NameOf(Various_ArgumentException_TestData))>
    Public Sub Hex_ArgumentException(value As Object)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Hex(value))
    End Sub
    <Theory>
    <InlineData(Nothing)>
    Public Sub Hex_ArgumentNullException(value As Object)
      Assert.Throws(Of ArgumentNullException)(Function() Conversion.Hex(value))
    End Sub
    Public Shared Iterator Function Hex_Byte_TestData() As IEnumerable(Of Object())
      Yield New Object() {Byte.MinValue, "0"}
      Yield New Object() {CByte(0), "0"}
      Yield New Object() {Byte.MaxValue, "FF"}
    End Function
    Public Shared Iterator Function Hex_SByte_TestData() As IEnumerable(Of Object())
      Yield New Object() {SByte.MinValue, "80"}
      Yield New Object() {CSByte(-1), "FF"}
      Yield New Object() {CSByte(0), "0"}
      Yield New Object() {CSByte(1), "1"}
      Yield New Object() {CSByte(15), "F"}
      Yield New Object() {SByte.MaxValue, "7F"}
    End Function
    Public Shared Iterator Function Hex_UShort_TestData() As IEnumerable(Of Object())
      Yield New Object() {UShort.MinValue, "0"}
      Yield New Object() {CUShort(0), "0"}
      Yield New Object() {CUShort(15), "F"}
      Yield New Object() {UShort.MaxValue, "FFFF"}
    End Function
    Public Shared Iterator Function Hex_Short_TestData() As IEnumerable(Of Object())
      Yield New Object() {Short.MinValue, "8000"}
      Yield New Object() {CShort(Fix(-1)), "FFFF"}
      Yield New Object() {CShort(Fix(0)), "0"}
      Yield New Object() {CShort(Fix(1)), "1"}
      Yield New Object() {CShort(Fix(15)), "F"}
      Yield New Object() {Short.MaxValue, "7FFF"}
    End Function
    Public Shared Iterator Function Hex_UInteger_TestData() As IEnumerable(Of Object())
      Yield New Object() {UInteger.MinValue, "0"}
      Yield New Object() {CUInt(0), "0"}
      Yield New Object() {CUInt(15), "F"}
      Yield New Object() {UInteger.MaxValue, "FFFFFFFF"}
    End Function
    Public Shared Iterator Function Hex_Integer_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue, "80000000"}
      Yield New Object() {-1, "FFFFFFFF"}
      Yield New Object() {0, "0"}
      Yield New Object() {1, "1"}
      Yield New Object() {15, "F"}
      Yield New Object() {Integer.MaxValue, "7FFFFFFF"}
    End Function
    Public Shared Iterator Function Hex_ULong_TestData() As IEnumerable(Of Object())
      Yield New Object() {ULong.MinValue, "0"}
      Yield New Object() {CULng(0), "0"}
      Yield New Object() {CULng(15), "F"}
      Yield New Object() {ULong.MaxValue, "FFFFFFFFFFFFFFFF"}
    End Function
    Public Shared Iterator Function Hex_Long_TestData() As IEnumerable(Of Object())
      Yield New Object() {Long.MinValue, "8000000000000000"}
      Yield New Object() {CLng(Fix(0)), "0"}
      Yield New Object() {CLng(Fix(1)), "1"}
      Yield New Object() {CLng(Fix(15)), "F"}
      Yield New Object() {Long.MaxValue, "7FFFFFFFFFFFFFFF"}
    End Function
    Public Shared Iterator Function Hex_Single_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Hex_Double_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Hex_Decimal_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    <Theory>
    <InlineData(True, 1)>
    <InlineData(False, 0)>
    <InlineData("0", 0.0)>
    <InlineData("1", 1.0)>
    <InlineData("-1", -1.0)>
    <MemberData(NameOf(Int_Short_TestData))>
    <MemberData(NameOf(Int_Integer_TestData))>
    <MemberData(NameOf(Int_Long_TestData))>
    <MemberData(NameOf(Int_Single_TestData))>
    <MemberData(NameOf(Int_Double_TestData))>
    <MemberData(NameOf(Int_Decimal_TestData))>
    Public Sub Int_Object_Object(value As Object, expected As Object)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Short_TestData))>
    Public Sub Int_Short(value As Short, expected As Short)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Integer_TestData))>
    Public Sub Int_Int(value As Integer, expected As Integer)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Long_TestData))>
    Public Sub Int_Long(value As Long, expected As Long)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Single_TestData))>
    Public Sub Int_Float(value As Single, expected As Single)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Double_TestData))>
    Public Sub Int_Double(value As Double, expected As Double)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Int_Decimal_TestData))>
    Public Sub Int_Decimal(value As Decimal, expected As Decimal)
      Assert.Equal(expected, Conversion.Int(value))
    End Sub
    <Theory>
    <InlineData(Char.MinValue)>
    <InlineData(Char.MaxValue)>
    <MemberData(NameOf(Various_ArgumentException_TestData))>
    Public Sub Int_ArgumentException(value As Object)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Int(value))
    End Sub
    <Theory>
    <InlineData(Nothing)>
    Public Sub Int_ArgumentNullException(value As Object)
      Assert.Throws(Of ArgumentNullException)(Function() Conversion.Int(value))
    End Sub
    Public Shared Iterator Function Int_Short_TestData() As IEnumerable(Of Object())
      Yield New Object() {Short.MinValue, Short.MinValue}
      Yield New Object() {CShort(Fix(0)), CShort(Fix(0))}
      Yield New Object() {Short.MaxValue, Short.MaxValue}
    End Function
    Public Shared Iterator Function Int_Integer_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue, Integer.MinValue}
      Yield New Object() {0, 0}
      Yield New Object() {Integer.MaxValue, Integer.MaxValue}
    End Function
    Public Shared Iterator Function Int_Long_TestData() As IEnumerable(Of Object())
      Yield New Object() {Long.MinValue, Long.MinValue}
      Yield New Object() {0L, 0L}
      Yield New Object() {Long.MaxValue, Long.MaxValue}
    End Function
    Public Shared Iterator Function Int_Single_TestData() As IEnumerable(Of Object())
      Yield New Object() {Single.MinValue, Single.MinValue}
      Yield New Object() {-999.999F, -1000.0F}
      Yield New Object() {-1.9F, -2.0F}
      Yield New Object() {0.0F, 0.0F}
      Yield New Object() {1.9F, 1.0F}
      Yield New Object() {999.999F, 999.0F}
      Yield New Object() {Single.MaxValue, Single.MaxValue}
    End Function
    Public Shared Iterator Function Int_Double_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.MinValue, Double.MinValue}
      Yield New Object() {-999.999, -1000.0}
      Yield New Object() {-1.9, -2.0}
      Yield New Object() {0.0, 0.0}
      Yield New Object() {1.9, 1.0}
      Yield New Object() {999.999, 999.0}
      Yield New Object() {Double.MaxValue, Double.MaxValue}
      Yield New Object() {Math.E, CDbl(2)}
      Yield New Object() {Math.PI, CDbl(3)}
    End Function
    Public Shared Iterator Function Int_Decimal_TestData() As IEnumerable(Of Object())
      Yield New Object() {Decimal.MinValue, Decimal.MinValue}
      Yield New Object() {CDec(-999.999), CDec(-1000.0)}
      Yield New Object() {CDec(-1.9), CDec(-2.0)}
      Yield New Object() {CDec(0), CDec(0)}
      Yield New Object() {CDec(1.9), CDec(1.0)}
      Yield New Object() {CDec(999.999), CDec(999.0)}
      Yield New Object() {Decimal.MaxValue, Decimal.MaxValue}
    End Function
    <Theory>
    <InlineData(-1, "37777777777")> ' expected for long overload: "1777777777777777777777"
    <InlineData("9223372036854775807", "777777777777777777777")> ' long
    <InlineData("18446744073709551615", "1777777777777777777777")> ' ulong
    <MemberData(NameOf(Oct_Byte_TestData))>
    <MemberData(NameOf(Oct_SByte_TestData))>
    <MemberData(NameOf(Oct_UShort_TestData))>
    <MemberData(NameOf(Oct_Short_TestData))>
    <MemberData(NameOf(Oct_UInteger_TestData))>
    <MemberData(NameOf(Oct_Integer_TestData))>
    <MemberData(NameOf(Oct_ULong_TestData))>
    <MemberData(NameOf(Oct_Long_TestData))>
    <MemberData(NameOf(Oct_Single_TestData))>
    <MemberData(NameOf(Oct_Double_TestData))>
    <MemberData(NameOf(Oct_Decimal_TestData))>
    Public Sub Oct_Object(value As Object, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Byte_TestData))>
    Public Sub Oct_Byte(value As Byte, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_SByte_TestData))>
    Public Sub Oct_Sbyte(value As SByte, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_UShort_TestData))>
    Public Sub Oct_Ushort(value As UShort, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Short_TestData))>
    Public Sub Oct_Short(value As Short, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_UInteger_TestData))>
    Public Sub Oct_Uint(value As UInteger, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Integer_TestData))>
    Public Sub Oct_Int(value As Integer, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_ULong_TestData))>
    Public Sub Oct_Ulong(value As ULong, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <InlineData(-1, "1777777777777777777777")> ' expected for object overload: "37777777777"
    <MemberData(NameOf(Oct_Long_TestData))>
    Public Sub Oct_Long(value As Long, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Single_TestData))>
    Public Sub Oct_Float(value As Single, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Double_TestData))>
    Public Sub Oct_Double(value As Double, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Oct_Decimal_TestData))>
    Public Sub Oct_Decimal(value As Decimal, expected As String)
      Assert.Equal(expected, Conversion.Oct(value))
    End Sub
    <Theory>
    <InlineData(True)>
    <InlineData(False)>
    <InlineData(Char.MinValue)>
    <InlineData(Char.MaxValue)>
    <MemberData(NameOf(Various_ArgumentException_TestData))>
    Public Sub Oct_ArgumentException(value As Object)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Oct(value))
    End Sub
    <Theory>
    <InlineData(Nothing)>
    Public Sub Oct_ArgumentNullException(value As Object)
      Assert.Throws(Of ArgumentNullException)(Function() Conversion.Oct(value))
    End Sub
    Public Shared Iterator Function Oct_Byte_TestData() As IEnumerable(Of Object())
      Yield New Object() {Byte.MinValue, "0"}
      Yield New Object() {CByte(0), "0"}
      Yield New Object() {CByte(15), "17"}
      Yield New Object() {Byte.MaxValue, "377"}
    End Function
    Public Shared Iterator Function Oct_SByte_TestData() As IEnumerable(Of Object())
      Yield New Object() {SByte.MinValue, "200"}
      Yield New Object() {CSByte(-1), "377"}
      Yield New Object() {CSByte(0), "0"}
      Yield New Object() {CSByte(1), "1"}
      Yield New Object() {CSByte(15), "17"}
      Yield New Object() {SByte.MaxValue, "177"}
    End Function
    Public Shared Iterator Function Oct_UShort_TestData() As IEnumerable(Of Object())
      Yield New Object() {UShort.MinValue, "0"}
      Yield New Object() {CUShort(0), "0"}
      Yield New Object() {CUShort(1), "1"}
      Yield New Object() {CUShort(15), "17"}
      Yield New Object() {UShort.MaxValue, "177777"}
    End Function
    Public Shared Iterator Function Oct_Short_TestData() As IEnumerable(Of Object())
      Yield New Object() {Short.MinValue, "100000"}
      Yield New Object() {CShort(Fix(-1)), "177777"}
      Yield New Object() {CShort(Fix(0)), "0"}
      Yield New Object() {CShort(Fix(1)), "1"}
      Yield New Object() {CUShort(15), "17"}
      Yield New Object() {Short.MaxValue, "77777"}
    End Function
    Public Shared Iterator Function Oct_UInteger_TestData() As IEnumerable(Of Object())
      Yield New Object() {UInteger.MinValue, "0"}
      Yield New Object() {CUInt(0), "0"}
      Yield New Object() {CUInt(1), "1"}
      Yield New Object() {CUInt(15), "17"}
      Yield New Object() {UInteger.MaxValue, "37777777777"}
    End Function
    Public Shared Iterator Function Oct_Integer_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue, "20000000000"}
      Yield New Object() {-1, "37777777777"}
      Yield New Object() {0, "0"}
      Yield New Object() {1, "1"}
      Yield New Object() {15, "17"}
      Yield New Object() {Integer.MaxValue, "17777777777"}
    End Function
    Public Shared Iterator Function Oct_ULong_TestData() As IEnumerable(Of Object())
      Yield New Object() {ULong.MinValue, "0"}
      Yield New Object() {CULng(0), "0"}
      Yield New Object() {CULng(1), "1"}
      Yield New Object() {CULng(15), "17"}
      Yield New Object() {ULong.MaxValue, "1777777777777777777777"}
    End Function
    Public Shared Iterator Function Oct_Long_TestData() As IEnumerable(Of Object())
      Yield New Object() {Long.MinValue, "10"}
      Yield New Object() {CLng(Fix(0)), "0"}
      Yield New Object() {CLng(Fix(1)), "1"}
      Yield New Object() {CLng(Fix(15)), "17"}
      Yield New Object() {Long.MaxValue, "777777777777777777777"}
    End Function
    Public Shared Iterator Function Oct_Single_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Oct_Double_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Oct_Decimal_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    <Theory>
    <MemberData(NameOf(Str_TestData))>
    Public Sub Str(value As Object, expected As String)
      Assert.Equal(expected, Conversion.Str(value))
    End Sub
    <Theory>
    <InlineData(Nothing)>
    Public Sub Str_ArgumentNullException(value As Object)
      Assert.Throws(Of ArgumentNullException)(Function() Conversion.Str(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Str_Object_InvalidCastException_TestData))>
    Public Sub Str_InvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversion.Str(value))
    End Sub
    Public Shared Iterator Function Str_TestData() As IEnumerable(Of Object())
      Yield New Object() {0, " 0"}
      Yield New Object() {1, " 1"}
      Yield New Object() {-1, "-1"}
      Yield New Object() {DBNull.Value, "Null"}
      Yield New Object() {True, "True"}
      Yield New Object() {False, "False"}
      Yield New Object() {"0", " 0"}
    End Function
    Public Shared Iterator Function Str_Object_InvalidCastException_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Object}
      Yield New Object() {String.Empty}
    End Function
    <Theory>
    <MemberData(NameOf(Val_Object_TestData))>
    <MemberData(NameOf(Val_Char_TestData))>
    <MemberData(NameOf(Val_String_TestData))>
    Public Sub Val_Object_Double(value As Object, expected As Double)
      Assert.Equal(expected, Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_Object_ArgumentException_TestData))>
    Public Sub Val_ArgumentException_Object(value As Object)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_Object_OverflowException_TestData))>
    Public Sub Val_OverflowException_Object(value As Object)
      Assert.Throws(Of OverflowException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_Char_TestData))>
    Public Sub Val_Char_Int(value As Char, expected As Integer)
      Assert.Equal(expected, Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_Char_ArgumentException_TestData))>
    Public Sub Val_ArgumentException_Char(value As Char)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_Char_OverflowException_TestData))>
    Public Sub Val_OverflowException_Char(value As Char)
      Assert.Throws(Of OverflowException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_String_TestData))>
    Public Sub Val_String_Double(value As String, expected As Double)
      Assert.Equal(expected, Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_String_ArgumentException_TestData))>
    Public Sub Val_ArgumentException_String(value As String)
      Assert.Throws(Of ArgumentException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_String_InvalidCastException_TestData))>
    Public Sub Val_InvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversion.Val(value))
    End Sub
    <Theory>
    <MemberData(NameOf(Val_String_OverflowException_TestData))>
    Public Sub Val_OverflowException_String(value As String)
      Assert.Throws(Of OverflowException)(Function() Conversion.Val(value))
    End Sub
    Public Shared Iterator Function Val_Object_TestData() As IEnumerable(Of Object())
      Yield New Object() {0, "0"}
      Yield New Object() {1, " 1"}
    End Function
    Public Shared Iterator Function Val_Object_ArgumentException_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Val_Object_OverflowException_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Val_Char_TestData() As IEnumerable(Of Object())
      Yield New Object() {ChrW(0), 0}
      Yield New Object() {"0"c, 0}
      Yield New Object() {"1"c, 1}
      Yield New Object() {"2"c, 2}
      Yield New Object() {"3"c, 3}
      Yield New Object() {"4"c, 4}
      Yield New Object() {"5"c, 5}
      Yield New Object() {"6"c, 6}
      Yield New Object() {"7"c, 7}
      Yield New Object() {"8"c, 8}
      Yield New Object() {"9"c, 9}
      Yield New Object() {"A"c, 0}
      Yield New Object() {Char.MaxValue, 0}
    End Function
    Public Shared Iterator Function Val_Char_ArgumentException_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Val_Char_OverflowException_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Val_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 0.0}
      Yield New Object() {"", 0.0}
      Yield New Object() {"0", 0.0}
      Yield New Object() {"1", 1.0}
      Yield New Object() {"1%", 1.0}
      Yield New Object() {"1&", 1.0}
      Yield New Object() {"1!", 1.0}
      Yield New Object() {"1@", 1.0}
      Yield New Object() {"-1", -1.0}
      Yield New Object() {"+1", 1.0}
      Yield New Object() {".1", 0.1}
      Yield New Object() {"1.0", 1.0}
      Yield New Object() {"1. 1", 1.1}
      Yield New Object() {"1..1", 1.0}
      Yield New Object() {"1.1.1", 1.1}
      Yield New Object() {"&H F", 15.0}
      Yield New Object() {"&O 7", 7.0}
      Yield New Object() {"&H7F", 127.0}
      Yield New Object() {"&hff", 255.0}
      Yield New Object() {"&O177", 127.0}
      Yield New Object() {"&o377", 255.0}
      Yield New Object() {"&H0F0", 240.0}
      Yield New Object() {"&O070", 56.0}
      Yield New Object() {"-1e20", -1.0E+20}
      Yield New Object() {"1e-3", 0.001}
      Yield New Object() {"1e+1+", 10.0}
      Yield New Object() {"1e+1-", 10.0}
      Yield New Object() {"1eA", 1.0}
      Yield New Object() {"1.1e +3", 1100.0}
      Yield New Object() {vbTab & vbCrLf & " 　", 0.0}
      Yield New Object() {"1." & vbTab & vbCrLf & " 　1", 1.1}
      Yield New Object() {"&HFFFF%", -1.0}
      Yield New Object() {"&HFFFFFFFF&", -1.0}
      Yield New Object() {"&HFFFFFFFFFFFFFFFF", -1.0}
      Yield New Object() {"&HFFFFFFFFFFFFFFFF", -1.0}
      Yield New Object() {"&O177777%", -1.0}
      Yield New Object() {"&O37777777777&", -1.0}
    End Function
    Public Shared Iterator Function Val_String_ArgumentException_TestData() As IEnumerable(Of Object())
      Return ' Add more...
    End Function
    Public Shared Iterator Function Val_String_InvalidCastException_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1.0%"}
      Yield New Object() {"1.0&"}
    End Function
    Public Shared Iterator Function Val_String_OverflowException_TestData() As IEnumerable(Of Object())
      Yield New Object() {"123456e789"}
    End Function
    Public Shared Iterator Function Various_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {DBNull.Value}
      Yield New Object() {New DateTime(2000, 1, 1)}
    End Function
  End Class

End Namespace