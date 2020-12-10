' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict Off ' NOTE: <----------------------------

Imports System.Collections.Generic
Imports System.Linq
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class InteractionTests

#If WINDOWS Then

    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNetCore))>
    Public Sub AppActivate_ProcessId()
      InvokeMissingMethod(Sub() Interaction.AppActivate(42))
    End Sub

    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNetCore))>
    Public Sub AppActivate_Title()
      InvokeMissingMethod(Sub() Interaction.AppActivate("MyProcess"))
    End Sub

#End If

    <Theory>
    <MemberData(NameOf(CallByName_TestData))>
    Public Sub CallByName(instance As Object, methodName As String, useCallType As CallType, args As Object(), getResult As Func(Of Object, Object), expected As Object)
      Assert.Equal(If(getResult Is Nothing, expected, Nothing), Interaction.CallByName(instance, methodName, useCallType, args))
      If getResult IsNot Nothing Then
        Assert.Equal(expected, getResult(instance)())
      End If
    End Sub

    <Theory>
    <MemberData(NameOf(CallByName_ArgumentException_TestData))>
    Public Sub CallByName_ArgumentException(instance As Object, methodName As String, useCallType As CallType, args As Object())
      Assert.Throws(Of ArgumentException)(Function() Interaction.CallByName(instance, methodName, useCallType, args))
    End Sub

    <Theory>
    <MemberData(NameOf(CallByName_MissingMemberException_TestData))>
    Public Sub CallByName_MissingMemberException(instance As Object, methodName As String, useCallType As CallType, args As Object())
      Assert.Throws(Of MissingMemberException)(Function() Interaction.CallByName(instance, methodName, useCallType, args))
    End Sub

    Public Shared Iterator Function CallByName_TestData() As IEnumerable(Of Object())
      Yield New Object() {New [Class], "Method", CallType.Method, New Object() {1, 2}, Nothing, 3}
      Yield New Object() {New [Class], "Method", CallType.[Get], New Object() {2, 3}, Nothing, 5}
      Yield New Object() {New [Class], "P", CallType.[Get], Array.Empty(Of Object)(), Nothing, 0}
      Yield New Object() {New [Class], "Item", CallType.[Get], New Object() {2}, Nothing, 2}
#If WINDOWS Then
      Yield New Object() {New [Class], "P", CallType.[Set], New Object() {3}, New Func(Of Object, Object)(Function(obj) CType(obj, [Class]).Value), 3}
      Yield New Object() {New [Class], "Item", CallType.[Let], New Object() {4, 5}, New Func(Of Object, Object)(Function(obj) CType(obj, [Class]).Value), 9}
#End If
    End Function

    Public Shared Iterator Function CallByName_ArgumentException_TestData() As IEnumerable(Of Object())
      Yield New Object() {Nothing, Nothing, CType(Nothing, CallType), Array.Empty(Of Object)()}
      Yield New Object() {New [Class], "Method", CType(Nothing, CallType), New Object() {1, 2}}
      Yield New Object() {New [Class], "Method", CType(Integer.MaxValue, CallType), New Object() {1, 2}}
    End Function

    Public Shared Iterator Function CallByName_MissingMemberException_TestData() As IEnumerable(Of Object())
      Yield New Object() {New [Class], "Method", CallType.Method, Array.Empty(Of Object)()}
      Yield New Object() {New [Class], "Q", CallType.[Get], Array.Empty(Of Object)()}
    End Function

    Private NotInheritable Class [Class]

      Public Value As Integer

      Public Shared Function Method(x As Integer, y As Integer) As Integer
        Return x + y
      End Function

      Public Property P As Integer
        Get
          Return Value
        End Get

        Set(value As Integer)
          Me.Value = value
        End Set
      End Property

      Default Public Property Item(index As Object) As Object
        Get
          Return Value + CInt(Fix(index))
        End Get
        Set(value As Object)
          Me.Value = CInt(Fix(value)) + CInt(Fix(index))
        End Set
      End Property

    End Class

    <Fact>
    Public Sub Choose()
      Dim x As Object() = {"Choice1", "Choice2", "Choice3", "Choice4", "Choice5", "Choice6"}
      Assert.Null(Interaction.Choose(5))
      Assert.Null(Interaction.Choose(0, x)) ' < 1
      Assert.Null(Interaction.Choose(x.Length + 1, x)) ' > UpperBound
      Assert.Equal(2, Interaction.Choose(2, 1, 2, 3))
      Assert.Equal("Choice3", Interaction.Choose(3, x(0), x(1), x(2)))
      For i As Integer = 1 To x.Length
        Assert.Equal(x(i - 1), Interaction.Choose(i, x))
      Next
    End Sub

    <Fact>
    Public Sub Command()
      Dim expected As String = Environment.CommandLine
      Dim actual = Interaction.Command()
      Assert.[False](String.IsNullOrEmpty(actual))
      Assert.EndsWith(actual, expected)
    End Sub

