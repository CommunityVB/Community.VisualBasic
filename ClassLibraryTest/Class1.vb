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

    CoverageInteraction()
    CoverageStrings()
    CoverageVBMath()

  End Sub

  Public Sub CoverageInteraction()

    'cVB.Shell("notepad.exe")
    'cVB.AppActivate(1)
    'cVB.AppActivate("a title")

    Dim commandline = cVB.Command()

  End Sub

  Private Sub CoverageStrings()

    Dim a1 = cVB.Asc("a"c)
    Dim a2 = cVB.Asc("a")
    Dim c1 = cVB.Chr(65)
    Dim c2 = cVB.Chr("a")
    Dim ascValue = AscW("A")
    Dim chrValue = ChrW(ascValue)

    Dim o() As Object = {"a", "b"}
    Dim s() As String = {"a", "b"}

    Dim r1 = cVB.Filter(o, "match", True, cVB.CompareMethod.Binary)
    Dim r2 = cVB.Filter(s, "match", True, cVB.CompareMethod.Text)

    Dim i = cVB.InStr(c1, c2)
    i = cVB.InStr(1, c1, c2)
    i = cVB.InStrRev(c1, c2)
    c1 = cVB.Join(o)
    c1 = cVB.Join(s)

    c1 = cVB.LCase("A"c)
    c1 = cVB.LCase("A")

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
    aString = cVB.StrDup(15, CObj("a"))
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
    Dim b = cVB.Rnd(cVB.Timer)

  End Sub

End Class
