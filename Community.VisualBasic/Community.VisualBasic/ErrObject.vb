' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Imports Community.VisualBasic.CompilerServices
Imports Community.VisualBasic.CompilerServices.Utils

Imports System
Imports System.Runtime.InteropServices

Namespace Global.Community.VisualBasic

  Public NotInheritable Class ErrObject

    ' Error object private values
    Private m_curException As Exception
    Private m_curErl As Integer
    Private m_curNumber As Integer
    Private m_curDescription As String
    Private m_NumberIsSet As Boolean
    Private m_ClearOnCapture As Boolean
    Private m_DescriptionIsSet As Boolean

    Private m_curSource As String
    Private m_SourceIsSet As Boolean
    Private m_curHelpFile As String
    Private m_curHelpContext As Integer
    Private m_HelpFileIsSet As Boolean
    Private m_HelpContextIsSet As Boolean

    Friend Sub New()
      Clear() 'need to do this so the fields are set to Empty string, not Nothing
    End Sub

    '============================================================================
    ' ErrObject functions.
    '============================================================================
    Public ReadOnly Property Erl() As Integer
      Get
        Return m_curErl
      End Get
    End Property

    Public Property Number() As Integer
      Get
        If m_NumberIsSet Then
          Return m_curNumber
        End If

        If m_curException IsNot Nothing Then
          Me.Number = MapExceptionToNumber(m_curException)
          Return m_curNumber
        Else
          'The default case.  NOTE:  falling into the default does not "Set" the property.
          'We only get here if the Err object was previously cleared.
          Return 0
        End If
      End Get

      Set(Value As Integer)
        m_curNumber = MapErrorNumber(Value)
        m_NumberIsSet = True
      End Set
    End Property

    Public Property Source() As String
      Get
        'Return the current Source if we've already calculated it.
        If m_SourceIsSet Then
          Return m_curSource
        End If

        If m_curException IsNot Nothing Then
          Me.Source = m_curException.Source
          Return m_curSource
        Else
          'The default case.  NOTE:  falling into the default does not "Set" the property.
          'We only get here if the Err object was previously cleared.
          '
          Return ""
        End If
      End Get

      Set(Value As String)
        m_curSource = Value
        m_SourceIsSet = True
      End Set
    End Property

    ''' <summary>
    ''' Determines what the correct error description should be.
    ''' If we don't have an exception that we are responding to then
    ''' we don't do anything to the message.
    ''' If we do have an exception pending, we morph the description
    ''' to match the corresponding VB error.
    ''' We also special case HRESULT exceptions to map to a VB description
    ''' if we have one.
    ''' </summary>
    ''' <param name="Msg"></param>
    ''' <returns></returns>
    Private Function FilterDefaultMessage(Msg As String) As String
      Dim NewMsg As String

      'This is one of the default messages, 
      If m_curException Is Nothing Then
        'Leave message as is
        Return Msg
      End If

      Dim tmpNumber As Integer = Number

      If Msg Is Nothing OrElse Msg.Length = 0 Then
        Msg = SR.GetResourceString("ID" & CStr(tmpNumber))
      ElseIf System.String.CompareOrdinal("Exception from HRESULT: 0x", 0, Msg, 0, Math.Min(Msg.Length, 26)) = 0 Then
        NewMsg = SR.GetResourceString("ID" & CStr(m_curNumber))
        If NewMsg IsNot Nothing Then
          Msg = NewMsg
        End If
      End If

      Return Msg
    End Function

    Public Property Description() As String
      Get
        If m_DescriptionIsSet Then
          Return m_curDescription
        End If

        If m_curException IsNot Nothing Then
          Me.Description = FilterDefaultMessage(m_curException.Message)
          Return m_curDescription
        Else
          'The default case.  NOTE:  falling into the default does not "Set" the property.
          'We only get here if the Err object was previously cleared.
          Return ""
        End If
      End Get

      Set(Value As String)
        m_curDescription = Value
        m_DescriptionIsSet = True
      End Set
    End Property

    Public Property HelpFile() As String
      Get
        If m_HelpFileIsSet Then
          Return m_curHelpFile
        End If

        If m_curException IsNot Nothing Then
          ParseHelpLink(m_curException.HelpLink)
          Return m_curHelpFile
        Else
          'The default case.  NOTE:  falling into the default does not "Set" the property.
          'We only get here if the Err object was previously cleared.
          '
          Return ""
        End If
      End Get

      Set(Value As String)
        m_curHelpFile = Value

        m_HelpFileIsSet = True
      End Set
    End Property

    Private Function MakeHelpLink(HelpFile As String, HelpContext As Integer) As String
      Return HelpFile & "#" & CStr(HelpContext)
    End Function

    Private Sub ParseHelpLink(HelpLink As String)

      Diagnostics.Debug.Assert((Not m_HelpContextIsSet) OrElse (Not m_HelpFileIsSet), "Why is this getting called?")

      If HelpLink Is Nothing OrElse HelpLink.Length = 0 Then

        If Not m_HelpContextIsSet Then
          HelpContext = 0
        End If
        If Not m_HelpFileIsSet Then
          HelpFile = ""
        End If

      Else

        Dim iContext As Integer = m_InvariantCompareInfo.IndexOf(HelpLink, "#", Globalization.CompareOptions.Ordinal)

        If iContext <> -1 Then
          If Not m_HelpContextIsSet Then
            If iContext < HelpLink.Length Then
              HelpContext = CInt(HelpLink.Substring(iContext + 1))
            Else
              HelpContext = 0
            End If
          End If
          If Not m_HelpFileIsSet Then
            HelpFile = HelpLink.Substring(0, iContext)
          End If
        Else
          If Not m_HelpContextIsSet Then
            HelpContext = 0
          End If
          If Not m_HelpFileIsSet Then
            HelpFile = HelpLink
          End If
        End If

      End If

    End Sub

    Public Property HelpContext() As Integer
      Get
        If m_HelpContextIsSet Then
          Return m_curHelpContext
        End If

        If m_curException IsNot Nothing Then
          ParseHelpLink(m_curException.HelpLink)
          Return m_curHelpContext

        Else
          'The default case.  NOTE:  falling into the default does not "Set" the property.
          'We only get here if the Err object was previously cleared.
          '
          Return 0
        End If

        Return m_curHelpContext
      End Get

      Set(Value As Integer)
        m_curHelpContext = Value
        m_HelpContextIsSet = True
      End Set
    End Property

    Public Function GetException() As Exception
      Return m_curException
    End Function

    ''' <summary>
    ''' VB calls clear whenever it executes any type of Resume statement, Exit Sub, Exit funcion, exit Property, or
    ''' any On Error statement.
    ''' </summary>
    Public Sub Clear()
      m_curException = Nothing
      m_curNumber = 0
      m_curSource = ""
      m_curHelpFile = ""
      m_curHelpContext = 0
      m_SourceIsSet = False
      m_HelpFileIsSet = False
      m_HelpContextIsSet = False
      m_curDescription = ""
      m_curErl = 0
      m_NumberIsSet = False
      m_DescriptionIsSet = False
      m_ClearOnCapture = True
    End Sub

    ''' <summary>
    ''' This function is called when the Raise code command is executed
    ''' </summary>
    ''' <param name="Number">The error code being raised</param>
    ''' <param name="Source">If not supplied we take the name from the assembly</param>
    ''' <param name="Description">If not supplied, we try to look one up based on the error code being raised</param>
    ''' <param name="HelpFile"></param>
    ''' <param name="HelpContext"></param>
    Public Sub Raise(Number As Integer,
                     Optional Source As Object = Nothing,
                     Optional Description As Object = Nothing,
                     Optional HelpFile As Object = Nothing,
                     Optional HelpContext As Object = Nothing)

      If Number = 0 Then
        'This is only called by Raise, so Raise(0) should give the following exception
        Throw New ArgumentException(SR.Format(SR.Argument_InvalidValue1, "Number"))
      End If
      Me.Number = Number

      If Source IsNot Nothing Then
        Me.Source = CStr(Source)
      Else
        ' .NET Framework uses VBHost.GetWindowTitle() if available
        ' but the VBHost type is not accessible here.
        Dim FullName As String
        Dim CommaPos As Integer

        FullName = System.Reflection.Assembly.GetCallingAssembly().FullName
        CommaPos = InStr(FullName, ",")
        If CommaPos < 1 Then
          Me.Source = FullName
        Else
          Me.Source = Left(FullName, CommaPos - 1)
        End If
      End If

      If HelpFile IsNot Nothing Then
        Me.HelpFile = CStr(HelpFile)
      End If

      If HelpContext IsNot Nothing Then
        Me.HelpContext = CInt(HelpContext)
      End If

      If Description IsNot Nothing Then
        Me.Description = CStr(Description)
      ElseIf Not m_DescriptionIsSet Then
        'Set the Description here so the exception object contains the right message
        Me.Description = GetResourceString(CType(m_curNumber, vbErrors))
      End If

      Dim e As Exception
      e = MapNumberToException(m_curNumber, m_curDescription)
      e.Source = m_curSource
      e.HelpLink = MakeHelpLink(m_curHelpFile, m_curHelpContext)
      m_ClearOnCapture = False
      Throw e
    End Sub

    ReadOnly Property LastDllError() As Integer
      Get
        Return Marshal.GetLastWin32Error()
      End Get
    End Property

    Friend Sub SetUnmappedError(Number As Integer)
      Clear()
      Me.Number = Number
      m_ClearOnCapture = False
    End Sub

    'a function like this that can be used by the runtime to generate errors which will also do a clear would be nice.
    Friend Function CreateException(Number As Integer, Description As String) As System.Exception
      Clear()
      Me.Number = Number

      If Number = 0 Then
        'This is only called by Error xxxx, zero is not a valid exception number
        Throw New ArgumentException(SR.Format(SR.Argument_InvalidValue1, "Number"))
      End If

      Dim e As Exception = MapNumberToException(m_curNumber, Description)
      m_ClearOnCapture = False
      Return e
    End Function

    Friend Overloads Sub CaptureException(ex As Exception)
      'if we've already captured this exception, then we're done
      If ex IsNot m_curException Then
        If m_ClearOnCapture Then
          Clear()
        Else
          m_ClearOnCapture = True   'False only used once - set this flag back to the default
        End If
        m_curException = ex
      End If
    End Sub

    Friend Overloads Sub CaptureException(ex As Exception, lErl As Integer)
      CaptureException(ex)
      m_curErl = lErl  'This is the only place where the line number can be set
    End Sub

    Private Function MapExceptionToNumber(e As Exception) As Integer
      Diagnostics.Debug.Assert(e IsNot Nothing, "Exception shouldn't be Nothing")
      Dim typ As Type = e.GetType()

      If typ Is GetType(System.IndexOutOfRangeException) Then
        Return vbErrors.OutOfBounds
      ElseIf typ Is GetType(System.RankException) Then
        Return vbErrors.OutOfBounds
      ElseIf typ Is GetType(System.DivideByZeroException) Then
        Return vbErrors.DivByZero
      ElseIf typ Is GetType(System.OverflowException) Then
        Return vbErrors.Overflow
      ElseIf typ Is GetType(System.NotFiniteNumberException) Then
        Dim exNotFiniteNumber As NotFiniteNumberException = CType(e, NotFiniteNumberException)
        If exNotFiniteNumber.OffendingNumber = 0 Then
          Return vbErrors.DivByZero
        Else
          Return vbErrors.Overflow
        End If
      ElseIf typ Is GetType(System.NullReferenceException) Then
        Return vbErrors.ObjNotSet
      ElseIf TypeOf e Is System.AccessViolationException Then
        Return vbErrors.AccessViolation
      ElseIf typ Is GetType(System.InvalidCastException) Then
        Return vbErrors.TypeMismatch
      ElseIf typ Is GetType(System.NotSupportedException) Then
        Return vbErrors.TypeMismatch
      ElseIf typ Is GetType(System.Runtime.InteropServices.SEHException) Then
        Return vbErrors.DLLCallException
      ElseIf typ Is GetType(System.DllNotFoundException) Then
        Return vbErrors.FileNotFound
      ElseIf typ Is GetType(System.EntryPointNotFoundException) Then
        Return vbErrors.InvalidDllFunctionName
        '
        'Must fall after EntryPointNotFoundException because of inheritance
        '
      ElseIf typ Is GetType(System.TypeLoadException) Then
        Return vbErrors.CantCreateObject
      ElseIf typ Is GetType(System.OutOfMemoryException) Then
        Return vbErrors.OutOfMemory
      ElseIf typ Is GetType(System.FormatException) Then
        Return vbErrors.TypeMismatch
      ElseIf typ Is GetType(System.IO.DirectoryNotFoundException) Then
        Return vbErrors.PathNotFound
      ElseIf typ Is GetType(System.IO.IOException) Then
        Return vbErrors.IOError
      ElseIf typ Is GetType(System.IO.FileNotFoundException) Then
        Return vbErrors.FileNotFound
      ElseIf TypeOf e Is MissingMemberException Then
        Return vbErrors.OLENoPropOrMethod
      ElseIf TypeOf e Is Runtime.InteropServices.InvalidOleVariantTypeException Then
        Return vbErrors.InvalidTypeLibVariable
      Else
        Return vbErrors.IllegalFuncCall   'Generic error
      End If

    End Function

    Private Function MapNumberToException(Number As Integer,
Description As String) As System.Exception
      Return ExceptionUtils.BuildException(Number, Description, False)
    End Function

    Friend Function MapErrorNumber(Number As Integer) As Integer
      If Number > 65535 Then
        ' Number cannot be greater than 65535.
        Throw New ArgumentException(SR.Format(SR.Argument_InvalidValue1), NameOf(Number))
      End If

      If Number >= 0 Then
        Return Number
      End If

      'strip off top two bytes if FACILITY_CONTROL is set
      If (Number And SCODE_FACILITY) = FACILITY_CONTROL Then
        Return (Number And &HFFFFI)
      End If

      Return Number
    End Function

  End Class

End Namespace