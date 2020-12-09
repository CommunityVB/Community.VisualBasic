'' Licensed to the .NET Foundation under one or more agreements.
'' The .NET Foundation licenses this file to you under the MIT license.

'Option Compare Text
'Option Explicit On
'Option Infer On
'Option Strict On

'Imports System.Linq
'Imports Microsoft.DotNet.RemoteExecutor
'Imports Xunit
'Imports Xunit.Sdk
'Imports Community.IOx

'Namespace Global.Community.VisualBasic.Tests

'  Public Class FileSystemTests
'    Inherits IOx.FileCleanupTestBase

'    Protected Overrides Sub Dispose(disposing As Boolean)
'      Try
'        FileSystem.FileClose(0) ' close all files
'      Catch __unusedException1__ As Exception

'      End Try
'      MyBase.Dispose(disposing)
'    End Sub

'    ' On OSX, the temp directory /tmp/ is a symlink to /private/tmp, so setting the current
'    ' directory to a symlinked path will result in GetCurrentDirectory returning the absolute
'    ' path that followed the symlink.
'    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNotOSX))>
'    Public Sub ChDir()
'      Dim savedDirectory As String = IO.Directory.GetCurrentDirectory()
'      FileSystem.ChDir(TestDirectory)
'      Assert.Equal(TestDirectory, IO.Directory.GetCurrentDirectory())
'      FileSystem.ChDir(savedDirectory)
'      Assert.Equal(savedDirectory, IO.Directory.GetCurrentDirectory())
'    End Sub

'    ' Not tested:
'    '   public static void ChDrive(char Drive){ throw null; }
'    '   public static void ChDrive(string Drive){ throw null; }

'    <ConditionalFact(GetType(RemoteExecutor), NameOf(RemoteExecutor.IsSupported))>
'    Public Sub CloseAllFiles()
'      ' TODO Check: Local function was replaced with Lambda
'      Dim putStringNoClose As Action(Of String, String) = Sub(fileName As String, str As String)

'                                                            Dim fileNumber As Integer = FileSystem.FreeFile()
'                                                            FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'                                                            FileSystem.FilePut(fileNumber, str)

'                                                          End Sub
'      ' TODO Check: Local function was replaced with Lambda
'      Dim getString As Func(Of String, String) = Function(fileName As String) As String

'                                                   Dim fileNumber As Integer = FileSystem.FreeFile()
'                                                   FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'                                                   Dim str As String = Nothing
'                                                   FileSystem.FileGet(fileNumber, str)
'                                                   FileSystem.FileClose(fileNumber)
'                                                   Return str

'                                                 End Function
'      Dim fileName1 = GetTestFilePath()
'      Dim fileName2 = GetTestFilePath()

'      RemoteExecutor.Invoke(Sub(fileName1, fileName2)
'                              putStringNoClose(fileName1, "abc")
'                              putStringNoClose(fileName2, "123")

'                              ' ProjectData.EndApp() should close all open files.
'                              Microsoft.VisualBasic.CompilerServices.ProjectData.EndApp()
'                              ' TODO Check: Local function was replaced with Lambda
'                              Dim putStringNoClose As Action(Of String, String) = Sub(fileName As String, str As String)

'                                                                                    Dim fileNumber As Integer = FileSystem.FreeFile()
'                                                                                    FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'                                                                                    FileSystem.FilePut(fileNumber, str)

'                                                                                  End Sub
'                            End Sub,
'                            fileName1,
'                            fileName2,
'                            New RemoteInvokeOptions() With {.ExpectedExitCode = 0}).Dispose()

'      ' Verify all text was written to the files.
'      Assert.Equal("abc", getString(fileName1))
'      Assert.Equal("123", getString(fileName2))

'    End Sub

'    ' Can't get current directory on OSX before setting it.
'    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNotOSX))>
'    Public Sub CurDir()
'      Assert.Equal(FileSystem.CurDir(), IO.Directory.GetCurrentDirectory())
'    End Sub

'    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsWindows))>
'    Public Sub CurDir_Drive()
'      Dim currentDirectory As String = IO.Directory.GetCurrentDirectory()
'      Dim index As Integer = currentDirectory.IndexOf(System.IO.Path.VolumeSeparatorChar)
'      If index = 1 Then
'        Assert.Equal(currentDirectory, FileSystem.CurDir(AscW(currentDirectory(0))))
'      End If
'    End Sub

