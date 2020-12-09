' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Threading

Namespace Global.Community.IOx

  ''' <summary>Base class for test classes the use temporary files that need to be cleaned up.</summary>
  Public MustInherit Class FileCleanupTestBase
    Implements IDisposable

    Private Shared ReadOnly s_isElevated As New Lazy(Of Boolean)(Function() AdminHelpers.IsProcessElevated())
    Private ReadOnly fallbackGuid As String = Guid.NewGuid().ToString("N").Substring(0, 10)

    Protected Shared ReadOnly Property IsProcessElevated As Boolean
      Get
        Return s_isElevated.Value
      End Get
    End Property

    ''' <summary>Initialize the test class base.  This creates the associated test directory.</summary>
    Protected Sub New()
      ' Use a unique test directory per test class.  The test directory lives in the user's temp directory,
      ' and includes both the name of the test class and a random string.  The test class name is included
      ' so that it can be easily correlated if necessary, and the random string to helps avoid conflicts if
      ' the same test should be run concurrently with itself (e.g. if a [Fact] method lives on a base class)
      ' or if some stray files were left over from a previous run.

      ' Make 3 attempts since we have seen this on rare occasions fail with access denied, perhaps due to machine
      ' configuration, and it doesn't make sense to fail arbitrary tests for this reason.
      Dim failure As String = String.Empty
      For i As Integer = 0 To 2
        TestDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), [GetType]().Name & "_" & System.IO.Path.GetRandomFileName())
        Try
          System.IO.Directory.CreateDirectory(TestDirectory)
          Exit For
        Catch ex As Exception
          failure += ex.ToString() & Environment.NewLine
          Thread.Sleep(10)
        End Try
      Next

      Assert.[True](System.IO.Directory.Exists(TestDirectory), $"FileCleanupTestBase failed to create {TestDirectory}. {failure}")
    End Sub
    ''' <summary>Delete the associated test directory.</summary>
    Protected Overrides Sub Finalize()
      Dispose(False)
    End Sub
    ''' <summary>Delete the associated test directory.</summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub
    ''' <summary>Delete the associated test directory.</summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
      ' No managed resources to clean up, so disposing is ignored.

      Try
        System.IO.Directory.Delete(TestDirectory, recursive:=True)
      Catch
        ' avoid exceptions escaping Dispose

      End Try ' avoid exceptions escaping Dispose
    End Sub

    ''' <summary>
    ''' Gets the test directory into which all files and directories created by tests should be stored.
    ''' This directory is isolated per test class.
    ''' </summary>
    Protected ReadOnly Property TestDirectory As String
    ''' <summary>Gets a test file full path that is associated with the call site.</summary>
    ''' <param name="index">An optional index value to use as a suffix on the file name.  Typically a loop index.</param>
    ''' <param name="memberName">The member name of the function calling this method.</param>
    ''' <param name="lineNumber">The line number of the function calling this method.</param>
    Protected Function GetTestFilePath(Optional index As Integer? = Nothing, <CallerMemberName> Optional memberName As String = Nothing, <CallerLineNumber> Optional lineNumber As Integer = 0) As String
      Return System.IO.Path.Combine(TestDirectory, GetTestFileName(index, memberName, lineNumber))
    End Function
    ''' <summary>Gets a test file name that is associated with the call site.</summary>
    ''' <param name="index">An optional index value to use as a suffix on the file name.  Typically a loop index.</param>
    ''' <param name="memberName">The member name of the function calling this method.</param>
    ''' <param name="lineNumber">The line number of the function calling this method.</param>
    Protected Function GetTestFileName(Optional index As Integer? = Nothing, <CallerMemberName> Optional memberName As String = Nothing, <CallerLineNumber> Optional lineNumber As Integer = 0) As String
      Dim testFileName As String = GenerateTestFileName(index, memberName, lineNumber)
      Dim testFilePath As String = System.IO.Path.Combine(TestDirectory, testFileName)

      Const maxLength As Integer = 260 - 5 ' Windows MAX_PATH minus a bit

      Dim excessLength As Integer = testFilePath.Length - maxLength

      If excessLength > 0 Then
        ' The path will be too long for Windows -- can we
        ' trim memberName to fix it?
        If excessLength < memberName.Length + "...".Length Then
          ' Take a chunk out of the middle as perhaps it's the least interesting part of the name
          memberName = memberName.Substring(0, memberName.Length \ 2 - excessLength \ 2) & "..." & memberName.Substring(memberName.Length \ 2 + excessLength \ 2)

          testFileName = GenerateTestFileName(index, memberName, lineNumber)
          testFilePath = System.IO.Path.Combine(TestDirectory, testFileName)
        Else
          Return fallbackGuid
        End If
      End If

      Debug.Assert(testFilePath.Length <= maxLength + "...".Length)

      Return testFileName
    End Function

    Private Shared Function GenerateTestFileName(index As Integer?, memberName As String, lineNumber As Integer) As String ' randomness to avoid collisions between derived test classes using same base method concurrently
      Return String.Format(If(index.HasValue, "{0}_{1}_{2}_{3}", "{0}_{1}_{3}"),
                           If(memberName, "TestBase"),
                           lineNumber,
                           index.GetValueOrDefault(),
                           Guid.NewGuid().ToString("N").Substring(0, 8))
    End Function

  End Class

End Namespace