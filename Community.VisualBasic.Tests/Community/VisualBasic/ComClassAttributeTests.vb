' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class ComClassAttributeTests

    <Fact>
    Public Sub Ctor_Default()
      Dim attribute As Community.VisualBasic.ComClassAttribute = New ComClassAttribute
      Assert.Null(attribute.ClassID)
      Assert.Null(attribute.InterfaceID)
      Assert.Null(attribute.EventID)
      Assert.[False](attribute.InterfaceShadows)
    End Sub

    <Theory>
    <InlineData(Nothing)>
    <InlineData("")>
    <InlineData("classID")>
    Public Sub Ctor_String(classID1 As String)
      Dim attribute As Community.VisualBasic.ComClassAttribute = New ComClassAttribute(classID1)
      Assert.Equal(classID1, attribute.ClassID)
      Assert.Null(attribute.InterfaceID)
      Assert.Null(attribute.EventID)
      Assert.[False](attribute.InterfaceShadows)
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing)>
    <InlineData("", "")>
    <InlineData("classID", "interfaceID")>
    Public Sub Ctor_String_String(classID1 As String, interfaceID1 As String)
      Dim attribute As Community.VisualBasic.ComClassAttribute = New ComClassAttribute(classID1, interfaceID1)
      Assert.Equal(classID1, attribute.ClassID)
      Assert.Equal(interfaceID1, attribute.InterfaceID)
      Assert.Null(attribute.EventID)
      Assert.[False](attribute.InterfaceShadows)
    End Sub

    <Theory>
    <InlineData(Nothing, Nothing, Nothing)>
    <InlineData("", "", "")>
    <InlineData("classID", "interfaceID", "eventID")>
    Public Sub Ctor_String_String_String(classID1 As String, interfaceID1 As String, eventID1 As String)
      Dim attribute As Community.VisualBasic.ComClassAttribute = New ComClassAttribute(classID1, interfaceID1, eventID1)
      Assert.Equal(classID1, attribute.ClassID)
      Assert.Equal(interfaceID1, attribute.InterfaceID)
      Assert.Equal(eventID1, attribute.EventID)
      Assert.[False](attribute.InterfaceShadows)
    End Sub

    <Theory>
    <InlineData(True)>
    <InlineData(False)>
    Public Sub InterfaceShadows_Set_GetReturnsExpected(value As Boolean)
      Dim attribute As Community.VisualBasic.ComClassAttribute = New ComClassAttribute() With
{
          .InterfaceShadows = value}
      Assert.Equal(value, attribute.InterfaceShadows)
    End Sub

  End Class

End Namespace