'    <Fact>
'    Public Sub Dir()
'      Dim fileNames = Enumerable.Range(0, 3).[Select](Function(i) GetTestFileName(i)).ToArray()
'      Dim n As Integer = fileNames.Length

'      For i As Integer = 0 To n - 1
'        IO.File.WriteAllText(IO.Path.Combine(TestDirectory, fileNames(i)), i.ToString())
'      Next

'      ' Get all files.
'      Dim fileName As String = FileSystem.Dir(IO.Path.Combine(TestDirectory, "*"))
'      Dim foundNames As String() = New String(n - 1) {}
'      For i As Integer = 0 To n - 1
'        foundNames(i) = fileName
'        fileName = FileSystem.Dir()
'      Next
'      Assert.Null(fileName)

'      Array.Sort(fileNames)
'      Array.Sort(foundNames)
'      For i As Integer = 0 To n - 1
'        Assert.Equal(fileNames(i), foundNames(i))
'      Next

'      ' Get single file.
'      fileName = FileSystem.Dir(IO.Path.Combine(TestDirectory, fileNames(2)))
'      Assert.Equal(fileName, fileNames(2))
'      fileName = FileSystem.Dir()
'      Assert.Null(fileName)

'      ' Get missing file.
'      fileName = FileSystem.Dir(GetTestFilePath(n))
'      Assert.Equal("", fileName)
'    End Sub

'    <Fact>
'    Public Sub Dir_Volume()
'      If PlatformDetection.IsWindows Then
'        Dim temp = FileSystem.Dir(TestDirectory, FileAttribute.Volume)
'      Else
'        AssertThrows(Of PlatformNotSupportedException)(Sub() FileSystem.Dir(TestDirectory, FileAttribute.Volume))
'      End If
'    End Sub

'    <Fact>
'    Public Sub EOF()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output)
'      FileSystem.Write(fileNumber, "a"c)
'      FileSystem.Write(fileNumber, "b"c)
'      FileSystem.Write(fileNumber, "c"c)
'      FileSystem.FileClose(fileNumber)

'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input)
'      For i As Integer = 0 To 3 - 1
'        Assert.[False](FileSystem.EOF(fileNumber))
'        Dim c As Char = CType(Nothing, Char)
'        FileSystem.Input(fileNumber, c)
'      Next
'      Assert.[True](FileSystem.EOF(fileNumber))
'      FileSystem.FileClose(fileNumber)
'    End Sub

'    ' Not tested:
'    '   public static OpenMode FileAttr(int FileNumber){ throw null; }

'    <Fact>
'    Public Sub FileClose()
'      ' TODO Check: Local function was replaced with Lambda
'      Dim createAndOpenFile As Action(Of Integer) = Sub(fileNumber As Integer)

'                                                      Dim fileName = GetTestFilePath()
'                                                      IO.File.WriteAllText(fileName, "abc123")
'                                                      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input)

'                                                    End Sub
'      Dim fileNumber As Integer = FileSystem.FreeFile()

'      ' Close before opening.
'      FileSystem.FileClose(fileNumber)

'      createAndOpenFile(fileNumber)
'      FileSystem.FileClose(fileNumber)

'      ' Close a second time.
'      FileSystem.FileClose(fileNumber)

'      ' Close all files.
'      fileNumber = FileSystem.FreeFile()
'      createAndOpenFile(fileNumber)
'      createAndOpenFile(FileSystem.FreeFile())
'      Assert.NotEqual(fileNumber, FileSystem.FreeFile())
'      FileSystem.FileClose(0)
'      Assert.Equal(fileNumber, FileSystem.FreeFile())

'    End Sub

'    <Fact>
'    Public Sub FileCopy()
'      ' Copy to a new file.
'      Dim sourceName = GetTestFilePath()
'      IO.File.WriteAllText(sourceName, "abc")
'      Dim destName = GetTestFilePath()
'      FileSystem.FileCopy(sourceName, destName)
'      Assert.Equal("abc", IO.File.ReadAllText(destName))

'      ' Copy over an existing file.
'      sourceName = GetTestFilePath()
'      IO.File.WriteAllText(sourceName, "def")
'      destName = GetTestFilePath()
'      IO.File.WriteAllText(destName, "123")
'      FileSystem.FileCopy(sourceName, destName)
'      Assert.Equal("def", IO.File.ReadAllText(destName))
'    End Sub

