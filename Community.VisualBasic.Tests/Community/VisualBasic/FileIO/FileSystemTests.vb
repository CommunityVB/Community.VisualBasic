' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports Xunit

Namespace Global.Community.VisualBasic.FileIO.Tests

  Public Class FileSystemTests
    Inherits IOx.FileCleanupTestBase

    Private Shared ReadOnly DestData As String = "xXy"
    Private Shared ReadOnly SourceData As String = "aAb"

#Disable Warning IDE0051 ' Remove unused private members
    Private Shared ReadOnly Property ManualTestsEnabled As Boolean
      Get
        Return Not String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MANUAL_TESTS"))
      End Get
    End Property
#Enable Warning IDE0051 ' Remove unused private members

    Private Shared Function HasExpectedData(FileNameWithPath As String, ExpectedData As String) As Boolean
      Dim actualData As String = IO.File.ReadAllText(FileNameWithPath)
      Return ExpectedData = actualData
    End Function

    Private Shared Sub WriteFile(FileName As String, TestData As String)
      IO.File.WriteAllText(FileName, TestData)
    End Sub

    <Fact>
    Public Sub CombinePathTest_BadBaseDirectory_RelativePath()
      Assert.Throws(Of ArgumentNullException)(Function() FileIO.FileSystem.CombinePath(Nothing, "Test2"))
      Assert.Throws(Of ArgumentNullException)(Function() FileIO.FileSystem.CombinePath("", "Test2"))
    End Sub

    <Fact>
    Public Sub CombinePathTest_BaseDirectory_RelativePath()
      Dim TestDirInfo As System.IO.DirectoryInfo = New IO.DirectoryInfo(TestDirectory)
      Dim Root As String = TestDirInfo.Root.Name
      Assert.Equal(FileIO.FileSystem.CombinePath(Root, "Test2"), IO.Path.Combine(Root, "Test2"))
    End Sub

    <Fact>
    Public Sub CombinePathTest_RootDirectory_RelativePath()
      Assert.Equal(FileIO.FileSystem.CombinePath(TestDirectory, Nothing), TestDirectory)
      Assert.Equal(FileIO.FileSystem.CombinePath(TestDirectory, ""), TestDirectory)
      Assert.Equal(FileIO.FileSystem.CombinePath(TestDirectory, "Test"), IO.Path.Combine(TestDirectory, "Test"))
    End Sub

    <Fact>
    Public Sub CopyDirectory_SourceDirectoryName_DestinationDirectoryName()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory)
      Assert.Equal(IO.Directory.GetFiles(FullPathToSourceDirectory).Length, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure copy transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
      IO.Directory.Delete(FullPathToTargetDirectory, recursive:=True)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      CreateTestFile(TestData:=SourceData, PathFromBase:="TargetDirectory", TestFileName:=$"NewFile0")
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory))
    End Sub

    <Fact>
    Public Sub CopyDirectory_SourceDirectoryName_DestinationDirectoryName_OverwriteFalse()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=False)
      Assert.Equal(IO.Directory.GetFiles(FullPathToSourceDirectory).Length, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure copy transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
      IO.Directory.Delete(FullPathToTargetDirectory, recursive:=True)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      CreateTestFile(DestData, PathFromBase:="TargetDirectory", TestFileName:=$"NewFile0")
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=False))
      Assert.Equal(IO.Directory.GetFiles(FullPathToTargetDirectory).Length, IO.Directory.GetFiles(FullPathToSourceDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        Assert.[True](HasExpectedData(CurrentFile, If(CurrentFile.EndsWith("0"), DestData, SourceData)))
      Next
    End Sub

    <Fact>
    Public Sub CopyDirectory_SourceDirectoryName_DestinationDirectoryName_OverwriteTrue()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=True)
      Assert.Equal(IO.Directory.GetFiles(FullPathToSourceDirectory).Length, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure copy transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
    End Sub

#If TARGET_WINDOWS Then

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub CopyDirectory_SourceDirectoryName_DestinationDirectoryName_SkipFile()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"Select_Skip_this_file{i}")
      Next
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      CreateTestFile(DestData, PathFromBase:="TargetDirectory", TestFileName:=$"Select_Skip_this_file0")
      FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, UIOption.AllDialogs, onUserCancel:=UICancelOption.ThrowException)
      Assert.Equal(IO.Directory.GetFiles(FullPathToTargetDirectory).Length, IO.Directory.GetFiles(FullPathToSourceDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        Assert.[True](HasExpectedData(CurrentFile, If(CurrentFile.EndsWith("0"), DestData, SourceData)))
      Next
    End Sub

#End If

    '<PlatformSpecific(TestPlatforms.AnyUnix)>
    <Fact>
    Public Sub CopyDirectory_SourceDirectoryName_DestinationDirectoryName_UIOptionUnix()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
        CreateTestFile(DestData, PathFromBase:="TargetDirectory", TestFileName:=$"NewFile{i}")
      Next
      Assert.Throws(Of PlatformNotSupportedException)(Sub() FileIO.FileSystem.CopyDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, UIOption.AllDialogs))
    End Sub

    <Fact>
    Public Sub CopyFile_FileSourceFileName_DestinationFileName()

      Dim testFileSource = GetTestFilePath()
      Dim testFileDest = GetTestFilePath()

      ' Write and copy file
      WriteFile(testFileSource, SourceData)
      WriteFile(testFileDest, DestData)
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.CopyFile(testFileSource, testFileDest))

      ' Ensure copy didn't overwrite existing data
      Assert.[True](HasExpectedData(testFileDest, DestData))

      ' Get a new destination name
      testFileDest = GetTestFilePath()
      FileIO.FileSystem.CopyFile(testFileSource, testFileDest)

      ' Ensure copy transferred written data
      Assert.[True](HasExpectedData(testFileDest, SourceData))

    End Sub

    <Fact>
    Public Sub CopyFile_FileSourceFileName_DestinationFileName_OverwriteFalse()

      Dim testFileSource = GetTestFilePath()
      Dim testFileDest = GetTestFilePath()

      ' Write and copy file
      WriteFile(testFileSource, SourceData)
      WriteFile(testFileDest, DestData)
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.CopyFile(testFileSource, testFileDest, overwrite:=False))

      ' Ensure copy didn't overwrite existing data
      Assert.[True](HasExpectedData(testFileDest, DestData))

    End Sub

    <Fact>
    Public Sub CopyFile_FileSourceFileName_DestinationFileName_OverwriteTrue()

      Dim testFileSource = GetTestFilePath()
      Dim testFileDest = GetTestFilePath()

      ' Write and copy file
      WriteFile(testFileSource, SourceData)
      WriteFile(testFileDest, DestData)
      FileIO.FileSystem.CopyFile(testFileSource, testFileDest, overwrite:=True)

      ' Ensure copy transferred written data
      Assert.[True](HasExpectedData(testFileDest, SourceData))

    End Sub

