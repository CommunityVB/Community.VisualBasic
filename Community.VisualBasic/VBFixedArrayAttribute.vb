' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Imports System

Imports Community.VisualBasic

Namespace Global.Community.VisualBasic

  ' -------------------------------------------------------------------
  ' VBFixedArray is used by the runtime to determine 
  ' if the array should be written/read without the array descriptor.
  ' -------------------------------------------------------------------
  <AttributeUsage(AttributeTargets.Field, Inherited:=False, AllowMultiple:=False)>
  Public NotInheritable Class VBFixedArrayAttribute
    Inherits Attribute

    Friend FirstBound As Integer
    Friend SecondBound As Integer

    Public ReadOnly Property Bounds() As Integer()
      Get
        If SecondBound = -1 Then
          Return New Integer() {FirstBound}
        Else
          Return New Integer() {FirstBound, SecondBound}
        End If
      End Get
    End Property

    Public ReadOnly Property Length() As Integer
      Get
        If SecondBound = -1 Then
          Return (FirstBound + 1)
        Else
          Return (FirstBound + 1) * (SecondBound + 1)
        End If
      End Get
    End Property

    Public Sub New(UpperBound1 As Integer)
      If UpperBound1 < 0 Then
        Throw New ArgumentException(SR.Invalid_VBFixedArray)
      End If

      FirstBound = UpperBound1
      SecondBound = -1

    End Sub

    Public Sub New(UpperBound1 As Integer, UpperBound2 As Integer)
      If UpperBound1 < 0 OrElse UpperBound2 < 0 Then
        Throw New ArgumentException(SR.Invalid_VBFixedArray)
      End If

      FirstBound = UpperBound1
      SecondBound = UpperBound2

    End Sub
  End Class

End Namespace