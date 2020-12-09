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

  Public Class ShortTypeTests

    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As Short)
      Assert.Equal(expected, ShortType.FromObject(value))
    End Sub

    ' The following conversions are not supported.

    <Theory>
    <MemberData(NameOf(FromObject_NotSupported_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() ShortType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() ShortType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Overflow_TestData))>
    Public Sub FromObject_ThrowsOverflowException(value As Object)
      Assert.Throws(Of OverflowException)(Function() ShortType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As Short)
      Assert.Equal(expected, ShortType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Invalid_TestData))>
    Public Sub FromString_ThrowsInvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() ShortType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Overflow_TestData))>
    Public Sub FromString_ThrowsOverflowException(value As String)
      Assert.Throws(Of OverflowException)(Function() ShortType.FromString(value))
    End Sub

    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {Byte.MinValue, CShort(Fix(0))}
      Yield New Object() {CByte(1), CShort(Fix(1))}
      Yield New Object() {Byte.MaxValue, CShort(Fix(255))}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, ByteEnum), CShort(Fix(1))}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CShort(Fix(255))}

      ' short.
      Yield New Object() {Short.MinValue, Short.MinValue}
      Yield New Object() {CShort(Fix((-1))), CShort(Fix((-1)))}
      Yield New Object() {CShort(Fix(0)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(1)), CShort(Fix(1))}
      Yield New Object() {Short.MaxValue, Short.MaxValue}
      Yield New Object() {CType(Short.MinValue, ShortEnum), Short.MinValue}
      Yield New Object() {CType((-1), ShortEnum), CShort(Fix((-1)))}
      Yield New Object() {CType(0, ShortEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, ShortEnum), CShort(Fix(1))}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), Short.MaxValue}

      ' int.
      Yield New Object() {-1, CShort(Fix((-1)))}
      Yield New Object() {0, CShort(Fix(0))}
      Yield New Object() {1, CShort(Fix(1))}
      Yield New Object() {CType((-1), IntEnum), CShort(Fix((-1)))}
      Yield New Object() {CType(0, IntEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, IntEnum), CShort(Fix(1))}

      ' long.
      Yield New Object() {CLng(Fix((-1))), CShort(Fix((-1)))}
      Yield New Object() {CLng(Fix(0)), CShort(Fix(0))}
      Yield New Object() {CLng(Fix(1)), CShort(Fix(1))}
      Yield New Object() {CType((-1), LongEnum), CShort(Fix((-1)))}
      Yield New Object() {CType(0, LongEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, LongEnum), CShort(Fix(1))}

      ' float.
      Yield New Object() {CSng((-1)), CShort(Fix((-1)))}
      Yield New Object() {CSng(0), CShort(Fix(0))}
      Yield New Object() {CSng(1), CShort(Fix(1))}

      ' double.
      Yield New Object() {CDbl((-1)), CShort(Fix((-1)))}
      Yield New Object() {CDbl(0), CShort(Fix(0))}
      Yield New Object() {CDbl(1), CShort(Fix(1))}

      ' decimal.
      Yield New Object() {CDec((-1)), CShort(Fix((-1)))}
      Yield New Object() {CDec(0), CShort(Fix(0))}
      Yield New Object() {CDec(1), CShort(Fix(1))}

      ' bool.
      Yield New Object() {True, CShort(Fix((-1)))}
      Yield New Object() {False, CShort(Fix(0))}

      ' null.
      Yield New Object() {Nothing, CShort(Fix(0))}

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
      Yield New Object() {CType(UShort.MinValue, UShortEnum)}
      Yield New Object() {CType(1, UShortEnum)}

      ' uint.
      Yield New Object() {UInteger.MinValue}
      Yield New Object() {CUInt(1)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum)}
      Yield New Object() {CType(1, UIntEnum)}

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

      ' ushort.
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}

    End Function

    Public Shared Iterator Function FromObject_Overflow_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MinValue}
      Yield New Object() {Integer.MaxValue}
      Yield New Object() {CType(Integer.MinValue, IntEnum)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum)}
    End Function

    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CShort(Fix(0))}
      Yield New Object() {"-1", CShort(Fix((-1)))}
      Yield New Object() {"0", CShort(Fix(0))}
      Yield New Object() {"1", CShort(Fix(1))}
      Yield New Object() {"&h5", CShort(Fix(5))}
      Yield New Object() {"&h0", CShort(Fix(0))}
      Yield New Object() {"&o5", CShort(Fix(5))}
      Yield New Object() {" &o5", CShort(Fix(5))}
      Yield New Object() {"&o0", CShort(Fix(0))}
      Yield New Object() {1.1.ToString(), CShort(Fix(1))}
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
      Yield New Object() {"32768"}
    End Function

  End Class

End Namespace