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

  Public Class DateTypeTests

    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As DateTime)
      Assert.Equal(expected, DateType.FromObject(value))
    End Sub

    ' The following conversions are not supported.

    <Theory>
    <MemberData(NameOf(FromObject_NotSupported_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() DateType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() DateType.FromObject(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As DateTime)
      Assert.Equal(expected, DateType.FromString(value))
      Assert.Equal(expected, DateType.FromString(value, System.Globalization.CultureInfo.InvariantCulture))
    End Sub

    ' The following conversions are not supported.

    <Theory>
    <MemberData(NameOf(FromString_NotSupported_TestData))>
    Public Sub FromString_NotSupported(value As String)
      Assert.Throws(Of InvalidCastException)(Function() DateType.FromString(value))
    End Sub

    <Theory>
    <MemberData(NameOf(FromString_Invalid_TestData))>
    Public Sub FromString_ThrowsInvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() DateType.FromString(value))
    End Sub

    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())
      ' null.
      Yield New Object() {Nothing, CType(Nothing, DateTime)}
    End Function

    Public Shared Iterator Function FromObject_NotSupported_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue}
      Yield New Object() {CByte(1)}
      Yield New Object() {Byte.MaxValue}
      Yield New Object() {CType(Byte.MinValue, ByteEnum)}
      Yield New Object() {CType(1, ByteEnum)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum)}

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

      ' short.
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

      ' uint.
      Yield New Object() {UInteger.MinValue}
      Yield New Object() {CUInt(1)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum)}
      Yield New Object() {CType(1, UIntEnum)}

      ' int.
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

      ' ulong.
      Yield New Object() {ULong.MinValue}
      Yield New Object() {CULng(1)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum)}
      Yield New Object() {CType(1, ULongEnum)}

      ' long.
      Yield New Object() {CLng(Fix((-1)))}
      Yield New Object() {CLng(Fix(0))}
      Yield New Object() {CLng(Fix(1))}
      Yield New Object() {CType((-1), LongEnum)}
      Yield New Object() {CType(0, LongEnum)}
      Yield New Object() {CType(1, LongEnum)}

      ' float.
      Yield New Object() {CSng((-1))}
      Yield New Object() {CSng(0)}
      Yield New Object() {CSng(1)}

      ' double.
      Yield New Object() {CDbl((-1))}
      Yield New Object() {CDbl(0)}
      Yield New Object() {CDbl(1)}

      ' decimal.
      Yield New Object() {CDec((-1))}
      Yield New Object() {CDec(0)}
      Yield New Object() {CDec(1)}

      ' bool.
      Yield New Object() {True}
      Yield New Object() {False}
    End Function

    Public Shared Iterator Function FromObject_Invalid_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Object}
    End Function

    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"12:00:00 AM", New DateTime(0)}
    End Function

    Public Shared Iterator Function FromString_NotSupported_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing}
      Yield New Object() {"-1"}
      Yield New Object() {"0"}
      Yield New Object() {"1"}
      Yield New Object() {"&h5"}
      Yield New Object() {"&h0"}
      Yield New Object() {"&o5"}
      Yield New Object() {" &o5"}
      Yield New Object() {"&o0"}
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

  End Class

End Namespace