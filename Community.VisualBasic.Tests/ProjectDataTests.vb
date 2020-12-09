'' Licensed to the .NET Foundation under one or more agreements.
'' The .NET Foundation licenses this file to you under the MIT license.

'Option Compare Text
'Option Explicit On
'Option Infer On
'Option Strict On

'Imports Microsoft.DotNet.RemoteExecutor
'Imports System
'Imports Xunit

'Namespace Global.Community.VisualBasic.CompilerServices.Tests

'  Public Class ProjectDataTests

'    <Fact>
'    Public Sub CreateProjectError()
'      Dim temp = Assert.Throws(Of ArgumentException)(Function() ProjectData.CreateProjectError(0)).ToString()
'      temp = Assert.IsType(Of Exception)(ProjectData.CreateProjectError(1)).ToString()
'      temp = Assert.IsType(Of OutOfMemoryException)(ProjectData.CreateProjectError(7)).ToString()
'      temp = Assert.IsType(Of Exception)(ProjectData.CreateProjectError(32768)).ToString()
'      temp = Assert.IsType(Of Exception)(ProjectData.CreateProjectError(40068)).ToString()
'      temp = Assert.IsType(Of Exception)(ProjectData.CreateProjectError(41000)).ToString()
'    End Sub

'    <Fact>
'    Public Sub SetProjectError()

'      Dim e As Exception = New ArgumentException
'      ProjectData.SetProjectError(e)
'      Assert.Same(e, Information.Err().GetException())
'      Assert.Equal(0, Information.Err().Erl)

'      e = New InvalidOperationException
'      ProjectData.SetProjectError(e, 3)
'      Assert.Same(e, Information.Err().GetException())
'      Assert.Equal(3, Information.Err().Erl)

'      e = New Exception
'      ProjectData.SetProjectError(e)
'      Assert.Same(e, Information.Err().GetException())
'      Assert.Equal(0, Information.Err().Erl)

'    End Sub

'    <Fact>
'    Public Sub ClearProjectError()
'      ProjectData.SetProjectError(New ArgumentException, 3)
'      ProjectData.ClearProjectError()
'      Assert.Null(Information.Err().GetException())
'      Assert.Equal(0, Information.Err().Erl)
'    End Sub

'    <ConditionalFact(GetType(RemoteExecutor), NameOf(RemoteExecutor.IsSupported))>
'    Public Sub EndApp()
'      RemoteExecutor.Invoke(
'          New Action(Sub()
'                       ' See FileSystemTests.CloseAllFiles() for a test that EndApp() closes open files.
'                       ProjectData.EndApp()
'                       Throw New Exception ' Shouldn't reach here.
'                     End Sub),
'          New RemoteInvokeOptions() With {}.ExpectedExitCode = 0}).Dispose()
'    End Sub

'  End Class

'End Namespace