#If WINDOWS Then
    <Fact>
    Public Sub CreateObject()
      Assert.Throws(Of NullReferenceException)(Function() Interaction.CreateObject(Nothing))
      Assert.Throws(Of Exception)(Function() Interaction.CreateObject(""))
      ' Not tested: valid ProgID.
    End Sub
#End If

    <Theory>
    <MemberData(NameOf(IIf_TestData))>
    Public Sub IIf(expression As Boolean, truePart As Object, falsePart As Object, expected As Object)
      Assert.Equal(expected, Interaction.IIf(expression, truePart, falsePart))
    End Sub

    Public Shared Iterator Function IIf_TestData() As IEnumerable(Of Object())
      Yield New Object() {False, 1, Nothing, Nothing}
      Yield New Object() {True, 1, Nothing, 1}
      Yield New Object() {False, Nothing, 2, 2}
      Yield New Object() {True, Nothing, 2, Nothing}
      Yield New Object() {False, 3, "str", "str"}
      Yield New Object() {True, 3, "str", 3}
    End Function

#If WINDOWS Then
    ''<ActiveIssue("https://github.com/dotnet/runtime/issues/2139", TestRuntimes.Mono)>
    <Fact>
    Public Sub DeleteSetting()
      If Not PlatformDetection.IsInAppContainer Then
        Assert.Throws(Of ArgumentException)(Function() Interaction.DeleteSetting(AppName:="", Section:=Nothing, Key:=Nothing))
      Else
        Assert.Throws(Of PlatformNotSupportedException)(Function() Interaction.DeleteSetting(AppName:="", Section:=Nothing, Key:=Nothing))
      End If
      ' Not tested: valid arguments.
    End Sub
#End If

    <Fact>
    Public Sub Environ_Index()
      Dim pairs As (String, String)() = GetEnvironmentVariables()
      Dim n As Integer = Math.Min(pairs.Length, 255)

      ' Exception.ToString() called to verify message is constructed successfully.
      Dim temp = Assert.Throws(Of ArgumentException)(Function() Interaction.Environ(0)).ToString()
      temp = Assert.Throws(Of ArgumentException)(Function() Interaction.Environ(256)).ToString()

      For i As Integer = 0 To n - 1
        Dim pair As (String, String) = pairs(i)
        Assert.Equal($"{pair.Item1}={pair.Item2}", Interaction.Environ(i + 1))
      Next

      For i As Integer = n To 255 - 1
        Assert.Equal("", Interaction.Environ(i + 1))
      Next
    End Sub

    <Fact>
    Public Sub Environ_Name()
      Dim pairs As (String, String)() = GetEnvironmentVariables()
      ' Exception.ToString() called to verify message is constructed successfully.
      Dim temp = Assert.Throws(Of ArgumentException)(Function() Interaction.Environ("")).ToString()
      temp = Assert.Throws(Of ArgumentException)(Function() Interaction.Environ(" ")).ToString()
      For Each value In pairs
        Assert.Equal(value.Item2, Interaction.Environ(value.Item1))
      Next
      If pairs.Length > 0 Then
        Dim TupleTempVar = pairs(pairs.Length - 1)
        Dim key1 = TupleTempVar.Item1
        Dim value1 = TupleTempVar.Item2
        Assert.Equal(value1, Interaction.Environ("  " & key1))
        Assert.Equal(value1, Interaction.Environ(key1 & "  "))
        Assert.Null(Interaction.Environ(key1 & "z"))
      End If
    End Sub

    Private Shared Function GetEnvironmentVariables() As (String, String)()
      Dim pairs As Collections.Generic.List(Of (String, String)) = New List(Of (String, String))
      Dim vars As System.Collections.IDictionary = Environment.GetEnvironmentVariables()
      For Each key1 As System.Collections.IEnumerable In vars.Keys
        pairs.Add((CStr(key1), CStr(vars(key1))))
      Next

      Return pairs.OrderBy(Function(pair) pair.Item1).ToArray()
    End Function

