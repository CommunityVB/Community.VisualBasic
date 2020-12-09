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

  Public Class SingleTypeTests

    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As Single)
      Assert.Equal(expected, SingleType.FromObject(value))
      Assert.Equal(expected, SingleType.FromObject(value, System.Globalization.NumberFormatInfo.InvariantInfo))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Other_TestData))>
    Public Sub FromObject_Other(value As String, expected As Single)
      Assert.Equal(expected, SingleType.FromObject(value))
    End Sub


    ' The following conversions are not supported.
    <Theory>
    <MemberData(NameOf(FromObject_NotSupported_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() SingleType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() SingleType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Overflow_TestData))>
    Public Sub FromObject_ThrowsOverflowException(value As Object)
      Assert.Throws(Of OverflowException)(Function() SingleType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As Single)
      Assert.Equal(expected, SingleType.FromString(value))
      Assert.Equal(expected, SingleType.FromString(value, System.Globalization.NumberFormatInfo.InvariantInfo))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Other_TestData))>
    Public Sub FromString_Other(value As String, expected As Single)
      Assert.Equal(expected, SingleType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Invalid_TestData))>
    Public Sub FromString_ThrowsInvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() SingleType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Overflow_TestData))>
    Public Sub FromString_ThrowsOverflowException(value As String)
      Assert.Throws(Of OverflowException)(Function() SingleType.FromString(value))
    End Sub

    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {Byte.MinValue, CSng(0)}
      Yield New Object() {CByte(1), CSng(1)}
      Yield New Object() {Byte.MaxValue, CSng(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CSng(0)}
      Yield New Object() {CType(1, ByteEnum), CSng(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CSng(255)}

      ' short.
      Yield New Object() {Short.MinValue, CSng((-32768))}
      Yield New Object() {CShort(Fix((-1))), CSng((-1))}
      Yield New Object() {CShort(Fix(0)), CSng(0)}
      Yield New Object() {CShort(Fix(1)), CSng(1)}
      Yield New Object() {Short.MaxValue, CSng(32767)}
      Yield New Object() {CType(Short.MinValue, ShortEnum), CSng((-32768))}
      Yield New Object() {CType((-1), ShortEnum), CSng((-1))}
      Yield New Object() {CType(0, ShortEnum), CSng(0)}
      Yield New Object() {CType(1, ShortEnum), CSng(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CSng(32767)}

      ' int.
      Yield New Object() {Integer.MinValue, CSng(Integer.MinValue)}
      Yield New Object() {-1, CSng((-1))}
      Yield New Object() {0, CSng(0)}
      Yield New Object() {1, CSng(1)}
      Yield New Object() {Integer.MaxValue, CSng(Integer.MaxValue)}
      Yield New Object() {CType(Integer.MinValue, IntEnum), CSng(Integer.MinValue)}
      Yield New Object() {CType((-1), IntEnum), CSng((-1))}
      Yield New Object() {CType(0, IntEnum), CSng(0)}
      Yield New Object() {CType(1, IntEnum), CSng(1)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CSng(Integer.MaxValue)}

      ' long.
      Yield New Object() {Long.MinValue, CSng(Long.MinValue)}
      Yield New Object() {CLng(Fix((-1))), CSng((-1))}
      Yield New Object() {CLng(Fix(0)), CSng(0)}
      Yield New Object() {CLng(Fix(1)), CSng(1)}
      Yield New Object() {Long.MaxValue, CSng(Long.MaxValue)}
      Yield New Object() {CType(Long.MinValue, LongEnum), CSng(Long.MinValue)}
      Yield New Object() {CType((-1), LongEnum), CSng((-1))}
      Yield New Object() {CType(0, LongEnum), CSng(0)}
      Yield New Object() {CType(1, LongEnum), CSng(1)}
      Yield New Object() {CType(Long.MaxValue, LongEnum), CSng(Long.MaxValue)}

      ' float.
      Yield New Object() {Single.MinValue, Single.MinValue}
      Yield New Object() {CSng((-1)), CSng((-1))}
      Yield New Object() {CSng(0), CSng(0)}
      Yield New Object() {CSng(1), CSng(1)}
      Yield New Object() {Single.MaxValue, Single.MaxValue}
      Yield New Object() {Single.PositiveInfinity, Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity, Single.NegativeInfinity}
      Yield New Object() {Single.NaN, Single.NaN}

      ' double.
      Yield New Object() {Double.MinValue, Single.NegativeInfinity}
      Yield New Object() {CDbl((-1)), CSng((-1))}
      Yield New Object() {CDbl(0), CSng(0)}
      Yield New Object() {CDbl(1), CSng(1)}
      Yield New Object() {Double.MaxValue, Single.PositiveInfinity}
      Yield New Object() {Double.PositiveInfinity, Single.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity, Single.NegativeInfinity}
      Yield New Object() {Double.NaN, Single.NaN}

      ' decimal.
      Yield New Object() {Decimal.MinValue, CSng(Decimal.MinValue)}
      Yield New Object() {CDec((-1)), CSng((-1))}
      Yield New Object() {CDec(0), CSng(0)}
      Yield New Object() {CDec(1), CSng(1)}
      Yield New Object() {Decimal.MaxValue, CSng(Decimal.MaxValue)}

      ' bool.
      Yield New Object() {True, CSng((-1))}
      Yield New Object() {False, CSng(0)}

      ' null.
      Yield New Object() {Nothing, CSng(0)}

    End Function

    Public Shared Iterator Function FromObject_NotSupported_TestData() As IEnumerable(Of Object())

      ' sbyte.
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

      ' ushort.
      Yield New Object() {UShort.MinValue}
      Yield New Object() {CUShort(1)}
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MinValue, UShortEnum)}
      Yield New Object() {CType(1, UShortEnum)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}

      ' uint.
      Yield New Object() {UInteger.MinValue}
      Yield New Object() {CUInt(1)}
      Yield New Object() {UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum)}
      Yield New Object() {CType(1, UIntEnum)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum)}

      ' ulong.
      Yield New Object() {ULong.MinValue}
      Yield New Object() {CULng(1)}
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MinValue, ULongEnum)}
      Yield New Object() {CType(1, ULongEnum)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}

    End Function

    Public Shared Iterator Function FromObject_Invalid_TestData() As IEnumerable(Of Object())

      ' char.
      Yield New Object() {Char.MinValue}
      Yield New Object() {ChrW(1)}
      Yield New Object() {Char.MaxValue}

      ' DateTime.
      Yield New Object() {New DateTime(10)}

      ' object.
      Yield New Object() {New Object}

    End Function

    Public Shared Iterator Function FromObject_Overflow_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1234567890123456789012345678901234567890"}
    End Function

    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CSng(0)}
      Yield New Object() {"-1", CSng((-1))}
      Yield New Object() {"0", CSng(0)}
      Yield New Object() {"1", CSng(1)}
      Yield New Object() {"&h5", CSng(5)}
      Yield New Object() {"&h0", CSng(0)}
      Yield New Object() {"&o5", CSng(5)}
      Yield New Object() {" &o5", CSng(5)}
      Yield New Object() {"&o0", CSng(0)}
      Yield New Object() {"18446744073709551616", 1.84467441E+19F}
      Yield New Object() {Double.NaN.ToString(), Single.NaN}
    End Function

    Public Shared Iterator Function FromString_Other_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.PositiveInfinity.ToString(), Single.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity.ToString(), Single.NegativeInfinity}
    End Function

    Public Shared Iterator Function FromString_Invalid_TestData() As IEnumerable(Of Object())
      Yield New Object() {""}
      Yield New Object() {"&"}
      Yield New Object() {"&a"}
      Yield New Object() {"&a0"}
      Yield New Object() {"true"}
      Yield New Object() {"false"}
      Yield New Object() {"invalid"}
    End Function

    Public Shared Iterator Function FromString_Overflow_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function

  End Class

End Namespace