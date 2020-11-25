Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Global.System

  Partial Friend Class OperatingSystem

    ' Inspired by https://mariusschulz.com/blog/detecting-the-operating-system-in-net-core
    ' and .NET 5.0 OperatingSystem class and extensions...
    ' Tweaked to be Extension Methods for the existing OperatingSystem class.
    ' This means that these extensions (this module) is 

    '#If TARGET_UNIX And Not TARGET_OSX Then
    '        private static readonly string s_osPlatformName = Interop.Sys.GetUnixName();
    '#End If

    Private Sub Test()
      If OperatingSystem.IsAndroid Then

      End If

    End Sub

#If REMOVE Then

    Private ReadOnly _version As Version
    Private ReadOnly _platform As PlatformID
    Private ReadOnly _servicePack As String = Nothing
    Private _versionString As String = Nothing

    Public Sub New(platform As PlatformID, version1 As Version)
      MyBase.New(platform, version1, Nothing)
    End Sub

    Friend Sub New(platform As PlatformID, version1 As Version, servicePack As String)
      If platform < PlatformID.Win32S OrElse platform > PlatformID.Other Then
        Throw New ArgumentOutOfRangeException(NameOf(platform), platform, SR.Format(SR.Arg_EnumIllegalVal, platform))
      End If

      If version1 Is Nothing Then
        Throw New ArgumentNullException(NameOf(version1))
      End If

      _platform = platform
      _version = version1
      _servicePack = servicePack
    End Sub

    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements Runtime.Serialization.ISerializable.GetObjectData
      Throw New PlatformNotSupportedException
    End Sub

    Public ReadOnly Property Platform1 As PlatformID
      Get
        Return _platform
      End Get
    End Property

    Public ReadOnly Property ServicePack1 As String
      Get
        Return If(_servicePack, String.Empty)
      End Get
    End Property

    Public ReadOnly Property Version As Version
      Get
        Return _version
      End Get
    End Property

    Public Function Clone() As Object Implements System.ICloneable.Clone
      Return New OperatingSystem(_platform, _version, _servicePack)
    End Function

    Public Overrides Function ToString() As String
      Return VersionString
    End Function

    Public ReadOnly Property VersionString As String
      Get
        If _versionString Is Nothing Then
          Dim os As String
          Select Case _platform
            Case PlatformID.Win32S
              os = "Microsoft Win32S "
            Case PlatformID.Win32Windows
              os = If((_version.Major > 4 OrElse (_version.Major = 4 AndAlso _version.Minor > 0)), "Microsoft Windows 98 ", "Microsoft Windows 95 ")
            Case PlatformID.Win32NT
              os = "Microsoft Windows NT "
            Case PlatformID.WinCE
              os = "Microsoft Windows CE "
            Case PlatformID.Unix
              os = "Unix "
            Case PlatformID.Xbox
              os = "Xbox "
            Case PlatformID.MacOSX
              os = "Mac OS X "
            Case PlatformID.Other
              os = "Other "
            Case Else
              Debug.Fail($"Unknown platform {_platform}")
              os = "<unknown> "
          End Select

          _versionString = If(String.IsNullOrEmpty(_servicePack),
                        os & _version.ToString(),
                        os & _version.ToString(3) & " " & _servicePack)
        End If

        Return _versionString
      End Get
    End Property