'    ' Not tested:
'    '   public static System.DateTime FileDateTime(string PathName){ throw null; }

'    <Fact>
'    Public Sub FileGet_FilePut()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      Dim dateTime1 As New DateTime(2019, 1, 1)

'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'      FileSystem.FilePut(fileNumber, True)
'      FileSystem.FilePut(fileNumber, CByte(1))
'      FileSystem.FilePut(fileNumber, ChrW(2))
'      FileSystem.FilePut(fileNumber, CDec(3))
'      FileSystem.FilePut(fileNumber, 4.0)
'      FileSystem.FilePut(fileNumber, 5.0F)
'      FileSystem.FilePut(fileNumber, 6)
'      FileSystem.FilePut(fileNumber, 7L)
'      FileSystem.FilePut(fileNumber, CShort(Fix(8)))
'      FileSystem.FilePut(fileNumber, "ABC")
'      FileSystem.FilePut(fileNumber, dateTime1)
'      FileSystem.FileClose(fileNumber)

'      Dim _bool As Boolean = CType(Nothing, Boolean)
'      Dim _byte As Byte = CType(Nothing, Byte)
'      Dim _char As Char = CType(Nothing, Char)
'      Dim _decimal As Decimal = CType(Nothing, Decimal)
'      Dim _double As Double = CType(Nothing, Double)
'      Dim _float As Single = CType(Nothing, Single)
'      Dim _int As Integer = CType(Nothing, Integer)
'      Dim _long As Long = CType(Nothing, Long)
'      Dim _short As Short = CType(Nothing, Short)
'      Dim _string As String = CType(Nothing, String)
'      Dim _dateTime As DateTime = CType(Nothing, DateTime)
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'      FileSystem.FileGet(fileNumber, _bool)
'      FileSystem.FileGet(fileNumber, _byte)
'      FileSystem.FileGet(fileNumber, _char)
'      FileSystem.FileGet(fileNumber, _decimal)
'      FileSystem.FileGet(fileNumber, _double)
'      FileSystem.FileGet(fileNumber, _float)
'      FileSystem.FileGet(fileNumber, _int)
'      FileSystem.FileGet(fileNumber, _long)
'      FileSystem.FileGet(fileNumber, _short)
'      FileSystem.FileGet(fileNumber, _string)
'      FileSystem.FileGet(fileNumber, _dateTime)
'      Assert.[True](FileSystem.EOF(fileNumber))
'      FileSystem.FileClose(fileNumber)

'      Assert.[True](_bool)
'      Assert.Equal(CByte(1), _byte)
'      Assert.Equal(ChrW(2), _char)
'      Assert.Equal(CDec(3), _decimal)
'      Assert.Equal(4.0, _double)
'      Assert.Equal(5.0F, _float)
'      Assert.Equal(6, _int)
'      Assert.Equal(7L, _long)
'      Assert.Equal(CShort(Fix(8)), _short)
'      Assert.Equal("ABC", _string)
'      Assert.Equal(dateTime1, _dateTime)
'    End Sub

'    ' Not tested:
'    '   public static void FileGet(int FileNumber, ref System.Array Value, long RecordNumber = -1, bool ArrayIsDynamic = false, bool StringIsFixedLength = false) { }
'    '   public static void FileGet(int FileNumber, ref System.ValueType Value, long RecordNumber = -1) { }
'    '   public static void FilePut(int FileNumber, System.Array Value, long RecordNumber = -1, bool ArrayIsDynamic = false, bool StringIsFixedLength = false) { }
'    '   public static void FilePut(int FileNumber, System.ValueType Value, long RecordNumber = -1) { }
'    '   public static void FilePut(object FileNumber, object Value, object RecordNumber/* = -1*/) { }

'    <Fact>
'    Public Sub FileGetObject_FilePutObject()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      Dim dateTime1 As New DateTime(2019, 1, 1)

