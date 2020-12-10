' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Community.VisualBasic.CompilerServices
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Partial Public Class OperatorsTests

    Public Shared Iterator Function AddObject_Idempotent_TestData() As IEnumerable(Of Object())

      ' byte + primitives.
      Yield New Object() {CByte(1), CByte(2), CByte(3)}
      Yield New Object() {CByte(2), CSByte(2), CShort(Fix(4))}
      Yield New Object() {CByte(3), CUShort(2), CUShort(5)}
      Yield New Object() {CByte(4), CShort(Fix(2)), CShort(Fix(6))}
      Yield New Object() {CByte(5), CUInt(2), CUInt(7)}
      Yield New Object() {CByte(6), 2, 8}
      Yield New Object() {CByte(7), CLng(Fix(2)), CLng(Fix(9))}
      Yield New Object() {CByte(8), CULng(2), CULng(10)}
      Yield New Object() {CByte(9), CSng(2), CSng(11)}
      Yield New Object() {CByte(10), CDbl(2), CDbl(12)}
      Yield New Object() {CByte(11), CDec(2), CDec(13)}
      Yield New Object() {CByte(12), "2", CDbl(14)}
      Yield New Object() {CByte(13), True, CShort(Fix(12))}
      Yield New Object() {CByte(14), Nothing, CByte(14)}
      Yield New Object() {CByte(15), Byte.MaxValue, CShort(Fix(270))}

      ' sbyte + primitives.
      Yield New Object() {CSByte(1), CSByte(2), CSByte(3)}
      Yield New Object() {CSByte(3), CUShort(2), 5}
      Yield New Object() {CSByte(4), CShort(Fix(2)), CShort(Fix(6))}
      Yield New Object() {CSByte(5), CUInt(2), CLng(Fix(7))}
      Yield New Object() {CSByte(6), 2, 8}
      Yield New Object() {CSByte(7), CLng(Fix(2)), CLng(Fix(9))}
      Yield New Object() {CSByte(9), CSng(2), CSng(11)}
      Yield New Object() {CSByte(8), CULng(2), CDec(10)}
      Yield New Object() {CSByte(10), CDbl(2), CDbl(12)}
      Yield New Object() {CSByte(11), CDec(2), CDec(13)}
      Yield New Object() {CSByte(12), "2", CDbl(14)}
      Yield New Object() {CSByte(13), True, CSByte(12)}
      Yield New Object() {CSByte(14), Nothing, CSByte(14)}
      Yield New Object() {CSByte(15), SByte.MaxValue, CShort(Fix(142))}

      ' ushort + primitives.
      Yield New Object() {CUShort(3), CUShort(2), CUShort(5)}
      Yield New Object() {CUShort(4), CShort(Fix(2)), 6}
      Yield New Object() {CUShort(5), CUInt(2), CUInt(7)}
      Yield New Object() {CUShort(6), 2, 8}
      Yield New Object() {CUShort(7), CLng(Fix(2)), CLng(Fix(9))}
      Yield New Object() {CUShort(8), CULng(2), CULng(10)}
      Yield New Object() {CUShort(9), CSng(2), CSng(11)}
      Yield New Object() {CUShort(10), CDbl(2), CDbl(12)}
      Yield New Object() {CUShort(11), CDec(2), CDec(13)}
      Yield New Object() {CUShort(12), "2", CDbl(14)}
      Yield New Object() {CUShort(13), True, 12}
      Yield New Object() {CUShort(14), Nothing, CUShort(14)}
      Yield New Object() {CUShort(15), UShort.MaxValue, 65550}

      ' short + primitives.
      Yield New Object() {CShort(Fix(4)), CShort(Fix(2)), CShort(Fix(6))}
      Yield New Object() {CShort(Fix(5)), CUInt(2), CLng(Fix(7))}
      Yield New Object() {CShort(Fix(6)), 2, 8}
      Yield New Object() {CShort(Fix(7)), CLng(Fix(2)), CLng(Fix(9))}
      Yield New Object() {CShort(Fix(8)), CULng(2), CDec(10)}
      Yield New Object() {CShort(Fix(9)), CSng(2), CSng(11)}
      Yield New Object() {CShort(Fix(10)), CDbl(2), CDbl(12)}
      Yield New Object() {CShort(Fix(11)), CDec(2), CDec(13)}
      Yield New Object() {CShort(Fix(12)), "2", CDbl(14)}
      Yield New Object() {CShort(Fix(13)), True, CShort(Fix(12))}
      Yield New Object() {CShort(Fix(14)), Nothing, CShort(Fix(14))}
      Yield New Object() {CShort(Fix(15)), Short.MaxValue, 32782}

      ' uint + primitives.
      Yield New Object() {CUInt(4), CShort(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CUInt(5), CUInt(2), CUInt(7)}
      Yield New Object() {CUInt(6), 2, CLng(Fix(8))}
      Yield New Object() {CUInt(7), CULng(2), CULng(9)}
      Yield New Object() {CUInt(8), CLng(Fix(2)), CLng(Fix(10))}
      Yield New Object() {CUInt(9), CSng(2), CSng(11)}
      Yield New Object() {CUInt(10), CDbl(2), CDbl(12)}
      Yield New Object() {CUInt(11), CDec(2), CDec(13)}
      Yield New Object() {CUInt(12), "2", CDbl(14)}
      Yield New Object() {CUInt(13), True, CLng(Fix(12))}
      Yield New Object() {CUInt(14), Nothing, CUInt(14)}
      Yield New Object() {CUInt(15), UInteger.MaxValue, 4294967310}

      ' int + primitives.
      Yield New Object() {6, 2, 8}
      Yield New Object() {7, CULng(2), CDec(9)}
      Yield New Object() {8, CLng(Fix(2)), CLng(Fix(10))}
      Yield New Object() {9, CSng(2), CSng(11)}
      Yield New Object() {10, CDbl(2), CDbl(12)}
      Yield New Object() {11, CDec(2), CDec(13)}
      Yield New Object() {12, "2", CDbl(14)}
      Yield New Object() {13, True, 12}
      Yield New Object() {14, Nothing, 14}
      Yield New Object() {15, Integer.MaxValue, CLng(Fix(2147483662))}

      ' ulong + primitives.
      Yield New Object() {CULng(7), CULng(2), CULng(9)}
      Yield New Object() {CULng(8), CLng(Fix(2)), CDec(10)}
      Yield New Object() {CULng(9), CSng(2), CSng(11)}
      Yield New Object() {CULng(10), CDbl(2), CDbl(12)}
      Yield New Object() {CULng(11), CDec(2), CDec(13)}
      Yield New Object() {CULng(12), "2", CDbl(14)}
      Yield New Object() {CULng(13), True, CDec(12)}
      Yield New Object() {CULng(14), Nothing, CULng(14)}
      Yield New Object() {CULng(15), ULong.MaxValue, Decimal.Parse("18446744073709551630", CultureInfo.InvariantCulture)}

      ' long + primitives.
      Yield New Object() {CLng(Fix(8)), CLng(Fix(2)), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(9)), CSng(2), CSng(11)}
      Yield New Object() {CLng(Fix(10)), CDbl(2), CDbl(12)}
      Yield New Object() {CLng(Fix(11)), CDec(2), CDec(13)}
      Yield New Object() {CLng(Fix(12)), "2", CDbl(14)}
      Yield New Object() {CLng(Fix(13)), True, CLng(Fix(12))}
      Yield New Object() {CLng(Fix(14)), Nothing, CLng(Fix(14))}
      Yield New Object() {CLng(Fix(15)), Long.MaxValue, Decimal.Parse("9223372036854775822", CultureInfo.InvariantCulture)}

      ' float + primitives
      Yield New Object() {CSng(9), CSng(2), CSng(11)}
      Yield New Object() {CSng(10), CDbl(2), CDbl(12)}
      Yield New Object() {CSng(11), CDec(2), CSng(13)}
      Yield New Object() {CSng(12), "2", CDbl(14)}
      Yield New Object() {CSng(13), True, CSng(12)}
      Yield New Object() {CSng(14), Nothing, CSng(14)}
      Yield New Object() {CSng(15), Single.PositiveInfinity, Single.PositiveInfinity}
      Yield New Object() {CSng(15), Single.NegativeInfinity, Single.NegativeInfinity}
      Yield New Object() {CSng(15), Single.NaN, Double.NaN}

      ' double + primitives
      Yield New Object() {CDbl(10), CDbl(2), CDbl(12)}
      Yield New Object() {CDbl(11), CDec(2), CDbl(13)}
      Yield New Object() {CDbl(12), "2", CDbl(14)}
      Yield New Object() {CDbl(13), True, CDbl(12)}
      Yield New Object() {CDbl(14), Nothing, CDbl(14)}
      Yield New Object() {CDbl(15), Double.PositiveInfinity, Double.PositiveInfinity}
      Yield New Object() {CDbl(15), Double.NegativeInfinity, Double.NegativeInfinity}
      Yield New Object() {CDbl(15), Double.NaN, Double.NaN}

      ' decimal + primitives
      Yield New Object() {CDec(11), CDec(2), CDec(13)}
      Yield New Object() {CDec(12), "2", CDbl(14)}
      Yield New Object() {CDec(13), True, CDec(12)}
      Yield New Object() {CDec(14), Nothing, CDec(14)}

      ' string + primitives
      Yield New Object() {"1", "2", "12"}
      Yield New Object() {"2", "2"c, "22"}
      Yield New Object() {"2", New Char() {"2"c}, "22"}
      Yield New Object() {"3", True, CDbl(2)}
      Yield New Object() {"5", DBNull.Value, "5"}
      Yield New Object() {"5", Nothing, "5"}

      ' chars + primitives.
      Yield New Object() {New Char() {"1"c}, "2", "12"}
      Yield New Object() {New Char() {"2"c}, New Char() {"2"c}, "22"}
      Yield New Object() {New Char() {"5"c}, Nothing, "5"}

      ' bool + primitives
      Yield New Object() {True, "2", CDbl(1)}
      Yield New Object() {True, True, CShort(Fix(-2))}
      Yield New Object() {True, False, CShort(Fix(-1))}
      Yield New Object() {True, Nothing, CShort(Fix(-1))}

      ' char + primitives
      Yield New Object() {"a"c, Nothing, "a" & vbNullChar}
      Yield New Object() {"a"c, "b"c, "ab"}

      ' DBNull.
      Yield New Object() {DBNull.Value, "1", "1"}

      ' null + null
      Yield New Object() {Nothing, Nothing, 0}

      ' object + object
      Yield New Object() {New AddObject, 2, "custom"}
      Yield New Object() {New AddObject, New OperatorsTests, "customobject"}
    End Function

    <Theory>
    <MemberData(NameOf(AddObject_Idempotent_TestData))>
    Public Sub AddObject_Convertible_ReturnsExpected(left As Object, right As Object, expected As Object)

      Assert.Equal(expected, Operators.AddObject(left, right))

      Dim isString As Boolean = TypeOf expected Is String

      If isString Then
        Dim expectedString As String = expected.ToString
        Dim reversed As New String(expectedString.Reverse().ToArray())
        Assert.Equal(reversed, Operators.AddObject(right, left))
      Else
        Assert.Equal(expected, Operators.AddObject(right, left))
      End If

    End Sub

    <Fact>
    Public Sub AddObject_DateString_ReturnsExpected()
      Dim expected As String = Assert.IsType(Of String)(Operators.AddObject("String", New DateTime(2017, 10, 10)))
      Assert.StartsWith("String", expected)
      Assert.Contains("17", expected)
    End Sub

    <Fact>
    Public Sub AddObject_DateDate_ReturnsExpected()
      Dim expected As String = Assert.IsType(Of String)(Operators.AddObject(New DateTime(2018, 10, 10), New DateTime(2017, 10, 10)))
      Assert.Contains("17", expected)
      Assert.Contains("18", expected)
    End Sub

    <Fact>
    Public Sub AddObject_DateNull_ReturnsExpected()
      Dim expected As String = Assert.IsType(Of String)(Operators.AddObject(New DateTime(2018, 10, 10), Nothing))
      Assert.Contains("18", expected)
    End Sub

    <Fact>
    Public Sub AddObject_NullDate_ReturnsExpected()
      Dim expected As String = Assert.IsType(Of String)(Operators.AddObject(Nothing, New DateTime(2018, 10, 10)))
      Assert.Contains("18", expected)
    End Sub

    <Fact>
    Public Sub AddObject_StringDate_ReturnsExpected()
      Dim expected As String = Assert.IsType(Of String)(Operators.AddObject(New DateTime(2017, 10, 10), "String"))
      Assert.EndsWith("String", expected)
      Assert.Contains("17", expected)
    End Sub

    <Fact>
    Public Sub AddObject_FloatDoubleDecimalOverflow_ReturnsMax()
      Assert.Equal(Single.MaxValue, Operators.AddObject(CSng(15), Single.MaxValue))
      Assert.Equal(Double.MaxValue, Operators.AddObject(CDbl(15), Double.MaxValue))
      Assert.NotEqual(Decimal.MaxValue, Operators.AddObject(CDec(15), Decimal.MaxValue))
    End Sub

    Public Shared Iterator Function AddObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {"2"c, 1}
      Yield New Object() {"3"c, New Object}
      Yield New Object() {New Object, "3"c}

      Yield New Object() {2, DBNull.Value}
      Yield New Object() {DBNull.Value, 2}
      Yield New Object() {Nothing, DBNull.Value}
      Yield New Object() {DBNull.Value, Nothing}
      Yield New Object() {DBNull.Value, DBNull.Value}

      Yield New Object() {New Char() {"8"c}, 10}
      Yield New Object() {10, New Char() {"8"c}}
      Yield New Object() {New Char() {"8"c}, DBNull.Value}
      Yield New Object() {DBNull.Value, New Char() {"8"c}}
      Yield New Object() {New Char() {"8"c}, New Object}
      Yield New Object() {New Object, New Char() {"8"c}}
    End Function

    <Theory>
    <MemberData(NameOf(AddObject_InvalidObjects_TestData))>
    Public Sub AddObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.AddObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.AddObject(right, left))
    End Sub

    Public Shared Iterator Function AddObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New AddObject, New Object}
      Yield New Object() {New Object, New AddObject}

      Yield New Object() {New AddObject, New AddObject}
    End Function

    <Theory>
    <MemberData(NameOf(AddObject_MismatchingObjects_TestData))>
    Public Sub AddObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.AddObject(left, right))
    End Sub

    Public Class AddObject

      Public Shared Operator +(left As AddObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator +(left As Integer, right As AddObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator +(left As AddObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator +(left As OperatorsTests, right As AddObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function AndObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(14), CByte(10)}
      Yield New Object() {CByte(10), CType(14, ByteEnum), CByte(10)}
      Yield New Object() {CByte(10), CSByte(14), CShort(Fix(10))}
      Yield New Object() {CByte(10), CType(14, SByteEnum), CShort(Fix(10))}
      Yield New Object() {CByte(10), CUShort(14), CUShort(10)}
      Yield New Object() {CByte(10), CType(14, UShortEnum), CUShort(10)}
      Yield New Object() {CByte(10), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CByte(10), CType(14, ShortEnum), CShort(Fix(10))}
      Yield New Object() {CByte(10), CUInt(14), CUInt(10)}
      Yield New Object() {CByte(10), CType(14, UIntEnum), CUInt(10)}
      Yield New Object() {CByte(10), 14, 10}
      Yield New Object() {CByte(10), CType(14, IntEnum), 10}
      Yield New Object() {CByte(10), CULng(14), CULng(10)}
      Yield New Object() {CByte(10), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CByte(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CByte(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CByte(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CByte(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CByte(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CByte(10), "14", CLng(Fix(10))}
      Yield New Object() {CByte(10), True, CShort(Fix(10))}
      Yield New Object() {CByte(10), Nothing, CByte(0)}

      Yield New Object() {CType(10, ByteEnum), CByte(14), CByte(10)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ByteEnum), CType(10, ByteEnum)}
      Yield New Object() {CType(10, ByteEnum), CSByte(14), CShort(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CType(14, SByteEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CUShort(14), CUShort(10)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UShortEnum), CUShort(10)}
      Yield New Object() {CType(10, ByteEnum), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CType(14, ShortEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CUInt(14), CUInt(10)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UIntEnum), CUInt(10)}
      Yield New Object() {CType(10, ByteEnum), 14, 10}
      Yield New Object() {CType(10, ByteEnum), CType(14, IntEnum), 10}
      Yield New Object() {CType(10, ByteEnum), CULng(14), CULng(10)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CType(10, ByteEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), True, CShort(Fix(10))}
      Yield New Object() {CType(10, ByteEnum), Nothing, CType(0, ByteEnum)}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(14), CShort(Fix(10))}
      Yield New Object() {CSByte(10), CType(14, ByteEnum), CShort(Fix(10))}
      Yield New Object() {CSByte(10), CSByte(14), CSByte(10)}
      Yield New Object() {CSByte(10), CType(14, SByteEnum), CSByte(10)}
      Yield New Object() {CSByte(10), CUShort(14), 10}
      Yield New Object() {CSByte(10), CType(14, UShortEnum), 10}
      Yield New Object() {CSByte(10), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CSByte(10), CType(14, ShortEnum), CShort(Fix(10))}
      Yield New Object() {CSByte(10), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CSByte(10), 14, 10}
      Yield New Object() {CSByte(10), CType(14, IntEnum), 10}
      Yield New Object() {CSByte(10), CULng(14), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CSByte(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CSByte(10), "14", CLng(Fix(10))}
      Yield New Object() {CSByte(10), True, CSByte(10)}
      Yield New Object() {CSByte(10), Nothing, CSByte(0)}

      Yield New Object() {CType(10, SByteEnum), CByte(14), CShort(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ByteEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CSByte(14), CSByte(10)}
      Yield New Object() {CType(10, SByteEnum), CType(14, SByteEnum), CType(10, SByteEnum)}
      Yield New Object() {CType(10, SByteEnum), CUShort(14), 10}
      Yield New Object() {CType(10, SByteEnum), CType(14, UShortEnum), 10}
      Yield New Object() {CType(10, SByteEnum), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ShortEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), 14, 10}
      Yield New Object() {CType(10, SByteEnum), CType(14, IntEnum), 10}
      Yield New Object() {CType(10, SByteEnum), CULng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, SByteEnum), True, CSByte(10)}
      Yield New Object() {CType(10, SByteEnum), Nothing, CType(0, SByteEnum)}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(14), CUShort(10)}
      Yield New Object() {CUShort(10), CType(14, ByteEnum), CUShort(10)}
      Yield New Object() {CUShort(10), CSByte(14), 10}
      Yield New Object() {CUShort(10), CType(14, SByteEnum), 10}
      Yield New Object() {CUShort(10), CUShort(14), CUShort(10)}
      Yield New Object() {CUShort(10), CType(14, UShortEnum), CUShort(10)}
      Yield New Object() {CUShort(10), CShort(Fix(14)), 10}
      Yield New Object() {CUShort(10), CType(14, ShortEnum), 10}
      Yield New Object() {CUShort(10), CUInt(14), CUInt(10)}
      Yield New Object() {CUShort(10), CType(14, UIntEnum), CUInt(10)}
      Yield New Object() {CUShort(10), 14, 10}
      Yield New Object() {CUShort(10), CType(14, IntEnum), 10}
      Yield New Object() {CUShort(10), CULng(14), CULng(10)}
      Yield New Object() {CUShort(10), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CUShort(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CUShort(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CUShort(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CUShort(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CUShort(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CUShort(10), "14", CLng(Fix(10))}
      Yield New Object() {CUShort(10), True, 10}
      Yield New Object() {CUShort(10), Nothing, CUShort(0)}

      Yield New Object() {CType(10, UShortEnum), CByte(14), CUShort(10)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ByteEnum), CUShort(10)}
      Yield New Object() {CType(10, UShortEnum), CSByte(14), 10}
      Yield New Object() {CType(10, UShortEnum), CType(14, SByteEnum), 10}
      Yield New Object() {CType(10, UShortEnum), CUShort(14), CUShort(10)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UShortEnum), CType(10, UShortEnum)}
      Yield New Object() {CType(10, UShortEnum), CShort(Fix(14)), 10}
      Yield New Object() {CType(10, UShortEnum), CType(14, ShortEnum), 10}
      Yield New Object() {CType(10, UShortEnum), CUInt(14), CUInt(10)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UIntEnum), CUInt(10)}
      Yield New Object() {CType(10, UShortEnum), 14, 10}
      Yield New Object() {CType(10, UShortEnum), CType(14, IntEnum), 10}
      Yield New Object() {CType(10, UShortEnum), CULng(14), CULng(10)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CType(10, UShortEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, UShortEnum), True, 10}
      Yield New Object() {CType(10, UShortEnum), Nothing, CType(0, UShortEnum)}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(14), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, ByteEnum), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CSByte(14), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, SByteEnum), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CUShort(14), 10}
      Yield New Object() {CShort(Fix(10)), CType(14, UShortEnum), 10}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, ShortEnum), CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), 14, 10}
      Yield New Object() {CShort(Fix(10)), CType(14, IntEnum), 10}
      Yield New Object() {CShort(Fix(10)), CULng(14), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CSng(14), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), CDec(14), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), "14", CLng(Fix(10))}
      Yield New Object() {CShort(Fix(10)), True, CShort(Fix(10))}
      Yield New Object() {CShort(Fix(10)), Nothing, CShort(Fix(0))}

      Yield New Object() {CType(10, ShortEnum), CByte(14), CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ByteEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CSByte(14), CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, SByteEnum), CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CUShort(14), 10}
      Yield New Object() {CType(10, ShortEnum), CType(14, UShortEnum), 10}
      Yield New Object() {CType(10, ShortEnum), CShort(Fix(14)), CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ShortEnum), CType(10, ShortEnum)}
      Yield New Object() {CType(10, ShortEnum), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), 14, 10}
      Yield New Object() {CType(10, ShortEnum), CType(14, IntEnum), 10}
      Yield New Object() {CType(10, ShortEnum), CULng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), True, CShort(Fix(10))}
      Yield New Object() {CType(10, ShortEnum), Nothing, CType(0, ShortEnum)}

      ' uint.
      Yield New Object() {CUInt(10), CByte(14), CUInt(10)}
      Yield New Object() {CUInt(10), CType(14, ByteEnum), CUInt(10)}
      Yield New Object() {CUInt(10), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CUShort(14), CUInt(10)}
      Yield New Object() {CUInt(10), CType(14, UShortEnum), CUInt(10)}
      Yield New Object() {CUInt(10), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CUInt(14), CUInt(10)}
      Yield New Object() {CUInt(10), CType(14, UIntEnum), CUInt(10)}
      Yield New Object() {CUInt(10), 14, CLng(Fix(10))}
      Yield New Object() {CUInt(10), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CULng(14), CULng(10)}
      Yield New Object() {CUInt(10), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CUInt(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CUInt(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CUInt(10), "14", CLng(Fix(10))}
      Yield New Object() {CUInt(10), True, CLng(Fix(10))}
      Yield New Object() {CUInt(10), Nothing, CUInt(0)}

      Yield New Object() {CType(10, UIntEnum), CByte(14), CUInt(10)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ByteEnum), CUInt(10)}
      Yield New Object() {CType(10, UIntEnum), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CUShort(14), CUInt(10)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UShortEnum), CUInt(10)}
      Yield New Object() {CType(10, UIntEnum), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CUInt(14), CUInt(10)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UIntEnum), CType(10, UIntEnum)}
      Yield New Object() {CType(10, UIntEnum), 14, CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CULng(14), CULng(10)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CType(10, UIntEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), True, CLng(Fix(10))}
      Yield New Object() {CType(10, UIntEnum), Nothing, CType(0, UIntEnum)}

      ' int.
      Yield New Object() {10, CByte(14), 10}
      Yield New Object() {10, CType(14, ByteEnum), 10}
      Yield New Object() {10, CSByte(14), 10}
      Yield New Object() {10, CType(14, SByteEnum), 10}
      Yield New Object() {10, CUShort(14), 10}
      Yield New Object() {10, CType(14, UShortEnum), 10}
      Yield New Object() {10, CShort(Fix(14)), 10}
      Yield New Object() {10, CType(14, ShortEnum), 10}
      Yield New Object() {10, CUInt(14), CLng(Fix(10))}
      Yield New Object() {10, CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {10, 14, 10}
      Yield New Object() {10, CType(14, IntEnum), 10}
      Yield New Object() {10, CULng(14), CLng(Fix(10))}
      Yield New Object() {10, CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {10, CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {10, CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {10, CSng(14), CLng(Fix(10))}
      Yield New Object() {10, CDbl(14), CLng(Fix(10))}
      Yield New Object() {10, CDec(14), CLng(Fix(10))}
      Yield New Object() {10, "14", CLng(Fix(10))}
      Yield New Object() {10, True, 10}
      Yield New Object() {10, Nothing, 0}

      Yield New Object() {CType(10, IntEnum), CByte(14), 10}
      Yield New Object() {CType(10, IntEnum), CType(14, ByteEnum), 10}
      Yield New Object() {CType(10, IntEnum), CSByte(14), 10}
      Yield New Object() {CType(10, IntEnum), CType(14, SByteEnum), 10}
      Yield New Object() {CType(10, IntEnum), CUShort(14), 10}
      Yield New Object() {CType(10, IntEnum), CType(14, UShortEnum), 10}
      Yield New Object() {CType(10, IntEnum), CShort(Fix(14)), 10}
      Yield New Object() {CType(10, IntEnum), CType(14, ShortEnum), 10}
      Yield New Object() {CType(10, IntEnum), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), 14, 10}
      Yield New Object() {CType(10, IntEnum), CType(14, IntEnum), CType(10, IntEnum)}
      Yield New Object() {CType(10, IntEnum), CULng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, IntEnum), True, 10}
      Yield New Object() {CType(10, IntEnum), Nothing, CType(0, IntEnum)}

      ' ulong.
      Yield New Object() {CULng(10), CByte(14), CULng(10)}
      Yield New Object() {CULng(10), CType(14, ByteEnum), CULng(10)}
      Yield New Object() {CULng(10), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CULng(10), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CULng(10), CUShort(14), CULng(10)}
      Yield New Object() {CULng(10), CType(14, UShortEnum), CULng(10)}
      Yield New Object() {CULng(10), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CULng(10), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CULng(10), CUInt(14), CULng(10)}
      Yield New Object() {CULng(10), CType(14, UIntEnum), CULng(10)}
      Yield New Object() {CULng(10), 14, CLng(Fix(10))}
      Yield New Object() {CULng(10), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CULng(10), CULng(14), CULng(10)}
      Yield New Object() {CULng(10), CType(14, ULongEnum), CULng(10)}
      Yield New Object() {CULng(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CULng(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CULng(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CULng(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CULng(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CULng(10), "14", CLng(Fix(10))}
      Yield New Object() {CULng(10), True, CLng(Fix(10))}
      Yield New Object() {CULng(10), Nothing, CULng(0)}

      Yield New Object() {CType(10, ULongEnum), CByte(14), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ByteEnum), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CUShort(14), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UShortEnum), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CUInt(14), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UIntEnum), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), 14, CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CULng(14), CULng(10)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ULongEnum), CType(10, ULongEnum)}
      Yield New Object() {CType(10, ULongEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), True, CLng(Fix(10))}
      Yield New Object() {CType(10, ULongEnum), Nothing, CType(0, ULongEnum)}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CUShort(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), 14, CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CULng(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CSng(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), CDec(14), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), "14", CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), True, CLng(Fix(10))}
      Yield New Object() {CLng(Fix(10)), Nothing, CLng(Fix(0))}

      Yield New Object() {CType(10, LongEnum), CByte(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CUShort(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), 14, CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CULng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CType(14, LongEnum), CType(10, LongEnum)}
      Yield New Object() {CType(10, LongEnum), CSng(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), CDec(14), CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), "14", CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), True, CLng(Fix(10))}
      Yield New Object() {CType(10, LongEnum), Nothing, CType(0, LongEnum)}

      ' float.
      Yield New Object() {CSng(10), CByte(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CUShort(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), 14, CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CULng(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CSng(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CSng(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CSng(10), "14", CLng(Fix(10))}
      Yield New Object() {CSng(10), True, CLng(Fix(10))}
      Yield New Object() {CSng(10), Nothing, CLng(Fix(0))}

      ' double.
      Yield New Object() {CDbl(10), CByte(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CUShort(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), 14, CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CULng(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CDbl(10), "14", CLng(Fix(10))}
      Yield New Object() {CDbl(10), True, CLng(Fix(10))}
      Yield New Object() {CDbl(10), Nothing, CLng(Fix(0))}

      ' decimal.
      Yield New Object() {CDec(10), CByte(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CSByte(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CUShort(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CUInt(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), 14, CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CULng(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {CDec(10), CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {CDec(10), CSng(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CDbl(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), CDec(14), CLng(Fix(10))}
      Yield New Object() {CDec(10), "14", CLng(Fix(10))}
      Yield New Object() {CDec(10), True, CLng(Fix(10))}
      Yield New Object() {CDec(10), Nothing, CLng(Fix(0))}

      ' string.
      Yield New Object() {"10", CByte(14), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, ByteEnum), CLng(Fix(10))}
      Yield New Object() {"10", CSByte(14), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, SByteEnum), CLng(Fix(10))}
      Yield New Object() {"10", CUShort(14), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, UShortEnum), CLng(Fix(10))}
      Yield New Object() {"10", CShort(Fix(14)), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, ShortEnum), CLng(Fix(10))}
      Yield New Object() {"10", CUInt(14), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, UIntEnum), CLng(Fix(10))}
      Yield New Object() {"10", 14, CLng(Fix(10))}
      Yield New Object() {"10", CType(14, IntEnum), CLng(Fix(10))}
      Yield New Object() {"10", CULng(14), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, ULongEnum), CLng(Fix(10))}
      Yield New Object() {"10", CLng(Fix(14)), CLng(Fix(10))}
      Yield New Object() {"10", CType(14, LongEnum), CLng(Fix(10))}
      Yield New Object() {"10", CSng(14), CLng(Fix(10))}
      Yield New Object() {"10", CDbl(14), CLng(Fix(10))}
      Yield New Object() {"10", CDec(14), CLng(Fix(10))}
      Yield New Object() {"10", "14", CLng(Fix(10))}
      Yield New Object() {"10", True, True}
      Yield New Object() {"10", Nothing, CLng(Fix(0))}

      ' bool.
      Yield New Object() {True, CByte(14), CShort(Fix(14))}
      Yield New Object() {True, CType(14, ByteEnum), CShort(Fix(14))}
      Yield New Object() {True, CSByte(14), CSByte(14)}
      Yield New Object() {True, CType(14, SByteEnum), CSByte(14)}
      Yield New Object() {True, CUShort(14), 14}
      Yield New Object() {True, CType(14, UShortEnum), 14}
      Yield New Object() {True, CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {True, CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {True, CUInt(14), CLng(Fix(14))}
      Yield New Object() {True, CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {True, 14, 14}
      Yield New Object() {True, CType(14, IntEnum), 14}
      Yield New Object() {True, CULng(14), CLng(Fix(14))}
      Yield New Object() {True, CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {True, CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {True, CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {True, CSng(14), CLng(Fix(14))}
      Yield New Object() {True, CDbl(14), CLng(Fix(14))}
      Yield New Object() {True, CDec(14), CLng(Fix(14))}
      Yield New Object() {True, "14", True}
      Yield New Object() {True, True, True}
      Yield New Object() {True, Nothing, False}

      ' null.
      Yield New Object() {Nothing, CByte(14), CByte(0)}
      Yield New Object() {Nothing, CType(14, ByteEnum), CType(0, ByteEnum)}
      Yield New Object() {Nothing, CSByte(14), CSByte(0)}
      Yield New Object() {Nothing, CType(14, SByteEnum), CType(0, SByteEnum)}
      Yield New Object() {Nothing, CUShort(14), CUShort(0)}
      Yield New Object() {Nothing, CType(14, UShortEnum), CType(0, UShortEnum)}
      Yield New Object() {Nothing, CShort(Fix(14)), CShort(Fix(0))}
      Yield New Object() {Nothing, CType(14, ShortEnum), CType(0, ShortEnum)}
      Yield New Object() {Nothing, CUInt(14), CUInt(0)}
      Yield New Object() {Nothing, CType(14, UIntEnum), CType(0, UIntEnum)}
      Yield New Object() {Nothing, 14, 0}
      Yield New Object() {Nothing, CType(14, IntEnum), CType(0, IntEnum)}
      Yield New Object() {Nothing, CULng(14), CULng(0)}
      Yield New Object() {Nothing, CType(14, ULongEnum), CType(0, ULongEnum)}
      Yield New Object() {Nothing, CLng(Fix(14)), CLng(Fix(0))}
      Yield New Object() {Nothing, CType(14, LongEnum), CType(0, LongEnum)}
      Yield New Object() {Nothing, CSng(14), CLng(Fix(0))}
      Yield New Object() {Nothing, CDbl(14), CLng(Fix(0))}
      Yield New Object() {Nothing, CDec(14), CLng(Fix(0))}
      Yield New Object() {Nothing, "14", CLng(Fix(0))}
      Yield New Object() {Nothing, True, False}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New AndObject, 2, "custom"}
      Yield New Object() {2, New AndObject, "motsuc"}
      Yield New Object() {New AndObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New AndObject, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(AndObject_TestData))>
    Public Sub AndObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.AndObject(left, right))
    End Sub

    Public Shared Iterator Function AndObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(AndObject_InvalidObjects_TestData))>
    Public Sub AndObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.AndObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.AndObject(right, left))
    End Sub

    Public Shared Iterator Function AndObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New AndObject, New Object}
      Yield New Object() {New Object, New AndObject}

      Yield New Object() {New AndObject, New AndObject}
    End Function

    <Theory>
    <MemberData(NameOf(AndObject_MismatchingObjects_TestData))>
    Public Sub AndObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.AndObject(left, right))
    End Sub

    Public Class AndObject

      Public Shared Operator And(left As AndObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator And(left As Integer, right As AndObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator And(left As AndObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator And(left As OperatorsTests, right As AndObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function ConcatenateObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(2), "102"}
      Yield New Object() {CByte(10), CSByte(2), "102"}
      Yield New Object() {CByte(10), CUShort(2), "102"}
      Yield New Object() {CByte(10), CShort(Fix(2)), "102"}
      Yield New Object() {CByte(10), CUInt(2), "102"}
      Yield New Object() {CByte(10), 2, "102"}
      Yield New Object() {CByte(10), CULng(2), "102"}
      Yield New Object() {CByte(10), CLng(Fix(2)), "102"}
      Yield New Object() {CByte(10), CSng(2), "102"}
      Yield New Object() {CByte(10), CDbl(2), "102"}
      Yield New Object() {CByte(10), CDec(2), "102"}
      Yield New Object() {CByte(10), "2", "102"}
      Yield New Object() {CByte(10), New Char() {"2"c}, "102"}
      Yield New Object() {CByte(10), True, "10True"}
      Yield New Object() {CByte(10), DBNull.Value, "10"}
      Yield New Object() {CByte(10), Nothing, "10"}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(2), "102"}
      Yield New Object() {CSByte(10), CSByte(2), "102"}
      Yield New Object() {CSByte(10), CUShort(2), "102"}
      Yield New Object() {CSByte(10), CShort(Fix(2)), "102"}
      Yield New Object() {CSByte(10), CUInt(2), "102"}
      Yield New Object() {CSByte(10), 2, "102"}
      Yield New Object() {CSByte(10), CULng(2), "102"}
      Yield New Object() {CSByte(10), CLng(Fix(2)), "102"}
      Yield New Object() {CSByte(10), CSng(2), "102"}
      Yield New Object() {CSByte(10), CDbl(2), "102"}
      Yield New Object() {CSByte(10), CDec(2), "102"}
      Yield New Object() {CSByte(10), "2", "102"}
      Yield New Object() {CSByte(10), New Char() {"2"c}, "102"}
      Yield New Object() {CSByte(10), True, "10True"}
      Yield New Object() {CSByte(10), DBNull.Value, "10"}
      Yield New Object() {CSByte(10), Nothing, "10"}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(2), "102"}
      Yield New Object() {CUShort(10), CSByte(2), "102"}
      Yield New Object() {CUShort(10), CUShort(2), "102"}
      Yield New Object() {CUShort(10), CShort(Fix(2)), "102"}
      Yield New Object() {CUShort(10), CUInt(2), "102"}
      Yield New Object() {CUShort(10), 2, "102"}
      Yield New Object() {CUShort(10), CULng(2), "102"}
      Yield New Object() {CUShort(10), CLng(Fix(2)), "102"}
      Yield New Object() {CUShort(10), CSng(2), "102"}
      Yield New Object() {CUShort(10), CDbl(2), "102"}
      Yield New Object() {CUShort(10), CDec(2), "102"}
      Yield New Object() {CUShort(10), "2", "102"}
      Yield New Object() {CUShort(10), New Char() {"2"c}, "102"}
      Yield New Object() {CUShort(10), True, "10True"}
      Yield New Object() {CUShort(10), DBNull.Value, "10"}
      Yield New Object() {CUShort(10), Nothing, "10"}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(2), "102"}
      Yield New Object() {CShort(Fix(10)), CSByte(2), "102"}
      Yield New Object() {CShort(Fix(10)), CUShort(2), "102"}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(2)), "102"}
      Yield New Object() {CShort(Fix(10)), CUInt(2), "102"}
      Yield New Object() {CShort(Fix(10)), 2, "102"}
      Yield New Object() {CShort(Fix(10)), CULng(2), "102"}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(2)), "102"}
      Yield New Object() {CShort(Fix(10)), CSng(2), "102"}
      Yield New Object() {CShort(Fix(10)), CDbl(2), "102"}
      Yield New Object() {CShort(Fix(10)), CDec(2), "102"}
      Yield New Object() {CShort(Fix(10)), "2", "102"}
      Yield New Object() {CShort(Fix(10)), New Char() {"2"c}, "102"}
      Yield New Object() {CShort(Fix(10)), True, "10True"}
      Yield New Object() {CShort(Fix(10)), DBNull.Value, "10"}
      Yield New Object() {CShort(Fix(10)), Nothing, "10"}

      ' uint.
      Yield New Object() {CUInt(10), CByte(2), "102"}
      Yield New Object() {CUInt(10), CSByte(2), "102"}
      Yield New Object() {CUInt(10), CUShort(2), "102"}
      Yield New Object() {CUInt(10), CShort(Fix(2)), "102"}
      Yield New Object() {CUInt(10), CUInt(2), "102"}
      Yield New Object() {CUInt(10), 2, "102"}
      Yield New Object() {CUInt(10), CULng(2), "102"}
      Yield New Object() {CUInt(10), CLng(Fix(2)), "102"}
      Yield New Object() {CUInt(10), CSng(2), "102"}
      Yield New Object() {CUInt(10), CDbl(2), "102"}
      Yield New Object() {CUInt(10), CDec(2), "102"}
      Yield New Object() {CUInt(10), "2", "102"}
      Yield New Object() {CUInt(10), New Char() {"2"c}, "102"}
      Yield New Object() {CUInt(10), True, "10True"}
      Yield New Object() {CUInt(10), DBNull.Value, "10"}
      Yield New Object() {CUInt(10), Nothing, "10"}

      ' int.
      Yield New Object() {10, CByte(2), "102"}
      Yield New Object() {10, CSByte(2), "102"}
      Yield New Object() {10, CUShort(2), "102"}
      Yield New Object() {10, CShort(Fix(2)), "102"}
      Yield New Object() {10, CUInt(2), "102"}
      Yield New Object() {10, 2, "102"}
      Yield New Object() {10, CULng(2), "102"}
      Yield New Object() {10, CLng(Fix(2)), "102"}
      Yield New Object() {10, CSng(2), "102"}
      Yield New Object() {10, CDbl(2), "102"}
      Yield New Object() {10, CDec(2), "102"}
      Yield New Object() {10, "2", "102"}
      Yield New Object() {10, New Char() {"2"c}, "102"}
      Yield New Object() {10, True, "10True"}
      Yield New Object() {10, DBNull.Value, "10"}
      Yield New Object() {10, Nothing, "10"}

      ' ulong.
      Yield New Object() {CULng(10), CByte(2), "102"}
      Yield New Object() {CULng(10), CSByte(2), "102"}
      Yield New Object() {CULng(10), CUShort(2), "102"}
      Yield New Object() {CULng(10), CShort(Fix(2)), "102"}
      Yield New Object() {CULng(10), CUInt(2), "102"}
      Yield New Object() {CULng(10), 2, "102"}
      Yield New Object() {CULng(10), CULng(2), "102"}
      Yield New Object() {CULng(10), CLng(Fix(2)), "102"}
      Yield New Object() {CULng(10), CSng(2), "102"}
      Yield New Object() {CULng(10), CDbl(2), "102"}
      Yield New Object() {CULng(10), CDec(2), "102"}
      Yield New Object() {CULng(10), "2", "102"}
      Yield New Object() {CULng(10), New Char() {"2"c}, "102"}
      Yield New Object() {CULng(10), True, "10True"}
      Yield New Object() {CULng(10), DBNull.Value, "10"}
      Yield New Object() {CULng(10), Nothing, "10"}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(2), "102"}
      Yield New Object() {CLng(Fix(10)), CSByte(2), "102"}
      Yield New Object() {CLng(Fix(10)), CUShort(2), "102"}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(2)), "102"}
      Yield New Object() {CLng(Fix(10)), CUInt(2), "102"}
      Yield New Object() {CLng(Fix(10)), 2, "102"}
      Yield New Object() {CLng(Fix(10)), CULng(2), "102"}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(2)), "102"}
      Yield New Object() {CLng(Fix(10)), CSng(2), "102"}
      Yield New Object() {CLng(Fix(10)), CDbl(2), "102"}
      Yield New Object() {CLng(Fix(10)), CDec(2), "102"}
      Yield New Object() {CLng(Fix(10)), "2", "102"}
      Yield New Object() {CLng(Fix(10)), New Char() {"2"c}, "102"}
      Yield New Object() {CLng(Fix(10)), True, "10True"}
      Yield New Object() {CLng(Fix(10)), DBNull.Value, "10"}
      Yield New Object() {CLng(Fix(10)), Nothing, "10"}

      ' float.
      Yield New Object() {CSng(10), CByte(2), "102"}
      Yield New Object() {CSng(10), CSByte(2), "102"}
      Yield New Object() {CSng(10), CUShort(2), "102"}
      Yield New Object() {CSng(10), CShort(Fix(2)), "102"}
      Yield New Object() {CSng(10), CUInt(2), "102"}
      Yield New Object() {CSng(10), 2, "102"}
      Yield New Object() {CSng(10), CULng(2), "102"}
      Yield New Object() {CSng(10), CLng(Fix(2)), "102"}
      Yield New Object() {CSng(10), CSng(2), "102"}
      Yield New Object() {CSng(10), CDbl(2), "102"}
      Yield New Object() {CSng(10), CDec(2), "102"}
      Yield New Object() {CSng(10), "2", "102"}
      Yield New Object() {CSng(10), New Char() {"2"c}, "102"}
      Yield New Object() {CSng(10), True, "10True"}
      Yield New Object() {CSng(10), DBNull.Value, "10"}
      Yield New Object() {CSng(10), Nothing, "10"}

      ' double.
      Yield New Object() {CDbl(10), CByte(2), "102"}
      Yield New Object() {CDbl(10), CSByte(2), "102"}
      Yield New Object() {CDbl(10), CUShort(2), "102"}
      Yield New Object() {CDbl(10), CShort(Fix(2)), "102"}
      Yield New Object() {CDbl(10), CUInt(2), "102"}
      Yield New Object() {CDbl(10), 2, "102"}
      Yield New Object() {CDbl(10), CULng(2), "102"}
      Yield New Object() {CDbl(10), CLng(Fix(2)), "102"}
      Yield New Object() {CDbl(10), CSng(2), "102"}
      Yield New Object() {CDbl(10), CDbl(2), "102"}
      Yield New Object() {CDbl(10), CDec(2), "102"}
      Yield New Object() {CDbl(10), "2", "102"}
      Yield New Object() {CDbl(10), New Char() {"2"c}, "102"}
      Yield New Object() {CDbl(10), True, "10True"}
      Yield New Object() {CDbl(10), DBNull.Value, "10"}
      Yield New Object() {CDbl(10), Nothing, "10"}

      ' decimal.
      Yield New Object() {CDec(10), CByte(2), "102"}
      Yield New Object() {CDec(10), CSByte(2), "102"}
      Yield New Object() {CDec(10), CUShort(2), "102"}
      Yield New Object() {CDec(10), CShort(Fix(2)), "102"}
      Yield New Object() {CDec(10), CUInt(2), "102"}
      Yield New Object() {CDec(10), 2, "102"}
      Yield New Object() {CDec(10), CULng(2), "102"}
      Yield New Object() {CDec(10), CLng(Fix(2)), "102"}
      Yield New Object() {CDec(10), CSng(2), "102"}
      Yield New Object() {CDec(10), CDbl(2), "102"}
      Yield New Object() {CDec(10), CDec(2), "102"}
      Yield New Object() {CDec(10), "2", "102"}
      Yield New Object() {CDec(10), New Char() {"2"c}, "102"}
      Yield New Object() {CDec(10), True, "10True"}
      Yield New Object() {CDec(10), DBNull.Value, "10"}
      Yield New Object() {CDec(10), Nothing, "10"}

      ' string.
      Yield New Object() {"10", CByte(2), "102"}
      Yield New Object() {"10", CSByte(2), "102"}
      Yield New Object() {"10", CUShort(2), "102"}
      Yield New Object() {"10", CShort(Fix(2)), "102"}
      Yield New Object() {"10", CUInt(2), "102"}
      Yield New Object() {"10", 2, "102"}
      Yield New Object() {"10", CULng(2), "102"}
      Yield New Object() {"10", CLng(Fix(2)), "102"}
      Yield New Object() {"10", CSng(2), "102"}
      Yield New Object() {"10", CDbl(2), "102"}
      Yield New Object() {"10", CDec(2), "102"}
      Yield New Object() {"10", "2", "102"}
      Yield New Object() {"10", New Char() {"2"c}, "102"}
      Yield New Object() {"10", True, "10True"}
      Yield New Object() {"10", DBNull.Value, "10"}
      Yield New Object() {"10", Nothing, "10"}

      ' chars.
      Yield New Object() {New Char() {"1"c, "0"c}, CByte(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CSByte(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CUShort(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CShort(Fix(2)), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CUInt(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, 2, "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CULng(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CLng(Fix(2)), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CSng(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CDbl(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, CDec(2), "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, "2", "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, New Char() {"2"c}, "102"}
      Yield New Object() {New Char() {"1"c, "0"c}, True, "10True"}
      Yield New Object() {New Char() {"1"c, "0"c}, DBNull.Value, "10"}
      Yield New Object() {New Char() {"1"c, "0"c}, Nothing, "10"}

      ' bool.
      Yield New Object() {True, CByte(2), "True2"}
      Yield New Object() {True, CSByte(2), "True2"}
      Yield New Object() {True, CUShort(2), "True2"}
      Yield New Object() {True, CShort(Fix(2)), "True2"}
      Yield New Object() {True, CUInt(2), "True2"}
      Yield New Object() {True, 2, "True2"}
      Yield New Object() {True, CULng(2), "True2"}
      Yield New Object() {True, CLng(Fix(2)), "True2"}
      Yield New Object() {True, CSng(2), "True2"}
      Yield New Object() {True, CDbl(2), "True2"}
      Yield New Object() {True, CDec(2), "True2"}
      Yield New Object() {True, "2", "True2"}
      Yield New Object() {True, New Char() {"2"c}, "True2"}
      Yield New Object() {True, True, "TrueTrue"}
      Yield New Object() {True, DBNull.Value, "True"}
      Yield New Object() {True, Nothing, "True"}

      ' DBNull.Value.
      Yield New Object() {DBNull.Value, CByte(2), "2"}
      Yield New Object() {DBNull.Value, CSByte(2), "2"}
      Yield New Object() {DBNull.Value, CUShort(2), "2"}
      Yield New Object() {DBNull.Value, CShort(Fix(2)), "2"}
      Yield New Object() {DBNull.Value, CUInt(2), "2"}
      Yield New Object() {DBNull.Value, 2, "2"}
      Yield New Object() {DBNull.Value, CULng(2), "2"}
      Yield New Object() {DBNull.Value, CLng(Fix(2)), "2"}
      Yield New Object() {DBNull.Value, CSng(2), "2"}
      Yield New Object() {DBNull.Value, CDbl(2), "2"}
      Yield New Object() {DBNull.Value, CDec(2), "2"}
      Yield New Object() {DBNull.Value, "2", "2"}
      Yield New Object() {DBNull.Value, New Char() {"2"c}, "2"}
      Yield New Object() {DBNull.Value, True, "True"}
      Yield New Object() {DBNull.Value, DBNull.Value, DBNull.Value}
      Yield New Object() {DBNull.Value, Nothing, ""}

      ' null.
      Yield New Object() {Nothing, CByte(2), "2"}
      Yield New Object() {Nothing, CSByte(2), "2"}
      Yield New Object() {Nothing, CUShort(2), "2"}
      Yield New Object() {Nothing, CShort(Fix(2)), "2"}
      Yield New Object() {Nothing, CUInt(2), "2"}
      Yield New Object() {Nothing, 2, "2"}
      Yield New Object() {Nothing, CULng(2), "2"}
      Yield New Object() {Nothing, CLng(Fix(2)), "2"}
      Yield New Object() {Nothing, CSng(2), "2"}
      Yield New Object() {Nothing, CDbl(2), "2"}
      Yield New Object() {Nothing, CDec(2), "2"}
      Yield New Object() {Nothing, "2", "2"}
      Yield New Object() {Nothing, New Char() {"2"c}, "2"}
      Yield New Object() {Nothing, True, "True"}
      Yield New Object() {Nothing, DBNull.Value, ""}
      Yield New Object() {Nothing, Nothing, ""}

      ' object.
      Yield New Object() {New ConcatenateObject, 2, "custom"}
      Yield New Object() {2, New ConcatenateObject, "motsuc"}
      Yield New Object() {New ConcatenateObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New ConcatenateObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(ConcatenateObject_TestData))>
    Public Sub ConcatenateObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConcatenateObject(left, right))
    End Sub

    Public Shared Iterator Function ConcatenateObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {"3"c, New Object}

      Yield New Object() {New Char() {"8"c}, New Object}
      Yield New Object() {New Object, New Char() {"8"c}}
    End Function

    <Theory>
    <MemberData(NameOf(ConcatenateObject_InvalidObjects_TestData))>
    Public Sub ConcatenateObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConcatenateObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConcatenateObject(right, left))
    End Sub

    Public Shared Iterator Function ConcatenateObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConcatenateObject, New Object}
      Yield New Object() {New Object, New ConcatenateObject}
      Yield New Object() {New ConcatenateObject, New ConcatenateObject}
    End Function

    <Theory>
    <MemberData(NameOf(ConcatenateObject_MismatchingObjects_TestData))>
    Public Sub ConcatenateObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConcatenateObject(left, right))
    End Sub

    Public Class ConcatenateObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_Concatenate(left As ConcatenateObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_Concatenate(left As Integer, right As ConcatenateObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_Concatenate(left As ConcatenateObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_Concatenate(left As OperatorsTests, right As ConcatenateObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function DivideObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(4), CByte(2), CDbl(2)}
      Yield New Object() {CByte(8), CSByte(2), CDbl(4)}
      Yield New Object() {CByte(12), CUShort(2), CDbl(6)}
      Yield New Object() {CByte(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CByte(20), CUInt(2), CDbl(10)}
      Yield New Object() {CByte(24), 2, CDbl(12)}
      Yield New Object() {CByte(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CByte(32), CULng(2), CDbl(16)}
      Yield New Object() {CByte(36), CSng(2), CSng(18)}
      Yield New Object() {CByte(40), CDbl(2), CDbl(20)}
      Yield New Object() {CByte(44), CDec(2), CDec(22)}
      Yield New Object() {CByte(48), "2", CDbl(24)}
      Yield New Object() {CByte(52), True, CDbl((-52))}
      Yield New Object() {CByte(56), Nothing, Double.PositiveInfinity}

      ' sbyte.
      Yield New Object() {CSByte(4), CByte(2), CDbl(2)}
      Yield New Object() {CSByte(8), CSByte(2), CDbl(4)}
      Yield New Object() {CSByte(12), CUShort(2), CDbl(6)}
      Yield New Object() {CSByte(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CSByte(20), CUInt(2), CDbl(10)}
      Yield New Object() {CSByte(24), 2, CDbl(12)}
      Yield New Object() {CSByte(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CSByte(32), CULng(2), CDbl(16)}
      Yield New Object() {CSByte(36), CSng(2), CSng(18)}
      Yield New Object() {CSByte(40), CDbl(2), CDbl(20)}
      Yield New Object() {CSByte(44), CDec(2), CDec(22)}
      Yield New Object() {CSByte(48), "2", CDbl(24)}
      Yield New Object() {CSByte(52), True, CDbl((-52))}
      Yield New Object() {CSByte(56), Nothing, Double.PositiveInfinity}

      ' ushort.
      Yield New Object() {CUShort(4), CByte(2), CDbl(2)}
      Yield New Object() {CUShort(8), CSByte(2), CDbl(4)}
      Yield New Object() {CUShort(12), CUShort(2), CDbl(6)}
      Yield New Object() {CUShort(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CUShort(20), CUInt(2), CDbl(10)}
      Yield New Object() {CUShort(24), 2, CDbl(12)}
      Yield New Object() {CUShort(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CUShort(32), CULng(2), CDbl(16)}
      Yield New Object() {CUShort(36), CSng(2), CSng(18)}
      Yield New Object() {CUShort(40), CDbl(2), CDbl(20)}
      Yield New Object() {CUShort(44), CDec(2), CDec(22)}
      Yield New Object() {CUShort(48), "2", CDbl(24)}
      Yield New Object() {CUShort(52), True, CDbl((-52))}
      Yield New Object() {CUShort(56), Nothing, Double.PositiveInfinity}

      ' short.
      Yield New Object() {CShort(Fix(4)), CByte(2), CDbl(2)}
      Yield New Object() {CShort(Fix(8)), CSByte(2), CDbl(4)}
      Yield New Object() {CShort(Fix(12)), CUShort(2), CDbl(6)}
      Yield New Object() {CShort(Fix(16)), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CShort(Fix(20)), CUInt(2), CDbl(10)}
      Yield New Object() {CShort(Fix(24)), 2, CDbl(12)}
      Yield New Object() {CShort(Fix(28)), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CShort(Fix(32)), CULng(2), CDbl(16)}
      Yield New Object() {CShort(Fix(36)), CSng(2), CSng(18)}
      Yield New Object() {CShort(Fix(40)), CDbl(2), CDbl(20)}
      Yield New Object() {CShort(Fix(44)), CDec(2), CDec(22)}
      Yield New Object() {CShort(Fix(48)), "2", CDbl(24)}
      Yield New Object() {CShort(Fix(52)), True, CDbl((-52))}
      Yield New Object() {CShort(Fix(56)), Nothing, Double.PositiveInfinity}

      ' uint.
      Yield New Object() {CUInt(4), CByte(2), CDbl(2)}
      Yield New Object() {CUInt(8), CSByte(2), CDbl(4)}
      Yield New Object() {CUInt(12), CUShort(2), CDbl(6)}
      Yield New Object() {CUInt(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CUInt(20), CUInt(2), CDbl(10)}
      Yield New Object() {CUInt(24), 2, CDbl(12)}
      Yield New Object() {CUInt(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CUInt(32), CULng(2), CDbl(16)}
      Yield New Object() {CUInt(36), CSng(2), CSng(18)}
      Yield New Object() {CUInt(40), CDbl(2), CDbl(20)}
      Yield New Object() {CUInt(44), CDec(2), CDec(22)}
      Yield New Object() {CUInt(48), "2", CDbl(24)}
      Yield New Object() {CUInt(52), True, CDbl((-52))}
      Yield New Object() {CUInt(56), Nothing, Double.PositiveInfinity}

      ' int.
      Yield New Object() {4, CByte(2), CDbl(2)}
      Yield New Object() {8, CSByte(2), CDbl(4)}
      Yield New Object() {12, CUShort(2), CDbl(6)}
      Yield New Object() {16, CShort(Fix(2)), CDbl(8)}
      Yield New Object() {20, CUInt(2), CDbl(10)}
      Yield New Object() {24, 2, CDbl(12)}
      Yield New Object() {28, CLng(Fix(2)), CDbl(14)}
      Yield New Object() {32, CULng(2), CDbl(16)}
      Yield New Object() {36, CSng(2), CSng(18)}
      Yield New Object() {40, CDbl(2), CDbl(20)}
      Yield New Object() {44, CDec(2), CDec(22)}
      Yield New Object() {48, "2", CDbl(24)}
      Yield New Object() {52, True, CDbl((-52))}
      Yield New Object() {56, Nothing, Double.PositiveInfinity}

      ' ulong.
      Yield New Object() {CULng(4), CByte(2), CDbl(2)}
      Yield New Object() {CULng(8), CSByte(2), CDbl(4)}
      Yield New Object() {CULng(12), CUShort(2), CDbl(6)}
      Yield New Object() {CULng(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CULng(20), CUInt(2), CDbl(10)}
      Yield New Object() {CULng(24), 2, CDbl(12)}
      Yield New Object() {CULng(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CULng(32), CULng(2), CDbl(16)}
      Yield New Object() {CULng(36), CSng(2), CSng(18)}
      Yield New Object() {CULng(40), CDbl(2), CDbl(20)}
      Yield New Object() {CULng(44), CDec(2), CDec(22)}
      Yield New Object() {CULng(48), "2", CDbl(24)}
      Yield New Object() {CULng(52), True, CDbl((-52))}
      Yield New Object() {CULng(56), Nothing, Double.PositiveInfinity}

      ' long + primitives.
      Yield New Object() {CLng(Fix(4)), CByte(2), CDbl(2)}
      Yield New Object() {CLng(Fix(8)), CSByte(2), CDbl(4)}
      Yield New Object() {CLng(Fix(12)), CUShort(2), CDbl(6)}
      Yield New Object() {CLng(Fix(16)), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CLng(Fix(20)), CUInt(2), CDbl(10)}
      Yield New Object() {CLng(Fix(24)), 2, CDbl(12)}
      Yield New Object() {CLng(Fix(28)), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CLng(Fix(32)), CULng(2), CDbl(16)}
      Yield New Object() {CLng(Fix(36)), CSng(2), CSng(18)}
      Yield New Object() {CLng(Fix(40)), CDbl(2), CDbl(20)}
      Yield New Object() {CLng(Fix(44)), CDec(2), CDec(22)}
      Yield New Object() {CLng(Fix(48)), "2", CDbl(24)}
      Yield New Object() {CLng(Fix(52)), True, CDbl((-52))}
      Yield New Object() {CLng(Fix(56)), Nothing, Double.PositiveInfinity}

      ' float.
      Yield New Object() {CSng(4), CByte(2), CSng(2)}
      Yield New Object() {CSng(8), CSByte(2), CSng(4)}
      Yield New Object() {CSng(12), CUShort(2), CSng(6)}
      Yield New Object() {CSng(16), CShort(Fix(2)), CSng(8)}
      Yield New Object() {CSng(20), CUInt(2), CSng(10)}
      Yield New Object() {CSng(24), 2, CSng(12)}
      Yield New Object() {CSng(28), CLng(Fix(2)), CSng(14)}
      Yield New Object() {CSng(32), CULng(2), CSng(16)}
      Yield New Object() {CSng(36), CSng(2), CSng(18)}
      Yield New Object() {CSng(40), CDbl(2), CDbl(20)}
      Yield New Object() {CSng(44), CDec(2), CSng(22)}
      Yield New Object() {CSng(48), "2", CDbl(24)}
      Yield New Object() {CSng(52), True, CSng((-52))}
      Yield New Object() {CSng(56), Nothing, Double.PositiveInfinity}
      Yield New Object() {CSng(58), Single.PositiveInfinity, CSng(0)}
      Yield New Object() {CSng(58), Single.NegativeInfinity, CSng((-0.0F))}
      Yield New Object() {CSng(58), Single.NaN, Single.NaN}
      Yield New Object() {Single.PositiveInfinity, CSng(2), Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity, CSng(2), Single.NegativeInfinity}
      Yield New Object() {Single.NaN, CSng(2), Single.NaN}

      ' double.
      Yield New Object() {CDbl(4), CByte(2), CDbl(2)}
      Yield New Object() {CDbl(8), CSByte(2), CDbl(4)}
      Yield New Object() {CDbl(12), CUShort(2), CDbl(6)}
      Yield New Object() {CDbl(16), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CDbl(20), CUInt(2), CDbl(10)}
      Yield New Object() {CDbl(24), 2, CDbl(12)}
      Yield New Object() {CDbl(28), CLng(Fix(2)), CDbl(14)}
      Yield New Object() {CDbl(32), CULng(2), CDbl(16)}
      Yield New Object() {CDbl(36), CSng(2), CDbl(18)}
      Yield New Object() {CDbl(40), CDbl(2), CDbl(20)}
      Yield New Object() {CDbl(44), CDec(2), CDbl(22)}
      Yield New Object() {CDbl(48), "2", CDbl(24)}
      Yield New Object() {CDbl(52), True, CDbl((-52))}
      Yield New Object() {CDbl(56), Nothing, Double.PositiveInfinity}
      Yield New Object() {CDbl(58), Double.PositiveInfinity, CDbl(0)}
      Yield New Object() {CDbl(58), Double.NegativeInfinity, CDbl((-0.0))}
      Yield New Object() {CDbl(58), Double.NaN, Double.NaN}
      Yield New Object() {Double.PositiveInfinity, CDbl(2), Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity, CDbl(2), Double.NegativeInfinity}
      Yield New Object() {Double.NaN, CDbl(2), Double.NaN}

      ' decimal.
      Yield New Object() {CDec(4), CByte(2), CDec(2)}
      Yield New Object() {CDec(8), CSByte(2), CDec(4)}
      Yield New Object() {CDec(12), CUShort(2), CDec(6)}
      Yield New Object() {CDec(16), CShort(Fix(2)), CDec(8)}
      Yield New Object() {CDec(20), CUInt(2), CDec(10)}
      Yield New Object() {CDec(24), 2, CDec(12)}
      Yield New Object() {CDec(28), CLng(Fix(2)), CDec(14)}
      Yield New Object() {CDec(32), CULng(2), CDec(16)}
      Yield New Object() {CDec(36), CSng(2), CSng(18)}
      Yield New Object() {CDec(40), CDbl(2), CDbl(20)}
      Yield New Object() {CDec(44), CDec(2), CDec(22)}
      Yield New Object() {CDec(48), "2", CDbl(24)}
      Yield New Object() {CDec(52), True, CDec((-52))}
      Yield New Object() {Decimal.MaxValue, CDec(0.5), Single.Parse("1.58456325028529E+29", NumberStyles.Any, CultureInfo.InvariantCulture)}

      ' string + primitives
      Yield New Object() {"4", CByte(2), CDbl(2)}
      Yield New Object() {"8", CSByte(2), CDbl(4)}
      Yield New Object() {"12", CUShort(2), CDbl(6)}
      Yield New Object() {"16", CShort(Fix(2)), CDbl(8)}
      Yield New Object() {"20", CUInt(2), CDbl(10)}
      Yield New Object() {"24", 2, CDbl(12)}
      Yield New Object() {"28", CLng(Fix(2)), CDbl(14)}
      Yield New Object() {"32", CULng(2), CDbl(16)}
      Yield New Object() {"36", CSng(2), CDbl(18)}
      Yield New Object() {"40", CDbl(2), CDbl(20)}
      Yield New Object() {"44", CDec(2), CDbl(22)}
      Yield New Object() {"48", "2", CDbl(24)}
      Yield New Object() {"52", True, CDbl((-52))}
      Yield New Object() {"56", Nothing, Double.PositiveInfinity}

      ' bool.
      Yield New Object() {True, CByte(2), CDbl((-0.5))}
      Yield New Object() {True, CSByte(2), CDbl((-0.5))}
      Yield New Object() {True, CUShort(2), CDbl((-0.5))}
      Yield New Object() {True, CShort(Fix(2)), CDbl((-0.5))}
      Yield New Object() {True, CUInt(2), CDbl((-0.5))}
      Yield New Object() {True, 2, CDbl((-0.5))}
      Yield New Object() {True, CLng(Fix(2)), CDbl((-0.5))}
      Yield New Object() {True, CULng(2), CDbl((-0.5))}
      Yield New Object() {True, CSng(2), CSng((-0.5))}
      Yield New Object() {True, CDbl(2), CDbl((-0.5))}
      Yield New Object() {True, CDec(2), CDec((-0.5))}
      Yield New Object() {True, "2", CDbl((-0.5))}
      Yield New Object() {True, True, CDbl(1)}
      Yield New Object() {True, Nothing, Double.NegativeInfinity}

      ' null.
      Yield New Object() {Nothing, CByte(2), CDbl(0)}
      Yield New Object() {Nothing, CSByte(2), CDbl(0)}
      Yield New Object() {Nothing, CUShort(2), CDbl(0)}
      Yield New Object() {Nothing, CShort(Fix(2)), CDbl(0)}
      Yield New Object() {Nothing, CUInt(2), CDbl(0)}
      Yield New Object() {Nothing, 2, CDbl(0)}
      Yield New Object() {Nothing, CLng(Fix(2)), CDbl(0)}
      Yield New Object() {Nothing, CULng(2), CDbl(0)}
      Yield New Object() {Nothing, CSng(2), CSng(0)}
      Yield New Object() {Nothing, CDbl(2), CDbl(0)}
      Yield New Object() {Nothing, CDec(2), CDec(0)}
      Yield New Object() {Nothing, "2", CDbl(0)}
      Yield New Object() {Nothing, False, Double.NaN}
      Yield New Object() {Nothing, Nothing, Double.NaN}

      ' object.
      Yield New Object() {New DivideObject, 2, "custom"}
      Yield New Object() {2, New DivideObject, "motsuc"}
      Yield New Object() {New DivideObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New DivideObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(DivideObject_TestData))>
    Public Sub DivideObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.DivideObject(left, right))
    End Sub

    <Fact>
    Public Sub DivideObject_DecimalWithNull_ThrowsDivideByZeroException()
      Assert.Throws(Of DivideByZeroException)(Function() Operators.DivideObject(CDec(56), Nothing))
    End Sub

    Public Shared Iterator Function DivideObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(DivideObject_InvalidObjects_TestData))>
    Public Sub DivideObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.DivideObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.DivideObject(right, left))
    End Sub

    Public Shared Iterator Function DivideObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New DivideObject, New Object}
      Yield New Object() {New Object, New DivideObject}
      Yield New Object() {New DivideObject, New DivideObject}
    End Function

    <Theory>
    <MemberData(NameOf(DivideObject_MismatchingObjects_TestData))>
    Public Sub DivideObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.DivideObject(left, right))
    End Sub

    Public Class DivideObject

      Public Shared Operator /(left As DivideObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator /(left As Integer, right As DivideObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator /(left As DivideObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator /(left As OperatorsTests, right As DivideObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function ExponentObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(2), CByte(4), CDbl(16)}
      Yield New Object() {CByte(2), CSByte(4), CDbl(16)}
      Yield New Object() {CByte(2), CUShort(4), CDbl(16)}
      Yield New Object() {CByte(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CByte(2), CUInt(4), CDbl(16)}
      Yield New Object() {CByte(2), 4, CDbl(16)}
      Yield New Object() {CByte(2), CULng(4), CDbl(16)}
      Yield New Object() {CByte(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CByte(2), CSng(4), CDbl(16)}
      Yield New Object() {CByte(2), CDbl(4), CDbl(16)}
      Yield New Object() {CByte(2), CDec(4), CDbl(16)}
      Yield New Object() {CByte(2), "4", CDbl(16)}
      Yield New Object() {CByte(2), True, CDbl(0.5)}
      Yield New Object() {CByte(2), Nothing, CDbl(1)}

      ' sbyte.
      Yield New Object() {CSByte(2), CByte(4), CDbl(16)}
      Yield New Object() {CSByte(2), CSByte(4), CDbl(16)}
      Yield New Object() {CSByte(2), CUShort(4), CDbl(16)}
      Yield New Object() {CSByte(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CSByte(2), CUInt(4), CDbl(16)}
      Yield New Object() {CSByte(2), 4, CDbl(16)}
      Yield New Object() {CSByte(2), CULng(4), CDbl(16)}
      Yield New Object() {CSByte(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CSByte(2), CSng(4), CDbl(16)}
      Yield New Object() {CSByte(2), CDbl(4), CDbl(16)}
      Yield New Object() {CSByte(2), CDec(4), CDbl(16)}
      Yield New Object() {CSByte(2), "4", CDbl(16)}
      Yield New Object() {CSByte(2), True, CDbl(0.5)}
      Yield New Object() {CSByte(2), Nothing, CDbl(1)}

      ' uint.
      Yield New Object() {CUInt(2), CByte(4), CDbl(16)}
      Yield New Object() {CUInt(2), CSByte(4), CDbl(16)}
      Yield New Object() {CUInt(2), CUShort(4), CDbl(16)}
      Yield New Object() {CUInt(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CUInt(2), CUInt(4), CDbl(16)}
      Yield New Object() {CUInt(2), 4, CDbl(16)}
      Yield New Object() {CUInt(2), CULng(4), CDbl(16)}
      Yield New Object() {CUInt(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CUInt(2), CSng(4), CDbl(16)}
      Yield New Object() {CUInt(2), CDbl(4), CDbl(16)}
      Yield New Object() {CUInt(2), CDec(4), CDbl(16)}
      Yield New Object() {CUInt(2), "4", CDbl(16)}
      Yield New Object() {CUInt(2), True, CDbl(0.5)}
      Yield New Object() {CUInt(2), Nothing, CDbl(1)}

      ' int.
      Yield New Object() {2, CByte(4), CDbl(16)}
      Yield New Object() {2, CSByte(4), CDbl(16)}
      Yield New Object() {2, CUShort(4), CDbl(16)}
      Yield New Object() {2, CShort(Fix(4)), CDbl(16)}
      Yield New Object() {2, CUInt(4), CDbl(16)}
      Yield New Object() {2, 4, CDbl(16)}
      Yield New Object() {2, CULng(4), CDbl(16)}
      Yield New Object() {2, CLng(Fix(4)), CDbl(16)}
      Yield New Object() {2, CSng(4), CDbl(16)}
      Yield New Object() {2, CDbl(4), CDbl(16)}
      Yield New Object() {2, CDec(4), CDbl(16)}
      Yield New Object() {2, "4", CDbl(16)}
      Yield New Object() {2, True, CDbl(0.5)}
      Yield New Object() {2, Nothing, CDbl(1)}

      ' ulong.
      Yield New Object() {CULng(2), CByte(4), CDbl(16)}
      Yield New Object() {CULng(2), CSByte(4), CDbl(16)}
      Yield New Object() {CULng(2), CUShort(4), CDbl(16)}
      Yield New Object() {CULng(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CULng(2), CUInt(4), CDbl(16)}
      Yield New Object() {CULng(2), 4, CDbl(16)}
      Yield New Object() {CULng(2), CULng(4), CDbl(16)}
      Yield New Object() {CULng(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CULng(2), CSng(4), CDbl(16)}
      Yield New Object() {CULng(2), CDbl(4), CDbl(16)}
      Yield New Object() {CULng(2), CDec(4), CDbl(16)}
      Yield New Object() {CULng(2), "4", CDbl(16)}
      Yield New Object() {CULng(2), True, CDbl(0.5)}
      Yield New Object() {CULng(2), Nothing, CDbl(1)}

      ' long.
      Yield New Object() {CLng(Fix(2)), CByte(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CSByte(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CUShort(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CUInt(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), 4, CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CULng(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CSng(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CDbl(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), CDec(4), CDbl(16)}
      Yield New Object() {CLng(Fix(2)), "4", CDbl(16)}
      Yield New Object() {CLng(Fix(2)), True, CDbl(0.5)}
      Yield New Object() {CLng(Fix(2)), Nothing, CDbl(1)}

      ' float.
      Yield New Object() {CSng(2), CByte(4), CDbl(16)}
      Yield New Object() {CSng(2), CSByte(4), CDbl(16)}
      Yield New Object() {CSng(2), CUShort(4), CDbl(16)}
      Yield New Object() {CSng(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CSng(2), CUInt(4), CDbl(16)}
      Yield New Object() {CSng(2), 4, CDbl(16)}
      Yield New Object() {CSng(2), CULng(4), CDbl(16)}
      Yield New Object() {CSng(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CSng(2), CSng(4), CDbl(16)}
      Yield New Object() {CSng(2), CDbl(4), CDbl(16)}
      Yield New Object() {CSng(2), CDec(4), CDbl(16)}
      Yield New Object() {CSng(2), "4", CDbl(16)}
      Yield New Object() {CSng(2), True, CDbl(0.5)}
      Yield New Object() {CSng(2), Nothing, CDbl(1)}

      ' double.
      Yield New Object() {CDbl(2), CByte(4), CDbl(16)}
      Yield New Object() {CDbl(2), CSByte(4), CDbl(16)}
      Yield New Object() {CDbl(2), CUShort(4), CDbl(16)}
      Yield New Object() {CDbl(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CDbl(2), CUInt(4), CDbl(16)}
      Yield New Object() {CDbl(2), 4, CDbl(16)}
      Yield New Object() {CDbl(2), CULng(4), CDbl(16)}
      Yield New Object() {CDbl(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CDbl(2), CSng(4), CDbl(16)}
      Yield New Object() {CDbl(2), CDbl(4), CDbl(16)}
      Yield New Object() {CDbl(2), CDec(4), CDbl(16)}
      Yield New Object() {CDbl(2), "4", CDbl(16)}
      Yield New Object() {CDbl(2), True, CDbl(0.5)}
      Yield New Object() {CDbl(2), Nothing, CDbl(1)}

      ' decimal.
      Yield New Object() {CDec(2), CByte(4), CDbl(16)}
      Yield New Object() {CDec(2), CSByte(4), CDbl(16)}
      Yield New Object() {CDec(2), CUShort(4), CDbl(16)}
      Yield New Object() {CDec(2), CShort(Fix(4)), CDbl(16)}
      Yield New Object() {CDec(2), CUInt(4), CDbl(16)}
      Yield New Object() {CDec(2), 4, CDbl(16)}
      Yield New Object() {CDec(2), CULng(4), CDbl(16)}
      Yield New Object() {CDec(2), CLng(Fix(4)), CDbl(16)}
      Yield New Object() {CDec(2), CSng(4), CDbl(16)}
      Yield New Object() {CDec(2), CDbl(4), CDbl(16)}
      Yield New Object() {CDec(2), CDec(4), CDbl(16)}
      Yield New Object() {CDec(2), "4", CDbl(16)}
      Yield New Object() {CDec(2), True, CDbl(0.5)}
      Yield New Object() {CDec(2), Nothing, CDbl(1)}

      ' string.
      Yield New Object() {"2", CByte(4), CDbl(16)}
      Yield New Object() {"2", CSByte(4), CDbl(16)}
      Yield New Object() {"2", CUShort(4), CDbl(16)}
      Yield New Object() {"2", CShort(Fix(4)), CDbl(16)}
      Yield New Object() {"2", CUInt(4), CDbl(16)}
      Yield New Object() {"2", 4, CDbl(16)}
      Yield New Object() {"2", CULng(4), CDbl(16)}
      Yield New Object() {"2", CLng(Fix(4)), CDbl(16)}
      Yield New Object() {"2", CSng(4), CDbl(16)}
      Yield New Object() {"2", CDbl(4), CDbl(16)}
      Yield New Object() {"2", CDec(4), CDbl(16)}
      Yield New Object() {"2", "4", CDbl(16)}
      Yield New Object() {"2", True, CDbl(0.5)}
      Yield New Object() {"2", Nothing, CDbl(1)}

      ' bool.
      Yield New Object() {True, CByte(4), CDbl(1)}
      Yield New Object() {True, CSByte(4), CDbl(1)}
      Yield New Object() {True, CUShort(4), CDbl(1)}
      Yield New Object() {True, CShort(Fix(4)), CDbl(1)}
      Yield New Object() {True, CUInt(4), CDbl(1)}
      Yield New Object() {True, 4, CDbl(1)}
      Yield New Object() {True, CULng(4), CDbl(1)}
      Yield New Object() {True, CLng(Fix(4)), CDbl(1)}
      Yield New Object() {True, CSng(4), CDbl(1)}
      Yield New Object() {True, CDbl(4), CDbl(1)}
      Yield New Object() {True, CDec(4), CDbl(1)}
      Yield New Object() {True, "4", CDbl(1)}
      Yield New Object() {True, True, CDbl((-1))}
      Yield New Object() {True, Nothing, CDbl(1)}

      ' null.
      Yield New Object() {Nothing, CByte(4), CDbl(0)}
      Yield New Object() {Nothing, CSByte(4), CDbl(0)}
      Yield New Object() {Nothing, CUShort(4), CDbl(0)}
      Yield New Object() {Nothing, CShort(Fix(4)), CDbl(0)}
      Yield New Object() {Nothing, CUInt(4), CDbl(0)}
      Yield New Object() {Nothing, 4, CDbl(0)}
      Yield New Object() {Nothing, CULng(4), CDbl(0)}
      Yield New Object() {Nothing, CLng(Fix(4)), CDbl(0)}
      Yield New Object() {Nothing, CSng(4), CDbl(0)}
      Yield New Object() {Nothing, CDbl(4), CDbl(0)}
      Yield New Object() {Nothing, CDec(4), CDbl(0)}
      Yield New Object() {Nothing, "4", CDbl(0)}
      Yield New Object() {Nothing, True, Double.PositiveInfinity}
      Yield New Object() {Nothing, Nothing, CDbl(1)}

      ' object.
      Yield New Object() {New ExponentObject, 2, "custom"}
      Yield New Object() {2, New ExponentObject, "motsuc"}
      Yield New Object() {New ExponentObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New ExponentObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(ExponentObject_TestData))>
    Public Sub ExponentObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ExponentObject(left, right))
    End Sub

    Public Shared Iterator Function ExponentObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(ExponentObject_InvalidObjects_TestData))>
    Public Sub ExponentObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ExponentObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ExponentObject(right, left))
    End Sub

    Public Shared Iterator Function ExponentObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ExponentObject, New Object}
      Yield New Object() {New Object, New ExponentObject}
      Yield New Object() {New ExponentObject, New ExponentObject}
    End Function

    <Theory>
    <MemberData(NameOf(ExponentObject_MismatchingObjects_TestData))>
    Public Sub ExponentObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ExponentObject(left, right))
    End Sub

    Public Class ExponentObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_Exponent(left As ExponentObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_Exponent(left As Integer, right As ExponentObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_Exponent(left As ExponentObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_Exponent(left As OperatorsTests, right As ExponentObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function IntDivideObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(4), CByte(2), CByte(2)}
      Yield New Object() {CByte(8), CSByte(2), CShort(Fix(4))}
      Yield New Object() {CByte(12), CUShort(2), CUShort(6)}
      Yield New Object() {CByte(16), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CByte(20), CUInt(2), CUInt(10)}
      Yield New Object() {CByte(24), 2, 12}
      Yield New Object() {CByte(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CByte(32), CULng(2), CULng(16)}
      Yield New Object() {CByte(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CByte(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CByte(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CByte(48), "2", CLng(Fix(24))}
      Yield New Object() {CByte(52), True, CShort(Fix((-52)))}

      ' sbyte.
      Yield New Object() {CSByte(4), CByte(2), CShort(Fix(2))}
      Yield New Object() {CSByte(8), CSByte(2), CSByte(4)}
      Yield New Object() {CSByte(12), CUShort(2), 6}
      Yield New Object() {CSByte(16), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CSByte(20), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CSByte(24), 2, 12}
      Yield New Object() {CSByte(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CSByte(32), CULng(2), CLng(Fix(16))}
      Yield New Object() {CSByte(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CSByte(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CSByte(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CSByte(48), "2", CLng(Fix(24))}
      Yield New Object() {CSByte(52), True, CSByte((-52))}
      Yield New Object() {SByte.MinValue, CSByte((-1)), CShort(Fix(128))}

      ' ushort.
      Yield New Object() {CUShort(4), CByte(2), CUShort(2)}
      Yield New Object() {CUShort(8), CSByte(2), 4}
      Yield New Object() {CUShort(12), CUShort(2), CUShort(6)}
      Yield New Object() {CUShort(16), CShort(Fix(2)), 8}
      Yield New Object() {CUShort(20), CUInt(2), CUInt(10)}
      Yield New Object() {CUShort(24), 2, 12}
      Yield New Object() {CUShort(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CUShort(32), CULng(2), CULng(16)}
      Yield New Object() {CUShort(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CUShort(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CUShort(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CUShort(48), "2", CLng(Fix(24))}
      Yield New Object() {CUShort(52), True, -52}

      ' short.
      Yield New Object() {CShort(Fix(4)), CByte(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(8)), CSByte(2), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(12)), CUShort(2), 6}
      Yield New Object() {CShort(Fix(16)), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CShort(Fix(20)), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(24)), 2, 12}
      Yield New Object() {CShort(Fix(28)), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(32)), CULng(2), CLng(Fix(16))}
      Yield New Object() {CShort(Fix(36)), CSng(2), CLng(Fix(18))}
      Yield New Object() {CShort(Fix(40)), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CShort(Fix(44)), CDec(2), CLng(Fix(22))}
      Yield New Object() {CShort(Fix(48)), "2", CLng(Fix(24))}
      Yield New Object() {CShort(Fix(52)), True, CShort(Fix((-52)))}
      Yield New Object() {Short.MinValue, CShort(Fix((-1))), 32768}

      ' uint.
      Yield New Object() {CUInt(4), CByte(2), CUInt(2)}
      Yield New Object() {CUInt(8), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CUInt(12), CUShort(2), CUInt(6)}
      Yield New Object() {CUInt(16), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CUInt(20), CUInt(2), CUInt(10)}
      Yield New Object() {CUInt(24), 2, CLng(Fix(12))}
      Yield New Object() {CUInt(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CUInt(32), CULng(2), CULng(16)}
      Yield New Object() {CUInt(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CUInt(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CUInt(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CUInt(48), "2", CLng(Fix(24))}
      Yield New Object() {CUInt(52), True, CLng(Fix((-52)))}

      ' int.
      Yield New Object() {4, CByte(2), 2}
      Yield New Object() {8, CSByte(2), 4}
      Yield New Object() {12, CUShort(2), 6}
      Yield New Object() {16, CShort(Fix(2)), 8}
      Yield New Object() {20, CUInt(2), CLng(Fix(10))}
      Yield New Object() {24, 2, 12}
      Yield New Object() {28, CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {32, CULng(2), CLng(Fix(16))}
      Yield New Object() {36, CSng(2), CLng(Fix(18))}
      Yield New Object() {40, CDbl(2), CLng(Fix(20))}
      Yield New Object() {44, CDec(2), CLng(Fix(22))}
      Yield New Object() {48, "2", CLng(Fix(24))}
      Yield New Object() {52, True, -52}
      Yield New Object() {Integer.MinValue, -1, CLng(Fix(2147483648))}

      ' ulong.
      Yield New Object() {CULng(4), CByte(2), CULng(2)}
      Yield New Object() {CULng(8), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CULng(12), CUShort(2), CULng(6)}
      Yield New Object() {CULng(16), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CULng(20), CUInt(2), CULng(10)}
      Yield New Object() {CULng(24), 2, CLng(Fix(12))}
      Yield New Object() {CULng(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CULng(32), CULng(2), CULng(16)}
      Yield New Object() {CULng(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CULng(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CULng(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CULng(48), "2", CLng(Fix(24))}
      Yield New Object() {CULng(52), True, CLng(Fix((-52)))}

      ' long.
      Yield New Object() {CLng(Fix(4)), CByte(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(8)), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(12)), CUShort(2), CLng(Fix(6))}
      Yield New Object() {CLng(Fix(16)), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CLng(Fix(20)), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(24)), 2, CLng(Fix(12))}
      Yield New Object() {CLng(Fix(28)), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(32)), CULng(2), CLng(Fix(16))}
      Yield New Object() {CLng(Fix(36)), CSng(2), CLng(Fix(18))}
      Yield New Object() {CLng(Fix(40)), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CLng(Fix(44)), CDec(2), CLng(Fix(22))}
      Yield New Object() {CLng(Fix(48)), "2", CLng(Fix(24))}
      Yield New Object() {CLng(Fix(52)), True, CLng(Fix((-52)))}

      ' float.
      Yield New Object() {CSng(4), CByte(2), CLng(Fix(2))}
      Yield New Object() {CSng(8), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CSng(12), CUShort(2), CLng(Fix(6))}
      Yield New Object() {CSng(16), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CSng(20), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CSng(24), 2, CLng(Fix(12))}
      Yield New Object() {CSng(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CSng(32), CULng(2), CLng(Fix(16))}
      Yield New Object() {CSng(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CSng(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CSng(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CSng(48), "2", CLng(Fix(24))}
      Yield New Object() {CSng(52), True, CLng(Fix((-52)))}

      ' double.
      Yield New Object() {CDbl(4), CByte(2), CLng(Fix(2))}
      Yield New Object() {CDbl(8), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CDbl(12), CUShort(2), CLng(Fix(6))}
      Yield New Object() {CDbl(16), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CDbl(20), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CDbl(24), 2, CLng(Fix(12))}
      Yield New Object() {CDbl(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CDbl(32), CULng(2), CLng(Fix(16))}
      Yield New Object() {CDbl(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CDbl(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CDbl(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CDbl(48), "2", CLng(Fix(24))}
      Yield New Object() {CDbl(52), True, CLng(Fix((-52)))}

      ' decimal.
      Yield New Object() {CDec(4), CByte(2), CLng(Fix(2))}
      Yield New Object() {CDec(8), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CDec(12), CUShort(2), CLng(Fix(6))}
      Yield New Object() {CDec(16), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CDec(20), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CDec(24), 2, CLng(Fix(12))}
      Yield New Object() {CDec(28), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CDec(32), CULng(2), CLng(Fix(16))}
      Yield New Object() {CDec(36), CSng(2), CLng(Fix(18))}
      Yield New Object() {CDec(40), CDbl(2), CLng(Fix(20))}
      Yield New Object() {CDec(44), CDec(2), CLng(Fix(22))}
      Yield New Object() {CDec(48), "2", CLng(Fix(24))}
      Yield New Object() {CDec(52), True, CLng(Fix((-52)))}

      ' string.
      Yield New Object() {"4", CByte(2), CLng(Fix(2))}
      Yield New Object() {"8", CSByte(2), CLng(Fix(4))}
      Yield New Object() {"12", CUShort(2), CLng(Fix(6))}
      Yield New Object() {"16", CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {"20", CUInt(2), CLng(Fix(10))}
      Yield New Object() {"24", 2, CLng(Fix(12))}
      Yield New Object() {"28", CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {"32", CULng(2), CLng(Fix(16))}
      Yield New Object() {"36", CSng(2), CLng(Fix(18))}
      Yield New Object() {"40", CDbl(2), CLng(Fix(20))}
      Yield New Object() {"44", CDec(2), CLng(Fix(22))}
      Yield New Object() {"48", "2", CLng(Fix(24))}
      Yield New Object() {"52", True, CLng(Fix((-52)))}

      ' bool.
      Yield New Object() {True, CByte(2), CShort(Fix(0))}
      Yield New Object() {True, CSByte(2), CSByte(0)}
      Yield New Object() {True, CUShort(2), 0}
      Yield New Object() {True, CShort(Fix(2)), CShort(Fix(0))}
      Yield New Object() {True, CUInt(2), CLng(Fix(0))}
      Yield New Object() {True, 2, 0}
      Yield New Object() {True, CLng(Fix(2)), CLng(Fix(0))}
      Yield New Object() {True, CULng(2), CLng(Fix(0))}
      Yield New Object() {True, CSng(2), CLng(Fix(0))}
      Yield New Object() {True, CDbl(2), CLng(Fix(0))}
      Yield New Object() {True, CDec(2), CLng(Fix(0))}
      Yield New Object() {True, "2", CLng(Fix(0))}
      Yield New Object() {True, True, CShort(Fix(1))}

      ' null.
      Yield New Object() {Nothing, CByte(2), CByte(0)}
      Yield New Object() {Nothing, CSByte(2), CSByte(0)}
      Yield New Object() {Nothing, CUShort(2), CUShort(0)}
      Yield New Object() {Nothing, CShort(Fix(2)), CShort(Fix(0))}
      Yield New Object() {Nothing, CUInt(2), CUInt(0)}
      Yield New Object() {Nothing, 2, 0}
      Yield New Object() {Nothing, CLng(Fix(2)), CLng(Fix(0))}
      Yield New Object() {Nothing, CULng(2), CULng(0)}
      Yield New Object() {Nothing, CSng(2), CLng(Fix(0))}
      Yield New Object() {Nothing, CDbl(2), CLng(Fix(0))}
      Yield New Object() {Nothing, CDec(2), CLng(Fix(0))}
      Yield New Object() {Nothing, "2", CLng(Fix(0))}

      ' object.
      Yield New Object() {New IntDivideObject, 2, "custom"}
      Yield New Object() {2, New IntDivideObject, "motsuc"}
      Yield New Object() {New IntDivideObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New IntDivideObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(IntDivideObject_TestData))>
    Public Sub IntDivideObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.IntDivideObject(left, right))
    End Sub

    Public Shared Iterator Function IntDivideObject_DivideByZero_TestData() As IEnumerable(Of Object())
      Yield New Object() {CByte(1)}
      Yield New Object() {CSByte(2)}
      Yield New Object() {CUShort(1)}
      Yield New Object() {CShort(Fix(1))}
      Yield New Object() {CUInt(1)}
      Yield New Object() {CInt(Fix(1))}
      Yield New Object() {CULng(1)}
      Yield New Object() {CLng(Fix(1))}
      Yield New Object() {CSng(1)}
      Yield New Object() {CDbl(1)}
      Yield New Object() {CDec(1)}
      Yield New Object() {"1"}
      Yield New Object() {True}
      Yield New Object() {Nothing}
    End Function

    <Theory>
    <MemberData(NameOf(IntDivideObject_DivideByZero_TestData))>
    Public Sub IntDivideObject_NullRight_ThrowsDivideByZeroException(left As Object)
      Assert.Throws(Of DivideByZeroException)(Function() Operators.IntDivideObject(left, Nothing))
      Assert.Throws(Of DivideByZeroException)(Function() Operators.IntDivideObject(left, False))
      Assert.Throws(Of DivideByZeroException)(Function() Operators.IntDivideObject(left, 0))
    End Sub

    Public Shared Iterator Function IntDivideObject_Overflow_TestData() As IEnumerable(Of Object())

      Yield New Object() {Long.MinValue, -1}

      Yield New Object() {CSng(58), Single.PositiveInfinity}
      Yield New Object() {CSng(58), Single.NegativeInfinity}
      Yield New Object() {CSng(58), Single.NaN}
      Yield New Object() {Single.PositiveInfinity, CSng(2)}
      Yield New Object() {Single.NegativeInfinity, CSng(2)}
      Yield New Object() {Single.NaN, CSng(2)}

      Yield New Object() {CDbl(58), Double.PositiveInfinity}
      Yield New Object() {CDbl(58), Double.NegativeInfinity}
      Yield New Object() {CDbl(58), Double.NaN}
      Yield New Object() {Double.PositiveInfinity, CDbl(2)}
      Yield New Object() {Double.NegativeInfinity, CDbl(2)}
      Yield New Object() {Double.NaN, CDbl(2)}

    End Function

    <Theory>
    <MemberData(NameOf(IntDivideObject_Overflow_TestData))>
    Public Sub IntDivideObject_ResultOverflows_ThrowsOverflowException(left As Object, right As Object)
      Assert.Throws(Of OverflowException)(Function() Operators.IntDivideObject(left, right))
    End Sub

    Public Shared Iterator Function IntDivideObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(IntDivideObject_InvalidObjects_TestData))>
    Public Sub IntDivideObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.IntDivideObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.IntDivideObject(right, left))
    End Sub

    Public Shared Iterator Function IntDivideObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New IntDivideObject, New Object}
      Yield New Object() {New Object, New IntDivideObject}

      Yield New Object() {New IntDivideObject, New IntDivideObject}
    End Function

    <Theory>
    <MemberData(NameOf(IntDivideObject_MismatchingObjects_TestData))>
    Public Sub IntDivideObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.IntDivideObject(left, right))
    End Sub

    Public Class IntDivideObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_IntegerDivision(left As IntDivideObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_IntegerDivision(left As Integer, right As IntDivideObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_IntegerDivision(left As IntDivideObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_IntegerDivision(left As OperatorsTests, right As IntDivideObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function LeftShiftObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(2), CByte(40)}
      Yield New Object() {CByte(10), CSByte(2), CByte(40)}
      Yield New Object() {CByte(10), CUShort(2), CByte(40)}
      Yield New Object() {CByte(10), CShort(Fix(2)), CByte(40)}
      Yield New Object() {CByte(10), CUInt(2), CByte(40)}
      Yield New Object() {CByte(10), 2, CByte(40)}
      Yield New Object() {CByte(10), CULng(2), CByte(40)}
      Yield New Object() {CByte(10), CLng(Fix(2)), CByte(40)}
      Yield New Object() {CByte(10), CSng(2), CByte(40)}
      Yield New Object() {CByte(10), CDbl(2), CByte(40)}
      Yield New Object() {CByte(10), CDec(2), CByte(40)}
      Yield New Object() {CByte(10), "2", CByte(40)}
      Yield New Object() {CByte(10), True, CByte(0)}
      Yield New Object() {CByte(10), Nothing, CByte(10)}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(2), CSByte(40)}
      Yield New Object() {CSByte(10), CSByte(2), CSByte(40)}
      Yield New Object() {CSByte(10), CUShort(2), CSByte(40)}
      Yield New Object() {CSByte(10), CShort(Fix(2)), CSByte(40)}
      Yield New Object() {CSByte(10), CUInt(2), CSByte(40)}
      Yield New Object() {CSByte(10), 2, CSByte(40)}
      Yield New Object() {CSByte(10), CULng(2), CSByte(40)}
      Yield New Object() {CSByte(10), CLng(Fix(2)), CSByte(40)}
      Yield New Object() {CSByte(10), CSng(2), CSByte(40)}
      Yield New Object() {CSByte(10), CDbl(2), CSByte(40)}
      Yield New Object() {CSByte(10), CDec(2), CSByte(40)}
      Yield New Object() {CSByte(10), "2", CSByte(40)}
      Yield New Object() {CSByte(10), True, CSByte(0)}
      Yield New Object() {CSByte(10), Nothing, CSByte(10)}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(2), CUShort(40)}
      Yield New Object() {CUShort(10), CSByte(2), CUShort(40)}
      Yield New Object() {CUShort(10), CUShort(2), CUShort(40)}
      Yield New Object() {CUShort(10), CShort(Fix(2)), CUShort(40)}
      Yield New Object() {CUShort(10), CUInt(2), CUShort(40)}
      Yield New Object() {CUShort(10), 2, CUShort(40)}
      Yield New Object() {CUShort(10), CULng(2), CUShort(40)}
      Yield New Object() {CUShort(10), CLng(Fix(2)), CUShort(40)}
      Yield New Object() {CUShort(10), CSng(2), CUShort(40)}
      Yield New Object() {CUShort(10), CDbl(2), CUShort(40)}
      Yield New Object() {CUShort(10), CDec(2), CUShort(40)}
      Yield New Object() {CUShort(10), "2", CUShort(40)}
      Yield New Object() {CUShort(10), True, CUShort(0)}
      Yield New Object() {CUShort(10), Nothing, CUShort(10)}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CSByte(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CUShort(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(2)), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CUInt(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), 2, CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CULng(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(2)), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CSng(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CDbl(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), CDec(2), CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), "2", CShort(Fix(40))}
      Yield New Object() {CShort(Fix(10)), True, CShort(Fix(0))}
      Yield New Object() {CShort(Fix(10)), Nothing, CShort(Fix(10))}

      ' uint.
      Yield New Object() {CUInt(10), CByte(2), CUInt(40)}
      Yield New Object() {CUInt(10), CSByte(2), CUInt(40)}
      Yield New Object() {CUInt(10), CUShort(2), CUInt(40)}
      Yield New Object() {CUInt(10), CShort(Fix(2)), CUInt(40)}
      Yield New Object() {CUInt(10), CUInt(2), CUInt(40)}
      Yield New Object() {CUInt(10), 2, CUInt(40)}
      Yield New Object() {CUInt(10), CULng(2), CUInt(40)}
      Yield New Object() {CUInt(10), CLng(Fix(2)), CUInt(40)}
      Yield New Object() {CUInt(10), CSng(2), CUInt(40)}
      Yield New Object() {CUInt(10), CDbl(2), CUInt(40)}
      Yield New Object() {CUInt(10), CDec(2), CUInt(40)}
      Yield New Object() {CUInt(10), "2", CUInt(40)}
      Yield New Object() {CUInt(10), True, CUInt(0)}
      Yield New Object() {CUInt(10), Nothing, CUInt(10)}

      ' int.
      Yield New Object() {10, CByte(2), 40}
      Yield New Object() {10, CSByte(2), 40}
      Yield New Object() {10, CUShort(2), 40}
      Yield New Object() {10, CShort(Fix(2)), 40}
      Yield New Object() {10, CUInt(2), 40}
      Yield New Object() {10, 2, 40}
      Yield New Object() {10, CULng(2), 40}
      Yield New Object() {10, CLng(Fix(2)), 40}
      Yield New Object() {10, CSng(2), 40}
      Yield New Object() {10, CDbl(2), 40}
      Yield New Object() {10, CDec(2), 40}
      Yield New Object() {10, "2", 40}
      Yield New Object() {10, True, 0}
      Yield New Object() {10, Nothing, 10}

      ' ulong.
      Yield New Object() {CULng(10), CByte(2), CULng(40)}
      Yield New Object() {CULng(10), CSByte(2), CULng(40)}
      Yield New Object() {CULng(10), CUShort(2), CULng(40)}
      Yield New Object() {CULng(10), CShort(Fix(2)), CULng(40)}
      Yield New Object() {CULng(10), CUInt(2), CULng(40)}
      Yield New Object() {CULng(10), 2, CULng(40)}
      Yield New Object() {CULng(10), CULng(2), CULng(40)}
      Yield New Object() {CULng(10), CLng(Fix(2)), CULng(40)}
      Yield New Object() {CULng(10), CSng(2), CULng(40)}
      Yield New Object() {CULng(10), CDbl(2), CULng(40)}
      Yield New Object() {CULng(10), CDec(2), CULng(40)}
      Yield New Object() {CULng(10), "2", CULng(40)}
      Yield New Object() {CULng(10), True, CULng(0)}
      Yield New Object() {CULng(10), Nothing, CULng(10)}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CSByte(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CUShort(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CUInt(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), 2, CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CULng(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CSng(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CDbl(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), CDec(2), CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), "2", CLng(Fix(40))}
      Yield New Object() {CLng(Fix(10)), True, CLng(Fix(0))}
      Yield New Object() {CLng(Fix(10)), Nothing, CLng(Fix(10))}

      ' float.
      Yield New Object() {CSng(10), CByte(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CSByte(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CUShort(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CShort(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CSng(10), CUInt(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), 2, CLng(Fix(40))}
      Yield New Object() {CSng(10), CULng(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CLng(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CSng(10), CSng(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CDbl(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), CDec(2), CLng(Fix(40))}
      Yield New Object() {CSng(10), "2", CLng(Fix(40))}
      Yield New Object() {CSng(10), True, CLng(Fix(0))}
      Yield New Object() {CSng(10), Nothing, CLng(Fix(10))}

      ' double.
      Yield New Object() {CDbl(10), CByte(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CSByte(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CUShort(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CShort(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CUInt(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), 2, CLng(Fix(40))}
      Yield New Object() {CDbl(10), CULng(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CLng(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CSng(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CDbl(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), CDec(2), CLng(Fix(40))}
      Yield New Object() {CDbl(10), "2", CLng(Fix(40))}
      Yield New Object() {CDbl(10), True, CLng(Fix(0))}
      Yield New Object() {CDbl(10), Nothing, CLng(Fix(10))}

      ' decimal.
      Yield New Object() {CDec(10), CByte(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CSByte(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CUShort(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CShort(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CDec(10), CUInt(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), 2, CLng(Fix(40))}
      Yield New Object() {CDec(10), CULng(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CLng(Fix(2)), CLng(Fix(40))}
      Yield New Object() {CDec(10), CSng(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CDbl(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), CDec(2), CLng(Fix(40))}
      Yield New Object() {CDec(10), "2", CLng(Fix(40))}
      Yield New Object() {CDec(10), True, CLng(Fix(0))}
      Yield New Object() {CDec(10), Nothing, CLng(Fix(10))}

      ' string.
      Yield New Object() {"10", CByte(2), CLng(Fix(40))}
      Yield New Object() {"10", CSByte(2), CLng(Fix(40))}
      Yield New Object() {"10", CUShort(2), CLng(Fix(40))}
      Yield New Object() {"10", CShort(Fix(2)), CLng(Fix(40))}
      Yield New Object() {"10", CUInt(2), CLng(Fix(40))}
      Yield New Object() {"10", 2, CLng(Fix(40))}
      Yield New Object() {"10", CULng(2), CLng(Fix(40))}
      Yield New Object() {"10", CLng(Fix(2)), CLng(Fix(40))}
      Yield New Object() {"10", CSng(2), CLng(Fix(40))}
      Yield New Object() {"10", CDbl(2), CLng(Fix(40))}
      Yield New Object() {"10", CDec(2), CLng(Fix(40))}
      Yield New Object() {"10", "2", CLng(Fix(40))}
      Yield New Object() {"10", True, CLng(Fix(0))}
      Yield New Object() {"10", Nothing, CLng(Fix(10))}

      ' bool.
      Yield New Object() {True, CByte(2), CShort(Fix(-4))}
      Yield New Object() {True, CSByte(2), CShort(Fix(-4))}
      Yield New Object() {True, CUShort(2), CShort(Fix(-4))}
      Yield New Object() {True, CShort(Fix(2)), CShort(Fix(-4))}
      Yield New Object() {True, CUInt(2), CShort(Fix(-4))}
      Yield New Object() {True, 2, CShort(Fix(-4))}
      Yield New Object() {True, CULng(2), CShort(Fix(-4))}
      Yield New Object() {True, CLng(Fix(2)), CShort(Fix(-4))}
      Yield New Object() {True, CSng(2), CShort(Fix(-4))}
      Yield New Object() {True, CDbl(2), CShort(Fix(-4))}
      Yield New Object() {True, CDec(2), CShort(Fix(-4))}
      Yield New Object() {True, "2", CShort(Fix(-4))}
      Yield New Object() {True, True, CShort(Fix(-32768))}
      Yield New Object() {True, Nothing, CShort(Fix(-1))}

      ' null.
      Yield New Object() {Nothing, CByte(2), 0}
      Yield New Object() {Nothing, CSByte(2), 0}
      Yield New Object() {Nothing, CUShort(2), 0}
      Yield New Object() {Nothing, CShort(Fix(2)), 0}
      Yield New Object() {Nothing, CUInt(2), 0}
      Yield New Object() {Nothing, 2, 0}
      Yield New Object() {Nothing, CULng(2), 0}
      Yield New Object() {Nothing, CLng(Fix(2)), 0}
      Yield New Object() {Nothing, CSng(2), 0}
      Yield New Object() {Nothing, CDbl(2), 0}
      Yield New Object() {Nothing, CDec(2), 0}
      Yield New Object() {Nothing, "2", 0}
      Yield New Object() {Nothing, True, 0}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New LeftShiftObject, 2, "custom"}
      Yield New Object() {2, New LeftShiftObject, "motsuc"}
      Yield New Object() {New LeftShiftObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New LeftShiftObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(LeftShiftObject_TestData))>
    Public Sub LeftShiftObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.LeftShiftObject(left, right))
    End Sub

    Public Shared Iterator Function LeftShiftObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(LeftShiftObject_InvalidObjects_TestData))>
    Public Sub LeftShiftObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.LeftShiftObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.LeftShiftObject(right, left))
    End Sub

    Public Shared Iterator Function LeftShiftObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New LeftShiftObject, New Object}
      Yield New Object() {New Object, New LeftShiftObject}

      Yield New Object() {New LeftShiftObject, New LeftShiftObject}
    End Function

    <Theory>
    <MemberData(NameOf(LeftShiftObject_MismatchingObjects_TestData))>
    Public Sub LeftShiftObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.LeftShiftObject(left, right))
    End Sub

    Public Class LeftShiftObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_LeftShift(left As LeftShiftObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_LeftShift(left As Integer, right As LeftShiftObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_LeftShift(left As LeftShiftObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_LeftShift(left As OperatorsTests, right As LeftShiftObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function MultiplyObject_Idempotent_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(1), CByte(2), CByte(2)}
      Yield New Object() {CByte(2), CSByte(2), CShort(Fix(4))}
      Yield New Object() {CByte(3), CUShort(2), CUShort(6)}
      Yield New Object() {CByte(4), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CByte(5), CUInt(2), CUInt(10)}
      Yield New Object() {CByte(6), 2, 12}
      Yield New Object() {CByte(7), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CByte(8), CULng(2), CULng(16)}
      Yield New Object() {CByte(9), CSng(2), CSng(18)}
      Yield New Object() {CByte(10), CDbl(2), CDbl(20)}
      Yield New Object() {CByte(11), CDec(2), CDec(22)}
      Yield New Object() {CByte(12), "2", CDbl(24)}
      Yield New Object() {CByte(13), True, CShort(Fix((-13)))}
      Yield New Object() {CByte(14), Nothing, CByte(0)}
      Yield New Object() {CByte(15), Byte.MaxValue, CShort(Fix(3825))}
      Yield New Object() {Byte.MaxValue, Byte.MaxValue, 65025}

      ' sbyte.
      Yield New Object() {CSByte(1), CByte(2), CShort(Fix(2))}
      Yield New Object() {CSByte(2), CSByte(2), CSByte(4)}
      Yield New Object() {CSByte(3), CUShort(2), 6}
      Yield New Object() {CSByte(4), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CSByte(5), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CSByte(6), 2, 12}
      Yield New Object() {CSByte(7), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CSByte(8), CSng(2), CSng(16)}
      Yield New Object() {CSByte(9), CULng(2), CDec(18)}
      Yield New Object() {CSByte(10), CDbl(2), CDbl(20)}
      Yield New Object() {CSByte(11), CDec(2), CDec(22)}
      Yield New Object() {CSByte(12), "2", CDbl(24)}
      Yield New Object() {CSByte(13), True, CSByte((-13))}
      Yield New Object() {CSByte(14), Nothing, CSByte(0)}
      Yield New Object() {CSByte(15), SByte.MaxValue, CShort(Fix(1905))}
      Yield New Object() {SByte.MaxValue, SByte.MaxValue, CShort(Fix(16129))}

      ' ushort.
      Yield New Object() {CUShort(1), CByte(2), CUShort(2)}
      Yield New Object() {CUShort(2), CSByte(2), 4}
      Yield New Object() {CUShort(3), CUShort(2), CUShort(6)}
      Yield New Object() {CUShort(4), CShort(Fix(2)), 8}
      Yield New Object() {CUShort(5), CUInt(2), CUInt(10)}
      Yield New Object() {CUShort(6), 2, 12}
      Yield New Object() {CUShort(7), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CUShort(8), CULng(2), CULng(16)}
      Yield New Object() {CUShort(9), CSng(2), CSng(18)}
      Yield New Object() {CUShort(10), CDbl(2), CDbl(20)}
      Yield New Object() {CUShort(11), CDec(2), CDec(22)}
      Yield New Object() {CUShort(12), "2", CDbl(24)}
      Yield New Object() {CUShort(13), True, -13}
      Yield New Object() {CUShort(14), Nothing, CUShort(0)}
      Yield New Object() {CUShort(15), UShort.MaxValue, 983025}
      Yield New Object() {UShort.MaxValue, UShort.MaxValue, CLng(Fix(4294836225))}

      ' short.
      Yield New Object() {CShort(Fix(1)), CByte(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(2)), CSByte(2), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(3)), CUShort(2), 6}
      Yield New Object() {CShort(Fix(4)), CShort(Fix(2)), CShort(Fix(8))}
      Yield New Object() {CShort(Fix(5)), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CShort(Fix(6)), 2, 12}
      Yield New Object() {CShort(Fix(7)), CLng(Fix(2)), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(8)), CULng(2), CDec(16)}
      Yield New Object() {CShort(Fix(9)), CSng(2), CSng(18)}
      Yield New Object() {CShort(Fix(10)), CDbl(2), CDbl(20)}
      Yield New Object() {CShort(Fix(11)), CDec(2), CDec(22)}
      Yield New Object() {CShort(Fix(12)), "2", CDbl(24)}
      Yield New Object() {CShort(Fix(13)), True, CShort(Fix((-13)))}
      Yield New Object() {CShort(Fix(14)), Nothing, CShort(Fix(0))}
      Yield New Object() {CShort(Fix(15)), Short.MaxValue, 491505}
      Yield New Object() {Short.MaxValue, Short.MaxValue, 1073676289}

      ' uint.
      Yield New Object() {CUInt(1), CByte(2), CUInt(2)}
      Yield New Object() {CUInt(2), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CUInt(3), CUShort(2), CUInt(6)}
      Yield New Object() {CUInt(4), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CUInt(5), CUInt(2), CUInt(10)}
      Yield New Object() {CUInt(6), 2, CLng(Fix(12))}
      Yield New Object() {CUInt(7), CULng(2), CULng(14)}
      Yield New Object() {CUInt(8), CLng(Fix(2)), CLng(Fix(16))}
      Yield New Object() {CUInt(9), CSng(2), CSng(18)}
      Yield New Object() {CUInt(10), CDbl(2), CDbl(20)}
      Yield New Object() {CUInt(11), CDec(2), CDec(22)}
      Yield New Object() {CUInt(12), "2", CDbl(24)}
      Yield New Object() {CUInt(13), True, CLng(Fix((-13)))}
      Yield New Object() {CUInt(14), Nothing, CUInt(0)}
      Yield New Object() {CUInt(15), UInteger.MaxValue, 64424509425}
      Yield New Object() {UInteger.MaxValue, UInteger.MaxValue, CDec(18446744065119617025UL)}

      ' int.
      Yield New Object() {1, CByte(2), 2}
      Yield New Object() {2, CSByte(2), 4}
      Yield New Object() {3, CUShort(2), 6}
      Yield New Object() {4, CShort(Fix(2)), 8}
      Yield New Object() {5, CUInt(2), CLng(Fix(10))}
      Yield New Object() {6, 2, 12}
      Yield New Object() {7, CULng(2), CDec(14)}
      Yield New Object() {8, CLng(Fix(2)), CLng(Fix(16))}
      Yield New Object() {9, CSng(2), CSng(18)}
      Yield New Object() {10, CDbl(2), CDbl(20)}
      Yield New Object() {11, CDec(2), CDec(22)}
      Yield New Object() {12, "2", CDbl(24)}
      Yield New Object() {13, True, -13}
      Yield New Object() {14, Nothing, 0}
      Yield New Object() {15, Integer.MaxValue, CLng(Fix(32212254705))}
      Yield New Object() {Integer.MaxValue, Integer.MaxValue, CLng(Fix(4611686014132420609))}

      ' ulong.
      Yield New Object() {CULng(1), CByte(2), CULng(2)}
      Yield New Object() {CULng(2), CSByte(2), CDec(4)}
      Yield New Object() {CULng(3), CUShort(2), CULng(6)}
      Yield New Object() {CULng(4), CShort(Fix(2)), CDec(8)}
      Yield New Object() {CULng(5), CUInt(2), CULng(10)}
      Yield New Object() {CULng(6), 2, CDec(12)}
      Yield New Object() {CULng(7), CULng(2), CULng(14)}
      Yield New Object() {CULng(8), CLng(Fix(2)), CDec(16)}
      Yield New Object() {CULng(9), CSng(2), CSng(18)}
      Yield New Object() {CULng(10), CDbl(2), CDbl(20)}
      Yield New Object() {CULng(11), CDec(2), CDec(22)}
      Yield New Object() {CULng(12), "2", CDbl(24)}
      Yield New Object() {CULng(13), True, CDec((-13))}
      Yield New Object() {CULng(14), Nothing, CULng(0)}
      Yield New Object() {CULng(15), ULong.MaxValue, Decimal.Parse("276701161105643274225", CultureInfo.InvariantCulture)}
      Yield New Object() {ULong.MaxValue, ULong.MaxValue, Double.Parse("3.4028236692093846E+38", NumberStyles.Any, CultureInfo.InvariantCulture)}

      ' long + primitives.
      Yield New Object() {CLng(Fix(1)), CByte(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(2)), CSByte(2), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(3)), CUShort(2), CLng(Fix(6))}
      Yield New Object() {CLng(Fix(4)), CShort(Fix(2)), CLng(Fix(8))}
      Yield New Object() {CLng(Fix(5)), CUInt(2), CLng(Fix(10))}
      Yield New Object() {CLng(Fix(6)), 2, CLng(Fix(12))}
      Yield New Object() {CLng(Fix(7)), CULng(2), CDec(14)}
      Yield New Object() {CLng(Fix(8)), CLng(Fix(2)), CLng(Fix(16))}
      Yield New Object() {CLng(Fix(9)), CSng(2), CSng(18)}
      Yield New Object() {CLng(Fix(10)), CDbl(2), CDbl(20)}
      Yield New Object() {CLng(Fix(11)), CDec(2), CDec(22)}
      Yield New Object() {CLng(Fix(12)), "2", CDbl(24)}
      Yield New Object() {CLng(Fix(13)), True, CLng(Fix((-13)))}
      Yield New Object() {CLng(Fix(14)), Nothing, CLng(Fix(0))}
      Yield New Object() {CLng(Fix(15)), Long.MaxValue, Decimal.Parse("138350580552821637105", CultureInfo.InvariantCulture)}
      Yield New Object() {Long.MaxValue, Long.MaxValue, Double.Parse("8.5070591730234616E+37", NumberStyles.Any, CultureInfo.InvariantCulture)}

      ' float + primitives
      Yield New Object() {CSng(1), CByte(2), CSng(2)}
      Yield New Object() {CSng(2), CSByte(2), CSng(4)}
      Yield New Object() {CSng(3), CUShort(2), CSng(6)}
      Yield New Object() {CSng(4), CShort(Fix(2)), CSng(8)}
      Yield New Object() {CSng(5), CUInt(2), CSng(10)}
      Yield New Object() {CSng(6), 2, CSng(12)}
      Yield New Object() {CSng(7), CULng(2), CSng(14)}
      Yield New Object() {CSng(8), CLng(Fix(2)), CSng(16)}
      Yield New Object() {CSng(9), CSng(2), CSng(18)}
      Yield New Object() {CSng(10), CDbl(2), CDbl(20)}
      Yield New Object() {CSng(11), CDec(2), CSng(22)}
      Yield New Object() {CSng(12), "2", CDbl(24)}
      Yield New Object() {CSng(13), True, CSng((-13))}
      Yield New Object() {CSng(14), Nothing, CSng(0)}
      Yield New Object() {CSng(15), Single.MaxValue, Double.Parse("5.1042351995779329E+39", NumberStyles.Any, CultureInfo.InvariantCulture)}
      Yield New Object() {Single.MaxValue, Single.MaxValue, Double.Parse("1.1579207543382391E+77", NumberStyles.Any, CultureInfo.InvariantCulture)}
      Yield New Object() {CSng(15), Single.PositiveInfinity, Single.PositiveInfinity}
      Yield New Object() {CSng(15), Single.NegativeInfinity, Single.NegativeInfinity}
      Yield New Object() {CSng(15), Single.NaN, Double.NaN}
      Yield New Object() {Single.PositiveInfinity, CSng(2), Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity, CSng(2), Single.NegativeInfinity}
      Yield New Object() {Single.NaN, CSng(2), Double.NaN}

      ' double.
      Yield New Object() {CDbl(1), CByte(2), CDbl(2)}
      Yield New Object() {CDbl(2), CSByte(2), CDbl(4)}
      Yield New Object() {CDbl(3), CUShort(2), CDbl(6)}
      Yield New Object() {CDbl(4), CShort(Fix(2)), CDbl(8)}
      Yield New Object() {CDbl(5), CUInt(2), CDbl(10)}
      Yield New Object() {CDbl(6), 2, CDbl(12)}
      Yield New Object() {CDbl(7), CULng(2), CDbl(14)}
      Yield New Object() {CDbl(8), CLng(Fix(2)), CDbl(16)}
      Yield New Object() {CDbl(9), CSng(2), CDbl(18)}
      Yield New Object() {CDbl(10), CDbl(2), CDbl(20)}
      Yield New Object() {CDbl(11), CDec(2), CDbl(22)}
      Yield New Object() {CDbl(12), "2", CDbl(24)}
      Yield New Object() {CDbl(13), True, CDbl((-13))}
      Yield New Object() {CDbl(14), Nothing, CDbl(0)}
      Yield New Object() {CDbl(15), Double.MaxValue, Double.PositiveInfinity}
      Yield New Object() {Double.MaxValue, Double.MaxValue, Double.PositiveInfinity}
      Yield New Object() {CDbl(15), Double.PositiveInfinity, Double.PositiveInfinity}
      Yield New Object() {CDbl(15), Double.NegativeInfinity, Double.NegativeInfinity}
      Yield New Object() {CDbl(15), Double.NaN, Double.NaN}
      Yield New Object() {Double.PositiveInfinity, CDbl(2), Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity, CDbl(2), Double.NegativeInfinity}
      Yield New Object() {Double.NaN, CDbl(2), Double.NaN}

      ' decimal.
      Yield New Object() {CDec(1), CByte(2), CDec(2)}
      Yield New Object() {CDec(2), CSByte(2), CDec(4)}
      Yield New Object() {CDec(3), CUShort(2), CDec(6)}
      Yield New Object() {CDec(4), CShort(Fix(2)), CDec(8)}
      Yield New Object() {CDec(5), CUInt(2), CDec(10)}
      Yield New Object() {CDec(6), 2, CDec(12)}
      Yield New Object() {CDec(7), CULng(2), CDec(14)}
      Yield New Object() {CDec(8), CLng(Fix(2)), CDec(16)}
      Yield New Object() {CDec(9), CSng(2), CSng(18)}
      Yield New Object() {CDec(10), CDbl(2), CDbl(20)}
      Yield New Object() {CDec(11), CDec(2), CDec(22)}
      Yield New Object() {CDec(12), "2", CDbl(24)}
      Yield New Object() {CDec(13), True, CDec((-13))}
      Yield New Object() {CDec(14), Nothing, CDec(0)}
      Yield New Object() {CDec(15), Decimal.MaxValue, Double.Parse("1.1884224377139651E+30", NumberStyles.Any, CultureInfo.InvariantCulture)}

      ' string + primitives
      Yield New Object() {"1", CByte(2), CDbl(2)}
      Yield New Object() {"2", CSByte(2), CDbl(4)}
      Yield New Object() {"3", CUShort(2), CDbl(6)}
      Yield New Object() {"4", CShort(Fix(2)), CDbl(8)}
      Yield New Object() {"5", CUInt(2), CDbl(10)}
      Yield New Object() {"6", 2, CDbl(12)}
      Yield New Object() {"7", CLng(Fix(2)), CDbl(14)}
      Yield New Object() {"8", CULng(2), CDbl(16)}
      Yield New Object() {"9", CSng(2), CDbl(18)}
      Yield New Object() {"10", CDbl(2), CDbl(20)}
      Yield New Object() {"11", CDec(2), CDbl(22)}
      Yield New Object() {"12", "2", CDbl(24)}
      Yield New Object() {"13", True, CDbl((-13))}
      Yield New Object() {"14", Nothing, CDbl(0)}

      ' bool.
      Yield New Object() {True, CByte(2), CShort(Fix((-2)))}
      Yield New Object() {True, CSByte(2), CSByte((-2))}
      Yield New Object() {True, CUShort(2), -2}
      Yield New Object() {True, CShort(Fix(2)), CShort(Fix((-2)))}
      Yield New Object() {True, CUInt(2), CLng(Fix((-2)))}
      Yield New Object() {True, 2, -2}
      Yield New Object() {True, CLng(Fix(2)), CLng(Fix((-2)))}
      Yield New Object() {True, CULng(2), CDec((-2))}
      Yield New Object() {True, CSng(2), CSng((-2))}
      Yield New Object() {True, CDbl(2), CDbl((-2))}
      Yield New Object() {True, CDec(2), CDec((-2))}
      Yield New Object() {True, "2", CDbl((-2))}
      Yield New Object() {True, False, CShort(Fix(0))}
      Yield New Object() {True, Nothing, CShort(Fix(0))}

      ' null.
      Yield New Object() {Nothing, CByte(2), CByte(0)}
      Yield New Object() {Nothing, CSByte(2), CSByte(0)}
      Yield New Object() {Nothing, CUShort(2), CUShort(0)}
      Yield New Object() {Nothing, CShort(Fix(2)), CShort(Fix(0))}
      Yield New Object() {Nothing, CUInt(2), CUInt(0)}
      Yield New Object() {Nothing, 2, 0}
      Yield New Object() {Nothing, CLng(Fix(2)), CLng(Fix(0))}
      Yield New Object() {Nothing, CULng(2), CULng(0)}
      Yield New Object() {Nothing, CSng(2), CSng(0)}
      Yield New Object() {Nothing, CDbl(2), CDbl(0)}
      Yield New Object() {Nothing, CDec(2), CDec(0)}
      Yield New Object() {Nothing, "2", CDbl(0)}
      Yield New Object() {Nothing, False, CShort(Fix(0))}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New MultiplyObject, 2, "custom"}
      Yield New Object() {2, New MultiplyObject, "motsuc"}
      Yield New Object() {New MultiplyObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New MultiplyObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(MultiplyObject_Idempotent_TestData))>
    Public Sub MultiplyObject_Convertible_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.MultiplyObject(left, right))
      Dim isString As Boolean = TypeOf expected Is String
      If isString Then
        Dim expectedString As String = expected.ToString
        Dim reversed As New String(expectedString.Reverse().ToArray())
        Assert.Equal(reversed, Operators.MultiplyObject(right, left))
      Else
        Assert.Equal(expected, Operators.MultiplyObject(right, left))
      End If
    End Sub

    Public Shared Iterator Function MultiplyObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(MultiplyObject_InvalidObjects_TestData))>
    Public Sub MultiplyObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.MultiplyObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.MultiplyObject(right, left))
    End Sub

    Public Shared Iterator Function MultiplyObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New MultiplyObject, New Object}
      Yield New Object() {New Object, New MultiplyObject}

      Yield New Object() {New MultiplyObject, New MultiplyObject}
    End Function

    <Theory>
    <MemberData(NameOf(MultiplyObject_MismatchingObjects_TestData))>
    Public Sub MultiplyObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.MultiplyObject(left, right))
    End Sub

    Public Class MultiplyObject

      Public Shared Operator *(left As MultiplyObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator *(left As Integer, right As MultiplyObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator *(left As MultiplyObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator *(left As OperatorsTests, right As MultiplyObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function ModObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(20), CByte(3), CByte(2)}
      Yield New Object() {CByte(20), CType(3, ByteEnum), CByte(2)}
      Yield New Object() {CByte(20), CSByte(3), CShort(Fix(2))}
      Yield New Object() {CByte(20), CType(3, SByteEnum), CShort(Fix(2))}
      Yield New Object() {CByte(20), CUShort(3), CUShort(2)}
      Yield New Object() {CByte(20), CType(3, UShortEnum), CUShort(2)}
      Yield New Object() {CByte(20), CShort(Fix(3)), CShort(Fix(2))}
      Yield New Object() {CByte(20), CType(3, ShortEnum), CShort(Fix(2))}
      Yield New Object() {CByte(20), CUInt(3), CUInt(2)}
      Yield New Object() {CByte(20), CType(3, UIntEnum), CUInt(2)}
      Yield New Object() {CByte(20), 3, 2}
      Yield New Object() {CByte(20), CType(3, IntEnum), 2}
      Yield New Object() {CByte(20), CULng(3), CULng(2)}
      Yield New Object() {CByte(20), CType(3, ULongEnum), CULng(2)}
      Yield New Object() {CByte(20), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CByte(20), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CByte(20), CSng(3), CSng(2)}
      Yield New Object() {CByte(20), CDbl(3), CDbl(2)}
      Yield New Object() {CByte(20), CDec(3), CDec(2)}
      Yield New Object() {CByte(20), "3", CDbl(2)}
      Yield New Object() {CByte(20), True, CShort(Fix(0))}

      ' sbyte.
      Yield New Object() {CSByte(20), CByte(3), CShort(Fix(2))}
      Yield New Object() {CSByte(20), CType(3, ByteEnum), CShort(Fix(2))}
      Yield New Object() {CSByte(20), CSByte(3), CSByte(2)}
      Yield New Object() {CSByte(20), CType(3, SByteEnum), CSByte(2)}
      Yield New Object() {CSByte(20), CUShort(3), 2}
      Yield New Object() {CSByte(20), CType(3, UShortEnum), 2}
      Yield New Object() {CSByte(20), CShort(Fix(3)), CShort(Fix(2))}
      Yield New Object() {CSByte(20), CType(3, ShortEnum), CShort(Fix(2))}
      Yield New Object() {CSByte(20), CUInt(3), CLng(Fix(2))}
      Yield New Object() {CSByte(20), CType(3, UIntEnum), CLng(Fix(2))}
      Yield New Object() {CSByte(20), 3, 2}
      Yield New Object() {CSByte(20), CType(3, IntEnum), 2}
      Yield New Object() {CSByte(20), CULng(3), CDec(2)}
      Yield New Object() {CSByte(20), CType(3, ULongEnum), CDec(2)}
      Yield New Object() {CSByte(20), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CSByte(20), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CSByte(20), CSng(3), CSng(2)}
      Yield New Object() {CSByte(20), CDbl(3), CDbl(2)}
      Yield New Object() {CSByte(20), CDec(3), CDec(2)}
      Yield New Object() {CSByte(20), "3", CDbl(2)}
      Yield New Object() {CSByte(20), True, CSByte(0)}
      Yield New Object() {CSByte(20), CSByte((-1)), CSByte(0)}
      Yield New Object() {CSByte(0), CSByte(1), CSByte(0)}
      Yield New Object() {CSByte(0), CSByte((-1)), CSByte(0)}
      Yield New Object() {CSByte((-20)), CSByte(1), CSByte(0)}
      Yield New Object() {CSByte((-20)), CSByte((-1)), CSByte(0)}
      Yield New Object() {SByte.MaxValue, SByte.MinValue, SByte.MaxValue}
      Yield New Object() {SByte.MaxValue, SByte.MinValue, SByte.MaxValue}
      Yield New Object() {SByte.MaxValue, CSByte((-1)), CSByte(0)}
      Yield New Object() {SByte.MinValue, SByte.MinValue, CSByte(0)}
      Yield New Object() {SByte.MinValue, SByte.MinValue, CSByte(0)}
      Yield New Object() {SByte.MinValue, CSByte((-1)), CSByte(0)}

      ' ushort.
      Yield New Object() {CUShort(20), CByte(3), CUShort(2)}
      Yield New Object() {CUShort(20), CType(3, ByteEnum), CUShort(2)}
      Yield New Object() {CUShort(20), CSByte(3), 2}
      Yield New Object() {CUShort(20), CType(3, SByteEnum), 2}
      Yield New Object() {CUShort(20), CUShort(3), CUShort(2)}
      Yield New Object() {CUShort(20), CType(3, UShortEnum), CUShort(2)}
      Yield New Object() {CUShort(20), CShort(Fix(3)), 2}
      Yield New Object() {CUShort(20), CType(3, ShortEnum), 2}
      Yield New Object() {CUShort(20), CUInt(3), CUInt(2)}
      Yield New Object() {CUShort(20), CType(3, UIntEnum), CUInt(2)}
      Yield New Object() {CUShort(20), 3, 2}
      Yield New Object() {CUShort(20), CType(3, IntEnum), 2}
      Yield New Object() {CUShort(20), CULng(3), CULng(2)}
      Yield New Object() {CUShort(20), CType(3, ULongEnum), CULng(2)}
      Yield New Object() {CUShort(20), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CUShort(20), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CUShort(20), CSng(3), CSng(2)}
      Yield New Object() {CUShort(20), CDbl(3), CDbl(2)}
      Yield New Object() {CUShort(20), CDec(3), CDec(2)}
      Yield New Object() {CUShort(20), "3", CDbl(2)}
      Yield New Object() {CUShort(20), True, 0}

      ' short.
      Yield New Object() {CShort(Fix(20)), CByte(3), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CType(3, ByteEnum), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CSByte(3), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CType(3, SByteEnum), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CUShort(3), 2}
      Yield New Object() {CShort(Fix(20)), CType(3, UShortEnum), 2}
      Yield New Object() {CShort(Fix(20)), CShort(Fix(3)), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CType(3, ShortEnum), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CUInt(3), CLng(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CType(3, UIntEnum), CLng(Fix(2))}
      Yield New Object() {CShort(Fix(20)), 3, 2}
      Yield New Object() {CShort(Fix(20)), CType(3, IntEnum), 2}
      Yield New Object() {CShort(Fix(20)), CULng(3), CDec(2)}
      Yield New Object() {CShort(Fix(20)), CType(3, ULongEnum), CDec(2)}
      Yield New Object() {CShort(Fix(20)), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CShort(Fix(20)), CSng(3), CSng(2)}
      Yield New Object() {CShort(Fix(20)), CDbl(3), CDbl(2)}
      Yield New Object() {CShort(Fix(20)), CDec(3), CDec(2)}
      Yield New Object() {CShort(Fix(20)), "3", CDbl(2)}
      Yield New Object() {CShort(Fix(20)), True, CShort(Fix(0))}
      Yield New Object() {CShort(Fix(20)), CShort(Fix((-1))), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(0)), CShort(Fix(1)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(0)), CShort(Fix((-1))), CShort(Fix(0))}
      Yield New Object() {CShort(Fix((-20))), CShort(Fix(1)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix((-20))), CShort(Fix((-1))), CShort(Fix(0))}
      Yield New Object() {Short.MaxValue, Short.MinValue, Short.MaxValue}
      Yield New Object() {Short.MaxValue, Short.MinValue, Short.MaxValue}
      Yield New Object() {Short.MaxValue, CShort(Fix((-1))), CShort(Fix(0))}
      Yield New Object() {Short.MinValue, Short.MinValue, CShort(Fix(0))}
      Yield New Object() {Short.MinValue, Short.MinValue, CShort(Fix(0))}
      Yield New Object() {Short.MinValue, CShort(Fix((-1))), CShort(Fix(0))}

      ' uint.
      Yield New Object() {CUInt(20), CByte(3), CUInt(2)}
      Yield New Object() {CUInt(20), CType(3, ByteEnum), CUInt(2)}
      Yield New Object() {CUInt(20), CSByte(3), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CType(3, SByteEnum), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CUShort(3), CUInt(2)}
      Yield New Object() {CUInt(20), CType(3, UShortEnum), CUInt(2)}
      Yield New Object() {CUInt(20), CShort(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CType(3, ShortEnum), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CUInt(3), CUInt(2)}
      Yield New Object() {CUInt(20), CType(3, UIntEnum), CUInt(2)}
      Yield New Object() {CUInt(20), 3, CLng(Fix(2))}
      Yield New Object() {CUInt(20), CType(3, IntEnum), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CULng(3), CULng(2)}
      Yield New Object() {CUInt(20), CType(3, ULongEnum), CULng(2)}
      Yield New Object() {CUInt(20), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CUInt(20), CSng(3), CSng(2)}
      Yield New Object() {CUInt(20), CDbl(3), CDbl(2)}
      Yield New Object() {CUInt(20), CDec(3), CDec(2)}
      Yield New Object() {CUInt(20), "3", CDbl(2)}
      Yield New Object() {CUInt(20), True, CLng(Fix(0))}

      ' int.
      Yield New Object() {20, CByte(3), 2}
      Yield New Object() {20, CType(3, ByteEnum), 2}
      Yield New Object() {20, CSByte(3), 2}
      Yield New Object() {20, CType(3, SByteEnum), 2}
      Yield New Object() {20, CUShort(3), 2}
      Yield New Object() {20, CType(3, UShortEnum), 2}
      Yield New Object() {20, CShort(Fix(3)), 2}
      Yield New Object() {20, CType(3, ShortEnum), 2}
      Yield New Object() {20, CUInt(3), CLng(Fix(2))}
      Yield New Object() {20, CType(3, UIntEnum), CLng(Fix(2))}
      Yield New Object() {20, 3, 2}
      Yield New Object() {20, CType(3, IntEnum), 2}
      Yield New Object() {20, CULng(3), CDec(2)}
      Yield New Object() {20, CType(3, ULongEnum), CDec(2)}
      Yield New Object() {20, CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {20, CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {20, CSng(3), CSng(2)}
      Yield New Object() {20, CDbl(3), CDbl(2)}
      Yield New Object() {20, CDec(3), CDec(2)}
      Yield New Object() {20, "3", CDbl(2)}
      Yield New Object() {20, True, 0}
      Yield New Object() {20, -1, 0}
      Yield New Object() {0, 1, 0}
      Yield New Object() {0, -1, 0}
      Yield New Object() {-20, 1, 0}
      Yield New Object() {-20, -1, 0}
      Yield New Object() {Integer.MaxValue, Integer.MinValue, Integer.MaxValue}
      Yield New Object() {Integer.MaxValue, Integer.MinValue, Integer.MaxValue}
      Yield New Object() {Integer.MaxValue, -1, 0}
      Yield New Object() {Integer.MinValue, Integer.MinValue, 0}
      Yield New Object() {Integer.MinValue, Integer.MinValue, 0}
      Yield New Object() {Integer.MinValue, -1, 0}

      ' ulong.
      Yield New Object() {CULng(20), CByte(3), CULng(2)}
      Yield New Object() {CULng(20), CType(3, ByteEnum), CULng(2)}
      Yield New Object() {CULng(20), CSByte(3), CDec(2)}
      Yield New Object() {CULng(20), CType(3, SByteEnum), CDec(2)}
      Yield New Object() {CULng(20), CUShort(3), CULng(2)}
      Yield New Object() {CULng(20), CType(3, UShortEnum), CULng(2)}
      Yield New Object() {CULng(20), CShort(Fix(3)), CDec(2)}
      Yield New Object() {CULng(20), CType(3, ShortEnum), CDec(2)}
      Yield New Object() {CULng(20), CUInt(3), CULng(2)}
      Yield New Object() {CULng(20), CType(3, UIntEnum), CULng(2)}
      Yield New Object() {CULng(20), 3, CDec(2)}
      Yield New Object() {CULng(20), CType(3, IntEnum), CDec(2)}
      Yield New Object() {CULng(20), CULng(3), CULng(2)}
      Yield New Object() {CULng(20), CType(3, ULongEnum), CULng(2)}
      Yield New Object() {CULng(20), CLng(Fix(3)), CDec(2)}
      Yield New Object() {CULng(20), CType(3, LongEnum), CDec(2)}
      Yield New Object() {CULng(20), CSng(3), CSng(2)}
      Yield New Object() {CULng(20), CDbl(3), CDbl(2)}
      Yield New Object() {CULng(20), CDec(3), CDec(2)}
      Yield New Object() {CULng(20), "3", CDbl(2)}
      Yield New Object() {CULng(20), True, CDec(0)}

      ' long.
      Yield New Object() {CLng(Fix(20)), CByte(3), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, ByteEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CSByte(3), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, SByteEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CUShort(3), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, UShortEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CShort(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, ShortEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CUInt(3), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, UIntEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), 3, CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, IntEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CULng(3), CDec(2)}
      Yield New Object() {CLng(Fix(20)), CType(3, ULongEnum), CDec(2)}
      Yield New Object() {CLng(Fix(20)), CLng(Fix(3)), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CType(3, LongEnum), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(20)), CSng(3), CSng(2)}
      Yield New Object() {CLng(Fix(20)), CDbl(3), CDbl(2)}
      Yield New Object() {CLng(Fix(20)), CDec(3), CDec(2)}
      Yield New Object() {CLng(Fix(20)), "3", CDbl(2)}
      Yield New Object() {CLng(Fix(20)), True, CLng(Fix(0))}
      Yield New Object() {CLng(Fix(20)), CLng(Fix((-1))), CLng(Fix(0))}
      Yield New Object() {CLng(Fix(0)), CLng(Fix(1)), CLng(Fix(0))}
      Yield New Object() {CLng(Fix(0)), CLng(Fix((-1))), CLng(Fix(0))}
      Yield New Object() {CLng(Fix((-20))), CLng(Fix(1)), CLng(Fix(0))}
      Yield New Object() {CLng(Fix((-20))), CLng(Fix((-1))), CLng(Fix(0))}
      Yield New Object() {Long.MaxValue, Long.MinValue, Long.MaxValue}
      Yield New Object() {Long.MaxValue, Long.MinValue, Long.MaxValue}
      Yield New Object() {Long.MaxValue, CLng(Fix((-1))), CLng(Fix(0))}
      Yield New Object() {Long.MinValue, Long.MinValue, CLng(Fix(0))}
      Yield New Object() {Long.MinValue, Long.MinValue, CLng(Fix(0))}
      Yield New Object() {Long.MinValue, CLng(Fix((-1))), CLng(Fix(0))}

      ' float.
      Yield New Object() {CSng(20), CByte(3), CSng(2)}
      Yield New Object() {CSng(20), CType(3, ByteEnum), CSng(2)}
      Yield New Object() {CSng(20), CSByte(3), CSng(2)}
      Yield New Object() {CSng(20), CType(3, SByteEnum), CSng(2)}
      Yield New Object() {CSng(20), CUShort(3), CSng(2)}
      Yield New Object() {CSng(20), CType(3, UShortEnum), CSng(2)}
      Yield New Object() {CSng(20), CShort(Fix(3)), CSng(2)}
      Yield New Object() {CSng(20), CType(3, ShortEnum), CSng(2)}
      Yield New Object() {CSng(20), CUInt(3), CSng(2)}
      Yield New Object() {CSng(20), CType(3, UIntEnum), CSng(2)}
      Yield New Object() {CSng(20), 3, CSng(2)}
      Yield New Object() {CSng(20), CType(3, IntEnum), CSng(2)}
      Yield New Object() {CSng(20), CULng(3), CSng(2)}
      Yield New Object() {CSng(20), CType(3, ULongEnum), CSng(2)}
      Yield New Object() {CSng(20), CLng(Fix(3)), CSng(2)}
      Yield New Object() {CSng(20), CType(3, LongEnum), CSng(2)}
      Yield New Object() {CSng(20), CSng(3), CSng(2)}
      Yield New Object() {CSng(20), CDbl(3), CDbl(2)}
      Yield New Object() {CSng(20), CDec(3), CSng(2)}
      Yield New Object() {CSng(20), "3", CDbl(2)}
      Yield New Object() {CSng(20), True, CSng(0)}
      Yield New Object() {CSng(20), False, Single.NaN}
      Yield New Object() {CSng(20), Nothing, Single.NaN}
      Yield New Object() {CSng(20), CSng(0), Single.NaN}
      Yield New Object() {CSng(20), CSng((-1)), CSng(0)}
      Yield New Object() {CSng(0), CSng(1), CSng(0)}
      Yield New Object() {CSng(0), CSng(0), Single.NaN}
      Yield New Object() {CSng(0), CSng((-1)), CSng(0)}
      Yield New Object() {CSng((-20)), CSng(1), CSng((-0.0F))}
      Yield New Object() {CSng((-20)), CSng(0), Single.NaN}
      Yield New Object() {CSng((-20)), CSng((-1)), CSng((-0.0F))}
      Yield New Object() {Single.MaxValue, Single.MaxValue, CSng(0)}
      Yield New Object() {Single.MaxValue, Single.MinValue, CSng(0)}
      Yield New Object() {Single.MaxValue, CSng(0), Single.NaN}
      Yield New Object() {Single.MaxValue, CSng((-1)), CSng(0)}
      Yield New Object() {Single.MinValue, Single.MaxValue, CSng((-0.0F))}
      Yield New Object() {Single.MinValue, Single.MinValue, CSng((-0.0F))}
      Yield New Object() {Single.MinValue, CSng(0), Single.NaN}
      Yield New Object() {Single.MinValue, CSng((-1)), CSng(0)}

      ' double.
      Yield New Object() {CDbl(20), CByte(3), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, ByteEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CSByte(3), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, SByteEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CUShort(3), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, UShortEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CShort(Fix(3)), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, ShortEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CUInt(3), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, UIntEnum), CDbl(2)}
      Yield New Object() {CDbl(20), 3, CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, IntEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CULng(3), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, ULongEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CLng(Fix(3)), CDbl(2)}
      Yield New Object() {CDbl(20), CType(3, LongEnum), CDbl(2)}
      Yield New Object() {CDbl(20), CSng(3), CDbl(2)}
      Yield New Object() {CDbl(20), CDbl(3), CDbl(2)}
      Yield New Object() {CDbl(20), CDec(3), CDbl(2)}
      Yield New Object() {CDbl(20), "3", CDbl(2)}
      Yield New Object() {CDbl(20), True, CDbl(0)}
      Yield New Object() {CDbl(20), False, Double.NaN}
      Yield New Object() {CDbl(20), Nothing, Double.NaN}
      Yield New Object() {CDbl(20), CDbl(0), Double.NaN}
      Yield New Object() {CDbl(20), CDbl((-1)), CDbl(0)}
      Yield New Object() {CDbl(0), CDbl(1), CDbl(0)}
      Yield New Object() {CDbl(0), CDbl(0), Double.NaN}
      Yield New Object() {CDbl(0), CDbl((-1)), CDbl(0)}
      Yield New Object() {CDbl((-20)), CDbl(1), CDbl((-0.0))}
      Yield New Object() {CDbl((-20)), CDbl(0), Double.NaN}
      Yield New Object() {CDbl((-20)), CDbl((-1)), CDbl((-0.0))}
      Yield New Object() {Double.MaxValue, Double.MaxValue, CDbl(0)}
      Yield New Object() {Double.MaxValue, Double.MinValue, CDbl(0)}
      Yield New Object() {Double.MaxValue, CDbl(0), Double.NaN}
      Yield New Object() {Double.MaxValue, CDbl((-1)), CDbl(0)}
      Yield New Object() {Double.MinValue, Double.MaxValue, CDbl((-0.0))}
      Yield New Object() {Double.MinValue, Double.MinValue, CDbl((-0.0))}
      Yield New Object() {Double.MinValue, CDbl(0), Double.NaN}
      Yield New Object() {Double.MinValue, CDbl((-1)), CDbl((-0.0))}

      ' decimal.
      Yield New Object() {CDec(20), CByte(3), CDec(2)}
      Yield New Object() {CDec(20), CType(3, ByteEnum), CDec(2)}
      Yield New Object() {CDec(20), CSByte(3), CDec(2)}
      Yield New Object() {CDec(20), CType(3, SByteEnum), CDec(2)}
      Yield New Object() {CDec(20), CUShort(3), CDec(2)}
      Yield New Object() {CDec(20), CType(3, UShortEnum), CDec(2)}
      Yield New Object() {CDec(20), CShort(Fix(3)), CDec(2)}
      Yield New Object() {CDec(20), CType(3, ShortEnum), CDec(2)}
      Yield New Object() {CDec(20), CUInt(3), CDec(2)}
      Yield New Object() {CDec(20), CType(3, UIntEnum), CDec(2)}
      Yield New Object() {CDec(20), 3, CDec(2)}
      Yield New Object() {CDec(20), CType(3, IntEnum), CDec(2)}
      Yield New Object() {CDec(20), CULng(3), CDec(2)}
      Yield New Object() {CDec(20), CType(3, ULongEnum), CDec(2)}
      Yield New Object() {CDec(20), CLng(Fix(3)), CDec(2)}
      Yield New Object() {CDec(20), CType(3, LongEnum), CDec(2)}
      Yield New Object() {CDec(20), CSng(3), CSng(2)}
      Yield New Object() {CDec(20), CDbl(3), CDbl(2)}
      Yield New Object() {CDec(20), CDec(3), CDec(2)}
      Yield New Object() {CDec(20), "3", CDbl(2)}
      Yield New Object() {CDec(20), True, CDec(0)}
      Yield New Object() {CDec(20), CDec((-1)), CDec(0)}
      Yield New Object() {CDec(0), CDec(1), CDec(0)}
      Yield New Object() {CDec(0), CDec((-1)), CDec(0)}
      Yield New Object() {CDec((-20)), CDec(1), CDec(0)}
      Yield New Object() {CDec((-20)), CDec((-1)), CDec(0)}
      Yield New Object() {Decimal.MaxValue, Decimal.MinValue, CDec(0)}
      Yield New Object() {Decimal.MaxValue, Decimal.MinValue, CDec(0)}
      Yield New Object() {Decimal.MaxValue, CDec((-1)), CDec(0)}
      Yield New Object() {Decimal.MinValue, Decimal.MinValue, CDec(0)}
      Yield New Object() {Decimal.MinValue, Decimal.MinValue, CDec(0)}
      Yield New Object() {Decimal.MinValue, CDec((-1)), CDec(0)}

      ' string.
      Yield New Object() {"20", CByte(3), CDbl(2)}
      Yield New Object() {"20", CType(3, ByteEnum), CDbl(2)}
      Yield New Object() {"20", CSByte(3), CDbl(2)}
      Yield New Object() {"20", CType(3, SByteEnum), CDbl(2)}
      Yield New Object() {"20", CUShort(3), CDbl(2)}
      Yield New Object() {"20", CType(3, UShortEnum), CDbl(2)}
      Yield New Object() {"20", CShort(Fix(3)), CDbl(2)}
      Yield New Object() {"20", CType(3, ShortEnum), CDbl(2)}
      Yield New Object() {"20", CUInt(3), CDbl(2)}
      Yield New Object() {"20", CType(3, UIntEnum), CDbl(2)}
      Yield New Object() {"20", 3, CDbl(2)}
      Yield New Object() {"20", CType(3, IntEnum), CDbl(2)}
      Yield New Object() {"20", CULng(3), CDbl(2)}
      Yield New Object() {"20", CType(3, ULongEnum), CDbl(2)}
      Yield New Object() {"20", CLng(Fix(3)), CDbl(2)}
      Yield New Object() {"20", CType(3, LongEnum), CDbl(2)}
      Yield New Object() {"20", CSng(3), CDbl(2)}
      Yield New Object() {"20", CDbl(3), CDbl(2)}
      Yield New Object() {"20", CDec(3), CDbl(2)}
      Yield New Object() {"20", "3", CDbl(2)}
      Yield New Object() {"20", True, CDbl(0)}
      Yield New Object() {"20", False, Double.NaN}
      Yield New Object() {"20", Nothing, Double.NaN}

      ' bool.
      Yield New Object() {True, CByte(3), CShort(Fix((-1)))}
      Yield New Object() {True, CType(3, ByteEnum), CShort(Fix((-1)))}
      Yield New Object() {True, CSByte(3), CSByte((-1))}
      Yield New Object() {True, CType(3, SByteEnum), CSByte((-1))}
      Yield New Object() {True, CUShort(3), -1}
      Yield New Object() {True, CType(3, UShortEnum), -1}
      Yield New Object() {True, CShort(Fix(3)), CShort(Fix((-1)))}
      Yield New Object() {True, CType(3, ShortEnum), CShort(Fix((-1)))}
      Yield New Object() {True, CUInt(3), CLng(Fix((-1)))}
      Yield New Object() {True, CType(3, UIntEnum), CLng(Fix((-1)))}
      Yield New Object() {True, 3, -1}
      Yield New Object() {True, CType(3, IntEnum), -1}
      Yield New Object() {True, CULng(3), CDec((-1))}
      Yield New Object() {True, CType(3, ULongEnum), CDec((-1))}
      Yield New Object() {True, CLng(Fix(3)), CLng(Fix((-1)))}
      Yield New Object() {True, CType(3, LongEnum), CLng(Fix((-1)))}
      Yield New Object() {True, CSng(3), CSng((-1))}
      Yield New Object() {True, CDbl(3), CDbl((-1))}
      Yield New Object() {True, CDec(3), CDec((-1))}
      Yield New Object() {True, "3", CDbl((-1))}
      Yield New Object() {True, True, CShort(Fix(0))}

      ' null.
      Yield New Object() {Nothing, CByte(3), Byte.MinValue}
      Yield New Object() {Nothing, CType(3, ByteEnum), Byte.MinValue}
      Yield New Object() {Nothing, CSByte(3), CSByte(0)}
      Yield New Object() {Nothing, CType(3, SByteEnum), CSByte(0)}
      Yield New Object() {Nothing, CUShort(3), UShort.MinValue}
      Yield New Object() {Nothing, CType(3, UShortEnum), UShort.MinValue}
      Yield New Object() {Nothing, CShort(Fix(3)), CShort(Fix(0))}
      Yield New Object() {Nothing, CType(3, ShortEnum), CShort(Fix(0))}
      Yield New Object() {Nothing, CUInt(3), UInteger.MinValue}
      Yield New Object() {Nothing, CType(3, UIntEnum), UInteger.MinValue}
      Yield New Object() {Nothing, 3, 0}
      Yield New Object() {Nothing, CType(3, IntEnum), 0}
      Yield New Object() {Nothing, CULng(3), ULong.MinValue}
      Yield New Object() {Nothing, CType(3, ULongEnum), ULong.MinValue}
      Yield New Object() {Nothing, CLng(Fix(3)), CLng(Fix(0))}
      Yield New Object() {Nothing, CType(3, LongEnum), CLng(Fix(0))}
      Yield New Object() {Nothing, CSng(3), CSng(0)}
      Yield New Object() {Nothing, CDbl(3), CDbl(0)}
      Yield New Object() {Nothing, CDec(3), CDec(0)}
      Yield New Object() {Nothing, "3", CDbl(0)}
      Yield New Object() {Nothing, True, CShort(Fix(0))}

      ' object.
      Yield New Object() {New ModObject, 2, "custom"}
      Yield New Object() {2, New ModObject, "motsuc"}
      Yield New Object() {New ModObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New ModObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(ModObject_TestData))>
    Public Sub ModObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ModObject(left, right))
    End Sub

    Public Shared Iterator Function ModObject_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {CByte(20), New Char() {"3"c}}
      Yield New Object() {CByte(20), "3"c}
      Yield New Object() {CByte(20), New DateTime(3)}
      Yield New Object() {CSByte(20), New Char() {"3"c}}
      Yield New Object() {CSByte(20), "3"c}
      Yield New Object() {CSByte(20), New DateTime(3)}
      Yield New Object() {CUShort(20), New Char() {"3"c}}
      Yield New Object() {CUShort(20), "3"c}
      Yield New Object() {CUShort(20), New DateTime(3)}
      Yield New Object() {CShort(Fix(20)), New Char() {"3"c}}
      Yield New Object() {CShort(Fix(20)), "3"c}
      Yield New Object() {CShort(Fix(20)), New DateTime(3)}
      Yield New Object() {CUInt(20), New Char() {"3"c}}
      Yield New Object() {CUInt(20), "3"c}
      Yield New Object() {CUInt(20), New DateTime(3)}
      Yield New Object() {20, New Char() {"3"c}}
      Yield New Object() {20, "3"c}
      Yield New Object() {20, New DateTime(3)}
      Yield New Object() {CULng(20), New Char() {"3"c}}
      Yield New Object() {CULng(20), "3"c}
      Yield New Object() {CULng(20), New DateTime(3)}
      Yield New Object() {CLng(Fix(20)), New Char() {"3"c}}
      Yield New Object() {CLng(Fix(20)), "3"c}
      Yield New Object() {CLng(Fix(20)), New DateTime(3)}
      Yield New Object() {CSng(20), New Char() {"3"c}}
      Yield New Object() {CSng(20), "3"c}
      Yield New Object() {CSng(20), New DateTime(3)}
      Yield New Object() {CDbl(20), New Char() {"3"c}}
      Yield New Object() {CDbl(20), "3"c}
      Yield New Object() {CDbl(20), New DateTime(3)}
      Yield New Object() {CDec(20), New Char() {"3"c}}
      Yield New Object() {CDec(20), "3"c}
      Yield New Object() {CDec(20), New DateTime(3)}
      Yield New Object() {"20", New Char() {"3"c}}
      Yield New Object() {"20", "3"c}
      Yield New Object() {"20", New DateTime(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CByte(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, ByteEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CSByte(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, SByteEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CUShort(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, UShortEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CShort(Fix(3))}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, ShortEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CUInt(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, UIntEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, 3}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, IntEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CULng(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, ULongEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CLng(Fix(3))}
      Yield New Object() {New Char() {"2"c, "0"c}, CType(3, LongEnum)}
      Yield New Object() {New Char() {"2"c, "0"c}, CSng(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CDbl(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, CDec(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, "3"}
      Yield New Object() {New Char() {"2"c, "0"c}, New Char() {"3"c}}
      Yield New Object() {New Char() {"2"c, "0"c}, True}
      Yield New Object() {New Char() {"2"c, "0"c}, False}
      Yield New Object() {New Char() {"2"c, "0"c}, "3"c}
      Yield New Object() {New Char() {"2"c, "0"c}, New DateTime(3)}
      Yield New Object() {New Char() {"2"c, "0"c}, Nothing}
      Yield New Object() {True, New Char() {"3"c}}
      Yield New Object() {True, "3"c}
      Yield New Object() {True, New DateTime(3)}
      Yield New Object() {New DateTime(20), CByte(3)}
      Yield New Object() {New DateTime(20), CType(3, ByteEnum)}
      Yield New Object() {New DateTime(20), CSByte(3)}
      Yield New Object() {New DateTime(20), CType(3, SByteEnum)}
      Yield New Object() {New DateTime(20), CUShort(3)}
      Yield New Object() {New DateTime(20), CType(3, UShortEnum)}
      Yield New Object() {New DateTime(20), CShort(Fix(3))}
      Yield New Object() {New DateTime(20), CType(3, ShortEnum)}
      Yield New Object() {New DateTime(20), CUInt(3)}
      Yield New Object() {New DateTime(20), CType(3, UIntEnum)}
      Yield New Object() {New DateTime(20), 3}
      Yield New Object() {New DateTime(20), CType(3, IntEnum)}
      Yield New Object() {New DateTime(20), CULng(3)}
      Yield New Object() {New DateTime(20), CType(3, ULongEnum)}
      Yield New Object() {New DateTime(20), CLng(Fix(3))}
      Yield New Object() {New DateTime(20), CType(3, LongEnum)}
      Yield New Object() {New DateTime(20), CSng(3)}
      Yield New Object() {New DateTime(20), CDbl(3)}
      Yield New Object() {New DateTime(20), CDec(3)}
      Yield New Object() {New DateTime(20), "3"}
      Yield New Object() {New DateTime(20), New Char() {"3"c}}
      Yield New Object() {New DateTime(20), True}
      Yield New Object() {New DateTime(20), False}
      Yield New Object() {New DateTime(20), "3"c}
      Yield New Object() {New DateTime(20), New DateTime(3)}
      Yield New Object() {New DateTime(20), Nothing}
      Yield New Object() {Nothing, New Char() {"3"c}}
      Yield New Object() {Nothing, "3"c}
      Yield New Object() {Nothing, New DateTime(3)}
    End Function

    <Theory>
    <MemberData(NameOf(ModObject_InvalidObject_TestData))>
    Public Sub ModObject_InvalidObject_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ModObject(left, right))
    End Sub

    Public Shared Iterator Function ModObject_DivideByZeroObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {CByte(20), False}
      Yield New Object() {CByte(20), Nothing}
      Yield New Object() {CByte(20), Byte.MinValue}
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {Byte.MaxValue, Byte.MinValue}
      Yield New Object() {Byte.MaxValue, Byte.MinValue}
      Yield New Object() {Byte.MaxValue, Byte.MinValue}
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {Byte.MinValue, Byte.MinValue}
      Yield New Object() {CSByte(20), False}
      Yield New Object() {CSByte(20), Nothing}
      Yield New Object() {CSByte(20), CSByte(0)}
      Yield New Object() {CSByte(0), CSByte(0)}
      Yield New Object() {CSByte((-20)), CSByte(0)}
      Yield New Object() {SByte.MaxValue, CSByte(0)}
      Yield New Object() {SByte.MinValue, CSByte(0)}
      Yield New Object() {CUShort(20), False}
      Yield New Object() {CUShort(20), Nothing}
      Yield New Object() {CUShort(20), UShort.MinValue}
      Yield New Object() {UShort.MinValue, UShort.MinValue}
      Yield New Object() {UShort.MaxValue, UShort.MinValue}
      Yield New Object() {UShort.MaxValue, UShort.MinValue}
      Yield New Object() {UShort.MaxValue, UShort.MinValue}
      Yield New Object() {UShort.MinValue, UShort.MinValue}
      Yield New Object() {UShort.MinValue, UShort.MinValue}
      Yield New Object() {UShort.MinValue, UShort.MinValue}
      Yield New Object() {CShort(Fix(20)), False}
      Yield New Object() {CShort(Fix(20)), Nothing}
      Yield New Object() {CShort(Fix(20)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(0)), CShort(Fix(0))}
      Yield New Object() {CShort(Fix((-20))), CShort(Fix(0))}
      Yield New Object() {Short.MaxValue, CShort(Fix(0))}
      Yield New Object() {Short.MinValue, CShort(Fix(0))}
      Yield New Object() {CUInt(20), False}
      Yield New Object() {CUInt(20), Nothing}
      Yield New Object() {CUInt(20), UInteger.MinValue}
      Yield New Object() {UInteger.MinValue, UInteger.MinValue}
      Yield New Object() {UInteger.MaxValue, UInteger.MinValue}
      Yield New Object() {UInteger.MaxValue, UInteger.MinValue}
      Yield New Object() {UInteger.MaxValue, UInteger.MinValue}
      Yield New Object() {UInteger.MinValue, UInteger.MinValue}
      Yield New Object() {UInteger.MinValue, UInteger.MinValue}
      Yield New Object() {UInteger.MinValue, UInteger.MinValue}
      Yield New Object() {20, False}
      Yield New Object() {20, Nothing}
      Yield New Object() {20, 0}
      Yield New Object() {0, 0}
      Yield New Object() {-20, 0}
      Yield New Object() {Integer.MaxValue, 0}
      Yield New Object() {Integer.MinValue, 0}
      Yield New Object() {CULng(20), False}
      Yield New Object() {CULng(20), Nothing}
      Yield New Object() {CULng(20), ULong.MinValue}
      Yield New Object() {ULong.MinValue, ULong.MinValue}
      Yield New Object() {ULong.MaxValue, ULong.MinValue}
      Yield New Object() {ULong.MaxValue, ULong.MinValue}
      Yield New Object() {ULong.MaxValue, ULong.MinValue}
      Yield New Object() {ULong.MinValue, ULong.MinValue}
      Yield New Object() {ULong.MinValue, ULong.MinValue}
      Yield New Object() {ULong.MinValue, ULong.MinValue}
      Yield New Object() {CLng(Fix(20)), False}
      Yield New Object() {CLng(Fix(20)), Nothing}
      Yield New Object() {CLng(Fix(20)), CLng(Fix(0))}
      Yield New Object() {CLng(Fix(0)), CLng(Fix(0))}
      Yield New Object() {CLng(Fix((-20))), CLng(Fix(0))}
      Yield New Object() {Long.MaxValue, CLng(Fix(0))}
      Yield New Object() {Long.MinValue, CLng(Fix(0))}
      Yield New Object() {CDec(20), False}
      Yield New Object() {CDec(20), Nothing}
      Yield New Object() {CDec(20), CDec(0)}
      Yield New Object() {CDec(0), CDec(0)}
      Yield New Object() {CDec((-20)), CDec(0)}
      Yield New Object() {Decimal.MaxValue, CDec(0)}
      Yield New Object() {Decimal.MinValue, CDec(0)}
      Yield New Object() {True, False}
      Yield New Object() {True, Nothing}
      Yield New Object() {Nothing, False}
      Yield New Object() {Nothing, Nothing}
    End Function

    '<SkipOnTargetFramework(TargetFrameworkMonikers.NetFramework, "Unfixed JIT bug in the .NET Framework")>
    <Theory>
    <MemberData(NameOf(ModObject_DivideByZeroObject_TestData))>
    Public Sub ModObject_DivideByZeroObject_ThrowsDivideByZeroException(left As Object, right As Object)
      Assert.Throws(Of DivideByZeroException)(Function() Operators.ModObject(left, right))
    End Sub

    Public Shared Iterator Function ModObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {"2"c, 1}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {DBNull.Value, 2}
      Yield New Object() {"3"c, New Object}
      Yield New Object() {New Object, "3"c}

      Yield New Object() {New Char() {"8"c}, 10}
      Yield New Object() {10, New Char() {"8"c}}
      Yield New Object() {New Char() {"8"c}, New Object}
      Yield New Object() {New Object, New Char() {"8"c}}
    End Function

    <Theory>
    <MemberData(NameOf(ModObject_InvalidObjects_TestData))>
    Public Sub ModObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ModObject(left, right))
    End Sub

    Public Shared Iterator Function ModObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ModObject, New Object}
      Yield New Object() {New Object, New ModObject}
      Yield New Object() {New ModObject, New ModObject}
    End Function

    <Theory>
    <MemberData(NameOf(ModObject_MismatchingObjects_TestData))>
    Public Sub ModObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ModObject(left, right))
    End Sub

    Public Class ModObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_Modulus(left As ModObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_Modulus(left As Integer, right As ModObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_Modulus(left As ModObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_Modulus(left As OperatorsTests, right As ModObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function NegateObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(9), CShort(Fix((-9)))}
      Yield New Object() {CType(9, ByteEnum), CShort(Fix((-9)))}
      Yield New Object() {Byte.MinValue, CShort(Fix(0))}
      Yield New Object() {Byte.MaxValue, CShort(Fix((-255)))}

      ' sbyte.
      Yield New Object() {CSByte(9), CSByte((-9))}
      Yield New Object() {CType(9, SByteEnum), CSByte((-9))}
      Yield New Object() {SByte.MinValue, CShort(Fix(128))}
      Yield New Object() {SByte.MaxValue, CSByte((-127))}

      ' ushort.
      Yield New Object() {CUShort(9), -9}
      Yield New Object() {CType(9, UShortEnum), -9}
      Yield New Object() {UShort.MinValue, 0}
      Yield New Object() {UShort.MaxValue, -65535}

      ' short.
      Yield New Object() {CShort(Fix(9)), CShort(Fix((-9)))}
      Yield New Object() {CType(9, ShortEnum), CShort(Fix((-9)))}
      Yield New Object() {Short.MinValue, 32768}
      Yield New Object() {Short.MaxValue, CShort(Fix((-32767)))}

      ' uint.
      Yield New Object() {CUInt(9), CLng(Fix((-9)))}
      Yield New Object() {CType(9, UIntEnum), CLng(Fix((-9)))}
      Yield New Object() {UInteger.MinValue, CLng(Fix(0))}
      Yield New Object() {UInteger.MaxValue, CLng(Fix((-4294967295)))}

      ' int.
      Yield New Object() {9, -9}
      Yield New Object() {CType(9, IntEnum), -9}
      Yield New Object() {Integer.MinValue, CLng(Fix(2147483648))}
      Yield New Object() {Integer.MaxValue, -2147483647}

      ' ulong.
      Yield New Object() {CULng(9), CDec((-9))}
      Yield New Object() {CType(9, ULongEnum), CDec((-9))}
      Yield New Object() {ULong.MinValue, CDec(0)}
      Yield New Object() {ULong.MaxValue, Decimal.Parse("-18446744073709551615", CultureInfo.InvariantCulture)}

      ' long.
      Yield New Object() {CLng(Fix(9)), CLng(Fix((-9)))}
      Yield New Object() {CType(9, LongEnum), CLng(Fix((-9)))}
      Yield New Object() {Long.MinValue, Decimal.Parse("9223372036854775808", CultureInfo.InvariantCulture)}
      Yield New Object() {Long.MaxValue, CLng(Fix((-9223372036854775807)))}

      ' float.
      Yield New Object() {CSng(9), CSng((-9))}
      Yield New Object() {Single.MinValue, Single.MaxValue}
      Yield New Object() {Single.MaxValue, Single.MinValue}

      ' double.
      Yield New Object() {CDbl(9), CDbl((-9))}
      Yield New Object() {Double.MinValue, Double.MaxValue}
      Yield New Object() {Double.MaxValue, Double.MinValue}

      ' decimal.
      Yield New Object() {CDec(9), CDec((-9))}
      Yield New Object() {Decimal.MinValue, Decimal.MaxValue}
      Yield New Object() {Decimal.MaxValue, Decimal.MinValue}

      ' string.
      Yield New Object() {"9", CDbl((-9))}

      ' bool.
      Yield New Object() {True, CShort(Fix(1))}
      Yield New Object() {False, CShort(Fix(0))}

      ' null.
      Yield New Object() {Nothing, 0}

      ' object.
      Yield New Object() {New NegateObject, "custom"}

    End Function

    <Theory>
    <MemberData(NameOf(NegateObject_TestData))>
    Public Sub NegateObject_Invoke_ReturnsExpected(value As Object, expected As Object)
      Assert.Equal(expected, Operators.NegateObject(value))
    End Sub

    Public Shared Iterator Function NegateObject_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {"a"}
      Yield New Object() {New Char() {"9"c}}
      Yield New Object() {"9"c}
      Yield New Object() {New DateTime(2018, 7, 20)}
    End Function

    <Theory>
    <MemberData(NameOf(NegateObject_InvalidObject_TestData))>
    Public Sub NegateObject_InvalidObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.NegateObject(value))
    End Sub

    Public Class NegateObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_UnaryNegation(value As NegateObject) As String
        If value IsNot Nothing Then
        End If
        Return "custom"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function NotObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(9), CByte(246)}
      Yield New Object() {CType(9, ByteEnum), CType(246, ByteEnum)}
      Yield New Object() {Byte.MinValue, Byte.MaxValue}
      Yield New Object() {Byte.MaxValue, Byte.MinValue}

      ' sbyte.
      Yield New Object() {CSByte(9), CSByte((-10))}
      Yield New Object() {CType(9, SByteEnum), CType((-10), SByteEnum)}
      Yield New Object() {SByte.MinValue, SByte.MaxValue}
      Yield New Object() {SByte.MaxValue, SByte.MinValue}

      ' ushort.
      Yield New Object() {CUShort(9), CUShort(65526)}
      Yield New Object() {CType(9, UShortEnum), CType(65526, UShortEnum)}
      Yield New Object() {UShort.MinValue, UShort.MaxValue}
      Yield New Object() {UShort.MaxValue, UShort.MinValue}

      ' short.
      Yield New Object() {CShort(Fix(9)), CShort(Fix((-10)))}
      Yield New Object() {CType(9, ShortEnum), CType((-10), ShortEnum)}
      Yield New Object() {Short.MinValue, Short.MaxValue}
      Yield New Object() {Short.MaxValue, Short.MinValue}

      ' uint.
      Yield New Object() {CUInt(9), CUInt(4294967286)}
      Yield New Object() {CType(9, UIntEnum), CType(4294967286, UIntEnum)}
      Yield New Object() {UInteger.MinValue, UInteger.MaxValue}
      Yield New Object() {UInteger.MaxValue, UInteger.MinValue}

      ' int.
      Yield New Object() {9, -10}
      Yield New Object() {CType(9, IntEnum), CType((-10), IntEnum)}
      Yield New Object() {Integer.MinValue, Integer.MaxValue}
      Yield New Object() {Integer.MaxValue, Integer.MinValue}

      ' ulong.
      Yield New Object() {CULng(9), CULng(18446744073709551606UL)}
      Yield New Object() {CType(9, ULongEnum), CType(18446744073709551606UL, ULongEnum)}
      Yield New Object() {ULong.MinValue, ULong.MaxValue}
      Yield New Object() {ULong.MaxValue, ULong.MinValue}

      ' long.
      Yield New Object() {CLng(Fix(9)), CLng(Fix((-10)))}
      Yield New Object() {CType(9, LongEnum), CType((-10), LongEnum)}
      Yield New Object() {Long.MinValue, Long.MaxValue}
      Yield New Object() {Long.MaxValue, Long.MinValue}

      ' float.
      Yield New Object() {CSng(9), CLng(Fix((-10)))}

      ' double.
      Yield New Object() {CDbl(9), CLng(Fix((-10)))}

      ' decimal.
      Yield New Object() {CDec(9), CLng(Fix((-10)))}

      ' string.
      Yield New Object() {"9", CLng(Fix((-10)))}

      ' bool.
      Yield New Object() {True, False}
      Yield New Object() {False, True}

      ' null.
      Yield New Object() {Nothing, -1}

      ' object.
      Yield New Object() {New NotObject, "custom"}

    End Function

    <Theory>
    <MemberData(NameOf(NotObject_TestData))>
    Public Sub NotObject_Invoke_ReturnsExpected(value As Object, expected As Object)
      Assert.Equal(expected, Operators.NotObject(value))
    End Sub

    Public Shared Iterator Function NotObject_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {"a"}
      Yield New Object() {New Char() {"9"c}}
      Yield New Object() {"9"c}
      Yield New Object() {New DateTime(2018, 7, 20)}
    End Function

    <Theory>
    <MemberData(NameOf(NotObject_InvalidObject_TestData))>
    Public Sub NotObject_InvalidObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.NotObject(value))
    End Sub

    Public Shared Iterator Function NotObject_OverflowObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {Single.MinValue}
      Yield New Object() {Single.MaxValue}
      Yield New Object() {Double.MinValue}
      Yield New Object() {Double.MaxValue}
      Yield New Object() {Decimal.MinValue}
      Yield New Object() {Decimal.MaxValue}
    End Function

    <Theory>
    <MemberData(NameOf(NotObject_OverflowObject_TestData))>
    Public Sub NotObject_OverflowObject_ThrowsOverflowException(value As Object)
      Assert.Throws(Of OverflowException)(Function() Operators.NotObject(value))
    End Sub

    Public Class NotObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_OnesComplement(value As NotObject) As String
        Return "custom"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function OrObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(14), CByte(14)}
      Yield New Object() {CByte(10), CType(14, ByteEnum), CByte(14)}
      Yield New Object() {CByte(10), CSByte(14), CShort(Fix(14))}
      Yield New Object() {CByte(10), CType(14, SByteEnum), CShort(Fix(14))}
      Yield New Object() {CByte(10), CUShort(14), CUShort(14)}
      Yield New Object() {CByte(10), CType(14, UShortEnum), CUShort(14)}
      Yield New Object() {CByte(10), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CByte(10), CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {CByte(10), CUInt(14), CUInt(14)}
      Yield New Object() {CByte(10), CType(14, UIntEnum), CUInt(14)}
      Yield New Object() {CByte(10), 14, 14}
      Yield New Object() {CByte(10), CType(14, IntEnum), 14}
      Yield New Object() {CByte(10), CULng(14), CULng(14)}
      Yield New Object() {CByte(10), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CByte(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CByte(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CByte(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CByte(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CByte(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CByte(10), "14", CLng(Fix(14))}
      Yield New Object() {CByte(10), True, CShort(Fix(-1))}
      Yield New Object() {CByte(10), Nothing, CByte(10)}

      Yield New Object() {CType(10, ByteEnum), CByte(14), CByte(14)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ByteEnum), CType(14, ByteEnum)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ByteEnum2), CByte(14)}
      Yield New Object() {CType(10, ByteEnum), CSByte(14), CShort(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CType(14, SByteEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CUShort(14), CUShort(14)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UShortEnum), CUShort(14)}
      Yield New Object() {CType(10, ByteEnum), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CUInt(14), CUInt(14)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UIntEnum), CUInt(14)}
      Yield New Object() {CType(10, ByteEnum), 14, 14}
      Yield New Object() {CType(10, ByteEnum), CType(14, IntEnum), 14}
      Yield New Object() {CType(10, ByteEnum), CULng(14), CULng(14)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CType(10, ByteEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, ByteEnum), True, CShort(Fix(-1))}
      Yield New Object() {CType(10, ByteEnum), Nothing, CType(10, ByteEnum)}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(14), CShort(Fix(14))}
      Yield New Object() {CSByte(10), CType(14, ByteEnum), CShort(Fix(14))}
      Yield New Object() {CSByte(10), CSByte(14), CSByte(14)}
      Yield New Object() {CSByte(10), CType(14, SByteEnum), CSByte(14)}
      Yield New Object() {CSByte(10), CUShort(14), 14}
      Yield New Object() {CSByte(10), CType(14, UShortEnum), 14}
      Yield New Object() {CSByte(10), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CSByte(10), CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {CSByte(10), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CSByte(10), 14, 14}
      Yield New Object() {CSByte(10), CType(14, IntEnum), 14}
      Yield New Object() {CSByte(10), CULng(14), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CSByte(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CSByte(10), "14", CLng(Fix(14))}
      Yield New Object() {CSByte(10), True, CSByte(-1)}
      Yield New Object() {CSByte(10), Nothing, CSByte(10)}

      Yield New Object() {CType(10, SByteEnum), CByte(14), CShort(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ByteEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CSByte(14), CSByte(14)}
      Yield New Object() {CType(10, SByteEnum), CType(14, SByteEnum), CType(14, SByteEnum)}
      Yield New Object() {CType(10, SByteEnum), CType(14, SByteEnum2), CSByte(14)}
      Yield New Object() {CType(10, SByteEnum), CUShort(14), 14}
      Yield New Object() {CType(10, SByteEnum), CType(14, UShortEnum), 14}
      Yield New Object() {CType(10, SByteEnum), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), 14, 14}
      Yield New Object() {CType(10, SByteEnum), CType(14, IntEnum), 14}
      Yield New Object() {CType(10, SByteEnum), CULng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, SByteEnum), True, CSByte(-1)}
      Yield New Object() {CType(10, SByteEnum), Nothing, CType(10, SByteEnum)}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(14), CUShort(14)}
      Yield New Object() {CUShort(10), CType(14, ByteEnum), CUShort(14)}
      Yield New Object() {CUShort(10), CSByte(14), 14}
      Yield New Object() {CUShort(10), CType(14, SByteEnum), 14}
      Yield New Object() {CUShort(10), CUShort(14), CUShort(14)}
      Yield New Object() {CUShort(10), CType(14, UShortEnum), CUShort(14)}
      Yield New Object() {CUShort(10), CShort(Fix(14)), 14}
      Yield New Object() {CUShort(10), CType(14, ShortEnum), 14}
      Yield New Object() {CUShort(10), CUInt(14), CUInt(14)}
      Yield New Object() {CUShort(10), CType(14, UIntEnum), CUInt(14)}
      Yield New Object() {CUShort(10), 14, 14}
      Yield New Object() {CUShort(10), CType(14, IntEnum), 14}
      Yield New Object() {CUShort(10), CULng(14), CULng(14)}
      Yield New Object() {CUShort(10), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CUShort(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CUShort(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CUShort(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CUShort(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CUShort(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CUShort(10), "14", CLng(Fix(14))}
      Yield New Object() {CUShort(10), True, -1}
      Yield New Object() {CUShort(10), Nothing, CUShort(10)}

      Yield New Object() {CType(10, UShortEnum), CByte(14), CUShort(14)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ByteEnum), CUShort(14)}
      Yield New Object() {CType(10, UShortEnum), CSByte(14), 14}
      Yield New Object() {CType(10, UShortEnum), CType(14, SByteEnum), 14}
      Yield New Object() {CType(10, UShortEnum), CUShort(14), CUShort(14)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UShortEnum), CType(14, UShortEnum)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UShortEnum2), CUShort(14)}
      Yield New Object() {CType(10, UShortEnum), CShort(Fix(14)), 14}
      Yield New Object() {CType(10, UShortEnum), CType(14, ShortEnum), 14}
      Yield New Object() {CType(10, UShortEnum), CUInt(14), CUInt(14)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UIntEnum), CUInt(14)}
      Yield New Object() {CType(10, UShortEnum), 14, 14}
      Yield New Object() {CType(10, UShortEnum), CType(14, IntEnum), 14}
      Yield New Object() {CType(10, UShortEnum), CULng(14), CULng(14)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CType(10, UShortEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, UShortEnum), True, -1}
      Yield New Object() {CType(10, UShortEnum), Nothing, CType(10, UShortEnum)}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(14), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, ByteEnum), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CSByte(14), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, SByteEnum), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CUShort(14), 14}
      Yield New Object() {CShort(Fix(10)), CType(14, UShortEnum), 14}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, ShortEnum), CShort(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), 14, 14}
      Yield New Object() {CShort(Fix(10)), CType(14, IntEnum), 14}
      Yield New Object() {CShort(Fix(10)), CULng(14), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CSng(14), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), CDec(14), CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), "14", CLng(Fix(14))}
      Yield New Object() {CShort(Fix(10)), True, CShort(Fix(-1))}
      Yield New Object() {CShort(Fix(10)), Nothing, CShort(Fix(10))}

      Yield New Object() {CType(10, ShortEnum), CByte(14), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ByteEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CSByte(14), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, SByteEnum), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CUShort(14), 14}
      Yield New Object() {CType(10, ShortEnum), CType(14, UShortEnum), 14}
      Yield New Object() {CType(10, ShortEnum), CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ShortEnum), CType(14, ShortEnum)}
      Yield New Object() {CType(10, ShortEnum), CType(14, ShortEnum2), CShort(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), 14, 14}
      Yield New Object() {CType(10, ShortEnum), CType(14, IntEnum), 14}
      Yield New Object() {CType(10, ShortEnum), CULng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, ShortEnum), True, CShort(Fix(-1))}
      Yield New Object() {CType(10, ShortEnum), Nothing, CType(10, ShortEnum)}

      ' uint.
      Yield New Object() {CUInt(10), CByte(14), CUInt(14)}
      Yield New Object() {CUInt(10), CType(14, ByteEnum), CUInt(14)}
      Yield New Object() {CUInt(10), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CUShort(14), CUInt(14)}
      Yield New Object() {CUInt(10), CType(14, UShortEnum), CUInt(14)}
      Yield New Object() {CUInt(10), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CUInt(14), CUInt(14)}
      Yield New Object() {CUInt(10), CType(14, UIntEnum), CUInt(14)}
      Yield New Object() {CUInt(10), 14, CLng(Fix(14))}
      Yield New Object() {CUInt(10), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CULng(14), CULng(14)}
      Yield New Object() {CUInt(10), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CUInt(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CUInt(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CUInt(10), "14", CLng(Fix(14))}
      Yield New Object() {CUInt(10), True, CLng(Fix(-1))}
      Yield New Object() {CUInt(10), Nothing, CUInt(10)}

      Yield New Object() {CType(10, UIntEnum), CByte(14), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ByteEnum), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CUShort(14), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UShortEnum), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CUInt(14), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UIntEnum), CType(14, UIntEnum)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UIntEnum2), CUInt(14)}
      Yield New Object() {CType(10, UIntEnum), 14, CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CULng(14), CULng(14)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CType(10, UIntEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, UIntEnum), True, CLng(Fix(-1))}
      Yield New Object() {CType(10, UIntEnum), Nothing, CType(10, UIntEnum)}

      ' int.
      Yield New Object() {10, CByte(14), 14}
      Yield New Object() {10, CType(14, ByteEnum), 14}
      Yield New Object() {10, CSByte(14), 14}
      Yield New Object() {10, CType(14, SByteEnum), 14}
      Yield New Object() {10, CUShort(14), 14}
      Yield New Object() {10, CType(14, UShortEnum), 14}
      Yield New Object() {10, CShort(Fix(14)), 14}
      Yield New Object() {10, CType(14, ShortEnum), 14}
      Yield New Object() {10, CUInt(14), CLng(Fix(14))}
      Yield New Object() {10, CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {10, 14, 14}
      Yield New Object() {10, CType(14, IntEnum), 14}
      Yield New Object() {10, CULng(14), CLng(Fix(14))}
      Yield New Object() {10, CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {10, CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {10, CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {10, CSng(14), CLng(Fix(14))}
      Yield New Object() {10, CDbl(14), CLng(Fix(14))}
      Yield New Object() {10, CDec(14), CLng(Fix(14))}
      Yield New Object() {10, "14", CLng(Fix(14))}
      Yield New Object() {10, True, -1}
      Yield New Object() {10, Nothing, 10}

      Yield New Object() {CType(10, IntEnum), CByte(14), 14}
      Yield New Object() {CType(10, IntEnum), CType(14, ByteEnum), 14}
      Yield New Object() {CType(10, IntEnum), CSByte(14), 14}
      Yield New Object() {CType(10, IntEnum), CType(14, SByteEnum), 14}
      Yield New Object() {CType(10, IntEnum), CUShort(14), 14}
      Yield New Object() {CType(10, IntEnum), CType(14, UShortEnum), 14}
      Yield New Object() {CType(10, IntEnum), CShort(Fix(14)), 14}
      Yield New Object() {CType(10, IntEnum), CType(14, ShortEnum), 14}
      Yield New Object() {CType(10, IntEnum), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), 14, 14}
      Yield New Object() {CType(10, IntEnum), CType(14, IntEnum), CType(14, IntEnum)}
      Yield New Object() {CType(10, IntEnum), CType(14, IntEnum2), 14}
      Yield New Object() {CType(10, IntEnum), CULng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, IntEnum), True, -1}
      Yield New Object() {CType(10, IntEnum), Nothing, CType(10, IntEnum)}

      ' ulong.
      Yield New Object() {CULng(10), CByte(14), CULng(14)}
      Yield New Object() {CULng(10), CType(14, ByteEnum), CULng(14)}
      Yield New Object() {CULng(10), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CULng(10), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CULng(10), CUShort(14), CULng(14)}
      Yield New Object() {CULng(10), CType(14, UShortEnum), CULng(14)}
      Yield New Object() {CULng(10), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CULng(10), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CULng(10), CUInt(14), CULng(14)}
      Yield New Object() {CULng(10), CType(14, UIntEnum), CULng(14)}
      Yield New Object() {CULng(10), 14, CLng(Fix(14))}
      Yield New Object() {CULng(10), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CULng(10), CULng(14), CULng(14)}
      Yield New Object() {CULng(10), CType(14, ULongEnum), CULng(14)}
      Yield New Object() {CULng(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CULng(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CULng(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CULng(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CULng(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CULng(10), "14", CLng(Fix(14))}
      Yield New Object() {CULng(10), True, CLng(Fix(-1))}
      Yield New Object() {CULng(10), Nothing, CULng(10)}

      Yield New Object() {CType(10, ULongEnum), CByte(14), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ByteEnum), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CUShort(14), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UShortEnum), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CUInt(14), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UIntEnum), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), 14, CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CULng(14), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ULongEnum), CType(14, ULongEnum)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ULongEnum2), CULng(14)}
      Yield New Object() {CType(10, ULongEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, ULongEnum), True, CLng(Fix(-1))}
      Yield New Object() {CType(10, ULongEnum), Nothing, CType(10, ULongEnum)}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CUShort(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), 14, CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CULng(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CSng(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), CDec(14), CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), "14", CLng(Fix(14))}
      Yield New Object() {CLng(Fix(10)), True, CLng(Fix(-1))}
      Yield New Object() {CLng(Fix(10)), Nothing, CLng(Fix(10))}

      Yield New Object() {CType(10, LongEnum), CByte(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CUShort(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), 14, CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CULng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CType(14, LongEnum), CType(14, LongEnum)}
      Yield New Object() {CType(10, LongEnum), CType(14, LongEnum2), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CSng(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), CDec(14), CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), "14", CLng(Fix(14))}
      Yield New Object() {CType(10, LongEnum), True, CLng(Fix(-1))}
      Yield New Object() {CType(10, LongEnum), Nothing, CType(10, LongEnum)}

      ' float.
      Yield New Object() {CSng(10), CByte(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CUShort(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), 14, CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CULng(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CSng(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CSng(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CSng(10), "14", CLng(Fix(14))}
      Yield New Object() {CSng(10), True, CLng(Fix(-1))}
      Yield New Object() {CSng(10), Nothing, CLng(Fix(10))}

      ' double.
      Yield New Object() {CDbl(10), CByte(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CUShort(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), 14, CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CULng(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CDbl(10), "14", CLng(Fix(14))}
      Yield New Object() {CDbl(10), True, CLng(Fix(-1))}
      Yield New Object() {CDbl(10), Nothing, CLng(Fix(10))}

      ' decimal.
      Yield New Object() {CDec(10), CByte(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CSByte(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CUShort(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CUInt(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), 14, CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CULng(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {CDec(10), CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {CDec(10), CSng(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CDbl(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), CDec(14), CLng(Fix(14))}
      Yield New Object() {CDec(10), "14", CLng(Fix(14))}
      Yield New Object() {CDec(10), True, CLng(Fix(-1))}
      Yield New Object() {CDec(10), Nothing, CLng(Fix(10))}

      ' string.
      Yield New Object() {"10", CByte(14), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, ByteEnum), CLng(Fix(14))}
      Yield New Object() {"10", CSByte(14), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, SByteEnum), CLng(Fix(14))}
      Yield New Object() {"10", CUShort(14), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, UShortEnum), CLng(Fix(14))}
      Yield New Object() {"10", CShort(Fix(14)), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, ShortEnum), CLng(Fix(14))}
      Yield New Object() {"10", CUInt(14), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, UIntEnum), CLng(Fix(14))}
      Yield New Object() {"10", 14, CLng(Fix(14))}
      Yield New Object() {"10", CType(14, IntEnum), CLng(Fix(14))}
      Yield New Object() {"10", CULng(14), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, ULongEnum), CLng(Fix(14))}
      Yield New Object() {"10", CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {"10", CType(14, LongEnum), CLng(Fix(14))}
      Yield New Object() {"10", CSng(14), CLng(Fix(14))}
      Yield New Object() {"10", CDbl(14), CLng(Fix(14))}
      Yield New Object() {"10", CDec(14), CLng(Fix(14))}
      Yield New Object() {"10", "14", CLng(Fix(14))}
      Yield New Object() {"10", True, True}
      Yield New Object() {"10", Nothing, CLng(Fix(10))}

      ' bool.
      Yield New Object() {True, CByte(14), CShort(Fix(-1))}
      Yield New Object() {True, CType(14, ByteEnum), CShort(Fix(-1))}
      Yield New Object() {True, CSByte(14), CSByte(-1)}
      Yield New Object() {True, CType(14, SByteEnum), CSByte(-1)}
      Yield New Object() {True, CUShort(14), -1}
      Yield New Object() {True, CType(14, UShortEnum), -1}
      Yield New Object() {True, CShort(Fix(14)), CShort(Fix(-1))}
      Yield New Object() {True, CType(14, ShortEnum), CShort(Fix(-1))}
      Yield New Object() {True, CUInt(14), CLng(Fix(-1))}
      Yield New Object() {True, CType(14, UIntEnum), CLng(Fix(-1))}
      Yield New Object() {True, 14, -1}
      Yield New Object() {True, CType(14, IntEnum), -1}
      Yield New Object() {True, CULng(14), CLng(Fix(-1))}
      Yield New Object() {True, CType(14, ULongEnum), CLng(Fix(-1))}
      Yield New Object() {True, CLng(Fix(14)), CLng(Fix(-1))}
      Yield New Object() {True, CType(14, LongEnum), CLng(Fix(-1))}
      Yield New Object() {True, CSng(14), CLng(Fix(-1))}
      Yield New Object() {True, CDbl(14), CLng(Fix(-1))}
      Yield New Object() {True, CDec(14), CLng(Fix(-1))}
      Yield New Object() {True, "14", True}
      Yield New Object() {True, True, True}
      Yield New Object() {True, Nothing, True}

      ' null.
      Yield New Object() {Nothing, CByte(14), CByte(14)}
      Yield New Object() {Nothing, CType(14, ByteEnum), CType(14, ByteEnum)}
      Yield New Object() {Nothing, CSByte(14), CSByte(14)}
      Yield New Object() {Nothing, CType(14, SByteEnum), CType(14, SByteEnum)}
      Yield New Object() {Nothing, CUShort(14), CUShort(14)}
      Yield New Object() {Nothing, CType(14, UShortEnum), CType(14, UShortEnum)}
      Yield New Object() {Nothing, CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {Nothing, CType(14, ShortEnum), CType(14, ShortEnum)}
      Yield New Object() {Nothing, CUInt(14), CUInt(14)}
      Yield New Object() {Nothing, CType(14, UIntEnum), CType(14, UIntEnum)}
      Yield New Object() {Nothing, 14, 14}
      Yield New Object() {Nothing, CType(14, IntEnum), CType(14, IntEnum)}
      Yield New Object() {Nothing, CULng(14), CULng(14)}
      Yield New Object() {Nothing, CType(14, ULongEnum), CType(14, ULongEnum)}
      Yield New Object() {Nothing, CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {Nothing, CType(14, LongEnum), CType(14, LongEnum)}
      Yield New Object() {Nothing, CSng(14), CLng(Fix(14))}
      Yield New Object() {Nothing, CDbl(14), CLng(Fix(14))}
      Yield New Object() {Nothing, CDec(14), CLng(Fix(14))}
      Yield New Object() {Nothing, "14", CLng(Fix(14))}
      Yield New Object() {Nothing, True, True}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New OrObject, 2, "custom"}
      Yield New Object() {2, New OrObject, "motsuc"}
      Yield New Object() {New OrObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New OrObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(OrObject_TestData))>
    Public Sub OrObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.OrObject(left, right))
    End Sub

    Public Shared Iterator Function OrObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(OrObject_InvalidObjects_TestData))>
    Public Sub OrObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.OrObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.OrObject(right, left))
    End Sub

    Public Shared Iterator Function OrObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New OrObject, New Object}
      Yield New Object() {New Object, New OrObject}

      Yield New Object() {New OrObject, New OrObject}
    End Function

    <Theory>
    <MemberData(NameOf(OrObject_MismatchingObjects_TestData))>
    Public Sub OrObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.OrObject(left, right))
    End Sub

    Public Class OrObject

      Public Shared Operator Or(left As OrObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator Or(left As Integer, right As OrObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator Or(left As OrObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator Or(left As OperatorsTests, right As OrObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function PlusObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(1), CByte(1)}

      ' sbyte.
      Yield New Object() {CSByte(1), CSByte(1)}
      Yield New Object() {CSByte((-1)), CSByte((-1))}

      ' ushort.
      Yield New Object() {CUShort(3), CUShort(3)}

      ' short.
      Yield New Object() {CShort(Fix(4)), CShort(Fix(4))}
      Yield New Object() {CShort(Fix((-4))), CShort(Fix((-4)))}

      ' uint.
      Yield New Object() {CUInt(4), CUInt(4)}

      ' int.
      Yield New Object() {6, 6}
      Yield New Object() {-6, -6}

      ' ulong.
      Yield New Object() {CULng(7), CULng(7)}

      ' long.
      Yield New Object() {CLng(Fix(8)), CLng(Fix(8))}
      Yield New Object() {CLng(Fix((-8))), CLng(Fix((-8)))}

      ' float.
      Yield New Object() {CSng(9), CSng(9)}
      Yield New Object() {CSng((-9)), CSng((-9))}

      ' double.
      Yield New Object() {CDbl(10), CDbl(10)}
      Yield New Object() {CDbl((-10)), CDbl((-10))}

      ' decimal.
      Yield New Object() {CDec(11), CDec(11)}
      Yield New Object() {CDec((-11)), CDec((-11))}

      ' bool.
      Yield New Object() {True, CShort(Fix((-1)))}
      Yield New Object() {False, CShort(Fix(0))}

      ' string.
      Yield New Object() {"1", CDbl(1)}
      Yield New Object() {"-1", CDbl((-1))}

      ' null.
      Yield New Object() {Nothing, 0}

      ' object.
      Yield New Object() {New PlusObject, "custom"}

    End Function

    <Theory>
    <MemberData(NameOf(PlusObject_TestData))>
    Public Sub PlusObject_Invoke_ReturnsExpected(value As Object, expected As Object)
      Assert.Equal(expected, Operators.PlusObject(value))
    End Sub

    Public Shared Iterator Function PlusObject_InvalidObject_TestData() As IEnumerable(Of Object())
      Yield New Object() {"a"}
      Yield New Object() {"a"c}
      Yield New Object() {"1"c}
      Yield New Object() {DBNull.Value}
      Yield New Object() {New DateTime(10)}
      Yield New Object() {New Object}
    End Function

    <Theory>
    <MemberData(NameOf(PlusObject_InvalidObject_TestData))>
    Public Sub PlusObject_InvalidObject_ThrowsInvalidCastException(value As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.PlusObject(value))
    End Sub

    Public Class PlusObject
      Public Shared Operator +(o As PlusObject) As String
        If o IsNot Nothing Then
        End If
        Return "custom"
      End Operator
    End Class

    Public Shared Iterator Function RightShiftObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(2), CByte(2)}
      Yield New Object() {CByte(10), CSByte(2), CByte(2)}
      Yield New Object() {CByte(10), CUShort(2), CByte(2)}
      Yield New Object() {CByte(10), CShort(Fix(2)), CByte(2)}
      Yield New Object() {CByte(10), CUInt(2), CByte(2)}
      Yield New Object() {CByte(10), 2, CByte(2)}
      Yield New Object() {CByte(10), CULng(2), CByte(2)}
      Yield New Object() {CByte(10), CLng(Fix(2)), CByte(2)}
      Yield New Object() {CByte(10), CSng(2), CByte(2)}
      Yield New Object() {CByte(10), CDbl(2), CByte(2)}
      Yield New Object() {CByte(10), CDec(2), CByte(2)}
      Yield New Object() {CByte(10), "2", CByte(2)}
      Yield New Object() {CByte(10), True, CByte(0)}
      Yield New Object() {CByte(10), Nothing, CByte(10)}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(2), CSByte(2)}
      Yield New Object() {CSByte(10), CSByte(2), CSByte(2)}
      Yield New Object() {CSByte(10), CUShort(2), CSByte(2)}
      Yield New Object() {CSByte(10), CShort(Fix(2)), CSByte(2)}
      Yield New Object() {CSByte(10), CUInt(2), CSByte(2)}
      Yield New Object() {CSByte(10), 2, CSByte(2)}
      Yield New Object() {CSByte(10), CULng(2), CSByte(2)}
      Yield New Object() {CSByte(10), CLng(Fix(2)), CSByte(2)}
      Yield New Object() {CSByte(10), CSng(2), CSByte(2)}
      Yield New Object() {CSByte(10), CDbl(2), CSByte(2)}
      Yield New Object() {CSByte(10), CDec(2), CSByte(2)}
      Yield New Object() {CSByte(10), "2", CSByte(2)}
      Yield New Object() {CSByte(10), True, CSByte(0)}
      Yield New Object() {CSByte(10), Nothing, CSByte(10)}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(2), CUShort(2)}
      Yield New Object() {CUShort(10), CSByte(2), CUShort(2)}
      Yield New Object() {CUShort(10), CUShort(2), CUShort(2)}
      Yield New Object() {CUShort(10), CShort(Fix(2)), CUShort(2)}
      Yield New Object() {CUShort(10), CUInt(2), CUShort(2)}
      Yield New Object() {CUShort(10), 2, CUShort(2)}
      Yield New Object() {CUShort(10), CULng(2), CUShort(2)}
      Yield New Object() {CUShort(10), CLng(Fix(2)), CUShort(2)}
      Yield New Object() {CUShort(10), CSng(2), CUShort(2)}
      Yield New Object() {CUShort(10), CDbl(2), CUShort(2)}
      Yield New Object() {CUShort(10), CDec(2), CUShort(2)}
      Yield New Object() {CUShort(10), "2", CUShort(2)}
      Yield New Object() {CUShort(10), True, CUShort(0)}
      Yield New Object() {CUShort(10), Nothing, CUShort(10)}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CSByte(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CUShort(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(2)), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CUInt(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), 2, CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CULng(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(2)), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CSng(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CDbl(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), CDec(2), CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), "2", CShort(Fix(2))}
      Yield New Object() {CShort(Fix(10)), True, CShort(Fix(0))}
      Yield New Object() {CShort(Fix(10)), Nothing, CShort(Fix(10))}

      ' uint.
      Yield New Object() {CUInt(10), CByte(2), CUInt(2)}
      Yield New Object() {CUInt(10), CSByte(2), CUInt(2)}
      Yield New Object() {CUInt(10), CUShort(2), CUInt(2)}
      Yield New Object() {CUInt(10), CShort(Fix(2)), CUInt(2)}
      Yield New Object() {CUInt(10), CUInt(2), CUInt(2)}
      Yield New Object() {CUInt(10), 2, CUInt(2)}
      Yield New Object() {CUInt(10), CULng(2), CUInt(2)}
      Yield New Object() {CUInt(10), CLng(Fix(2)), CUInt(2)}
      Yield New Object() {CUInt(10), CSng(2), CUInt(2)}
      Yield New Object() {CUInt(10), CDbl(2), CUInt(2)}
      Yield New Object() {CUInt(10), CDec(2), CUInt(2)}
      Yield New Object() {CUInt(10), "2", CUInt(2)}
      Yield New Object() {CUInt(10), True, CUInt(0)}
      Yield New Object() {CUInt(10), Nothing, CUInt(10)}

      ' int.
      Yield New Object() {10, CByte(2), 2}
      Yield New Object() {10, CSByte(2), 2}
      Yield New Object() {10, CUShort(2), 2}
      Yield New Object() {10, CShort(Fix(2)), 2}
      Yield New Object() {10, CUInt(2), 2}
      Yield New Object() {10, 2, 2}
      Yield New Object() {10, CULng(2), 2}
      Yield New Object() {10, CLng(Fix(2)), 2}
      Yield New Object() {10, CSng(2), 2}
      Yield New Object() {10, CDbl(2), 2}
      Yield New Object() {10, CDec(2), 2}
      Yield New Object() {10, "2", 2}
      Yield New Object() {10, True, 0}
      Yield New Object() {10, Nothing, 10}

      ' ulong.
      Yield New Object() {CULng(10), CByte(2), CULng(2)}
      Yield New Object() {CULng(10), CSByte(2), CULng(2)}
      Yield New Object() {CULng(10), CUShort(2), CULng(2)}
      Yield New Object() {CULng(10), CShort(Fix(2)), CULng(2)}
      Yield New Object() {CULng(10), CUInt(2), CULng(2)}
      Yield New Object() {CULng(10), 2, CULng(2)}
      Yield New Object() {CULng(10), CULng(2), CULng(2)}
      Yield New Object() {CULng(10), CLng(Fix(2)), CULng(2)}
      Yield New Object() {CULng(10), CSng(2), CULng(2)}
      Yield New Object() {CULng(10), CDbl(2), CULng(2)}
      Yield New Object() {CULng(10), CDec(2), CULng(2)}
      Yield New Object() {CULng(10), "2", CULng(2)}
      Yield New Object() {CULng(10), True, CULng(0)}
      Yield New Object() {CULng(10), Nothing, CULng(10)}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CSByte(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CUShort(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CUInt(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), 2, CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CULng(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CSng(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CDbl(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), CDec(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), "2", CLng(Fix(2))}
      Yield New Object() {CLng(Fix(10)), True, CLng(Fix(0))}
      Yield New Object() {CLng(Fix(10)), Nothing, CLng(Fix(10))}

      ' float.
      Yield New Object() {CSng(10), CByte(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CSByte(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CUShort(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CShort(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CSng(10), CUInt(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), 2, CLng(Fix(2))}
      Yield New Object() {CSng(10), CULng(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CLng(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CSng(10), CSng(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CDbl(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), CDec(2), CLng(Fix(2))}
      Yield New Object() {CSng(10), "2", CLng(Fix(2))}
      Yield New Object() {CSng(10), True, CLng(Fix(0))}
      Yield New Object() {CSng(10), Nothing, CLng(Fix(10))}

      ' double.
      Yield New Object() {CDbl(10), CByte(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CSByte(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CUShort(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CShort(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CUInt(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), 2, CLng(Fix(2))}
      Yield New Object() {CDbl(10), CULng(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CLng(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CSng(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CDbl(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), CDec(2), CLng(Fix(2))}
      Yield New Object() {CDbl(10), "2", CLng(Fix(2))}
      Yield New Object() {CDbl(10), True, CLng(Fix(0))}
      Yield New Object() {CDbl(10), Nothing, CLng(Fix(10))}

      ' decimal.
      Yield New Object() {CDec(10), CByte(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CSByte(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CUShort(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CShort(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CDec(10), CUInt(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), 2, CLng(Fix(2))}
      Yield New Object() {CDec(10), CULng(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CLng(Fix(2)), CLng(Fix(2))}
      Yield New Object() {CDec(10), CSng(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CDbl(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), CDec(2), CLng(Fix(2))}
      Yield New Object() {CDec(10), "2", CLng(Fix(2))}
      Yield New Object() {CDec(10), True, CLng(Fix(0))}
      Yield New Object() {CDec(10), Nothing, CLng(Fix(10))}

      ' string.
      Yield New Object() {"10", CByte(2), CLng(Fix(2))}
      Yield New Object() {"10", CSByte(2), CLng(Fix(2))}
      Yield New Object() {"10", CUShort(2), CLng(Fix(2))}
      Yield New Object() {"10", CShort(Fix(2)), CLng(Fix(2))}
      Yield New Object() {"10", CUInt(2), CLng(Fix(2))}
      Yield New Object() {"10", 2, CLng(Fix(2))}
      Yield New Object() {"10", CULng(2), CLng(Fix(2))}
      Yield New Object() {"10", CLng(Fix(2)), CLng(Fix(2))}
      Yield New Object() {"10", CSng(2), CLng(Fix(2))}
      Yield New Object() {"10", CDbl(2), CLng(Fix(2))}
      Yield New Object() {"10", CDec(2), CLng(Fix(2))}
      Yield New Object() {"10", "2", CLng(Fix(2))}
      Yield New Object() {"10", True, CLng(Fix(0))}
      Yield New Object() {"10", Nothing, CLng(Fix(10))}

      ' bool.
      Yield New Object() {True, CByte(2), CShort(Fix(-1))}
      Yield New Object() {True, CSByte(2), CShort(Fix(-1))}
      Yield New Object() {True, CUShort(2), CShort(Fix(-1))}
      Yield New Object() {True, CShort(Fix(2)), CShort(Fix(-1))}
      Yield New Object() {True, CUInt(2), CShort(Fix(-1))}
      Yield New Object() {True, 2, CShort(Fix(-1))}
      Yield New Object() {True, CULng(2), CShort(Fix(-1))}
      Yield New Object() {True, CLng(Fix(2)), CShort(Fix(-1))}
      Yield New Object() {True, CSng(2), CShort(Fix(-1))}
      Yield New Object() {True, CDbl(2), CShort(Fix(-1))}
      Yield New Object() {True, CDec(2), CShort(Fix(-1))}
      Yield New Object() {True, "2", CShort(Fix(-1))}
      Yield New Object() {True, True, CShort(Fix(-1))}
      Yield New Object() {True, Nothing, CShort(Fix(-1))}

      ' null.
      Yield New Object() {Nothing, CByte(2), 0}
      Yield New Object() {Nothing, CSByte(2), 0}
      Yield New Object() {Nothing, CUShort(2), 0}
      Yield New Object() {Nothing, CShort(Fix(2)), 0}
      Yield New Object() {Nothing, CUInt(2), 0}
      Yield New Object() {Nothing, 2, 0}
      Yield New Object() {Nothing, CULng(2), 0}
      Yield New Object() {Nothing, CLng(Fix(2)), 0}
      Yield New Object() {Nothing, CSng(2), 0}
      Yield New Object() {Nothing, CDbl(2), 0}
      Yield New Object() {Nothing, CDec(2), 0}
      Yield New Object() {Nothing, "2", 0}
      Yield New Object() {Nothing, True, 0}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New RightShiftObject, 2, "custom"}
      Yield New Object() {2, New RightShiftObject, "motsuc"}
      Yield New Object() {New RightShiftObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New RightShiftObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(RightShiftObject_TestData))>
    Public Sub RightShiftObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.RightShiftObject(left, right))
    End Sub

    Public Shared Iterator Function RightShiftObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(RightShiftObject_InvalidObjects_TestData))>
    Public Sub RightShiftObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.RightShiftObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.RightShiftObject(right, left))
    End Sub

    Public Shared Iterator Function RightShiftObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New RightShiftObject, New Object}
      Yield New Object() {New Object, New RightShiftObject}
      Yield New Object() {New RightShiftObject, New RightShiftObject}
    End Function

    <Theory>
    <MemberData(NameOf(RightShiftObject_MismatchingObjects_TestData))>
    Public Sub RightShiftObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.RightShiftObject(left, right))
    End Sub

    Public Class RightShiftObject

#Disable Warning IDE1006 ' Naming Styles
      <SpecialName>
      Public Shared Function op_RightShift(left As RightShiftObject, right As Integer) As String
        If left IsNot Nothing AndAlso
           right <> 0 Then
        End If
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_RightShift(left As Integer, right As RightShiftObject) As String
        If left <> 0 AndAlso
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_RightShift(left As RightShiftObject, right As OperatorsTests) As String
        If left IsNot Nothing AndAlso
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_RightShift(left As OperatorsTests, right As RightShiftObject) As String
        If left IsNot Nothing AndAlso
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Function
#Enable Warning IDE1006 ' Naming Styles

    End Class

    Public Shared Iterator Function SubtractObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(2), CByte(2), CByte(0)}
      Yield New Object() {CByte(3), CSByte(2), CShort(Fix(1))}
      Yield New Object() {CByte(4), CUShort(2), CUShort(2)}
      Yield New Object() {CByte(5), CShort(Fix(2)), CShort(Fix(3))}
      Yield New Object() {CByte(6), CUInt(2), CUInt(4)}
      Yield New Object() {CByte(7), 2, 5}
      Yield New Object() {CByte(8), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CByte(9), CULng(2), CULng(7)}
      Yield New Object() {CByte(10), CSng(2), CSng(8)}
      Yield New Object() {CByte(11), CDbl(2), CDbl(9)}
      Yield New Object() {CByte(12), CDec(2), CDec(10)}
      Yield New Object() {CByte(13), "2", CDbl(11)}
      Yield New Object() {CByte(14), True, CShort(Fix(15))}
      Yield New Object() {CByte(15), Nothing, CByte(15)}
      Yield New Object() {CByte(16), Byte.MaxValue, CShort(Fix((-239)))}

      ' sbyte.
      Yield New Object() {CSByte(2), CByte(2), CShort(Fix(0))}
      Yield New Object() {CSByte(3), CSByte(2), CSByte(1)}
      Yield New Object() {CSByte(4), CUShort(2), 2}
      Yield New Object() {CSByte(5), CShort(Fix(2)), CShort(Fix(3))}
      Yield New Object() {CSByte(6), CUInt(2), CLng(Fix(4))}
      Yield New Object() {CSByte(7), 2, 5}
      Yield New Object() {CSByte(8), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CSByte(9), CULng(2), CDec(7)}
      Yield New Object() {CSByte(10), CSng(2), CSng(8)}
      Yield New Object() {CSByte(11), CDbl(2), CDbl(9)}
      Yield New Object() {CSByte(12), CDec(2), CDec(10)}
      Yield New Object() {CSByte(13), "2", CDbl(11)}
      Yield New Object() {CSByte(14), True, CSByte(15)}
      Yield New Object() {CSByte(15), Nothing, CSByte(15)}
      Yield New Object() {CSByte((-2)), SByte.MaxValue, CShort(Fix((-129)))}

      ' ushort.
      Yield New Object() {CUShort(2), CByte(2), CUShort(0)}
      Yield New Object() {CUShort(3), CSByte(2), 1}
      Yield New Object() {CUShort(4), CUShort(2), CUShort(2)}
      Yield New Object() {CUShort(5), CShort(Fix(2)), 3}
      Yield New Object() {CUShort(6), CUInt(2), CUInt(4)}
      Yield New Object() {CUShort(7), 2, 5}
      Yield New Object() {CUShort(8), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CUShort(9), CULng(2), CULng(7)}
      Yield New Object() {CUShort(10), CSng(2), CSng(8)}
      Yield New Object() {CUShort(11), CDbl(2), CDbl(9)}
      Yield New Object() {CUShort(12), CDec(2), CDec(10)}
      Yield New Object() {CUShort(13), "2", CDbl(11)}
      Yield New Object() {CUShort(14), True, 15}
      Yield New Object() {CUShort(15), Nothing, CUShort(15)}
      Yield New Object() {CUShort(16), UShort.MaxValue, -65519}

      ' short.
      Yield New Object() {CShort(Fix(2)), CByte(2), CShort(Fix(0))}
      Yield New Object() {CShort(Fix(3)), CSByte(2), CShort(Fix(1))}
      Yield New Object() {CShort(Fix(4)), CUShort(2), 2}
      Yield New Object() {CShort(Fix(5)), CShort(Fix(2)), CShort(Fix(3))}
      Yield New Object() {CShort(Fix(6)), CUInt(2), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(7)), 2, 5}
      Yield New Object() {CShort(Fix(8)), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CShort(Fix(9)), CULng(2), CDec(7)}
      Yield New Object() {CShort(Fix(10)), CSng(2), CSng(8)}
      Yield New Object() {CShort(Fix(11)), CDbl(2), CDbl(9)}
      Yield New Object() {CShort(Fix(12)), CDec(2), CDec(10)}
      Yield New Object() {CShort(Fix(13)), "2", CDbl(11)}
      Yield New Object() {CShort(Fix(14)), True, CShort(Fix(15))}
      Yield New Object() {CShort(Fix(15)), Nothing, CShort(Fix(15))}
      Yield New Object() {CShort(Fix((-2))), Short.MaxValue, -32769}

      ' uint.
      Yield New Object() {CUInt(2), CByte(2), CUInt(0)}
      Yield New Object() {CUInt(3), CSByte(2), CLng(Fix(1))}
      Yield New Object() {CUInt(4), CUShort(2), CUInt(2)}
      Yield New Object() {CUInt(5), CShort(Fix(2)), CLng(Fix(3))}
      Yield New Object() {CUInt(6), CUInt(2), CUInt(4)}
      Yield New Object() {CUInt(7), 2, CLng(Fix(5))}
      Yield New Object() {CUInt(8), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CUInt(9), CULng(2), CULng(7)}
      Yield New Object() {CUInt(10), CSng(2), CSng(8)}
      Yield New Object() {CUInt(11), CDbl(2), CDbl(9)}
      Yield New Object() {CUInt(12), CDec(2), CDec(10)}
      Yield New Object() {CUInt(13), "2", CDbl(11)}
      Yield New Object() {CUInt(14), True, CLng(Fix(15))}
      Yield New Object() {CUInt(15), Nothing, CUInt(15)}
      Yield New Object() {CUInt(16), UInteger.MaxValue, CLng(Fix((-4294967279)))}

      ' int.
      Yield New Object() {2, CByte(2), 0}
      Yield New Object() {3, CSByte(2), 1}
      Yield New Object() {4, CUShort(2), 2}
      Yield New Object() {5, CShort(Fix(2)), 3}
      Yield New Object() {6, CUInt(2), CLng(Fix(4))}
      Yield New Object() {7, 2, 5}
      Yield New Object() {8, CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {9, CULng(2), CDec(7)}
      Yield New Object() {10, CSng(2), CSng(8)}
      Yield New Object() {11, CDbl(2), CDbl(9)}
      Yield New Object() {12, CDec(2), CDec(10)}
      Yield New Object() {13, "2", CDbl(11)}
      Yield New Object() {14, True, 15}
      Yield New Object() {15, Nothing, 15}
      Yield New Object() {-2, Integer.MaxValue, CLng(Fix((-2147483649)))}

      ' ulong.
      Yield New Object() {CULng(2), CByte(2), CULng(0)}
      Yield New Object() {CULng(3), CSByte(2), CDec(1)}
      Yield New Object() {CULng(4), CUShort(2), CULng(2)}
      Yield New Object() {CULng(5), CShort(Fix(2)), CDec(3)}
      Yield New Object() {CULng(6), CUInt(2), CULng(4)}
      Yield New Object() {CULng(7), 2, CDec(5)}
      Yield New Object() {CULng(8), CLng(Fix(2)), CDec(6)}
      Yield New Object() {CULng(9), CULng(2), CULng(7)}
      Yield New Object() {CULng(10), CSng(2), CSng(8)}
      Yield New Object() {CULng(11), CDbl(2), CDbl(9)}
      Yield New Object() {CULng(12), CDec(2), CDec(10)}
      Yield New Object() {CULng(13), "2", CDbl(11)}
      Yield New Object() {CULng(14), True, CDec(15)}
      Yield New Object() {CULng(15), Nothing, CULng(15)}
      Yield New Object() {CULng(16), ULong.MaxValue, Decimal.Parse("-18446744073709551599", CultureInfo.InvariantCulture)}

      ' long.
      Yield New Object() {CLng(Fix(2)), CByte(2), CLng(Fix(0))}
      Yield New Object() {CLng(Fix(3)), CSByte(2), CLng(Fix(1))}
      Yield New Object() {CLng(Fix(4)), CUShort(2), CLng(Fix(2))}
      Yield New Object() {CLng(Fix(5)), CShort(Fix(2)), CLng(Fix(3))}
      Yield New Object() {CLng(Fix(6)), CUInt(2), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(7)), 2, CLng(Fix(5))}
      Yield New Object() {CLng(Fix(8)), CLng(Fix(2)), CLng(Fix(6))}
      Yield New Object() {CLng(Fix(9)), CULng(2), CDec(7)}
      Yield New Object() {CLng(Fix(10)), CSng(2), CSng(8)}
      Yield New Object() {CLng(Fix(11)), CDbl(2), CDbl(9)}
      Yield New Object() {CLng(Fix(12)), CDec(2), CDec(10)}
      Yield New Object() {CLng(Fix(13)), "2", CDbl(11)}
      Yield New Object() {CLng(Fix(14)), True, CLng(Fix(15))}
      Yield New Object() {CLng(Fix(15)), Nothing, CLng(Fix(15))}
      Yield New Object() {CLng(Fix((-2))), Long.MaxValue, Decimal.Parse("-9223372036854775809", CultureInfo.InvariantCulture)}

      ' float.
      Yield New Object() {CSng(2), CByte(2), CSng(0)}
      Yield New Object() {CSng(3), CSByte(2), CSng(1)}
      Yield New Object() {CSng(4), CUShort(2), CSng(2)}
      Yield New Object() {CSng(5), CShort(Fix(2)), CSng(3)}
      Yield New Object() {CSng(6), CUInt(2), CSng(4)}
      Yield New Object() {CSng(7), 2, CSng(5)}
      Yield New Object() {CSng(8), CLng(Fix(2)), CSng(6)}
      Yield New Object() {CSng(9), CULng(2), CSng(7)}
      Yield New Object() {CSng(10), CSng(2), CSng(8)}
      Yield New Object() {CSng(11), CDbl(2), CDbl(9)}
      Yield New Object() {CSng(12), CDec(2), CSng(10)}
      Yield New Object() {CSng(13), "2", CDbl(11)}
      Yield New Object() {CSng(14), True, CSng(15)}
      Yield New Object() {CSng(15), Nothing, CSng(15)}
      Yield New Object() {Single.MinValue, Single.MaxValue, CDbl(Single.MinValue) - CDbl(Single.MaxValue)}
      Yield New Object() {CSng(16), Single.PositiveInfinity, Single.NegativeInfinity}
      Yield New Object() {CSng(17), Single.NegativeInfinity, Single.PositiveInfinity}
      Yield New Object() {CSng(18), Single.NaN, Double.NaN}
      Yield New Object() {Single.PositiveInfinity, CSng(2), Single.PositiveInfinity}
      Yield New Object() {Single.NegativeInfinity, CSng(2), Single.NegativeInfinity}
      Yield New Object() {Single.NaN, CSng(2), Double.NaN}

      ' double.
      Yield New Object() {CDbl(2), CByte(2), CDbl(0)}
      Yield New Object() {CDbl(3), CSByte(2), CDbl(1)}
      Yield New Object() {CDbl(4), CUShort(2), CDbl(2)}
      Yield New Object() {CDbl(5), CShort(Fix(2)), CDbl(3)}
      Yield New Object() {CDbl(6), CUInt(2), CDbl(4)}
      Yield New Object() {CDbl(7), 2, CDbl(5)}
      Yield New Object() {CDbl(8), CLng(Fix(2)), CDbl(6)}
      Yield New Object() {CDbl(9), CULng(2), CDbl(7)}
      Yield New Object() {CDbl(10), CSng(2), CDbl(8)}
      Yield New Object() {CDbl(11), CDbl(2), CDbl(9)}
      Yield New Object() {CDbl(12), CDec(2), CDbl(10)}
      Yield New Object() {CDbl(13), "2", CDbl(11)}
      Yield New Object() {CDbl(14), True, CDbl(15)}
      Yield New Object() {CDbl(15), Nothing, CDbl(15)}
      Yield New Object() {Double.MinValue, Double.MaxValue, Double.NegativeInfinity}
      Yield New Object() {CDbl(16), Double.PositiveInfinity, Double.NegativeInfinity}
      Yield New Object() {CDbl(17), Double.NegativeInfinity, Double.PositiveInfinity}
      Yield New Object() {CDbl(18), Double.NaN, Double.NaN}
      Yield New Object() {Double.PositiveInfinity, CDbl(2), Double.PositiveInfinity}
      Yield New Object() {Double.NegativeInfinity, CDbl(2), Double.NegativeInfinity}
      Yield New Object() {Double.NaN, CDbl(2), Double.NaN}

      ' decimal.
      Yield New Object() {CDec(2), CByte(2), CDec(0)}
      Yield New Object() {CDec(3), CSByte(2), CDec(1)}
      Yield New Object() {CDec(4), CUShort(2), CDec(2)}
      Yield New Object() {CDec(5), CShort(Fix(2)), CDec(3)}
      Yield New Object() {CDec(6), CUInt(2), CDec(4)}
      Yield New Object() {CDec(7), 2, CDec(5)}
      Yield New Object() {CDec(8), CLng(Fix(2)), CDec(6)}
      Yield New Object() {CDec(9), CULng(2), CDec(7)}
      Yield New Object() {CDec(10), CSng(2), CSng(8)}
      Yield New Object() {CDec(11), CDbl(2), CDbl(9)}
      Yield New Object() {CDec(12), CDec(2), CDec(10)}
      Yield New Object() {CDec(13), "2", CDbl(11)}
      Yield New Object() {CDec(14), True, CDec(15)}
      Yield New Object() {CDec(15), Nothing, CDec(15)}
      Yield New Object() {Decimal.MinValue, Decimal.MaxValue, Double.Parse("-1.5845632502852868E+29", NumberStyles.Any, CultureInfo.InvariantCulture)}

      ' string.
      Yield New Object() {"2", CByte(2), CDbl(0)}
      Yield New Object() {"3", CSByte(2), CDbl(1)}
      Yield New Object() {"4", CUShort(2), CDbl(2)}
      Yield New Object() {"5", CShort(Fix(2)), CDbl(3)}
      Yield New Object() {"6", CUInt(2), CDbl(4)}
      Yield New Object() {"7", 2, CDbl(5)}
      Yield New Object() {"8", CLng(Fix(2)), CDbl(6)}
      Yield New Object() {"9", CULng(2), CDbl(7)}
      Yield New Object() {"10", CSng(2), CDbl(8)}
      Yield New Object() {"11", CDbl(2), CDbl(9)}
      Yield New Object() {"12", CDec(2), CDbl(10)}
      Yield New Object() {"13", "2", CDbl(11)}
      Yield New Object() {"14", True, CDbl(15)}
      Yield New Object() {"15", Nothing, CDbl(15)}

      ' bool.
      Yield New Object() {True, CByte(2), CShort(Fix((-3)))}
      Yield New Object() {True, CSByte(2), CSByte((-3))}
      Yield New Object() {True, CUShort(2), -3}
      Yield New Object() {True, CShort(Fix(2)), CShort(Fix((-3)))}
      Yield New Object() {True, CUInt(2), CLng(Fix((-3)))}
      Yield New Object() {True, 2, -3}
      Yield New Object() {True, CLng(Fix(2)), CLng(Fix((-3)))}
      Yield New Object() {True, CULng(2), CDec((-3))}
      Yield New Object() {True, CSng(2), CSng((-3))}
      Yield New Object() {True, CDbl(2), CDbl((-3))}
      Yield New Object() {True, CDec(2), CDec((-3))}
      Yield New Object() {True, "2", CDbl((-3))}
      Yield New Object() {True, False, CShort(Fix((-1)))}
      Yield New Object() {True, Nothing, CShort(Fix((-1)))}

      ' null.
      Yield New Object() {Nothing, CByte(2), CShort(Fix((-2)))}
      Yield New Object() {Nothing, CSByte(2), CSByte((-2))}
      Yield New Object() {Nothing, CUShort(2), -2}
      Yield New Object() {Nothing, CShort(Fix(2)), CShort(Fix((-2)))}
      Yield New Object() {Nothing, CUInt(2), CLng(Fix((-2)))}
      Yield New Object() {Nothing, 2, -2}
      Yield New Object() {Nothing, CLng(Fix(2)), CLng(Fix((-2)))}
      Yield New Object() {Nothing, CULng(2), CDec((-2))}
      Yield New Object() {Nothing, CSng(2), CSng((-2))}
      Yield New Object() {Nothing, CDbl(2), CDbl((-2))}
      Yield New Object() {Nothing, CDec(2), CDec((-2))}
      Yield New Object() {Nothing, "2", CDbl((-2))}
      Yield New Object() {Nothing, False, CShort(Fix(0))}
      Yield New Object() {Nothing, New DateTime(10), New TimeSpan(-10)}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New SubtractObject, 2, "custom"}
      Yield New Object() {2, New SubtractObject, "motsuc"}
      Yield New Object() {New SubtractObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New SubtractObject, "tcejbomotsuc"}

      ' DateTime.
      Yield New Object() {New DateTime(10), New TimeSpan(5), New DateTime(5)}
      Yield New Object() {New DateTime(10), New DateTime(5), New TimeSpan(5)}
      Yield New Object() {New DateTime(10), Nothing, New TimeSpan(10)}

    End Function

    <Theory>
    <MemberData(NameOf(SubtractObject_TestData))>
    Public Sub SubtractObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.SubtractObject(left, right))
    End Sub

    Public Shared Iterator Function SubtractObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(SubtractObject_InvalidObjects_TestData))>
    Public Sub SubtractObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.SubtractObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.SubtractObject(right, left))
    End Sub

    Public Shared Iterator Function SubtractObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New SubtractObject, New Object}
      Yield New Object() {New Object, New SubtractObject}
      Yield New Object() {New SubtractObject, New SubtractObject}
    End Function

    <Theory>
    <MemberData(NameOf(SubtractObject_MismatchingObjects_TestData))>
    Public Sub SubtractObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.SubtractObject(left, right))
    End Sub

    Public Class SubtractObject

      Public Shared Operator -(left As SubtractObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator -(left As Integer, right As SubtractObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator -(left As SubtractObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator -(left As OperatorsTests, right As SubtractObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Shared Iterator Function XorObject_TestData() As IEnumerable(Of Object())

      ' byte.
      Yield New Object() {CByte(10), CByte(14), CByte(4)}
      Yield New Object() {CByte(10), CType(14, ByteEnum), CByte(4)}
      Yield New Object() {CByte(10), CSByte(14), CShort(Fix(4))}
      Yield New Object() {CByte(10), CType(14, SByteEnum), CShort(Fix(4))}
      Yield New Object() {CByte(10), CUShort(14), CUShort(4)}
      Yield New Object() {CByte(10), CType(14, UShortEnum), CUShort(4)}
      Yield New Object() {CByte(10), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CByte(10), CType(14, ShortEnum), CShort(Fix(4))}
      Yield New Object() {CByte(10), CUInt(14), CUInt(4)}
      Yield New Object() {CByte(10), CType(14, UIntEnum), CUInt(4)}
      Yield New Object() {CByte(10), 14, 4}
      Yield New Object() {CByte(10), CType(14, IntEnum), 4}
      Yield New Object() {CByte(10), CULng(14), CULng(4)}
      Yield New Object() {CByte(10), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CByte(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CByte(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CByte(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CByte(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CByte(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CByte(10), "14", CLng(Fix(4))}
      Yield New Object() {CByte(10), True, CShort(Fix(-11))}
      Yield New Object() {CByte(10), Nothing, CByte(10)}

      Yield New Object() {CType(10, ByteEnum), CByte(14), CByte(4)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ByteEnum), CType(4, ByteEnum)}
      Yield New Object() {CType(10, ByteEnum), CSByte(14), CShort(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CType(14, SByteEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CUShort(14), CUShort(4)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UShortEnum), CUShort(4)}
      Yield New Object() {CType(10, ByteEnum), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CType(14, ShortEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CUInt(14), CUInt(4)}
      Yield New Object() {CType(10, ByteEnum), CType(14, UIntEnum), CUInt(4)}
      Yield New Object() {CType(10, ByteEnum), 14, 4}
      Yield New Object() {CType(10, ByteEnum), CType(14, IntEnum), 4}
      Yield New Object() {CType(10, ByteEnum), CULng(14), CULng(4)}
      Yield New Object() {CType(10, ByteEnum), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CType(10, ByteEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, ByteEnum), True, CShort(Fix(-11))}
      Yield New Object() {CType(10, ByteEnum), Nothing, CType(10, ByteEnum)}

      ' sbyte.
      Yield New Object() {CSByte(10), CByte(14), CShort(Fix(4))}
      Yield New Object() {CSByte(10), CType(14, ByteEnum), CShort(Fix(4))}
      Yield New Object() {CSByte(10), CSByte(14), CSByte(4)}
      Yield New Object() {CSByte(10), CType(14, SByteEnum), CSByte(4)}
      Yield New Object() {CSByte(10), CUShort(14), 4}
      Yield New Object() {CSByte(10), CType(14, UShortEnum), 4}
      Yield New Object() {CSByte(10), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CSByte(10), CType(14, ShortEnum), CShort(Fix(4))}
      Yield New Object() {CSByte(10), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CSByte(10), 14, 4}
      Yield New Object() {CSByte(10), CType(14, IntEnum), 4}
      Yield New Object() {CSByte(10), CULng(14), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CSByte(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CSByte(10), "14", CLng(Fix(4))}
      Yield New Object() {CSByte(10), True, CSByte(-11)}
      Yield New Object() {CSByte(10), Nothing, CSByte(10)}

      Yield New Object() {CType(10, SByteEnum), CByte(14), CShort(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ByteEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CSByte(14), CSByte(4)}
      Yield New Object() {CType(10, SByteEnum), CType(14, SByteEnum), CType(4, SByteEnum)}
      Yield New Object() {CType(10, SByteEnum), CUShort(14), 4}
      Yield New Object() {CType(10, SByteEnum), CType(14, UShortEnum), 4}
      Yield New Object() {CType(10, SByteEnum), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ShortEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), 14, 4}
      Yield New Object() {CType(10, SByteEnum), CType(14, IntEnum), 4}
      Yield New Object() {CType(10, SByteEnum), CULng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, SByteEnum), True, CSByte(-11)}
      Yield New Object() {CType(10, SByteEnum), Nothing, CType(10, SByteEnum)}

      ' ushort.
      Yield New Object() {CUShort(10), CByte(14), CUShort(4)}
      Yield New Object() {CUShort(10), CType(14, ByteEnum), CUShort(4)}
      Yield New Object() {CUShort(10), CSByte(14), 4}
      Yield New Object() {CUShort(10), CType(14, SByteEnum), 4}
      Yield New Object() {CUShort(10), CUShort(14), CUShort(4)}
      Yield New Object() {CUShort(10), CType(14, UShortEnum), CUShort(4)}
      Yield New Object() {CUShort(10), CShort(Fix(14)), 4}
      Yield New Object() {CUShort(10), CType(14, ShortEnum), 4}
      Yield New Object() {CUShort(10), CUInt(14), CUInt(4)}
      Yield New Object() {CUShort(10), CType(14, UIntEnum), CUInt(4)}
      Yield New Object() {CUShort(10), 14, 4}
      Yield New Object() {CUShort(10), CType(14, IntEnum), 4}
      Yield New Object() {CUShort(10), CULng(14), CULng(4)}
      Yield New Object() {CUShort(10), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CUShort(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CUShort(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CUShort(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CUShort(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CUShort(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CUShort(10), "14", CLng(Fix(4))}
      Yield New Object() {CUShort(10), True, -11}
      Yield New Object() {CUShort(10), Nothing, CUShort(10)}

      Yield New Object() {CType(10, UShortEnum), CByte(14), CUShort(4)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ByteEnum), CUShort(4)}
      Yield New Object() {CType(10, UShortEnum), CSByte(14), 4}
      Yield New Object() {CType(10, UShortEnum), CType(14, SByteEnum), 4}
      Yield New Object() {CType(10, UShortEnum), CUShort(14), CUShort(4)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UShortEnum), CType(4, UShortEnum)}
      Yield New Object() {CType(10, UShortEnum), CShort(Fix(14)), 4}
      Yield New Object() {CType(10, UShortEnum), CType(14, ShortEnum), 4}
      Yield New Object() {CType(10, UShortEnum), CUInt(14), CUInt(4)}
      Yield New Object() {CType(10, UShortEnum), CType(14, UIntEnum), CUInt(4)}
      Yield New Object() {CType(10, UShortEnum), 14, 4}
      Yield New Object() {CType(10, UShortEnum), CType(14, IntEnum), 4}
      Yield New Object() {CType(10, UShortEnum), CULng(14), CULng(4)}
      Yield New Object() {CType(10, UShortEnum), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CType(10, UShortEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, UShortEnum), True, -11}
      Yield New Object() {CType(10, UShortEnum), Nothing, CType(10, UShortEnum)}

      ' short.
      Yield New Object() {CShort(Fix(10)), CByte(14), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, ByteEnum), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CSByte(14), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, SByteEnum), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CUShort(14), 4}
      Yield New Object() {CShort(Fix(10)), CType(14, UShortEnum), 4}
      Yield New Object() {CShort(Fix(10)), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, ShortEnum), CShort(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), 14, 4}
      Yield New Object() {CShort(Fix(10)), CType(14, IntEnum), 4}
      Yield New Object() {CShort(Fix(10)), CULng(14), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CSng(14), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), CDec(14), CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), "14", CLng(Fix(4))}
      Yield New Object() {CShort(Fix(10)), True, CShort(Fix(-11))}
      Yield New Object() {CShort(Fix(10)), Nothing, CShort(Fix(10))}

      Yield New Object() {CType(10, ShortEnum), CByte(14), CShort(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ByteEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CSByte(14), CShort(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, SByteEnum), CShort(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CUShort(14), 4}
      Yield New Object() {CType(10, ShortEnum), CType(14, UShortEnum), 4}
      Yield New Object() {CType(10, ShortEnum), CShort(Fix(14)), CShort(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ShortEnum), CType(4, ShortEnum)}
      Yield New Object() {CType(10, ShortEnum), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), 14, 4}
      Yield New Object() {CType(10, ShortEnum), CType(14, IntEnum), 4}
      Yield New Object() {CType(10, ShortEnum), CULng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, ShortEnum), True, CShort(Fix(-11))}
      Yield New Object() {CType(10, ShortEnum), Nothing, CType(10, ShortEnum)}

      ' uint.
      Yield New Object() {CUInt(10), CByte(14), CUInt(4)}
      Yield New Object() {CUInt(10), CType(14, ByteEnum), CUInt(4)}
      Yield New Object() {CUInt(10), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CUShort(14), CUInt(4)}
      Yield New Object() {CUInt(10), CType(14, UShortEnum), CUInt(4)}
      Yield New Object() {CUInt(10), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CUInt(14), CUInt(4)}
      Yield New Object() {CUInt(10), CType(14, UIntEnum), CUInt(4)}
      Yield New Object() {CUInt(10), 14, CLng(Fix(4))}
      Yield New Object() {CUInt(10), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CULng(14), CULng(4)}
      Yield New Object() {CUInt(10), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CUInt(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CUInt(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CUInt(10), "14", CLng(Fix(4))}
      Yield New Object() {CUInt(10), True, CLng(Fix(-11))}
      Yield New Object() {CUInt(10), Nothing, CUInt(10)}

      Yield New Object() {CType(10, UIntEnum), CByte(14), CUInt(4)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ByteEnum), CUInt(4)}
      Yield New Object() {CType(10, UIntEnum), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CUShort(14), CUInt(4)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UShortEnum), CUInt(4)}
      Yield New Object() {CType(10, UIntEnum), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CUInt(14), CUInt(4)}
      Yield New Object() {CType(10, UIntEnum), CType(14, UIntEnum), CType(4, UIntEnum)}
      Yield New Object() {CType(10, UIntEnum), 14, CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CULng(14), CULng(4)}
      Yield New Object() {CType(10, UIntEnum), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CType(10, UIntEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, UIntEnum), True, CLng(Fix(-11))}
      Yield New Object() {CType(10, UIntEnum), Nothing, CType(10, UIntEnum)}

      ' int.
      Yield New Object() {10, CByte(14), 4}
      Yield New Object() {10, CType(14, ByteEnum), 4}
      Yield New Object() {10, CSByte(14), 4}
      Yield New Object() {10, CType(14, SByteEnum), 4}
      Yield New Object() {10, CUShort(14), 4}
      Yield New Object() {10, CType(14, UShortEnum), 4}
      Yield New Object() {10, CShort(Fix(14)), 4}
      Yield New Object() {10, CType(14, ShortEnum), 4}
      Yield New Object() {10, CUInt(14), CLng(Fix(4))}
      Yield New Object() {10, CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {10, 14, 4}
      Yield New Object() {10, CType(14, IntEnum), 4}
      Yield New Object() {10, CULng(14), CLng(Fix(4))}
      Yield New Object() {10, CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {10, CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {10, CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {10, CSng(14), CLng(Fix(4))}
      Yield New Object() {10, CDbl(14), CLng(Fix(4))}
      Yield New Object() {10, CDec(14), CLng(Fix(4))}
      Yield New Object() {10, "14", CLng(Fix(4))}
      Yield New Object() {10, True, -11}
      Yield New Object() {10, Nothing, 10}

      Yield New Object() {CType(10, IntEnum), CByte(14), 4}
      Yield New Object() {CType(10, IntEnum), CType(14, ByteEnum), 4}
      Yield New Object() {CType(10, IntEnum), CSByte(14), 4}
      Yield New Object() {CType(10, IntEnum), CType(14, SByteEnum), 4}
      Yield New Object() {CType(10, IntEnum), CUShort(14), 4}
      Yield New Object() {CType(10, IntEnum), CType(14, UShortEnum), 4}
      Yield New Object() {CType(10, IntEnum), CShort(Fix(14)), 4}
      Yield New Object() {CType(10, IntEnum), CType(14, ShortEnum), 4}
      Yield New Object() {CType(10, IntEnum), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), 14, 4}
      Yield New Object() {CType(10, IntEnum), CType(14, IntEnum), CType(4, IntEnum)}
      Yield New Object() {CType(10, IntEnum), CULng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, IntEnum), True, -11}
      Yield New Object() {CType(10, IntEnum), Nothing, CType(10, IntEnum)}

      ' ulong.
      Yield New Object() {CULng(10), CByte(14), CULng(4)}
      Yield New Object() {CULng(10), CType(14, ByteEnum), CULng(4)}
      Yield New Object() {CULng(10), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CULng(10), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CULng(10), CUShort(14), CULng(4)}
      Yield New Object() {CULng(10), CType(14, UShortEnum), CULng(4)}
      Yield New Object() {CULng(10), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CULng(10), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CULng(10), CUInt(14), CULng(4)}
      Yield New Object() {CULng(10), CType(14, UIntEnum), CULng(4)}
      Yield New Object() {CULng(10), 14, CLng(Fix(4))}
      Yield New Object() {CULng(10), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CULng(10), CULng(14), CULng(4)}
      Yield New Object() {CULng(10), CType(14, ULongEnum), CULng(4)}
      Yield New Object() {CULng(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CULng(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CULng(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CULng(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CULng(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CULng(10), "14", CLng(Fix(4))}
      Yield New Object() {CULng(10), True, CLng(Fix(-11))}
      Yield New Object() {CULng(10), Nothing, CULng(10)}

      Yield New Object() {CType(10, ULongEnum), CByte(14), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ByteEnum), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CUShort(14), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UShortEnum), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CUInt(14), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CType(14, UIntEnum), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), 14, CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CULng(14), CULng(4)}
      Yield New Object() {CType(10, ULongEnum), CType(14, ULongEnum), CType(4, ULongEnum)}
      Yield New Object() {CType(10, ULongEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, ULongEnum), True, CLng(Fix(-11))}
      Yield New Object() {CType(10, ULongEnum), Nothing, CType(10, ULongEnum)}

      ' long.
      Yield New Object() {CLng(Fix(10)), CByte(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CUShort(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), 14, CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CULng(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CSng(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), CDec(14), CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), "14", CLng(Fix(4))}
      Yield New Object() {CLng(Fix(10)), True, CLng(Fix(-11))}
      Yield New Object() {CLng(Fix(10)), Nothing, CLng(Fix(10))}

      Yield New Object() {CType(10, LongEnum), CByte(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CUShort(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), 14, CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CULng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CType(14, LongEnum), CType(4, LongEnum)}
      Yield New Object() {CType(10, LongEnum), CSng(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), CDec(14), CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), "14", CLng(Fix(4))}
      Yield New Object() {CType(10, LongEnum), True, CLng(Fix(-11))}
      Yield New Object() {CType(10, LongEnum), Nothing, CType(10, LongEnum)}

      ' float.
      Yield New Object() {CSng(10), CByte(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CUShort(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), 14, CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CULng(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CSng(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CSng(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CSng(10), "14", CLng(Fix(4))}
      Yield New Object() {CSng(10), True, CLng(Fix(-11))}
      Yield New Object() {CSng(10), Nothing, CLng(Fix(10))}

      ' double.
      Yield New Object() {CDbl(10), CByte(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CUShort(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), 14, CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CULng(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CDbl(10), "14", CLng(Fix(4))}
      Yield New Object() {CDbl(10), True, CLng(Fix(-11))}
      Yield New Object() {CDbl(10), Nothing, CLng(Fix(10))}

      ' decimal.
      Yield New Object() {CDec(10), CByte(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CSByte(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CUShort(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CUInt(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), 14, CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CULng(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {CDec(10), CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {CDec(10), CSng(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CDbl(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), CDec(14), CLng(Fix(4))}
      Yield New Object() {CDec(10), "14", CLng(Fix(4))}
      Yield New Object() {CDec(10), True, CLng(Fix(-11))}
      Yield New Object() {CDec(10), Nothing, CLng(Fix(10))}

      ' string.
      Yield New Object() {"10", CByte(14), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, ByteEnum), CLng(Fix(4))}
      Yield New Object() {"10", CSByte(14), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, SByteEnum), CLng(Fix(4))}
      Yield New Object() {"10", CUShort(14), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, UShortEnum), CLng(Fix(4))}
      Yield New Object() {"10", CShort(Fix(14)), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, ShortEnum), CLng(Fix(4))}
      Yield New Object() {"10", CUInt(14), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, UIntEnum), CLng(Fix(4))}
      Yield New Object() {"10", 14, CLng(Fix(4))}
      Yield New Object() {"10", CType(14, IntEnum), CLng(Fix(4))}
      Yield New Object() {"10", CULng(14), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, ULongEnum), CLng(Fix(4))}
      Yield New Object() {"10", CLng(Fix(14)), CLng(Fix(4))}
      Yield New Object() {"10", CType(14, LongEnum), CLng(Fix(4))}
      Yield New Object() {"10", CSng(14), CLng(Fix(4))}
      Yield New Object() {"10", CDbl(14), CLng(Fix(4))}
      Yield New Object() {"10", CDec(14), CLng(Fix(4))}
      Yield New Object() {"10", "14", CLng(Fix(4))}
      Yield New Object() {"10", True, False}
      Yield New Object() {"10", Nothing, CLng(Fix(10))}

      ' bool.
      Yield New Object() {True, CByte(14), CShort(Fix(-15))}
      Yield New Object() {True, CType(14, ByteEnum), CShort(Fix(-15))}
      Yield New Object() {True, CSByte(14), CSByte(-15)}
      Yield New Object() {True, CType(14, SByteEnum), CSByte(-15)}
      Yield New Object() {True, CUShort(14), -15}
      Yield New Object() {True, CType(14, UShortEnum), -15}
      Yield New Object() {True, CShort(Fix(14)), CShort(Fix(-15))}
      Yield New Object() {True, CType(14, ShortEnum), CShort(Fix(-15))}
      Yield New Object() {True, CUInt(14), CLng(Fix(-15))}
      Yield New Object() {True, CType(14, UIntEnum), CLng(Fix(-15))}
      Yield New Object() {True, 14, -15}
      Yield New Object() {True, CType(14, IntEnum), -15}
      Yield New Object() {True, CULng(14), CLng(Fix(-15))}
      Yield New Object() {True, CType(14, ULongEnum), CLng(Fix(-15))}
      Yield New Object() {True, CLng(Fix(14)), CLng(Fix(-15))}
      Yield New Object() {True, CType(14, LongEnum), CLng(Fix(-15))}
      Yield New Object() {True, CSng(14), CLng(Fix(-15))}
      Yield New Object() {True, CDbl(14), CLng(Fix(-15))}
      Yield New Object() {True, CDec(14), CLng(Fix(-15))}
      Yield New Object() {True, "14", False}
      Yield New Object() {True, True, False}
      Yield New Object() {True, Nothing, True}

      ' null.
      Yield New Object() {Nothing, CByte(14), CByte(14)}
      Yield New Object() {Nothing, CType(14, ByteEnum), CType(14, ByteEnum)}
      Yield New Object() {Nothing, CSByte(14), CSByte(14)}
      Yield New Object() {Nothing, CType(14, SByteEnum), CType(14, SByteEnum)}
      Yield New Object() {Nothing, CUShort(14), CUShort(14)}
      Yield New Object() {Nothing, CType(14, UShortEnum), CType(14, UShortEnum)}
      Yield New Object() {Nothing, CShort(Fix(14)), CShort(Fix(14))}
      Yield New Object() {Nothing, CType(14, ShortEnum), CType(14, ShortEnum)}
      Yield New Object() {Nothing, CUInt(14), CUInt(14)}
      Yield New Object() {Nothing, CType(14, UIntEnum), CType(14, UIntEnum)}
      Yield New Object() {Nothing, 14, 14}
      Yield New Object() {Nothing, CType(14, IntEnum), CType(14, IntEnum)}
      Yield New Object() {Nothing, CULng(14), CULng(14)}
      Yield New Object() {Nothing, CType(14, ULongEnum), CType(14, ULongEnum)}
      Yield New Object() {Nothing, CLng(Fix(14)), CLng(Fix(14))}
      Yield New Object() {Nothing, CType(14, LongEnum), CType(14, LongEnum)}
      Yield New Object() {Nothing, CSng(14), CLng(Fix(14))}
      Yield New Object() {Nothing, CDbl(14), CLng(Fix(14))}
      Yield New Object() {Nothing, CDec(14), CLng(Fix(14))}
      Yield New Object() {Nothing, "14", CLng(Fix(14))}
      Yield New Object() {Nothing, True, True}
      Yield New Object() {Nothing, Nothing, 0}

      ' object.
      Yield New Object() {New XorObject, 2, "custom"}
      Yield New Object() {2, New XorObject, "motsuc"}
      Yield New Object() {New XorObject, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New XorObject, "tcejbomotsuc"}

    End Function

    <Theory>
    <MemberData(NameOf(XorObject_TestData))>
    Public Sub XorObject_Invoke_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.XorObject(left, right))
    End Sub

    Public Shared Iterator Function XorObject_InvalidObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {1, "2"c}
      Yield New Object() {2, DBNull.Value}
      Yield New Object() {"3"c, New Object}
    End Function

    <Theory>
    <MemberData(NameOf(XorObject_InvalidObjects_TestData))>
    Public Sub XorObject_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.XorObject(left, right))
      Assert.Throws(Of InvalidCastException)(Function() Operators.XorObject(right, left))
    End Sub

    Public Shared Iterator Function XorObject_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New XorObject, New Object}
      Yield New Object() {New Object, New XorObject}
      Yield New Object() {New XorObject, New XorObject}
    End Function

    <Theory>
    <MemberData(NameOf(XorObject_MismatchingObjects_TestData))>
    Public Sub XorObject_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.XorObject(left, right))
    End Sub

    Public Class XorObject

      Public Shared Operator Xor(left As XorObject, right As Integer) As String
        If left IsNot Nothing OrElse
           right <> 0 Then
        End If
        Return "custom"
      End Operator

      Public Shared Operator Xor(left As Integer, right As XorObject) As String
        If left <> 0 OrElse
           right IsNot Nothing Then
        End If
        Return "motsuc"
      End Operator

      Public Shared Operator Xor(left As XorObject, right As OperatorsTests) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "customobject"
      End Operator

      Public Shared Operator Xor(left As OperatorsTests, right As XorObject) As String
        If left IsNot Nothing OrElse
           right IsNot Nothing Then
        End If
        Return "tcejbomotsuc"
      End Operator

    End Class

    Public Enum ByteEnum As Byte
      Value = 1
    End Enum

    Public Enum ByteEnum2 As Byte
      Value = 1
    End Enum

    Public Enum SByteEnum As SByte
      Value = 1
    End Enum

    Public Enum SByteEnum2 As SByte
      Value = 1
    End Enum

    Public Enum UShortEnum As UShort
      Value = 1
    End Enum

    Public Enum UShortEnum2 As UShort
      Value = 1
    End Enum
    Public Enum ShortEnum As Short
      Value = 1
    End Enum

    Public Enum ShortEnum2 As Short
      Value = 1
    End Enum

    Public Enum UIntEnum As UInteger
      Value = 1
    End Enum

    Public Enum UIntEnum2 As UInteger
      Value = 1
    End Enum

    Public Enum IntEnum As Integer
      Value = 1
    End Enum

    Public Enum IntEnum2 As Integer
      Value = 1
    End Enum

    Public Enum ULongEnum As ULong
      Value = 1
    End Enum

    Public Enum ULongEnum2 As ULong
      Value = 1
    End Enum

    Public Enum LongEnum As [Int64]
      Value = 1
    End Enum

    Public Enum LongEnum2 As [Int64]
      Value = 1
    End Enum

  End Class

End Namespace