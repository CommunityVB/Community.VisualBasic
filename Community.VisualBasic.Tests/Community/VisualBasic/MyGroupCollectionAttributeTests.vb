' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class MyGroupCollectionAttributeTests
    <Theory>
    <InlineData(Nothing, Nothing, Nothing, Nothing)>
    <InlineData("", "", "", "")>
    <InlineData("typeToCollect", "createInstanceMethodName", "disposeInstanceMethodName", "defaultInstanceAlias")>
    Public Sub Ctor_String_String_String(typeToCollect As String, createInstanceMethodName As String, disposeInstanceMethodName As String, defaultInstanceAlias As String)
      Dim attribute As Community.VisualBasic.MyGroupCollectionAttribute = New MyGroupCollectionAttribute(typeToCollect, createInstanceMethodName, disposeInstanceMethodName, defaultInstanceAlias)
      Assert.Equal(typeToCollect, attribute.MyGroupName)
      Assert.Equal(createInstanceMethodName, attribute.CreateMethod)
      Assert.Equal(disposeInstanceMethodName, attribute.DisposeMethod)
      Assert.Equal(defaultInstanceAlias, attribute.DefaultInstanceAlias)
    End Sub

  End Class

End Namespace