'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'      FileSystem.FilePutObject(fileNumber, True)
'      FileSystem.FilePutObject(fileNumber, CByte(1))
'      FileSystem.FilePutObject(fileNumber, ChrW(2))
'      FileSystem.FilePutObject(fileNumber, CDec(3))
'      FileSystem.FilePutObject(fileNumber, 4.0)
'      FileSystem.FilePutObject(fileNumber, 5.0F)
'      FileSystem.FilePutObject(fileNumber, 6)
'      FileSystem.FilePutObject(fileNumber, 7L)
'      FileSystem.FilePutObject(fileNumber, CShort(Fix(8)))
'      FileSystem.FilePutObject(fileNumber, "ABC")
'      FileSystem.FilePutObject(fileNumber, dateTime1)
'      FileSystem.FileClose(fileNumber)

'      Dim _bool As Object = Nothing
'      Dim _byte As Object = Nothing
'      Dim _char As Object = Nothing
'      Dim _decimal As Object = Nothing
'      Dim _double As Object = Nothing
'      Dim _float As Object = Nothing
'      Dim _int As Object = Nothing
'      Dim _long As Object = Nothing
'      Dim _short As Object = Nothing
'      Dim _string As Object = Nothing
'      Dim _dateTime As Object = Nothing
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'      FileSystem.FileGetObject(fileNumber, _bool)
'      FileSystem.FileGetObject(fileNumber, _byte)
'      FileSystem.FileGetObject(fileNumber, _char)
'      FileSystem.FileGetObject(fileNumber, _decimal)
'      FileSystem.FileGetObject(fileNumber, _double)
'      FileSystem.FileGetObject(fileNumber, _float)
'      FileSystem.FileGetObject(fileNumber, _int)
'      FileSystem.FileGetObject(fileNumber, _long)
'      FileSystem.FileGetObject(fileNumber, _short)
'      FileSystem.FileGetObject(fileNumber, _string)
'      FileSystem.FileGetObject(fileNumber, _dateTime)
'      Assert.[True](FileSystem.EOF(fileNumber))
'      FileSystem.FileClose(fileNumber)

'      Assert.Equal(CObj(True), _bool)
'      Assert.Equal(CByte(1), _byte)
'      Assert.Equal(ChrW(2), _char)
'      Assert.Equal(CDec(3), _decimal)
'      Assert.Equal(4.0, _double)
'      Assert.Equal(5.0F, _float)
'      Assert.Equal(6, _int)
'      Assert.Equal(7L, _long)
'      Assert.Equal(CShort(Fix(8)), _short)
'      Assert.Equal("ABC", _string)
'      Assert.Equal(dateTime1, _dateTime)
'    End Sub

'    <Fact>
'    Public Sub FileLen()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName As String = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Append)
'      FileSystem.FileClose(fileNumber)
'      Assert.Equal(0, FileSystem.FileLen(fileName))

'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      IO.File.WriteAllText(fileName, "abc123")
'      Assert.Equal(6, FileSystem.FileLen(fileName))
'    End Sub

'    <Fact>
'    Public Sub FileOpen()
'      ' OpenMode.Append:
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName As String = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Append)
'      FileSystem.FileClose(fileNumber)

'      ' OpenMode.Binary:
'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Binary)
'      FileSystem.FileClose(fileNumber)

'      ' OpenMode.Input:
'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      AssertThrows(Of IO.FileNotFoundException)(Sub() FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input))
'      IO.File.WriteAllText(fileName, "abc123")
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input)
'      FileSystem.FileClose(fileNumber)

'      ' OpenMode.Output:
'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output)
'      FileSystem.FileClose(fileNumber)

'      ' OpenMode.Random:
'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Random)
'      FileSystem.FileClose(fileNumber)

'      ' Open a second time.
'      fileNumber = FileSystem.FreeFile()
'      fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Append)
'      AssertThrows(Of IO.IOException)(Sub() FileSystem.FileOpen(fileNumber, fileName, OpenMode.Append))
'      FileSystem.FileClose(fileNumber)

'      ' Open an invalid fileNumber.
'      AssertThrows(Of IO.IOException)(Sub() FileSystem.FileOpen(256, GetTestFilePath(), OpenMode.Append))
'    End Sub

'    ' Not tested:
'    '   public static void FileWidth(int FileNumber, int RecordWidth) { }

'    <Fact>
'    Public Sub FreeFile()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Assert.Equal(fileNumber, FileSystem.FreeFile())
'      FileSystem.FileOpen(fileNumber, GetTestFilePath(), OpenMode.Append)
'      Assert.NotEqual(fileNumber, FileSystem.FreeFile())
'    End Sub