#If TARGET_WINDOWS Then

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub CopyFile_SourceFileName_DestinationFileName_UIOptionTestOverWriteFalse()

      Dim testFileSource As String = CreateTestFile(TestData:=SourceData, PathFromBase:=Nothing, TestFileName:="Select_Skip_this_file")
      Dim testFileDest = GetTestFilePath()

      ' Write and copy file
      WriteFile(testFileSource, SourceData)
      WriteFile(testFileDest, DestData)
      FileIO.FileSystem.CopyFile(testFileSource, testFileDest, showUI:=UIOption.AllDialogs, onUserCancel:=UICancelOption.DoNothing)

      ' Ensure copy transferred written data
      Assert.[True](HasExpectedData(testFileDest, DestData))

    End Sub

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub CopyFile_SourceFileName_DestinationFileName_UIOptionTestOverWriteTrue()

      Dim testFileSource As String = CreateTestFile(TestData:=SourceData, PathFromBase:=Nothing, TestFileName:="Select_Replace_the_file")
      Dim testFileDest = GetTestFilePath()

      ' Write and copy file
      WriteFile(testFileSource, SourceData)
      WriteFile(testFileDest, DestData)

      FileIO.FileSystem.CopyFile(testFileSource, testFileDest, showUI:=UIOption.AllDialogs, onUserCancel:=UICancelOption.DoNothing)

      ' Ensure copy transferred written data
      Assert.[True](HasExpectedData(testFileDest, SourceData))

    End Sub

