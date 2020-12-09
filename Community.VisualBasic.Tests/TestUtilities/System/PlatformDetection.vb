' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.Diagnostics
Imports System.IO
Imports System.Security
Imports System.Security.Authentication
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices
Imports Microsoft.Win32
Imports System.Threading

Namespace Global.Community

  Partial Public Module PlatformDetection

    '
    ' Do not use the " { get; } = <expression> " pattern here. Having all the initialization happen in the type initializer
    ' means that one exception anywhere means all tests using PlatformDetection fail. If you feel a value is worth latching,
    ' do it in a way that failures don't cascade.
    '

    Public ReadOnly Property IsNetCore As Boolean
      Get
        Return Environment.Version.Major >= 5 OrElse RuntimeInformation.FrameworkDescription.StartsWith(".NET Core", StringComparison.OrdinalIgnoreCase)
      End Get
    End Property

    Public ReadOnly Property IsMonoRuntime As Boolean
      Get
        Return Type.[GetType]("Mono.RuntimeStructs") IsNot Nothing
      End Get
    End Property

    Public ReadOnly Property IsNotMonoRuntime As Boolean
      Get
        Return Not IsMonoRuntime
      End Get
    End Property

    Public ReadOnly Property IsMonoInterpreter As Boolean
      Get
        Return GetIsRunningOnMonoInterpreter()
      End Get
    End Property

    Public ReadOnly Property IsFreeBSD As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("FREEBSD"))
      End Get
    End Property

    Public ReadOnly Property IsNetBSD As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("NETBSD"))
      End Get
    End Property

    Public ReadOnly Property IsiOS As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"))
      End Get
    End Property

    Public ReadOnly Property IstvOS As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("TVOS"))
      End Get
    End Property

    Public ReadOnly Property Isillumos As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("ILLUMOS"))
      End Get
    End Property

    Public ReadOnly Property IsSolaris As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("SOLARIS"))
      End Get
    End Property

    Public ReadOnly Property IsBrowser As Boolean
      Get
        Return RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER"))
      End Get
    End Property

    Public ReadOnly Property IsNotBrowser As Boolean
      Get
        Return Not IsBrowser
      End Get
    End Property

    Public ReadOnly Property IsNotNetFramework As Boolean
      Get
        Return Not IsNetFramework
      End Get
    End Property

    Public ReadOnly Property IsArmProcess As Boolean
      Get
        Return RuntimeInformation.ProcessArchitecture = Architecture.Arm
      End Get
    End Property

    Public ReadOnly Property IsNotArmProcess As Boolean
      Get
        Return Not IsArmProcess
      End Get
    End Property

    Public ReadOnly Property IsArm64Process As Boolean
      Get
        Return RuntimeInformation.ProcessArchitecture = Architecture.Arm64
      End Get
    End Property

    Public ReadOnly Property IsNotArm64Process As Boolean
      Get
        Return Not IsArm64Process
      End Get
    End Property

    Public ReadOnly Property IsArmOrArm64Process As Boolean
      Get
        Return IsArmProcess OrElse IsArm64Process
      End Get
    End Property

    Public ReadOnly Property IsNotArmNorArm64Process As Boolean
      Get
        Return Not IsArmOrArm64Process
      End Get
    End Property

    Public ReadOnly Property IsArgIteratorSupported As Boolean
      Get
        Return IsMonoRuntime OrElse (IsWindows AndAlso IsNotArmProcess)
      End Get
    End Property

    Public ReadOnly Property IsArgIteratorNotSupported As Boolean
      Get
        Return Not IsArgIteratorSupported
      End Get
    End Property

    Public ReadOnly Property Is32BitProcess As Boolean
      Get
        Return IntPtr.Size = 4
      End Get
    End Property

    Public ReadOnly Property IsNotWindows As Boolean
      Get
        Return Not IsWindows
      End Get
    End Property

    Public ReadOnly Property IsThreadingSupported As Boolean
      Get
        Return Not IsBrowser
      End Get
    End Property

    Public ReadOnly Property IsBinaryFormatterSupported As Boolean
      Get
        Return Not IsBrowser
      End Get
    End Property

    Public ReadOnly Property IsBrowserDomSupported As Boolean
      Get
        Return GetIsBrowserDomSupported()
      End Get
    End Property

    Public ReadOnly Property IsNotBrowserDomSupported As Boolean
      Get
        Return Not IsBrowserDomSupported
      End Get
    End Property

    ' Please make sure that you have the libgdiplus dependency installed.
    ' For details, see https://docs.microsoft.com/dotnet/core/install/dependencies?pivots=os-macos&tabs=netcore31#libgdiplus

    Public ReadOnly Property IsDrawingSupported As Boolean
      Get
