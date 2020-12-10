' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class HideModuleNameAttributeTests

    <Fact>
    Public Sub Ctor_Empty_Success()
      Dim tempVar As New HideModuleNameAttribute
    End Sub

  End Class

End Namespace