#If WINDOWS Then
    '<ActiveIssue("https://github.com/dotnet/runtime/issues/2139", TestRuntimes.Mono)>
    <Fact>
    Public Sub GetAllSettings()
      If Not PlatformDetection.IsInAppContainer Then
        Assert.Throws(Of ArgumentException)(Function() Interaction.GetAllSettings(AppName:="", Section:=""))
      Else
        Assert.Throws(Of PlatformNotSupportedException)(Function() Interaction.GetAllSettings(AppName:="", Section:=""))
      End If
      ' Not tested: valid arguments.
    End Sub

    '<ActiveIssue("https://github.com/dotnet/runtime/issues/2139", TestRuntimes.Mono)>
    <Fact>
    Public Sub GetSetting()
      If Not PlatformDetection.IsInAppContainer Then
        Assert.Throws(Of ArgumentException)(Function() Interaction.GetSetting(AppName:="", Section:="", Key:="", [Default]:=""))
      Else
        Assert.Throws(Of PlatformNotSupportedException)(Function() Interaction.GetSetting(AppName:="", Section:="", Key:="", [Default]:=""))
      End If
      ' Not tested: valid arguments.
    End Sub
#End If

#If WINDOWS Then
    <Fact>
    Public Sub GetObject()
      Assert.Throws(Of Exception)(Function() Interaction.GetObject())
      Assert.Throws(Of Exception)(Function() Interaction.GetObject("", Nothing))
      Assert.Throws(Of Exception)(Function() Interaction.GetObject(Nothing, ""))
      Assert.Throws(Of Exception)(Function() Interaction.GetObject("", ""))
      ' Not tested: valid arguments.
    End Sub
#End If

#If WINDOWS Then
    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNetCore))>
    Public Sub InputBox()
      InvokeMissingMethod(Sub() Interaction.InputBox("Prompt", Title:="", DefaultResponse:="", XPos:=-1, YPos:=-1))
    End Sub

    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNetCore))>
    Public Sub MsgBox()
      InvokeMissingMethod(Sub() Interaction.MsgBox("Prompt", Buttons:=MsgBoxStyle.ApplicationModal, Title:=Nothing))
    End Sub
