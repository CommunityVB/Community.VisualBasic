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

  Public Class ByteTypeTests
    <Theory>
    <MemberData(NameOf(FromObject_TestData))>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromObject(value As Object, expected As Byte)
      Assert.Equal(expected, ByteType.FromObject(value))
    End Sub
    ' The following conversions are not supported.
    <Theory>
    <MemberData(NameOf(FromObject_NotSupported_TestData))>
    Public Sub FromObject_NotSupported(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() ByteType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromObject_Invalid_TestData))>
    Public Sub FromObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() ByteType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromObject_Overflow_TestData))>
    Public Sub FromObject_ThrowsOverflowException(value As Object)
      Assert.Throws(Of OverflowException)(Function() ByteType.FromObject(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromString_TestData))>
    Public Sub FromString(value As String, expected As Byte)
      Assert.Equal(expected, ByteType.FromString(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromString_Invalid_TestData))>
    Public Sub FromString_ThrowsInvalidCastException(value As String)
      Assert.Throws(Of InvalidCastException)(Function() ByteType.FromString(value))
    End Sub
    <Theory>
    <MemberData(NameOf(FromString_Overflow_TestData))>
    Public Sub FromString_ThrowsOverflowException(value As String)
      Assert.Throws(Of OverflowException)(Function() ByteType.FromString(value))
    End Sub
    Public Shared Iterator Function FromObject_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {CByte(1), CByte(1)}
      Yield New Object() {Byte.MaxValue, Byte.MaxValue}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), Byte.MinValue}
      Yield New Object() {CType(1, ByteEnum), CByte(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), Byte.MaxValue}

      ' short.
      Yield New Object() {CShort(Fix(0)), Byte.MinValue}
      Yield New Object() {CShort(Fix(1)), CByte(1)}
      Yield New Object() {CType(0, ShortEnum), Byte.MinValue}
      Yield New Object() {CType(1, ShortEnum), CByte(1)}

      ' int.
      Yield New Object() {0, Byte.MinValue}
      Yield New Object() {1, CByte(1)}
      Yield New Object() {CType(0, IntEnum), Byte.MinValue}
      Yield New Object() {CType(1, IntEnum), CByte(1)}

      ' long.
      Yield New Object() {CLng(Fix(0)), Byte.MinValue}
      Yield New Object() {CLng(Fix(1)), CByte(1)}
      Yield New Object() {CType(0, LongEnum), Byte.MinValue}
      Yield New Object() {CType(1, LongEnum), CByte(1)}

      ' float.
      Yield New Object() {CSng(0), Byte.MinValue}
      Yield New Object() {CSng(1), CByte(1)}

      ' double.
      Yield New Object() {CDbl(0), Byte.MinValue}
      Yield New Object() {CDbl(1), CByte(1)}

      ' decimal.
      Yield New Object() {CDec(0), Byte.MinValue}
      Yield New Object() {CDec(1), CByte(1)}

      ' bool.
      Yield New Object() {True, Byte.MaxValue}
      Yield New Object() {False, Byte.MinValue}

      ' null.
      Yield New Object() {Nothing, Byte.MinValue}
    End Function
    Public Shared Iterator Function FromObject_NotSupported_TestData() As IEnumerable(Of Object())
      ' sbyte.
      Yield New Object() {CSByte(0)}
      Yield New Object() {CSByte(1)}
      Yield New Object() {SByte.MaxValue}
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
      Yield New Object() {Short.MaxValue}
      Yield New Object() {CType(Short.MaxValue, ShortEnum)}
    End Function
    Public Shared Iterator Function FromString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Byte.MinValue}
      Yield New Object() {"0", Byte.MinValue}
      Yield New Object() {"1", CByte(1)}
      Yield New Object() {"&h5", CByte(5)}
      Yield New Object() {"&h0", Byte.MinValue}
      Yield New Object() {"&o5", CByte(5)}
      Yield New Object() {" &o5", CByte(5)}
      Yield New Object() {"&o0", Byte.MinValue}
      Yield New Object() {1.1.ToString(), CByte(1)}
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
      Yield New Object() {"256"}
    End Function
  End Class

End Namespace