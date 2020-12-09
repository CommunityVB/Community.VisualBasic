'' Licensed to the .NET Foundation under one or more agreements.
'' The .NET Foundation licenses this file to you under the MIT license.

'Option Compare Text
'Option Explicit On
'Option Infer Off
'Option Strict On

'Imports System.Collections.Generic
'Imports Community.VisualBasic.CompilerServices
'Imports Xunit

'Namespace Global.Community.VisualBasic.CompilerServices.Tests

'  Public Class UtilsTests

'    Public Shared Iterator Function CopyArray_TestData() As IEnumerable(Of Object())

'      Yield New Object() {New Integer() {1, 2}, New Integer(2) {}, New Integer() {1, 2, 0}}
'      Yield New Object() {New Integer() {1, 2}, New Integer(1) {}, New Integer() {1, 2}}
'      Yield New Object() {New Integer() {1, 2}, New Integer(0) {}, New Integer() {1}}

'      Yield New Object() {New Integer(,) {
'      {1, 2},
'      {3, 4}}, New Integer(1, 1) {}, New Integer(,) {
'      {1, 2},
'      {3, 4}}}

'      If PlatformDetection.IsNonZeroLowerBoundArraySupported Then
'        Dim array1 As Array = Array.CreateInstance(GetType(Integer), 1)
'        Dim array2 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1}, New Integer() {2})
'        Yield New Object() {array1, array2, array2}

'        Dim array3 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 1}, New Integer() {0, -1})
'        Dim array4 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 0}, New Integer() {0, 2})
'        Yield New Object() {array3, array4, array4}

'        Dim array5 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 2}, New Integer() {0, 0})
'        Dim array6 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 1}, New Integer() {0, 1})
'        Yield New Object() {array5, array6, array6}
'      End If

'    End Function

'    <Theory>
'    <MemberData(NameOf(CopyArray_TestData))>
'    Public Sub CopyArray_Valid_ReturnsExpected(source As Array, destination As Array, expected As Array)
'      Assert.Same(destination, Utils.CopyArray(source, destination))
'      Assert.Equal(expected, destination)
'    End Sub

'    <Fact>
'    Public Sub CopyArray_NullSourceArray_ReturnsDestination()
'      Dim destination As Array = New Object() {}
'      Assert.Same(destination, Utils.CopyArray(Nothing, destination))
'    End Sub

'    <Fact>
'    Public Sub CopyArray_EmptySourceArray_ReturnsDestination()
'      Dim destination As Array = New Object() {}
'      Assert.Same(destination, Utils.CopyArray(New Integer() {}, destination))
'      Assert.Null(Utils.CopyArray(New Integer() {}, Nothing))
'    End Sub

'    <Fact>
'    Public Sub CopyArray_NullDestinationArray_ThrowsNullReferenceException()
'      Assert.Throws(Of NullReferenceException)(Function() Utils.CopyArray(New Integer(0) {}, Nothing))
'    End Sub

'    <Fact>
'    Public Sub CopyArray_NonMatchingRanks_ThrowsInvalidCastException()
'      Assert.Throws(Of InvalidCastException)(Function() Utils.CopyArray(New Integer(0) {}, New Integer(0, 0) {}))
'    End Sub

'    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNonZeroLowerBoundArraySupported))>
'    Public Sub CopyArray_RankGreaterThanTwoAndNonMatchingBounds_ThrowsArrayTypeMismatchException()
'      Dim array1 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 2, 3}, New Integer() {2, 3, 4})
'      Dim array2 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 2, 3}, New Integer() {2, 4, 4})
'      Assert.Throws(Of ArrayTypeMismatchException)(Function() Utils.CopyArray(array1, array2))
'      Assert.Throws(Of ArrayTypeMismatchException)(Function() Utils.CopyArray(array2, array1))
'    End Sub

'    <ConditionalFact(GetType(PlatformDetection), NameOf(PlatformDetection.IsNonZeroLowerBoundArraySupported))>
'    Public Sub CopyArray_NonMatchingBounds_ThrowsArgumentOutOfRangeException()
'      Dim array1 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 2}, New Integer() {2, 3})
'      Dim array2 As Array = Array.CreateInstance(GetType(Integer), New Integer() {1, 2}, New Integer() {2, 4})
'      AssertExtensions.Throws(Of ArgumentOutOfRangeException)("sourceIndex", "srcIndex", Function() Utils.CopyArray(array1, array2))
'      AssertExtensions.Throws(Of ArgumentOutOfRangeException)("sourceIndex", "srcIndex", Function() Utils.CopyArray(array2, array1))
'    End Sub

'    <Fact>
'    Public Sub GetResourceString()
'      If System.Threading.Thread.CurrentThread.CurrentCulture.Name = "en-US" Then
'        Assert.Equal("Argument '42' is not a valid value.", Utils.GetResourceString("Argument_InvalidValue1", "42"))
'        Assert.Equal("Argument '42' is not a valid value.", Utils.GetResourceString(ResourceKey:="Argument_InvalidValue1", Args:={"42"}))
'        Assert.Equal("Application-defined or object-defined error.", Utils.GetResourceString("UnrecognizedResourceKey"))
'      End If
'    End Sub

'  End Class

'End Namespace