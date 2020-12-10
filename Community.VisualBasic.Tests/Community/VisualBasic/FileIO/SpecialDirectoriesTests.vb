' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Environment
Imports Xunit

Namespace Global.Community.VisualBasic.FileIO.Tests

  Public NotInheritable Class SpecialDirectoriesTests

    Private Shared Sub CheckSpecialFolder(folder As SpecialFolder, getSpecialDirectory As Func(Of String))
      Dim path As String = Environment.GetFolderPath(folder)
      If String.IsNullOrEmpty(path) Then
        Assert.Throws(Of IO.DirectoryNotFoundException)(getSpecialDirectory)
      Else
        Assert.Equal(TrimSeparators(path), TrimSeparators(getSpecialDirectory()))
      End If
    End Sub

    '<SkipOnTargetFramework(TargetFrameworkMonikers.NetFramework, "Supported on netfx")>
    <Fact>
    Public Shared Sub AllUsersApplicationDataFolderTest()
      Assert.Throws(Of IO.DirectoryNotFoundException)(Function() SpecialDirectories.AllUsersApplicationData)
    End Sub

    '<SkipOnTargetFramework(TargetFrameworkMonikers.NetFramework, "Supported on netfx")>
    <Fact>
    Public Shared Sub CurrentUserApplicationDataFolderTest()
      Assert.Throws(Of IO.DirectoryNotFoundException)(Function() SpecialDirectories.CurrentUserApplicationData)
    End Sub

    <Fact>
    Public Shared Sub DesktopFolderTest()
      CheckSpecialFolder(SpecialFolder.Desktop, Function() SpecialDirectories.Desktop)
    End Sub

    <Fact>
    Public Shared Sub MyDocumentsFolderTest()
      If PlatformDetection.IsWindowsNanoServer Then
        Assert.Throws(Of IO.DirectoryNotFoundException)(Function() SpecialDirectories.MyDocuments)
      Else
        CheckSpecialFolder(SpecialFolder.MyDocuments, Function() SpecialDirectories.MyDocuments)
      End If
    End Sub

    <Fact>
    Public Shared Sub MyMusicFolderTest()
      CheckSpecialFolder(SpecialFolder.MyMusic, Function() SpecialDirectories.MyMusic)
    End Sub

    <Fact>
    Public Shared Sub MyPicturesFolderTest()
      CheckSpecialFolder(SpecialFolder.MyPictures, Function() SpecialDirectories.MyPictures)
    End Sub

    <Fact>
    Public Shared Sub ProgramFilesFolderTest()
      CheckSpecialFolder(SpecialFolder.ProgramFiles, Function() SpecialDirectories.ProgramFiles)
    End Sub

    <Fact>
    Public Shared Sub ProgramsFolderTest()
      CheckSpecialFolder(SpecialFolder.Programs, Function() SpecialDirectories.Programs)
    End Sub

    <Fact>
    Public Shared Sub TempFolderTest()
      ' On Nano Server >=1809 the temp path's case is changed during the normalization.
      Assert.Equal(TrimSeparators(IO.Path.GetTempPath()), TrimSeparators(SpecialDirectories.Temp), ignoreCase:=PlatformDetection.IsWindowsNanoServer)
    End Sub

    Private Shared Function TrimSeparators(s As String) As String
      Return s.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
    End Function

  End Class

End Namespace