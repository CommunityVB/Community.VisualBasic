' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.IO
Imports Xunit

Namespace Global.Community.VisualBasic.FileIO.Tests
  Public Class TextFieldParserTests
    Inherits IOx.FileCleanupTestBase

    <Fact>
    Public Sub Constructors()

      Dim path = GetTestFilePath()
      File.WriteAllText(path, "abc123")
      Using stream As System.IO.FileStream = New FileStream(path, FileMode.Open)
        Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(stream)

        End Using
        Assert.Throws(Of ObjectDisposedException)(Function() stream.ReadByte())
      End Using
      Using stream As System.IO.FileStream = New FileStream(path, FileMode.Open)
        Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(stream, defaultEncoding:=System.Text.Encoding.Unicode, detectEncoding:=True, leaveOpen:=True)

        End Using
        Dim temp = stream.ReadByte()
      End Using
      Using reader As System.IO.StreamReader = New StreamReader(path)
        Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(reader)

        End Using
        Assert.Throws(Of ObjectDisposedException)(Function() reader.ReadToEnd())
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)

      End Using

      ' public TextFieldParser(string path)
      Assert.Throws(Of FileNotFoundException)(Function() As Community.VisualBasic.FileIO.TextFieldParser
                                                Return New TextFieldParser(GetTestFilePath())
                                              End Function)
    End Sub

    <Fact>
    Public Sub Close()

      Dim path = GetTestFilePath()
      File.WriteAllText(path, "abc123")
      Using stream As System.IO.FileStream = New FileStream(path, FileMode.Open)
        Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(stream)
          parser.Close()
        End Using
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.Close()
      End Using

      If True Then
        Dim parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.Close()
        parser.Close()
      End If

      If True Then
        Dim parser As TextFieldParser = Nothing
        Using InlineAssignHelper(Of TextFieldParser)(parser, New Community.VisualBasic.FileIO.TextFieldParser(path))

        End Using
        parser.Close()
      End If

    End Sub

    <Fact>
    Public Sub Properties()

      Dim path = GetTestFilePath()
      File.WriteAllText(path, "abc123")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Equal(Array.Empty(Of String)(), parser.CommentTokens)
        parser.CommentTokens = {"[", "]"}
        Assert.Equal({"[", "]"}, parser.CommentTokens)

        Assert.Null(parser.Delimiters)
        parser.Delimiters = {"A", "123"}
        Assert.Equal({"A", "123"}, parser.Delimiters)
        parser.SetDelimiters({"123", "B"})
        Assert.Equal({"123", "B"}, parser.Delimiters)

        Assert.Null(parser.FieldWidths)
        parser.FieldWidths = {1, 2, Integer.MaxValue}
        Assert.Equal({1, 2, Integer.MaxValue}, parser.FieldWidths)
        parser.SetFieldWidths({Integer.MaxValue, 3})
        Assert.Equal({Integer.MaxValue, 3}, parser.FieldWidths)
        Assert.Throws(Of ArgumentException)(Sub() parser.SetFieldWidths({-1, -1}))

        Assert.[True](parser.HasFieldsEnclosedInQuotes)
        parser.HasFieldsEnclosedInQuotes = False
        Assert.[False](parser.HasFieldsEnclosedInQuotes)

        Assert.Equal(FieldType.Delimited, parser.TextFieldType)
        parser.TextFieldType = FieldType.FixedWidth
        Assert.Equal(FieldType.FixedWidth, parser.TextFieldType)

        Assert.[True](parser.TrimWhiteSpace)
        parser.TrimWhiteSpace = False
        Assert.[False](parser.TrimWhiteSpace)
      End Using
    End Sub

    ' Not tested:
    '   public string[] CommentTokens { get { throw null; } set { } }

    <Fact>
    Public Sub ErrorLine()

      Dim path = GetTestFilePath()
      File.WriteAllText(path,
"abc 123
def 45
ghi 789")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.TextFieldType = FieldType.FixedWidth
        parser.SetFieldWidths({3, 4})

        Assert.Equal(-1, parser.ErrorLineNumber)
        Assert.Equal("", parser.ErrorLine)

        Assert.Equal({"abc", "123"}, parser.ReadFields())
        Assert.Equal(-1, parser.ErrorLineNumber)
        Assert.Equal("", parser.ErrorLine)

        Assert.Throws(Of MalformedLineException)(Function() parser.ReadFields())
        Assert.Equal(2, parser.ErrorLineNumber)
        Assert.Equal("def 45", parser.ErrorLine)

        Assert.Equal({"ghi", "789"}, parser.ReadFields())
        Assert.Equal(2, parser.ErrorLineNumber)
        Assert.Equal("def 45", parser.ErrorLine)
      End Using

    End Sub

    <Fact>
    Public Sub HasFieldsEnclosedInQuotes_TrimWhiteSpace()
      Dim path = GetTestFilePath()
      File.WriteAllText(path, """"", "" "" ,""abc"", "" 123 "" ,")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.Delimiters = {","}
        Assert.Equal({"", "", "abc", "123", ""}, parser.ReadFields())
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.TrimWhiteSpace = False
        parser.Delimiters = {","}
        Assert.Equal({"", " ", "abc", " 123 ", ""}, parser.ReadFields())
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.HasFieldsEnclosedInQuotes = False
        parser.Delimiters = {","}
        Assert.Equal({"""""", """ """, """abc""", """ 123 """, ""}, parser.ReadFields())
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.TrimWhiteSpace = False
        parser.HasFieldsEnclosedInQuotes = False
        parser.Delimiters = {","}
        Assert.Equal({"""""", " "" "" ", """abc""", " "" 123 "" ", ""}, parser.ReadFields())
      End Using
    End Sub

    <Fact>
    Public Sub PeekChars()

      Dim path = GetTestFilePath()
      File.WriteAllText(path,
"abc,123
def,456
ghi,789")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Throws(Of ArgumentException)(Function() parser.PeekChars(0))

        Assert.Equal("a", parser.PeekChars(1))
        Assert.Equal("abc,123", parser.PeekChars(10))

        Assert.Equal("abc,123", parser.ReadLine())

        parser.TextFieldType = FieldType.FixedWidth
        parser.SetFieldWidths({3, -1})

        Assert.Equal("d", parser.PeekChars(1))
        Assert.Equal("def,456", parser.PeekChars(10))
        Assert.Equal({"def", ",456"}, parser.ReadFields())

        parser.TextFieldType = FieldType.Delimited
        parser.SetDelimiters({","})

        Assert.Equal("g", parser.PeekChars(1))
        Assert.Equal("ghi,789", parser.PeekChars(10))
        Assert.Equal({"ghi", "789"}, parser.ReadFields())

        Assert.Null(parser.PeekChars(1))
        Assert.Null(parser.PeekChars(10))
      End Using

    End Sub

    <Fact>
    Public Sub ReadFields_FieldWidths()

      Dim path = GetTestFilePath()
      File.WriteAllText(path,
"abc,123
def,456
ghi,789")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.TextFieldType = FieldType.FixedWidth

        Assert.Throws(Of InvalidOperationException)(Function() parser.ReadFields())

        parser.SetFieldWidths({-1})
        Assert.Equal({"abc,123"}, parser.ReadFields())

        parser.SetFieldWidths({3, -1})
        Assert.Equal({"def", ",456"}, parser.ReadFields())

        parser.SetFieldWidths({3, 2})
        Assert.Equal({"ghi", ",7"}, parser.ReadFields())

        parser.SetFieldWidths({3, 2})
        Assert.Null(parser.ReadFields())
      End Using

    End Sub

    <Fact>
    Public Sub ReadFields_Delimiters_LineNumber()

      Dim path = GetTestFilePath()
      File.WriteAllText(path,
"abc,123
def,456
ghi,789")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Equal(1, parser.LineNumber)

        Assert.Throws(Of ArgumentException)(Function() parser.ReadFields())
        Assert.Equal(1, parser.LineNumber)

        parser.SetDelimiters({", "})
        Assert.Equal({"abc,123"}, parser.ReadFields())
        Assert.Equal(2, parser.LineNumber)

        parser.SetDelimiters({";", ","})
        Assert.Equal({"def", "456"}, parser.ReadFields())
        Assert.Equal(3, parser.LineNumber)

        parser.SetDelimiters({"g", "9"})
        Assert.Equal({"", "hi,78", ""}, parser.ReadFields())
        Assert.Equal(-1, parser.LineNumber)
      End Using

      File.WriteAllText(path,
",,

,
")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Equal(1, parser.LineNumber)

        parser.SetDelimiters({","})
        Assert.Equal({"", "", ""}, parser.ReadFields())
        Assert.Equal(2, parser.LineNumber)

        Assert.Equal({"", ""}, parser.ReadFields())
        Assert.Equal(-1, parser.LineNumber)

        Assert.Null(parser.ReadFields())
        Assert.Equal(-1, parser.LineNumber)

        Assert.Null(parser.ReadFields())
        Assert.Equal(-1, parser.LineNumber)
      End Using
    End Sub
    <Fact>
    Public Sub ReadLine_ReadToEnd()
      Dim path = GetTestFilePath()
      File.WriteAllText(path,
"abc
123")
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.[False](parser.EndOfData)

        Assert.Equal(
"abc
123",
            parser.ReadToEnd())
        Assert.Equal(-1, parser.LineNumber)
        Assert.[True](parser.EndOfData)
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Equal("abc", parser.ReadLine())
        Assert.Equal(2, parser.LineNumber)
        Assert.[False](parser.EndOfData)

        Assert.Equal("123", parser.ReadToEnd())
        Assert.Equal(-1, parser.LineNumber)
        Assert.[True](parser.EndOfData)
      End Using
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        Assert.Equal("abc", parser.ReadLine())
        Assert.Equal(2, parser.LineNumber)
        Assert.[False](parser.EndOfData)

        Assert.Equal("123", parser.ReadLine())
        Assert.Equal(-1, parser.LineNumber)
        Assert.[True](parser.EndOfData)

        Assert.Null(parser.ReadToEnd())
        Assert.Equal(-1, parser.LineNumber)
        Assert.[True](parser.EndOfData)
      End Using

    End Sub

    <Fact>
    Public Sub UnmatchedQuote_MalformedLineException()
      Dim path = GetTestFilePath()
      File.WriteAllText(path, """"", """)
      Using parser As Community.VisualBasic.FileIO.TextFieldParser = New TextFieldParser(path)
        parser.Delimiters = {","}
        Assert.Throws(Of MalformedLineException)(Function() parser.ReadFields())
      End Using
    End Sub

    '<Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
      target = value
      Return value
    End Function

  End Class

End Namespace