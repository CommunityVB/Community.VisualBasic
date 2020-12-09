' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Community.VisualBasic.CompilerServices
Imports Community.VisualBasic.Tests
'Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Class StringTypeTests
    <Theory>
    <MemberData(NameOf(FromBoolean_TestData))>
    Public Sub FromBoolean(value As Boolean, expected As String)
      Assert.Equal(expected, StringType.FromBoolean(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromByte_TestData))>
    Public Sub FromByte(value As Byte, expected As String)
      Assert.Equal(expected, StringType.FromByte(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromChar_TestData))>
    Public Sub FromChar(value As Char, expected As String)
      Assert.Equal(expected, StringType.FromChar(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromDateTime_TestData))>
    Public Sub FromDate(value As DateTime, expected As String)
      Assert.Equal(expected, StringType.FromDate(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromDecimal_TestData))>
    Public Sub FromDecimal(value As Decimal, expected As String)
      Assert.Equal(expected, StringType.FromDecimal(value))
      Assert.Equal(expected, StringType.FromDecimal(value, CType(Nothing, NumberFormatInfo)))
    End Sub
    <Theory>
    <MemberData(NameOf(FromDecimal_Format_TestData))>
    Public Sub FromDecimal_Format(value As Decimal, format As NumberFormatInfo, expected As String)
      Assert.Equal(expected, StringType.FromDecimal(value, format))
    End Sub
    <Theory>
    <MemberData(NameOf(FromDouble_TestData))>
    Public Sub FromDouble(value As Double, expected As String)
      Assert.Equal(expected, StringType.FromDouble(value))
      Assert.Equal(expected, StringType.FromDouble(value, CType(Nothing, NumberFormatInfo)))
    End Sub
    <Theory>
    <MemberData(NameOf(FromDouble_Format_TestData))>
    Public Sub FromDouble_Format(value As Double, format As NumberFormatInfo, expected As String)
      Assert.Equal(expected, StringType.FromDouble(value, format))
    End Sub
    <Theory>
    <MemberData(NameOf(FromInt32_TestData))>
    Public Sub FromInteger(value As Integer, expected As String)
      Assert.Equal(expected, StringType.FromInteger(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromInt64_TestData))>
    Public Sub FromLong(value As Long, expected As String)
      Assert.Equal(expected, StringType.FromLong(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromByte_TestData))>
    <MemberData(NameOf(FromInt16_TestData))>
    <MemberData(NameOf(FromInt32_TestData))>
    <MemberData(NameOf(FromInt64_TestData))>
    <MemberData(NameOf(FromSingle_TestData))>
    <MemberData(NameOf(FromDouble_TestData))>
    <MemberData(NameOf(FromDecimal_TestData))>
    <MemberData(NameOf(FromBoolean_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    <MemberData(NameOf(FromNull_TestData))>
    <MemberData(NameOf(FromChar_TestData))>
    <MemberData(NameOf(FromCharArray_TestData))>
    <MemberData(NameOf(FromDateTime_TestData))>
    Public Sub FromObject(value As Object, expected As String)
      Assert.Equal(expected, StringType.FromObject(value))
    End Sub
    ' The following conversions are not supported.
    <Theory>
    <MemberData(NameOf(FromSByte_TestData))>
    <MemberData(NameOf(FromUInt16_TestData))>
    <MemberData(NameOf(FromUInt32_TestData))>
    <MemberData(NameOf(FromUInt64_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() StringType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    Public Sub FromObject_InvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() StringType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromInt16_TestData))>
    Public Sub FromShort(value As Short, expected As String)
      Assert.Equal(expected, StringType.FromShort(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromSingle_TestData))>
    Public Sub FromSingle(value As Single, expected As String)
      Assert.Equal(expected, StringType.FromSingle(value))
      Assert.Equal(expected, StringType.FromSingle(value, CType(Nothing, NumberFormatInfo)))
    End Sub
    <Theory>
    <MemberData(NameOf(FromSingle_Format_TestData))>
    Public Sub FromSingle_Format(value As Single, format As NumberFormatInfo, expected As String)
      Assert.Equal(expected, StringType.FromSingle(value, format))
    End Sub

    Public Shared Iterator Function FromByte_TestData() As IEnumerable(Of Object())
      Yield New Object() {Byte.MinValue, "0"}
      Yield New Object() {CByte(1), "1"}
      Yield New Object() {Byte.MaxValue, "255"}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), "0"}
      Yield New Object() {CType(1, ByteEnum), "1"}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), "255"}
    End Function
    Public Shared Iterator Function FromSByte_TestData() As IEnumerable(Of Object())
      Yield New Object() {SByte.MinValue}
      Yield New Object() {CSByte((-1))}
      Yield New Object() {CSByte(0)}
      Yield New Object() {CSByte(1)}
      Yield New Object() {SByte.MaxValue}
      Yield New Object() {CType(SByte.MinValue, SByteEnum)}
      Yield New Object() {CType((-1), SByteEnum)}
      Yield New Object() {CType(0, SByteEnum)}
      Yield New Object() {CType(1, SByteEnum)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum)}
    End Function
    Public Shared Iterator Function FromUInt16_TestData() As IEnumerable(Of Object())
      Yield New Object() {UShort.MinValue}
      Yield New Object() {CUShort(1)}
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MinValue, UShortEnum)}
      Yield New Object() {CType(1, UShortEnum)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}
    End Function
    Public Shared Iterator Function FromInt16_TestData() As IEnumerable(Of Object())
      Yield New Object() {Short.MinValue, "-32768"}
      Yield New Object() {CShort(Fix((-1))), "-1"}
      Yield New Object() {CShort(Fix(0)), "0"}
      Yield New Object() {CShort(Fix(1)), "1"}
      Yield New Object() {Short.MaxValue, "32767"}
      Yield New Object() {CType(Short.MinValue, ShortEnum), "-32768"}
      Yield New Object() {CType((-1), ShortEnum), "-1"}
      Yield New Object() {CType(0, ShortEnum), "0"}
      Yield New Object() {CType(1, ShortEnum), "1"}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), "32767"}
    End Function
    Public Shared Iterator Function FromUInt32_TestData() As IEnumerable(Of Object())
      Yield New Object() {UInteger.MinValue}
      Yield New Object() {CUInt(1)}
      Yield New Object() {UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum)}
      Yield New Object() {CType(1, UIntEnum)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum)}
    End Function
    Public Shared Iterator Function FromInt32_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue, "-2147483648"}
      Yield New Object() {-1, "-1"}
      Yield New Object() {0, "0"}
      Yield New Object() {1, "1"}
      Yield New Object() {Integer.MaxValue, "2147483647"}
      Yield New Object() {CType(Integer.MinValue, IntEnum), "-2147483648"}
      Yield New Object() {CType((-1), IntEnum), "-1"}
      Yield New Object() {CType(0, IntEnum), "0"}
      Yield New Object() {CType(1, IntEnum), "1"}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), "2147483647"}
    End Function
    Public Shared Iterator Function FromUInt64_TestData() As IEnumerable(Of Object())
      Yield New Object() {ULong.MinValue}
      Yield New Object() {CULng(1)}
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MinValue, ULongEnum)}
      Yield New Object() {CType(1, ULongEnum)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}
    End Function
    Public Shared Iterator Function FromInt64_TestData() As IEnumerable(Of Object())
      Yield New Object() {Long.MinValue, "-9223372036854775808"}
      Yield New Object() {CLng(Fix((-1))), "-1"}
      Yield New Object() {CLng(Fix(0)), "0"}
      Yield New Object() {CLng(Fix(1)), "1"}
      Yield New Object() {Long.MaxValue, "9223372036854775807"}
      Yield New Object() {CType(Long.MinValue, LongEnum), "-9223372036854775808"}
      Yield New Object() {CType((-1), LongEnum), "-1"}
      Yield New Object() {CType(0, LongEnum), "0"}
      Yield New Object() {CType(1, LongEnum), "1"}
      Yield New Object() {CType(Long.MaxValue, LongEnum), "9223372036854775807"}
    End Function
    Public Shared Iterator Function FromSingle_TestData() As IEnumerable(Of Object())
      Yield New Object() {CSng((-1)), "-1"}
      Yield New Object() {CSng(0), "0"}
      Yield New Object() {CSng(1), "1"}
      Yield New Object() {Single.PositiveInfinity, Single.PositiveInfinity.ToString()}
      Yield New Object() {Single.NegativeInfinity, Single.NegativeInfinity.ToString()}
      Yield New Object() {Single.NaN, "NaN"}
    End Function
    Public Shared Iterator Function FromDouble_TestData() As IEnumerable(Of Object())
      Yield New Object() {CDbl((-1)), "-1"}
      Yield New Object() {CDbl(0), "0"}
      Yield New Object() {CDbl(1), "1"}
      Yield New Object() {Double.PositiveInfinity, Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity, Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN, "NaN"}
    End Function
    Public Shared Iterator Function FromDecimal_TestData() As IEnumerable(Of Object())
      Yield New Object() {Decimal.MinValue, Decimal.MinValue.ToString()}
      Yield New Object() {CDec((-1)), "-1"}
      Yield New Object() {CDec(0), "0"}
      Yield New Object() {CDec(1), "1"}
      Yield New Object() {Decimal.MaxValue, Decimal.MaxValue.ToString()}
    End Function
    Public Shared Iterator Function FromBoolean_TestData() As IEnumerable(Of Object())
      Yield New Object() {True, "True"}
      Yield New Object() {False, "False"}
    End Function
    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"", ""}
      Yield New Object() {"abc", "abc"}
    End Function
    Public Shared Iterator Function FromNull_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CStr(Nothing)}
    End Function
    Public Shared Iterator Function FromChar_TestData() As IEnumerable(Of Object())
      Yield New Object() {Char.MinValue, vbNullChar}
      Yield New Object() {ChrW(1), ChrW(1)}
      Yield New Object() {"a"c, "a"}
      Yield New Object() {Char.MaxValue, Char.MaxValue.ToString()}
    End Function
    Public Shared Iterator Function FromCharArray_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Char() {}, ""}
      Yield New Object() {New Char() {ChrW(0)}, vbNullChar}
      Yield New Object() {New Char() {"A"c, "B"c}, "AB"}
    End Function
    Public Shared Iterator Function FromDateTime_TestData() As IEnumerable(Of Object())
      Yield New Object() {New DateTime(10), New DateTime(10).ToString("T", Nothing)}
    End Function
    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Object}
    End Function
    Public Shared Iterator Function FromSingle_Format_TestData() As IEnumerable(Of Object())
      Yield New Object() {CSng((-1)), CType(Nothing, NumberFormatInfo), "-1"}
      Yield New Object() {CSng((-1)), New NumberFormatInfo() With
{
          .NegativeSign = "#"}, "#1"}
    End Function
    Public Shared Iterator Function FromDouble_Format_TestData() As IEnumerable(Of Object())
      Yield New Object() {CDbl((-1)), CType(Nothing, NumberFormatInfo), "-1"}
      Yield New Object() {CDbl((-1)), New NumberFormatInfo() With
{
          .NegativeSign = "#"}, "#1"}
    End Function
    Public Shared Iterator Function FromDecimal_Format_TestData() As IEnumerable(Of Object())
      Yield New Object() {CDec((-1)), CType(Nothing, NumberFormatInfo), "-1"}
      Yield New Object() {CDec((-1)), New NumberFormatInfo() With
{
          .NegativeSign = "#"}, "#1"}
    End Function
    <Theory>
    <InlineData("a", 1, 0, Nothing, "a")>
    <InlineData("a", 1, 0, "", "a")>
    <InlineData("a", 1, 1, "", "a")>
    <InlineData("a", 1, 0, "b", "a")>
    <InlineData("a", 1, 1, "b", "b")>
    <InlineData("a", 1, 2, "b", "b")>
    <InlineData("abc", 2, 0, "def", "abc")>
    <InlineData("abc", 2, 1, "def", "adc")>
    <InlineData("abc", 2, 2, "def", "ade")>
    <InlineData("abc", 2, 3, "def", "ade")>
    Public Sub MidStmtStr(str As String, start As Integer, length As Integer, insert As String, expected As String)
      Global.Microsoft.VisualBasic.CompilerServices.StringType.MidStmtStr(str, start, length, insert)
      Assert.Equal(expected, str)
    End Sub
    <Theory>
    <InlineData(Nothing, 1, 0, Nothing)>
    <InlineData(Nothing, 1, 0, "")>
    <InlineData("", 1, 0, Nothing)>
    <InlineData("", -1, 0, "")>
    <InlineData("", 0, 0, "")>
    <InlineData("", 1, 0, "")>
    <InlineData("", 2, 0, "")>
    <InlineData("", 1, -1, "")>
    <InlineData("abc", -1, 0, "")>
    <InlineData("abc", 0, 0, "")>
    <InlineData("abc", 4, 0, "")>
    <InlineData("abc", 1, -3, "")>
    Public Sub MidStmtStr_ArgumentException(str As String, start As Integer, length As Integer, insert As String)
      Assert.Throws(Of ArgumentException)(Sub() Global.Microsoft.VisualBasic.CompilerServices.StringType.MidStmtStr(str, start, length, insert))
    End Sub
    <Theory>
    <MemberData(NameOf(StrCmp_TestData))>
    Public Sub StrCmp(left As String, right As String, expectedBinaryCompare As Integer, expectedTextCompare As Integer)
      Assert.Equal(expectedBinaryCompare, StringType.StrCmp(left, right, TextCompare:=False))
      Assert.Equal(expectedTextCompare, StringType.StrCmp(left, right, TextCompare:=True))
    End Sub
    Public Shared Iterator Function StrCmp_TestData() As IEnumerable(Of Object())
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
      'Yield New Object() {"a", "ABC", 32, If(PlatformDetection.IsInvariantGlobalization, -2, -1)}
      'Yield New Object() {"ABC", "a", -32, If(PlatformDetection.IsInvariantGlobalization, 2, 1)}
      Yield New Object() {"abc", "ABC", 32, 0}
    End Function
    <Theory>
    <InlineData(Nothing, Nothing, True, True)>
    <InlineData("", Nothing, True, True)>
    <InlineData("", "*", True, True)>
    <InlineData("", "?", False, False)>
    <InlineData("a", "?", True, True)>
    <InlineData("a3", "[A-Z]#", False, True)>
    <InlineData("A3", "[a-z]#", False, True)>
    Public Sub StrLike(source As String, pattern As String, expectedBinaryCompare As Boolean, expectedTextCompare As Boolean)
      Assert.Equal(expectedBinaryCompare, StringType.StrLike(source, pattern, CompareMethod.Binary))
      Assert.Equal(expectedTextCompare, StringType.StrLike(source, pattern, CompareMethod.Text))
      Assert.Equal(expectedBinaryCompare, StringType.StrLikeBinary(source, pattern))
      Assert.Equal(expectedTextCompare, StringType.StrLikeText(source, pattern))
    End Sub
    <Theory>
    <InlineData(Nothing, "*")>
    Public Sub StrLike_NullReferenceException(source As String, pattern As String)
      Assert.Throws(Of NullReferenceException)(Function() StringType.StrLike(source, pattern, CompareMethod.Binary))
      Assert.Throws(Of NullReferenceException)(Function() StringType.StrLike(source, pattern, CompareMethod.Text))
      Assert.Throws(Of NullReferenceException)(Function() StringType.StrLikeBinary(source, pattern))
      Assert.Throws(Of NullReferenceException)(Function() StringType.StrLikeText(source, pattern))
    End Sub

  End Class

End Namespace
