' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles
Imports System.Security.Principal

Namespace Global.Community

  Partial Friend Class Interop

    ''' <summary>
    ''' Blittable version of Windows BOOL type. It is convenient in situations where
    ''' manual marshalling is required, or to avoid overhead of regular bool marshalling.
    ''' </summary>
    ''' <remarks>
    ''' Some Windows APIs return arbitrary integer values although the return type is defined
    ''' as BOOL. It is best to never compare BOOL to TRUE. Always use bResult != BOOL.FALSE
    ''' or bResult == BOOL.FALSE .
    ''' </remarks>
    Friend Enum BOOL As Integer
      [FALSE] = 0
      [TRUE] = 1
    End Enum

    Partial Friend NotInheritable Class Libraries
      Friend Const Advapi32 As String = "advapi32.dll"
      Friend Const BCrypt As String = "BCrypt.dll"
      Friend Const Crypt32 As String = "crypt32.dll"
      Friend Const CryptUI As String = "cryptui.dll"
      Friend Const Gdi32 As String = "gdi32.dll"
      Friend Const HttpApi As String = "httpapi.dll"
      Friend Const IpHlpApi As String = "iphlpapi.dll"
      Friend Const Kernel32 As String = "kernel32.dll"
      Friend Const Mswsock As String = "mswsock.dll"
      Friend Const NCrypt As String = "ncrypt.dll"
      Friend Const NtDll As String = "ntdll.dll"
      Friend Const Odbc32 As String = "odbc32.dll"
      Friend Const Ole32 As String = "ole32.dll"
      Friend Const OleAut32 As String = "oleaut32.dll"
      Friend Const Pdh As String = "pdh.dll"
      Friend Const Secur32 As String = "secur32.dll"
      Friend Const Shell32 As String = "shell32.dll"
      Friend Const SspiCli As String = "sspicli.dll"
      Friend Const User32 As String = "user32.dll"
      Friend Const Version As String = "version.dll"
      Friend Const WebSocket As String = "websocket.dll"
      Friend Const WinHttp As String = "winhttp.dll"
      Friend Const WinMM As String = "winmm.dll"
      Friend Const Wldap32 As String = "wldap32.dll"
      Friend Const Ws2_32 As String = "ws2_32.dll"
      Friend Const Wtsapi32 As String = "wtsapi32.dll"
      Friend Const CompressionNative As String = "System.IO.Compression.Native"
      Friend Const GlobalizationNative As String = "System.Globalization.Native"
      Friend Const MsQuic As String = "msquic.dll"
      Friend Const HostPolicy As String = "hostpolicy.dll"
      ' Shims
      Public Const CryptoNative As String = "libSystem.Security.Cryptography.Native.OpenSsl"
      Public Const SystemNative As String = "libSystem.Native"
    End Class

    Partial Friend NotInheritable Class Sys

      <DllImport(Libraries.SystemNative, EntryPoint:="SystemNative_GetEUid")>
      Friend Shared Function GetEUid() As UInteger
      End Function

    End Class

    Partial Friend NotInheritable Class Advapi32

      ' https://msdn.microsoft.com/en-us/library/windows/desktop/bb530717.aspx
      Friend Structure TOKEN_ELEVATION
        Public TokenIsElevated As BOOL
      End Structure

      <DllImport(Interop.Libraries.Advapi32, SetLastError:=True)>
      Friend Shared Function GetTokenInformation(TokenHandle As SafeAccessTokenHandle,
                                                 TokenInformationClass As TOKEN_INFORMATION_CLASS,
                                                 TokenInformation As Object,
                                                 TokenInformationLength As UInteger,
                                                 <Out> ByRef ReturnLength As UInteger) As Boolean
      End Function

      ''' <summary>
      ''' <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379626.aspx">TOKEN_INFORMATION_CLASS</a> enumeration.
      ''' </summary>
      Friend Enum TOKEN_INFORMATION_CLASS As UInteger
        TokenUser = 1
        TokenGroups
        TokenPrivileges
        TokenOwner
        TokenPrimaryGroup
        TokenDefaultDacl
        TokenSource
        TokenType
        TokenImpersonationLevel
        TokenStatistics
        TokenRestrictedSids
        TokenSessionId
        TokenGroupsAndPrivileges
        TokenSessionReference
        TokenSandBoxInert
        TokenAuditPolicy
        TokenOrigin
        TokenElevationType
        TokenLinkedToken
        TokenElevation
        TokenHasRestrictions
        TokenAccessInformation
        TokenVirtualizationAllowed
        TokenVirtualizationEnabled
        TokenIntegrityLevel
        TokenUIAccess
        TokenMandatoryPolicy
        TokenLogonSid
        TokenIsAppContainer
        TokenCapabilities
        TokenAppContainerSid
        TokenAppContainerNumber
        TokenUserClaimAttributes
        TokenDeviceClaimAttributes
        TokenRestrictedUserClaimAttributes
        TokenRestrictedDeviceClaimAttributes
        TokenDeviceGroups
        TokenRestrictedDeviceGroups
        TokenSecurityAttributes
        TokenIsRestricted
        MaxTokenInfoClass
      End Enum

      <DllImport(Interop.Libraries.Advapi32, SetLastError:=True)>
      Friend Shared Function OpenProcessToken(ProcessToken As IntPtr, DesiredAccess As TokenAccessLevels, <Out> ByRef TokenHandle As SafeAccessTokenHandle) As Boolean
      End Function

    End Class

    Partial Friend NotInheritable Class Kernel32
      <DllImport(Libraries.Kernel32)>
      Friend Shared Function GetCurrentProcess() As IntPtr
      End Function
    End Class

    Partial Friend NotInheritable Class OpenSsl
      <DllImport(Libraries.CryptoNative, EntryPoint:="CryptoNative_OpenSslGetProtocolSupport")>
      Friend Shared Function OpenSslGetProtocolSupport(protocol As Integer) As Integer
      End Function

      <DllImport(Libraries.CryptoNative, EntryPoint:="CryptoNative_OpenSslVersionNumber")>
      Friend Shared Function OpenSslVersionNumber() As Long
      End Function

    End Class

    Partial Friend Class NtDll

      <DllImport(Libraries.NtDll, ExactSpelling:=True)>
      Private Shared Function RtlGetVersion(ByRef lpVersionInformation As RTL_OSVERSIONINFOEX) As Integer
      End Function

      Friend Shared Function RtlGetVersionEx(<Out> ByRef osvi As RTL_OSVERSIONINFOEX) As Integer
        osvi = CType(Nothing, RTL_OSVERSIONINFOEX)
        osvi.Init()
        osvi.dwOSVersionInfoSize = CUInt(Len(New RTL_OSVERSIONINFOEX()))
        Return RtlGetVersion(osvi)
      End Function

      <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
      Friend Structure RTL_OSVERSIONINFOEX
        Sub Init()
          ReDim szCSDVersion(127)
        End Sub
        Friend dwOSVersionInfoSize As UInteger
        Friend dwMajorVersion As UInteger
        Friend dwMinorVersion As UInteger
        Friend dwBuildNumber As UInteger
        Friend dwPlatformId As UInteger
        Friend szCSDVersion() As Char
      End Structure

    End Class

  End Class

End Namespace