#End If

    <Fact>
    Public Sub CreateDirectory_Directory()
      Dim FullPathToNewDirectory As String = IO.Path.Combine(TestDirectory, "NewDirectory")
      Assert.[False](IO.Directory.Exists(FullPathToNewDirectory))
      FileIO.FileSystem.CreateDirectory(FullPathToNewDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
    End Sub

    <Fact>
    Public Sub CreateDirectory_LongPath()

      Dim PathLength = TestDirectory.Length
      Assert.[True](PathLength < 257) ' Need room for path separator and new directory name
      Dim DirectoryName As New String("B"c, 30)

      Assert.[True](DirectoryName.Length < 248, $"DirectoryName.Length at {DirectoryName.Length} is not < 248")
      Assert.[True](IO.Directory.Exists(TestDirectory))

      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, DirectoryName)
      Assert.[True](FullPathToTargetDirectory.Length < 260, $"FullPathToTargetDirectory.Length at {FullPathToTargetDirectory.Length} is not < 260")

      FileIO.FileSystem.CreateDirectory(FullPathToTargetDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToTargetDirectory))

      Try
        Dim VeryLongFullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, New [String]("E"c, 239))
        FileIO.FileSystem.CreateDirectory(VeryLongFullPathToTargetDirectory)
        Assert.[True](IO.Directory.Exists(VeryLongFullPathToTargetDirectory), $"Directory {VeryLongFullPathToTargetDirectory} does not exist")
      Catch __unusedPathTooLongException1__ As IO.PathTooLongException
        Assert.[True](RuntimeInformation.IsOSPlatform(OSPlatform.Windows), "Unexpected Failure on non-Windows Platform")
      Catch __unusedDirectoryNotFoundException2__ As IO.DirectoryNotFoundException
        Assert.[True](RuntimeInformation.IsOSPlatform(OSPlatform.Windows), "Unexpected Failure on non-Windows Platform")
        Assert.Equal(8, IntPtr.Size)
      End Try

    End Sub

    ' Can't get current directory on OSX before setting it.
    '<ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNotOSX))>
    <Fact>
    Public Sub CurrentDirectoryGet()
      Dim CurrentDirectory As String = IO.Directory.GetCurrentDirectory()
      Assert.Equal(FileIO.FileSystem.CurrentDirectory, CurrentDirectory)
    End Sub

    ' On OSX, the temp directory /tmp/ is a symlink to /private/tmp, so setting the current
    ' directory to a symlinked path will result in GetCurrentDirectory returning the absolute
    ' path that followed the symlink.

    '<ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNotOSX))>
    <Fact>
    Public Sub CurrentDirectorySet()
      Dim SavedCurrentDirectory As String = IO.Directory.GetCurrentDirectory()
      FileIO.FileSystem.CurrentDirectory = TestDirectory
      Assert.Equal(TestDirectory, FileIO.FileSystem.CurrentDirectory)
      FileIO.FileSystem.CurrentDirectory = SavedCurrentDirectory
      Assert.Equal(FileIO.FileSystem.CurrentDirectory, SavedCurrentDirectory)
    End Sub

    <Fact>
    Public Sub DeleteDirectory_Directory_DeleteAllContents()
      Dim FullPathToNewDirectory As String = IO.Path.Combine(TestDirectory, "NewDirectory")
      IO.Directory.CreateDirectory(FullPathToNewDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Dim testFileSource As String = CreateTestFile(SourceData, PathFromBase:="NewDirectory", TestFileName:="TestFile")
      Assert.[True](IO.File.Exists(testFileSource))
      FileIO.FileSystem.DeleteDirectory(FullPathToNewDirectory, DeleteDirectoryOption.DeleteAllContents)
      Assert.[False](IO.Directory.Exists(FullPathToNewDirectory))
    End Sub

    <Fact>
    Public Sub DeleteDirectory_Directory_ThrowIfDirectoryNonEmpty()

      Dim FullPathToNewDirectory As String = IO.Path.Combine(TestDirectory, "NewDirectory")
      FileIO.FileSystem.CreateDirectory(FullPathToNewDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Dim testFileSource As String = CreateTestFile(SourceData, PathFromBase:="NewDirectory", TestFileName:="TestFile")

      Assert.[True](IO.File.Exists(testFileSource))
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.DeleteDirectory(FullPathToNewDirectory, DeleteDirectoryOption.ThrowIfDirectoryNonEmpty))
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Assert.[True](IO.File.Exists(testFileSource))

    End Sub

#If TARGET_WINDOWS Then

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub DeleteDirectory_Directory_UIOption_Delete()

      Dim FullPathToNewDirectory As String = IO.Path.Combine(TestDirectory, "Select_Yes")
      FileIO.FileSystem.CreateDirectory(FullPathToNewDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Dim testFileSource As String = CreateTestFile(SourceData, PathFromBase:="Select_Yes", TestFileName:="DoNotCare")

      Assert.[True](IO.File.Exists(testFileSource))
      FileIO.FileSystem.DeleteDirectory(FullPathToNewDirectory, showUI:=UIOption.AllDialogs, recycle:=RecycleOption.DeletePermanently, onUserCancel:=UICancelOption.ThrowException)
      Assert.[False](IO.Directory.Exists(FullPathToNewDirectory))
      Assert.[False](IO.File.Exists(testFileSource))

    End Sub

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub DeleteDirectory_Directory_UIOption_DoNotDelete()

      Dim FullPathToNewDirectory As String = IO.Path.Combine(TestDirectory, "Select_No")
      FileIO.FileSystem.CreateDirectory(FullPathToNewDirectory)
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Dim testFileSource As String = CreateTestFile(SourceData, PathFromBase:="Select_No", TestFileName:="DoNotCare")

      Assert.[True](IO.File.Exists(testFileSource))
      Assert.Throws(Of OperationCanceledException)(Sub() FileIO.FileSystem.DeleteDirectory(FullPathToNewDirectory, showUI:=UIOption.AllDialogs, recycle:=RecycleOption.DeletePermanently, onUserCancel:=UICancelOption.ThrowException))
      Assert.[True](IO.Directory.Exists(FullPathToNewDirectory))
      Assert.[True](IO.File.Exists(testFileSource))

    End Sub

#End If

    <Fact>
    Public Sub DeleteFile_File()

      Dim testFileSource As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())

      Assert.[True](IO.File.Exists(testFileSource))
      FileIO.FileSystem.DeleteFile(testFileSource)
      Assert.[False](IO.File.Exists(testFileSource))

    End Sub

    <Fact>
    Public Sub DirectoryExists_Directory()
      Assert.[True](FileIO.FileSystem.DirectoryExists(TestDirectory))
      Assert.[False](FileIO.FileSystem.DirectoryExists(IO.Path.Combine(TestDirectory, "NewDirectory")))
    End Sub

    ' Not tested:
    '   public System.Collections.ObjectModel.ReadOnlyCollection<System.IO.DriveInfo> Drives { get { throw null; } }

    <Fact>
    Public Sub FileExists_File()
      Dim testFileSource As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Assert.[True](FileIO.FileSystem.FileExists(testFileSource))
      FileIO.FileSystem.FileExists(testFileSource)
      IO.File.Delete(testFileSource)
      Assert.[False](FileIO.FileSystem.FileExists(testFileSource))
    End Sub

    ' Not tested:
    '   public System.Collections.ObjectModel.ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, FileIO.SearchOption searchType) { throw null; }
    '   public System.Collections.ObjectModel.ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, FileIO.SearchOption searchType, params string[] fileWildcards) { throw null; }

    <Fact>
    Public Sub GetDirectories_Directory()
      Dim DirectoryList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetDirectories(TestDirectory)
      Assert.Equal(0, DirectoryList.Count)
      For i As Integer = 0 To 6 - 1
        IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"GetDirectories_DirectoryNewSubDirectory{i}"))
      Next
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory)
      Assert.Equal(6, DirectoryList.Count)
      For i As Integer = 0 To 6 - 1
        Assert.Contains(IO.Path.Combine(TestDirectory, $"GetDirectories_DirectoryNewSubDirectory{i}"), DirectoryList)
      Next
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"GetDirectories_DirectoryNewSubDirectory0", $"NewSubSubDirectory"))
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory)
      Assert.Equal(6, DirectoryList.Count)
    End Sub

    <Fact>
    Public Sub GetDirectories_Directory_SearchOption()
      Dim DirectoryList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly)
      Assert.Equal(0, DirectoryList.Count)
      For i As Integer = 0 To 6 - 1
        IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"GetDirectories_Directory_SearchOptionNewSubDirectory{i}"))
      Next
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly)
      Assert.Equal(6, DirectoryList.Count)
      For i As Integer = 0 To 6 - 1
        Assert.Contains(IO.Path.Combine(TestDirectory, $"GetDirectories_Directory_SearchOptionNewSubDirectory{i}"), DirectoryList)
      Next
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"GetDirectories_Directory_SearchOptionNewSubDirectory0", $"NewSubSubDirectory"))
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly)
      Assert.Equal(6, DirectoryList.Count)
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchAllSubDirectories)
      Assert.Equal(7, DirectoryList.Count)
    End Sub

    <Fact>
    Public Sub GetDirectories_Directory_SearchOption_Wildcards()
      Dim DirectoryList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly, "*")
      Assert.Equal(0, DirectoryList.Count)
      Dim CreatedDirectories As Collections.Generic.List(Of String) = New List(Of String)
      For i As Integer = 0 To 6 - 1
        CreatedDirectories.Add(IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"NewSubDirectory00{i}")).Name)
      Next
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly, "*000", "*001")
      Assert.Equal(2, DirectoryList.Count)
      For i As Integer = 0 To 2 - 1
        Dim DirectoryName As String = IO.Path.Combine(TestDirectory, $"NewSubDirectory00{i}")
        Assert.[True](DirectoryList.Contains(DirectoryName), $"{DirectoryName} Is missing from Wildcard Search")
      Next
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"NewSubDirectory000", $"NewSubSubDirectory000"))
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchTopLevelOnly, "*000")
      Assert.Equal(1, DirectoryList.Count)
      DirectoryList = FileIO.FileSystem.GetDirectories(TestDirectory, SearchOption.SearchAllSubDirectories, "*000")
      Assert.Equal(2, DirectoryList.Count)
    End Sub

    <Fact>
    Public Sub GetDirectoryInfo_Directory()
      For i As Integer = 0 To 6 - 1
        IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"NewSubDirectory{i}"))
      Next
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, $"NewSubDirectory0", $"NewSubSubDirectory"))
      Dim info As System.IO.DirectoryInfo = FileIO.FileSystem.GetDirectoryInfo(TestDirectory)
      Dim infoFromIO As System.IO.DirectoryInfo = New IO.DirectoryInfo(TestDirectory)
      Assert.Equal(info.CreationTime, infoFromIO.CreationTime)
      Assert.Equal(info.Extension, infoFromIO.Extension)
      Assert.Equal(info.FullName, TestDirectory)
      Assert.Equal(info.LastAccessTime, infoFromIO.LastAccessTime)
      Assert.Equal(info.Name, infoFromIO.Name)
      Assert.Equal(info.Parent.ToString(), infoFromIO.Parent.ToString())
      Assert.Equal(info.Root.Name, infoFromIO.Root.Name)
    End Sub

    <Fact>
    Public Sub GetDriveInfo_Drive()
      Dim Drives As System.IO.DriveInfo() = IO.DriveInfo.GetDrives()
      Assert.[True](Drives.Length > 0)
      Assert.Equal(FileIO.FileSystem.GetDriveInfo(Drives(0).Name).Name, New IO.DriveInfo(Drives(0).Name).Name)
    End Sub

    <Fact>
    Public Sub GetFileInfo_File()

      Dim TestFile As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())

      Dim FileInfoFromSystemIO As System.IO.FileInfo = New IO.FileInfo(TestFile)
      Assert.NotNull(FileInfoFromSystemIO)

      Dim info As System.IO.FileInfo = FileIO.FileSystem.GetFileInfo(TestFile)
      Assert.NotNull(info)
      Assert.[True](info.Exists)
      Assert.Equal(info.Attributes, FileInfoFromSystemIO.Attributes)
      Assert.Equal(info.CreationTime, FileInfoFromSystemIO.CreationTime)
      Assert.[True](info.CreationTime > DateTime.MinValue)
      Assert.Equal(info.DirectoryName, FileInfoFromSystemIO.DirectoryName)
      Assert.Equal(info.Extension, FileInfoFromSystemIO.Extension)
      Assert.Equal(info.FullName, FileInfoFromSystemIO.FullName)
      Assert.Equal(info.IsReadOnly, FileInfoFromSystemIO.IsReadOnly)
      Assert.Equal(info.LastAccessTime, FileInfoFromSystemIO.LastAccessTime)
      Assert.Equal(info.LastWriteTime, FileInfoFromSystemIO.LastWriteTime)
      Assert.Equal(info.Length, FileInfoFromSystemIO.Length)
      Assert.Equal(info.Name, FileInfoFromSystemIO.Name)

    End Sub

    <Fact>
    Public Sub GetFiles_Directory()
      Dim FileList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetFiles(TestDirectory)
      Assert.Equal(0, FileList.Count)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:=Nothing, TestFileName:=$"NewFile{i}")
      Next
      FileList = FileIO.FileSystem.GetFiles(TestDirectory)
      Assert.Equal(6, FileList.Count)
      For i As Integer = 0 To 6 - 1
        Assert.Contains(IO.Path.Combine(TestDirectory, $"NewFile{i}"), FileList)
      Next
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, "GetFiles_DirectoryNewSubDirectory"))
      CreateTestFile(SourceData, PathFromBase:="GetFiles_DirectoryNewSubDirectory", TestFileName:="NewFile")
      FileList = FileIO.FileSystem.GetFiles(TestDirectory)
      Assert.Equal(6, FileList.Count)
    End Sub

    <Fact>
    Public Sub GetFiles_Directory_SearchOption()
      Dim NewSubDirectoryPath As String = IO.Path.Combine(TestDirectory, "GetFiles_Directory_SearchOptionNewSubDirectory")
      IO.Directory.CreateDirectory(NewSubDirectoryPath)
      CreateTestFile(SourceData, PathFromBase:="GetFiles_Directory_SearchOptionNewSubDirectory", TestFileName:="NewFile")
      Dim FileList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetFiles(TestDirectory)
      Assert.Equal(0, FileList.Count)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:=Nothing, TestFileName:=$"NewFile{i}")
      Next
      FileList = FileIO.FileSystem.GetFiles(TestDirectory, SearchOption.SearchTopLevelOnly)
      CreateTestFile(SourceData, PathFromBase:=Nothing, TestFileName:="NewFile")
      Assert.Equal(6, FileList.Count)
      For i As Integer = 0 To 6 - 1
        Assert.Contains(IO.Path.Combine(TestDirectory, $"NewFile{i}"), FileList)
      Next
      FileList = FileIO.FileSystem.GetFiles(TestDirectory, SearchOption.SearchAllSubDirectories)
      Assert.Equal(8, FileList.Count)
      For i As Integer = 0 To 7 - 1
        Assert.[True](IO.File.Exists(FileList(i)))
      Next
    End Sub

    <Fact>
    Public Sub GetFiles_Directory_SearchOption_Wildcards()
      Dim FileList As Collections.ObjectModel.ReadOnlyCollection(Of String) = FileIO.FileSystem.GetFiles(TestDirectory)
      Assert.Equal(0, FileList.Count)
      Dim TestFileList As Collections.Generic.List(Of String) = New List(Of String)
      For i As Integer = 0 To 6 - 1
        TestFileList.Add(CreateTestFile(SourceData, PathFromBase:=Nothing, TestFileName:=$"NewFile{i}{(If(i Mod 2 = 0, ".vb", ".cs"))}"))
      Next
      FileList = FileIO.FileSystem.GetFiles(TestDirectory, SearchOption.SearchTopLevelOnly, "*.vb")
      Assert.Equal(3, FileList.Count)
      For i As Integer = 0 To 3 - 1
        Assert.Contains(FileList(i), TestFileList)
      Next
      Dim NewSubDirectoryPath As String = IO.Path.Combine(TestDirectory, "GetFiles_Directory_SearchOption_WildcardsNewSubDirectory")
      IO.Directory.CreateDirectory(NewSubDirectoryPath)
      TestFileList.Add(CreateTestFile(SourceData, PathFromBase:="GetFiles_Directory_SearchOption_WildcardsNewSubDirectory", TestFileName:="NewFile.cs"))
      FileList = FileIO.FileSystem.GetFiles(TestDirectory, SearchOption.SearchAllSubDirectories, "*.cs")
      Assert.[True](FileList.Contains(TestFileList(TestFileList.Count - 1)), "File in Subdirectory not found")
      Assert.Equal(4, FileList.Count)
    End Sub

    <Fact>
    Public Sub GetName_Path()
      Assert.Equal(FileIO.FileSystem.GetName(TestDirectory), IO.Path.GetFileName(TestDirectory))
    End Sub

    <Fact>
    Public Sub GetParentPath_Path()
      Assert.Equal(FileIO.FileSystem.GetParentPath(TestDirectory), IO.Path.GetDirectoryName(TestDirectory))
    End Sub

    <Fact>
    Public Sub GetTempFileName()
      Dim TempFile As String = FileIO.FileSystem.GetTempFileName()
      Assert.[True](IO.File.Exists(TempFile))
      ' TODO: Check, VB does not directly support MemberAccess off a Conditional If Expression
      Dim tempVar = New IO.FileInfo(TempFile)
      Assert.Equal(0, tempVar.Length)
      IO.File.Delete(TempFile)
    End Sub

