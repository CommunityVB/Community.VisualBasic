' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security
Imports Microsoft.Win32
Imports Xunit

Namespace Global.Community

  Partial Public Module PlatformDetection

    '
    ' Do not use the " { get; } = <expression> " pattern here. Having all the initialization happen in the type initializer
    ' means that one exception anywhere means all tests using PlatformDetection fail. If you feel a value is worth latching,
    ' do it in a way that failures don't cascade.
    '

    Public ReadOnly Property IsWindows As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
      End Get
    End Property

    Public ReadOnly Property IsNetFramework As Boolean
      Get
        Return RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase)
      End Get
    End Property

    Public ReadOnly Property HasWindowsShell As Boolean
      Get
        Return IsWindows AndAlso IsNotWindowsServerCore AndAlso IsNotWindowsNanoServer AndAlso IsNotWindowsIoTCore
      End Get
    End Property

    Public ReadOnly Property IsWindows7 As Boolean
      Get
        Return IsWindows AndAlso GetWindowsVersion() = 6 AndAlso GetWindowsMinorVersion() = 1
      End Get
    End Property

    Public ReadOnly Property IsWindows8x As Boolean
      Get
        Return IsWindows AndAlso GetWindowsVersion() = 6 AndAlso (GetWindowsMinorVersion() = 2 OrElse GetWindowsMinorVersion() = 3)
      End Get
    End Property

    Public ReadOnly Property IsWindows8xOrLater As Boolean
      Get
        Return IsWindows AndAlso New Version(CInt(Fix(GetWindowsVersion())), CInt(Fix(GetWindowsMinorVersion()))) >= New Version(6, 2)
      End Get
    End Property

    Public ReadOnly Property IsWindowsNanoServer As Boolean
      Get
        Return IsWindows AndAlso (IsNotWindowsIoTCore AndAlso GetWindowsInstallationType().Equals("Nano Server", StringComparison.OrdinalIgnoreCase))
      End Get
    End Property

    Public ReadOnly Property IsWindowsServerCore As Boolean
      Get
        Return IsWindows AndAlso GetWindowsInstallationType().Equals("Server Core", StringComparison.OrdinalIgnoreCase)
      End Get
    End Property

    Public ReadOnly Property WindowsVersion As Integer
      Get
        Return If(IsWindows, CInt(Fix(GetWindowsVersion())), -1)
      End Get
    End Property

    Public ReadOnly Property IsNotWindows7 As Boolean
      Get
        Return Not IsWindows7
      End Get
    End Property

    Public ReadOnly Property IsNotWindows8x As Boolean
      Get
        Return Not IsWindows8x
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsNanoServer As Boolean
      Get
        Return Not IsWindowsNanoServer
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsServerCore As Boolean
      Get
        Return Not IsWindowsServerCore
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsNanoNorServerCore As Boolean
      Get
        Return IsNotWindowsNanoServer AndAlso IsNotWindowsServerCore
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsIoTCore As Boolean
      Get
        Return Not IsWindowsIoTCore
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsHomeEdition As Boolean
      Get
        Return Not IsWindowsHomeEdition
      End Get
    End Property

    Public ReadOnly Property IsNotInAppContainer As Boolean
      Get
        Return Not IsInAppContainer
      End Get
    End Property

    Public ReadOnly Property IsSoundPlaySupported As Boolean
      Get
        Return IsWindows AndAlso IsNotWindowsNanoServer
      End Get
    End Property

    ' >= Windows 10 Anniversary Update
    Public ReadOnly Property IsWindows10Version1607OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 14393
      End Get
    End Property

    ' >= Windows 10 Creators Update
    Public ReadOnly Property IsWindows10Version1703OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 15063
      End Get
    End Property

    ' >= Windows 10 Fall Creators Update
    Public ReadOnly Property IsWindows10Version1709OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 16299
      End Get
    End Property

    ' >= Windows 10 April 2018 Update
    Public ReadOnly Property IsWindows10Version1803OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 17134
      End Get
    End Property

    ' >= Windows 10 May 2019 Update (19H1)
    Public ReadOnly Property IsWindows10Version1903OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 18362
      End Get
    End Property

    ' >= Windows 10 20H1 Update (As of Jan. 2020 yet to be released)
    ' Per https://docs.microsoft.com/en-us/windows-insider/flight-hub/ the first 20H1 build is 18836.
    Public ReadOnly Property IsWindows10Version2004OrGreater As Boolean
      Get
        Return IsWindows AndAlso
        GetWindowsVersion() = 10 AndAlso GetWindowsMinorVersion() = 0 AndAlso GetWindowsBuildNumber() >= 18836
      End Get
    End Property

    Public ReadOnly Property IsWindows10Version2004Build19573OrGreater As Boolean
      Get
        Return IsWindows10Version2004OrGreater AndAlso GetWindowsBuildNumber() >= 19573
      End Get
    End Property

    Public ReadOnly Property IsWindowsIoTCore As Boolean
      Get
        If Not IsWindows Then
          Return False
        End If

        Dim productType As Integer = GetWindowsProductType()
        If (productType = PRODUCT_IOTUAPCOMMERCIAL) OrElse
        (productType = PRODUCT_IOTUAP) Then
          Return True
        End If

        Return False
      End Get
    End Property

    Public ReadOnly Property IsWindowsHomeEdition As Boolean
      Get
        If Not IsWindows Then
          Return False
        End If
        Dim productType As Integer = GetWindowsProductType()
        Select Case productType
          Case PRODUCT_CORE, PRODUCT_CORE_COUNTRYSPECIFIC, PRODUCT_CORE_N, PRODUCT_CORE_SINGLELANGUAGE, PRODUCT_HOME_BASIC, PRODUCT_HOME_BASIC_N, PRODUCT_HOME_PREMIUM, PRODUCT_HOME_PREMIUM_N
            Return True
          Case Else
            Return False
        End Select
      End Get
    End Property

    Public ReadOnly Property IsWindowsSubsystemForLinux As Boolean
      Get
        Return m_isWindowsSubsystemForLinux.Value
      End Get
    End Property

    Public ReadOnly Property IsNotWindowsSubsystemForLinux As Boolean
      Get
        Return Not IsWindowsSubsystemForLinux
      End Get
    End Property

    Private ReadOnly m_isWindowsSubsystemForLinux As New Lazy(Of Boolean)(GetIsWindowsSubsystemForLinux)

    Private Function GetIsWindowsSubsystemForLinux() As Boolean
      ' https://github.com/Microsoft/BashOnWindows/issues/423#issuecomment-221627364
      If IsLinux Then
        Const versionFile As String = "/proc/version"
        If File.Exists(versionFile) Then
          Dim s As String = File.ReadAllText(versionFile)

          If s.Contains("Microsoft") OrElse s.Contains("WSL") Then
            Return True
          End If
        End If
      End If

      Return False
    End Function

    Private Function GetWindowsInstallationType() As String
      If OperatingSystem.IsWindows Then
        Dim key As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion"
        Dim value1 As String = ""

        Try
          value1 = CStr(Registry.GetValue(key, "InstallationType", defaultValue:=""))
        Catch e As Exception When TypeOf e Is SecurityException OrElse TypeOf e Is InvalidCastException

        End Try

        Return value1
      Else
        Throw New PlatformNotSupportedException
      End If
    End Function

    Private Function GetWindowsProductType() As Integer
      Dim productType As Integer = Nothing
      Assert.[True](GetProductInfo(Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor, 0, 0, productType))
      Return productType
    End Function

    Private Const PRODUCT_IOTUAP As Integer = &H7B
    Private Const PRODUCT_IOTUAPCOMMERCIAL As Integer = &H83
    Private Const PRODUCT_CORE As Integer = &H65
    Private Const PRODUCT_CORE_COUNTRYSPECIFIC As Integer = &H63
    Private Const PRODUCT_CORE_N As Integer = &H62
    Private Const PRODUCT_CORE_SINGLELANGUAGE As Integer = &H64
    Private Const PRODUCT_HOME_BASIC As Integer = &H2
    Private Const PRODUCT_HOME_BASIC_N As Integer = &H5
    Private Const PRODUCT_HOME_PREMIUM As Integer = &H3
    Private Const PRODUCT_HOME_PREMIUM_N As Integer = &H1A

    <DllImport("kernel32.dll", SetLastError:=False)>
    Private Function GetProductInfo(
        dwOSMajorVersion As Integer,
        dwOSMinorVersion As Integer,
        dwSpMajorVersion As Integer,
        dwSpMinorVersion As Integer,
        <Out> ByRef pdwReturnedProductType As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True)>
    Private Function GetCurrentApplicationUserModelId(ByRef applicationUserModelIdLength As UInteger, applicationUserModelId As Byte()) As Integer
    End Function

    Friend Function GetWindowsVersion() As UInteger
      Dim osvi As Interop.NtDll.RTL_OSVERSIONINFOEX = Nothing
      Assert.Equal(0, Interop.NtDll.RtlGetVersionEx(osvi))
      Return osvi.dwMajorVersion
    End Function

    Friend Function GetWindowsMinorVersion() As UInteger
      Dim osvi As Interop.NtDll.RTL_OSVERSIONINFOEX = Nothing
      Assert.Equal(0, Interop.NtDll.RtlGetVersionEx(osvi))
      Return osvi.dwMinorVersion
    End Function

    Friend Function GetWindowsBuildNumber() As UInteger
      Dim osvi As Interop.NtDll.RTL_OSVERSIONINFOEX = Nothing
      Assert.Equal(0, Interop.NtDll.RtlGetVersionEx(osvi))
      Return osvi.dwBuildNumber
    End Function

    Private s_isInAppContainer As Integer = -1

    Public ReadOnly Property IsInAppContainer As Boolean
      ' This actually checks whether code is running in a modern app.
      ' Currently this is the only situation where we run in app container.
      ' If we want to distinguish the two cases in future,
      ' EnvironmentHelpers.IsAppContainerProcess in .NET Framework code shows how to check for the AC token.
      Get
        If s_isInAppContainer <> -1 Then
          Return s_isInAppContainer = 1
        End If

        If Not IsWindows OrElse IsWindows7 Then
          s_isInAppContainer = 0
          Return False
        End If

        Dim buffer As Byte() = Array.Empty(Of Byte)()
        Dim bufferSize As UInteger = 0
        Try
          Dim result As Integer = GetCurrentApplicationUserModelId(bufferSize, buffer)
          Select Case result
            Case 15703, 120
              ' This function is not supported on this system.
              ' In example on Windows Nano Server
              s_isInAppContainer = 0
            Case 0, 122
              ' Success is actually insufficent buffer as we're really only looking for
              ' not NO_APPLICATION and we're not actually giving a buffer here. The
              ' API will always return NO_APPLICATION if we're not running under a
              ' WinRT process, no matter what size the buffer is.
              s_isInAppContainer = 1
            Case Else
              Throw New InvalidOperationException($"Failed to get AppId, result was {result}.")
          End Select
        Catch e As Exception
          ' We could catch this here, being friendly with older portable surface area should we
          ' desire to use this method elsewhere.
          If e.[GetType]().FullName.Equals("System.EntryPointNotFoundException", StringComparison.Ordinal) Then
            ' API doesn't exist, likely pre Win8
            s_isInAppContainer = 0
          Else
            Throw
          End If

        End Try

        Return s_isInAppContainer = 1
      End Get
    End Property

    Private s_isWindowsElevated As Integer = -1

    Public ReadOnly Property IsWindowsAndElevated As Boolean
      Get
        If s_isWindowsElevated <> -1 Then
          Return s_isWindowsElevated = 1
        End If

        If Not IsWindows OrElse IsInAppContainer Then
          s_isWindowsElevated = 0
          Return False
        End If

        s_isWindowsElevated = If(AdminHelpers.IsProcessElevated(), 1, 0)
        Return s_isWindowsElevated = 1
      End Get
    End Property

  End Module

End Namespace