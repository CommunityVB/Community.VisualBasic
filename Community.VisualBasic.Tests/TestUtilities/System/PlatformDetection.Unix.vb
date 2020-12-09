' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.IO
Imports System.Runtime.InteropServices

Namespace Global.Community

  Partial Public Module PlatformDetection

    '
    ' Do not use the " { get; } = <expression> " pattern here. Having all the initialization happen in the type initializer
    ' means that one exception anywhere means all tests using PlatformDetection fail. If you feel a value is worth latching,
    ' do it in a way that failures don't cascade.
    '

    Private ReadOnly Property IsLinux As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
      End Get
    End Property

    Public ReadOnly Property IsOpenSUSE As Boolean
      Get
        Return IsDistroAndVersion("opensuse")
      End Get
    End Property

    Public ReadOnly Property IsUbuntu As Boolean
      Get
        Return IsDistroAndVersion("ubuntu")
      End Get
    End Property

    Public ReadOnly Property IsDebian As Boolean
      Get
        Return IsDistroAndVersion("debian")
      End Get
    End Property

    Public ReadOnly Property IsAlpine As Boolean
      Get
        Return IsDistroAndVersion("alpine")
      End Get
    End Property

    Public ReadOnly Property IsDebian8 As Boolean
      Get
        Return IsDistroAndVersion("debian", 8)
      End Get
    End Property

    Public ReadOnly Property IsDebian10 As Boolean
      Get
        Return IsDistroAndVersion("debian", 10)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1604 As Boolean
      Get
        Return IsDistroAndVersion("ubuntu", 16, 4)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1704 As Boolean
      Get
        Return IsDistroAndVersion("ubuntu", 17, 4)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1710 As Boolean
      Get
        Return IsDistroAndVersion("ubuntu", 17, 10)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1710OrHigher As Boolean
      Get
        Return IsDistroAndVersionOrHigher("ubuntu", 17, 10)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1804 As Boolean
      Get
        Return IsDistroAndVersion("ubuntu", 18, 4)
      End Get
    End Property

    Public ReadOnly Property IsUbuntu1810OrHigher As Boolean
      Get
        Return IsDistroAndVersionOrHigher("ubuntu", 18, 10)
      End Get
    End Property

    Public ReadOnly Property IsTizen As Boolean
      Get
        Return IsDistroAndVersion("tizen")
      End Get
    End Property

    Public ReadOnly Property IsFedora As Boolean
      Get
        Return IsDistroAndVersion("fedora")
      End Get
    End Property

    ' OSX family

    Public ReadOnly Property IsOSXLike As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS")) OrElse
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX) OrElse
        RuntimeInformation.IsOSPlatform(OSPlatform.Create("TVOS"))
      End Get
    End Property

    Public ReadOnly Property IsOSX As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
      End Get
    End Property

    Public ReadOnly Property IsNotOSX As Boolean
      Get
        Return Not IsOSX
      End Get
    End Property

    Public ReadOnly Property IsMacOsMojaveOrHigher As Boolean
      Get
        Return IsOSX AndAlso Environment.OSVersion.Version >= New Version(10, 14)
      End Get
    End Property

    Public ReadOnly Property IsMacOsCatalinaOrHigher As Boolean
      Get
        Return IsOSX AndAlso Environment.OSVersion.Version >= New Version(10, 15)
      End Get
    End Property

    ' RedHat family covers RedHat and CentOS

    Public ReadOnly Property IsRedHatFamily As Boolean
      Get
        Return IsRedHatFamilyAndVersion()
      End Get
    End Property

    Public ReadOnly Property IsNotRedHatFamily As Boolean
      Get
        Return Not IsRedHatFamily
      End Get
    End Property

    Public ReadOnly Property IsRedHatFamily7 As Boolean
      Get
        Return IsRedHatFamilyAndVersion(7)
      End Get
    End Property

    Public ReadOnly Property IsNotFedoraOrRedHatFamily As Boolean
      Get
        Return Not IsFedora AndAlso Not IsRedHatFamily
      End Get
    End Property

    Public ReadOnly Property IsNotDebian10 As Boolean
      Get
        Return Not IsDebian10
      End Get
    End Property

    ' Android

    Public ReadOnly Property IsAndroid As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("Android"))
      End Get
    End Property

    Public ReadOnly Property IsSuperUser As Boolean
      Get
        Return Not IsBrowser AndAlso Not IsWindows AndAlso libc.geteuid() = 0
      End Get
    End Property

    Public ReadOnly Property OpenSslVersion As Version
      Get
        If Not Not IsOSXLike AndAlso Not IsWindows Then Throw New PlatformNotSupportedException
        Return GetOpenSslVersion()
      End Get
    End Property

    ''' <summary>
    ''' If gnulibc is available, returns the release, such as "stable".
    ''' Otherwise returns "glibc_not_found".
    ''' </summary>
    Public ReadOnly Property LibcRelease As String
      Get
        If IsWindows Then
          Return "glibc_not_found"
        End If

        Try
          Return Marshal.PtrToStringAnsi(libc.gnu_get_libc_release())
        Catch e As Exception When TypeOf e Is DllNotFoundException OrElse TypeOf e Is EntryPointNotFoundException
          Return "glibc_not_found"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' If gnulibc is available, returns the version, such as "2.22".
    ''' Otherwise returns "glibc_not_found". (In future could run "ldd -version" for musl)
    ''' </summary>
    Public ReadOnly Property LibcVersion As String
      Get
        If IsWindows Then
          Return "glibc_not_found"
        End If

        Try
          Return Marshal.PtrToStringAnsi(libc.gnu_get_libc_version())
        Catch e As Exception When TypeOf e Is DllNotFoundException OrElse TypeOf e Is EntryPointNotFoundException
          Return "glibc_not_found"
        End Try
      End Get
    End Property

    Private s_opensslVersion As Version

    Private Function GetOpenSslVersion() As Version
      If s_opensslVersion Is Nothing Then
        ' OpenSSL version numbers are encoded as
        ' 0xMNNFFPPS: major (one nybble), minor (one byte, unaligned),
        ' "fix" (one byte, unaligned), patch (one byte, unaligned), status (one nybble)
        '
        ' e.g. 1.0.2a final is 0x1000201F
        '
        ' Currently they don't exceed 29-bit values, but we use long here to account
        ' for the expanded range on their 64-bit C-long return value.
        Dim versionNumber As Long = Interop.OpenSsl.OpenSslVersionNumber()
        Dim major As Integer = CInt(Community.VisualBasic.Fix(((versionNumber >> 28) And &HF)))
        Dim minor As Integer = CInt(Community.VisualBasic.Fix(((versionNumber >> 20) And &HFF)))
        Dim fix As Integer = CInt(Community.VisualBasic.Fix(((versionNumber >> 12) And &HFF)))

        s_opensslVersion = New Version(major, minor, fix)
      End If

      Return s_opensslVersion
    End Function

    Private Function ToVersion(versionString As String) As Version
      ' In some distros/versions we cannot discover the distro version; return something valid.
      ' Pick a high version number, since this seems to happen on newer distros.
      If String.IsNullOrEmpty(versionString) Then
        versionString = New Version(Int32.MaxValue, Int32.MaxValue).ToString()
      End If

      Try
        If versionString.IndexOf("."c) <> -1 Then
          Return New Version(versionString)
        End If

        ' minor version is required by Version
        ' let's default it to 0
        Return New Version(Integer.Parse(versionString), 0)
      Catch exc As Exception
        Throw New FormatException($"Failed to parse version string: '{versionString}'", exc)
      End Try
    End Function

    Private Function GetDistroInfo() As DistroInfo
      Dim result As New DistroInfo

      If IsFreeBSD Then
        result.Id = "FreeBSD"
        ' example:
        ' FreeBSD 11.0-RELEASE-p1 FreeBSD 11.0-RELEASE-p1 #0 r306420: Thu Sep 29 01:43:23 UTC 2016     root@releng2.nyi.freebsd.org:/usr/obj/usr/src/sys/GENERIC
        ' What we want is major release as minor releases should be compatible.
        result.VersionId = ToVersion(RuntimeInformation.OSDescription.Split()(1).Split("."c)(0))
      ElseIf Isillumos Then
        ' examples:
        '   on OmniOS
        '       SunOS 5.11 omnios-r151018-95eaa7e
        '   on OpenIndiana Hipster:
        '       SunOS 5.11 illumos-63878f749f
        '   on SmartOS:
        '       SunOS 5.11 joyent_20200408T231825Z
        Dim versionDescription As String = RuntimeInformation.OSDescription.Split(" "c)(2)
        Select Case True
          Case TypeOf versionDescription Is String
            Dim version1 As String = CType(versionDescription, String)
            If version1.StartsWith("omnios") Then Exit Select
            result.Id = "OmniOS"
            result.VersionId = ToVersion(version1.Substring("omnios-r".Length, 2)) ' e.g. 15
          Case TypeOf versionDescription Is String
            Dim version1 As String = CType(versionDescription, String)
            If version1.StartsWith("joyent") Then Exit Select
            result.Id = "SmartOS"
            result.VersionId = ToVersion(version1.Substring("joyent_".Length, 4)) ' e.g. 2020
          Case TypeOf versionDescription Is String
            Dim version1 As String = CType(versionDescription, String)
            If version1.StartsWith("illumos") Then Exit Select
            result.Id = "OpenIndiana"
            ' version-less

        End Select
      ElseIf IsSolaris Then
        ' example:
        '   SunOS 5.11 11.3
        result.Id = "Solaris"
        ' we only need the major version; 11
        result.VersionId = ToVersion(RuntimeInformation.OSDescription.Split(" "c)(2).Split("."c)(0)) ' e.g. 11
      ElseIf File.Exists("/etc/os-release") Then
        For Each line As String In File.ReadAllLines("/etc/os-release")
          If line.StartsWith("ID=", StringComparison.Ordinal) Then
            result.Id = line.Substring(3).Trim(""""c, "'"c)
          ElseIf line.StartsWith("VERSION_ID=", StringComparison.Ordinal) Then
            result.VersionId = ToVersion(line.Substring(11).Trim(""""c, "'"c))
          End If
        Next
      End If

      result.Id = If(result.Id, "Linux")
      result.VersionId = If(result.VersionId, ToVersion(String.Empty))

      Return result
    End Function

    Private Function IsRedHatFamilyAndVersion(Optional major As Integer = -1, Optional minor As Integer = -1, Optional build As Integer = -1, Optional revision As Integer = -1) As Boolean
      Return IsDistroAndVersion(Function(distro) distro = "rhel" OrElse distro = "centos", major, minor, build, revision)
    End Function

    ''' <summary>
    ''' Get whether the OS platform matches the given Linux distro and optional version.
    ''' </summary>
    ''' <param name="distroId">The distribution id.</param>
    ''' <param name="major">The distro major version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="minor">The distro minor version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="build">The distro build version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="revision">The distro revision version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <returns>Whether the OS platform matches the given Linux distro and optional version.</returns>
    Private Function IsDistroAndVersion(distroId As String, Optional major As Integer = -1, Optional minor As Integer = -1, Optional build As Integer = -1, Optional revision As Integer = -1) As Boolean
      Return IsDistroAndVersion(Function(distro) (distro = distroId), major, minor, build, revision)
    End Function

    ''' <summary>
    ''' Get whether the OS platform matches the given Linux distro and optional version is same or higher.
    ''' </summary>
    ''' <param name="distroId">The distribution id.</param>
    ''' <param name="major">The distro major version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="minor">The distro minor version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="build">The distro build version. If omitted, this portion of the version is not included in the comparison.</param>
    ''' <param name="revision">The distro revision version.  If omitted, this portion of the version is not included in the comparison.</param>
    ''' <returns>Whether the OS platform matches the given Linux distro and optional version is same or higher.</returns>
    Private Function IsDistroAndVersionOrHigher(distroId As String, Optional major As Integer = -1, Optional minor As Integer = -1, Optional build As Integer = -1, Optional revision As Integer = -1) As Boolean
      Return IsDistroAndVersionOrHigher(Function(distro) (distro = distroId), major, minor, build, revision)
    End Function

    Private Function IsDistroAndVersion(distroPredicate As Predicate(Of String), Optional major As Integer = -1, Optional minor As Integer = -1, Optional build As Integer = -1, Optional revision As Integer = -1) As Boolean
      If IsLinux Then
        Dim v As DistroInfo = GetDistroInfo()
        If distroPredicate(v.Id) AndAlso VersionEquivalentTo(major, minor, build, revision, v.VersionId) Then
          Return True
        End If
      End If

      Return False
    End Function

    Private Function IsDistroAndVersionOrHigher(distroPredicate As Predicate(Of String), Optional major As Integer = -1, Optional minor As Integer = -1, Optional build As Integer = -1, Optional revision As Integer = -1) As Boolean
      If IsLinux Then
        Dim v As DistroInfo = GetDistroInfo()
        If distroPredicate(v.Id) AndAlso VersionEquivalentToOrHigher(major, minor, build, revision, v.VersionId) Then
          Return True
        End If
      End If

      Return False
    End Function

    Private Function VersionEquivalentTo(major As Integer, minor As Integer, build As Integer, revision As Integer, actualVersionId As Version) As Boolean
      Return (major = -1 OrElse major = actualVersionId.Major) AndAlso (minor = -1 OrElse minor = actualVersionId.Minor) AndAlso (build = -1 OrElse build = actualVersionId.Build) AndAlso (revision = -1 OrElse revision = actualVersionId.Revision)
    End Function

    Private Function VersionEquivalentToOrHigher(major As Integer, minor As Integer, build As Integer, revision As Integer, actualVersionId As Version) As Boolean
      Return VersionEquivalentTo(major, minor, build, revision, actualVersionId) OrElse
             (actualVersionId.Major > major OrElse
             (actualVersionId.Major = major AndAlso (actualVersionId.Minor > minor OrElse
             (actualVersionId.Minor = minor AndAlso (actualVersionId.Build > build OrElse
             (actualVersionId.Build = build AndAlso (actualVersionId.Revision > revision OrElse
             (actualVersionId.Revision = revision))))))))
    End Function

    Private Structure DistroInfo
      Public Property Id As String
      Public Property VersionId As Version
    End Structure

    Private NotInheritable Class Libc

      <DllImport("libc", SetLastError:=True)>
      Public Shared Function geteuid() As UInteger
      End Function


      <DllImport("libc", CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
      Public Shared Function gnu_get_libc_release() As IntPtr

      End Function

      <DllImport("libc", CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
      Public Shared Function gnu_get_libc_version() As IntPtr
      End Function

    End Class

  End Module

End Namespace