#End If

    <Theory>
    <InlineData(0, 1, 2, 1, " :0")>
    <InlineData(1, 1, 2, 1, "1:1")>
    <InlineData(2, 1, 2, 1, "2:2")>
    <InlineData(3, 1, 2, 1, "3: ")>
    <InlineData(10, 1, 9, 1, "10:  ")>
    <InlineData(-1, 0, 1, 1, "  :-1")>
    <InlineData(-50, 0, 1, 1, "  :-1")>
    <InlineData(0, 1, 100, 10, "   :  0")>
    <InlineData(1, 1, 100, 10, "  1: 10")>
    <InlineData(15, 1, 100, 10, " 11: 20")>
    <InlineData(25, 1, 100, 10, " 21: 30")>
    <InlineData(35, 1, 100, 10, " 31: 40")>
    <InlineData(45, 1, 100, 10, " 41: 50")>
    <InlineData(50, 40, 100, 10, " 50: 59")>
    <InlineData(120, 100, 200, 10, "120:129")>
    <InlineData(150, 100, 120, 10, "121:   ")>
    <InlineData(5001, 1, 10000, 100, " 5001: 5100")>
    <InlineData(1, 0, 1, Long.MaxValue, " 0: 1")>
    <InlineData(1, 0, Long.MaxValue - 1, Long.MaxValue, "                  0:9223372036854775806")>
    <InlineData(Long.MaxValue, 0, Long.MaxValue - 1, 1, "9223372036854775807:                   ")>
    <InlineData(Long.MaxValue - 1, 0, Long.MaxValue - 1, 1, "9223372036854775806:9223372036854775806")>
    Public Sub Partition(Number As Long, Start As Long, [Stop] As Long, Interval As Long, expected As String)
      Assert.Equal(expected, Interaction.Partition(Number, Start, [Stop], Interval))
    End Sub

    <Theory>
    <InlineData(0, -1, 100, 10)> ' Start < 0
    <InlineData(0, 100, 100, 10)> ' Stop <= Start
    <InlineData(0, 1, 100, 0)> ' Interval < 1
    Public Sub Partition_Invalid(Number As Long, Start As Long, [Stop] As Long, Interval As Long)
      Assert.Throws(Of ArgumentException)(Function() Interaction.Partition(Number, Start, [Stop], Interval))
    End Sub

    <Theory>
    <InlineData(1, 0, Long.MaxValue, 1)> ' Stop + 1
    <InlineData(1, 0, Long.MaxValue, Long.MaxValue)>
    <InlineData(2, 1, 2, Long.MaxValue)> ' Lower + Interval
    <InlineData(Long.MaxValue - 1, Long.MaxValue - 1, Long.MaxValue, 1)>
    Public Sub Partition_Overflow(Number As Long, Start As Long, [Stop] As Long, Interval As Long)
      Assert.Throws(Of OverflowException)(Function() Interaction.Partition(Number, Start, [Stop], Interval))
    End Sub

#If WINDOWS Then

    '<ActiveIssue("https://github.com/dotnet/runtime/issues/2139", TestRuntimes.Mono)>
    <Fact>
    Public Sub SaveSetting()
      If Not PlatformDetection.IsInAppContainer Then
        Assert.Throws(Of ArgumentException)(Function() Interaction.SaveSetting(AppName:="", Section:="", Key:="", Setting:=""))
      Else
        Assert.Throws(Of PlatformNotSupportedException)(Function() Interaction.SaveSetting(AppName:="", Section:="", Key:="", Setting:=""))
      End If
      ' Not tested: valid arguments.
    End Sub

#End If

#If WINDOWS Then
    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNetCore))>
    Public Sub Shell()
      InvokeMissingMethod(Sub() Interaction.Shell("MyPath", Style:=AppWinStyle.NormalFocus, Wait:=False, Timeout:=-1))
    End Sub
#End If

    <Theory>
    <InlineData(Nothing, Nothing)> ' empty
    <InlineData(New Object() {}, Nothing)> ' empty
    <InlineData(New Object() {False, "red", False, "green", False, "blue"}, Nothing)> ' none
    <InlineData(New Object() {True, "red", False, "green", False, "blue"}, "red")>
    <InlineData(New Object() {False, "red", True, "green", False, "blue"}, "green")>
    <InlineData(New Object() {False, "red", False, "green", True, "blue"}, "blue")>
    Public Sub Switch(VarExpr As Object(), expected As Object)
      Assert.Equal(expected, Interaction.Switch(VarExpr))
    End Sub

    <Fact>
    Public Sub Switch_Invalid()
      Assert.Throws(Of ArgumentException)(Function() Interaction.Switch(True, "a", False))
    End Sub

    ' Methods that rely on reflection of missing assembly.

    Private Shared Sub InvokeMissingMethod(action1 As Action)
      ' Exception.ToString() called to verify message is constructed successfully.
      Dim temp = Assert.Throws(Of PlatformNotSupportedException)(action1).ToString()
    End Sub

  End Class

End Namespace