#End If

    ''' <summary>
    ''' Indicates whether the current application is running on the specified platform.
    ''' </summary>
    ''' <param name="platform">Case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    Public Shared Function IsOSPlatform(platform As String) As Boolean
      If platform Is Nothing Then
        Throw New ArgumentNullException(NameOf(platform))
      End If
      '#If TARGET_BROWSER Then
      '            return platform.Equals("BROWSER", StringComparison.OrdinalIgnoreCase);
      '#ElseIf TARGET_WINDOWS Then
      '            return platform.Equals("WINDOWS", StringComparison.OrdinalIgnoreCase);
      '#ElseIf TARGET_OSX Then
      '            return platform.Equals("OSX", StringComparison.OrdinalIgnoreCase) || platform.Equals("MACOS", StringComparison.OrdinalIgnoreCase);
      '#ElseIf TARGET_UNIX Then
      '            return platform.Equals(s_osPlatformName, StringComparison.OrdinalIgnoreCase);
      '#Else
      '      #error Unknown OS
      '#End If
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the OS with a >= version comparison. Used to guard APIs that were added in the given OS release.
    ''' </summary>
    ''' <param name="platform">Case-insensitive platform name. Examples: Browser, Linux, FreeBSD, Android, iOS, macOS, tvOS, watchOS, Windows.</param>
    ''' <param name="major">Major OS version number.</param>
    ''' <param name="minor">Minor OS version number (optional).</param>
    ''' <param name="build">Build OS version number (optional).</param>
    ''' <param name="revision">Revision OS version number (optional).</param>
    Public Shared Function IsOSPlatformVersionAtLeast(platform As String, major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0, Optional revision As Integer = 0) As Boolean
      Return IsOSPlatform(platform) AndAlso IsOSVersionAtLeast(major, minor, build, revision)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running as WASM in a Browser.
    ''' </summary>
    Public Shared Function IsBrowser() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on Linux.
    ''' </summary>
    Public Shared Function IsLinux() As Boolean
      Return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on FreeBSD.
    ''' </summary>
    Public Shared Function IsFreeBSD() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the FreeBSD version (returned by 'uname') with a >= version comparison. Used to guard APIs that were added in the given FreeBSD release.
    ''' </summary>
    Public Shared Function IsFreeBSDVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0, Optional revision As Integer = 0) As Boolean
      Return IsFreeBSD() AndAlso IsOSVersionAtLeast(major, minor, build, revision)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on Android.
    ''' </summary>
    Public Shared Function IsAndroid() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the Android version (returned by 'uname') with a >= version comparison. Used to guard APIs that were added in the given Android release.
    ''' </summary>
    Public Shared Function IsAndroidVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0, Optional revision As Integer = 0) As Boolean
      Return IsAndroid() AndAlso IsOSVersionAtLeast(major, minor, build, revision)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on iOS.
    ''' </summary>
    Public Shared Function IsIOS() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the iOS version (returned by 'libobjc.get_operatingSystemVersion') with a >= version comparison. Used to guard APIs that were added in the given iOS release.
    ''' </summary>
    Public Shared Function IsIOSVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0) As Boolean
      Return IsIOS() AndAlso IsOSVersionAtLeast(major, minor, build, 0)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on macOS.
    ''' </summary>
    Public Shared Function IsMacOS() As Boolean
      Return RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
    End Function

    ''' <summary>
    ''' Check for the macOS version (returned by 'libobjc.get_operatingSystemVersion') with a >= version comparison. Used to guard APIs that were added in the given macOS release.
    ''' </summary>
    Public Shared Function IsMacOSVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0) As Boolean
      Return IsMacOS() AndAlso IsOSVersionAtLeast(major, minor, build, 0)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on tvOS.
    ''' </summary>
    Public Shared Function IsTvOS() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the tvOS version (returned by 'libobjc.get_operatingSystemVersion') with a >= version comparison. Used to guard APIs that were added in the given tvOS release.
    ''' </summary>
    Public Shared Function IsTvOSVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0) As Boolean
      Return IsTvOS() AndAlso IsOSVersionAtLeast(major, minor, build, 0)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on watchOS.
    ''' </summary>
    Public Shared Function IsWatchOS() As Boolean
      Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Check for the watchOS version (returned by 'libobjc.get_operatingSystemVersion') with a >= version comparison. Used to guard APIs that were added in the given watchOS release.
    ''' </summary>
    Public Shared Function IsWatchOSVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0) As Boolean
      Return IsWatchOS() AndAlso IsOSVersionAtLeast(major, minor, build, 0)
    End Function

    ''' <summary>
    ''' Indicates whether the current application is running on Windows.
    ''' </summary>
    Public Shared Function IsWindows() As Boolean
      Return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    End Function


    ''' <summary>
    ''' Check for the Windows version (returned by 'RtlGetVersion') with a >= version comparison. Used to guard APIs that were added in the given Windows release.
    ''' </summary>
    Public Shared Function IsWindowsVersionAtLeast(major As Integer, Optional minor As Integer = 0, Optional build As Integer = 0, Optional revision As Integer = 0) As Boolean
      Return IsWindows() AndAlso IsOSVersionAtLeast(major, minor, build, revision)
    End Function

    Private Shared Function IsOSVersionAtLeast(major As Integer, minor As Integer, build As Integer, revision As Integer) As Boolean
      Dim current = Environment.OSVersion.Version
      If current.Major <> major Then
        Return current.Major > major
      End If
      If current.Minor <> minor Then
        Return current.Minor > minor
      End If
      If current.Build <> build Then
        Return current.Build > build
      End If
      ' it is unavailable on OSX and Environment.OSVersion.Version.Revision returns -1
      Return current.Revision >= revision OrElse (current.Revision = -1 AndAlso revision = 0)
    End Function

  End Class

End Namespace