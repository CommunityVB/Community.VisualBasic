Option Explicit On
Option Strict On
Option Infer On

Imports Community.VisualBasic
Imports System.Text

Namespace My

  <HideModuleName()>
  Friend Module MyDotStuff

    'Friend Property Application As New Devices.Application
    Friend Property Computer As New Devices.Computer
    Friend Property User As New Community.VisualBasic.ApplicationServices.User

  End Module

End Namespace

Namespace Global.Microsoft.VisualBasic.CompilerServices

  <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)>
  Friend NotInheritable Class StringType

    ' Prevent creation.
    Private Sub New()
    End Sub

    Friend Shared Sub MidStmtStr(ByRef sDest As String, StartPosition As Integer, MaxInsertLength As Integer, sInsert As String)
      Dim DestLength As Integer
      Dim InsertLength As Integer
      Dim EndSegmentLength As Integer

      If sDest Is Nothing Then
        'DestLength = 0
      Else
        DestLength = sDest.Length
      End If

      If sInsert Is Nothing Then
        'InsertLength = 0
      Else
        InsertLength = sInsert.Length
      End If

      'Zero base the index
      StartPosition -= 1

      If StartPosition < 0 OrElse StartPosition >= DestLength Then
        Throw New ArgumentException(SR.Format(SR.Argument_InvalidValue1, "Start"))
      End If

      If MaxInsertLength < 0 Then
        Throw New ArgumentException(SR.Format(SR.Argument_InvalidValue1, "Length"))
      End If

      '  first, limit the length of the source string
      '  to lenChange

      If (InsertLength > MaxInsertLength) Then
        InsertLength = MaxInsertLength
      End If

      '  second, limit the length to the available space
      '  in the destination string

      If (InsertLength > DestLength - StartPosition) Then
        InsertLength = DestLength - StartPosition
      End If

      If InsertLength = 0 Then
        'Destination string remains unchanged
        Exit Sub
      End If

      'This looks a bit complex for removing and inserting strings
      'but when manipulating long strings, it should provide
      'better performance because of fewer memcpys

      Dim sb As StringBuilder

      sb = New StringBuilder(DestLength)

      If StartPosition > 0 Then
        'Append first part of destination string
        sb.Append(sDest, 0, StartPosition)
      End If

      'Append InsertString
      sb.Append(sInsert, 0, InsertLength)
      EndSegmentLength = DestLength - (StartPosition + InsertLength)

      If EndSegmentLength > 0 Then
        'Append remainder of destination string
        sb.Append(sDest, StartPosition + InsertLength, EndSegmentLength)
      End If

      sDest = sb.ToString()
    End Sub

  End Class

End Namespace