#If TARGET_WINDOWS Then

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub MoveDirectory_Source_DirectoryName_DestinationDirectoryName_UIOptionOverwriteFalse()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"Select_Skip_this_file{i}")
      Next
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      Dim NewFile0WithPath As String = CreateTestFile(DestData, PathFromBase:="TargetDirectory", TestFileName:="Select_Skip_this_file0")
      FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, showUI:=UIOption.AllDialogs, onUserCancel:=UICancelOption.ThrowException)
      Dim RemainingSourceFilesWithPath As String() = IO.Directory.GetFiles(FullPathToSourceDirectory)
      ' We couldn't move one file
      Assert.Equal(1, RemainingSourceFilesWithPath.Length)
      ' Ensure the file left has correct data
      Assert.[True](HasExpectedData(RemainingSourceFilesWithPath(0), SourceData))

      Dim DestinationFilesWithPath As String() = IO.Directory.GetFiles(FullPathToTargetDirectory)
      Assert.Equal(6, DestinationFilesWithPath.Length)
      For Each CurrentFile As String In DestinationFilesWithPath
        Assert.[True](HasExpectedData(CurrentFile, If(CurrentFile.EndsWith("0"), DestData, SourceData)))
      Next
    End Sub

#End If

    <Fact>
    Public Sub MoveDirectory_SourceDirectoryName_DestinationDirectoryName()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory)
      Assert.Equal(6, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      Assert.[False](IO.Directory.Exists(FullPathToSourceDirectory))
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure move transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
      IO.Directory.Move(FullPathToTargetDirectory, FullPathToSourceDirectory)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      CreateTestFile(SourceData, PathFromBase:="TargetDirectory", TestFileName:="NewFile0")
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory))
    End Sub

    <Fact>
    Public Sub MoveDirectory_SourceDirectoryName_DestinationDirectoryName_OverwriteFalse()

      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=False)
      Assert.Equal(6, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      Assert.[False](IO.Directory.Exists(FullPathToSourceDirectory))
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure move transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
      IO.Directory.Move(FullPathToTargetDirectory, FullPathToSourceDirectory)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      Dim NewFile0WithPath As String = CreateTestFile(DestData, PathFromBase:="TargetDirectory", TestFileName:="NewFile0")
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=False))
      Dim RemainingSourceFilesWithPath As String() = IO.Directory.GetFiles(FullPathToSourceDirectory)
      ' We couldn't move one file
      Assert.Equal(1, RemainingSourceFilesWithPath.Length)
      ' Ensure the file left has correct data
      Assert.[True](HasExpectedData(RemainingSourceFilesWithPath(0), SourceData))

      Dim DestinationFilesWithPath As String() = IO.Directory.GetFiles(FullPathToTargetDirectory)
      Assert.Equal(6, DestinationFilesWithPath.Length)
      For Each CurrentFile As String In DestinationFilesWithPath
        Assert.[True](HasExpectedData(CurrentFile, If(CurrentFile.EndsWith("0"), DestData, SourceData)))
      Next

    End Sub

    <Fact>
    Public Sub MoveDirectory_SourceDirectoryName_DestinationDirectoryName_OverwriteTrue()
      Dim FullPathToSourceDirectory As String = IO.Path.Combine(TestDirectory, "SourceDirectory")
      Dim FullPathToTargetDirectory As String = IO.Path.Combine(TestDirectory, "TargetDirectory")
      IO.Directory.CreateDirectory(FullPathToSourceDirectory)
      IO.Directory.CreateDirectory(FullPathToTargetDirectory)
      For i As Integer = 0 To 6 - 1
        CreateTestFile(SourceData, PathFromBase:="SourceDirectory", TestFileName:=$"NewFile{i}")
      Next
      FileIO.FileSystem.MoveDirectory(FullPathToSourceDirectory, FullPathToTargetDirectory, overwrite:=True)
      Assert.[False](IO.Directory.Exists(FullPathToSourceDirectory))
      Assert.Equal(6, IO.Directory.GetFiles(FullPathToTargetDirectory).Length)
      For Each CurrentFile As String In IO.Directory.GetFiles(FullPathToTargetDirectory)
        ' Ensure copy transferred written data
        Assert.[True](HasExpectedData(CurrentFile, SourceData))
      Next
    End Sub

    <Fact>
    Public Sub MoveFile_SourceFileName_DestinationFileName()

      Dim SourceFileNameWithPath As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Dim DestinationFileNameWithPath As String = IO.Path.Combine(TestDirectory, "NewName")
      FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath)
      Assert.[False](IO.File.Exists(SourceFileNameWithPath))
      Assert.[True](IO.File.Exists(DestinationFileNameWithPath))
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, SourceData))

      SourceFileNameWithPath = DestinationFileNameWithPath
      DestinationFileNameWithPath = CreateTestFile(DestData, TestFileName:=GetTestFileName())
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath))
      ' Make sure we did not override existing file
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, DestData))
      Assert.[True](IO.File.Exists(SourceFileNameWithPath))

    End Sub

    <Fact>
    Public Sub MoveFile_SourceFileName_DestinationFileName_OverwriteFalse()
      Dim SourceFileNameWithPath As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Dim DestinationFileNameWithPath As String = IO.Path.Combine(TestDirectory, "NewName")
      FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath, overwrite:=False)
      Assert.[False](IO.File.Exists(SourceFileNameWithPath))
      Assert.[True](IO.File.Exists(DestinationFileNameWithPath))
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, SourceData))
      SourceFileNameWithPath = DestinationFileNameWithPath
      DestinationFileNameWithPath = CreateTestFile(DestData, TestFileName:=GetTestFileName())
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath, overwrite:=False))
      ' Make sure we did not override existing file
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, DestData))
      Assert.[True](IO.File.Exists(SourceFileNameWithPath))
    End Sub

    <Fact>
    Public Sub MoveFile_SourceFileName_DestinationFileName_OverwriteTrue()
      Dim SourceFileNameWithPath = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Dim DestinationFileNameWithPath = IO.Path.Combine(TestDirectory, "NewName")
      FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath, overwrite:=True)
      Assert.[False](IO.File.Exists(SourceFileNameWithPath))
      Assert.[True](IO.File.Exists(DestinationFileNameWithPath))
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, SourceData))
      'CreateTestFile(DestData, PathFromBase: null, TestFileName: (new System.IO.FileInfo(SourceFileNameWithPath)).Name);
      CreateTestFile(DestData, (New IO.FileInfo(SourceFileNameWithPath)).Name, Nothing)
      FileIO.FileSystem.MoveFile(sourceFileName:=DestinationFileNameWithPath, destinationFileName:=SourceFileNameWithPath, overwrite:=True)
      Assert.[True](IO.File.Exists(SourceFileNameWithPath))
      Assert.[False](IO.File.Exists(DestinationFileNameWithPath))
      Assert.[True](HasExpectedData(SourceFileNameWithPath, SourceData))
    End Sub