'    ' Not tested:
'    '   public static FileAttribute GetAttr(string PathName) { throw null; }

'    <Fact>
'    Public Sub Input_Write()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      Dim dateTime1 As New DateTime(2019, 1, 1)

'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output)
'      FileSystem.Write(fileNumber, True)
'      FileSystem.Write(fileNumber, CByte(1), ChrW(2), CDec(3), 4.0, 5.0F)
'      FileSystem.Write(fileNumber, 6, 7L)
'      FileSystem.Write(fileNumber, CShort(Fix(8)), "ABC", dateTime1)
'      FileSystem.FileClose(fileNumber)

'      Dim _bool As Boolean = CType(Nothing, Boolean)
'      Dim _byte As Byte = CType(Nothing, Byte)
'      Dim _char As Char = CType(Nothing, Char)
'      Dim _decimal As Decimal = CType(Nothing, Decimal)
'      Dim _double As Double = CType(Nothing, Double)
'      Dim _float As Single = CType(Nothing, Single)
'      Dim _int As Integer = CType(Nothing, Integer)
'      Dim _long As Long = CType(Nothing, Long)
'      Dim _short As Short = CType(Nothing, Short)
'      Dim _string As String = CType(Nothing, String)
'      Dim _dateTime As DateTime = CType(Nothing, DateTime)
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input)
'      FileSystem.Input(fileNumber, _bool)
'      FileSystem.Input(fileNumber, _byte)
'      FileSystem.Input(fileNumber, _char)
'      FileSystem.Input(fileNumber, _decimal)
'      FileSystem.Input(fileNumber, _double)
'      FileSystem.Input(fileNumber, _float)
'      FileSystem.Input(fileNumber, _int)
'      FileSystem.Input(fileNumber, _long)
'      FileSystem.Input(fileNumber, _short)
'      FileSystem.Input(fileNumber, _string)
'      FileSystem.Input(fileNumber, _dateTime)
'      Assert.[True](FileSystem.EOF(fileNumber))
'      FileSystem.FileClose(fileNumber)

'      Assert.[True](_bool)
'      Assert.Equal(CByte(1), _byte)
'      Assert.Equal(ChrW(2), _char)
'      Assert.Equal(CDec(3), _decimal)
'      Assert.Equal(4.0, _double)
'      Assert.Equal(5.0F, _float)
'      Assert.Equal(6, _int)
'      Assert.Equal(7L, _long)
'      Assert.Equal(CShort(Fix(8)), _short)
'      Assert.Equal("ABC", _string)
'      Assert.Equal(dateTime1, _dateTime)
'    End Sub

'    '<ActiveIssue("https://github.com/dotnet/runtime/issues/34362", TestPlatforms.Windows, TargetFrameworkMonikers.Netcoreapp, TestRuntimes.Mono)>
'    <Fact>
'    Public Sub Input_Object_Write()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      Dim dateTime1 As New DateTime(2019, 1, 1)

'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output)
'      FileSystem.Write(fileNumber, DBNull.Value)
'      FileSystem.Write(fileNumber, True)
'      FileSystem.Write(fileNumber, CByte(1), ChrW(2), CDec(3), 4.0, 5.0F)
'      FileSystem.Write(fileNumber, 6, 7L)
'      FileSystem.Write(fileNumber, CShort(Fix(8)), "ABC", dateTime1)
'      FileSystem.FileClose(fileNumber)

'      Dim _dbnull As Object = Nothing
'      Dim _bool As Object = Nothing
'      Dim _byte As Object = Nothing
'      Dim _char As Object = Nothing
'      Dim _decimal As Object = Nothing
'      Dim _double As Object = Nothing
'      Dim _float As Object = Nothing
'      Dim _int As Object = Nothing
'      Dim _long As Object = Nothing
'      Dim _short As Object = Nothing
'      Dim _string As Object = Nothing
'      Dim _dateTime As Object = Nothing
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Input)
'      Try
'        FileSystem.Input(fileNumber, _dbnull)
'        FileSystem.Input(fileNumber, _bool)
'        FileSystem.Input(fileNumber, _byte)
'        FileSystem.Input(fileNumber, _char)
'        FileSystem.Input(fileNumber, _decimal)
'        FileSystem.Input(fileNumber, _double)
'        FileSystem.Input(fileNumber, _float)
'        FileSystem.Input(fileNumber, _int)
'        FileSystem.Input(fileNumber, _long)
'        FileSystem.Input(fileNumber, _short)
'        FileSystem.Input(fileNumber, _string)
'        FileSystem.Input(fileNumber, _dateTime)
'        Assert.[True](FileSystem.EOF(fileNumber))

