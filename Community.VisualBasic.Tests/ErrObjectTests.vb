' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports Community.VisualBasic.CompilerServices
Imports System
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class ErrObjectTests

    '<ActiveIssue("https://github.com/mono/mono/issues/14854", TestRuntimes.Mono)>
    <Fact>
    Public Sub Clear()
      ProjectData.ClearProjectError()
      ProjectData.SetProjectError(New ArgumentException, 3)
      Dim errObj As Community.VisualBasic.ErrObject = Information.Err()
      errObj.Number = 5
      errObj.Description = "Description"
      errObj.HelpContext = 6
      errObj.HelpFile = "File"
      errObj.Source = "Source"
      errObj.Clear()
      Assert.Equal(0, errObj.Erl)
      Assert.Equal(0, errObj.HelpContext)
      Assert.Equal("", errObj.HelpFile)
      Assert.Equal("", errObj.Source)
      Assert.Equal(0, errObj.LastDllError)
      Assert.Equal(0, errObj.Number)
      Assert.Equal("", errObj.Description)
      Assert.Null(errObj.GetException())
      Assert.Equal(0, Information.Erl())
    End Sub

    <Fact>
    Public Sub Raise()
      ProjectData.ClearProjectError()

      ProjectData.SetProjectError(New Exception)
      Dim temp = Assert.Throws(Of ArgumentException)(Sub() Information.Err().Raise(0)).ToString()

      ProjectData.SetProjectError(New Exception)
      temp = Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7)).ToString()

      ProjectData.SetProjectError(New ArgumentException)
      temp = Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7)).ToString()

      ProjectData.SetProjectError(New ArgumentException)
      temp = Assert.Throws(Of Exception)(Sub() Information.Err().Raise(32768)).ToString()

      ProjectData.SetProjectError(New InvalidOperationException)
      temp = Assert.Throws(Of Exception)(Sub() Information.Err().Raise(1, Description:="MyDescription")).ToString()
    End Sub

    <Fact>
    Public Sub Source()
      ProjectData.ClearProjectError()

      ProjectData.SetProjectError(New Exception() With {
          .Source = Nothing})
      Assert.Null(Information.Err().Source)

      ProjectData.SetProjectError(New Exception() With {
          .Source = "MySource1"})
      Assert.Equal("MySource1", Information.Err().Source)

      ProjectData.SetProjectError(New Exception() With {
          .Source = Nothing})
      Assert.Null(Information.Err().Source)

      ProjectData.SetProjectError(New Exception() With {
          .Source = Nothing})
      Dim temp = Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7, Source:="MySource2"))
      Assert.Equal("MySource2", Information.Err().Source)
    End Sub

    <Fact>
    Public Sub FilterDefaultMessage()
      ProjectData.ClearProjectError()

      Dim message As String = "Description"
      ProjectData.SetProjectError(New IO.FileNotFoundException(message))
      Assert.Equal(message, Information.Err().Description)

      message = ""
      ProjectData.SetProjectError(New IO.FileNotFoundException(message))
      Assert.NotEqual(message, Information.Err().Description)

      message = "Exception from HRESULT: 0x80"
      ProjectData.SetProjectError(New IO.FileNotFoundException(message))
      Assert.NotEqual(message, Information.Err().Description)
    End Sub

    <Fact>
    Public Sub MakeHelpLink()
      ProjectData.ClearProjectError()

      ProjectData.SetProjectError(New ArgumentException)
      Assert.Equal("#0", Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7)).HelpLink)

      ProjectData.SetProjectError(New ArgumentException)
      Assert.Equal("#3", Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7, HelpContext:=3)).HelpLink)

      ProjectData.SetProjectError(New ArgumentException)
      Assert.Equal("MyFile1#3", Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7, HelpFile:="MyFile1")).HelpLink)

      ProjectData.ClearProjectError()
      ProjectData.SetProjectError(New ArgumentException)
      Assert.Equal("MyFile2#0", Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7, HelpFile:="MyFile2")).HelpLink)

      ProjectData.SetProjectError(New ArgumentException)
      Assert.Equal("MyFile3#3", Assert.Throws(Of OutOfMemoryException)(Sub() Information.Err().Raise(7, HelpContext:=3, HelpFile:="MyFile3")).HelpLink)
    End Sub

    <Theory>
    <InlineData(Nothing, 0, "")>
    <InlineData("", 0, "")>
    <InlineData("#0", 0, "")>
    <InlineData("#1", 1, "")>
    <InlineData("#-3", -3, "")>
    <InlineData("MyFile1", 0, "MyFile1")>
    <InlineData("MyFile4#4", 4, "MyFile4")>
    Public Sub ParseHelpLink(helpLink1 As String, expectedHelpContext As Integer, expectedHelpFile As String)
      ProjectData.ClearProjectError()
      ProjectData.SetProjectError(New ArgumentException() With {
          .HelpLink = helpLink1})
      Assert.Equal(expectedHelpContext, Information.Err().HelpContext)
      Assert.Equal(expectedHelpFile, Information.Err().HelpFile)
    End Sub

    <Theory>
    <InlineData("#")>
    <InlineData("##")>
    <InlineData("##2")>
    <InlineData("MyFile2#")>
    <InlineData("MyFile3##")>
    Public Sub ParseHelpLink_InvalidCastException(helpLink1 As String)
      ProjectData.ClearProjectError()
      ProjectData.SetProjectError(New ArgumentException() With {
          .HelpLink = helpLink1})
      Assert.Throws(Of InvalidCastException)(Function() Information.Err().HelpContext)
      Assert.Throws(Of InvalidCastException)(Function() Information.Err().HelpFile)
    End Sub

  End Class

End Namespace