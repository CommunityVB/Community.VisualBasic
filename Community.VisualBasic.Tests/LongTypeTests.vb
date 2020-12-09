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

  Public Class LongTypeTests

    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As Long)
      Assert.Equal(expected, LongType.FromObject(value))
    End Sub

    ' The following conversions are not supported.

    <Theory>
    <MemberData(NameOf(FromObject_NotSupported_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() LongType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() LongType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Overflow_TestData))>
    Public Sub FromObject_ThrowsOverflowException(value As Object)
      Assert.Throws(Of OverflowException)(Function() LongType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As Long)
      Assert.Equal(expected, LongType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Invalid_TestData))>
    Public Sub FromString_ThrowsInvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() LongType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Overflow_TestData))>
    Public Sub FromString_ThrowsOverflowException(value As String)
      Assert.Throws(Of OverflowException)(Function() LongType.FromString(value))
    End Sub

    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CLng(Fix(0))}
      Yield New Object() {CByte(1), CLng(Fix(1))}
      Yield New Object() {Byte.MaxValue, CLng(Fix(255))}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, ByteEnum), CLng(Fix(1))}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CLng(Fix(255))}

      ' short.
      Yield New Object() {Short.MinValue, CLng(Fix((-32768)))}
      Yield New Object() {CShort(Fix((-1))), CLng(Fix((-1)))}
      Yield New Object() {CShort(Fix(0)), CLng(Fix(0))}
      Yield New Object() {CShort(Fix(1)), CLng(Fix(1))}
      Yield New Object() {Short.MaxValue, CLng(Fix(32767))}
      Yield New Object() {CType(Short.MinValue, ShortEnum), CLng(Fix((-32768)))}
      Yield New Object() {CType((-1), ShortEnum), CLng(Fix((-1)))}
      Yield New Object() {CType(0, ShortEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, ShortEnum), CLng(Fix(1))}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CLng(Fix(32767))}

      ' int.
      Yield New Object() {Integer.MinValue, CLng(Fix((-2147483648)))}
      Yield New Object() {-1, CLng(Fix((-1)))}
      Yield New Object() {0, CLng(Fix(0))}
      Yield New Object() {1, CLng(Fix(1))}
      Yield New Object() {Integer.MaxValue, CLng(Fix(2147483647))}
      Yield New Object() {CType(Integer.MinValue, IntEnum), CLng(Fix((-2147483648)))}
      Yield New Object() {CType((-1), IntEnum), CLng(Fix((-1)))}
      Yield New Object() {CType(0, IntEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, IntEnum), CLng(Fix(1))}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CLng(Fix(2147483647))}

      ' long.
      Yield New Object() {Long.MinValue, Long.MinValue}
      Yield New Object() {CLng(Fix((-1))), CLng(Fix((-1)))}
      Yield New Object() {CLng(Fix(0)), CLng(Fix(0))}
      Yield New Object() {CLng(Fix(1)), CLng(Fix(1))}
      Yield New Object() {Long.MaxValue, Long.MaxValue}
      Yield New Object() {CType(Long.MinValue, LongEnum), Long.MinValue}
      Yield New Object() {CType((-1), LongEnum), CLng(Fix((-1)))}
      Yield New Object() {CType(0, LongEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, LongEnum), CLng(Fix(1))}
      Yield New Object() {CType(Long.MaxValue, LongEnum), Long.MaxValue}

      ' float.
      Yield New Object() {CSng((-1)), CLng(Fix((-1)))}
      Yield New Object() {CSng(0), CLng(Fix(0))}
      Yield New Object() {CSng(1), CLng(Fix(1))}

      ' double.
      Yield New Object() {CDbl((-1)), CLng(Fix((-1)))}
      Yield New Object() {CDbl(0), CLng(Fix(0))}
      Yield New Object() {CDbl(1), CLng(Fix(1))}

      ' decimal.
      Yield New Object() {CDec((-1)), CLng(Fix((-1)))}
      Yield New Object() {CDec(0), CLng(Fix(0))}
      Yield New Object() {CDec(1), CLng(Fix(1))}

      ' bool.
      Yield New Object() {True, CLng(Fix((-1)))}
      Yield New Object() {False, CLng(Fix(0))}

      ' null.
      Yield New Object() {Nothing, CLng(Fix(0))}
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
      Yield New Object() {CType(ULong.MinValue, ULongEnum)}
      Yield New Object() {CType(1, ULongEnum)}
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

      ' ulong.
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}
    End Function

    Public Shared Iterator Function FromObject_Overflow_TestData() As IEnumerable(Of Object())
      Yield New Object() {Single.MinValue}
      Yield New Object() {Single.MaxValue}
      Yield New Object() {Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity}
      Yield New Object() {Single.NaN}
      Yield New Object() {Double.MinValue}
      Yield New Object() {Double.MaxValue}
      Yield New Object() {Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity}
      Yield New Object() {Double.NaN}
      Yield New Object() {Decimal.MinValue}
      Yield New Object() {Decimal.MaxValue}
    End Function

    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CLng(Fix(0))}
      Yield New Object() {"-1", CLng(Fix((-1)))}
      Yield New Object() {"0", CLng(Fix(0))}
      Yield New Object() {"1", CLng(Fix(1))}
      Yield New Object() {"&h5", CLng(Fix(5))}
      Yield New Object() {"&h0", CLng(Fix(0))}
      Yield New Object() {"&o5", CLng(Fix(5))}
      Yield New Object() {" &o5", CLng(Fix(5))}
      Yield New Object() {"&o0", CLng(Fix(0))}
      Yield New Object() {1.1.ToString(), CLng(Fix(1))}
    End Function

    Public Shared Iterator Function FromString_Invalid_TestData() As IEnumerable(Of Object())
      Yield New Object() {""}
      Yield New Object() {"&"}
      Yield New Object() {"&a"}
      Yield New Object() {"&a0"}
      Yield New Object() {"true"}
      Yield New Object() {"false"}
      Yield New Object() {"invalid"}
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function

    Public Shared Iterator Function FromString_Overflow_TestData() As IEnumerable(Of Object())
      Yield New Object() {"12345678901234567890"}
    End Function

  End Class

End Namespace