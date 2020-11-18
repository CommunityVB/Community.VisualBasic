'Option Explicit On
'Option Strict On
'Option Infer On

'Imports System.Runtime.InteropServices
''Imports Microsoft.VisualBasic.CompilerServices
''Imports Microsoft.VisualBasic.MyServices

'Namespace Global.Community.VisualBasic.Devices

'  Public Class ComputerInfo

'    ''' <summary>
'    ''' Gets the full operating system name.
'    ''' </summary>
'    ''' <value>A string contains the operating system name.</value>
'    Public ReadOnly Property OSFullName() As String
'      Get
'        Return RuntimeInformation.OSDescription
'      End Get
'    End Property

'    ''' <summary>
'    ''' Gets the platform OS name.
'    ''' </summary>
'    ''' <value>A string containing a Platform ID like "Win32NT", "Win32S", "Win32Windows". See PlatformID enum.</value>
'    ''' <exception cref="ExecutionEngineException">If cannot obtain the OS Version information.</exception>
'    Public ReadOnly Property OSPlatform() As String
'      Get
'        Return Environment.OSVersion.Platform.ToString
'      End Get
'    End Property

'    ''' <summary>
'    ''' Gets the current version number of the operating system.
'    ''' </summary>
'    ''' <value>A string contains the current version number of the operating system.</value>
'    ''' <exception cref="ExecutionEngineException">If cannot obtain the OS Version information.</exception>
'    Public ReadOnly Property OSVersion() As String
'      Get
'        Return Environment.OSVersion.Version.ToString
'      End Get
'    End Property

'  End Class

'  ''' <summary>
'  ''' A RAD object representing the server 'computer' for the web/Windows Services
'  ''' that serves as a discovery mechanism for finding principle abstractions in
'  ''' the system that you can code against
'  ''' </summary>
'  Public Class ServerComputer

'    Private _computerInfo As ComputerInfo 'Lazy initialized cache for ComputerInfo
'#If False Then
'    Private _fileIO As FileSystemProxy 'Lazy initialized cache for the FileSystem.
'    Private _network As Network 'Lazy initialized cache for the Network class.
'    Private _registryInstance As RegistryProxy 'Lazy initialized cache for the Registry class

'    Private Shared s_clock As Clock 'Lazy initialized cache for the Clock class.  SHARED because Clock behaves as a readonly singleton class
'#End If

'    'NOTE: The .Net design guidelines state that access to Instance members does not have to be thread-safe.  Access to Shared members does have to be thread-safe.
'    'Since My.Computer creates the instance of Computer in a thread-safe way, access to the Computer will necessarily be thread-safe.
'    'There is nothing to prevent a user from passing our computer object across threads or creating their own instance and then getting into trouble.
'    'But that is completely consistent with the rest of the FX design.  It is MY.* that is thread safe and leads to best practice access to these objects.
'    'If you dim them up yourself, you are responsible for managing the threading.

'#If False Then
'    ''' <summary>
'    ''' Returns the Clock object which contains the LocalTime and GMTTime.
'    ''' </summary>
'    Public ReadOnly Property Clock() As Clock
'      Get
'        If s_clock IsNot Nothing Then Return s_clock
'        s_clock = New Clock
'        Return s_clock
'      End Get
'    End Property

'    ''' <summary>
'    ''' Gets the object representing the file system of the computer.
'    ''' </summary>
'    ''' <value>A System.IO.FileSystem object.</value>
'    ''' <remarks>The instance returned by this property is lazy initialized and cached.</remarks>
'    Public ReadOnly Property FileSystem() As FileSystemProxy
'      Get
'        If _fileIO Is Nothing Then
'          _fileIO = New FileSystemProxy
'        End If
'        Return _fileIO
'      End Get
'    End Property
'    #End If

'    ''' <summary>
'    ''' Gets the object representing information about the computer's state
'    ''' </summary>
'    ''' <value>A Microsoft.VisualBasic.MyServices.ComputerInfo object.</value>
'    ''' <remarks>The instance returned by this property is lazy initialized and cached.</remarks>
'    Public ReadOnly Property Info() As ComputerInfo
'      Get
'        If _computerInfo Is Nothing Then
'          _computerInfo = New ComputerInfo
'        End If
'        Return _computerInfo
'      End Get
'    End Property

'#If False Then
'    ''' <summary>
'    ''' This property returns the Network object containing information about
'    ''' the network the machine is part of.
'    ''' </summary>
'    ''' <value>An instance of the Network.Network class.</value>
'    Public ReadOnly Property Network() As Network
'      Get
'        If _network IsNot Nothing Then Return _network
'        _network = New Network
'        Return _network
'      End Get
'    End Property
'#End If

'    ''' <summary>
'    ''' This property wraps the System.Environment.MachineName property
'    ''' in the .NET framework to return the name of the computer.
'    ''' </summary>
'    ''' <value>A string containing the name of the computer.</value>
'    Public ReadOnly Property Name() As String
'      Get
'        Return System.Environment.MachineName
'      End Get
'    End Property

'#If False Then
'    ''' <summary>
'    ''' Gets the Registry object, which can be used to read, set and
'    ''' enumerate keys and values in the system registry.
'    ''' </summary>
'    ''' <value>An instance of the RegistryProxy object</value>
'    Public ReadOnly Property Registry() As RegistryProxy
'      Get
'        If _registryInstance IsNot Nothing Then Return _registryInstance
'        _registryInstance = New RegistryProxy
'        Return _registryInstance
'      End Get
'    End Property
'#End If

'  End Class


'  Partial Public NotInheritable Class Computer
'    Inherits Devices.ServerComputer

'  End Class

'End Namespace