#If TARGET_WINDOWS Then

    '<ConditionalFact(NameOf(ManualTestsEnabled))>
    '<PlatformSpecific(TestPlatforms.Windows)>
    <Fact>
    Public Sub MoveFile_SourceFileName_DestinationFileName_UIOptionOverWriteFalse()
      Dim SourceFileNameWithPath As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Dim DestinationFileNameWithPath As String = IO.Path.Combine(TestDirectory, "Select_Skip_this_file")
      FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath, showUI:=UIOption.AllDialogs, onUserCancel:=UICancelOption.DoNothing)
      Assert.[False](IO.File.Exists(SourceFileNameWithPath))
      Assert.[True](IO.File.Exists(DestinationFileNameWithPath))
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, SourceData))
      SourceFileNameWithPath = DestinationFileNameWithPath
      DestinationFileNameWithPath = CreateTestFile(DestData, TestFileName:=GetTestFileName())
      FileIO.FileSystem.MoveFile(SourceFileNameWithPath, DestinationFileNameWithPath, showUI:=UIOption.AllDialogs, onUserCancel:=UICancelOption.ThrowException)
      ' Make sure we did not override existing file
      Assert.[True](HasExpectedData(DestinationFileNameWithPath, DestData))
      Assert.[True](IO.File.Exists(SourceFileNameWithPath))
    End Sub

