' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.Collections.Generic
Imports System.Linq
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class VBMathTests

    ' Reset seed, without access to implementation internals.

#Disable Warning CA1822 ' Mark members as static

    Private Sub ResetSeed()
      Dim x As Single = BitConverter.ToSingle(BitConverter.GetBytes(&H8076F5C1UI), 0)
      VBMath.Rnd(x)

      Dim startupSeed As Single = CSng(&H50000)
      Dim period As Single = 16777216.0F
      Dim currentSeed As Single = VBMath.Rnd(0.0F) * period
      Assert.Equal(startupSeed, currentSeed)
    End Sub

#Enable Warning CA1822 ' Mark members as static

    <Fact>
    Public Sub Rnd_0_RepeatsPreviousNumber()
      ResetSeed()

      Dim expected As Single
      Dim actual As Single
      For Each i In Enumerable.Range(0, 5)
        expected = VBMath.Rnd()
        actual = VBMath.Rnd(0.0F)
        Assert.Equal(expected, actual)
        actual = VBMath.Rnd(0.0F)
        Assert.Equal(expected, actual)
        actual = VBMath.Rnd(0.0F)
        Assert.Equal(expected, actual)
      Next
    End Sub

    <Fact>
    Public Sub Rnd_0_PreviousNumberInSequenceIsNotAlwaysThePreviouslyGeneratedNumber()
      ResetSeed()

      Dim previouslyGeneratedNumber As Single = VBMath.Rnd()
      VBMath.Randomize(42.0F)
      Dim actual As Single = VBMath.Rnd(0.0F)
      Dim expected As Single = 0.251064479F
      Assert.Equal(expected, actual)
      Assert.NotEqual(previouslyGeneratedNumber, actual)
    End Sub

    <Theory>
    <InlineData(0.5F)>
    <InlineData(1.0F)>
    <InlineData(824000.3F)>
    Public Sub Rnd_Positive_EqualsRndUnit(positive As Single)
      ResetSeed()
      Dim actual As Single = VBMath.Rnd(positive)
      ResetSeed()
      Dim expected As Single = VBMath.Rnd()
      Assert.Equal(expected, actual)
    End Sub

    <Fact>
    Public Sub Rnd_Unit_ReturnsExpectedSequence()

      ResetSeed()

      Dim actual1 As Single = VBMath.Rnd()
      Dim expected1 As Single = 0.7055475F
      Assert.Equal(expected1, actual1)

      Dim actual2 As Single = VBMath.Rnd()
      Dim expected2 As Single = 0.533424F
      Assert.Equal(expected2, actual2)

      Dim actual3 As Single = VBMath.Rnd()
      Dim expected3 As Single = 0.5795186F
      Assert.Equal(expected3, actual3)

      Dim actual4 As Single = VBMath.Rnd()
      Dim expected4 As Single = 0.289562464F
      Assert.Equal(expected4, actual4)

      Dim actual5 As Single = VBMath.Rnd()
      Dim expected5 As Single = 0.301948F
      Assert.Equal(expected5, actual5)

    End Sub

    <Theory>
    <InlineData(-25820.53F)>
    <InlineData(-66.0F)>
    <InlineData(-2.00008F)>
    Public Sub Rnd_Negative_DoesNotDependUponPreviousState(number As Single)

      ResetSeed()

      Dim actual1 As Single = VBMath.Rnd(number)
      VBMath.Rnd()
      Dim actual2 As Single = VBMath.Rnd(number)
      VBMath.Rnd()
      VBMath.Rnd()
      Dim actual3 As Single = VBMath.Rnd(number)
      VBMath.Rnd()
      VBMath.Rnd()
      VBMath.Rnd()
      VBMath.Rnd()
      Dim actual4 As Single = VBMath.Rnd(number)
      Assert.Equal(actual2, actual1)
      Assert.Equal(actual3, actual1)
      Assert.Equal(actual4, actual1)

    End Sub

    <Theory>
    <InlineData(-1.2345E+38F, 0.03195423F)>
    <InlineData(-8.0E+9F, 0.7537669F)>
    <InlineData(-79.4F, 0.828889251F)>
    <InlineData(-44.48306F, 0.5418172F)>
    Public Sub Rnd_Negative_ReturnsExpected(input As Single, expected As Single)
      ResetSeed()
      Dim actual As Single = VBMath.Rnd(input)
      Assert.Equal(expected, actual)
    End Sub

    Public Shared Iterator Function Randomize_TestData() As IEnumerable(Of Object())
      Yield New Object() {-44.8}
      Yield New Object() {683000000000.0}
      Yield New Object() {45782930.2389523}
      Yield New Object() {3.2452834095829303E+19}
    End Function

    <Theory>
    <MemberData(NameOf(Randomize_TestData))>
    Public Sub Randomize_IsIdempotent(seed As Double)

      ResetSeed()

      VBMath.Randomize(seed)
      Dim actualState1 As Single = VBMath.Rnd(0.0F)
      VBMath.Randomize(seed)
      Dim actualState2 As Single = VBMath.Rnd(0.0F)
      Assert.Equal(actualState1, actualState2)

    End Sub

    <Theory>
    <MemberData(NameOf(Randomize_TestData))>
    Public Sub Randomize_UseExistingStateWhenGeneratingNewState(seed As Double)

      ResetSeed()

      VBMath.Randomize(seed)
      Dim actualState1 As Single = VBMath.Rnd(0.0F)

      VBMath.Rnd()
      VBMath.Randomize(seed)
      Dim actualState2 As Single = VBMath.Rnd(0.0F)

      Assert.NotEqual(actualState1, actualState2)

    End Sub

    <Fact>
    Public Sub Randomize_SetsExpectedState()

      ResetSeed()

      If Not BitConverter.IsLittleEndian Then
        Throw New NotImplementedException("big endian tests")
      End If

      VBMath.Randomize(-2.0E+30)
      Assert.Equal(-0.0297851563F, VBMath.Rnd(0.0F))
      VBMath.Randomize(-0.003356)
      Assert.Equal(-0.244613647F, VBMath.Rnd(0.0F))
      VBMath.Randomize(0.0)
      Assert.Equal(-1.0F, VBMath.Rnd(0.0F))
      VBMath.Randomize(10.12345678901)
      Assert.Equal(-0.503646851F, VBMath.Rnd(0.0F))
      VBMath.Randomize(3.5356E+99)
      Assert.Equal(-0.4624939F, VBMath.Rnd(0.0F))

    End Sub

  End Class

End Namespace