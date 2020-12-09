' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Community.VisualBasic.Tests
Imports System
Imports System.Collections.Generic
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Public Class CharTypeTests
    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As Char)
      Assert.Equal(expected, CharType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() CharType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As Char)
      Assert.Equal(expected, CharType.FromString(value))
    End Sub
    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())
      ' char.
      Yield New Object() {Char.MinValue, Char.MinValue}
      Yield New Object() {ChrW(1), ChrW(1)}
      Yield New Object() {Char.MaxValue, Char.MaxValue}

      ' null.
      Yield New Object() {Nothing, Char.MinValue}
    End Function
    Public Shared Iterator Function FromObject_Invalid_TestData() As IEnumerable(Of Object())
      Yield New Object() {Byte.MinValue}
      Yield New Object() {CByte(1)}
      Yield New Object() {Byte.MaxValue}
      Yield New Object() {CType(Byte.MinValue, ByteEnum)}
      Yield New Object() {CType(1, ByteEnum)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum)}
      Yield New Object() {SByte.MinValue}
      Yield New Object() {CSByte((-1))}
      Yield New Object() {CSByte(0)}
      Yield New Object() {CSByte(1)}
      Yield New Object() {CSByte(1)}
      Yield New Object() {SByte.MaxValue}
      Yield New Object() {CType(SByte.MinValue, SByteEnum)}
      Yield New Object() {CType((-1), SByteEnum)}
      Yield New Object() {CType(0, SByteEnum)}
      Yield New Object() {CType(1, SByteEnum)}
      Yield New Object() {CType(1, SByteEnum)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum)}
      Yield New Object() {UShort.MinValue}
      Yield New Object() {CUShort(1)}
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MinValue, UShortEnum)}
      Yield New Object() {CType(1, UShortEnum)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}
      Yield New Object() {Short.MinValue}
      Yield New Object() {CShort(Fix((-1)))}
      Yield New Object() {CShort(Fix(0))}
      Yield New Object() {CShort(Fix(1))}
      Yield New Object() {Short.MaxValue}
      Yield New Object() {CType(Short.MinValue, ShortEnum)}
      Yield New Object() {CType((-1), ShortEnum)}
      Yield New Object() {CType(0, ShortEnum)}
      Yield New Object() {CType(1, ShortEnum)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum)}
      Yield New Object() {UInteger.MinValue}
      Yield New Object() {CUInt(1)}
      Yield New Object() {UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum)}
      Yield New Object() {CType(1, UIntEnum)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum)}
      Yield New Object() {Integer.MinValue}
      Yield New Object() {-1}
      Yield New Object() {0}
      Yield New Object() {1}
      Yield New Object() {Integer.MaxValue}
      Yield New Object() {CType(Integer.MinValue, IntEnum)}
      Yield New Object() {CType((-1), IntEnum)}
      Yield New Object() {CType(0, IntEnum)}
      Yield New Object() {CType(1, IntEnum)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum)}
      Yield New Object() {ULong.MinValue}
      Yield New Object() {CULng(1)}
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MinValue, ULongEnum)}
      Yield New Object() {CType(1, ULongEnum)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}
      Yield New Object() {Long.MinValue}
      Yield New Object() {CLng(Fix((-1)))}
      Yield New Object() {CLng(Fix(0))}
      Yield New Object() {CLng(Fix(1))}
      Yield New Object() {Long.MaxValue}
      Yield New Object() {CType(Long.MinValue, LongEnum)}
      Yield New Object() {CType((-1), LongEnum)}
      Yield New Object() {CType(0, LongEnum)}
      Yield New Object() {CType(1, LongEnum)}
      Yield New Object() {CType(Long.MaxValue, LongEnum)}
      Yield New Object() {Single.MinValue}
      Yield New Object() {CSng((-1))}
      Yield New Object() {CSng(0)}
      Yield New Object() {CSng(1)}
      Yield New Object() {Single.MaxValue}
      Yield New Object() {Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity}
      Yield New Object() {Single.NaN}
      Yield New Object() {Double.MinValue}
      Yield New Object() {CDbl((-1))}
      Yield New Object() {CDbl(0)}
      Yield New Object() {CDbl(1)}
      Yield New Object() {Double.MaxValue}
      Yield New Object() {Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity}
      Yield New Object() {Double.NaN}
      Yield New Object() {Decimal.MinValue}
      Yield New Object() {CDec((-1))}
      Yield New Object() {CDec(0)}
      Yield New Object() {CDec(1)}
      Yield New Object() {Decimal.MaxValue}
      Yield New Object() {True}
      Yield New Object() {False}
      Yield New Object() {New DateTime(10)}
      Yield New Object() {New Object}
    End Function
    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Char.MinValue}
      Yield New Object() {"", Char.MinValue}
      Yield New Object() {"-1", ChrW(45)}
      Yield New Object() {"0", "0"c}
      Yield New Object() {"1", "1"c}
      Yield New Object() {"&h5", ChrW(38)}
      Yield New Object() {"&h0", ChrW(38)}
      Yield New Object() {"&o5", ChrW(38)}
      Yield New Object() {" &o5", ChrW(32)}
      Yield New Object() {"&o0", ChrW(38)}
      Yield New Object() {"&", ChrW(38)}
      Yield New Object() {"&a", ChrW(38)}
      Yield New Object() {"&a0", ChrW(38)}
      Yield New Object() {1.1.ToString(), "1"c}
      Yield New Object() {"true", "t"c}
      Yield New Object() {"false", "f"c}
      Yield New Object() {"invalid", "i"c}
      Yield New Object() {"18446744073709551616", "1"c}
      Yield New Object() {"1844674407370955161618446744073709551616", "1"c}
    End Function
  End Class

End Namespace