#End If

    ' Not tested:
    '   public Microsoft.VisualBasic.FileIO.TextFieldParser OpenTextFieldParser(string file) { throw null; }
    '   public Microsoft.VisualBasic.FileIO.TextFieldParser OpenTextFieldParser(string file, params int[] fieldWidths) { throw null; }
    '   public Microsoft.VisualBasic.FileIO.TextFieldParser OpenTextFieldParser(string file, params string[] delimiters) { throw null; }
    '   public System.IO.StreamReader OpenTextFileReader(string file) { throw null; }
    '   public System.IO.StreamReader OpenTextFileReader(string file, System.Text.Encoding encoding) { throw null; }
    '   public System.IO.StreamWriter OpenTextFileWriter(string file, bool append) { throw null; }
    '   public System.IO.StreamWriter OpenTextFileWriter(string file, bool append, System.Text.Encoding encoding) { throw null; }
    '   public byte[] ReadAllBytes(string file) { throw null; }
    '   public string ReadAllText(string file) { throw null; }
    '   public string ReadAllText(string file, System.Text.Encoding encoding) { throw null; }

    <Fact>
    Public Sub RenameDirectory_Directory_NewName()
      ' <exception cref="IO.FileNotFoundException">If directory does not point to an existing directory.</exception>
      Assert.Throws(Of IO.DirectoryNotFoundException)(Sub() FileIO.FileSystem.RenameDirectory(IO.Path.Combine(TestDirectory, "DoesNotExistDirectory"), "NewDirectory"))
      Dim OrigDirectoryWithPath As String = IO.Path.Combine(TestDirectory, "OriginalDirectory")
      IO.Directory.CreateDirectory(OrigDirectoryWithPath)
      ' <exception cref="System.ArgumentException">If newName is null or Empty String.</exception>
      Assert.Throws(Of ArgumentNullException)(Sub() FileIO.FileSystem.RenameDirectory(OrigDirectoryWithPath, ""))
      Dim DirectoryNameWithPath As String = IO.Path.Combine(TestDirectory, "DoesNotExist")
      ' <exception cref="System.ArgumentException">If contains path information.</exception>
      Assert.Throws(Of ArgumentException)(Sub() FileIO.FileSystem.RenameDirectory(OrigDirectoryWithPath, DirectoryNameWithPath))
      FileIO.FileSystem.RenameDirectory(OrigDirectoryWithPath, "NewFDirectory")
      Dim NewFDirectoryPath As String = IO.Path.Combine(TestDirectory, "NewFDirectory")
      Assert.[True](IO.Directory.Exists(NewFDirectoryPath))
      Assert.[False](IO.Directory.Exists(OrigDirectoryWithPath))
      ' <exception cref="IO.IOException">If directory points to a root directory or if there's an existing directory or an existing file with the same name.</exception>
      IO.Directory.CreateDirectory(OrigDirectoryWithPath)
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.RenameDirectory(NewFDirectoryPath, "OriginalDirectory"))
    End Sub

    <Fact>
    Public Sub RenameFile_File_NewName()
      ' <exception cref="IO.FileNotFoundException">If file does not point to an existing file.</exception>
      Assert.Throws(Of IO.FileNotFoundException)(Sub() FileIO.FileSystem.RenameFile(IO.Path.Combine(TestDirectory, "DoesNotExistFile"), "NewFile"))
      Dim OrigFileWithPath As String = CreateTestFile(SourceData, TestFileName:=GetTestFileName())
      Dim ExistingFileWithPath As String = CreateTestFile(DestData, TestFileName:=GetTestFileName())
      ' <exception cref="System.ArgumentException">If newName is null or Empty String.</exception>
      Assert.Throws(Of ArgumentNullException)(Sub() FileIO.FileSystem.RenameFile(OrigFileWithPath, ""))
      ' <exception cref="System.ArgumentException">If contains path information.</exception>
      Assert.Throws(Of ArgumentException)(Sub() FileIO.FileSystem.RenameFile(OrigFileWithPath, ExistingFileWithPath))
      FileIO.FileSystem.RenameFile(OrigFileWithPath, "NewFile")
      Dim NewFileWithPath As String = IO.Path.Combine(TestDirectory, "NewFile")
      Assert.[True](IO.File.Exists(NewFileWithPath))
      Assert.[False](IO.File.Exists(OrigFileWithPath))
      ' <exception cref="IO.IOException">If there's an existing directory or an existing file with the same name.</exception>
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.RenameFile(NewFileWithPath, "NewFile"))
      IO.Directory.CreateDirectory(IO.Path.Combine(TestDirectory, "NewFDirectory"))
      Assert.Throws(Of IO.IOException)(Sub() FileIO.FileSystem.RenameFile(NewFileWithPath, "NewFDirectory"))
    End Sub

    ' Not tested:
    '   public void WriteAllBytes(string file, byte[] data, bool append) { }
    '   public void WriteAllText(string file, string text, bool append) { }
    '   public void WriteAllText(string file, string text, bool append, System.Text.Encoding encoding) { }

    Private Function CreateTestFile(TestData As String, TestFileName As String, Optional PathFromBase As String = Nothing) As String
      Assert.[False]([String].IsNullOrEmpty(TestFileName))
      Dim TempFileNameWithPath = TestDirectory
      If Not String.IsNullOrEmpty(PathFromBase) Then
        TempFileNameWithPath = IO.Path.Combine(TempFileNameWithPath, PathFromBase)
      End If
      TempFileNameWithPath = IO.Path.Combine(TempFileNameWithPath, TestFileName)
      Assert.[False](IO.File.Exists(TempFileNameWithPath), $"File {TempFileNameWithPath} should not exist!")
      WriteFile(TempFileNameWithPath, TestData)
      Return TempFileNameWithPath
    End Function

  End Class

End Namespace