#If NETCOREAPP Then
        If Not IsWindows Then
          Dim temp As IntPtr
          If IsOSX Then
            Return NativeLibrary.TryLoad("libgdiplus.dylib", temp)
          Else
            Return NativeLibrary.TryLoad("libgdiplus.so", temp) OrElse NativeLibrary.TryLoad("libgdiplus.so.0", temp)
          End If
        End If

#End If
        Return IsNotWindowsNanoServer AndAlso IsNotWindowsServerCore
      End Get
    End Property

    Public ReadOnly Property IsInContainer As Boolean
      Get
        Return GetIsInContainer()
      End Get
    End Property

    Public ReadOnly Property SupportsComInterop As Boolean
      Get
        Return IsWindows AndAlso IsNotMonoRuntime
      End Get
    End Property
    Public ReadOnly Property SupportsSsl3 As Boolean
      Get
        Return GetSsl3Support()
      End Get
    End Property

    Public ReadOnly Property SupportsSsl2 As Boolean
      Get
        Return IsWindows AndAlso Not PlatformDetection.IsWindows10Version1607OrGreater
      End Get
    End Property

#If NETCOREAPP Then
    Public ReadOnly Property IsReflectionEmitSupported As Boolean
      Get
        Return RuntimeFeature.IsDynamicCodeSupported
      End Get
    End Property
#Else
        public static bool IsReflectionEmitSupported => true;