'        Assert.Equal(DBNull.Value, _dbnull)
'        Assert.Equal(CObj(True), _bool)
'        Assert.Equal(CShort(Fix(1)), _byte)
'        Assert.Equal(ChrW(2), _char)
'        Assert.Equal(CShort(Fix(3)), _decimal)
'        Assert.Equal(CShort(Fix(4)), _double)
'        Assert.Equal(CShort(Fix(5)), _float)
'        Assert.Equal(CShort(Fix(6)), _int)
'        Assert.Equal(CShort(Fix(7)), _long)
'        Assert.Equal(CShort(Fix(8)), _short)
'        Assert.Equal("ABC", _string)
'        Assert.Equal(dateTime1, _dateTime)
'      Catch __unusedPlatformNotSupportedException1__ As PlatformNotSupportedException

'      End Try
'      FileSystem.FileClose(fileNumber)
'    End Sub

'    ' Not tested:
'    '   public static string InputString(int FileNumber, int CharCount) { throw null; }

'    <Fact>
'    Public Sub Kill()
'      Dim fileName = GetTestFilePath()
'      IO.File.WriteAllText(fileName, "abc123")
'      Assert.[True](IO.File.Exists(fileName))
'      FileSystem.Kill(fileName)
'      Assert.[False](IO.File.Exists(fileName))

'      ' Missing file.
'      fileName = GetTestFilePath()
'      AssertThrows(Of IO.FileNotFoundException)(Sub() FileSystem.Kill(fileName))
'      Assert.[False](IO.File.Exists(fileName))
'    End Sub

'    ' Not tested:
'    '   public static string LineInput(int FileNumber) { throw null; }
'    '   public static long Loc(int FileNumber) { throw null; }

'    ' Lock is supported on Windows only currently.
'    <ConditionalFact(GetType(RemoteExecutor), NameOf(RemoteExecutor.IsSupported))>
'    <PlatformSpecific(TestPlatforms.Windows)>
'    Public Sub Lock_Unlock()
'      ' TODO Check: Local function was replaced with Lambda
'      Dim remoteWrite As Action(Of String, String) = Sub(fileName As String, text As String)
'                                                       RemoteExecutor.Invoke(Sub(fileName, text)
'                                                                               Using stream As System.IO.FileStream = IO.File.Open(fileName, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite)
'                                                                                 Try
'                                                                                   Using writer As System.IO.StreamWriter = New IO.StreamWriter(stream)
'                                                                                     writer.Write(text)
'                                                                                   End Using
'                                                                                 Catch __unusedIOException1__ As IO.IOException

'                                                                                 End Try
'                                                                               End Using
'                                                                             End Sub,
'                                                                             fileName,
'                                                                             text,
'                                                                             New RemoteInvokeOptions() With {.ExpectedExitCode = 0}).Dispose()
'                                                     End Sub
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output, Share:=OpenShare.[Shared])
'      remoteWrite(fileName, "abc")
'      Try
'        FileSystem.Lock(fileNumber)
'        remoteWrite(fileName, "123")
'      Finally
'        FileSystem.Unlock(fileNumber)
'      End Try
'      remoteWrite(fileName, "456")
'      FileSystem.FileClose(fileNumber)
'      Assert.Equal("abc456", IO.File.ReadAllText(fileName))

'    End Sub

'    ' Not tested:
'    '   public static void Lock(int FileNumber, long Record) { }
'    '   public static void Lock(int FileNumber, long FromRecord, long ToRecord) { }
'    '   public static long LOF(int FileNumber) { throw null; }
'    '   public static void Unlock(int FileNumber, long Record) { }
'    '   public static void Unlock(int FileNumber, long FromRecord, long ToRecord) { }

'    <Fact>
'    Public Sub MkDir_RmDir()
'      Dim dirName = GetTestFilePath()
'      Assert.[False](IO.Directory.Exists(dirName))

