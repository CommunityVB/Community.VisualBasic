' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports Xunit
Imports Xunit.Sdk
Imports System.Linq

Namespace Global.Community

  Public Module AssertExtensions

    Private ReadOnly Property IsNetFramework As Boolean
      Get
        Return RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework")
      End Get
    End Property

    Public Sub Throws(Of T As Exception)(action1 As Action, expectedMessage As String)
      Assert.Equal(expectedMessage, Assert.Throws(Of T)(action1).Message)
    End Sub

    Public Sub ThrowsContains(Of T As Exception)(action1 As Action, expectedMessageContent As String)
      Assert.Contains(expectedMessageContent, Assert.Throws(Of T)(action1).Message)
    End Sub

    Public Function Throws(Of T As ArgumentException)(netCoreParamName As String, netFxParamName As String, action1 As Action) As T

      Dim exception1 As T = Assert.Throws(Of T)(action1)

      If netFxParamName Is Nothing AndAlso IsNetFramework Then
        ' Param name varies between .NET Framework versions -- skip checking it
        Return exception1
      End If

      Dim expectedParamName As String = If(IsNetFramework, netFxParamName, netCoreParamName)

      Assert.Equal(expectedParamName, exception1.ParamName)
      Return exception1

    End Function

    Public Sub Throws(Of T As ArgumentException)(netCoreParamName As String, netFxParamName As String, testCode As Func(Of Object))

      Dim exception1 As T = Assert.Throws(Of T)(testCode)

      If netFxParamName Is Nothing AndAlso IsNetFramework Then
        ' Param name varies between .NET Framework versions -- skip checking it
        Return
      End If

      Dim expectedParamName As String = If(IsNetFramework, netFxParamName, netCoreParamName)

      Assert.Equal(expectedParamName, exception1.ParamName)

    End Sub

    Public Function Throws(Of T As ArgumentException)(expectedParamName As String, action1 As Action) As T

      Dim exception1 As T = Assert.Throws(Of T)(action1)

      Assert.Equal(expectedParamName, exception1.ParamName)

      Return exception1

    End Function

    Public Function Throws(Of T As Exception)(action1 As Action) As T

      Dim exception1 As T = Assert.Throws(Of T)(action1)

      Return exception1
    End Function

    Public Function Throws(Of TException As Exception, TResult)(func1 As Func(Of TResult)) As TException

      Dim result As Object = Nothing
      Dim returned As Boolean = False
      Try
        Return Assert.Throws(Of TException)(Sub()
                                              result = func1
                                              returned = True
                                            End Sub)
      Catch ex As Exception When returned
        Dim resultStr As String
        If result Is Nothing Then
          resultStr = "(null)"
        Else
          resultStr = result.ToString()
          If GetType(TResult) Is GetType(String) Then
            resultStr = $"""{resultStr}"""
          End If
        End If

        Throw New AggregateException($"Result: {resultStr}", ex)
      End Try
    End Function

    Public Function Throws(Of T As ArgumentException)(expectedParamName As String, testCode As Func(Of Object)) As T

      Dim exception1 As T = Assert.Throws(Of T)(testCode)

      Assert.Equal(expectedParamName, exception1.ParamName)

      Return exception1

    End Function

    Public Async Function ThrowsAsync(Of T As ArgumentException)(expectedParamName As String, testCode As Func(Of Task)) As Task(Of T)

      Dim exception1 As T = Await Assert.ThrowsAsync(Of T)(testCode)

      Assert.Equal(expectedParamName, exception1.ParamName)

      Return exception1

    End Function

    Public Sub Throws(Of TNetCoreExceptionType As ArgumentException, TNetFxExceptionType As Exception)(expectedParamName As String, action1 As Action)
      If IsNetFramework Then
        ' Support cases where the .NET Core exception derives from ArgumentException
        ' but the .NET Framework exception is not.
        If GetType(ArgumentException).IsAssignableFrom(GetType(TNetFxExceptionType)) Then
          Dim exception1 As Exception = Assert.Throws(GetType(TNetFxExceptionType), action1)
          Assert.Equal(expectedParamName, CType(exception1, ArgumentException).ParamName)
        Else
          AssertExtensions.Throws(Of TNetFxExceptionType)(action1)
        End If
      Else
        AssertExtensions.Throws(Of TNetCoreExceptionType)(expectedParamName, action1)
      End If
    End Sub

    Public Function Throws(Of TNetCoreExceptionType As Exception, TNetFxExceptionType As Exception)(action1 As Action) As Exception
      Return Throws(GetType(TNetCoreExceptionType), GetType(TNetFxExceptionType), action1)
    End Function

    Public Function Throws(netCoreExceptionType As Type, netFxExceptionType As Type, action1 As Action) As Exception
      If IsNetFramework Then
        Return Assert.Throws(netFxExceptionType, action1)
      Else
        Return Assert.Throws(netCoreExceptionType, action1)
      End If
    End Function

    Public Sub Throws(Of TNetCoreExceptionType As ArgumentException, TNetFxExceptionType As ArgumentException)(netCoreParamName As String, netFxParamName As String, action1 As Action)
      If IsNetFramework Then
        Throws(Of TNetFxExceptionType)(netFxParamName, action1)
      Else
        Throws(Of TNetCoreExceptionType)(netCoreParamName, action1)
      End If
    End Sub

    Public Sub ThrowsAny(firstExceptionType As Type, secondExceptionType As Type, action1 As Action)
      ThrowsAnyInternal(action1, firstExceptionType, secondExceptionType)
    End Sub

    Private Sub ThrowsAnyInternal(action1 As Action, ParamArray exceptionTypes As Type())

      Try
        action1()
      Catch e As Exception
        Dim exceptionType As Type = e.[GetType]()
        If exceptionTypes.Any(Function(t1) t1.Equals(exceptionType)) Then
          Return
        End If

        Throw New XunitException($"Expected one of: ({String.Join(Of Type)(", ", exceptionTypes)}) -> Actual: ({exceptionType})")
      End Try

      Throw New XunitException($"Expected one of: ({String.Join(Of Type)(", ", exceptionTypes)}) -> Actual: No exception thrown")

    End Sub

    Public Sub ThrowsAny(Of TFirstExceptionType As Exception, TSecondExceptionType As Exception)(action1 As Action)
      ThrowsAnyInternal(action1, GetType(TFirstExceptionType), GetType(TSecondExceptionType))
    End Sub

    Public Sub ThrowsAny(Of TFirstExceptionType As Exception, TSecondExceptionType As Exception, TThirdExceptionType As Exception)(action1 As Action)
      ThrowsAnyInternal(action1, GetType(TFirstExceptionType), GetType(TSecondExceptionType), GetType(TThirdExceptionType))
    End Sub

    Public Sub ThrowsIf(Of T As Exception)(condition As Boolean, action1 As Action)
      If condition Then
        Assert.Throws(Of T)(action1)
      Else
        action1()
      End If
    End Sub

    Private Function AddOptionalUserMessage(message1 As String, userMessage As String) As String
      If userMessage Is Nothing Then
        Return message1
      Else
        Return $"{message1} {userMessage}"
      End If
    End Function

    ''' <summary>
    ''' Tests whether the specified string contains the specified substring
    ''' and throws an exception if the substring does not occur within the
    ''' test string or if either string or substring is null.
    ''' </summary>
    ''' <param name="value">
    ''' The string that is expected to contain <paramref name="substring"/>.
    ''' </param>
    ''' <param name="substring">
    ''' The string expected to occur within <paramref name="value"/>.
    ''' </param>
    Public Sub Contains(value As String, substring As String)
      Assert.NotNull(value)
      Assert.NotNull(substring)
      Assert.Contains(substring, value, StringComparison.Ordinal)
    End Sub

    ''' <summary>
    ''' Validate that a given value is greater than another value.
    ''' </summary>
    ''' <param name="actual">The value that should be greater than <paramref name="greaterThan"/>.</param>
    ''' <param name="greaterThan">The value that <paramref name="actual"/> should be greater than.</param>
    Public Sub GreaterThan(Of T As IComparable)(actual As T, greaterThan1 As T, Optional userMessage As String = Nothing)

      If actual Is Nothing Then
        Throw New XunitException(
            If(greaterThan1 Is Nothing, AddOptionalUserMessage($"Expected: <null> to be greater than <null>.", userMessage) _
                , AddOptionalUserMessage($"Expected: <null> to be greater than {greaterThan1}.", userMessage)))
      End If

      If actual.CompareTo(greaterThan1) <= 0 Then
        Throw New XunitException(AddOptionalUserMessage($"Expected: {actual} to be greater than {greaterThan1}", userMessage))
      End If

    End Sub

    ''' <summary>
    ''' Validate that a given value is less than another value.
    ''' </summary>
    ''' <param name="actual">The value that should be less than <paramref name="lessThan"/>.</param>
    ''' <param name="lessThan">The value that <paramref name="actual"/> should be less than.</param>
    Public Sub LessThan(Of T As IComparable)(actual As T, lessThan1 As T, Optional userMessage As String = Nothing)

      If actual Is Nothing Then
        If lessThan1 Is Nothing Then
          Throw New XunitException(AddOptionalUserMessage($"Expected: <null> to be less than <null>.", userMessage))
        Else
          ' Null is always less than non-null
          Return
        End If
      End If

      If actual.CompareTo(lessThan1) >= 0 Then
        Throw New XunitException(AddOptionalUserMessage($"Expected: {actual} to be less than {lessThan1}", userMessage))
      End If

    End Sub

    ''' <summary>
    ''' Validate that a given value is less than or equal to another value.
    ''' </summary>
    ''' <param name="actual">The value that should be less than or equal to <paramref name="lessThanOrEqualTo"/></param>
    ''' <param name="lessThanOrEqualTo">The value that <paramref name="actual"/> should be less than or equal to.</param>
    Public Sub LessThanOrEqualTo(Of T As IComparable)(actual As T, lessThanOrEqualTo1 As T, Optional userMessage As String = Nothing)

      ' null, by definition is always less than or equal to
      If actual Is Nothing Then
        Return
      End If

      If actual.CompareTo(lessThanOrEqualTo1) > 0 Then
        Throw New XunitException(AddOptionalUserMessage($"Expected: {actual} to be less than or equal to {lessThanOrEqualTo1}", userMessage))
      End If

    End Sub

    ''' <summary>
    ''' Validate that a given value is greater than or equal to another value.
    ''' </summary>
    ''' <param name="actual">The value that should be greater than or equal to <paramref name="greaterThanOrEqualTo"/></param>
    ''' <param name="greaterThanOrEqualTo">The value that <paramref name="actual"/> should be greater than or equal to.</param>
    Public Sub GreaterThanOrEqualTo(Of T As IComparable)(actual As T, greaterThanOrEqualTo1 As T, Optional userMessage As String = Nothing)

      ' null, by definition is always less than or equal to
      If actual Is Nothing Then
        If greaterThanOrEqualTo1 Is Nothing Then
          ' We're equal
          Return
        Else
          ' Null is always less than non-null
          Throw New XunitException(AddOptionalUserMessage($"Expected: <null> to be greater than or equal to <null>.", userMessage))
        End If
      End If

      If actual.CompareTo(greaterThanOrEqualTo1) < 0 Then
        Throw New XunitException(AddOptionalUserMessage($"Expected: {actual} to be greater than or equal to {greaterThanOrEqualTo1}", userMessage))
      End If

    End Sub

    ''' <summary>
    ''' Validates that the actual array is equal to the expected array. XUnit only displays the first 5 values
    ''' of each collection if the test fails. This doesn't display at what point or how the equality assertion failed.
    ''' </summary>
    ''' <param name="expected">The array that <paramref name="actual"/> should be equal to.</param>
    ''' <param name="actual"></param>
    Public Sub Equal(Of T As IEquatable(Of T))(expected As T(), actual As T())
      ' Use the SequenceEqual to compare the arrays for better performance. The default Assert.Equal method compares
      ' the arrays by boxing each element that is very slow for large arrays.
      ' NOTE: Worked in .NET 5 and .NET 6, but apparently no longer works in .NET 7?
      If Not expected.Equals(actual) Then 'If Not expected.AsSpan().SequenceEqual(actual.AsSpan()) Then
        Dim expectedString As String = String.Join(", ", expected)
        Dim actualString As String = String.Join(", ", actual)
        Throw New AssertActualExpectedException(expectedString, actualString, Nothing)
      End If
    End Sub

    ''' <summary>Validates that the two sets contains the same elements. XUnit doesn't display the full collections.</summary>
    Public Sub Equal(Of T)(expected As HashSet(Of T), actual As HashSet(Of T))
      If Not actual.SetEquals(expected) Then
        Throw New XunitException($"Expected: {String.Join(", ", expected)}{Environment.NewLine}Actual: {String.Join(", ", actual)}")
      End If
    End Sub

    Public Sub AtLeastOneEquals(Of T)(expected1 As T, expected2 As T, value As T)
      Dim comparer As EqualityComparer(Of T) = EqualityComparer(Of T).[Default]
      If Not (comparer.Equals(value, expected1) OrElse comparer.Equals(value, expected2)) Then
        Throw New XunitException($"Expected: {expected1} || {expected2}{Environment.NewLine}Actual: {value}")
      End If
    End Sub

    'Public Delegate Sub AssertThrowsActionReadOnly(Of T)(span As ReadOnlySpan(Of T))

    'Public Delegate Sub AssertThrowsAction(Of T)(span As Span(Of T))

    '' Cannot use standard Assert.Throws() when testing Span - Span and closures don't get along.
    'Public Function AssertThrows(Of E As Exception, T)(span As ReadOnlySpan(Of T), action1 As AssertThrowsActionReadOnly(Of T)) As E1

    '  Dim exception1 As Exception

    '  Try
    '    Action(span)
    '    exception1 = Nothing
    '  Catch ex As Exception
    '    exception1 = ex
    '  End Try

    '  Select Case exception1
    '    Case Nothing
    '      Throw New ThrowsException(GetType(E1))
    '    Case TypeOf exception1 Is E1
    '      Dim ex As E1 = CType(exception1, E1)
    '      If (ex.[GetType]() Is GetType(E1)) Then Exit Select
    '      Return ex
    '    Case Else
    '      Throw New ThrowsException(GetType(E1), exception1)
    '  End Select

    'End Function

    'Public Function AssertThrows(Of E As Exception, T)(span As Span(Of T), action1 As AssertThrowsAction(Of T)) As E1

    '  Dim exception1 As Exception

    '  Try
    '    Action(span)
    '    exception1 = Nothing
    '  Catch ex As Exception
    '    exception1 = ex
    '  End Try

    '  Select Case exception1
    '    Case Nothing
    '      Throw New ThrowsException(GetType(E1))
    '    Case TypeOf exception1 Is E1
    '      Dim ex As E1 = CType(exception1, E1)
    '      If (ex.[GetType]() Is GetType(E1)) Then Exit Select
    '      Return ex
    '    Case Else
    '      Throw New ThrowsException(GetType(E1), exception1)
    '  End Select

    'End Function

    'Public Function Throws(Of E As ArgumentException, T)(expectedParamName As String, span As ReadOnlySpan(Of T), action1 As AssertThrowsActionReadOnly(Of T)) As E1
    '  Dim exception1 As E1 = AssertThrows(Of E, T)(span, action1)
    '  Assert.Equal(expectedParamName, exception1.ParamName)
    '  Return exception1
    'End Function

    'Public Function Throws(Of E As ArgumentException, T)(expectedParamName As String, span As Span(Of T), action1 As AssertThrowsAction(Of T)) As E1
    '  Dim exception1 As E1 = AssertThrows(Of E, T)(span, action1)
    '  Assert.Equal(expectedParamName, exception1.ParamName)
    '  Return exception1
    'End Function

  End Module

End Namespace