' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Community.VisualBasic.Tests
Imports Xunit

Namespace Global.Community.VisualBasic.CompilerServices.Tests

  Partial Public Class OperatorsTests

    Public Shared Iterator Function Compare_Primitives_TestData() As IEnumerable(Of Object())
      ' byte.
      Yield New Object() {CByte(8), CByte(7), True, False, False}
      Yield New Object() {CByte(8), CByte(8), False, True, False}
      Yield New Object() {CByte(8), CByte(9), False, False, True}
      Yield New Object() {CByte(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CByte(8), CSByte(7), True, False, False}
      Yield New Object() {CByte(8), CSByte(8), False, True, False}
      Yield New Object() {CByte(8), CSByte(9), False, False, True}
      Yield New Object() {CByte(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CByte(8), CUShort(7), True, False, False}
      Yield New Object() {CByte(8), CUShort(8), False, True, False}
      Yield New Object() {CByte(8), CUShort(9), False, False, True}
      Yield New Object() {CByte(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CByte(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CByte(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CByte(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CByte(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CByte(8), CUInt(7), True, False, False}
      Yield New Object() {CByte(8), CUInt(8), False, True, False}
      Yield New Object() {CByte(8), CUInt(9), False, False, True}
      Yield New Object() {CByte(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CByte(8), 7, True, False, False}
      Yield New Object() {CByte(8), 8, False, True, False}
      Yield New Object() {CByte(8), 9, False, False, True}
      Yield New Object() {CByte(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CByte(8), CULng(7), True, False, False}
      Yield New Object() {CByte(8), CULng(8), False, True, False}
      Yield New Object() {CByte(8), CULng(9), False, False, True}
      Yield New Object() {CByte(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CByte(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CByte(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CByte(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CByte(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CByte(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CByte(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CByte(8), CSng(7), True, False, False}
      Yield New Object() {CByte(8), CSng(8), False, True, False}
      Yield New Object() {CByte(8), CSng(9), False, False, True}
      Yield New Object() {CByte(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CByte(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CByte(8), Single.NaN, False, False, False}
      Yield New Object() {CByte(8), CDbl(7), True, False, False}
      Yield New Object() {CByte(8), CDbl(8), False, True, False}
      Yield New Object() {CByte(8), CDbl(9), False, False, True}
      Yield New Object() {CByte(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CByte(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CByte(8), Double.NaN, False, False, False}
      Yield New Object() {CByte(8), CDec(7), True, False, False}
      Yield New Object() {CByte(8), CDec(8), False, True, False}
      Yield New Object() {CByte(8), CDec(9), False, False, True}
      Yield New Object() {CByte(8), "7", True, False, False}
      Yield New Object() {CByte(8), "8", False, True, False}
      Yield New Object() {CByte(8), "9", False, False, True}
      Yield New Object() {CByte(8), True, True, False, False}
      Yield New Object() {CByte(8), False, True, False, False}
      Yield New Object() {CByte(8), Nothing, True, False, False}

      ' sbyte.
      Yield New Object() {CSByte(8), CByte(7), True, False, False}
      Yield New Object() {CSByte(8), CByte(8), False, True, False}
      Yield New Object() {CSByte(8), CByte(9), False, False, True}
      Yield New Object() {CSByte(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CSByte(8), CSByte(7), True, False, False}
      Yield New Object() {CSByte(8), CSByte(8), False, True, False}
      Yield New Object() {CSByte(8), CSByte(9), False, False, True}
      Yield New Object() {CSByte(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CSByte(8), CUShort(7), True, False, False}
      Yield New Object() {CSByte(8), CUShort(8), False, True, False}
      Yield New Object() {CSByte(8), CUShort(9), False, False, True}
      Yield New Object() {CSByte(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CSByte(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CSByte(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CSByte(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CSByte(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CSByte(8), CUInt(7), True, False, False}
      Yield New Object() {CSByte(8), CUInt(8), False, True, False}
      Yield New Object() {CSByte(8), CUInt(9), False, False, True}
      Yield New Object() {CSByte(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CSByte(8), 7, True, False, False}
      Yield New Object() {CSByte(8), 8, False, True, False}
      Yield New Object() {CSByte(8), 9, False, False, True}
      Yield New Object() {CSByte(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CSByte(8), CULng(7), True, False, False}
      Yield New Object() {CSByte(8), CULng(8), False, True, False}
      Yield New Object() {CSByte(8), CULng(9), False, False, True}
      Yield New Object() {CSByte(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CSByte(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CSByte(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CSByte(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CSByte(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CSByte(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CSByte(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CSByte(8), CSng(7), True, False, False}
      Yield New Object() {CSByte(8), CSng(8), False, True, False}
      Yield New Object() {CSByte(8), CSng(9), False, False, True}
      Yield New Object() {CSByte(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CSByte(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CSByte(8), Single.NaN, False, False, False}
      Yield New Object() {CSByte(8), CDbl(7), True, False, False}
      Yield New Object() {CSByte(8), CDbl(8), False, True, False}
      Yield New Object() {CSByte(8), CDbl(9), False, False, True}
      Yield New Object() {CSByte(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CSByte(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CSByte(8), Double.NaN, False, False, False}
      Yield New Object() {CSByte(8), CDec(7), True, False, False}
      Yield New Object() {CSByte(8), CDec(8), False, True, False}
      Yield New Object() {CSByte(8), CDec(9), False, False, True}
      Yield New Object() {CSByte(8), "7", True, False, False}
      Yield New Object() {CSByte(8), "8", False, True, False}
      Yield New Object() {CSByte(8), "9", False, False, True}
      Yield New Object() {CSByte(8), True, True, False, False}
      Yield New Object() {CSByte(8), False, True, False, False}
      Yield New Object() {CSByte(8), Nothing, True, False, False}

      ' ushort.
      Yield New Object() {CUShort(8), CByte(7), True, False, False}
      Yield New Object() {CUShort(8), CByte(8), False, True, False}
      Yield New Object() {CUShort(8), CByte(9), False, False, True}
      Yield New Object() {CUShort(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CUShort(8), CSByte(7), True, False, False}
      Yield New Object() {CUShort(8), CSByte(8), False, True, False}
      Yield New Object() {CUShort(8), CSByte(9), False, False, True}
      Yield New Object() {CUShort(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CUShort(8), CUShort(7), True, False, False}
      Yield New Object() {CUShort(8), CUShort(8), False, True, False}
      Yield New Object() {CUShort(8), CUShort(9), False, False, True}
      Yield New Object() {CUShort(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CUShort(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CUShort(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CUShort(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CUShort(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CUShort(8), CUInt(7), True, False, False}
      Yield New Object() {CUShort(8), CUInt(8), False, True, False}
      Yield New Object() {CUShort(8), CUInt(9), False, False, True}
      Yield New Object() {CUShort(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CUShort(8), 7, True, False, False}
      Yield New Object() {CUShort(8), 8, False, True, False}
      Yield New Object() {CUShort(8), 9, False, False, True}
      Yield New Object() {CUShort(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CUShort(8), CULng(7), True, False, False}
      Yield New Object() {CUShort(8), CULng(8), False, True, False}
      Yield New Object() {CUShort(8), CULng(9), False, False, True}
      Yield New Object() {CUShort(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CUShort(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CUShort(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CUShort(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CUShort(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CUShort(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CUShort(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CUShort(8), CSng(7), True, False, False}
      Yield New Object() {CUShort(8), CSng(8), False, True, False}
      Yield New Object() {CUShort(8), CSng(9), False, False, True}
      Yield New Object() {CUShort(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CUShort(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CUShort(8), Single.NaN, False, False, False}
      Yield New Object() {CUShort(8), CDbl(7), True, False, False}
      Yield New Object() {CUShort(8), CDbl(8), False, True, False}
      Yield New Object() {CUShort(8), CDbl(9), False, False, True}
      Yield New Object() {CUShort(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CUShort(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CUShort(8), Double.NaN, False, False, False}
      Yield New Object() {CUShort(8), CDec(7), True, False, False}
      Yield New Object() {CUShort(8), CDec(8), False, True, False}
      Yield New Object() {CUShort(8), CDec(9), False, False, True}
      Yield New Object() {CUShort(8), "7", True, False, False}
      Yield New Object() {CUShort(8), "8", False, True, False}
      Yield New Object() {CUShort(8), "9", False, False, True}
      Yield New Object() {CUShort(8), True, True, False, False}
      Yield New Object() {CUShort(8), False, True, False, False}
      Yield New Object() {CUShort(8), Nothing, True, False, False}

      ' short.
      Yield New Object() {CShort(Fix(8)), CByte(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CByte(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CByte(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CSByte(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CSByte(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CSByte(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CUShort(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CUShort(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CUShort(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CShort(Fix(7)), True, False, False}
      Yield New Object() {CShort(Fix(8)), CShort(Fix(8)), False, True, False}
      Yield New Object() {CShort(Fix(8)), CShort(Fix(9)), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CUInt(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CUInt(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CUInt(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), 7, True, False, False}
      Yield New Object() {CShort(Fix(8)), 8, False, True, False}
      Yield New Object() {CShort(Fix(8)), 9, False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, IntEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, IntEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, IntEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CULng(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CULng(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CULng(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CLng(Fix(7)), True, False, False}
      Yield New Object() {CShort(Fix(8)), CLng(Fix(8)), False, True, False}
      Yield New Object() {CShort(Fix(8)), CLng(Fix(9)), False, False, True}
      Yield New Object() {CShort(Fix(8)), CType(7, LongEnum), True, False, False}
      Yield New Object() {CShort(Fix(8)), CType(8, LongEnum), False, True, False}
      Yield New Object() {CShort(Fix(8)), CType(9, LongEnum), False, False, True}
      Yield New Object() {CShort(Fix(8)), CSng(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CSng(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CSng(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CShort(Fix(8)), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CShort(Fix(8)), Single.NaN, False, False, False}
      Yield New Object() {CShort(Fix(8)), CDbl(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CDbl(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CDbl(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CShort(Fix(8)), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CShort(Fix(8)), Double.NaN, False, False, False}
      Yield New Object() {CShort(Fix(8)), CDec(7), True, False, False}
      Yield New Object() {CShort(Fix(8)), CDec(8), False, True, False}
      Yield New Object() {CShort(Fix(8)), CDec(9), False, False, True}
      Yield New Object() {CShort(Fix(8)), "7", True, False, False}
      Yield New Object() {CShort(Fix(8)), "8", False, True, False}
      Yield New Object() {CShort(Fix(8)), "9", False, False, True}
      Yield New Object() {CShort(Fix(8)), True, True, False, False}
      Yield New Object() {CShort(Fix(8)), False, True, False, False}
      Yield New Object() {CShort(Fix(8)), Nothing, True, False, False}

      ' uint.
      Yield New Object() {CUInt(8), CByte(7), True, False, False}
      Yield New Object() {CUInt(8), CByte(8), False, True, False}
      Yield New Object() {CUInt(8), CByte(9), False, False, True}
      Yield New Object() {CUInt(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CUInt(8), CSByte(7), True, False, False}
      Yield New Object() {CUInt(8), CSByte(8), False, True, False}
      Yield New Object() {CUInt(8), CSByte(9), False, False, True}
      Yield New Object() {CUInt(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CUInt(8), CUShort(7), True, False, False}
      Yield New Object() {CUInt(8), CUShort(8), False, True, False}
      Yield New Object() {CUInt(8), CUShort(9), False, False, True}
      Yield New Object() {CUInt(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CUInt(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CUInt(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CUInt(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CUInt(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CUInt(8), CUInt(7), True, False, False}
      Yield New Object() {CUInt(8), CUInt(8), False, True, False}
      Yield New Object() {CUInt(8), CUInt(9), False, False, True}
      Yield New Object() {CUInt(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CUInt(8), 7, True, False, False}
      Yield New Object() {CUInt(8), 8, False, True, False}
      Yield New Object() {CUInt(8), 9, False, False, True}
      Yield New Object() {CUInt(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CUInt(8), CULng(7), True, False, False}
      Yield New Object() {CUInt(8), CULng(8), False, True, False}
      Yield New Object() {CUInt(8), CULng(9), False, False, True}
      Yield New Object() {CUInt(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CUInt(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CUInt(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CUInt(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CUInt(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CUInt(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CUInt(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CUInt(8), CSng(7), True, False, False}
      Yield New Object() {CUInt(8), CSng(8), False, True, False}
      Yield New Object() {CUInt(8), CSng(9), False, False, True}
      Yield New Object() {CUInt(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CUInt(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CUInt(8), Single.NaN, False, False, False}
      Yield New Object() {CUInt(8), CDbl(7), True, False, False}
      Yield New Object() {CUInt(8), CDbl(8), False, True, False}
      Yield New Object() {CUInt(8), CDbl(9), False, False, True}
      Yield New Object() {CUInt(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CUInt(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CUInt(8), Double.NaN, False, False, False}
      Yield New Object() {CUInt(8), CDec(7), True, False, False}
      Yield New Object() {CUInt(8), CDec(8), False, True, False}
      Yield New Object() {CUInt(8), CDec(9), False, False, True}
      Yield New Object() {CUInt(8), "7", True, False, False}
      Yield New Object() {CUInt(8), "8", False, True, False}
      Yield New Object() {CUInt(8), "9", False, False, True}
      Yield New Object() {CUInt(8), True, True, False, False}
      Yield New Object() {CUInt(8), False, True, False, False}
      Yield New Object() {CUInt(8), Nothing, True, False, False}

      ' int.
      Yield New Object() {8, CByte(7), True, False, False}
      Yield New Object() {8, CByte(8), False, True, False}
      Yield New Object() {8, CByte(9), False, False, True}
      Yield New Object() {8, CType(7, ByteEnum), True, False, False}
      Yield New Object() {8, CType(8, ByteEnum), False, True, False}
      Yield New Object() {8, CType(9, ByteEnum), False, False, True}
      Yield New Object() {8, CSByte(7), True, False, False}
      Yield New Object() {8, CSByte(8), False, True, False}
      Yield New Object() {8, CSByte(9), False, False, True}
      Yield New Object() {8, CType(7, SByteEnum), True, False, False}
      Yield New Object() {8, CType(8, SByteEnum), False, True, False}
      Yield New Object() {8, CType(9, SByteEnum), False, False, True}
      Yield New Object() {8, CUShort(7), True, False, False}
      Yield New Object() {8, CUShort(8), False, True, False}
      Yield New Object() {8, CUShort(9), False, False, True}
      Yield New Object() {8, CType(7, UShortEnum), True, False, False}
      Yield New Object() {8, CType(8, UShortEnum), False, True, False}
      Yield New Object() {8, CType(9, UShortEnum), False, False, True}
      Yield New Object() {8, CShort(Fix(7)), True, False, False}
      Yield New Object() {8, CShort(Fix(8)), False, True, False}
      Yield New Object() {8, CShort(Fix(9)), False, False, True}
      Yield New Object() {8, CType(7, ShortEnum), True, False, False}
      Yield New Object() {8, CType(8, ShortEnum), False, True, False}
      Yield New Object() {8, CType(9, ShortEnum), False, False, True}
      Yield New Object() {8, CUInt(7), True, False, False}
      Yield New Object() {8, CUInt(8), False, True, False}
      Yield New Object() {8, CUInt(9), False, False, True}
      Yield New Object() {8, CType(7, UIntEnum), True, False, False}
      Yield New Object() {8, CType(8, UIntEnum), False, True, False}
      Yield New Object() {8, CType(9, UIntEnum), False, False, True}
      Yield New Object() {8, 7, True, False, False}
      Yield New Object() {8, 8, False, True, False}
      Yield New Object() {8, 9, False, False, True}
      Yield New Object() {8, CType(7, IntEnum), True, False, False}
      Yield New Object() {8, CType(8, IntEnum), False, True, False}
      Yield New Object() {8, CType(9, IntEnum), False, False, True}
      Yield New Object() {8, CULng(7), True, False, False}
      Yield New Object() {8, CULng(8), False, True, False}
      Yield New Object() {8, CULng(9), False, False, True}
      Yield New Object() {8, CType(7, ULongEnum), True, False, False}
      Yield New Object() {8, CType(8, ULongEnum), False, True, False}
      Yield New Object() {8, CType(9, ULongEnum), False, False, True}
      Yield New Object() {8, CLng(Fix(7)), True, False, False}
      Yield New Object() {8, CLng(Fix(8)), False, True, False}
      Yield New Object() {8, CLng(Fix(9)), False, False, True}
      Yield New Object() {8, CType(7, LongEnum), True, False, False}
      Yield New Object() {8, CType(8, LongEnum), False, True, False}
      Yield New Object() {8, CType(9, LongEnum), False, False, True}
      Yield New Object() {8, CSng(7), True, False, False}
      Yield New Object() {8, CSng(8), False, True, False}
      Yield New Object() {8, CSng(9), False, False, True}
      Yield New Object() {8, Single.PositiveInfinity, False, False, True}
      Yield New Object() {8, Single.NegativeInfinity, True, False, False}
      Yield New Object() {8, Single.NaN, False, False, False}
      Yield New Object() {8, CDbl(7), True, False, False}
      Yield New Object() {8, CDbl(8), False, True, False}
      Yield New Object() {8, CDbl(9), False, False, True}
      Yield New Object() {8, Double.PositiveInfinity, False, False, True}
      Yield New Object() {8, Double.NegativeInfinity, True, False, False}
      Yield New Object() {8, Double.NaN, False, False, False}
      Yield New Object() {8, CDec(7), True, False, False}
      Yield New Object() {8, CDec(8), False, True, False}
      Yield New Object() {8, CDec(9), False, False, True}
      Yield New Object() {8, "7", True, False, False}
      Yield New Object() {8, "8", False, True, False}
      Yield New Object() {8, "9", False, False, True}
      Yield New Object() {8, True, True, False, False}
      Yield New Object() {8, False, True, False, False}
      Yield New Object() {8, Nothing, True, False, False}

      ' ulong.
      Yield New Object() {CULng(8), CByte(7), True, False, False}
      Yield New Object() {CULng(8), CByte(8), False, True, False}
      Yield New Object() {CULng(8), CByte(9), False, False, True}
      Yield New Object() {CULng(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CULng(8), CSByte(7), True, False, False}
      Yield New Object() {CULng(8), CSByte(8), False, True, False}
      Yield New Object() {CULng(8), CSByte(9), False, False, True}
      Yield New Object() {CULng(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CULng(8), CUShort(7), True, False, False}
      Yield New Object() {CULng(8), CUShort(8), False, True, False}
      Yield New Object() {CULng(8), CUShort(9), False, False, True}
      Yield New Object() {CULng(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CULng(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CULng(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CULng(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CULng(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CULng(8), CUInt(7), True, False, False}
      Yield New Object() {CULng(8), CUInt(8), False, True, False}
      Yield New Object() {CULng(8), CUInt(9), False, False, True}
      Yield New Object() {CULng(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CULng(8), 7, True, False, False}
      Yield New Object() {CULng(8), 8, False, True, False}
      Yield New Object() {CULng(8), 9, False, False, True}
      Yield New Object() {CULng(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CULng(8), CULng(7), True, False, False}
      Yield New Object() {CULng(8), CULng(8), False, True, False}
      Yield New Object() {CULng(8), CULng(9), False, False, True}
      Yield New Object() {CULng(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CULng(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CULng(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CULng(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CULng(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CULng(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CULng(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CULng(8), CSng(7), True, False, False}
      Yield New Object() {CULng(8), CSng(8), False, True, False}
      Yield New Object() {CULng(8), CSng(9), False, False, True}
      Yield New Object() {CULng(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CULng(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CULng(8), Single.NaN, False, False, False}
      Yield New Object() {CULng(8), CDbl(7), True, False, False}
      Yield New Object() {CULng(8), CDbl(8), False, True, False}
      Yield New Object() {CULng(8), CDbl(9), False, False, True}
      Yield New Object() {CULng(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CULng(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CULng(8), Double.NaN, False, False, False}
      Yield New Object() {CULng(8), CDec(7), True, False, False}
      Yield New Object() {CULng(8), CDec(8), False, True, False}
      Yield New Object() {CULng(8), CDec(9), False, False, True}
      Yield New Object() {CULng(8), "7", True, False, False}
      Yield New Object() {CULng(8), "8", False, True, False}
      Yield New Object() {CULng(8), "9", False, False, True}
      Yield New Object() {CULng(8), True, True, False, False}
      Yield New Object() {CULng(8), False, True, False, False}
      Yield New Object() {CULng(8), Nothing, True, False, False}

      ' long.
      Yield New Object() {CLng(Fix(8)), CByte(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CByte(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CByte(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CSByte(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CSByte(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CSByte(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CUShort(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CUShort(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CUShort(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CShort(Fix(7)), True, False, False}
      Yield New Object() {CLng(Fix(8)), CShort(Fix(8)), False, True, False}
      Yield New Object() {CLng(Fix(8)), CShort(Fix(9)), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CUInt(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CUInt(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CUInt(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), 7, True, False, False}
      Yield New Object() {CLng(Fix(8)), 8, False, True, False}
      Yield New Object() {CLng(Fix(8)), 9, False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, IntEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, IntEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, IntEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CULng(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CULng(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CULng(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CLng(Fix(7)), True, False, False}
      Yield New Object() {CLng(Fix(8)), CLng(Fix(8)), False, True, False}
      Yield New Object() {CLng(Fix(8)), CLng(Fix(9)), False, False, True}
      Yield New Object() {CLng(Fix(8)), CType(7, LongEnum), True, False, False}
      Yield New Object() {CLng(Fix(8)), CType(8, LongEnum), False, True, False}
      Yield New Object() {CLng(Fix(8)), CType(9, LongEnum), False, False, True}
      Yield New Object() {CLng(Fix(8)), CSng(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CSng(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CSng(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CLng(Fix(8)), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CLng(Fix(8)), Single.NaN, False, False, False}
      Yield New Object() {CLng(Fix(8)), CDbl(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CDbl(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CDbl(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CLng(Fix(8)), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CLng(Fix(8)), Double.NaN, False, False, False}
      Yield New Object() {CLng(Fix(8)), CDec(7), True, False, False}
      Yield New Object() {CLng(Fix(8)), CDec(8), False, True, False}
      Yield New Object() {CLng(Fix(8)), CDec(9), False, False, True}
      Yield New Object() {CLng(Fix(8)), "7", True, False, False}
      Yield New Object() {CLng(Fix(8)), "8", False, True, False}
      Yield New Object() {CLng(Fix(8)), "9", False, False, True}
      Yield New Object() {CLng(Fix(8)), True, True, False, False}
      Yield New Object() {CLng(Fix(8)), False, True, False, False}
      Yield New Object() {CLng(Fix(8)), Nothing, True, False, False}

      ' float.
      Yield New Object() {CSng(8), CByte(7), True, False, False}
      Yield New Object() {CSng(8), CByte(8), False, True, False}
      Yield New Object() {CSng(8), CByte(9), False, False, True}
      Yield New Object() {CSng(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CSng(8), CSByte(7), True, False, False}
      Yield New Object() {CSng(8), CSByte(8), False, True, False}
      Yield New Object() {CSng(8), CSByte(9), False, False, True}
      Yield New Object() {CSng(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CSng(8), CUShort(7), True, False, False}
      Yield New Object() {CSng(8), CUShort(8), False, True, False}
      Yield New Object() {CSng(8), CUShort(9), False, False, True}
      Yield New Object() {CSng(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CSng(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CSng(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CSng(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CSng(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CSng(8), CUInt(7), True, False, False}
      Yield New Object() {CSng(8), CUInt(8), False, True, False}
      Yield New Object() {CSng(8), CUInt(9), False, False, True}
      Yield New Object() {CSng(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CSng(8), 7, True, False, False}
      Yield New Object() {CSng(8), 8, False, True, False}
      Yield New Object() {CSng(8), 9, False, False, True}
      Yield New Object() {CSng(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CSng(8), CULng(7), True, False, False}
      Yield New Object() {CSng(8), CULng(8), False, True, False}
      Yield New Object() {CSng(8), CULng(9), False, False, True}
      Yield New Object() {CSng(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CSng(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CSng(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CSng(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CSng(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CSng(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CSng(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CSng(8), CSng(7), True, False, False}
      Yield New Object() {CSng(8), CSng(8), False, True, False}
      Yield New Object() {CSng(8), CSng(9), False, False, True}
      Yield New Object() {CSng(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CSng(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CSng(8), Single.NaN, False, False, False}
      Yield New Object() {CSng(8), CDbl(7), True, False, False}
      Yield New Object() {CSng(8), CDbl(8), False, True, False}
      Yield New Object() {CSng(8), CDbl(9), False, False, True}
      Yield New Object() {CSng(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CSng(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CSng(8), Double.NaN, False, False, False}
      Yield New Object() {CSng(8), CDec(7), True, False, False}
      Yield New Object() {CSng(8), CDec(8), False, True, False}
      Yield New Object() {CSng(8), CDec(9), False, False, True}
      Yield New Object() {CSng(8), "7", True, False, False}
      Yield New Object() {CSng(8), "8", False, True, False}
      Yield New Object() {CSng(8), "9", False, False, True}
      Yield New Object() {CSng(8), True, True, False, False}
      Yield New Object() {CSng(8), False, True, False, False}
      Yield New Object() {CSng(8), Nothing, True, False, False}
      Yield New Object() {CSng(8), Single.NaN, False, False, False}

      ' float.
      Yield New Object() {Single.NaN, CSng(8), False, False, False}
      Yield New Object() {Single.NaN, Single.NaN, False, False, False}

      ' double.
      Yield New Object() {CDbl(8), CByte(7), True, False, False}
      Yield New Object() {CDbl(8), CByte(8), False, True, False}
      Yield New Object() {CDbl(8), CByte(9), False, False, True}
      Yield New Object() {CDbl(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CDbl(8), CSByte(7), True, False, False}
      Yield New Object() {CDbl(8), CSByte(8), False, True, False}
      Yield New Object() {CDbl(8), CSByte(9), False, False, True}
      Yield New Object() {CDbl(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CDbl(8), CUShort(7), True, False, False}
      Yield New Object() {CDbl(8), CUShort(8), False, True, False}
      Yield New Object() {CDbl(8), CUShort(9), False, False, True}
      Yield New Object() {CDbl(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CDbl(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CDbl(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CDbl(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CDbl(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CDbl(8), CUInt(7), True, False, False}
      Yield New Object() {CDbl(8), CUInt(8), False, True, False}
      Yield New Object() {CDbl(8), CUInt(9), False, False, True}
      Yield New Object() {CDbl(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CDbl(8), 7, True, False, False}
      Yield New Object() {CDbl(8), 8, False, True, False}
      Yield New Object() {CDbl(8), 9, False, False, True}
      Yield New Object() {CDbl(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CDbl(8), CULng(7), True, False, False}
      Yield New Object() {CDbl(8), CULng(8), False, True, False}
      Yield New Object() {CDbl(8), CULng(9), False, False, True}
      Yield New Object() {CDbl(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CDbl(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CDbl(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CDbl(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CDbl(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CDbl(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CDbl(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CDbl(8), CSng(7), True, False, False}
      Yield New Object() {CDbl(8), CSng(8), False, True, False}
      Yield New Object() {CDbl(8), CSng(9), False, False, True}
      Yield New Object() {CDbl(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CDbl(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CDbl(8), Single.NaN, False, False, False}
      Yield New Object() {CDbl(8), CDbl(7), True, False, False}
      Yield New Object() {CDbl(8), CDbl(8), False, True, False}
      Yield New Object() {CDbl(8), CDbl(9), False, False, True}
      Yield New Object() {CDbl(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CDbl(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CDbl(8), Double.NaN, False, False, False}
      Yield New Object() {CDbl(8), CDec(7), True, False, False}
      Yield New Object() {CDbl(8), CDec(8), False, True, False}
      Yield New Object() {CDbl(8), CDec(9), False, False, True}
      Yield New Object() {CDbl(8), "7", True, False, False}
      Yield New Object() {CDbl(8), "8", False, True, False}
      Yield New Object() {CDbl(8), "9", False, False, True}
      Yield New Object() {CDbl(8), True, True, False, False}
      Yield New Object() {CDbl(8), False, True, False, False}
      Yield New Object() {CDbl(8), Nothing, True, False, False}
      Yield New Object() {CDbl(8), Double.NaN, False, False, False}

      ' double.
      Yield New Object() {Double.NaN, CDbl(8), False, False, False}
      Yield New Object() {Double.NaN, Double.NaN, False, False, False}

      ' decimal.
      Yield New Object() {CDec(8), CByte(7), True, False, False}
      Yield New Object() {CDec(8), CByte(8), False, True, False}
      Yield New Object() {CDec(8), CByte(9), False, False, True}
      Yield New Object() {CDec(8), CType(7, ByteEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, ByteEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, ByteEnum), False, False, True}
      Yield New Object() {CDec(8), CSByte(7), True, False, False}
      Yield New Object() {CDec(8), CSByte(8), False, True, False}
      Yield New Object() {CDec(8), CSByte(9), False, False, True}
      Yield New Object() {CDec(8), CType(7, SByteEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, SByteEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, SByteEnum), False, False, True}
      Yield New Object() {CDec(8), CUShort(7), True, False, False}
      Yield New Object() {CDec(8), CUShort(8), False, True, False}
      Yield New Object() {CDec(8), CUShort(9), False, False, True}
      Yield New Object() {CDec(8), CType(7, UShortEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, UShortEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, UShortEnum), False, False, True}
      Yield New Object() {CDec(8), CShort(Fix(7)), True, False, False}
      Yield New Object() {CDec(8), CShort(Fix(8)), False, True, False}
      Yield New Object() {CDec(8), CShort(Fix(9)), False, False, True}
      Yield New Object() {CDec(8), CType(7, ShortEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, ShortEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, ShortEnum), False, False, True}
      Yield New Object() {CDec(8), CUInt(7), True, False, False}
      Yield New Object() {CDec(8), CUInt(8), False, True, False}
      Yield New Object() {CDec(8), CUInt(9), False, False, True}
      Yield New Object() {CDec(8), CType(7, UIntEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, UIntEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, UIntEnum), False, False, True}
      Yield New Object() {CDec(8), 7, True, False, False}
      Yield New Object() {CDec(8), 8, False, True, False}
      Yield New Object() {CDec(8), 9, False, False, True}
      Yield New Object() {CDec(8), CType(7, IntEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, IntEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, IntEnum), False, False, True}
      Yield New Object() {CDec(8), CULng(7), True, False, False}
      Yield New Object() {CDec(8), CULng(8), False, True, False}
      Yield New Object() {CDec(8), CULng(9), False, False, True}
      Yield New Object() {CDec(8), CType(7, ULongEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, ULongEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, ULongEnum), False, False, True}
      Yield New Object() {CDec(8), CLng(Fix(7)), True, False, False}
      Yield New Object() {CDec(8), CLng(Fix(8)), False, True, False}
      Yield New Object() {CDec(8), CLng(Fix(9)), False, False, True}
      Yield New Object() {CDec(8), CType(7, LongEnum), True, False, False}
      Yield New Object() {CDec(8), CType(8, LongEnum), False, True, False}
      Yield New Object() {CDec(8), CType(9, LongEnum), False, False, True}
      Yield New Object() {CDec(8), CSng(7), True, False, False}
      Yield New Object() {CDec(8), CSng(8), False, True, False}
      Yield New Object() {CDec(8), CSng(9), False, False, True}
      Yield New Object() {CDec(8), Single.PositiveInfinity, False, False, True}
      Yield New Object() {CDec(8), Single.NegativeInfinity, True, False, False}
      Yield New Object() {CDec(8), Single.NaN, False, False, False}
      Yield New Object() {CDec(8), CDbl(7), True, False, False}
      Yield New Object() {CDec(8), CDbl(8), False, True, False}
      Yield New Object() {CDec(8), CDbl(9), False, False, True}
      Yield New Object() {CDec(8), Double.PositiveInfinity, False, False, True}
      Yield New Object() {CDec(8), Double.NegativeInfinity, True, False, False}
      Yield New Object() {CDec(8), Double.NaN, False, False, False}
      Yield New Object() {CDec(8), CDec(7), True, False, False}
      Yield New Object() {CDec(8), CDec(8), False, True, False}
      Yield New Object() {CDec(8), CDec(9), False, False, True}
      Yield New Object() {CDec(8), "7", True, False, False}
      Yield New Object() {CDec(8), "8", False, True, False}
      Yield New Object() {CDec(8), "9", False, False, True}
      Yield New Object() {CDec(8), True, True, False, False}
      Yield New Object() {CDec(8), False, True, False, False}
      Yield New Object() {CDec(8), Nothing, True, False, False}

      ' string.
      Yield New Object() {"8", CByte(7), True, False, False}
      Yield New Object() {"8", CByte(8), False, True, False}
      Yield New Object() {"8", CByte(9), False, False, True}
      Yield New Object() {"8", CType(7, ByteEnum), True, False, False}
      Yield New Object() {"8", CType(8, ByteEnum), False, True, False}
      Yield New Object() {"8", CType(9, ByteEnum), False, False, True}
      Yield New Object() {"8", CSByte(7), True, False, False}
      Yield New Object() {"8", CSByte(8), False, True, False}
      Yield New Object() {"8", CSByte(9), False, False, True}
      Yield New Object() {"8", CType(7, SByteEnum), True, False, False}
      Yield New Object() {"8", CType(8, SByteEnum), False, True, False}
      Yield New Object() {"8", CType(9, SByteEnum), False, False, True}
      Yield New Object() {"8", CUShort(7), True, False, False}
      Yield New Object() {"8", CUShort(8), False, True, False}
      Yield New Object() {"8", CUShort(9), False, False, True}
      Yield New Object() {"8", CType(7, UShortEnum), True, False, False}
      Yield New Object() {"8", CType(8, UShortEnum), False, True, False}
      Yield New Object() {"8", CType(9, UShortEnum), False, False, True}
      Yield New Object() {"8", CShort(Fix(7)), True, False, False}
      Yield New Object() {"8", CShort(Fix(8)), False, True, False}
      Yield New Object() {"8", CShort(Fix(9)), False, False, True}
      Yield New Object() {"8", CType(7, ShortEnum), True, False, False}
      Yield New Object() {"8", CType(8, ShortEnum), False, True, False}
      Yield New Object() {"8", CType(9, ShortEnum), False, False, True}
      Yield New Object() {"8", CUInt(7), True, False, False}
      Yield New Object() {"8", CUInt(8), False, True, False}
      Yield New Object() {"8", CUInt(9), False, False, True}
      Yield New Object() {"8", CType(7, UIntEnum), True, False, False}
      Yield New Object() {"8", CType(8, UIntEnum), False, True, False}
      Yield New Object() {"8", CType(9, UIntEnum), False, False, True}
      Yield New Object() {"8", 7, True, False, False}
      Yield New Object() {"8", 8, False, True, False}
      Yield New Object() {"8", 9, False, False, True}
      Yield New Object() {"8", CType(7, IntEnum), True, False, False}
      Yield New Object() {"8", CType(8, IntEnum), False, True, False}
      Yield New Object() {"8", CType(9, IntEnum), False, False, True}
      Yield New Object() {"8", CULng(7), True, False, False}
      Yield New Object() {"8", CULng(8), False, True, False}
      Yield New Object() {"8", CULng(9), False, False, True}
      Yield New Object() {"8", CType(7, ULongEnum), True, False, False}
      Yield New Object() {"8", CType(8, ULongEnum), False, True, False}
      Yield New Object() {"8", CType(9, ULongEnum), False, False, True}
      Yield New Object() {"8", CLng(Fix(7)), True, False, False}
      Yield New Object() {"8", CLng(Fix(8)), False, True, False}
      Yield New Object() {"8", CLng(Fix(9)), False, False, True}
      Yield New Object() {"8", CType(7, LongEnum), True, False, False}
      Yield New Object() {"8", CType(8, LongEnum), False, True, False}
      Yield New Object() {"8", CType(9, LongEnum), False, False, True}
      Yield New Object() {"8", CSng(7), True, False, False}
      Yield New Object() {"8", CSng(8), False, True, False}
      Yield New Object() {"8", CSng(9), False, False, True}
      Yield New Object() {"8", Single.PositiveInfinity, False, False, True}
      Yield New Object() {"8", Single.NegativeInfinity, True, False, False}
      Yield New Object() {"8", Single.NaN, False, False, False}
      Yield New Object() {"8", CDbl(7), True, False, False}
      Yield New Object() {"8", CDbl(8), False, True, False}
      Yield New Object() {"8", CDbl(9), False, False, True}
      Yield New Object() {"8", Double.PositiveInfinity, False, False, True}
      Yield New Object() {"8", Double.NegativeInfinity, True, False, False}
      Yield New Object() {"8", Double.NaN, False, False, False}
      Yield New Object() {"8", CDec(7), True, False, False}
      Yield New Object() {"8", CDec(8), False, True, False}
      Yield New Object() {"8", CDec(9), False, False, True}
      Yield New Object() {"8", "7", True, False, False}
      Yield New Object() {"8", "8", False, True, False}
      Yield New Object() {"8", "9", False, False, True}
      Yield New Object() {"8", "", True, False, False}
      Yield New Object() {"8", New Char() {"7"c}, True, False, False}
      Yield New Object() {"8", New Char() {"8"c}, False, True, False}
      Yield New Object() {"8", New Char() {"9"c}, False, False, True}
      Yield New Object() {"8", True, False, True, False}
      Yield New Object() {"8", False, False, False, True}
      Yield New Object() {"8", "7"c, True, False, False}
      Yield New Object() {"8", "8"c, False, True, False}
      Yield New Object() {"8", "9"c, False, False, True}
      Yield New Object() {"8", Nothing, True, False, False}

      ' string.
      Yield New Object() {"", "7", False, False, True}
      Yield New Object() {"", "8", False, False, True}
      Yield New Object() {"", "9", False, False, True}
      Yield New Object() {"", "", False, True, False}
      Yield New Object() {"", New Char() {"7"c}, False, False, True}
      Yield New Object() {"", New Char() {"8"c}, False, False, True}
      Yield New Object() {"", New Char() {"9"c}, False, False, True}
      Yield New Object() {"", "7"c, False, False, True}
      Yield New Object() {"", "8"c, False, False, True}
      Yield New Object() {"", "9"c, False, False, True}
      Yield New Object() {"", Nothing, False, True, False}

      ' chars.
      Yield New Object() {New Char() {"8"c}, "7", True, False, False}
      Yield New Object() {New Char() {"8"c}, "8", False, True, False}
      Yield New Object() {New Char() {"8"c}, "9", False, False, True}
      Yield New Object() {New Char() {"8"c}, "", True, False, False}
      Yield New Object() {New Char() {"8"c}, New Char() {"7"c}, True, False, False}
      Yield New Object() {New Char() {"8"c}, New Char() {"8"c}, False, True, False}
      Yield New Object() {New Char() {"8"c}, New Char() {"9"c}, False, False, True}
      Yield New Object() {New Char() {"8"c}, Nothing, True, False, False}

      ' chars.
      Yield New Object() {New Char() {}, "7", False, False, True}
      Yield New Object() {New Char() {}, "8", False, False, True}
      Yield New Object() {New Char() {}, "9", False, False, True}
      Yield New Object() {New Char() {}, "", False, True, False}
      Yield New Object() {New Char() {}, New Char() {"7"c}, False, False, True}
      Yield New Object() {New Char() {}, New Char() {"8"c}, False, False, True}
      Yield New Object() {New Char() {}, New Char() {"9"c}, False, False, True}
      Yield New Object() {New Char() {}, Nothing, False, True, False}

      ' bool.
      Yield New Object() {True, CByte(7), False, False, True}
      Yield New Object() {True, CByte(8), False, False, True}
      Yield New Object() {True, CByte(9), False, False, True}
      Yield New Object() {True, CType(7, ByteEnum), False, False, True}
      Yield New Object() {True, CType(8, ByteEnum), False, False, True}
      Yield New Object() {True, CType(9, ByteEnum), False, False, True}
      Yield New Object() {True, CSByte(7), False, False, True}
      Yield New Object() {True, CSByte(8), False, False, True}
      Yield New Object() {True, CSByte(9), False, False, True}
      Yield New Object() {True, CType(7, SByteEnum), False, False, True}
      Yield New Object() {True, CType(8, SByteEnum), False, False, True}
      Yield New Object() {True, CType(9, SByteEnum), False, False, True}
      Yield New Object() {True, CUShort(7), False, False, True}
      Yield New Object() {True, CUShort(8), False, False, True}
      Yield New Object() {True, CUShort(9), False, False, True}
      Yield New Object() {True, CType(7, UShortEnum), False, False, True}
      Yield New Object() {True, CType(8, UShortEnum), False, False, True}
      Yield New Object() {True, CType(9, UShortEnum), False, False, True}
      Yield New Object() {True, CShort(Fix(7)), False, False, True}
      Yield New Object() {True, CShort(Fix(8)), False, False, True}
      Yield New Object() {True, CShort(Fix(9)), False, False, True}
      Yield New Object() {True, CType(7, ShortEnum), False, False, True}
      Yield New Object() {True, CType(8, ShortEnum), False, False, True}
      Yield New Object() {True, CType(9, ShortEnum), False, False, True}
      Yield New Object() {True, CUInt(7), False, False, True}
      Yield New Object() {True, CUInt(8), False, False, True}
      Yield New Object() {True, CUInt(9), False, False, True}
      Yield New Object() {True, CType(7, UIntEnum), False, False, True}
      Yield New Object() {True, CType(8, UIntEnum), False, False, True}
      Yield New Object() {True, CType(9, UIntEnum), False, False, True}
      Yield New Object() {True, 7, False, False, True}
      Yield New Object() {True, 8, False, False, True}
      Yield New Object() {True, 9, False, False, True}
      Yield New Object() {True, CType(7, IntEnum), False, False, True}
      Yield New Object() {True, CType(8, IntEnum), False, False, True}
      Yield New Object() {True, CType(9, IntEnum), False, False, True}
      Yield New Object() {True, CULng(7), False, False, True}
      Yield New Object() {True, CULng(8), False, False, True}
      Yield New Object() {True, CULng(9), False, False, True}
      Yield New Object() {True, CType(7, ULongEnum), False, False, True}
      Yield New Object() {True, CType(8, ULongEnum), False, False, True}
      Yield New Object() {True, CType(9, ULongEnum), False, False, True}
      Yield New Object() {True, CLng(Fix(7)), False, False, True}
      Yield New Object() {True, CLng(Fix(8)), False, False, True}
      Yield New Object() {True, CLng(Fix(9)), False, False, True}
      Yield New Object() {True, CType(7, LongEnum), False, False, True}
      Yield New Object() {True, CType(8, LongEnum), False, False, True}
      Yield New Object() {True, CType(9, LongEnum), False, False, True}
      Yield New Object() {True, CSng(7), False, False, True}
      Yield New Object() {True, CSng(8), False, False, True}
      Yield New Object() {True, CSng(9), False, False, True}
      Yield New Object() {True, Single.PositiveInfinity, False, False, True}
      Yield New Object() {True, Single.NegativeInfinity, True, False, False}
      Yield New Object() {True, Single.NaN, False, False, False}
      Yield New Object() {True, CDbl(7), False, False, True}
      Yield New Object() {True, CDbl(8), False, False, True}
      Yield New Object() {True, CDbl(9), False, False, True}
      Yield New Object() {True, Double.PositiveInfinity, False, False, True}
      Yield New Object() {True, Double.NegativeInfinity, True, False, False}
      Yield New Object() {True, Double.NaN, False, False, False}
      Yield New Object() {True, CDec(7), False, False, True}
      Yield New Object() {True, CDec(8), False, False, True}
      Yield New Object() {True, CDec(9), False, False, True}
      Yield New Object() {True, "7", False, True, False}
      Yield New Object() {True, "8", False, True, False}
      Yield New Object() {True, "9", False, True, False}
      Yield New Object() {True, True, False, True, False}
      Yield New Object() {True, False, False, False, True}
      Yield New Object() {True, Nothing, False, False, True}

      ' char.
      Yield New Object() {"8"c, "7", True, False, False}
      Yield New Object() {"8"c, "8", False, True, False}
      Yield New Object() {"8"c, "9", False, False, True}
      Yield New Object() {"8"c, "", True, False, False}
      Yield New Object() {"8"c, "7"c, True, False, False}
      Yield New Object() {"8"c, "8"c, False, True, False}
      Yield New Object() {"8"c, "9"c, False, False, True}
      Yield New Object() {"8"c, Nothing, True, False, False}

      ' DateTime.
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 19), True, False, False}
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 20), False, True, False}
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 11), True, False, False}
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 19).ToString(), True, False, False}
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 20).ToString(), False, True, False}
      Yield New Object() {New DateTime(2018, 7, 20), New DateTime(2018, 7, 11).ToString(), True, False, False}
      Yield New Object() {New DateTime(2018, 7, 20), Nothing, True, False, False}

      ' string.
      Yield New Object() {New DateTime(2018, 7, 20).ToString(), New DateTime(2018, 7, 19), True, False, False}
      Yield New Object() {New DateTime(2018, 7, 20).ToString(), New DateTime(2018, 7, 20), False, True, False}
      Yield New Object() {New DateTime(2018, 7, 20).ToString(), New DateTime(2018, 7, 21), False, False, True}

      ' null.
      Yield New Object() {Nothing, CByte(7), False, False, True}
      Yield New Object() {Nothing, CByte(8), False, False, True}
      Yield New Object() {Nothing, CByte(9), False, False, True}
      Yield New Object() {Nothing, CType(7, ByteEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, ByteEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, ByteEnum), False, False, True}
      Yield New Object() {Nothing, CSByte(7), False, False, True}
      Yield New Object() {Nothing, CSByte(8), False, False, True}
      Yield New Object() {Nothing, CSByte(9), False, False, True}
      Yield New Object() {Nothing, CType(7, SByteEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, SByteEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, SByteEnum), False, False, True}
      Yield New Object() {Nothing, CUShort(7), False, False, True}
      Yield New Object() {Nothing, CUShort(8), False, False, True}
      Yield New Object() {Nothing, CUShort(9), False, False, True}
      Yield New Object() {Nothing, CType(7, UShortEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, UShortEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, UShortEnum), False, False, True}
      Yield New Object() {Nothing, CShort(Fix(7)), False, False, True}
      Yield New Object() {Nothing, CShort(Fix(8)), False, False, True}
      Yield New Object() {Nothing, CShort(Fix(9)), False, False, True}
      Yield New Object() {Nothing, CType(7, ShortEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, ShortEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, ShortEnum), False, False, True}
      Yield New Object() {Nothing, CUInt(7), False, False, True}
      Yield New Object() {Nothing, CUInt(8), False, False, True}
      Yield New Object() {Nothing, CUInt(9), False, False, True}
      Yield New Object() {Nothing, CType(7, UIntEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, UIntEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, UIntEnum), False, False, True}
      Yield New Object() {Nothing, 7, False, False, True}
      Yield New Object() {Nothing, 8, False, False, True}
      Yield New Object() {Nothing, 9, False, False, True}
      Yield New Object() {Nothing, CType(7, IntEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, IntEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, IntEnum), False, False, True}
      Yield New Object() {Nothing, CULng(7), False, False, True}
      Yield New Object() {Nothing, CULng(8), False, False, True}
      Yield New Object() {Nothing, CULng(9), False, False, True}
      Yield New Object() {Nothing, CType(7, ULongEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, ULongEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, ULongEnum), False, False, True}
      Yield New Object() {Nothing, CLng(Fix(7)), False, False, True}
      Yield New Object() {Nothing, CLng(Fix(8)), False, False, True}
      Yield New Object() {Nothing, CLng(Fix(9)), False, False, True}
      Yield New Object() {Nothing, CType(7, LongEnum), False, False, True}
      Yield New Object() {Nothing, CType(8, LongEnum), False, False, True}
      Yield New Object() {Nothing, CType(9, LongEnum), False, False, True}
      Yield New Object() {Nothing, CSng(7), False, False, True}
      Yield New Object() {Nothing, CSng(8), False, False, True}
      Yield New Object() {Nothing, CSng(9), False, False, True}
      Yield New Object() {Nothing, Single.PositiveInfinity, False, False, True}
      Yield New Object() {Nothing, Single.NegativeInfinity, True, False, False}
      Yield New Object() {Nothing, Single.NaN, False, False, False}
      Yield New Object() {Nothing, CDbl(7), False, False, True}
      Yield New Object() {Nothing, CDbl(8), False, False, True}
      Yield New Object() {Nothing, CDbl(9), False, False, True}
      Yield New Object() {Nothing, Double.PositiveInfinity, False, False, True}
      Yield New Object() {Nothing, Double.NegativeInfinity, True, False, False}
      Yield New Object() {Nothing, Double.NaN, False, False, False}
      Yield New Object() {Nothing, CDec(7), False, False, True}
      Yield New Object() {Nothing, CDec(8), False, False, True}
      Yield New Object() {Nothing, CDec(9), False, False, True}
      Yield New Object() {Nothing, "7", False, False, True}
      Yield New Object() {Nothing, "8", False, False, True}
      Yield New Object() {Nothing, "9", False, False, True}
      Yield New Object() {Nothing, "", False, True, False}
      Yield New Object() {Nothing, New Char() {"7"c}, False, False, True}
      Yield New Object() {Nothing, New Char() {"8"c}, False, False, True}
      Yield New Object() {Nothing, New Char() {"9"c}, False, False, True}
      Yield New Object() {Nothing, True, True, False, False}
      Yield New Object() {Nothing, False, False, True, False}
      Yield New Object() {Nothing, "7"c, False, False, True}
      Yield New Object() {Nothing, "8"c, False, False, True}
      Yield New Object() {Nothing, "9"c, False, False, True}
      Yield New Object() {Nothing, New DateTime(7), False, False, True}
      Yield New Object() {Nothing, New DateTime(8), False, False, True}
      Yield New Object() {Nothing, New DateTime(9), False, False, True}
      Yield New Object() {Nothing, Nothing, False, True, False}
    End Function

    Public Shared Iterator Function Compare_InvalidObjects_TestData() As IEnumerable(Of Object())
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
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = less
      Assert.Equal(equal, Operators.CompareObjectEqual(left, right, True))
      Assert.Equal(equal, Operators.CompareObjectEqual(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectEqual, 2, "custom"}
      Yield New Object() {2, New CompareObjectEqual, "motsuc"}
      Yield New Object() {New CompareObjectEqual, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectEqual, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectEqual_OverloadOperator_TestData))>
    Public Sub CompareObjectEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectEqual(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectEqual(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectEqual, New Object}
      Yield New Object() {New Object, New CompareObjectEqual}

      Yield New Object() {New CompareObjectEqual, New CompareObjectEqual}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectEqual_MismatchingObjects_TestData))>
    Public Sub CompareObjectEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectEqual(left, right, True))
    End Sub

    Public Class CompareObjectEqual
      <SpecialName>
      Public Shared Function op_Equality(left As CompareObjectEqual, right As Integer) As String
        Return "custom"
      End Function
      <SpecialName>
      Public Shared Function op_Equality(left As Integer, right As CompareObjectEqual) As String
        Return "motsuc"
      End Function
      <SpecialName>
      Public Shared Function op_Equality(left As CompareObjectEqual, right As OperatorsTests) As String
        Return "customobject"
      End Function
      <SpecialName>
      Public Shared Function op_Equality(left As OperatorsTests, right As CompareObjectEqual) As String
        Return "tcejbomotsuc"
      End Function
    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectGreater_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = equal
      temp = less
      Assert.Equal(greater, Operators.CompareObjectGreater(left, right, True))
      Assert.Equal(greater, Operators.CompareObjectGreater(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectGreater_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectGreater, 2, "custom"}
      Yield New Object() {2, New CompareObjectGreater, "motsuc"}
      Yield New Object() {New CompareObjectGreater, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectGreater, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectGreater_OverloadOperator_TestData))>
    Public Sub CompareObjectGreater_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectGreater(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectGreater(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectGreater_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectGreater(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectGreater(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectGreater_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectGreater, New Object}
      Yield New Object() {New Object, New CompareObjectGreater}
      Yield New Object() {New CompareObjectGreater, New CompareObjectGreater}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectGreater_MismatchingObjects_TestData))>
    Public Sub CompareObjectGreater_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectGreater(left, right, True))
    End Sub

    Public Class CompareObjectGreater

      <SpecialName>
      Public Shared Function op_GreaterThan(left As CompareObjectGreater, right As Integer) As String
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As Integer, right As CompareObjectGreater) As String
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As CompareObjectGreater, right As OperatorsTests) As String
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As OperatorsTests, right As CompareObjectGreater) As String
        Return "tcejbomotsuc"
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectGreaterEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = less
      Assert.Equal(greater OrElse equal, Operators.CompareObjectGreaterEqual(left, right, True))
      Assert.Equal(greater OrElse equal, Operators.CompareObjectGreaterEqual(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectGreaterEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectGreaterEqual, 2, "custom"}
      Yield New Object() {2, New CompareObjectGreaterEqual, "motsuc"}
      Yield New Object() {New CompareObjectGreaterEqual, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectGreaterEqual, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectGreaterEqual_OverloadOperator_TestData))>
    Public Sub CompareObjectGreaterEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectGreaterEqual(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectGreaterEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectGreaterEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectGreaterEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectGreaterEqual(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectGreaterEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectGreaterEqual, New Object}
      Yield New Object() {New Object, New CompareObjectGreaterEqual}
      Yield New Object() {New CompareObjectGreaterEqual, New CompareObjectGreaterEqual}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectGreaterEqual_MismatchingObjects_TestData))>
    Public Sub CompareObjectGreaterEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectGreaterEqual(left, right, True))
    End Sub

    Public Class CompareObjectGreaterEqual

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As CompareObjectGreaterEqual, right As Integer) As String
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As Integer, right As CompareObjectGreaterEqual) As String
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As CompareObjectGreaterEqual, right As OperatorsTests) As String
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As OperatorsTests, right As CompareObjectGreaterEqual) As String
        Return "tcejbomotsuc"
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectLess_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = equal
      Assert.Equal(less, Operators.CompareObjectLess(left, right, True))
      Assert.Equal(less, Operators.CompareObjectLess(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectLess_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectLess, 2, "custom"}
      Yield New Object() {2, New CompareObjectLess, "motsuc"}
      Yield New Object() {New CompareObjectLess, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectLess, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectLess_OverloadOperator_TestData))>
    Public Sub CompareObjectLess_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectLess(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectLess(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectLess_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectLess(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectLess(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectLess_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectLess, New Object}
      Yield New Object() {New Object, New CompareObjectLess}
      Yield New Object() {New CompareObjectLess, New CompareObjectLess}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectLess_MismatchingObjects_TestData))>
    Public Sub CompareObjectLess_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectLess(left, right, True))
    End Sub

    Public Class CompareObjectLess

      <SpecialName>
      Public Shared Function op_LessThan(left As CompareObjectLess, right As Integer) As String
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As Integer, right As CompareObjectLess) As String
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As CompareObjectLess, right As OperatorsTests) As String
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As OperatorsTests, right As CompareObjectLess) As String
        Return "tcejbomotsuc"
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectLessEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      Assert.Equal(less OrElse equal, Operators.CompareObjectLessEqual(left, right, True))
      Assert.Equal(less OrElse equal, Operators.CompareObjectLessEqual(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectLessEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectLessEqual, 2, "custom"}
      Yield New Object() {2, New CompareObjectLessEqual, "motsuc"}
      Yield New Object() {New CompareObjectLessEqual, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectLessEqual, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectLessEqual_OverloadOperator_TestData))>
    Public Sub CompareObjectLessEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectLessEqual(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectLessEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectLessEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectLessEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectLessEqual(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectLessEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectLessEqual, New Object}
      Yield New Object() {New Object, New CompareObjectLessEqual}

      Yield New Object() {New CompareObjectLessEqual, New CompareObjectLessEqual}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectLessEqual_MismatchingObjects_TestData))>
    Public Sub CompareObjectLessEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectLessEqual(left, right, True))
    End Sub

    Public Class CompareObjectLessEqual

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As CompareObjectLessEqual, right As Integer) As String
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As Integer, right As CompareObjectLessEqual) As String
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As CompareObjectLessEqual, right As OperatorsTests) As String
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As OperatorsTests, right As CompareObjectLessEqual) As String
        Return "tcejbomotsuc"
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub CompareObjectNotEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = less
      Assert.Equal(Not equal, Operators.CompareObjectNotEqual(left, right, True))
      Assert.Equal(Not equal, Operators.CompareObjectNotEqual(left, right, False))
    End Sub

    Public Shared Iterator Function CompareObjectNotEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectNotEqual, 2, "custom"}
      Yield New Object() {2, New CompareObjectNotEqual, "motsuc"}
      Yield New Object() {New CompareObjectNotEqual, New OperatorsTests, "customobject"}
      Yield New Object() {New OperatorsTests, New CompareObjectNotEqual, "tcejbomotsuc"}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectNotEqual_OverloadOperator_TestData))>
    Public Sub CompareObjectNotEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.CompareObjectNotEqual(left, right, True))
      Assert.Equal(expected, Operators.CompareObjectNotEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub CompareObjectNotEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectNotEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.CompareObjectNotEqual(right, left, True))
    End Sub

    Public Shared Iterator Function CompareObjectNotEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New CompareObjectNotEqual, New Object}
      Yield New Object() {New Object, New CompareObjectNotEqual}
      Yield New Object() {New CompareObjectNotEqual, New CompareObjectNotEqual}
    End Function

    <Theory>
    <MemberData(NameOf(CompareObjectNotEqual_MismatchingObjects_TestData))>
    Public Sub CompareObjectNotEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.CompareObjectNotEqual(left, right, True))
    End Sub

    Public Class CompareObjectNotEqual

      <SpecialName>
      Public Shared Function op_Inequality(left As CompareObjectNotEqual, right As Integer) As String
        Return "custom"
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As Integer, right As CompareObjectNotEqual) As String
        Return "motsuc"
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As CompareObjectNotEqual, right As OperatorsTests) As String
        Return "customobject"
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As OperatorsTests, right As CompareObjectNotEqual) As String
        Return "tcejbomotsuc"
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = less
      Assert.Equal(equal, Operators.ConditionalCompareObjectEqual(left, right, True))
      Assert.Equal(equal, Operators.ConditionalCompareObjectEqual(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectEqual, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectEqual, False}
      Yield New Object() {New ConditionalCompareObjectEqual, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectEqual, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectEqual_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectEqual(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectEqual(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectEqual, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectEqual}
      Yield New Object() {New ConditionalCompareObjectEqual, New ConditionalCompareObjectEqual}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectEqual_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectEqual(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectEqual

      <SpecialName>
      Public Shared Function op_Equality(left As ConditionalCompareObjectEqual, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_Equality(left As Integer, right As ConditionalCompareObjectEqual) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_Equality(left As ConditionalCompareObjectEqual, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_Equality(left As OperatorsTests, right As ConditionalCompareObjectEqual) As Boolean
        Return True
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectGreater_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = equal
      temp = less
      Assert.Equal(greater, Operators.ConditionalCompareObjectGreater(left, right, True))
      Assert.Equal(greater, Operators.ConditionalCompareObjectGreater(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectGreater_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectGreater, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectGreater, False}
      Yield New Object() {New ConditionalCompareObjectGreater, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectGreater, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectGreater_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectGreater_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectGreater(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectGreater(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectGreater_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectGreater(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectGreater(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectGreater_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectGreater, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectGreater}

      Yield New Object() {New ConditionalCompareObjectGreater, New ConditionalCompareObjectGreater}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectGreater_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectGreater_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectGreater(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectGreater

      <SpecialName>
      Public Shared Function op_GreaterThan(left As ConditionalCompareObjectGreater, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As Integer, right As ConditionalCompareObjectGreater) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As ConditionalCompareObjectGreater, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThan(left As OperatorsTests, right As ConditionalCompareObjectGreater) As Boolean
        Return True
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectGreaterEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = less
      Assert.Equal(greater OrElse equal, Operators.ConditionalCompareObjectGreaterEqual(left, right, True))
      Assert.Equal(greater OrElse equal, Operators.ConditionalCompareObjectGreaterEqual(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectGreaterEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectGreaterEqual, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectGreaterEqual, False}
      Yield New Object() {New ConditionalCompareObjectGreaterEqual, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectGreaterEqual, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectGreaterEqual_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectGreaterEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectGreaterEqual(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectGreaterEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectGreaterEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectGreaterEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectGreaterEqual(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectGreaterEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectGreaterEqual, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectGreaterEqual}
      Yield New Object() {New ConditionalCompareObjectGreaterEqual, New ConditionalCompareObjectGreaterEqual}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectGreaterEqual_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectGreaterEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectGreaterEqual(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectGreaterEqual

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As ConditionalCompareObjectGreaterEqual, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As Integer, right As ConditionalCompareObjectGreaterEqual) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As ConditionalCompareObjectGreaterEqual, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_GreaterThanOrEqual(left As OperatorsTests, right As ConditionalCompareObjectGreaterEqual) As Boolean
        Return True
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectLess_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = equal
      Assert.Equal(less, Operators.ConditionalCompareObjectLess(left, right, True))
      Assert.Equal(less, Operators.ConditionalCompareObjectLess(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectLess_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectLess, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectLess, False}
      Yield New Object() {New ConditionalCompareObjectLess, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectLess, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectLess_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectLess_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectLess(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectLess(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectLess_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectLess(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectLess(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectLess_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectLess, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectLess}
      Yield New Object() {New ConditionalCompareObjectLess, New ConditionalCompareObjectLess}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectLess_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectLess_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectLess(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectLess

      <SpecialName>
      Public Shared Function op_LessThan(left As ConditionalCompareObjectLess, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As Integer, right As ConditionalCompareObjectLess) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As ConditionalCompareObjectLess, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_LessThan(left As OperatorsTests, right As ConditionalCompareObjectLess) As Boolean
        Return True
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectLessEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      Assert.Equal(less OrElse equal, Operators.ConditionalCompareObjectLessEqual(left, right, True))
      Assert.Equal(less OrElse equal, Operators.ConditionalCompareObjectLessEqual(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectLessEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectLessEqual, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectLessEqual, False}
      Yield New Object() {New ConditionalCompareObjectLessEqual, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectLessEqual, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectLessEqual_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectLessEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectLessEqual(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectLessEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectLessEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectLessEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectLessEqual(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectLessEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectLessEqual, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectLessEqual}

      Yield New Object() {New ConditionalCompareObjectLessEqual, New ConditionalCompareObjectLessEqual}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectLessEqual_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectLessEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectLessEqual(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectLessEqual

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As ConditionalCompareObjectLessEqual, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As Integer, right As ConditionalCompareObjectLessEqual) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As ConditionalCompareObjectLessEqual, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_LessThanOrEqual(left As OperatorsTests, right As ConditionalCompareObjectLessEqual) As Boolean
        Return True
      End Function

    End Class

    <Theory>
    <MemberData(NameOf(Compare_Primitives_TestData))>
    Public Sub ConditionalCompareObjectNotEqual_Invoke_ReturnsExpected(left As Object, right As Object, greater As Boolean, equal As Boolean, less As Boolean)
      Dim temp = greater
      temp = less
      Assert.Equal(Not equal, Operators.ConditionalCompareObjectNotEqual(left, right, True))
      Assert.Equal(Not equal, Operators.ConditionalCompareObjectNotEqual(left, right, False))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectNotEqual_OverloadOperator_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectNotEqual, 2, True}
      Yield New Object() {2, New ConditionalCompareObjectNotEqual, False}
      Yield New Object() {New ConditionalCompareObjectNotEqual, New OperatorsTests, False}
      Yield New Object() {New OperatorsTests, New ConditionalCompareObjectNotEqual, True}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectNotEqual_OverloadOperator_TestData))>
    Public Sub ConditionalCompareObjectNotEqual_InvokeOverloadedOperator_ReturnsExpected(left As Object, right As Object, expected As Object)
      Assert.Equal(expected, Operators.ConditionalCompareObjectNotEqual(left, right, True))
      Assert.Equal(expected, Operators.ConditionalCompareObjectNotEqual(left, right, False))
    End Sub

    <Theory>
    <MemberData(NameOf(Compare_InvalidObjects_TestData))>
    Public Sub ConditionalCompareObjectNotEqual_InvalidObjects_ThrowsInvalidCastException(left As Object, right As Object)
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectNotEqual(left, right, True))
      Assert.Throws(Of InvalidCastException)(Function() Operators.ConditionalCompareObjectNotEqual(right, left, True))
    End Sub

    Public Shared Iterator Function ConditionalCompareObjectNotEqual_MismatchingObjects_TestData() As IEnumerable(Of Object())
      Yield New Object() {New ConditionalCompareObjectNotEqual, New Object}
      Yield New Object() {New Object, New ConditionalCompareObjectNotEqual}
      Yield New Object() {New ConditionalCompareObjectNotEqual, New ConditionalCompareObjectNotEqual}
    End Function

    <Theory>
    <MemberData(NameOf(ConditionalCompareObjectNotEqual_MismatchingObjects_TestData))>
    Public Sub ConditionalCompareObjectNotEqual_MismatchingObjects_ThrowsAmibguousMatchException(left As Object, right As Object)
      Assert.Throws(Of AmbiguousMatchException)(Function() Operators.ConditionalCompareObjectNotEqual(left, right, True))
    End Sub

    Public Class ConditionalCompareObjectNotEqual

      <SpecialName>
      Public Shared Function op_Inequality(left As ConditionalCompareObjectNotEqual, right As Integer) As Boolean
        Return True
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As Integer, right As ConditionalCompareObjectNotEqual) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As ConditionalCompareObjectNotEqual, right As OperatorsTests) As Boolean
        Return False
      End Function

      <SpecialName>
      Public Shared Function op_Inequality(left As OperatorsTests, right As ConditionalCompareObjectNotEqual) As Boolean
        Return True
      End Function

    End Class

  End Class

End Namespace