'      FileSystem.MkDir(dirName)
'      Assert.[True](IO.Directory.Exists(dirName))

'      ' Create the directory a second time.
'      AssertThrows(Of IO.IOException)(Sub() FileSystem.MkDir(dirName))

'      FileSystem.RmDir(dirName)
'      Assert.[False](IO.Directory.Exists(dirName))

'      ' Remove the directory a second time.
'      AssertThrows(Of IO.DirectoryNotFoundException)(Sub() FileSystem.RmDir(dirName))
'    End Sub

'    ' Not tested:
'    '   public static void Print(int FileNumber, params object[] Output) { }
'    '   public static void PrintLine(int FileNumber, params object[] Output) { }

'    <Fact>
'    Public Sub Rename()
'      ' Rename to an unused name.
'      Dim sourceName = GetTestFilePath()
'      IO.File.WriteAllText(sourceName, "abc")
'      Dim destName = GetTestFilePath()
'      If PlatformDetection.IsWindows Then
'        FileSystem.Rename(sourceName, destName)
'        Assert.[False](IO.File.Exists(sourceName))
'        Assert.[True](IO.File.Exists(destName))
'        Assert.Equal("abc", IO.File.ReadAllText(destName))
'      Else
'        AssertThrows(Of PlatformNotSupportedException)(Sub() FileSystem.Rename(sourceName, destName))
'        Assert.[True](IO.File.Exists(sourceName))
'        Assert.[False](IO.File.Exists(destName))
'        Assert.Equal("abc", IO.File.ReadAllText(sourceName))
'      End If

'      ' Rename to an existing name.
'      sourceName = GetTestFilePath()
'      IO.File.WriteAllText(sourceName, "def")
'      destName = GetTestFilePath()
'      IO.File.WriteAllText(destName, "123")
'      If PlatformDetection.IsWindows Then
'        AssertThrows(Of IO.IOException)(Sub() FileSystem.Rename(sourceName, destName))
'      Else
'        AssertThrows(Of PlatformNotSupportedException)(Sub() FileSystem.Rename(sourceName, destName))
'      End If
'      Assert.[True](IO.File.Exists(sourceName))
'      Assert.[True](IO.File.Exists(destName))
'      Assert.Equal("def", IO.File.ReadAllText(sourceName))
'      Assert.Equal("123", IO.File.ReadAllText(destName))
'    End Sub

'    ' Not tested:
'    '   public static void Reset() { }
'    '   public static void Seek(int FileNumber, long Position) { }
'    '   public static long Seek(int FileNumber) { throw null; }
'    '   public static void SetAttr(string PathName, FileAttribute Attributes) { }
'    '   public static SpcInfo SPC(short Count) { throw null; }
'    '   public static TabInfo TAB() { throw null; }
'    '   public static TabInfo TAB(short Column) { throw null; }
'    '   public static void WriteLine(int FileNumber, params object[] Output) { }

'    <Fact>
'    Public Sub Write_ArgumentException()
'      Dim fileNumber As Integer = FileSystem.FreeFile()
'      Dim fileName = GetTestFilePath()
'      FileSystem.FileOpen(fileNumber, fileName, OpenMode.Output)
'      AssertThrows(Of ArgumentException)(Sub() FileSystem.Write(fileNumber, New Object))
'      FileSystem.FileClose(fileNumber)
'    End Sub

'    ' We cannot use XUnit.Assert.Throws<T>() for lambdas that rely on AssemblyData (such as
'    ' file numbers) because AssemblyData instances are associated with the calling assembly, and
'    ' in RELEASE builds, the calling assembly of a lambda invoked from XUnit.Assert.Throws<T>()
'    ' is corelib rather than this assembly, so file numbers created outside the lambda will be invalid.
'    Private Shared Sub AssertThrows(Of TException As Exception)(action1 As Action)
'      Dim ex As TException = Nothing
'      Try
'        Action()
'      Catch e As TException
'        ex = e
'      End Try
'      If ex Is Nothing Then
'        Throw New ThrowsException(GetType(TException))
'      End If

'      If ex.[GetType]() IsNot GetType(TException) Then
'        Throw New ThrowsException(GetType(TException), ex)
'      End If
'    End Sub

'  End Class

'End Namespace