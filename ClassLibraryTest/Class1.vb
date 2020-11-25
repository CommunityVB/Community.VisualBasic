Option Explicit On
Option Strict On
Option Infer On

Imports Community.VisualBasic
Imports cVB = Community.VisualBasic
Imports mVB = Microsoft.VisualBasic

Public Class Class1

  Public Sub Test()

    Dim aString = $"Computer Name: {My.Computer.Name}."
    aString = $"OS Name: {My.Computer.Info.OSFullName}."
    aString = $"OS Type: {My.Computer.Info.OSPlatform}."
    aString = $"OS Version: {My.Computer.Info.OSVersion}."

    Dim aDouble = 1.1
    Dim cintFixTest = CInt(cVB.Fix(aDouble))

    CoverageFileSystem()
    'CoverageDateAndTime()
    'CoverageInformation()
    'CoverageInteraction()
    'CoverageStrings()
    'CoverageVBMath()

  End Sub

  Private Sub CoverageDateAndTime()

    Console.WriteLine($"Today={Today}")
    Console.WriteLine($"Now={Now}")
    Console.WriteLine($"TimeOfDay={TimeOfDay}")
    Console.WriteLine($"TimeString={TimeString}")
    Console.WriteLine($"DateString={DateString}")
    Console.WriteLine($"Timer={Timer}")
    Console.WriteLine($"DateAdd(Day, 1, Today)={DateAdd(DateInterval.Day, 1, Today)}")
    Console.WriteLine($"DateDiff(Day, Min, Max)={DateDiff(DateInterval.Day, Date.MinValue, Date.MaxValue)}")
    Console.WriteLine($"DatePart(Day, Now)={DatePart(DateInterval.Day, Now)}")
    Console.WriteLine($"DateSerial(2000, 12, 31)={DateSerial(2000, 12, 31)}")
    Console.WriteLine($"TimeSerial(5, 30, 15)={TimeSerial(5, 30, 15)}")
    Console.WriteLine($"DateValue={DateValue(Now.ToString)}")
    Console.WriteLine($"TimeValue={TimeValue(Now.ToString)}")

    Console.WriteLine($"Year={Year(Now)}")
    Console.WriteLine($"Month={Month(Now)}")
    Console.WriteLine($"Day={Day(Now)}")
    Console.WriteLine($"Hour={Hour(Now)}")
    Console.WriteLine($"Minute={Minute(Now)}")
    Console.WriteLine($"Second={Second(Now)}")
    Console.WriteLine($"Weekday={Weekday(Now)}")
    Console.WriteLine($"MonthName={MonthName(Month(Now))}")
    Console.WriteLine($"WeekdayName={WeekdayName(Weekday(Now))}")

  End Sub

  Private Sub CoverageFileSystem()

    Console.WriteLine($"CurDir={CurDir()}")
    'ChDir("..\..")
    'Console.WriteLine($"CurDir={CurDir()}")
    Dim result = Dir("*.dll")
    Do
      Console.WriteLine(result)
      result = Dir()
      If Not result <> "" Then
        Exit Do
      End If
    Loop

    If IO.File.Exists("test.txt") Then Kill("test.txt")

    'Dim contents = ""
    'IO.File.WriteAllText("test.txt", contents)
    'FileOpen(1, "test.txt", OpenMode.Append, OpenAccess.Write, OpenShare.Shared)
    'Print(1, "This is a test" & vbCrLf)
    'Print(1, "This is another test" & vbCrLf)
    'FileClose(1)
    'contents = IO.File.ReadAllText("test.txt")
    'Console.WriteLine(contents)

    Dim fs As FileStructure
    fs.Number = 1
    fs.Money = 300.1

    FileOpen(1, "test.txt", OpenMode.Random, OpenAccess.ReadWrite, OpenShare.Shared, Len(fs))
    fs.Number = 1 : fs.Money = 300.1 : FilePut(1, fs, 1)
    fs.Number = 2 : fs.Money = 700.2 : FilePut(1, fs, 2)
    fs.Number = 3 : fs.Money = 754 : FilePut(1, fs, 3)
    FileGet(1, fs, 1)
    FileClose(1)

    Console.WriteLine($"{fs.Number}={fs.Money}")

  End Sub

  Private Structure FileStructure
    Public Number As Integer
    Public Money As Double
  End Structure


  Private Sub CoverageInformation()

    Dim err1 = Err.Erl
    Dim erl1 = Erl()

    Dim ar = {"a", "b", "c"}

    Console.WriteLine($"IsArray={IsArray(ar)}")
    Console.WriteLine($"IsDate={IsDate(ar)}")
    Console.WriteLine($"IsDBNull={IsDBNull(ar)}")
    Console.WriteLine($"IsNothing={IsNothing(ar)}")
    Console.WriteLine($"IsError={IsError(ar)}")
    Console.WriteLine($"IsReference={IsReference(ar)}")
    Console.WriteLine($"IsNumeric={IsNumeric(ar)}")

    Console.WriteLine($"LBound={LBound(ar)}")
    Console.WriteLine($"UBound={UBound(ar)}")

    Dim qbc = QBColor(15)
    Dim rgb1 = RGB(100, 100, 100)

    Console.WriteLine($"VarType={VarType(ar)}")
    Console.WriteLine($"TypeName={TypeName(ar)}")
    Console.WriteLine($"SystemTypeName={TypeName(ar)}")
    Console.WriteLine($"VbTypeName={VbTypeName("String")}")

  End Sub

  Public Function GetCommandLine() As String
    Return cVB.Command
  End Function

  Public Function GetEnviron(name As String) As String
    Return cVB.Environ(name)
  End Function

  Public Sub CoverageInteraction()

    'cVB.Shell("notepad.exe")
    'cVB.AppActivate(1)
    'cVB.AppActivate("a title")

    Dim commandline = cVB.Command()
    Dim evar = cVB.Environ("Path")
    Dim choice = cVB.Choose(2, {"a", "b", "c", "d", "e"})
    Dim iifresult1 = IIf(True, "A", "B")

    Dim year As Long = 1984
    Dim decade = Partition(year, 1950, 2049, 10)
    Console.WriteLine($"Year {year} is in decade {decade}.")
    Console.WriteLine($"Language (Switch Test): {MatchLanguage("Rome")}")

    Dim col As New Collection()
    'Store the string "Item One" in a collection by calling the Add method.
    CallByName(col, "Add", CallType.Method, "Item One")
    'Retrieve the first entry from the collection using the Item property and display it using Console.WriteLine().
    Console.WriteLine(CallByName(col, "Item", CallType.Get, 1))

  End Sub

  Function MatchLanguage(ByVal cityName As String) As String
    Return $"{Switch(cityName = "London", "English",
                     cityName = "Rome", "Italian",
                     cityName = "Paris", "French")}"
  End Function

  Private Sub CoverageStrings()

    Dim a1 = cVB.Asc("a"c)
    Dim a2 = cVB.Asc("a")
    Dim c1 = cVB.Chr(65)
    Dim c2 = cVB.Chr(AscW("a"c))
    Dim ascValue = AscW("A")
    Dim chrValue = ChrW(ascValue)

    Dim o() As Object = {"a", "b"}
    Dim s() As String = {"a", "b"}

    Dim r1 = cVB.Filter(o, "match", True, cVB.CompareMethod.Binary)
    Dim r2 = cVB.Filter(s, "match", True, cVB.CompareMethod.Text)

    Dim sa As String()
    Dim st As String

    Dim i = cVB.InStr(c1, c2)
    i = cVB.InStr(1, c1, c2)
    i = cVB.InStrRev(c1, c2)
    st = cVB.Join(o)
    st = cVB.Join(s)

    st = cVB.LCase("A"c)
    st = cVB.LCase("A")

    Dim aBoolean = True
    Dim aByte = CByte(1)
    Dim aInt16 = Convert.ToInt16(1)
    Dim aUInt16 = Convert.ToUInt16(1)
    Dim aInt32 = Convert.ToInt32(1)
    Dim aUInt32 = Convert.ToUInt32(1)
    Dim aInt64 = Convert.ToInt64(1)
    Dim aUInt64 = Convert.ToUInt64(1)
    Dim aDecimal = CDec(1)
    Dim aSingle = CSng(1)
    Dim aDouble = CDbl(1)
    Dim aShort = CShort(1)
    Dim aInteger = CInt(1)
    Dim aLong = CLng(1)
    Dim aDate = Date.MinValue
    Dim aString = "a"
    Dim aChar = "a"c

    Dim lenBoolean = cVB.Len(aBoolean)
    Dim lenByte = cVB.Len(aByte)
    Dim lenInt16 = cVB.Len(aInt16)
    Dim lenUInt16 = cVB.Len(aUInt16)
    Dim lenInt32 = cVB.Len(aInt32)
    Dim lenUInt32 = cVB.Len(aUInt32)
    Dim lenInt64 = cVB.Len(aInt64)
    Dim lenUInt64 = cVB.Len(aUInt64)
    Dim lenDecimal = cVB.Len(aDecimal)
    Dim lenSingle = cVB.Len(aSingle)
    Dim lenDouble = cVB.Len(aDouble)
    Dim lenShort = cVB.Len(aShort)
    Dim lenInteger = cVB.Len(aInteger)
    Dim lenLong = cVB.Len(aLong)
    Dim lenDate = cVB.Len(aDate)
    Dim lenString = cVB.Len(aString)
    Dim lenChar = cVB.Len(aChar)
    Dim lenObj = cVB.Len(CObj(aChar))

    aString = cVB.Replace(aString, "a", "b")
    aString = cVB.Space(15)
    Dim aArray = cVB.Split("a,b,c,d,e", ",")

    aString = cVB.LSet("abc", 5)
    aString = cVB.RSet("abc", 5)

    aString = cVB.StrDup(15, "a"c)
    aString = CStr(cVB.StrDup(15, CObj("a")))
    aString = cVB.StrDup(15, "a")

    aString = cVB.StrReverse(aString)

    aString = cVB.UCase(aString)
    aString = cVB.UCase("a"c)

    aString = cVB.Format("abc")
    aString = cVB.FormatCurrency("abc")
    aString = cVB.FormatDateTime(cVB.Today)
    aString = cVB.FormatNumber(35.1)
    aString = cVB.FormatPercent(1.1)

    aChar = cVB.GetChar(aString, 1)

    Dim aa1 = "This is a test"
    'Dim leftSide = Microsoft.VisualBasic.Strings.Left(aa1, 4)
    Dim leftString = cVB.Left(aa1, 4)
    leftString = cVB.LTrim(leftString)
    Dim midString = cVB.Mid(aa1, 2, 2)
    midString = cVB.Mid(aa1, 2)
    Mid(aa1, 2, 1) = "X"
    Dim rightString = cVB.Right(aa1, 4)
    rightString = cVB.RTrim(rightString)
    Dim trimString = cVB.Trim(aa1)

    Dim result = cVB.StrComp(leftString, rightString)

    ' The following generates a PlatformNotSupportedException... so remarking out.
    'aString = cVB.StrConv(aString, cVB.VbStrConv.Narrow)

  End Sub

  Private Sub CoverageVBMath()

    cVB.Randomize()
    cVB.Randomize(cVB.Timer)

    Dim a = cVB.Rnd()
    Dim b = cVB.Rnd(CSng(cVB.Timer))

  End Sub

End Class
