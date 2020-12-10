' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Community.Tests
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text
Imports Community.VisualBasic.CompilerServices.Tests
'Imports Microsoft.DotNet.RemoteExecutor
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class StringsTests

    Shared Sub New()
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
    End Sub

    <Fact>
    Public Sub Asc_Char_ReturnsChar()
      Assert.Equal("3"c, ChrW(Community.VisualBasic.Strings.Asc("3"c)))
    End Sub

    <Theory>
    <InlineData("3", 51)>
    <InlineData("345", 51)>
    <InlineData("ABCD", 65)>
    Public Sub Asc_String_ReturnsExpected([String] As String, expected As Integer)
      Assert.Equal(expected, Strings.Asc([String]))
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    Public Sub Asc_NullOrEmpty_ThrowsArgumentException([String] As String)
      AssertExtensions.Throws(Of ArgumentException)("String", Nothing, Function() Strings.Asc([String]))
    End Sub

    <Fact>
    Public Sub AscW_Char_ReturnsChar()
      Assert.Equal("3"c, ChrW(AscW("3"c)))
    End Sub

    <Theory>
    <InlineData("3", 51)>
    <InlineData("345", 51)>
    <InlineData("ABCD", 65)>
    Public Sub AscW_String_ReturnsExpected([String] As String, expected As Integer)
      Assert.Equal(expected, AscW([String]))
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    Public Sub AscW_NullOrEmpty_ThrowsArgumentException([String] As String)
      AssertExtensions.Throws(Of ArgumentException)("String", Nothing, Function() AscW([String]))
    End Sub

    <Theory>
    <InlineData(97)>
    <InlineData(65)>
    <InlineData(0)>
    Public Sub Chr_CharCodeInRange_ReturnsExpected(charCode As Integer)
      Assert.Equal(Convert.ToChar(charCode And &HFFFF), Strings.Chr(charCode))
    End Sub

    <Theory>
    <InlineData(-1)>
    <InlineData(256)>
    Public Sub Chr_CharCodeOutOfRange_ThrowsNotSupportedException(charCode As Integer)
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
      Using New ThreadCultureChange("en-US")
        AssertExtensions.Throws(Of ArgumentException)(Nothing, Function() Strings.Chr(charCode))
      End Using
    End Sub

    <Theory>
    <InlineData(-32769)>
    <InlineData(65536)>
    Public Sub Chr_CharCodeOutOfRange_ThrowsArgumentException(charCode As Integer)
      AssertExtensions.Throws(Of ArgumentException)("CharCode", Nothing, Function() Strings.Chr(charCode))
    End Sub

    <Theory>
    <InlineData(97)>
    <InlineData(65)>
    <InlineData(65535)>
    <InlineData(-32768)>
    Public Sub ChrW_CharCodeInRange_ReturnsExpected(charCode As Integer)
      Assert.Equal(Convert.ToChar(charCode And &HFFFF), ChrW(charCode))
    End Sub

    <Theory>
    <InlineData(-32769)>
    <InlineData(65536)>
    Public Sub ChrW_CharCodeOutOfRange_ThrowsArgumentException(charCode As Integer)
      AssertExtensions.Throws(Of ArgumentException)("CharCode", Nothing, Function() ChrW(charCode))
    End Sub

    <Theory>
    <InlineData(0, 0)>
    <InlineData(33, 33)>
    <InlineData(172, 172)>
    <InlineData(255, 255)>
    Public Sub Asc_Chr_Invariant(charCode As Integer, expected As Integer)
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
      Using New ThreadCultureChange(CultureInfo.InvariantCulture)
        Assert.Equal(1252, CultureInfo.CurrentCulture.TextInfo.ANSICodePage)
        Assert.Equal(expected, Strings.Asc(Strings.Chr(charCode)))
      End Using
    End Sub

    '<ActiveIssue("https://github.com/dotnet/runtime/issues/30419", TargetFrameworkMonikers.NetFramework)>
    '<ConditionalTheory(GetType(PlatformDetection), NameOf(PlatformDetection.IsNotInvariantGlobalization))>
    <Theory>
    <InlineData(0, 0)>
    <InlineData(33, 33)>
    <InlineData(172, 0)>
    <InlineData(255, 255)>
    <InlineData(256, 1)>
    <InlineData(&H8141, &H8141)>
    <InlineData(&HC8FE, &HC8FE)>
    <InlineData(&HFFFF, &HFF)>
    Public Sub Asc_Chr_DoubleByte(charCode As Integer, expected As Integer)
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
      Using New ThreadCultureChange("ko-KR")
        Assert.Equal(949, CultureInfo.CurrentCulture.TextInfo.ANSICodePage)
        Assert.Equal(expected, CUShort(Strings.Asc(Strings.Chr(charCode))))
      End Using
    End Sub

    <Theory>
    <InlineData(New String() {}, Nothing, Nothing)>
    <InlineData(New String() {}, "", Nothing)>
    Public Sub Filter_WhenNoMatchArgument_ReturnsNull(source As String(), match As String, expected As String())
      Assert.Equal(expected, Strings.Filter(source, match))
    End Sub

    <Theory>
    <InlineData(New String() {}, "a", New String() {}, New String() {})>
    Public Sub Filter_NoElements(source As String(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False))
    End Sub

    <Theory>
    <InlineData(New String() {}, "a", New String() {}, New String() {})>
    <InlineData(New String() {"a"}, "a", New String() {"a"}, New String() {})>
    <InlineData(New String() {"ab"}, "a", New String() {"ab"}, New String() {})>
    <InlineData(New String() {"ba"}, "a", New String() {"ba"}, New String() {})>
    <InlineData(New String() {"bab"}, "a", New String() {"bab"}, New String() {})>
    <InlineData(New String() {"b"}, "a", New String() {}, New String() {"b"})>
    <InlineData(New String() {"a"}, "ab", New String() {}, New String() {"a"})>
    <InlineData(New String() {"ab"}, "ab", New String() {"ab"}, New String() {})>
    Public Sub Filter_SingleElement(source As String(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False))
    End Sub

    <Theory>
    <InlineData(New String() {"A"}, "a", New String() {}, New String() {"A"})>
    Public Sub Filter_SingleElement_BinaryCompare(source As String(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True, Compare:=CompareMethod.Binary))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False, Compare:=CompareMethod.Binary))
    End Sub

    <Theory>
    <InlineData(New String() {"A"}, "a", New String() {"A"}, New String() {})>
    Public Sub Filter_SingleElement_TextCompare(source As String(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True, Compare:=CompareMethod.Text))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False, Compare:=CompareMethod.Text))
    End Sub

    <Theory>
    <InlineData(New String() {"a", "a"}, "a", New String() {"a", "a"}, New String() {})>
    <InlineData(New String() {"a", "b"}, "a", New String() {"a"}, New String() {"b"})>
    <InlineData(New String() {"b", "a"}, "a", New String() {"a"}, New String() {"b"})>
    <InlineData(New String() {"b", "b"}, "a", New String() {}, New String() {"b", "b"})>
    Public Sub Filter_MultipleElements(source As String(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False))
    End Sub

    <Fact>
    Public Sub Filter_Objects_WhenObjectCannotBeConvertedToString_ThrowsArgumentOutOfRangeException()
      Dim source As Object() = New Object() {GetType(Object)}
      Dim match As String = "a"

      AssertExtensions.Throws(Of ArgumentException)("Source", Nothing, Function() Strings.Filter(source, match))
    End Sub

    <Theory>
    <InlineData(New Object() {42}, "42", New String() {"42"}, New String() {})>
    <InlineData(New Object() {True}, "True", New String() {"True"}, New String() {})>
    Public Sub Filter_Objects(source As Object(), match As String, includeExpected As String(), excludeExpected As String())
      Assert.Equal(includeExpected, Strings.Filter(source, match, Include:=True))
      Assert.Equal(excludeExpected, Strings.Filter(source, match, Include:=False))
    End Sub

    <Theory>
    <MemberData(NameOf(Format_TestData))>
    Public Sub Format(expression As Object, style As String, expected As String)
      Assert.Equal(expected, Strings.Format(expression, style))
    End Sub

    <Theory>
    <MemberData(NameOf(Format_InvalidCastException_TestData))>
    Public Sub Format_InvalidCastException(expression As Object, style As String)
      Assert.Throws(Of InvalidCastException)(Function() Strings.Format(expression, style))
    End Sub

    Public Shared Iterator Function Format_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, ""}
      Yield New Object() {Nothing, "", ""}
      Yield New Object() {"", Nothing, ""}
      Yield New Object() {"", "", ""}
      Yield New Object() {SByte.MinValue, "0", "-128"}
      Yield New Object() {SByte.MaxValue, "0", "127"}
      Yield New Object() {UShort.MinValue, "0", "0"}
      Yield New Object() {UShort.MaxValue, "0", "65535"}
      Yield New Object() {False, "", "False"}
      Yield New Object() {False, "0", "0"}
      If IsEnUS() Then
        Yield New Object() {1.234, "", "1.234"}
        Yield New Object() {1.234, "0", "1"}
        Yield New Object() {1.234, "0.0", "1.2"}
        Yield New Object() {1.234, "fixed", "1.23"}
        Yield New Object() {1.234, "percent", "123.40%"}
        Yield New Object() {1.234, "standard", "1.23"}
        Yield New Object() {1.234, "currency", "$1.23"}
        Yield New Object() {False, "yes/no", "No"}
        Yield New Object() {True, "yes/no", "Yes"}
        Yield New Object() {False, "on/off", "Off"}
        Yield New Object() {True, "on/off", "On"}
        Yield New Object() {False, "true/false", "False"}
        Yield New Object() {True, "true/false", "True"}
        Yield New Object() {0, "yes/no", "No"}
        Yield New Object() {"ABC", "yes/no", "ABC"}
        Yield New Object() {123.4, "scientific", "1.23E+02"}
      End If
      Dim d As DateTime = DateTime.Now
      Yield New Object() {d, "long time", d.ToString("T")}
      Yield New Object() {d, "medium time", d.ToString("T")}
      Yield New Object() {d, "short time", d.ToString("t")}
      Yield New Object() {d, "long date", d.ToString("D")}
      Yield New Object() {d, "medium date", d.ToString("D")}
      Yield New Object() {d, "short date", d.ToString("d")}
      Yield New Object() {d, "general date", d.ToString("G")}
      Yield New Object() {123.4, "general number", 123.4.ToString("G", Nothing)}
    End Function

    Public Shared Iterator Function Format_InvalidCastException_TestData() As IEnumerable(Of Object())
      Yield New Object() {New Object, Nothing}
      Yield New Object() {New Object, "0"}
    End Function

    <Theory>
    <MemberData(NameOf(FormatCurrency_TestData))>
    Public Sub FormatCurrency(expression As Object, numDigitsAfterDecimal As Integer, includeLeadingDigit As TriState, useParensForNegativeNumbers As TriState, groupDigits As TriState, expected As String)
      Assert.Equal(expected, Strings.FormatCurrency(expression, numDigitsAfterDecimal, includeLeadingDigit, useParensForNegativeNumbers, groupDigits))
    End Sub

    Public Shared Iterator Function FormatCurrency_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, ""}
      If IsEnUS() Then
        Yield New Object() {0.123, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "$0"}
        Yield New Object() {0.123, 1, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "$0.1"}
        Yield New Object() {0.123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "$0.12"}
        Yield New Object() {0.123, 4, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "$0.1230"}
        Yield New Object() {0.123, 2, TriState.[False], TriState.UseDefault, TriState.UseDefault, "$.12"}
        Yield New Object() {0.123, 2, TriState.[True], TriState.UseDefault, TriState.UseDefault, "$0.12"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[False], TriState.UseDefault, "-$0.12"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[True], TriState.UseDefault, "($0.12)"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "$1,234.50"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.[False], "$1234.50"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.[True], "$1,234.50"}
      End If
    End Function

    <Theory>
    <MemberData(NameOf(FormatDateTime_TestData))>
    Public Sub FormatDateTime(expression As DateTime, format1 As DateFormat, expected As String)
      Assert.Equal(expected, Strings.FormatDateTime(expression, format1))
    End Sub

    Public Shared Iterator Function FormatDateTime_TestData() As IEnumerable(Of Object())
      Dim d As DateTime = DateTime.Now
      Yield New Object() {d, DateFormat.LongTime, d.ToString("T")}
      Yield New Object() {d, DateFormat.ShortTime, d.ToString("HH:mm")}
      Yield New Object() {d, DateFormat.LongDate, d.ToString("D")}
      Yield New Object() {d, DateFormat.ShortDate, d.ToString("d")}
      Yield New Object() {d, DateFormat.GeneralDate, d.ToString("G")}
    End Function

    <Theory>
    <MemberData(NameOf(FormatNumber_TestData))>
    Public Sub FormatNumber(expression As Object, numDigitsAfterDecimal As Integer, includeLeadingDigit As TriState, useParensForNegativeNumbers As TriState, groupDigits As TriState, expected As String)
      Assert.Equal(expected, Strings.FormatNumber(expression, numDigitsAfterDecimal, includeLeadingDigit, useParensForNegativeNumbers, groupDigits))
    End Sub

    Public Shared Iterator Function FormatNumber_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, ""}
      If IsEnUS() Then
        Yield New Object() {0.123, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "0"}
        Yield New Object() {0.123, 1, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "0.1"}
        Yield New Object() {0.123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "0.12"}
        Yield New Object() {0.123, 4, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "0.1230"}
        Yield New Object() {0.123, 2, TriState.[False], TriState.UseDefault, TriState.UseDefault, ".12"}
        Yield New Object() {0.123, 2, TriState.[True], TriState.UseDefault, TriState.UseDefault, "0.12"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "-0.12"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[False], TriState.UseDefault, "-0.12"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[True], TriState.UseDefault, "(0.12)"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "1,234.50"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.[False], "1234.50"}
        Yield New Object() {1234.5, 2, TriState.UseDefault, TriState.UseDefault, TriState.[True], "1,234.50"}
      End If
    End Function

    <Theory>
    <MemberData(NameOf(FormatPercent_TestData))>
    Public Sub FormatPercent(expression As Object, numDigitsAfterDecimal As Integer, includeLeadingDigit As TriState, useParensForNegativeNumbers As TriState, groupDigits As TriState, expected As String)
      Assert.Equal(expected, Strings.FormatPercent(expression, numDigitsAfterDecimal, includeLeadingDigit, useParensForNegativeNumbers, groupDigits))
    End Sub

    Public Shared Iterator Function FormatPercent_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, ""}
      If IsEnUS() Then
        Yield New Object() {0.123, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "12%"}
        Yield New Object() {0.123, 1, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "12.3%"}
        Yield New Object() {0.123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "12.30%"}
        Yield New Object() {0.123, 4, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "12.3000%"}
        Yield New Object() {0.00123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "0.12%"}
        Yield New Object() {0.00123, 2, TriState.[False], TriState.UseDefault, TriState.UseDefault, ".12%"}
        Yield New Object() {0.00123, 2, TriState.[True], TriState.UseDefault, TriState.UseDefault, "0.12%"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "-12.30%"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[False], TriState.UseDefault, "-12.30%"}
        Yield New Object() {-0.123, 2, TriState.UseDefault, TriState.[True], TriState.UseDefault, "(12.30%)"}
        Yield New Object() {12.345, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault, "1,234.50%"}
        Yield New Object() {12.345, 2, TriState.UseDefault, TriState.UseDefault, TriState.[False], "1234.50%"}
        Yield New Object() {12.345, 2, TriState.UseDefault, TriState.UseDefault, TriState.[True], "1,234.50%"}
      End If
    End Function

    <Theory>
    <InlineData("ABC", 1, "A"c)>
    <InlineData("ABC", 2, "B"c)>
    <InlineData("ABC", 3, "C"c)>
    Public Sub GetChar(str As String, index As Integer, expected As Char)
      Assert.Equal(expected, Strings.GetChar(str, index))
    End Sub

    <Theory>
    <InlineData(Nothing, 0)>
    <InlineData(Nothing, 1)>
    <InlineData("", 0)>
    <InlineData("", 1)>
    <InlineData("ABC", 0)>
    <InlineData("ABC", 4)>
    Public Sub GetChar_ArgumentException(str As String, index As Integer)
      Assert.Throws(Of ArgumentException)(Function() Strings.GetChar(str, index))
    End Sub

    <Theory>
    <MemberData(NameOf(InStr_TestData_NullsAndEmpties))>
    <MemberData(NameOf(InStr_FromBegin_TestData))>
    Public Sub InStr_FromBegin(string1 As String, string2 As String, expected As Integer)
      Assert.Equal(expected, Strings.InStr(string1, string2))
      Assert.Equal(expected, Strings.InStr(1, string1, string2))
    End Sub

    <Theory>
    <MemberData(NameOf(InStr_TestData_NullsAndEmpties))>
    <MemberData(NameOf(InStr_FromWithin_TestData))>
    Public Sub InStr_FromWithin(string1 As String, string2 As String, expected As Integer)
      Assert.Equal(expected, Strings.InStr(2, string1, string2))
    End Sub

    <Theory>
    <InlineData("A", "a", 0)>
    <InlineData("Aa", "a", 2)>
    Public Sub InStr_BinaryCompare(string1 As String, string2 As String, expected As Integer)
      Assert.Equal(expected, Strings.InStr(string1, string2, CompareMethod.Binary))
      Assert.Equal(expected, Strings.InStr(1, string1, string2, CompareMethod.Binary))
    End Sub

    <Theory>
    <InlineData("A", "a", 1)>
    <InlineData("Aa", "a", 1)>
    Public Sub InStr_TextCompare(string1 As String, string2 As String, expected As Integer)
      Assert.Equal(expected, Strings.InStr(string1, string2, CompareMethod.Text))
      Assert.Equal(expected, Strings.InStr(1, string1, string2, CompareMethod.Text))
    End Sub

    <Theory>
    <InlineData(2)>
    <InlineData(3)>
    Public Sub InStr_WhenStartGreatherThanLength_ReturnsZero(start As Integer)
      Assert.Equal(0, Strings.InStr(start, "a", "a"))
    End Sub

    <Theory>
    <InlineData(0)>
    <InlineData(-1)>
    Public Sub InStr_WhenStartZeroOrLess_ThrowsArgumentException(start As Integer)
      AssertExtensions.Throws(Of ArgumentException)("Start", Nothing, Function() Strings.InStr(start, "a", "a"))
    End Sub

    <Theory>
    <MemberData(NameOf(InStr_TestData_NullsAndEmpties))>
    <MemberData(NameOf(InStrRev_FromEnd_TestData))>
    Public Sub InStrRev_FromEnd(stringCheck As String, stringMatch As String, expected As Integer)
      Assert.Equal(expected, Strings.InStrRev(stringCheck, stringMatch))
    End Sub

    <Theory>
    <MemberData(NameOf(InStrRev_FromWithin_TestData))>
    Public Sub InStrRev_FromWithin(stringCheck As String, stringMatch As String, start As Integer, expected As Integer)
      Assert.Equal(expected, Strings.InStrRev(stringCheck, stringMatch, start))
    End Sub

    <Theory>
    <InlineData("A", "a", 1, 0)>
    <InlineData("aA", "a", 2, 1)>
    Public Sub InStrRev_BinaryCompare(stringCheck As String, stringMatch As String, start As Integer, expected As Integer)
      Assert.Equal(expected, Strings.InStrRev(stringCheck, stringMatch, start, CompareMethod.Binary))
    End Sub

    <Theory>
    <InlineData("A", "a", 1, 1)>
    <InlineData("aA", "a", 2, 2)>
    Public Sub InStrRev_TextCompare(stringCheck As String, stringMatch As String, start As Integer, expected As Integer)
      Assert.Equal(expected, Strings.InStrRev(stringCheck, stringMatch, start, CompareMethod.Text))
    End Sub

    <Fact>
    Public Sub InStrRev_WhenStartMinusOne_SearchesFromEnd()
      Assert.Equal(2, Strings.InStrRev("aa", "a", -1))
    End Sub

    <Theory>
    <InlineData(2)>
    <InlineData(3)>
    Public Sub InStrRev_WhenStartGreatherThanLength_ReturnsZero(start As Integer)
      Assert.Equal(0, Strings.InStrRev("a", "a", start))
    End Sub

    <Theory>
    <InlineData(0)>
    <InlineData(-2)>
    <InlineData(-3)>
    Public Sub InStrRev_WhenStartZeroOrMinusTwoOrLess_ThrowsArgumentException(start As Integer)
      AssertExtensions.Throws(Of ArgumentException)("Start", Nothing, Function() Strings.InStrRev("a", "a", start))
    End Sub

    <Theory>
    <MemberData(NameOf(Join_Object_TestData))>
    <MemberData(NameOf(Join_String_TestData))>
    Public Sub Join_ObjectArray(source As Object(), delimiter As String, expected As String)
      Assert.Equal(expected, Strings.Join(source, delimiter))
    End Sub

    Public Shared Iterator Function Join_Object_TestData() As IEnumerable(Of Object())
      Yield New Object() {Array.Empty(Of Object)(), Nothing, Nothing}
      Yield New Object() {Array.Empty(Of Object)(), ",", Nothing}
      Yield New Object() {New Object() {1}, ",", "1"}
      Yield New Object() {New Object() {1, Nothing, 3}, Nothing, "13"}
      Yield New Object() {New Object() {True, False}, "", "TrueFalse"}
      Yield New Object() {New Object() {1, 2, 3}, ", ", "1, 2, 3"}
    End Function

    <Theory>
    <MemberData(NameOf(Join_String_TestData))>
    Public Sub Join_StringArray(source As String(), delimiter As String, expected As String)
      Assert.Equal(expected, Strings.Join(source, delimiter))
    End Sub

    Public Shared Iterator Function Join_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {Array.Empty(Of String)(), Nothing, Nothing}
      Yield New Object() {Array.Empty(Of String)(), ",", Nothing}
      Yield New Object() {New String() {"A"}, ",", "A"}
      Yield New Object() {New String() {"", Nothing, ""}, Nothing, ""}
      Yield New Object() {New String() {"", "AB", "C"}, "", "ABC"}
      Yield New Object() {New String() {"A", "B", "C"}, ", ", "A, B, C"}
    End Function

    <Theory>
    <InlineData(vbNullChar, vbNullChar)>
    <InlineData(ChrW(&HFFFF), ChrW(&HFFFF))>
    <InlineData("a"c, "a"c)>
    <InlineData("A"c, "a"c)>
    <InlineData("1"c, "1"c)>
    Public Sub LCase_Char(value As Char, expected As Char)
      Assert.Equal(expected, Strings.LCase(value))
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("", "")>
    <InlineData(vbNullChar, vbNullChar)>
    <InlineData(ChrW(65535), ChrW(65535))>
    <InlineData("abc", "abc")>
    <InlineData("ABC", "abc")>
    <InlineData("123", "123")>
    Public Sub LCase_String(value As String, expected As String)
      Assert.Equal(expected, Strings.LCase(value))
    End Sub

    <Theory>
    <InlineData(vbNullChar, vbNullChar)>
    <InlineData(ChrW(&HFFFF), ChrW(&HFFFF))>
    <InlineData("a"c, "A"c)>
    <InlineData("A"c, "A"c)>
    <InlineData("1"c, "1"c)>
    Public Sub UCase_Char(value As Char, expected As Char)
      Assert.Equal(expected, Strings.UCase(value))
    End Sub

    <Theory>
    <InlineData(Nothing, "")>
    <InlineData("", "")>
    <InlineData(vbNullChar, vbNullChar)>
    <InlineData(ChrW(65535), ChrW(65535))>
    <InlineData("abc", "ABC")>
    <InlineData("ABC", "ABC")>
    <InlineData("123", "123")>
    Public Sub UCase_String(value As String, expected As String)
      Assert.Equal(expected, Strings.UCase(value))
    End Sub

    <Theory>
    <InlineData("a", -1)>
    Public Sub Left_Invalid(str As String, length As Integer)
      AssertExtensions.Throws(Of ArgumentException)("Length", Nothing, Function() Strings.Left(str, length))
    End Sub

    <Theory>
    <InlineData("", 0, "")>
    <InlineData("", 1, "")>
    <InlineData("abc", 0, "")>
    <InlineData("abc", 2, "ab")>
    <InlineData("abc", Integer.MaxValue, "abc")>
    Public Sub Left_Valid(str As String, length As Integer, expected As String)
      Assert.Equal(expected, Strings.Left(str, length))
    End Sub

    <Theory>
    <MemberData(NameOf(Len_Object_Data))>
    <MemberData(NameOf(StructUtilsTestData.RecordsAndLength), MemberType:=GetType(StructUtilsTestData))>
    Public Sub Len_Object(o As Object, length As Integer)
      Assert.Equal(length, Strings.Len(o))
    End Sub

    Public Shared Function Len_Object_Data() As TheoryData(Of Object, Integer)
      Return New TheoryData(Of Object, Integer) From {
                  {Nothing, 0},
                  {New Boolean, 2},
                  {New SByte, 1},
                  {New Byte, 1},
                  {New Short, 2},
                  {New UShort, 2},
                  {New UInteger, 4},
                  {New Integer, 4},
                  {New ULong, 8},
                  {New Decimal, 16},
                  {New Single, 4},
                  {New Double, 8},
                  {New DateTime, 8},
                  {New Char, 2},
                  {"", 0},
                  {"a", 1},
                  {"ab", 2},
                  {"ab" & vbNullChar, 3}}
    End Function

    <Theory>
    <InlineData("a", -1)>
    Public Sub Right_Invalid(str As String, length As Integer)
      AssertExtensions.Throws(Of ArgumentException)("Length", Nothing, Function() Strings.Right(str, length))
    End Sub

    <Theory>
    <InlineData("", 0, "")>
    <InlineData("", 1, "")>
    <InlineData("abc", 0, "")>
    <InlineData("abc", 2, "bc")>
    <InlineData("abc", Integer.MaxValue, "abc")>
    Public Sub Right_Valid(str As String, length As Integer, expected As String)
      Assert.Equal(expected, Strings.Right(str, length))
    End Sub

    <Theory>
    <InlineData("a", -1)>
    Public Sub Mid2_Invalid(str As String, start As Integer)
      AssertExtensions.Throws(Of ArgumentException)("Start", Nothing, Function() Strings.Mid(str, start))
    End Sub

    <Theory>
    <InlineData("", 1, "")>
    <InlineData(Nothing, 1, Nothing)>
    <InlineData("abc", 1000, "")>
    <InlineData("abcde", 2, "bcde")>
    <InlineData("abc", 1, "abc")>   ' 1-based strings in VB
    <InlineData("abcd", 2, "bcd")>
    <InlineData("abcd", 3, "cd")>
    Public Sub Mid2_Valid(str As String, start As Integer, expected As String)
      Assert.Equal(expected, Strings.Mid(str, start))
    End Sub

    <Theory>
    <InlineData("a", 1, -1)>
    <InlineData("a", -1, 1)>
    Public Sub Mid3_Invalid(str As String, start As Integer, length As Integer)
      AssertExtensions.Throws(Of ArgumentException)(If(start < 1, "Start", "Length"), Nothing, Function() Strings.Mid(str, start, length))
    End Sub

    <Theory>
    <InlineData("", 1, 0, "")>
    <InlineData(Nothing, 1, 1, "")>
    <InlineData("abc", 1000, 1, "")>
    <InlineData("abcde", 2, 1000, "bcde")>
    <InlineData("abc", 1, 2, "ab")>   ' 1-based strings in VB
    <InlineData("abcd", 2, 2, "bc")>
    <InlineData("abcd", 2, 3, "bcd")>
    Public Sub Mid3_Valid(str As String, start As Integer, length As Integer, expected As String)
      Assert.Equal(expected, Strings.Mid(str, start, length))
    End Sub

    <Theory>
    <InlineData(Nothing, 0, "")>
    <InlineData(Nothing, 1, " ")>
    <InlineData("", 0, "")>
    <InlineData("", 1, " ")>
    <InlineData("A", 0, "")>
    <InlineData("A", 1, "A")>
    <InlineData("A", 2, "A ")>
    <InlineData("AB", 0, "")>
    <InlineData("AB", 1, "A")>
    <InlineData("AB", 2, "AB")>
    <InlineData("AB", 4, "AB  ")>
    Public Sub LSet(source As String, length As Integer, expected As String)
      Assert.Equal(expected, Strings.LSet(source, length))
    End Sub

    <Theory>
    <InlineData(Nothing, 0, "")>
    <InlineData(Nothing, 1, " ")>
    <InlineData("", 0, "")>
    <InlineData("", 1, " ")>
    <InlineData("A", 0, "")>
    <InlineData("A", 1, "A")>
    <InlineData("A", 2, " A")>
    <InlineData("AB", 0, "")>
    <InlineData("AB", 1, "A")>
    <InlineData("AB", 2, "AB")>
    <InlineData("AB", 4, "  AB")>
    Public Sub RSet(source As String, length As Integer, expected As String)
      Assert.Equal(expected, Strings.RSet(source, length))
    End Sub

    <Theory>
    <InlineData(Nothing, "")>
    <InlineData("", "")>
    <InlineData(" ", "")>
    <InlineData(" abc ", "abc ")>
    <InlineData("　" & vbLf & "abc ", vbLf & "abc ")>
    <InlineData(vbLf & "abc ", vbLf & "abc ")>
    <InlineData("abc ", "abc ")>
    <InlineData("abc", "abc")>
    Public Sub LTrim_Valid(str As String, expected As String)
      ' Trims only space and \u3000 specifically
      Assert.Equal(expected, Strings.LTrim(str))
    End Sub

    <Theory>
    <InlineData(Nothing, "")>
    <InlineData("", "")>
    <InlineData(" ", "")>
    <InlineData(" abc ", " abc")>
    <InlineData(" abc" & vbLf & "　", " abc" & vbLf)>
    <InlineData(" abc" & vbLf, " abc" & vbLf)>
    <InlineData(" abc", " abc")>
    <InlineData("abc", "abc")>
    Public Sub RTrim_Valid(str As String, expected As String)
      ' Trims only space and \u3000 specifically
      Assert.Equal(expected, Strings.RTrim(str))
    End Sub

    <Theory>
    <InlineData(Nothing, "")>
    <InlineData("", "")>
    <InlineData(" ", "")>
    <InlineData(" abc ", "abc")>
    <InlineData("abc" & vbLf & "　", "abc" & vbLf)>
    <InlineData("　abc" & vbLf & "　", "abc" & vbLf)>
    <InlineData("abc" & vbLf, "abc" & vbLf)>
    <InlineData("abc", "abc")>
    Public Sub Trim_Valid(str As String, expected As String)
      ' Trims only space and \u3000 specifically
      Assert.Equal(expected, Strings.Trim(str))
    End Sub

    <Theory>
    <InlineData("", "", Nothing, 1, -1, CompareMethod.Text, Nothing)>
    <InlineData("", Nothing, "", 1, -1, CompareMethod.Text, Nothing)>
    <InlineData("", "", "", 1, -1, CompareMethod.Text, Nothing)>
    <InlineData("ABC", "", "", 1, -1, CompareMethod.Text, "ABC")>
    <InlineData("ABC", "bc", "23", 1, -1, CompareMethod.Binary, "ABC")>
    <InlineData("ABC", "BC", "23", 1, -1, CompareMethod.Binary, "A23")>
    <InlineData("ABC", "bc", "23", 1, -1, CompareMethod.Text, "A23")>
    <InlineData("abcbc", "bc", "23", 1, -1, CompareMethod.Text, "a2323")>
    <InlineData("abcbc", "bc", "23", 1, 0, CompareMethod.Text, "abcbc")>
    <InlineData("abcbc", "bc", "23", 1, 1, CompareMethod.Text, "a23bc")>
    <InlineData("abc", "bc", "23", 2, -1, CompareMethod.Text, "23")>
    <InlineData("abc", "bc", "23", 3, -1, CompareMethod.Text, "c")>
    <InlineData("abc", "bc", "23", 4, -1, CompareMethod.Text, Nothing)>
    Public Sub Replace(expression As String, find As String, replacement As String, start As Integer, n As Integer, compare1 As CompareMethod, expected As String)
      Assert.Equal(expected, Strings.Replace(expression, find, replacement, start, n, compare1))
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing, Nothing, 0, 0, CompareMethod.Text)>
    <InlineData(Nothing, "", "", 0, 0, CompareMethod.Text)>
    Public Sub Replace_ArgumentException(expression As String, find As String, replacement As String, start As Integer, length As Integer, compare1 As CompareMethod)
      Assert.Throws(Of ArgumentException)(Function() Strings.Replace(expression, find, replacement, start, length, compare1))
    End Sub

    <Theory>
    <InlineData(0, "")>
    <InlineData(1, " ")>
    <InlineData(3, "   ")>
    Public Sub Space(number As Integer, expected As String)
      Assert.Equal(expected, Strings.Space(number))
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing, -1, CompareMethod.Text, New String() {""})>
    <InlineData(Nothing, "", -1, CompareMethod.Text, New String() {""})>
    <InlineData("", Nothing, -1, CompareMethod.Text, New String() {""})>
    <InlineData("", "", -1, CompareMethod.Text, New String() {""})>
    <InlineData("ABC", ",", -1, CompareMethod.Text, New String() {"ABC"})>
    <InlineData("A,,BC", ",", -1, CompareMethod.Text, New String() {"A", "", "BC"})>
    <InlineData("ABC", "b", -1, CompareMethod.Text, New String() {"A", "C"})>
    <InlineData("ABC", "b", -1, CompareMethod.Binary, New String() {"ABC"})>
    <InlineData("A, B, C", ", ", -1, CompareMethod.Text, New String() {"A", "B", "C"})>
    <InlineData("A, B, C", ", ", 1, CompareMethod.Text, New String() {"A, B, C"})>
    <InlineData("A, B, C", ", ", 2, CompareMethod.Text, New String() {"A", "B, C"})>
    <InlineData("A, B, C", ", ", Integer.MaxValue, CompareMethod.Text, New String() {"A", "B", "C"})>
    Public Sub Split(expression As String, delimiter As String, limit As Integer, compare1 As CompareMethod, expected As String())
      Assert.Equal(expected, Strings.Split(expression, delimiter, limit, compare1))
    End Sub

    <Theory>
    <InlineData("A, B, C", ", ", 0, CompareMethod.Text)>
    Public Sub Split_IndexOutOfRangeException(expression As String, delimiter As String, limit As Integer, compare1 As CompareMethod)
      Assert.Throws(Of IndexOutOfRangeException)(Function() Strings.Split(expression, delimiter, limit, compare1))
    End Sub

    <Theory>
    <InlineData("a", "a", 0, 0)>
    <InlineData("a", "b", -1, -1)>
    <InlineData("b", "a", 1, 1)>
    <InlineData("a", "ABC", 1, -1)>
    <InlineData("ABC", "a", -1, 1)>
    <InlineData("abc", "ABC", 1, 0)>
    Public Sub StrComp(left1 As String, right1 As String, expectedBinaryCompare As Integer, expectedTextCompare As Integer)
      Assert.Equal(expectedBinaryCompare, Strings.StrComp(left1, right1, CompareMethod.Binary))
      Assert.Equal(expectedTextCompare, Strings.StrComp(left1, right1, CompareMethod.Text))
    End Sub

    <Theory>
    <InlineData(Nothing, VbStrConv.None, 0, Nothing)>
    <InlineData("", VbStrConv.None, 0, "")>
    <InlineData("ABC123", VbStrConv.None, 0, "ABC123")>
    <InlineData("", VbStrConv.Lowercase, 0, "")>
    <InlineData("Abc123", VbStrConv.Lowercase, 0, "abc123")>
    <InlineData("Abc123", VbStrConv.Uppercase, 0, "ABC123")>
    Public Sub StrConv(str As String, conversion As Community.VisualBasic.VbStrConv, localeID As Integer, expected As String)
      If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
        Assert.Equal(expected, Strings.StrConv(str, conversion, localeID))
      Else
        Assert.Throws(Of PlatformNotSupportedException)(Function() Strings.StrConv(str, conversion, localeID))
      End If
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_Object_TestData))>
    <MemberData(NameOf(StrDup_Char_TestData))>
    <MemberData(NameOf(StrDup_String_TestData))>
    Public Sub StrDup_Int_Object_Object(number As Integer, character As Object, expected As Object)
      Assert.Equal(expected, Strings.StrDup(number, character))
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_Object_ArgumentException_TestData))>
    Public Sub StrDup_ArgumentException_Int_Object(number As Integer, character As Object)
      Assert.Throws(Of ArgumentException)(Function() Strings.StrDup(number, character))
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_Char_TestData))>
    Public Sub StrDup_Int_Char_String(number As Integer, character As Char, expected As String)
      Assert.Equal(expected, Strings.StrDup(number, character))
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_Char_ArgumentException_TestData))>
    Public Sub StrDup_ArgumentException_Int_Char(number As Integer, character As Char)
      Assert.Throws(Of ArgumentException)(Function() Strings.StrDup(number, character))
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_String_TestData))>
    Public Sub StrDup_Int_String_String(number As Integer, character As String, expected As String)
      Assert.Equal(expected, Strings.StrDup(number, character))
    End Sub

    <Theory>
    <MemberData(NameOf(StrDup_String_ArgumentException_TestData))>
    Public Sub StrDup_ArgumentException_Int_String(number As Integer, character As String)
      Assert.Throws(Of ArgumentException)(Function() Strings.StrDup(number, character))
    End Sub

    Public Shared Iterator Function StrDup_Object_TestData() As IEnumerable(Of Object())
      Return
    End Function

    Public Shared Iterator Function StrDup_Char_TestData() As IEnumerable(Of Object())
      Yield New Object() {3, vbNullChar, vbNullChar & vbNullChar & vbNullChar}
      Yield New Object() {0, "A"c, ""}
      Yield New Object() {1, "A"c, "A"}
      Yield New Object() {3, "A"c, "AAA"}
    End Function

    Public Shared Iterator Function StrDup_String_TestData() As IEnumerable(Of Object())
      Yield New Object() {0, "A", ""}
      Yield New Object() {1, "A", "A"}
      Yield New Object() {3, "A", "AAA"}
      Yield New Object() {0, "ABC", ""}
      Yield New Object() {1, "ABC", "A"}
      Yield New Object() {3, "ABC", "AAA"}
    End Function

    Public Shared Iterator Function StrDup_Object_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {-1, New Object}
      Yield New Object() {1, 0}
      Yield New Object() {1, AscW("A"c)}
      Yield New Object() {-1, "A"c}
      Yield New Object() {-1, "A"}
      Yield New Object() {1, ""}
    End Function

    Public Shared Iterator Function StrDup_Char_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {-1, "A"c}
    End Function

    Public Shared Iterator Function StrDup_String_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {-1, "A"}
      Yield New Object() {1, Nothing}
      Yield New Object() {1, ""}
    End Function

    <Theory>
    <InlineData(Nothing, "")>
    <InlineData("", "")>
    <InlineData(vbNullChar, vbNullChar)>
    <InlineData("ABC", "CBA")>
    <InlineData("🏈", "🏈")>
    <InlineData("A🏈", "🏈A")>
    Public Sub StrReverse(str As String, expected As String)
      Assert.Equal(expected, Strings.StrReverse(str))
    End Sub

    Public Shared Function InStr_TestData_NullsAndEmpties() As TheoryData(Of String, String, Integer)
      Return New TheoryData(Of String, String, Integer) From {
                  {Nothing, Nothing, 0},
                  {Nothing, "", 0},
                  {"", Nothing, 0},
                  {"", "", 0}}
    End Function

    Public Shared Function InStr_FromBegin_TestData() As TheoryData(Of String, String, Integer)
      Return New TheoryData(Of String, String, Integer) From {
                  {Nothing, "a", 0},
                  {"a", Nothing, 1},
                  {"a", "a", 1},
                  {"aa", "a", 1},
                  {"ab", "a", 1},
                  {"ba", "a", 2},
                  {"b", "a", 0},
                  {"a", "ab", 0},
                  {"ab", "ab", 1}}
    End Function

    Public Shared Function InStr_FromWithin_TestData() As TheoryData(Of String, String, Integer)
      Return New TheoryData(Of String, String, Integer) From {
                  {Nothing, "a", 0},
                  {"aa", Nothing, 2},
                  {"aa", "a", 2},
                  {"aab", "a", 2},
                  {"aba", "a", 3},
                  {"ab", "a", 0},
                  {"aa", "ab", 0},
                  {"abab", "ab", 3}}
    End Function

    Public Shared Function InStrRev_FromEnd_TestData() As TheoryData(Of String, String, Integer)
      Return New TheoryData(Of String, String, Integer) From {
                  {Nothing, "a", 0},
                  {"a", Nothing, 1},
                  {"a", "a", 1},
                  {"aa", "a", 2},
                  {"ba", "a", 2},
                  {"ab", "a", 1},
                  {"b", "a", 0},
                  {"a", "ab", 0},
                  {"ab", "ab", 1}}
    End Function

    Public Shared Function InStrRev_FromWithin_TestData() As TheoryData(Of String, String, Integer, Integer)
      Return New TheoryData(Of String, String, Integer, Integer) From {
                  {Nothing, Nothing, 1, 0},
                  {Nothing, "", 1, 0},
                  {"", Nothing, 1, 0},
                  {"", "", 1, 0},
                  {Nothing, "a", 1, 0},
                  {"aa", Nothing, 1, 1},
                  {"aa", "a", 1, 1},
                  {"baa", "a", 2, 2},
                  {"aba", "a", 2, 1},
                  {"ba", "a", 1, 0},
                  {"aa", "ab", 1, 0},
                  {"abab", "ab", 3, 1}}
    End Function

    Private Shared Function IsEnUS() As Boolean
      Return System.Threading.Thread.CurrentThread.CurrentCulture.Name = "en-US"
    End Function

  End Class

End Namespace