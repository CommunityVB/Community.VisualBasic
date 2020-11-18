Namespace Global.Community.VisualBasic

  <HideModuleName()>
  Public Module DoesntMatterWhatThisIsCalled

    Public Function Fix(value As Double) As Integer
      Return Math.Floor(value)
    End Function

    Public Function DateAdd(a As Date, b As Integer) As Date
      Return Date.MinValue
    End Function

  End Module

End Namespace