#End If

    Public ReadOnly Property IsInvokingStaticConstructorsSupported As Boolean
      Get
        Return True
      End Get
    End Property

    ' System.Security.Cryptography.Xml.XmlDsigXsltTransform.GetOutput() relies on XslCompiledTransform which relies
    ' heavily on Reflection.Emit

    Public ReadOnly Property IsXmlDsigXsltTransformSupported As Boolean
      Get
        Return Not PlatformDetection.IsInAppContainer
      End Get
    End Property

    Public ReadOnly Property IsPreciseGcSupported As Boolean
      Get
        Return Not IsMonoRuntime
      End Get
    End Property

    Public ReadOnly Property IsNotIntMaxValueArrayIndexSupported As Boolean
      Get
        Return s_largeArrayIsNotSupported.Value
      End Get
    End Property

    '<Volatile>
    Private s_lazyNonZeroLowerBoundArraySupported As Tuple(Of Boolean)

    Public ReadOnly Property IsNonZeroLowerBoundArraySupported As Boolean
      Get
        If s_lazyNonZeroLowerBoundArraySupported Is Nothing Then
          Dim nonZeroLowerBoundArraysSupported As Boolean = False
          Try
            Array.CreateInstance(GetType(Integer), New Integer() {5}, New Integer() {5})
            nonZeroLowerBoundArraysSupported = True
          Catch __unusedPlatformNotSupportedException1__ As PlatformNotSupportedException

          End Try

          s_lazyNonZeroLowerBoundArraySupported = Tuple.Create(Of Boolean)(nonZeroLowerBoundArraysSupported)
        End If

        Return s_lazyNonZeroLowerBoundArraySupported.Item1
      End Get
    End Property

    Public ReadOnly Property IsDomainJoinedMachine As Boolean
      Get
        Return Not Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase)
      End Get
    End Property

    Public ReadOnly Property IsNotDomainJoinedMachine As Boolean
      Get
        Return Not IsDomainJoinedMachine
      End Get
    End Property

    ' Windows - Schannel supports alpn from win8.1/2012 R2 and higher.
    ' Linux - OpenSsl supports alpn from openssl 1.0.2 and higher.
    ' OSX - SecureTransport doesn't expose alpn APIs. TODO https://github.com/dotnet/runtime/issues/27727
    Public ReadOnly Property IsOpenSslSupported As Boolean
      Get
        Return IsLinux OrElse IsFreeBSD OrElse Isillumos OrElse IsSolaris
      End Get
    End Property

    Public ReadOnly Property SupportsAlpn As Boolean
      Get
        Return (IsWindows AndAlso Not IsWindows7) OrElse
        (IsOpenSslSupported AndAlso
        (OpenSslVersion.Major >= 1 AndAlso (OpenSslVersion.Minor >= 1 OrElse OpenSslVersion.Build >= 2)))
      End Get
    End Property

    Public ReadOnly Property SupportsClientAlpn As Boolean
      Get
        Return SupportsAlpn OrElse IsOSX OrElse IsiOS OrElse IstvOS
      End Get
    End Property

    Private ReadOnly s_supportsTls10 As New Lazy(Of Boolean)(GetTls10Support)
    Private ReadOnly s_supportsTls11 As New Lazy(Of Boolean)(GetTls11Support)
    Private ReadOnly s_supportsTls12 As New Lazy(Of Boolean)(GetTls12Support)
    Private ReadOnly s_supportsTls13 As New Lazy(Of Boolean)(GetTls13Support)

    Public ReadOnly Property SupportsTls10 As Boolean
      Get
        Return s_supportsTls10.Value
      End Get
    End Property

    Public ReadOnly Property SupportsTls11 As Boolean
      Get
        Return s_supportsTls11.Value
      End Get
    End Property

    Public ReadOnly Property SupportsTls12 As Boolean
      Get
        Return s_supportsTls12.Value
      End Get
    End Property

    Public ReadOnly Property SupportsTls13 As Boolean
      Get
        Return s_supportsTls13.Value
      End Get
    End Property

    Private ReadOnly s_largeArrayIsNotSupported As New Lazy(Of Boolean)(IsLargeArrayNotSupported)

    <MethodImpl(MethodImplOptions.NoOptimization)>
    Private Function IsLargeArrayNotSupported() As Boolean
      Try
        Dim tmp As Byte() = New Byte(2147483646) {}
        Return tmp Is Nothing
      Catch __unusedOutOfMemoryException1__ As OutOfMemoryException
        Return True
      End Try
    End Function

    Public Function GetDistroVersionString() As String
      If IsWindows Then
        Return "WindowsProductType=" & GetWindowsProductType() & " WindowsInstallationType=" + GetWindowsInstallationType()

      ElseIf IsOSX Then
        Return "OSX Version=" & Environment.OSVersion.Version.ToString()

      Else
        Dim v As DistroInfo = GetDistroInfo()

        Return $"Distro={v.Id} VersionId={v.VersionId}"
      End If
    End Function

    Private ReadOnly m_isInvariant As New Lazy(Of Boolean)(GetIsInvariantGlobalization)

    Private Function GetIsInvariantGlobalization() As Boolean
      Dim globalizationMode As Type = Type.[GetType]("System.Globalization.GlobalizationMode")
      If globalizationMode IsNot Nothing Then
        Dim methodInfo1 As MethodInfo = globalizationMode.GetProperty("Invariant", BindingFlags.NonPublic Or BindingFlags.[Static])?.GetMethod
        If methodInfo1 IsNot Nothing Then
          Return CBool(methodInfo1.Invoke(Nothing, Nothing))
        End If
      End If

      Return False
    End Function

    Private ReadOnly m_icuVersion As New Lazy(Of Version)(GetICUVersion)

    Public ReadOnly Property ICUVersion As Version
      Get
        Return m_icuVersion.Value
      End Get
    End Property

    Public ReadOnly Property IsInvariantGlobalization As Boolean
      Get
        Return m_isInvariant.Value
      End Get
    End Property

    Public ReadOnly Property IsNotInvariantGlobalization As Boolean
      Get
        Return Not IsInvariantGlobalization
      End Get
    End Property

    Public ReadOnly Property IsIcuGlobalization As Boolean
      Get
        Return ICUVersion > New Version(0, 0, 0, 0)
      End Get
    End Property

    Public ReadOnly Property IsNlsGlobalization As Boolean
      Get
        Return IsNotInvariantGlobalization AndAlso Not IsIcuGlobalization
      End Get
    End Property

    Private Function GetICUVersion() As Version
      Dim version1 As Integer = 0
      Try
        Dim interopGlobalization As Type = Type.[GetType]("Interop+Globalization")
        If interopGlobalization IsNot Nothing Then
          Dim methodInfo1 As MethodInfo = interopGlobalization.GetMethod("GetICUVersion", BindingFlags.NonPublic Or BindingFlags.[Static])
          If methodInfo1 IsNot Nothing Then
            version1 = CInt(Fix(methodInfo1.Invoke(Nothing, Nothing)))
          End If
        End If
      Catch

      End Try

      Return New Version(version1 >> 24,
(version1 >> 16) And &HFF,
(version1 >> 8) And &HFF,
                        version1 And &HFF)
    End Function

    Private Function GetIsInContainer() As Boolean
      If OperatingSystem.IsWindows Then
        Dim key As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control"
        Return Registry.GetValue(key, "ContainerType", defaultValue:=Nothing) IsNot Nothing
      End If

      Return (IsLinux AndAlso File.Exists("/.dockerenv"))
    End Function

    Private Function GetSsl3Support() As Boolean
      If OperatingSystem.IsWindows Then
        Dim clientKey As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 3.0\Client"
        Dim serverKey As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 3.0\Server"

        Dim client, server As Object
        Try
          client = Registry.GetValue(clientKey, "Enabled", Nothing)
          server = Registry.GetValue(serverKey, "Enabled", Nothing)
        Catch __unusedSecurityException1__ As SecurityException
          ' Insufficient permission, assume that we don't have SSL3 (since we aren't exactly sure)
          Return False
        End Try

        Dim TempVar As Boolean = TypeOf client Is Integer
        Dim c As Integer
        Dim temp = Integer.TryParse(client.ToString, c)
        Dim TempVar1 As Boolean = TypeOf server Is Integer
        Dim s As Integer
        temp = Integer.TryParse(server.ToString, s)

        If TempVar AndAlso TempVar1 Then
          Return c = 1 AndAlso s = 1
        End If

        ' Missing key. If we're pre-20H1 then assume SSL3 is enabled.
        ' Otherwise, disabled. (See comments on https://github.com/dotnet/runtime/issues/1166)
        ' Alternatively the returned values must have been some other types.
        Return Not IsWindows10Version2004OrGreater
      End If

      Return (IsOSX OrElse (IsLinux AndAlso OpenSslVersion < New Version(1, 0, 2) AndAlso Not IsDebian))
    End Function

    Private Function OpenSslGetTlsSupport(protocol As SslProtocols) As Boolean
      Debug.Assert(IsOpenSslSupported)

      Dim ret As Integer = Interop.OpenSsl.OpenSslGetProtocolSupport(CInt(Fix(protocol)))
      Return ret = 1
    End Function

    Private Function GetTls10Support() As Boolean
      ' on Windows and macOS TLS1.0/1.1 are supported.
      If IsWindows OrElse IsOSXLike Then
        Return True
      End If

      Return OpenSslGetTlsSupport(SslProtocols.Tls)
    End Function

    Private Function GetTls11Support() As Boolean
      ' on Windows and macOS TLS1.0/1.1 are supported.
      ' TLS 1.1 and 1.2 can work on Windows7 but it is not enabled by default.
      If IsWindows Then
        Return Not IsWindows7
      ElseIf IsOSXLike Then
        Return True
      End If

      Return OpenSslGetTlsSupport(SslProtocols.Tls11)
    End Function

    Private Function GetTls12Support() As Boolean
      ' TLS 1.1 and 1.2 can work on Windows7 but it is not enabled by default.
      Return Not IsWindows7
    End Function

    Private Function GetTls13Support() As Boolean
      If OperatingSystem.IsWindows Then
        If Not IsWindows10Version2004OrGreater Then
          Return False
        End If

        Dim clientKey As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Client"
        Dim serverKey As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Server"

        Dim client, server As Object
        Try
          client = Registry.GetValue(clientKey, "Enabled", Nothing)
          server = Registry.GetValue(serverKey, "Enabled", Nothing)
          Dim TempVar2 As Boolean = TypeOf client Is Integer
          Dim c As Integer
          Dim temp = Integer.TryParse(client.ToString, c)
          Dim TempVar3 As Boolean = TypeOf server Is Integer
          Dim s As Integer
          temp = Integer.TryParse(server.ToString, s)

          If TempVar2 AndAlso TempVar3 Then
            Return c = 1 AndAlso s = 1
          End If
        Catch

        End Try
        ' assume no if positive entry is missing on older Windows
        ' Latest insider builds have TLS 1.3 enabled by default.
        ' The build number is approximation.
        Return IsWindows10Version2004Build19573OrGreater
      ElseIf IsOSX OrElse IsiOS OrElse IstvOS Then
        ' [ActiveIssue("https://github.com/dotnet/runtime/issues/1979")]
        Return False
      ElseIf IsOpenSslSupported Then
        ' Covers Linux, FreeBSD, illumos and Solaris
        Return OpenSslVersion >= New Version(1, 1, 1)
      End If

      Return False
    End Function

    Private Function GetIsRunningOnMonoInterpreter() As Boolean
      ' Browser is always using interpreter right now
      If IsBrowser Then
        Return True
      End If

      ' This is a temporary solution because mono does not support interpreter detection
      ' within the runtime.
      Dim val As String = Environment.GetEnvironmentVariable("MONO_ENV_OPTIONS")
      Return (val IsNot Nothing AndAlso val.Contains("--interpreter"))
    End Function

    Private Function GetIsBrowserDomSupported() As Boolean
      If Not IsBrowser Then
        Return False
      End If

      Dim val As String = Environment.GetEnvironmentVariable("IsBrowserDomSupported")
      Return (val IsNot Nothing AndAlso val = "true")
    End Function

  End Module

End Namespace