' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections.Generic
Imports System.Globalization
Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class DateAndTimeTests

    <Theory>
    <MemberData(NameOf(DateAdd_DateInterval_TestData))>
    Public Sub DateAdd_DateInterval(interval As DateInterval, number As Double, dateValue As DateTime, expected As DateTime)
      Assert.Equal(expected, DateAndTime.DateAdd(interval, number, dateValue))
    End Sub

    <Theory>
    <MemberData(NameOf(DateAdd_DateInterval_ArgumentOutOfRangeException_TestData))>
    Public Sub DateAdd_DateInterval_ArgumentOutOfRangeException(interval As DateInterval, number As Double, dateValue As DateTime)
      Assert.Throws(Of ArgumentOutOfRangeException)(Function() DateAndTime.DateAdd(interval, number, dateValue))
    End Sub

    Public Shared Iterator Function DateAdd_DateInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow
      Dim calendar1 As System.Globalization.Calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar

      Yield New Object() {DateInterval.Year, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Year, 0.0, now, now}
      Yield New Object() {DateInterval.Year, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Year, 2.0, DateTime.MinValue, calendar1.AddYears(DateTime.MinValue, 2)}
      Yield New Object() {DateInterval.Year, 2.0, now, calendar1.AddYears(now, 2)}
      Yield New Object() {DateInterval.Year, -2.0, now, calendar1.AddYears(now, -2)}
      Yield New Object() {DateInterval.Year, -2.0, DateTime.MaxValue, calendar1.AddYears(DateTime.MaxValue, -2)}

      Yield New Object() {DateInterval.Quarter, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Quarter, 0.0, now, now}
      Yield New Object() {DateInterval.Quarter, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Quarter, 2.0, DateTime.MinValue, calendar1.AddMonths(DateTime.MinValue, 6)}
      Yield New Object() {DateInterval.Quarter, 2.0, now, calendar1.AddMonths(now, 6)}
      Yield New Object() {DateInterval.Quarter, -2.0, now, calendar1.AddMonths(now, -6)}
      Yield New Object() {DateInterval.Quarter, -2.0, DateTime.MaxValue, calendar1.AddMonths(DateTime.MaxValue, -6)}

      Yield New Object() {DateInterval.Month, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Month, 0.0, now, now}
      Yield New Object() {DateInterval.Month, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Month, 2.0, DateTime.MinValue, calendar1.AddMonths(DateTime.MinValue, 2)}
      Yield New Object() {DateInterval.Month, 2.0, now, calendar1.AddMonths(now, 2)}
      Yield New Object() {DateInterval.Month, -2.0, now, calendar1.AddMonths(now, -2)}
      Yield New Object() {DateInterval.Month, -2.0, DateTime.MaxValue, calendar1.AddMonths(DateTime.MaxValue, -2)}

      Yield New Object() {DateInterval.DayOfYear, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.DayOfYear, 0.0, now, now}
      Yield New Object() {DateInterval.DayOfYear, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.DayOfYear, 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {DateInterval.DayOfYear, 2.0, now, now.AddDays(2)}
      Yield New Object() {DateInterval.DayOfYear, -2.0, now, now.AddDays(-2)}
      Yield New Object() {DateInterval.DayOfYear, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {DateInterval.Day, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Day, 0.0, now, now}
      Yield New Object() {DateInterval.Day, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Day, 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {DateInterval.Day, 2.0, now, now.AddDays(2)}
      Yield New Object() {DateInterval.Day, -2.0, now, now.AddDays(-2)}
      Yield New Object() {DateInterval.Day, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {DateInterval.WeekOfYear, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.WeekOfYear, 0.0, now, now}
      Yield New Object() {DateInterval.WeekOfYear, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.WeekOfYear, 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(14)}
      Yield New Object() {DateInterval.WeekOfYear, 2.0, now, now.AddDays(14)}
      Yield New Object() {DateInterval.WeekOfYear, -2.0, now, now.AddDays(-14)}
      Yield New Object() {DateInterval.WeekOfYear, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-14)}

      Yield New Object() {DateInterval.Weekday, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Weekday, 0.0, now, now}
      Yield New Object() {DateInterval.Weekday, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Weekday, 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {DateInterval.Weekday, 2.0, now, now.AddDays(2)}
      Yield New Object() {DateInterval.Weekday, -2.0, now, now.AddDays(-2)}
      Yield New Object() {DateInterval.Weekday, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {DateInterval.Hour, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Hour, 0.0, now, now}
      Yield New Object() {DateInterval.Hour, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Hour, 2.0, DateTime.MinValue, DateTime.MinValue.AddHours(2)}
      Yield New Object() {DateInterval.Hour, 2.0, now, now.AddHours(2)}
      Yield New Object() {DateInterval.Hour, -2.0, now, now.AddHours(-2)}
      Yield New Object() {DateInterval.Hour, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddHours(-2)}

      Yield New Object() {DateInterval.Minute, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Minute, 0.0, now, now}
      Yield New Object() {DateInterval.Minute, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Minute, 2.0, DateTime.MinValue, DateTime.MinValue.AddMinutes(2)}
      Yield New Object() {DateInterval.Minute, 2.0, now, now.AddMinutes(2)}
      Yield New Object() {DateInterval.Minute, -2.0, now, now.AddMinutes(-2)}
      Yield New Object() {DateInterval.Minute, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddMinutes(-2)}

      Yield New Object() {DateInterval.Second, 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {DateInterval.Second, 0.0, now, now}
      Yield New Object() {DateInterval.Second, 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {DateInterval.Second, 2.0, DateTime.MinValue, DateTime.MinValue.AddSeconds(2)}
      Yield New Object() {DateInterval.Second, 2.0, now, now.AddSeconds(2)}
      Yield New Object() {DateInterval.Second, -2.0, now, now.AddSeconds(-2)}
      Yield New Object() {DateInterval.Second, -2.0, DateTime.MaxValue, DateTime.MaxValue.AddSeconds(-2)}
    End Function

    Public Shared Iterator Function DateAdd_DateInterval_ArgumentOutOfRangeException_TestData() As IEnumerable(Of Object())
      Yield New Object() {DateInterval.Year, 2.0, DateTime.MaxValue}
      Yield New Object() {DateInterval.Year, -2.0, DateTime.MinValue}

      Yield New Object() {DateInterval.Day, 2.0, DateTime.MaxValue}
      Yield New Object() {DateInterval.Day, -2.0, DateTime.MinValue}

      Yield New Object() {DateInterval.Second, 2.0, DateTime.MaxValue}
      Yield New Object() {DateInterval.Second, -2.0, DateTime.MinValue}
    End Function

    <Theory>
    <MemberData(NameOf(DateAdd_StringInterval_TestData))>
    Public Sub DateAdd_StringInterval(interval As String, number As Double, dateValue As Object, expected As DateTime)
      Assert.Equal(expected, DateAndTime.DateAdd(interval, number, dateValue))
    End Sub

    <Theory>
    <MemberData(NameOf(DateAdd_StringInterval_ArgumentOutOfRangeException_TestData))>
    Public Sub DateAdd_StringInterval_ArgumentOutOfRangeException(interval As String, number As Double, dateValue As Object)
      Assert.Throws(Of ArgumentOutOfRangeException)(Function() DateAndTime.DateAdd(interval, number, dateValue))
    End Sub

    Public Shared Iterator Function DateAdd_StringInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow
      Dim calendar1 As System.Globalization.Calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar

      Yield New Object() {"YYYY", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"YYYY", 0.0, now, now}
      Yield New Object() {"YYYY", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"YYYY", 2.0, DateTime.MinValue, calendar1.AddYears(DateTime.MinValue, 2)}
      Yield New Object() {"YYYY", 2.0, now, calendar1.AddYears(now, 2)}
      Yield New Object() {"YYYY", -2.0, now, calendar1.AddYears(now, -2)}
      Yield New Object() {"YYYY", -2.0, DateTime.MaxValue, calendar1.AddYears(DateTime.MaxValue, -2)}

      Yield New Object() {"Q", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"Q", 0.0, now, now}
      Yield New Object() {"Q", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"Q", 2.0, DateTime.MinValue, calendar1.AddMonths(DateTime.MinValue, 6)}
      Yield New Object() {"Q", 2.0, now, calendar1.AddMonths(now, 6)}
      Yield New Object() {"Q", -2.0, now, calendar1.AddMonths(now, -6)}
      Yield New Object() {"Q", -2.0, DateTime.MaxValue, calendar1.AddMonths(DateTime.MaxValue, -6)}

      Yield New Object() {"M", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"M", 0.0, now, now}
      Yield New Object() {"M", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"M", 2.0, DateTime.MinValue, calendar1.AddMonths(DateTime.MinValue, 2)}
      Yield New Object() {"M", 2.0, now, calendar1.AddMonths(now, 2)}
      Yield New Object() {"M", -2.0, now, calendar1.AddMonths(now, -2)}
      Yield New Object() {"M", -2.0, DateTime.MaxValue, calendar1.AddMonths(DateTime.MaxValue, -2)}

      Yield New Object() {"Y", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"Y", 0.0, now, now}
      Yield New Object() {"Y", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"Y", 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {"Y", 2.0, now, now.AddDays(2)}
      Yield New Object() {"Y", -2.0, now, now.AddDays(-2)}
      Yield New Object() {"Y", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {"D", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"D", 0.0, now, now}
      Yield New Object() {"D", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"D", 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {"D", 2.0, now, now.AddDays(2)}
      Yield New Object() {"D", -2.0, now, now.AddDays(-2)}
      Yield New Object() {"D", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {"WW", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"WW", 0.0, now, now}
      Yield New Object() {"WW", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"WW", 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(14)}
      Yield New Object() {"WW", 2.0, now, now.AddDays(14)}
      Yield New Object() {"WW", -2.0, now, now.AddDays(-14)}
      Yield New Object() {"WW", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-14)}

      Yield New Object() {"W", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"W", 0.0, now, now}
      Yield New Object() {"W", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"W", 2.0, DateTime.MinValue, DateTime.MinValue.AddDays(2)}
      Yield New Object() {"W", 2.0, now, now.AddDays(2)}
      Yield New Object() {"W", -2.0, now, now.AddDays(-2)}
      Yield New Object() {"W", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddDays(-2)}

      Yield New Object() {"H", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"H", 0.0, now, now}
      Yield New Object() {"H", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"H", 2.0, DateTime.MinValue, DateTime.MinValue.AddHours(2)}
      Yield New Object() {"H", 2.0, now, now.AddHours(2)}
      Yield New Object() {"H", -2.0, now, now.AddHours(-2)}
      Yield New Object() {"H", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddHours(-2)}

      Yield New Object() {"N", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"N", 0.0, now, now}
      Yield New Object() {"N", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"N", 2.0, DateTime.MinValue, DateTime.MinValue.AddMinutes(2)}
      Yield New Object() {"N", 2.0, now, now.AddMinutes(2)}
      Yield New Object() {"N", -2.0, now, now.AddMinutes(-2)}
      Yield New Object() {"N", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddMinutes(-2)}

      Yield New Object() {"S", 0.0, DateTime.MinValue, DateTime.MinValue}
      Yield New Object() {"S", 0.0, now, now}
      Yield New Object() {"S", 0.0, DateTime.MaxValue, DateTime.MaxValue}
      Yield New Object() {"S", 2.0, DateTime.MinValue, DateTime.MinValue.AddSeconds(2)}
      Yield New Object() {"S", 2.0, now, now.AddSeconds(2)}
      Yield New Object() {"S", -2.0, now, now.AddSeconds(-2)}
      Yield New Object() {"S", -2.0, DateTime.MaxValue, DateTime.MaxValue.AddSeconds(-2)}
    End Function

    Public Shared Iterator Function DateAdd_StringInterval_ArgumentOutOfRangeException_TestData() As IEnumerable(Of Object())
      Yield New Object() {"YYYY", 2.0, DateTime.MaxValue}
      Yield New Object() {"YYYY", -2.0, DateTime.MinValue}

      Yield New Object() {"D", 2.0, DateTime.MaxValue}
      Yield New Object() {"D", -2.0, DateTime.MinValue}

      Yield New Object() {"S", 2.0, DateTime.MaxValue}
      Yield New Object() {"S", -2.0, DateTime.MinValue}
    End Function

    <Theory>
    <MemberData(NameOf(DateDiff_DateInterval_TestData))>
    Public Sub DateDiff_DateInterval(interval As DateInterval, dateTime1 As DateTime, dateTime2 As DateTime, expected As Long)
      Assert.Equal(expected, DateAndTime.DateDiff(interval, dateTime1, dateTime2))
    End Sub

    Public Shared Iterator Function DateDiff_DateInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow

      Yield New Object() {DateInterval.Year, DateTime.MinValue, DateTime.MinValue, 0}
      Yield New Object() {DateInterval.Year, now, now, 0}
      Yield New Object() {DateInterval.Year, DateTime.MaxValue, DateTime.MaxValue, 0}

      Yield New Object() {DateInterval.Quarter, now, now, 0}

      Yield New Object() {DateInterval.Month, now, now, 0}

      Yield New Object() {DateInterval.DayOfYear, now, now, 0}
      Yield New Object() {DateInterval.DayOfYear, now, now.AddDays(2), 2}
      Yield New Object() {DateInterval.DayOfYear, now, now.AddDays(-2), -2}

      Yield New Object() {DateInterval.Day, now, now, 0}
      Yield New Object() {DateInterval.Day, now, now.AddDays(2), 2}
      Yield New Object() {DateInterval.Day, now, now.AddDays(-2), -2}

      Yield New Object() {DateInterval.Hour, now, now, 0}
      Yield New Object() {DateInterval.Hour, now, now.AddHours(2), 2}
      Yield New Object() {DateInterval.Hour, now, now.AddHours(-2), -2}

      Yield New Object() {DateInterval.Minute, now, now, 0}
      Yield New Object() {DateInterval.Minute, now, now.AddMinutes(2), 2}
      Yield New Object() {DateInterval.Minute, now, now.AddMinutes(-2), -2}

      Yield New Object() {DateInterval.Second, now, now, 0}
      Yield New Object() {DateInterval.Second, now, now.AddSeconds(2), 2}
      Yield New Object() {DateInterval.Second, now, now.AddSeconds(-2), -2}
    End Function

    <Theory>
    <MemberData(NameOf(DateDiff_StringInterval_TestData))>
    Public Sub DateDiff_StringInterval(interval As String, dateTime1 As Object, dateTime2 As Object, expected As Long)
      Assert.Equal(expected, DateAndTime.DateDiff(interval, dateTime1, dateTime2))
    End Sub

    Public Shared Iterator Function DateDiff_StringInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow

      Yield New Object() {"YYYY", DateTime.MinValue, DateTime.MinValue, 0}
      Yield New Object() {"YYYY", now, now, 0}
      Yield New Object() {"YYYY", DateTime.MaxValue, DateTime.MaxValue, 0}

      Yield New Object() {"Q", now, now, 0}

      Yield New Object() {"M", now, now, 0}

      Yield New Object() {"Y", now, now, 0}
      Yield New Object() {"Y", now, now.AddDays(2), 2}
      Yield New Object() {"Y", now, now.AddDays(-2), -2}

      Yield New Object() {"D", now, now, 0}
      Yield New Object() {"D", now, now.AddDays(2), 2}
      Yield New Object() {"D", now, now.AddDays(-2), -2}

      Yield New Object() {"H", now, now, 0}
      Yield New Object() {"H", now, now.AddHours(2), 2}
      Yield New Object() {"H", now, now.AddHours(-2), -2}

      Yield New Object() {"N", now, now, 0}
      Yield New Object() {"N", now, now.AddMinutes(2), 2}
      Yield New Object() {"N", now, now.AddMinutes(-2), -2}

      Yield New Object() {"S", now, now, 0}
      Yield New Object() {"S", now, now.AddSeconds(2), 2}
      Yield New Object() {"S", now, now.AddSeconds(-2), -2}
    End Function

    <Theory>
    <MemberData(NameOf(DatePart_DateInterval_TestData))>
    Public Sub DatePart_DateInterval(interval As DateInterval, dateValue As DateTime, expected As Integer)
      Assert.Equal(expected, DateAndTime.DatePart(interval, dateValue))
    End Sub

    Public Shared Iterator Function DatePart_DateInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow
      Dim calendar1 As System.Globalization.Calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar

      Yield New Object() {DateInterval.Year, now, calendar1.GetYear(now)}
      Yield New Object() {DateInterval.Month, now, calendar1.GetMonth(now)}
      Yield New Object() {DateInterval.Day, now, calendar1.GetDayOfMonth(now)}
      Yield New Object() {DateInterval.Hour, now, calendar1.GetHour(now)}
      Yield New Object() {DateInterval.Minute, now, calendar1.GetMinute(now)}
      Yield New Object() {DateInterval.Second, now, calendar1.GetSecond(now)}
    End Function

    <Theory>
    <MemberData(NameOf(DatePart_StringInterval_TestData))>
    Public Sub DatePart_StringInterval(interval As String, dateValue As Object, expected As Integer)
      Assert.Equal(expected, DateAndTime.DatePart(interval, dateValue))
    End Sub

    Public Shared Iterator Function DatePart_StringInterval_TestData() As IEnumerable(Of Object())
      Dim now As System.DateTime = DateTime.UtcNow
      Dim calendar1 As System.Globalization.Calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar

      Yield New Object() {"YYYY", now, calendar1.GetYear(now)}
      Yield New Object() {"M", now, calendar1.GetMonth(now)}
      Yield New Object() {"D", now, calendar1.GetDayOfMonth(now)}
      Yield New Object() {"H", now, calendar1.GetHour(now)}
      Yield New Object() {"N", now, calendar1.GetMinute(now)}
      Yield New Object() {"S", now, calendar1.GetSecond(now)}
    End Function

    ' Not tested:
    '   public static DateTime DateSerial(int Year, int Month, int Day) { throw null; }

    <Fact>
    Public Sub DateString()
      Dim str As String = DateAndTime.DateString
      ' Should return a date with three non-empty parts.
      Dim parts As String() = str.Split("-"c)
      Assert.Equal(3, parts.Length)
      For Each part As String In parts
        Assert.[False](String.IsNullOrEmpty(part))
      Next
    End Sub

    ' Not tested:
    '   public static string DateString { set { } }
    '   public static DateTime DateValue(string StringDate) { throw null; }

    <Fact>
    Public Sub Fields()
      Dim now As System.DateTime = DateTime.UtcNow
      Dim calendar1 As System.Globalization.Calendar = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar

      Assert.Equal(calendar1.GetYear(now), DateAndTime.Year(now))
      Assert.Equal(calendar1.GetMonth(now), DateAndTime.Month(now))
      Assert.Equal(calendar1.GetDayOfMonth(now), DateAndTime.Day(now))
      Assert.Equal(calendar1.GetHour(now), DateAndTime.Hour(now))
      Assert.Equal(calendar1.GetMinute(now), DateAndTime.Minute(now))
      Assert.Equal(calendar1.GetSecond(now), DateAndTime.Second(now))
    End Sub

    <Fact>
    Public Sub MonthName()
      Dim culture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
      Try
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture

        Assert.Throws(Of ArgumentException)(Function() DateAndTime.MonthName(0, Abbreviate:=False))
        Assert.Throws(Of ArgumentException)(Function() DateAndTime.MonthName(0, Abbreviate:=True))

        Assert.Equal("January", DateAndTime.MonthName(1, Abbreviate:=False))
        Assert.Equal("Jan", DateAndTime.MonthName(1, Abbreviate:=True))

        Assert.Equal("December", DateAndTime.MonthName(12, Abbreviate:=False))
        Assert.Equal("Dec", DateAndTime.MonthName(12, Abbreviate:=True))

        Assert.Throws(Of ArgumentException)(Function() DateAndTime.MonthName(Integer.MaxValue, Abbreviate:=False))
        Assert.Throws(Of ArgumentException)(Function() DateAndTime.MonthName(Integer.MaxValue, Abbreviate:=True))
      Finally
        System.Threading.Thread.CurrentThread.CurrentCulture = culture
      End Try
    End Sub

    <Fact>
    Public Sub Now1()
      Dim dateTimeNowBefore As System.DateTime = DateTime.Now
      Dim now As System.DateTime = DateAndTime.Now
      Dim dateTimeNowAfter As System.DateTime = DateTime.Now

      Assert.InRange(now, dateTimeNowBefore, dateTimeNowAfter)
    End Sub

    ' Not tested:
    '   public static DateTime TimeOfDay { get { throw null; } set { } }
    '   public static double Timer { get { throw null; } }
    '   public static DateTime TimeSerial(int Hour, int Minute, int Second) { throw null; }
    '   public static string TimeString { get { throw null; } set { } }
    '   public static DateTime TimeValue(string StringTime) { throw null; }

    <Fact>
    Public Sub Today()
      Dim dateTimeTodayBefore As System.DateTime = DateTime.Today
      Dim today1 As System.DateTime = DateAndTime.Today
      Dim dateTimeTodayAfter As System.DateTime = DateTime.Today

      Assert.InRange(today1, dateTimeTodayBefore, dateTimeTodayAfter)
      Assert.Equal(TimeSpan.Zero, today1.TimeOfDay)
    End Sub

    ' Not tested:
    '   public static DateTime Today { set { } }
    '   public static int Weekday(System.DateTime DateValue, FirstDayOfWeek DayOfWeek = FirstDayOfWeek.Sunday) { throw null; }

    <Fact>
    Public Sub WeekdayName()
      Dim culture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
      Try
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture

        Assert.Throws(Of ArgumentException)(Function() DateAndTime.WeekdayName(0, Abbreviate:=False))
        Assert.Throws(Of ArgumentException)(Function() DateAndTime.WeekdayName(0, Abbreviate:=True))

        Assert.Equal("Sunday", DateAndTime.WeekdayName(1, Abbreviate:=False))
        Assert.Equal("Sun", DateAndTime.WeekdayName(1, Abbreviate:=True))

        Assert.Equal("Saturday", DateAndTime.WeekdayName(7, Abbreviate:=False))
        Assert.Equal("Sat", DateAndTime.WeekdayName(7, Abbreviate:=True))

        Assert.Throws(Of ArgumentException)(Function() DateAndTime.WeekdayName(Integer.MaxValue, Abbreviate:=False))
        Assert.Throws(Of ArgumentException)(Function() DateAndTime.WeekdayName(Integer.MaxValue, Abbreviate:=True))
      Finally
        System.Threading.Thread.CurrentThread.CurrentCulture = culture
      End Try
    End Sub

  End Class

End Namespace