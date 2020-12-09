' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Globalization
Imports System.Reflection
Imports System.Reflection.Emit
Imports Community.VisualBasic.CompilerServices
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class ConversionsTests
    Private Shared s_reflectionEmitSupported As Boolean? = Nothing
    Public Shared ReadOnly Property ReflectionEmitSupported As Boolean
      Get
        If s_reflectionEmitSupported Is Nothing Then
          Try
            Dim o As Object = FloatEnum
            s_reflectionEmitSupported = True
          Catch __unusedPlatformNotSupportedException1__ As PlatformNotSupportedException
            s_reflectionEmitSupported = False
          End Try
        End If

        Return s_reflectionEmitSupported.Value
      End Get
    End Property
    Public Shared Iterator Function InvalidString_TestData() As IEnumerable(Of Object())
      Yield New Object() {""}
      Yield New Object() {"&"}
      Yield New Object() {"&a"}
      Yield New Object() {"&a0"}
      Yield New Object() {"true"}
      Yield New Object() {"false"}
      Yield New Object() {"invalid"}
    End Function
    Public Shared Iterator Function InvalidBool_TestData() As IEnumerable(Of Object())
      If ReflectionEmitSupported Then
        Yield New Object() {FloatEnum}
        Yield New Object() {DoubleEnum}
        Yield New Object() {IntPtrEnum}
        Yield New Object() {UIntPtrEnum}
      End If
    End Function
    Public Shared Iterator Function InvalidNumberObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Char.MinValue}
      Yield New Object() {ChrW(1)}
      Yield New Object() {Char.MaxValue}
      Yield New Object() {New DateTime(10)}
      Yield New Object() {New Object}
      If ReflectionEmitSupported Then
        Yield New Object() {CharEnum}
      End If
    End Function
    Public Shared Iterator Function ToByte_String_TestData() As IEnumerable(Of Object())
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
    <Theory>
    <MemberData(NameOf(ToByte_String_TestData))>
    Public Sub ToByte_String_ReturnsExpected(value1 As String, expected As Byte)
      AssertEqual(expected, Conversions.ToByte(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToByte_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToByte(value1))
    End Sub
    Public Shared Iterator Function ToByte_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"256"}
    End Function
    <Theory>
    <MemberData(NameOf(ToByte_OverflowString_TestData))>
    <MemberData(NameOf(ToUShort_OverflowString_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToByte_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToByte(value1))
    End Sub
    Public Shared Iterator Function ToByte_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {CByte(1), CByte(1)}
      Yield New Object() {Byte.MaxValue, Byte.MaxValue}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), Byte.MinValue}
      Yield New Object() {CType(1, ByteEnum), CByte(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), Byte.MaxValue}

      ' sbyte.
      Yield New Object() {CSByte(0), Byte.MinValue}
      Yield New Object() {CSByte(1), CByte(1)}
      Yield New Object() {SByte.MaxValue, CByte(127)}
      Yield New Object() {CType(0, SByteEnum), Byte.MinValue}
      Yield New Object() {CType(1, SByteEnum), CByte(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CByte(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, Byte.MinValue}
      Yield New Object() {CUShort(1), CByte(1)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), Byte.MinValue}
      Yield New Object() {CType(1, UShortEnum), CByte(1)}

      ' short.
      Yield New Object() {CShort(Fix(0)), Byte.MinValue}
      Yield New Object() {CShort(Fix(1)), CByte(1)}
      Yield New Object() {CType(0, ShortEnum), Byte.MinValue}
      Yield New Object() {CType(1, ShortEnum), CByte(1)}

      ' uint.
      Yield New Object() {UInteger.MinValue, Byte.MinValue}
      Yield New Object() {CUInt(1), CByte(1)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), Byte.MinValue}
      Yield New Object() {CType(1, UIntEnum), CByte(1)}

      ' int.
      Yield New Object() {0, Byte.MinValue}
      Yield New Object() {1, CByte(1)}
      Yield New Object() {CType(0, IntEnum), Byte.MinValue}
      Yield New Object() {CType(1, IntEnum), CByte(1)}

      ' ulong.
      Yield New Object() {ULong.MinValue, Byte.MinValue}
      Yield New Object() {CULng(1), CByte(1)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), Byte.MinValue}
      Yield New Object() {CType(1, ULongEnum), CByte(1)}

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
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, Byte.MinValue}
      End If

      ' null.
      Yield New Object() {Nothing, Byte.MinValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToByte_Object_TestData))>
    <MemberData(NameOf(ToByte_String_TestData))>
    Public Sub ToByte_Object_ReturnsExpected(value1 As IConvertible, expected As Byte)
      AssertEqual(expected, Conversions.ToByte(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToByte(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToByte_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToByte(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToByte_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToByte(value1))
    End Sub
    Public Shared Iterator Function ToByte_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}
      Yield New Object() {Short.MaxValue}
      Yield New Object() {CType(Short.MaxValue, ShortEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToByte_OverflowObject_TestData))>
    <MemberData(NameOf(ToUShort_OverflowObject_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToULong_OverflowObject_TestData))>
    <MemberData(NameOf(ToByte_OverflowString_TestData))>
    <MemberData(NameOf(ToUShort_OverflowString_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToByte_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToByte(value1))
    End Sub
    Public Shared Iterator Function ToSByte_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CSByte(0)}
      Yield New Object() {"-1", CSByte((-1))}
      Yield New Object() {"0", CSByte(0)}
      Yield New Object() {"1", CSByte(1)}
      Yield New Object() {"&h5", CSByte(5)}
      Yield New Object() {"&h0", CSByte(0)}
      Yield New Object() {"&o5", CSByte(5)}
      Yield New Object() {" &o5", CSByte(5)}
      Yield New Object() {"&o0", CSByte(0)}
      Yield New Object() {1.1.ToString(), CSByte(1)}
    End Function
    <Theory>
    <MemberData(NameOf(ToSByte_String_TestData))>
    Public Sub ToSByte_String_ReturnsExpected(value1 As String, expected As SByte)
      AssertEqual(expected, Conversions.ToSByte(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToSByte_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToSByte(value1))
    End Sub
    Public Shared Iterator Function ToSByte_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"128"}
    End Function
    <Theory>
    <MemberData(NameOf(ToSByte_OverflowString_TestData))>
    <MemberData(NameOf(ToShort_OverflowString_TestData))>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToSByte_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToSByte(value1))
    End Sub
    Public Shared Iterator Function ToSByte_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CSByte(0)}
      Yield New Object() {CByte(1), CSByte(1)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CSByte(0)}
      Yield New Object() {CType(1, ByteEnum), CSByte(1)}

      ' sbyte.
      Yield New Object() {SByte.MinValue, SByte.MinValue}
      Yield New Object() {CSByte((-1)), CSByte((-1))}
      Yield New Object() {CSByte(0), CSByte(0)}
      Yield New Object() {CSByte(1), CSByte(1)}
      Yield New Object() {SByte.MaxValue, SByte.MaxValue}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), SByte.MinValue}
      Yield New Object() {CType((-1), SByteEnum), CSByte((-1))}
      Yield New Object() {CType(0, SByteEnum), CSByte(0)}
      Yield New Object() {CType(1, SByteEnum), CSByte(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), SByte.MaxValue}

      ' ushort.
      Yield New Object() {UShort.MinValue, CSByte(0)}
      Yield New Object() {CUShort(1), CSByte(1)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CSByte(0)}
      Yield New Object() {CType(1, UShortEnum), CSByte(1)}

      ' short.
      Yield New Object() {CShort(Fix((-1))), CSByte((-1))}
      Yield New Object() {CShort(Fix(0)), CSByte(0)}
      Yield New Object() {CShort(Fix(1)), CSByte(1)}
      Yield New Object() {CType((-1), ShortEnum), CSByte((-1))}
      Yield New Object() {CType(0, ShortEnum), CSByte(0)}
      Yield New Object() {CType(1, ShortEnum), CSByte(1)}

      ' uint.
      Yield New Object() {UInteger.MinValue, CSByte(0)}
      Yield New Object() {CUInt(1), CSByte(1)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CSByte(0)}
      Yield New Object() {CType(1, UIntEnum), CSByte(1)}

      ' int.
      Yield New Object() {-1, CSByte((-1))}
      Yield New Object() {0, CSByte(0)}
      Yield New Object() {1, CSByte(1)}
      Yield New Object() {CType((-1), IntEnum), CSByte((-1))}
      Yield New Object() {CType(0, IntEnum), CSByte(0)}
      Yield New Object() {CType(1, IntEnum), CSByte(1)}

      ' ulong.
      Yield New Object() {ULong.MinValue, CSByte(0)}
      Yield New Object() {CULng(1), CSByte(1)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CSByte(0)}
      Yield New Object() {CType(1, ULongEnum), CSByte(1)}

      ' long.
      Yield New Object() {CLng(Fix((-1))), CSByte((-1))}
      Yield New Object() {CLng(Fix(0)), CSByte(0)}
      Yield New Object() {CLng(Fix(1)), CSByte(1)}
      Yield New Object() {CType((-1), LongEnum), CSByte((-1))}
      Yield New Object() {CType(0, LongEnum), CSByte(0)}
      Yield New Object() {CType(1, LongEnum), CSByte(1)}

      ' float.
      Yield New Object() {CSng((-1)), CSByte((-1))}
      Yield New Object() {CSng(0), CSByte(0)}
      Yield New Object() {CSng(1), CSByte(1)}

      ' double.
      Yield New Object() {CDbl((-1)), CSByte((-1))}
      Yield New Object() {CDbl(0), CSByte(0)}
      Yield New Object() {CDbl(1), CSByte(1)}

      ' decimal.
      Yield New Object() {CDec((-1)), CSByte((-1))}
      Yield New Object() {CDec(0), CSByte(0)}
      Yield New Object() {CDec(1), CSByte(1)}

      ' bool.
      Yield New Object() {True, CSByte((-1))}
      Yield New Object() {False, CSByte(0)}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CSByte(0)}
      End If

      ' null.
      Yield New Object() {Nothing, CSByte(0)}
    End Function
    <Theory>
    <MemberData(NameOf(ToSByte_Object_TestData))>
    <MemberData(NameOf(ToSByte_String_TestData))>
    Public Sub ToSByte_Object_ReturnsExpected(value1 As IConvertible, expected As SByte)
      AssertEqual(expected, Conversions.ToSByte(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToSByte(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToSByte_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToSByte(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToSByte_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToSByte(value1))
    End Sub
    Public Shared Iterator Function ToSByte_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Byte.MaxValue}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum)}
      Yield New Object() {Short.MinValue}
      Yield New Object() {Short.MaxValue}
      Yield New Object() {CType(Short.MinValue, ShortEnum)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToSByte_OverflowObject_TestData))>
    <MemberData(NameOf(ToShort_OverflowObject_TestData))>
    <MemberData(NameOf(ToInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToLong_OverflowObject_TestData))>
    <MemberData(NameOf(ToSByte_OverflowString_TestData))>
    <MemberData(NameOf(ToShort_OverflowString_TestData))>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToSByte_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToSByte(value1))
    End Sub
    Public Shared Iterator Function ToUShort_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, UShort.MinValue}
      Yield New Object() {"0", UShort.MinValue}
      Yield New Object() {"1", CUShort(1)}
      Yield New Object() {"&h5", CUShort(5)}
      Yield New Object() {"&h0", UShort.MinValue}
      Yield New Object() {"&o5", CUShort(5)}
      Yield New Object() {" &o5", CUShort(5)}
      Yield New Object() {"&o0", UShort.MinValue}
      Yield New Object() {1.1.ToString(), CUShort(1)}
    End Function
    <Theory>
    <MemberData(NameOf(ToUShort_String_TestData))>
    Public Sub ToUShort_String_ReturnsExpected(value1 As String, expected As UShort)
      AssertEqual(expected, Conversions.ToUShort(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToUShort_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToUShort(value1))
    End Sub
    Public Shared Iterator Function ToUShort_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"65536"}
    End Function
    <Theory>
    <MemberData(NameOf(ToUShort_OverflowString_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToUShort_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToUShort(value1))
    End Sub
    Public Shared Iterator Function ToUShort_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, UShort.MinValue}
      Yield New Object() {CByte(1), CUShort(1)}
      Yield New Object() {Byte.MaxValue, CUShort(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), UShort.MinValue}
      Yield New Object() {CType(1, ByteEnum), CUShort(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CUShort(255)}

      ' sbyte.
      Yield New Object() {CSByte(0), UShort.MinValue}
      Yield New Object() {CSByte(1), CUShort(1)}
      Yield New Object() {SByte.MaxValue, CUShort(127)}
      Yield New Object() {CType(0, SByteEnum), UShort.MinValue}
      Yield New Object() {CType(1, SByteEnum), CUShort(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CUShort(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, UShort.MinValue}
      Yield New Object() {CUShort(1), CUShort(1)}
      Yield New Object() {UShort.MaxValue, UShort.MaxValue}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), UShort.MinValue}
      Yield New Object() {CType(1, UShortEnum), CUShort(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), UShort.MaxValue}

      ' short.
      Yield New Object() {CShort(Fix(0)), UShort.MinValue}
      Yield New Object() {CShort(Fix(1)), CUShort(1)}
      Yield New Object() {Short.MaxValue, CUShort(32767)}
      Yield New Object() {CType(0, ShortEnum), UShort.MinValue}
      Yield New Object() {CType(1, ShortEnum), CUShort(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CUShort(32767)}

      ' uint.
      Yield New Object() {UInteger.MinValue, UShort.MinValue}
      Yield New Object() {CUInt(1), CUShort(1)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), UShort.MinValue}
      Yield New Object() {CType(1, UIntEnum), CUShort(1)}

      ' int.
      Yield New Object() {0, UShort.MinValue}
      Yield New Object() {1, CUShort(1)}
      Yield New Object() {CType(0, IntEnum), UShort.MinValue}
      Yield New Object() {CType(1, IntEnum), CUShort(1)}

      ' ulong.
      Yield New Object() {ULong.MinValue, UShort.MinValue}
      Yield New Object() {CULng(1), CUShort(1)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), UShort.MinValue}
      Yield New Object() {CType(1, ULongEnum), CUShort(1)}

      ' long.
      Yield New Object() {CLng(Fix(0)), UShort.MinValue}
      Yield New Object() {CLng(Fix(1)), CUShort(1)}
      Yield New Object() {CType(0, LongEnum), UShort.MinValue}
      Yield New Object() {CType(1, LongEnum), CUShort(1)}

      ' float.
      Yield New Object() {CSng(0), UShort.MinValue}
      Yield New Object() {CSng(1), CUShort(1)}

      ' double.
      Yield New Object() {CDbl(0), UShort.MinValue}
      Yield New Object() {CDbl(1), CUShort(1)}

      ' decimal.
      Yield New Object() {CDec(0), UShort.MinValue}
      Yield New Object() {CDec(1), CUShort(1)}

      ' bool.
      Yield New Object() {True, UShort.MaxValue}
      Yield New Object() {False, UShort.MinValue}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, UShort.MinValue}
      End If

      ' null.
      Yield New Object() {Nothing, UShort.MinValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToUShort_Object_TestData))>
    <MemberData(NameOf(ToUShort_String_TestData))>
    Public Sub ToUShort_Object_ReturnsExpected(value1 As IConvertible, expected As UShort)
      AssertEqual(expected, Conversions.ToUShort(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToUShort(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToUShort_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToUShort(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToUShort_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToUShort(value1))
    End Sub
    Public Shared Iterator Function ToUShort_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Integer.MaxValue}
      Yield New Object() {CType(Integer.MaxValue, IntEnum)}
      Yield New Object() {UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToUShort_OverflowString_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    <MemberData(NameOf(ToUShort_OverflowObject_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToULong_OverflowObject_TestData))>
    Public Sub ToUShort_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToUShort(value1))
    End Sub
    Public Shared Iterator Function ToShort_String_TestData() As IEnumerable(Of Object())
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
    <Theory>
    <MemberData(NameOf(ToShort_String_TestData))>
    Public Sub ToShort_String_ReturnsExpected(value1 As String, expected As Short)
      AssertEqual(expected, Conversions.ToShort(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToShort_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToShort(value1))
    End Sub
    Public Shared Iterator Function ToShort_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"32768"}
    End Function
    <Theory>
    <MemberData(NameOf(ToShort_OverflowString_TestData))>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToShort_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToShort(value1))
    End Sub
    Public Shared Iterator Function ToShort_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CShort(Fix(0))}
      Yield New Object() {CByte(1), CShort(Fix(1))}
      Yield New Object() {Byte.MaxValue, CShort(Fix(255))}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, ByteEnum), CShort(Fix(1))}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CShort(Fix(255))}

      ' sbyte.
      Yield New Object() {SByte.MinValue, CShort(Fix((-128)))}
      Yield New Object() {CSByte((-1)), CShort(Fix((-1)))}
      Yield New Object() {CSByte(0), CShort(Fix(0))}
      Yield New Object() {CSByte(1), CShort(Fix(1))}
      Yield New Object() {SByte.MaxValue, CShort(Fix(127))}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), CShort(Fix((-128)))}
      Yield New Object() {CType((-1), SByteEnum), CShort(Fix((-1)))}
      Yield New Object() {CType(0, SByteEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, SByteEnum), CShort(Fix(1))}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CShort(Fix(127))}

      ' ushort.
      Yield New Object() {UShort.MinValue, CShort(Fix(0))}
      Yield New Object() {CUShort(1), CShort(Fix(1))}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, UShortEnum), CShort(Fix(1))}

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

      ' uint.
      Yield New Object() {UInteger.MinValue, CShort(Fix(0))}
      Yield New Object() {CUInt(1), CShort(Fix(1))}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, UIntEnum), CShort(Fix(1))}

      ' int.
      Yield New Object() {-1, CShort(Fix((-1)))}
      Yield New Object() {0, CShort(Fix(0))}
      Yield New Object() {1, CShort(Fix(1))}
      Yield New Object() {CType((-1), IntEnum), CShort(Fix((-1)))}
      Yield New Object() {CType(0, IntEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, IntEnum), CShort(Fix(1))}

      ' ulong.
      Yield New Object() {ULong.MinValue, CShort(Fix(0))}
      Yield New Object() {CULng(1), CShort(Fix(1))}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CShort(Fix(0))}
      Yield New Object() {CType(1, ULongEnum), CShort(Fix(1))}

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
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CShort(Fix(0))}
      End If

      ' null.
      Yield New Object() {Nothing, CShort(Fix(0))}
    End Function
    <Theory>
    <MemberData(NameOf(ToShort_Object_TestData))>
    <MemberData(NameOf(ToShort_String_TestData))>
    Public Sub ToShort_Object_ReturnsExpected(value1 As IConvertible, expected As Short)
      AssertEqual(expected, Conversions.ToShort(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToShort(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToShort_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToShort(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToShort_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToShort(value1))
    End Sub
    Public Shared Iterator Function ToShort_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {UShort.MaxValue}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum)}
      Yield New Object() {Integer.MinValue}
      Yield New Object() {Integer.MaxValue}
      Yield New Object() {CType(Integer.MinValue, IntEnum)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToShort_OverflowObject_TestData))>
    <MemberData(NameOf(ToInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToLong_OverflowObject_TestData))>
    <MemberData(NameOf(ToShort_OverflowString_TestData))>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToShort_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToShort(value1))
    End Sub
    Public Shared Iterator Function ToUInteger_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, UInteger.MinValue}
      Yield New Object() {"0", UInteger.MinValue}
      Yield New Object() {"1", CUInt(1)}
      Yield New Object() {"&h5", CUInt(5)}
      Yield New Object() {"&h0", UInteger.MinValue}
      Yield New Object() {"&o5", CUInt(5)}
      Yield New Object() {" &o5", CUInt(5)}
      Yield New Object() {"&o0", UInteger.MinValue}
      Yield New Object() {1.1.ToString(), CUInt(1)}
    End Function
    <Theory>
    <MemberData(NameOf(ToUInteger_String_TestData))>
    Public Sub ToUInteger_String_ReturnsExpected(value1 As String, expected As UInteger)
      AssertEqual(expected, Conversions.ToUInteger(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToUInteger_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToUInteger(value1))
    End Sub
    Public Shared Iterator Function ToUInteger_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"4294967296"}
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToUInteger_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToUInteger(value1))
    End Sub
    Public Shared Iterator Function ToUInteger_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, UInteger.MinValue}
      Yield New Object() {CByte(1), CUInt(1)}
      Yield New Object() {Byte.MaxValue, CUInt(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), UInteger.MinValue}
      Yield New Object() {CType(1, ByteEnum), CUInt(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CUInt(255)}

      ' sbyte.
      Yield New Object() {CSByte(0), UInteger.MinValue}
      Yield New Object() {CSByte(1), CUInt(1)}
      Yield New Object() {SByte.MaxValue, CUInt(127)}
      Yield New Object() {CType(0, SByteEnum), UInteger.MinValue}
      Yield New Object() {CType(1, SByteEnum), CUInt(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CUInt(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, UInteger.MinValue}
      Yield New Object() {CUShort(1), CUInt(1)}
      Yield New Object() {UShort.MaxValue, CUInt(65535)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), UInteger.MinValue}
      Yield New Object() {CType(1, UShortEnum), CUInt(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CUInt(65535)}

      ' short.
      Yield New Object() {CShort(Fix(0)), UInteger.MinValue}
      Yield New Object() {CShort(Fix(1)), CUInt(1)}
      Yield New Object() {Short.MaxValue, CUInt(32767)}
      Yield New Object() {CType(0, ShortEnum), UInteger.MinValue}
      Yield New Object() {CType(1, ShortEnum), CUInt(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CUInt(32767)}

      ' uint.
      Yield New Object() {UInteger.MinValue, UInteger.MinValue}
      Yield New Object() {CUInt(1), CUInt(1)}
      Yield New Object() {UInteger.MaxValue, UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), UInteger.MinValue}
      Yield New Object() {CType(1, UIntEnum), CUInt(1)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), UInteger.MaxValue}

      ' int.
      Yield New Object() {0, UInteger.MinValue}
      Yield New Object() {1, CUInt(1)}
      Yield New Object() {Integer.MaxValue, CUInt(2147483647)}
      Yield New Object() {CType(0, IntEnum), UInteger.MinValue}
      Yield New Object() {CType(1, IntEnum), CUInt(1)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CUInt(2147483647)}

      ' ulong.
      Yield New Object() {ULong.MinValue, UInteger.MinValue}
      Yield New Object() {CULng(1), CUInt(1)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), UInteger.MinValue}
      Yield New Object() {CType(1, ULongEnum), CUInt(1)}

      ' long.
      Yield New Object() {CLng(Fix(0)), UInteger.MinValue}
      Yield New Object() {CLng(Fix(1)), CUInt(1)}
      Yield New Object() {CType(0, LongEnum), UInteger.MinValue}
      Yield New Object() {CType(1, LongEnum), CUInt(1)}

      ' float.
      Yield New Object() {CSng(0), UInteger.MinValue}
      Yield New Object() {CSng(1), CUInt(1)}

      ' double.
      Yield New Object() {CDbl(0), UInteger.MinValue}
      Yield New Object() {CDbl(1), CUInt(1)}

      ' decimal.
      Yield New Object() {CDec(0), UInteger.MinValue}
      Yield New Object() {CDec(1), CUInt(1)}

      ' bool.
      Yield New Object() {True, UInteger.MaxValue}
      Yield New Object() {False, UInteger.MinValue}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, UInteger.MinValue}
      End If

      ' null.
      Yield New Object() {Nothing, UInteger.MinValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToUInteger_Object_TestData))>
    <MemberData(NameOf(ToUInteger_String_TestData))>
    Public Sub ToUInteger_Object_ReturnsExpected(value1 As IConvertible, expected As UInteger)
      AssertEqual(expected, Conversions.ToUInteger(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToUInteger(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToUInteger_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToUInteger(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToUInteger_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToUInteger(value1))
    End Sub
    Public Shared Iterator Function ToUInteger_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}
      Yield New Object() {Long.MaxValue}
      Yield New Object() {CType(Long.MaxValue, LongEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToUInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToULong_OverflowObject_TestData))>
    <MemberData(NameOf(ToUInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToUInteger_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToUInteger(value1))
    End Sub
    Public Shared Iterator Function ToInteger_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 0}
      Yield New Object() {"-1", -1}
      Yield New Object() {"0", 0}
      Yield New Object() {"1", 1}
      Yield New Object() {"&h5", 5}
      Yield New Object() {"&h0", 0}
      Yield New Object() {"&o5", 5}
      Yield New Object() {" &o5", 5}
      Yield New Object() {"&o0", 0}
      Yield New Object() {1.1.ToString(), 1}
    End Function
    <Theory>
    <MemberData(NameOf(ToInteger_String_TestData))>
    Public Sub ToInteger_String_ReturnsExpected(value1 As String, expected As Integer)
      AssertEqual(expected, Conversions.ToInteger(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToInteger_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToInteger(value1))
    End Sub
    Public Shared Iterator Function ToInteger_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"2147483648"}
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToInteger_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToInteger(value1))
    End Sub
    Public Shared Iterator Function ToInteger_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, 0}
      Yield New Object() {CByte(1), 1}
      Yield New Object() {Byte.MaxValue, 255}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), 0}
      Yield New Object() {CType(1, ByteEnum), 1}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), 255}

      ' sbyte.
      Yield New Object() {SByte.MinValue, -128}
      Yield New Object() {CSByte((-1)), -1}
      Yield New Object() {CSByte(0), 0}
      Yield New Object() {CSByte(1), 1}
      Yield New Object() {SByte.MaxValue, 127}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), -128}
      Yield New Object() {CType((-1), SByteEnum), -1}
      Yield New Object() {CType(0, SByteEnum), 0}
      Yield New Object() {CType(1, SByteEnum), 1}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), 127}

      ' ushort.
      Yield New Object() {UShort.MinValue, 0}
      Yield New Object() {CUShort(1), 1}
      Yield New Object() {UShort.MaxValue, 65535}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), 0}
      Yield New Object() {CType(1, UShortEnum), 1}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), 65535}

      ' short.
      Yield New Object() {Short.MinValue, -32768}
      Yield New Object() {CShort(Fix((-1))), -1}
      Yield New Object() {CShort(Fix(0)), 0}
      Yield New Object() {CShort(Fix(1)), 1}
      Yield New Object() {Short.MaxValue, 32767}
      Yield New Object() {CType(Short.MinValue, ShortEnum), -32768}
      Yield New Object() {CType((-1), ShortEnum), -1}
      Yield New Object() {CType(0, ShortEnum), 0}
      Yield New Object() {CType(1, ShortEnum), 1}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), 32767}

      ' uint.
      Yield New Object() {UInteger.MinValue, 0}
      Yield New Object() {CUInt(1), 1}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), 0}
      Yield New Object() {CType(1, UIntEnum), 1}

      ' int.
      Yield New Object() {Integer.MinValue, Integer.MinValue}
      Yield New Object() {-1, -1}
      Yield New Object() {0, 0}
      Yield New Object() {1, 1}
      Yield New Object() {Integer.MaxValue, Integer.MaxValue}
      Yield New Object() {CType(Integer.MinValue, IntEnum), Integer.MinValue}
      Yield New Object() {CType((-1), IntEnum), -1}
      Yield New Object() {CType(0, IntEnum), 0}
      Yield New Object() {CType(1, IntEnum), 1}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), Integer.MaxValue}

      ' ulong.
      Yield New Object() {ULong.MinValue, 0}
      Yield New Object() {CULng(1), 1}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), 0}
      Yield New Object() {CType(1, ULongEnum), 1}

      ' long.
      Yield New Object() {CLng(Fix((-1))), -1}
      Yield New Object() {CLng(Fix(0)), 0}
      Yield New Object() {CLng(Fix(1)), 1}
      Yield New Object() {CType((-1), LongEnum), -1}
      Yield New Object() {CType(0, LongEnum), 0}
      Yield New Object() {CType(1, LongEnum), 1}

      ' float.
      Yield New Object() {CSng((-1)), -1}
      Yield New Object() {CSng(0), 0}
      Yield New Object() {CSng(1), 1}

      ' double.
      Yield New Object() {CDbl((-1)), -1}
      Yield New Object() {CDbl(0), 0}
      Yield New Object() {CDbl(1), 1}

      ' decimal.
      Yield New Object() {CDec((-1)), -1}
      Yield New Object() {CDec(0), 0}
      Yield New Object() {CDec(1), 1}

      ' bool.
      Yield New Object() {True, -1}
      Yield New Object() {False, 0}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, 0}
      End If

      ' null.
      Yield New Object() {Nothing, 0}
    End Function
    <Theory>
    <MemberData(NameOf(ToInteger_Object_TestData))>
    <MemberData(NameOf(ToInteger_String_TestData))>
    Public Sub ToInteger_Object_ReturnsExpected(value1 As IConvertible, expected As Integer)
      AssertEqual(expected, Conversions.ToInteger(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToInteger(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToInteger_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToInteger(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToInteger_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToInteger(value1))
    End Sub
    Public Shared Iterator Function ToInteger_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {UInteger.MaxValue}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum)}
      Yield New Object() {Long.MinValue}
      Yield New Object() {Long.MaxValue}
      Yield New Object() {CType(Long.MinValue, LongEnum)}
      Yield New Object() {CType(Long.MaxValue, LongEnum)}
    End Function
    <Theory>
    <MemberData(NameOf(ToInteger_OverflowObject_TestData))>
    <MemberData(NameOf(ToLong_OverflowObject_TestData))>
    <MemberData(NameOf(ToInteger_OverflowString_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToInteger_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToInteger(value1))
    End Sub
    Public Shared Iterator Function ToULong_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, ULong.MinValue}
      Yield New Object() {"0", ULong.MinValue}
      Yield New Object() {"1", CULng(1)}
      Yield New Object() {"&h5", CULng(5)}
      Yield New Object() {"&h0", ULong.MinValue}
      Yield New Object() {"&o5", CULng(5)}
      Yield New Object() {" &o5", CULng(5)}
      Yield New Object() {"&o0", ULong.MinValue}
      Yield New Object() {1.1.ToString(), CULng(1)}
    End Function
    <Theory>
    <MemberData(NameOf(ToULong_String_TestData))>
    Public Sub ToULong_String_ReturnsExpected(value1 As String, expected As ULong)
      AssertEqual(expected, Conversions.ToULong(value1))
    End Sub
    Public Shared Iterator Function ToULong_InvalidString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToULong_InvalidString_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToULong_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToULong(value1))
    End Sub
    Public Shared Iterator Function ToULong_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"-1"}
      Yield New Object() {"18446744073709551616"}
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToULong_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToULong(value1))
    End Sub
    Public Shared Iterator Function ToULong_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, ULong.MinValue}
      Yield New Object() {CByte(1), CULng(1)}
      Yield New Object() {Byte.MaxValue, CULng(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), ULong.MinValue}
      Yield New Object() {CType(1, ByteEnum), CULng(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CULng(255)}

      ' sbyte.
      Yield New Object() {CSByte(0), ULong.MinValue}
      Yield New Object() {CSByte(1), CULng(1)}
      Yield New Object() {SByte.MaxValue, CULng(127)}
      Yield New Object() {CType(0, SByteEnum), ULong.MinValue}
      Yield New Object() {CType(1, SByteEnum), CULng(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CULng(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, ULong.MinValue}
      Yield New Object() {CUShort(1), CULng(1)}
      Yield New Object() {UShort.MaxValue, CULng(65535)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), ULong.MinValue}
      Yield New Object() {CType(1, UShortEnum), CULng(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CULng(65535)}

      ' short.
      Yield New Object() {CShort(Fix(0)), ULong.MinValue}
      Yield New Object() {CShort(Fix(1)), CULng(1)}
      Yield New Object() {Short.MaxValue, CULng(32767)}
      Yield New Object() {CType(0, ShortEnum), ULong.MinValue}
      Yield New Object() {CType(1, ShortEnum), CULng(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CULng(32767)}

      ' uint.
      Yield New Object() {UInteger.MinValue, ULong.MinValue}
      Yield New Object() {CUInt(1), CULng(1)}
      Yield New Object() {UInteger.MaxValue, CULng(4294967295)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), ULong.MinValue}
      Yield New Object() {CType(1, UIntEnum), CULng(1)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), CULng(4294967295)}

      ' int.
      Yield New Object() {0, ULong.MinValue}
      Yield New Object() {1, CULng(1)}
      Yield New Object() {Integer.MaxValue, CULng(2147483647)}
      Yield New Object() {CType(0, IntEnum), ULong.MinValue}
      Yield New Object() {CType(1, IntEnum), CULng(1)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CULng(2147483647)}

      ' ulong.
      Yield New Object() {ULong.MinValue, ULong.MinValue}
      Yield New Object() {CULng(1), CULng(1)}
      Yield New Object() {ULong.MaxValue, ULong.MaxValue}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), ULong.MinValue}
      Yield New Object() {CType(1, ULongEnum), CULng(1)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), ULong.MaxValue}

      ' long.
      Yield New Object() {CLng(Fix(0)), ULong.MinValue}
      Yield New Object() {CLng(Fix(1)), CULng(1)}
      Yield New Object() {Long.MaxValue, CULng(9223372036854775807)}
      Yield New Object() {CType(0, LongEnum), ULong.MinValue}
      Yield New Object() {CType(1, LongEnum), CULng(1)}
      Yield New Object() {CType(Long.MaxValue, LongEnum), CULng(9223372036854775807)}

      ' float.
      Yield New Object() {CSng(0), ULong.MinValue}
      Yield New Object() {CSng(1), CULng(1)}

      ' double.
      Yield New Object() {CDbl(0), ULong.MinValue}
      Yield New Object() {CDbl(1), CULng(1)}

      ' decimal.
      Yield New Object() {CDec(0), ULong.MinValue}
      Yield New Object() {CDec(1), CULng(1)}

      ' bool.
      Yield New Object() {True, ULong.MaxValue}
      Yield New Object() {False, ULong.MinValue}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, ULong.MinValue}
      End If

      ' null.
      Yield New Object() {Nothing, ULong.MinValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToULong_Object_TestData))>
    <MemberData(NameOf(ToULong_String_TestData))>
    Public Sub ToULong_Object_ReturnsExpected(value1 As IConvertible, expected As ULong)
      AssertEqual(expected, Conversions.ToULong(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToULong(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(ToULong_InvalidString_TestData))>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToULong_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToULong(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToULong_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToULong(value1))
    End Sub
    Public Shared Iterator Function ToULong_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {SByte.MinValue}
      Yield New Object() {CSByte((-1))}
      Yield New Object() {CType(SByte.MinValue, SByteEnum)}
      Yield New Object() {CType((-1), SByteEnum)}
      Yield New Object() {Short.MinValue}
      Yield New Object() {CShort(Fix((-1)))}
      Yield New Object() {CType(Short.MinValue, ShortEnum)}
      Yield New Object() {CType((-1), ShortEnum)}
      Yield New Object() {Integer.MinValue}
      Yield New Object() {-1}
      Yield New Object() {CType(Integer.MinValue, IntEnum)}
      Yield New Object() {CType((-1), IntEnum)}
      Yield New Object() {Long.MinValue}
      Yield New Object() {CLng(Fix((-1)))}
      Yield New Object() {CType(Long.MinValue, LongEnum)}
      Yield New Object() {CType((-1), LongEnum)}
      Yield New Object() {Single.MinValue}
      Yield New Object() {CSng((-1))}
      Yield New Object() {Single.MaxValue}
      Yield New Object() {Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity}
      Yield New Object() {Single.NaN}
      Yield New Object() {Double.MinValue}
      Yield New Object() {CDbl((-1))}
      Yield New Object() {Double.MaxValue}
      Yield New Object() {Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity}
      Yield New Object() {Double.NaN}
      Yield New Object() {Decimal.MinValue}
      Yield New Object() {CDec((-1))}
      Yield New Object() {Decimal.MaxValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToULong_OverflowObject_TestData))>
    <MemberData(NameOf(ToULong_OverflowString_TestData))>
    Public Sub ToULong_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToULong(value1))
    End Sub
    Public Shared Iterator Function ToLong_String_TestData() As IEnumerable(Of Object())
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
    <Theory>
    <MemberData(NameOf(ToLong_String_TestData))>
    Public Sub ToLong_String_ReturnsExpected(value1 As String, expected As Long)
      AssertEqual(expected, Conversions.ToLong(value1))
    End Sub
    Public Shared Iterator Function ToLong_InvalidString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToLong_InvalidString_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToLong_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToLong(value1))
    End Sub
    Public Shared Iterator Function ToLong_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"9223372036854775808"}
      Yield New Object() {"18446744073709551616"}
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToLong_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToLong(value1))
    End Sub
    Public Shared Iterator Function ToLong_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CLng(Fix(0))}
      Yield New Object() {CByte(1), CLng(Fix(1))}
      Yield New Object() {Byte.MaxValue, CLng(Fix(255))}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, ByteEnum), CLng(Fix(1))}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CLng(Fix(255))}

      ' sbyte.
      Yield New Object() {SByte.MinValue, CLng(Fix((-128)))}
      Yield New Object() {CSByte((-1)), CLng(Fix((-1)))}
      Yield New Object() {CSByte(0), CLng(Fix(0))}
      Yield New Object() {CSByte(1), CLng(Fix(1))}
      Yield New Object() {SByte.MaxValue, CLng(Fix(127))}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), CLng(Fix((-128)))}
      Yield New Object() {CType((-1), SByteEnum), CLng(Fix((-1)))}
      Yield New Object() {CType(0, SByteEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, SByteEnum), CLng(Fix(1))}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CLng(Fix(127))}

      ' ushort.
      Yield New Object() {UShort.MinValue, CLng(Fix(0))}
      Yield New Object() {CUShort(1), CLng(Fix(1))}
      Yield New Object() {UShort.MaxValue, CLng(Fix(65535))}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, UShortEnum), CLng(Fix(1))}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CLng(Fix(65535))}

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

      ' uint.
      Yield New Object() {UInteger.MinValue, CLng(Fix(0))}
      Yield New Object() {CUInt(1), CLng(Fix(1))}
      Yield New Object() {UInteger.MaxValue, CLng(Fix(4294967295))}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, UIntEnum), CLng(Fix(1))}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), CLng(Fix(4294967295))}

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

      ' ulong.
      Yield New Object() {ULong.MinValue, CLng(Fix(0))}
      Yield New Object() {CULng(1), CLng(Fix(1))}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CLng(Fix(0))}
      Yield New Object() {CType(1, ULongEnum), CLng(Fix(1))}

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
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CLng(Fix(0))}
      End If

      ' null.
      Yield New Object() {Nothing, CLng(Fix(0))}
    End Function
    <Theory>
    <MemberData(NameOf(ToLong_Object_TestData))>
    <MemberData(NameOf(ToLong_String_TestData))>
    Public Sub ToLong_Object_ReturnsExpected(value1 As IConvertible, expected As Long)
      AssertEqual(expected, Conversions.ToLong(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToLong(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToLong_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToLong_InvalidObject_TestData))>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToLong_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToLong(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToLong_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToLong(value1))
    End Sub
    Public Shared Iterator Function ToLong_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {ULong.MaxValue}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum)}
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
    <Theory>
    <MemberData(NameOf(ToLong_OverflowObject_TestData))>
    <MemberData(NameOf(ToLong_OverflowString_TestData))>
    Public Sub ToLong_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToLong(value1))
    End Sub
    Public Shared Iterator Function ToSingle_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CSng(0)}
      Yield New Object() {"-1", CSng((-1))}
      Yield New Object() {"0", CSng(0)}
      Yield New Object() {"1", CSng(1)}
      Yield New Object() {"&h5", CSng(5)}
      Yield New Object() {"&h0", CSng(0)}
      Yield New Object() {"&o5", CSng(5)}
      Yield New Object() {" &o5", CSng(5)}
      Yield New Object() {"&o0", CSng(0)}
      Yield New Object() {1.1.ToString(), CSng(1.1)}
      Yield New Object() {"18446744073709551616", 1.84467441E+19F}
      Yield New Object() {Double.PositiveInfinity.ToString(), Single.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity.ToString(), Single.NegativeInfinity}
      Yield New Object() {Double.NaN.ToString(), Single.NaN}
    End Function
    <Theory>
    <MemberData(NameOf(ToSingle_String_TestData))>
    Public Sub ToSingle_String_ReturnsExpected(value1 As String, expected As Single)
      AssertEqual(expected, Conversions.ToSingle(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToSingle_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToSingle(value1))
    End Sub
    Public Shared Iterator Function ToSingle_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToSingle_OverflowString_TestData))>
    Public Sub ToSingle_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToSingle(value1))
    End Sub
    Public Shared Iterator Function ToSingle_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CSng(0)}
      Yield New Object() {CByte(1), CSng(1)}
      Yield New Object() {Byte.MaxValue, CSng(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CSng(0)}
      Yield New Object() {CType(1, ByteEnum), CSng(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CSng(255)}

      ' sbyte.
      Yield New Object() {SByte.MinValue, CSng((-128))}
      Yield New Object() {CSByte((-1)), CSng((-1))}
      Yield New Object() {CSByte(0), CSng(0)}
      Yield New Object() {CSByte(1), CSng(1)}
      Yield New Object() {SByte.MaxValue, CSng(127)}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), CSng((-128))}
      Yield New Object() {CType((-1), SByteEnum), CSng((-1))}
      Yield New Object() {CType(0, SByteEnum), CSng(0)}
      Yield New Object() {CType(1, SByteEnum), CSng(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CSng(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, CSng(0)}
      Yield New Object() {CUShort(1), CSng(1)}
      Yield New Object() {UShort.MaxValue, CSng(65535)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CSng(0)}
      Yield New Object() {CType(1, UShortEnum), CSng(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CSng(65535)}

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

      ' uint.
      Yield New Object() {UInteger.MinValue, CSng(0)}
      Yield New Object() {CUInt(1), CSng(1)}
      Yield New Object() {UInteger.MaxValue, CSng(UInteger.MaxValue)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CSng(0)}
      Yield New Object() {CType(1, UIntEnum), CSng(1)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), CSng(UInteger.MaxValue)}

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

      ' ulong.
      Yield New Object() {ULong.MinValue, CSng(0)}
      Yield New Object() {CULng(1), CSng(1)}
      Yield New Object() {ULong.MaxValue, CSng(ULong.MaxValue)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CSng(0)}
      Yield New Object() {CType(1, ULongEnum), CSng(1)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), CSng(ULong.MaxValue)}

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
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CSng(0)}
      End If

      ' null.
      Yield New Object() {Nothing, CSng(0)}
    End Function
    <Theory>
    <MemberData(NameOf(ToSingle_Object_TestData))>
    <MemberData(NameOf(ToSingle_String_TestData))>
    Public Sub ToSingle_Object_ReturnsExpected(value1 As IConvertible, expected As Single)
      AssertEqual(expected, Conversions.ToSingle(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToSingle(New ConvertibleWrapper(value1)))
      End If
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToSingle_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToSingle(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToSingle_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToSingle(value1))
    End Sub
    Public Shared Iterator Function ToSingle_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToSingle_OverflowObject_TestData))>
    Public Sub ToSingle_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToSingle(value1))
    End Sub
    Public Shared Iterator Function ToDouble_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CDbl(0)}
      Yield New Object() {"-1", CDbl((-1))}
      Yield New Object() {"0", CDbl(0)}
      Yield New Object() {"1", CDbl(1)}
      Yield New Object() {"&h5", CDbl(5)}
      Yield New Object() {"&h0", CDbl(0)}
      Yield New Object() {"&o5", CDbl(5)}
      Yield New Object() {" &o5", CDbl(5)}
      Yield New Object() {"&o0", CDbl(0)}
      Yield New Object() {1.1.ToString(), CDbl(1.1)}
      Yield New Object() {"18446744073709551616", 1.8446744073709552E+19}
      Yield New Object() {"1844674407370955161618446744073709551616", 1.8446744073709552E+39}
      Yield New Object() {Double.PositiveInfinity.ToString(), Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity.ToString(), Double.NegativeInfinity}
      Yield New Object() {Double.NaN.ToString(), Double.NaN}
    End Function
    <Theory>
    <MemberData(NameOf(ToDouble_String_TestData))>
    Public Sub ToDouble_String_ReturnsExpected(value1 As String, expected As Double)
      AssertEqual(expected, Conversions.ToDouble(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToDouble_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToDouble(value1))
    End Sub
    Public Shared Iterator Function ToDouble_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CDbl(0)}
      Yield New Object() {CByte(1), CDbl(1)}
      Yield New Object() {Byte.MaxValue, CDbl(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CDbl(0)}
      Yield New Object() {CType(1, ByteEnum), CDbl(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CDbl(255)}

      ' sbyte.
      Yield New Object() {SByte.MinValue, CDbl((-128))}
      Yield New Object() {CSByte((-1)), CDbl((-1))}
      Yield New Object() {CSByte(0), CDbl(0)}
      Yield New Object() {CSByte(1), CDbl(1)}
      Yield New Object() {SByte.MaxValue, CDbl(127)}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), CDbl((-128))}
      Yield New Object() {CType((-1), SByteEnum), CDbl((-1))}
      Yield New Object() {CType(0, SByteEnum), CDbl(0)}
      Yield New Object() {CType(1, SByteEnum), CDbl(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CDbl(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, CDbl(0)}
      Yield New Object() {CUShort(1), CDbl(1)}
      Yield New Object() {UShort.MaxValue, CDbl(65535)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CDbl(0)}
      Yield New Object() {CType(1, UShortEnum), CDbl(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CDbl(65535)}

      ' short.
      Yield New Object() {Short.MinValue, CDbl((-32768))}
      Yield New Object() {CShort(Fix((-1))), CDbl((-1))}
      Yield New Object() {CShort(Fix(0)), CDbl(0)}
      Yield New Object() {CShort(Fix(1)), CDbl(1)}
      Yield New Object() {Short.MaxValue, CDbl(32767)}
      Yield New Object() {CType(Short.MinValue, ShortEnum), CDbl((-32768))}
      Yield New Object() {CType((-1), ShortEnum), CDbl((-1))}
      Yield New Object() {CType(0, ShortEnum), CDbl(0)}
      Yield New Object() {CType(1, ShortEnum), CDbl(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CDbl(32767)}

      ' uint.
      Yield New Object() {UInteger.MinValue, CDbl(0)}
      Yield New Object() {CUInt(1), CDbl(1)}
      Yield New Object() {UInteger.MaxValue, CDbl(4294967295)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CDbl(0)}
      Yield New Object() {CType(1, UIntEnum), CDbl(1)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), CDbl(4294967295)}

      ' int.
      Yield New Object() {Integer.MinValue, CDbl((-2147483648))}
      Yield New Object() {-1, CDbl((-1))}
      Yield New Object() {0, CDbl(0)}
      Yield New Object() {1, CDbl(1)}
      Yield New Object() {Integer.MaxValue, CDbl(2147483647)}
      Yield New Object() {CType(Integer.MinValue, IntEnum), CDbl((-2147483648))}
      Yield New Object() {CType((-1), IntEnum), CDbl((-1))}
      Yield New Object() {CType(0, IntEnum), CDbl(0)}
      Yield New Object() {CType(1, IntEnum), CDbl(1)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CDbl(2147483647)}

      ' ulong.
      Yield New Object() {ULong.MinValue, CDbl(0)}
      Yield New Object() {CULng(1), CDbl(1)}
      Yield New Object() {ULong.MaxValue, CDbl(ULong.MaxValue)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CDbl(0)}
      Yield New Object() {CType(1, ULongEnum), CDbl(1)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), CDbl(ULong.MaxValue)}

      ' long.
      Yield New Object() {Long.MinValue, CDbl(Long.MinValue)}
      Yield New Object() {CLng(Fix((-1))), CDbl((-1))}
      Yield New Object() {CLng(Fix(0)), CDbl(0)}
      Yield New Object() {CLng(Fix(1)), CDbl(1)}
      Yield New Object() {Long.MaxValue, CDbl(Long.MaxValue)}
      Yield New Object() {CType(Long.MinValue, LongEnum), CDbl(Long.MinValue)}
      Yield New Object() {CType((-1), LongEnum), CDbl((-1))}
      Yield New Object() {CType(0, LongEnum), CDbl(0)}
      Yield New Object() {CType(1, LongEnum), CDbl(1)}
      Yield New Object() {CType(Long.MaxValue, LongEnum), CDbl(Long.MaxValue)}

      ' float.
      Yield New Object() {Single.MinValue, CDbl(Single.MinValue)}
      Yield New Object() {CSng((-1)), CDbl((-1))}
      Yield New Object() {CSng(0), CDbl(0)}
      Yield New Object() {CSng(1), CDbl(1)}
      Yield New Object() {Single.MaxValue, CDbl(Single.MaxValue)}
      Yield New Object() {Single.PositiveInfinity, Double.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity, Double.NegativeInfinity}
      Yield New Object() {Single.NaN, Double.NaN}

      ' double.
      Yield New Object() {Double.MinValue, Double.MinValue}
      Yield New Object() {CDbl((-1)), CDbl((-1))}
      Yield New Object() {CDbl(0), CDbl(0)}
      Yield New Object() {CDbl(1), CDbl(1)}
      Yield New Object() {Double.MaxValue, Double.MaxValue}
      Yield New Object() {Double.PositiveInfinity, Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity, Double.NegativeInfinity}
      Yield New Object() {Double.NaN, Double.NaN}

      ' decimal.
      Yield New Object() {Decimal.MinValue, CDbl(Decimal.MinValue)}
      Yield New Object() {CDec((-1)), CDbl((-1))}
      Yield New Object() {CDec(0), CDbl(0)}
      Yield New Object() {CDec(1), CDbl(1)}
      Yield New Object() {Decimal.MaxValue, CDbl(Decimal.MaxValue)}

      ' bool.
      Yield New Object() {True, CDbl((-1))}
      Yield New Object() {False, CDbl(0)}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CDbl(0)}
      End If

      ' null.
      Yield New Object() {Nothing, CDbl(0)}
    End Function
    <Theory>
    <MemberData(NameOf(ToDouble_Object_TestData))>
    <MemberData(NameOf(ToDouble_String_TestData))>
    Public Sub ToDouble_Object_ReturnsExpected(value1 As IConvertible, expected As Double)
      AssertEqual(expected, Conversions.ToDouble(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToDouble(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToDouble_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Char.MinValue}
      Yield New Object() {ChrW(1)}
      Yield New Object() {Char.MaxValue}
      Yield New Object() {New DateTime(10)}
      Yield New Object() {New Object}
    End Function
    <Theory>
    <MemberData(NameOf(ToDouble_InvalidObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToDouble_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToDouble(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToDouble_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToDouble(value1))
    End Sub
    Public Shared Iterator Function ToDecimal_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, CDec(0)}
      Yield New Object() {"-1", CDec((-1))}
      Yield New Object() {"0", CDec(0)}
      Yield New Object() {"1", CDec(1)}
      Yield New Object() {"&h5", CDec(5)}
      Yield New Object() {"&h0", CDec(0)}
      Yield New Object() {"&o5", CDec(5)}
      Yield New Object() {" &o5", CDec(5)}
      Yield New Object() {"&o0", CDec(0)}
      Yield New Object() {1.1.ToString(), CDec(1.1)}
      Yield New Object() {"18446744073709551616", 18446744073709551616D}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_String_TestData))>
    Public Sub ToDecimal_String_ReturnsExpected(value1 As String, expected As Decimal)
      AssertEqual(expected, Conversions.ToDecimal(value1))
    End Sub
    Public Shared Iterator Function ToDecimal_InvalidString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_InvalidString_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToDecimal_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToDecimal(value1))
    End Sub
    Public Shared Iterator Function ToDecimal_OverflowString_TestData() As IEnumerable(Of Object())
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_OverflowString_TestData))>
    Public Sub ToDecimal_OverflowString_ThrowsOverflowException(value1 As String)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToDecimal(value1))
    End Sub
    Public Shared Iterator Function ToDecimal_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, CDec(0)}
      Yield New Object() {CByte(1), CDec(1)}
      Yield New Object() {Byte.MaxValue, CDec(255)}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), CDec(0)}
      Yield New Object() {CType(1, ByteEnum), CDec(1)}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), CDec(255)}

      ' sbyte.
      Yield New Object() {SByte.MinValue, CDec((-128))}
      Yield New Object() {CSByte((-1)), CDec((-1))}
      Yield New Object() {CSByte(0), CDec(0)}
      Yield New Object() {CSByte(1), CDec(1)}
      Yield New Object() {SByte.MaxValue, CDec(127)}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), CDec((-128))}
      Yield New Object() {CType((-1), SByteEnum), CDec((-1))}
      Yield New Object() {CType(0, SByteEnum), CDec(0)}
      Yield New Object() {CType(1, SByteEnum), CDec(1)}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), CDec(127)}

      ' ushort.
      Yield New Object() {UShort.MinValue, CDec(0)}
      Yield New Object() {CUShort(1), CDec(1)}
      Yield New Object() {UShort.MaxValue, CDec(65535)}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), CDec(0)}
      Yield New Object() {CType(1, UShortEnum), CDec(1)}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), CDec(65535)}

      ' short.
      Yield New Object() {Short.MinValue, CDec((-32768))}
      Yield New Object() {CShort(Fix((-1))), CDec((-1))}
      Yield New Object() {CShort(Fix(0)), CDec(0)}
      Yield New Object() {CShort(Fix(1)), CDec(1)}
      Yield New Object() {Short.MaxValue, CDec(32767)}
      Yield New Object() {CType(Short.MinValue, ShortEnum), CDec((-32768))}
      Yield New Object() {CType((-1), ShortEnum), CDec((-1))}
      Yield New Object() {CType(0, ShortEnum), CDec(0)}
      Yield New Object() {CType(1, ShortEnum), CDec(1)}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), CDec(32767)}

      ' uint.
      Yield New Object() {UInteger.MinValue, CDec(0)}
      Yield New Object() {CUInt(1), CDec(1)}
      Yield New Object() {UInteger.MaxValue, CDec(4294967295)}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), CDec(0)}
      Yield New Object() {CType(1, UIntEnum), CDec(1)}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), CDec(4294967295)}

      ' int.
      Yield New Object() {Integer.MinValue, CDec((-2147483648))}
      Yield New Object() {-1, CDec((-1))}
      Yield New Object() {0, CDec(0)}
      Yield New Object() {1, CDec(1)}
      Yield New Object() {Integer.MaxValue, CDec(2147483647)}
      Yield New Object() {CType(Integer.MinValue, IntEnum), CDec((-2147483648))}
      Yield New Object() {CType((-1), IntEnum), CDec((-1))}
      Yield New Object() {CType(0, IntEnum), CDec(0)}
      Yield New Object() {CType(1, IntEnum), CDec(1)}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), CDec(2147483647)}

      ' ulong.
      Yield New Object() {ULong.MinValue, CDec(0)}
      Yield New Object() {CULng(1), CDec(1)}
      Yield New Object() {ULong.MaxValue, CDec(ULong.MaxValue)}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), CDec(0)}
      Yield New Object() {CType(1, ULongEnum), CDec(1)}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), CDec(ULong.MaxValue)}

      ' long.
      Yield New Object() {Long.MinValue, CDec((-9223372036854775808UL))}
      Yield New Object() {CLng(Fix((-1))), CDec((-1))}
      Yield New Object() {CLng(Fix(0)), CDec(0)}
      Yield New Object() {CLng(Fix(1)), CDec(1)}
      Yield New Object() {Long.MaxValue, CDec(9223372036854775807)}
      Yield New Object() {CType(Long.MinValue, LongEnum), CDec((-9223372036854775808UL))}
      Yield New Object() {CType((-1), LongEnum), CDec((-1))}
      Yield New Object() {CType(0, LongEnum), CDec(0)}
      Yield New Object() {CType(1, LongEnum), CDec(1)}
      Yield New Object() {CType(Long.MaxValue, LongEnum), CDec(9223372036854775807)}

      ' float.
      Yield New Object() {CSng((-1)), CDec((-1))}
      Yield New Object() {CSng(0), CDec(0)}
      Yield New Object() {CSng(1), CDec(1)}

      ' double.
      Yield New Object() {CDbl((-1)), CDec((-1))}
      Yield New Object() {CDbl(0), CDec(0)}
      Yield New Object() {CDbl(1), CDec(1)}

      ' decimal.
      Yield New Object() {Decimal.MinValue, Decimal.MinValue}
      Yield New Object() {CDec((-1)), CDec((-1))}
      Yield New Object() {CDec(0), CDec(0)}
      Yield New Object() {CDec(1), CDec(1)}
      Yield New Object() {Decimal.MaxValue, Decimal.MaxValue}

      ' bool.
      Yield New Object() {True, CDec((-1))}
      Yield New Object() {False, CDec(0)}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, CDec(0)}
      End If

      ' null.
      Yield New Object() {Nothing, CDec(0)}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_Object_TestData))>
    <MemberData(NameOf(ToDecimal_String_TestData))>
    Public Sub ToDecimal_Object_ReturnsExpected(value1 As IConvertible, expected As Decimal)
      AssertEqual(expected, Conversions.ToDecimal(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToDecimal(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToDecimal_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Char.MinValue}
      Yield New Object() {ChrW(1)}
      Yield New Object() {Char.MaxValue}
      Yield New Object() {New DateTime(10)}
      Yield New Object() {Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN.ToString()}
      Yield New Object() {New Object}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_InvalidObject_TestData))>
    <MemberData(NameOf(InvalidString_TestData))>
    Public Sub ToDecimal_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToDecimal(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToDecimal_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToDecimal(value1))
    End Sub
    Public Shared Iterator Function ToDecimal_OverflowObject_TestData() As IEnumerable(Of Object())
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
      Yield New Object() {"1844674407370955161618446744073709551616"}
    End Function
    <Theory>
    <MemberData(NameOf(ToDecimal_OverflowObject_TestData))>
    Public Sub ToDecimal_OverflowObject_ThrowsOverflowException(value1 As Object)
      Assert.Throws(Of OverflowException)(Function() Conversions.ToDecimal(value1))
    End Sub
    Public Shared Iterator Function ToBoolean_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {"-1", True}
      Yield New Object() {"0", False}
      Yield New Object() {"1", True}
      Yield New Object() {"&h5", True}
      Yield New Object() {"&h0", False}
      Yield New Object() {"&o5", True}
      Yield New Object() {" &o5", True}
      Yield New Object() {"&o0", False}
      Yield New Object() {1.1.ToString(), True}
      Yield New Object() {"true", True}
      Yield New Object() {"false", False}
      Yield New Object() {"18446744073709551616", True}
      Yield New Object() {"1844674407370955161618446744073709551616", True}
      Yield New Object() {Double.PositiveInfinity.ToString(), True}
      Yield New Object() {Double.NegativeInfinity.ToString(), True}
      Yield New Object() {Double.NaN.ToString(), True}
    End Function
    <Theory>
    <MemberData(NameOf(ToBoolean_String_TestData))>
    Public Sub ToBoolean_String_ReturnsExpected(value1 As String, expected As Boolean)
      AssertEqual(expected, Conversions.ToBoolean(value1))
    End Sub
    Public Shared Iterator Function ToBoolean_InvalidString_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing}
      Yield New Object() {""}
      Yield New Object() {"&"}
      Yield New Object() {"&a"}
      Yield New Object() {"&a0"}
      Yield New Object() {"invalid"}
    End Function
    <Theory>
    <MemberData(NameOf(ToBoolean_InvalidString_TestData))>
    Public Sub ToBoolean_InvalidString_ThrowsInvalidCastException(value1 As String)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToBoolean(value1))
    End Sub
    Public Shared Iterator Function ToBoolean_Object_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, False}
      Yield New Object() {CByte(1), True}
      Yield New Object() {Byte.MaxValue, True}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), False}
      Yield New Object() {CType(1, ByteEnum), True}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), True}

      ' sbyte.
      Yield New Object() {SByte.MinValue, True}
      Yield New Object() {CSByte((-1)), True}
      Yield New Object() {CSByte(0), False}
      Yield New Object() {CSByte(1), True}
      Yield New Object() {SByte.MaxValue, True}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), True}
      Yield New Object() {CType((-1), SByteEnum), True}
      Yield New Object() {CType(0, SByteEnum), False}
      Yield New Object() {CType(1, SByteEnum), True}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), True}

      ' ushort.
      Yield New Object() {UShort.MinValue, False}
      Yield New Object() {CUShort(1), True}
      Yield New Object() {UShort.MaxValue, True}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), False}
      Yield New Object() {CType(1, UShortEnum), True}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), True}

      ' short.
      Yield New Object() {Short.MinValue, True}
      Yield New Object() {CShort(Fix((-1))), True}
      Yield New Object() {CShort(Fix(0)), False}
      Yield New Object() {CShort(Fix(1)), True}
      Yield New Object() {Short.MaxValue, True}
      Yield New Object() {CType(Short.MinValue, ShortEnum), True}
      Yield New Object() {CType((-1), ShortEnum), True}
      Yield New Object() {CType(0, ShortEnum), False}
      Yield New Object() {CType(1, ShortEnum), True}
      Yield New Object() {CType(Short.MaxValue, ShortEnum), True}

      ' uint.
      Yield New Object() {UInteger.MinValue, False}
      Yield New Object() {CUInt(1), True}
      Yield New Object() {UInteger.MaxValue, True}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), False}
      Yield New Object() {CType(1, UIntEnum), True}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), True}

      ' int.
      Yield New Object() {Integer.MinValue, True}
      Yield New Object() {-1, True}
      Yield New Object() {0, False}
      Yield New Object() {1, True}
      Yield New Object() {Integer.MaxValue, True}
      Yield New Object() {CType(Integer.MinValue, IntEnum), True}
      Yield New Object() {CType((-1), IntEnum), True}
      Yield New Object() {CType(0, IntEnum), False}
      Yield New Object() {CType(1, IntEnum), True}
      Yield New Object() {CType(Integer.MaxValue, IntEnum), True}

      ' ulong.
      Yield New Object() {ULong.MinValue, False}
      Yield New Object() {CULng(1), True}
      Yield New Object() {ULong.MaxValue, True}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), False}
      Yield New Object() {CType(1, ULongEnum), True}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), True}

      ' long.
      Yield New Object() {Long.MinValue, True}
      Yield New Object() {CLng(Fix((-1))), True}
      Yield New Object() {CLng(Fix(0)), False}
      Yield New Object() {CLng(Fix(1)), True}
      Yield New Object() {Long.MaxValue, True}
      Yield New Object() {CType(Long.MinValue, LongEnum), True}
      Yield New Object() {CType((-1), LongEnum), True}
      Yield New Object() {CType(0, LongEnum), False}
      Yield New Object() {CType(1, LongEnum), True}
      Yield New Object() {CType(Long.MaxValue, LongEnum), True}

      ' float.
      Yield New Object() {Single.MinValue, True}
      Yield New Object() {CSng((-1)), True}
      Yield New Object() {CSng(0), False}
      Yield New Object() {CSng(1), True}
      Yield New Object() {Single.MaxValue, True}
      Yield New Object() {Single.PositiveInfinity, True}
      Yield New Object() {Single.NegativeInfinity, True}
      Yield New Object() {Single.NaN, True}

      ' double.
      Yield New Object() {Double.MinValue, True}
      Yield New Object() {CDbl((-1)), True}
      Yield New Object() {CDbl(0), False}
      Yield New Object() {CDbl(1), True}
      Yield New Object() {Double.MaxValue, True}
      Yield New Object() {Double.PositiveInfinity, True}
      Yield New Object() {Double.NegativeInfinity, True}
      Yield New Object() {Double.NaN, True}

      ' decimal.
      Yield New Object() {Decimal.MinValue, True}
      Yield New Object() {CDec((-1)), True}
      Yield New Object() {CDec(0), False}
      Yield New Object() {CDec(1), True}
      Yield New Object() {Decimal.MaxValue, True}

      ' bool.
      Yield New Object() {True, True}
      Yield New Object() {False, False}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, False}
      End If

      ' null.
      Yield New Object() {Nothing, False}
    End Function
    <Theory>
    <MemberData(NameOf(ToBoolean_Object_TestData))>
    <MemberData(NameOf(ToBoolean_String_TestData))>
    Public Sub ToBoolean_Object_ReturnsExpected(value1 As IConvertible, expected As Boolean)
      AssertEqual(expected, Conversions.ToBoolean(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToBoolean(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToBoolean_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {""}
      Yield New Object() {"&"}
      Yield New Object() {"&a"}
      Yield New Object() {"&a0"}
      Yield New Object() {"invalid"}
    End Function
    <Theory>
    <MemberData(NameOf(ToBoolean_InvalidObject_TestData))>
    <MemberData(NameOf(InvalidNumberObject_TestData))>
    Public Sub ToBoolean_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToBoolean(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToBoolean_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToBoolean(value1))
    End Sub
    Public Shared Iterator Function ToChar_String_TestData() As IEnumerable(Of Object())
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
    <Theory>
    <MemberData(NameOf(ToChar_String_TestData))>
    Public Sub ToChar_String_ReturnsExpected(value1 As String, expected As Char)
      AssertEqual(expected, Conversions.ToChar(value1))
    End Sub
    Public Shared Iterator Function ToChar_Object_TestData() As IEnumerable(Of Object())
      ' char.
      Yield New Object() {Char.MinValue, Char.MinValue}
      Yield New Object() {ChrW(1), ChrW(1)}
      Yield New Object() {Char.MaxValue, Char.MaxValue}

      ' null.
      Yield New Object() {Nothing, Char.MinValue}
    End Function
    <Theory>
    <MemberData(NameOf(ToChar_Object_TestData))>
    <MemberData(NameOf(ToChar_String_TestData))>
    Public Sub ToChar_Object_ReturnsExpected(value1 As IConvertible, expected As Char)
      AssertEqual(expected, Conversions.ToChar(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToChar(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToChar_InvalidObject_TestData() As IEnumerable(Of Object())
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
    <Theory>
    <MemberData(NameOf(ToChar_InvalidObject_TestData))>
    Public Sub ToChar_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToChar(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToChar_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToChar(value1))
    End Sub
    Public Shared Iterator Function ToString_IConvertible_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {Byte.MinValue, "0"}
      Yield New Object() {CByte(1), "1"}
      Yield New Object() {Byte.MaxValue, "255"}
      Yield New Object() {CType(Byte.MinValue, ByteEnum), "0"}
      Yield New Object() {CType(1, ByteEnum), "1"}
      Yield New Object() {CType(Byte.MaxValue, ByteEnum), "255"}

      ' sbyte.
      Yield New Object() {SByte.MinValue, "-128"}
      Yield New Object() {CSByte((-1)), "-1"}
      Yield New Object() {CSByte(0), "0"}
      Yield New Object() {CSByte(1), "1"}
      Yield New Object() {SByte.MaxValue, "127"}
      Yield New Object() {CType(SByte.MinValue, SByteEnum), "-128"}
      Yield New Object() {CType((-1), SByteEnum), "-1"}
      Yield New Object() {CType(0, SByteEnum), "0"}
      Yield New Object() {CType(1, SByteEnum), "1"}
      Yield New Object() {CType(SByte.MaxValue, SByteEnum), "127"}

      ' ushort.
      Yield New Object() {UShort.MinValue, "0"}
      Yield New Object() {CUShort(1), "1"}
      Yield New Object() {UShort.MaxValue, "65535"}
      Yield New Object() {CType(UShort.MinValue, UShortEnum), "0"}
      Yield New Object() {CType(1, UShortEnum), "1"}
      Yield New Object() {CType(UShort.MaxValue, UShortEnum), "65535"}

      ' short.
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

      ' uint.
      Yield New Object() {UInteger.MinValue, "0"}
      Yield New Object() {CUInt(1), "1"}
      Yield New Object() {UInteger.MaxValue, "4294967295"}
      Yield New Object() {CType(UInteger.MinValue, UIntEnum), "0"}
      Yield New Object() {CType(1, UIntEnum), "1"}
      Yield New Object() {CType(UInteger.MaxValue, UIntEnum), "4294967295"}

      ' int.
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

      ' ulong.
      Yield New Object() {ULong.MinValue, "0"}
      Yield New Object() {CULng(1), "1"}
      Yield New Object() {ULong.MaxValue, "18446744073709551615"}
      Yield New Object() {CType(ULong.MinValue, ULongEnum), "0"}
      Yield New Object() {CType(1, ULongEnum), "1"}
      Yield New Object() {CType(ULong.MaxValue, ULongEnum), "18446744073709551615"}

      ' long.
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

      ' float.
      Yield New Object() {CSng((-1)), "-1"}
      Yield New Object() {CSng(0), "0"}
      Yield New Object() {CSng(1), "1"}
      Yield New Object() {Single.PositiveInfinity, Single.PositiveInfinity.ToString()}
      Yield New Object() {Single.NegativeInfinity, Single.NegativeInfinity.ToString()}
      Yield New Object() {Single.NaN, "NaN"}

      ' double.
      Yield New Object() {CDbl((-1)), "-1"}
      Yield New Object() {CDbl(0), "0"}
      Yield New Object() {CDbl(1), "1"}
      Yield New Object() {Double.PositiveInfinity, Double.PositiveInfinity.ToString()}
      Yield New Object() {Double.NegativeInfinity, Double.NegativeInfinity.ToString()}
      Yield New Object() {Double.NaN, "NaN"}

      ' decimal.
      Yield New Object() {Decimal.MinValue, Decimal.MinValue.ToString()}
      Yield New Object() {CDec((-1)), "-1"}
      Yield New Object() {CDec(0), "0"}
      Yield New Object() {CDec(1), "1"}
      Yield New Object() {Decimal.MaxValue, Decimal.MaxValue.ToString()}

      ' bool.
      Yield New Object() {True, "True"}
      Yield New Object() {False, "False"}
      If ReflectionEmitSupported Then
        Yield New Object() {BoolEnum, "False"}
      End If

      ' string.
      Yield New Object() {"", ""}
      Yield New Object() {"abc", "abc"}

      ' null.
      Yield New Object() {Nothing, CStr(Nothing)}

      ' char.
      Yield New Object() {Char.MinValue, vbNullChar}
      Yield New Object() {ChrW(1), ChrW(1)}
      Yield New Object() {"a"c, "a"}
      Yield New Object() {Char.MaxValue, Char.MaxValue.ToString()}

      ' DateTime.
      Yield New Object() {New DateTime(10), New DateTime(10).ToString("T", Nothing)}
    End Function
    <Theory>
    <MemberData(NameOf(ToString_IConvertible_TestData))>
    Public Sub ToString_IConvertible_ReturnsExpected(value1 As IConvertible, expected As String)
      AssertEqual(expected, Conversions.ToString(value1))
      If value1 IsNot Nothing Then
        AssertEqual(expected, Conversions.ToString(New ConvertibleWrapper(value1)))
      End If
    End Sub
    Public Shared Iterator Function ToString_Object_TestData() As IEnumerable(Of Object())
      ' char[]
      Yield New Object() {New Char() {}, ""}
      Yield New Object() {New Char() {ChrW(0)}, vbNullChar}
      Yield New Object() {New Char() {"A"c, "B"c}, "AB"}
    End Function
    <Theory>
    <MemberData(NameOf(ToString_Object_TestData))>
    Public Sub ToString_Object_ReturnsExpected(value1 As Object, expected As String)
      AssertEqual(expected, Conversions.ToString(value1))
    End Sub
    Public Shared Iterator Function ToString_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Object}
    End Function
    <Theory>
    <MemberData(NameOf(ToString_InvalidObject_TestData))>
    Public Sub ToString_InvalidObject_ThrowsInvalidCastException(value1 As Object)
      Assert.Throws(Of InvalidCastException)(Function() Conversions.ToString(value1))
    End Sub
    <Theory>
    <MemberData(NameOf(InvalidBool_TestData))>
    Public Sub ToString_InvalidBool_ThrowsInvalidOperationException(value1 As Object)
      Assert.Throws(Of InvalidOperationException)(Function() Conversions.ToString(value1))
    End Sub
    Private Shared s_floatEnum As Object
    Public Shared ReadOnly Property FloatEnum As Object
      Get
        If s_floatEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("CharEnumType", TypeAttributes.[Public], GetType(Single))
          eb.DefineLiteral("A", 1.0F)
          eb.DefineLiteral("B", 2.0F)
          eb.DefineLiteral("C", 3.0F)
          s_floatEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_floatEnum
      End Get
    End Property
    Private Shared s_doubleEnum As Object
    Public Shared ReadOnly Property DoubleEnum As Object
      Get
        If s_doubleEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("CharEnumType", TypeAttributes.[Public], GetType(Double))
          eb.DefineLiteral("A", 1.0)
          eb.DefineLiteral("B", 2.0)
          eb.DefineLiteral("C", 3.0)
          s_doubleEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_doubleEnum
      End Get
    End Property
    Private Shared s_boolEnum As Object
    Public Shared ReadOnly Property BoolEnum As Object
      Get
        If s_boolEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("BoolEnumType", TypeAttributes.[Public], GetType(Boolean))
          eb.DefineLiteral("False", False)
          eb.DefineLiteral("True", True)
          s_boolEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_boolEnum
      End Get
    End Property
    Private Shared s_charEnum As Object
    Public Shared ReadOnly Property CharEnum As Object
      Get
        If s_charEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("CharEnumType", TypeAttributes.[Public], GetType(Char))
          eb.DefineLiteral("A", "A"c)
          eb.DefineLiteral("B", "B"c)
          eb.DefineLiteral("C", "C"c)
          s_charEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_charEnum
      End Get
    End Property
    Private Shared s_intPtrEnum As Object
    Public Shared ReadOnly Property IntPtrEnum As Object
      Get
        If s_intPtrEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("CharEnumType", TypeAttributes.[Public], GetType(IntPtr))
          s_intPtrEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_intPtrEnum
      End Get
    End Property
    Private Shared s_uintPtrEnum As Object
    Public Shared ReadOnly Property UIntPtrEnum As Object
      Get
        If s_uintPtrEnum Is Nothing Then
          Dim assembly As AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(New AssemblyName("Name"), AssemblyBuilderAccess.RunAndCollect)
          Dim [module] As ModuleBuilder = assembly.DefineDynamicModule("Name")
          Dim eb As EnumBuilder = [module].DefineEnum("CharEnumType", TypeAttributes.[Public], GetType(UIntPtr))
          s_uintPtrEnum = Activator.CreateInstance(eb.CreateTypeInfo())
        End If

        Return s_uintPtrEnum
      End Get
    End Property
    Private Shared Sub AssertEqual(expected As Object, actual As Object)
      Dim TempVar As Boolean = TypeOf expected Is Single
      Dim expectedFloat As Single
      Single.TryParse(expected.ToString, expectedFloat)
      Dim TempVar1 As Boolean = TypeOf actual Is Single
      Dim actualFloat As Single
      Single.TryParse(actual.ToString, actualFloat)
      Dim TempVar2 As Boolean = TypeOf expected Is Double
      Dim expectedDouble As Double
      Double.TryParse(expected.ToString, expectedDouble)
      Dim TempVar3 As Boolean = TypeOf actual Is Double
      Dim actualDouble As Double
      Double.TryParse(actual.ToString, actualDouble)

      If TempVar2 AndAlso TempVar3 Then
        Assert.Equal(expected.ToString(), actual.ToString())
      ElseIf TempVar AndAlso TempVar1 Then
        Assert.Equal(expected.ToString(), actual.ToString())
      Else
        Assert.Equal(expected, actual)
      End If
    End Sub
  End Class


  Public Enum ByteEnum As Byte
    Value = 1
  End Enum

  Public Enum SByteEnum As SByte
    Value = 1
  End Enum

  Public Enum UShortEnum As UShort
    Value = 1
  End Enum

  Public Enum ShortEnum As Short
    Value = 1
  End Enum

  Public Enum UIntEnum As UInteger
    Value = 1
  End Enum

  Public Enum IntEnum As Integer
    Value = 1
  End Enum

  Public Enum ULongEnum As ULong
    Value = 1
  End Enum

  Public Enum LongEnum As [Int64]
    Value = 1
  End Enum

End Namespace