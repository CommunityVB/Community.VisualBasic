' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

Namespace Global.Community.VisualBasic.Tests

  Public Class FinancialTests

    Private Shared ReadOnly Property IsArmOrArm64OrAlpine As Boolean
      Get
        Return PlatformDetection.IsAlpine OrElse PlatformDetection.IsArmOrArm64Process
      End Get
    End Property

#Disable Warning IDE0051 ' Remove unused private members
    Private Shared ReadOnly Property IsNotArmNorArm64NorAlpine As Boolean
      Get
        Return Not IsArmOrArm64OrAlpine
      End Get
    End Property
#Enable Warning IDE0051 ' Remove unused private members

    ' The accuracy to which we can validate some numeric test cases depends on the platform.

    Private Shared ReadOnly s_precision As Integer = If(IsArmOrArm64OrAlpine, 12, If((PlatformDetection.IsBrowser OrElse PlatformDetection.IsNetFramework), 14, 15))

    <Theory>
    <InlineData(0, 1.0, 1.0, 1.0, 1.0, 0, 0)>
    <InlineData(2000.0, 500.0, 2.0, 1.0, 2.0, 1500.0, -4)>
    <InlineData(10000.0, 4350.0, 84.0, 35.0, 2.0, 57.326806353887477, -2)>
    <InlineData(15006.0, 6350.0, 81.0, 23.0, 1.5, 184.19489836666187, -3)>
    <InlineData(11606.0, 6350.0, 74.0, 17.0, 2.1, 207.79010936598854, -3)>
    <InlineData(11606.0, 16245.0, 71.0, 17.0, 2.0, 0, -1)> ' cost < salvage
    <InlineData(10100.0, 10100.0, 70.0, 20.0, 2.0, 0, -1)> ' cost = salvage
    Public Sub DDB(Cost As Double, Salvage As Double, Life As Double, Period As Double, Factor As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.DDB(Cost, Salvage, Life, Period, Factor), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub DDB_Default()
      Assert.Equal(57.326806353887477, Financial.DDB(10000.0, 4350.0, 84.0, 35.0), s_precision - 2)
    End Sub

    <Theory>
    <InlineData(-10000, 5211, -81, 35, 2)>
    Public Sub DDB_Invalid(Cost As Double, Salvage As Double, Life As Double, Period As Double, Factor As Double)
      Assert.Throws(Of ArgumentException)(Function() Financial.DDB(Cost, Salvage, Life, Period, Factor))
    End Sub

    <Theory>
    <InlineData(0, 0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.02 / 12, 12.0, -100.0, -100.0, DueDate.BegOfPeriod, 1315.0982120264073, -4)>
    <InlineData(0.0083, 15, 263.0, 0, DueDate.EndOfPeriod, -4182.6572911381636, -4)>
    <InlineData(0.013, 90, 81.0, 5000.0, DueDate.BegOfPeriod, -29860.905154180855, -5)>
    <InlineData(0.01, 37, 100.0, 0, DueDate.BegOfPeriod, -4495.2723614177185, -4)>
    <InlineData(-0.0083, 15, 263.0, 0, DueDate.EndOfPeriod, -3723.8376500804811, -4)> ' rate < 0
    <InlineData(0, 15, 263, 0, DueDate.EndOfPeriod, -3945, -4)> ' rate = 0
    <InlineData(0.0083, 15, 263.0, 0, CType(8, DueDate), -4217.37334665461, -4)> ' type <> 0 and type <> 1
    Public Sub FV(Rate As Double, NPer As Double, Pmt As Double, PV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.FV(Rate, NPer, Pmt, PV, Due), s_precision + relativePrecision)
    End Sub

    '<ConditionalTheory(NameOf(IsNotArmNorArm64NorAlpine))>
    <Theory>
    <InlineData(1.0E+25, 12, 1797, 0, CType(1, DueDate), -1.7970000000000019E+303, -10)> ' overFlow
    Public Sub FV_Overflow(Rate As Double, NPer As Double, Pmt As Double, PV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.FV(Rate, NPer, Pmt, PV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub FV_Default()
      Assert.Equal(-4182.6572911381636, Financial.FV(0.0083, 15, 263.0, 0), s_precision - 4)
      Assert.Equal(-4182.6572911381636, Financial.FV(0.0083, 15, 263.0), s_precision - 4)
    End Sub

    <Theory>
    <InlineData(0, 1.0, 1.0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.1 / 12, 12.0, 48.0, -20000.0, 0, DueDate.BegOfPeriod, 133.00409235108953, -3)>
    <InlineData(0.008, 4, 12, 3000, 0, CType(0, DueDate), -18.213395984179868, -2)>
    <InlineData(0.012, 15, 79, 2387, 200, CType(1, DueDate), -24.744071482829071, -2)>
    <InlineData(0.0096, 54, 123, 4760, 0, CType(0, DueDate), -32.239158754290543, -2)>
    <InlineData(-0.008, 4, 12, 3000, 0, CType(0, DueDate), 17.781421333448126, -2)> ' rate < 0
    <InlineData(0, 4, 12, 3000, 0, CType(0, DueDate), 0, 0)> ' rate = 0
    <InlineData(0.008, 4, 12, 3000, 0, CType(7, DueDate), -18.068845222400633, -2)> ' type <> 0 and type <> 1
    Public Sub IPmt(Rate As Double, Per As Double, NPer As Double, PV As Double, FV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.IPmt(Rate, Per, NPer, PV, FV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub IPmt_Default()
      Assert.Equal(-18.213395984179868, Financial.IPmt(0.008, 4, 12, 3000, 0), s_precision - 2)
      Assert.Equal(-18.213395984179868, Financial.IPmt(0.008, 4, 12, 3000), s_precision - 2)
    End Sub

    <Theory>
    <InlineData(0.008, -4, 12, 3000, 0, CType(0, DueDate))>
    <InlineData(0.008, 4, -12, 3000, 0, CType(0, DueDate))>
    <InlineData(0.008, 12, 4, 3000, 0, CType(0, DueDate))>
    Public Sub IPmt_Invalid(Rate As Double, Per As Double, NPer As Double, PV As Double, FV As Double, Due As DueDate)
      Assert.Throws(Of ArgumentException)(Function() Financial.IPmt(Rate, Per, NPer, PV, FV, Due))
    End Sub

    <Theory>
    <InlineData(New Double() {-1, 1}, 0, 0, 0)>
    <InlineData(New Double() {-70000.0, 22000.0, 25000.0, 28000.0, 31000.0}, 0.1, 0.177435884421108, 0)>
    <InlineData(New Double() {-10000.0, 6000.0, -2000.0, 7000.0, 1000.0}, 0.1, 0.086672047429171645, 0)>
    <InlineData(New Double() {-30000.0, -10000.0, 25000.0, 12000.0, 15000.0}, 0.1, 0.10928101434575988, 0)>
    Public Sub IRR(ValueArray As Double(), Guess As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.IRR(ValueArray, Guess), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub IRR_Default()
      Dim ValueArray As Double() = New Double() {-70000.0, 22000.0, 25000.0, 28000.0, 31000.0}
      Assert.Equal(0.177435884421108, Financial.IRR(ValueArray), s_precision - 0)
    End Sub

    <Theory>
    <InlineData(New Double() {})>
    <InlineData(New Double() {70000, 22000, 25000, 28000, 31000})>
    <InlineData(New Double() {-70000, -22000, -25000, -28000, -31000})>
    Public Sub IRR_Invalid(ValueArray As Double())
      Assert.Throws(Of ArgumentException)(Function() Financial.IRR(ValueArray))
    End Sub

    <Theory>
    <InlineData(New Double() {-1, 1}, 0, 0, 0, 0)>
    <InlineData(New Double() {-70000.0, 22000.0, 25000.0, 28000.0, 31000.0}, 0.1, 0.12, 0.15512706281927668, 0)>
    Public Sub MIRR(ValueArray As Double(), FinanceRate As Double, ReinvestRate As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.MIRR(ValueArray, FinanceRate, ReinvestRate), s_precision + relativePrecision)
    End Sub

    <Theory>
    <InlineData(New Double() {70000, 22000, 25000, 28000, 31000}, 0.1, 0.12)>
    Public Sub MIRR_Invalid(ValueArray As Double(), FinanceRate As Double, ReinvestRate As Double)
      Assert.Throws(Of DivideByZeroException)(Function() Financial.MIRR(ValueArray, FinanceRate, ReinvestRate))
    End Sub

    <Theory>
    <InlineData(0, 1.0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.02 / 12, -800.0, 10000, 0, DueDate.BegOfPeriod, 12.621310788105905, -2)>
    <InlineData(0.0072, -350.0, 7000.0, 0, DueDate.EndOfPeriod, 21.672774889301333, -2)>
    <InlineData(0.018, -982.0, 33000.0, 2387, DueDate.BegOfPeriod, 52.912706055483291, -2)>
    <InlineData(0.0096, 1500.0, -70000.0, 10000, DueDate.EndOfPeriod, 55.2706372559078, -2)>
    <InlineData(-0.0072, -350.0, 7000.0, 0, DueDate.EndOfPeriod, 18.617499787178836, -2)> ' rate < 0
    <InlineData(0, -350.0, 7000.0, 0, DueDate.EndOfPeriod, 20, -2)> ' rate = 0
    <InlineData(0.0072, -350.0, 7000.0, 0, CType(7, DueDate), 21.505253294376303, -2)> ' type <> 0 and type <> 1
    <InlineData(0.0072, -9000.0, 200.0, 0, DueDate.EndOfPeriod, 0.022303910926832409, 0)> ' pmt > pv
    Public Sub NPer(Rate As Double, Pmt As Double, PV As Double, FV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.NPer(Rate, Pmt, PV, FV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub NPer_Default()
      Assert.Equal(21.672774889301333, Financial.NPer(0.0072, -350.0, 7000.0, 0), s_precision - 2)
      Assert.Equal(21.672774889301333, Financial.NPer(0.0072, -350.0, 7000.0), s_precision - 2)
    End Sub

    <Theory>
    <InlineData(0, New Double() {0}, 0, 0)>
    <InlineData(0.1, New Double() {-70000.0, 22000.0, 25000.0, 28000.0, 31000.0}, 11701.262333049774, -5)>
    <InlineData(0.0625, New Double() {-70000.0, 22000.0, 25000.0, 28000.0, 31000.0}, 19312.570209535177, -5)>
    <InlineData(0.089, New Double() {-10000, 6000, -2000, 7000, 1000}, -41.865531602947726, -2)>
    <InlineData(0.011, New Double() {-30000, -10000, 25000, 12000, 15000}, 10423.402571986529, -5)>
    <InlineData(-0.011, New Double() {-30000, -10000, 25000, 12000, 15000}, 13681.919127606127, -5)> ' rate < 0
    <InlineData(0.0625, New Double() {70000.0, 22000.0, 25000.0, 28000.0, 31000.0}, 151077.27609188814, -6)> ' all positive cash flows
    <InlineData(0.0625, New Double() {-70000.0, -22000.0, -25000.0, -28000.0, -31000.0}, -151077.27609188814, -6)> ' all negative cash flows
    Public Sub NPV(Rate As Double, ValueArray As Double(), expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.NPV(Rate, ValueArray), s_precision + relativePrecision)
    End Sub

    <Theory>
    <InlineData(-1, New Double() {})>
    <InlineData(0.12, New Double() {})>
    Public Sub NPV_Invalid(Rate As Double, ValueArray As Double())
      Assert.Throws(Of ArgumentException)(Function() Financial.NPV(Rate, ValueArray))
    End Sub

    <Theory>
    <InlineData(0, 1.0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.02 / 12, 24, -10000, 0, DueDate.BegOfPeriod, 424.69480900312141, -3)>
    <InlineData(0.007, 25, -3000, 0, DueDate.EndOfPeriod, 131.2245402332282, -3)>
    <InlineData(0.019, 70, 80000, 20000, DueDate.BegOfPeriod, -2173.6132234513088, -4)>
    <InlineData(0.0012, 5, 500, 0, DueDate.EndOfPeriod, -100.36028782715209, -3)>
    <InlineData(-0.007, 25, -3000, 0, DueDate.EndOfPeriod, 109.38667732684138, -3)> ' rate < 0
    <InlineData(0.007, -25, 3000, 0, DueDate.EndOfPeriod, 110.22454023322811, -3)> ' nper < 0
    <InlineData(0.007, 25, 3000, 0, CType(7, DueDate), -130.31235375692967, -3)> ' type <> 0 and type <> 1
    <InlineData(0, 25, 3000, 0, DueDate.EndOfPeriod, -120, -3)> ' rate = 0
    Public Sub Pmt(Rate As Double, NPer As Double, PV As Double, FV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.Pmt(Rate, NPer, PV, FV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub Pmt_Default()
      Assert.Equal(131.2245402332282, Financial.Pmt(0.007, 25, -3000, 0), s_precision - 3)
      Assert.Equal(131.2245402332282, Financial.Pmt(0.007, 25, -3000), s_precision - 3)
    End Sub

    <Theory>
    <InlineData(0, 1.0, 1.0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.02 / 12, 1.0, 24, -10000, 0, DueDate.BegOfPeriod, 424.69480900312141, -3)>
    <InlineData(0.008, 4, 12, 3000, 0, DueDate.EndOfPeriod, -244.97648292629228, -3)>
    <InlineData(0.012, 15, 79, 2387, 200, DueDate.BegOfPeriod, -23.148683359577142, -2)>
    <InlineData(0.0096, 54, 123, 4760, 0, DueDate.EndOfPeriod, -33.868800792363771, -2)>
    <InlineData(-0.008, 4, 12, 3000, 0, DueDate.EndOfPeriod, -254.97282491851354, -3)> ' rate < 0
    <InlineData(0.008, 4, 12, 3000, 0, CType(7, DueDate), -243.03222512529004, -3)> ' type <> 0 and type <> 1
    Public Sub PPmt(Rate As Double, Per As Double, NPer As Double, PV As Double, FV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.PPmt(Rate, Per, NPer, PV, FV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub PPmt_Default()
      Assert.Equal(-244.97648292629228, Financial.PPmt(0.008, 4, 12, 3000, 0), s_precision - 3)
      Assert.Equal(-244.97648292629228, Financial.PPmt(0.008, 4, 12, 3000), s_precision - 3)
    End Sub

    <Theory>
    <InlineData(0, 0, 0, 0, DueDate.EndOfPeriod, 0, 0)>
    <InlineData(0.02 / 12, 12.0, -100.0, -100.0, DueDate.BegOfPeriod, 1287.1004825212165, -4)>
    <InlineData(0.008, 31, 2000.0, 0, DueDate.EndOfPeriod, -54717.415910413583, -5)>
    <InlineData(0.012, 15, 780.0, 2000.0, DueDate.BegOfPeriod, -12449.356090210378, -5)>
    <InlineData(0.0096, 54, 123.0, 4760.0, DueDate.EndOfPeriod, -8005.5869008740428, -4)>
    <InlineData(-0.008, 31, 2000.0, 0, DueDate.EndOfPeriod, -70684.64967009431, -5)> ' rate < 0
    <InlineData(0, 31, 2000.0, 0, DueDate.EndOfPeriod, -62000, -5)> ' rate = 0
    <InlineData(0.008, -31, 2000.0, 0, DueDate.EndOfPeriod, 70049.021736254566, -5)> ' nper < 0
    <InlineData(1.0E+25, 12, 1797, 0, DueDate.BegOfPeriod, -1797, -4)> ' overflow
    Public Sub PV(Rate As Double, NPer As Double, Pmt As Double, FV As Double, Due As DueDate, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.PV(Rate, NPer, Pmt, FV, Due), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub PV_Default()
      Assert.Equal(-2952.9448523201449, Financial.PV(0.008, 4, 12, 3000, 0), s_precision - 4)
      Assert.Equal(-2952.9448523201449, Financial.PV(0.008, 4, 12, 3000), s_precision - 4)
    End Sub

    <Theory>
    <InlineData(1.0, 1.0, 1.0, 0, DueDate.EndOfPeriod, 0, -2, 0)>
    <InlineData(24.0, -800.0, 10000.0, 0.0, DueDate.BegOfPeriod, 0.1, 0.067670278656511415, 0)>
    <InlineData(12, -263.0, 3000, 0, DueDate.EndOfPeriod, 0.1, 0.0078864383776339579, 0)>
    <InlineData(48, -570, 24270.0, 0, DueDate.BegOfPeriod, 0.1, 0.0052240164339990847, 0)>
    <InlineData(96, -1000.0, 56818, 0, DueDate.EndOfPeriod, 0.1, 0.012000207330254708, 0)>
    <InlineData(12, -3000.0, 300, 0, DueDate.EndOfPeriod, 0.1, -1.9850238772287565, -1)> ' pmt > pv
    Public Sub Rate(NPer As Double, Pmt As Double, PV As Double, FV As Double, Due As DueDate, Guess As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.Rate(NPer, Pmt, PV, FV, Due, Guess), s_precision + relativePrecision)
    End Sub

    <Fact>
    Public Sub Rate_Default()
      Assert.Equal(0.0078864383776339579, Financial.Rate(12, -263.0, 3000, 0), s_precision - 0)
      Assert.Equal(0.0078864383776339579, Financial.Rate(12, -263.0, 3000), s_precision - 0)
    End Sub

    <Fact>
    Public Sub Rate_Invalid()
      Assert.Throws(Of ArgumentException)(Function() Financial.Rate(-12, -263.0, 3000, 0, 0, 0.1))
    End Sub

    <Theory>
    <InlineData(1.0, 1.0, 1.0, 0, 0)>
    <InlineData(1000.0, 800.0, 50.0, 4.0, -1)>
    <InlineData(5000, 1000, 20, 200, -3)>
    <InlineData(54870, 21008, 7, 4837.4285714285716, -4)>
    <InlineData(2, 1.1, 12, 0.075, 0)>
    <InlineData(1000, 5000, 20, -200, -3)> ' salvage > cost
    <InlineData(5000, 1000, -20, -200, -3)> ' life < 0
    <InlineData(5000, 0, 12, 416.66666666666669, -3)> ' salvage = 0
    <InlineData(-5000, -1000, -20, 200, -3)> ' all parameter -ve
    Public Sub SLN(Cost As Double, Salvage As Double, Life As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.SLN(Cost, Salvage, Life), s_precision + relativePrecision)
    End Sub

    <Theory>
    <InlineData(1.0, 1.0, 1.0, 1.0, 0, 0)>
    <InlineData(1000.0, 800.0, 50, 2.0, 7.6862745098039218, -1)>
    <InlineData(4322.0, 1009.0, 73, 23, 62.555720103665308, -2)>
    <InlineData(78000.0, 21008, 8, 2, 11081.777777777777, -5)>
    <InlineData(23.0, 7.0, 21, 9, 0.90043290043290047, 0)>
    <InlineData(1009.0, 4322.0, 73, 23, -62.555720103665308, -2)> ' salvage > cost
    Public Sub SYD(Cost As Double, Salvage As Double, Life As Double, Period As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.SYD(Cost, Salvage, Life, Period), s_precision + relativePrecision)
    End Sub

    '<ConditionalTheory(NameOf(IsNotArmNorArm64NorAlpine))>
    <Theory>
    <InlineData(9.9999999999999E+305, 0, 100, 10, 1.801980198019784E+304, -10)> ' overflow
    Public Sub SYD_Overflow(Cost As Double, Salvage As Double, Life As Double, Period As Double, expected As Double, relativePrecision As Integer)
      Assert.Equal(expected, Financial.SYD(Cost, Salvage, Life, Period), s_precision + relativePrecision)
    End Sub

    <Theory>
    <InlineData(4322.0, 1009.0, 23, 73)>
    <InlineData(4322.0, 1009.0, 0, 23)>
    <InlineData(4322.0, 1009.0, 73, 0)>
    <InlineData(-4322.0, -1009.0, -73, -23)>
    <InlineData(0.0, 0.0, 0, 0)>
    Public Sub SYD_Invalid(Cost As Double, Salvage As Double, Life As Double, Period As Double)
      Assert.Throws(Of ArgumentException)(Function() Financial.SYD(Cost, Salvage, Life, Period))
    End Sub

  End Class

End Namespace