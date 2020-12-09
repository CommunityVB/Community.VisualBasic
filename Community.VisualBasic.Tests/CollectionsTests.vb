' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Collections
Imports Xunit
Imports Community.VisualBasic

Namespace Global.Community.VisualBasic.Tests

  Public Class CollectionsTests

    <Fact>
    Public Sub Ctor_Empty()
      Dim coll As Community.VisualBasic.Collection = New Collection
      Assert.Equal(0, coll.Count)
    End Sub

    Private Shared Function CreateValue(i As Integer) As Foo
      Return New Foo(i, i.ToString())
    End Function

    Private Shared Function CreateCollection(count1 As Integer) As Collection
      If count1 <> 0 Then

      End If
      Dim coll As Community.VisualBasic.Collection = New Collection
      For i As Integer = 0 To 10 - 1
        coll.Add(CreateValue(i))
      Next
      Return coll
    End Function

    Private Shared Function CreateKeyedCollection(count1 As Integer) As Collection
      If count1 <> 0 Then

      End If
      Dim coll As Community.VisualBasic.Collection = New Collection
      For i As Integer = 0 To 10 - 1
        coll.Add(CreateValue(i), Key:="Key" & i.ToString())
      Next
      Return coll
    End Function

    <Fact>
    Public Sub Add()
      Dim coll As IList = New Collection
      For i As Integer = 0 To 10 - 1
        Dim value As Foo = CreateValue(i)
        coll.Add(value)
        Assert.[True](coll.Contains(value))
      Next
      Assert.Equal(10, coll.Count)
      For i As Integer = 0 To coll.Count - 1
        Dim value As Foo = CreateValue(i)
        Assert.Equal(value, coll(i))
      Next
    End Sub

    <Fact>
    Public Sub Add_RelativeIndex()
      Dim coll As Community.VisualBasic.Collection = New Collection
      Dim item1 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(1)
      Dim item2 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(2)
      Dim item3 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(3)

      coll.Add(item1)
      coll.Add(item2, Before:=1)
      coll.Add(item3, After:=1)

      Assert.Equal(item2, coll(1))
      Assert.Equal(item3, coll(2))
      Assert.Equal(item1, coll(3))
    End Sub

    <Fact>
    Public Sub Add_RelativeKey()
      Dim coll As Community.VisualBasic.Collection = New Collection
      Dim item1 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(1)
      Dim item2 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(2)
      Dim item3 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(3)

      coll.Add(item1, "Key1")
      coll.Add(item2, Key:="Key2", Before:="Key1")
      coll.Add(item3, After:="Key2")

      Assert.Equal(item2, coll(1))
      Assert.Equal(item3, coll(2))
      Assert.Equal(item1, coll(3))
    End Sub

    <Fact>
    Public Sub Add_Relative_Invalid()
      Dim coll As Community.VisualBasic.Collection = New Collection
      Dim item1 As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(1)

      Assert.Throws(Of ArgumentException)(Sub() coll.Add(item1, Before:=1, After:=1)) ' Before and after specified
      Assert.Throws(Of InvalidCastException)(Sub() coll.Add(item1, Before:=New Object)) ' Before not in a string or int
      Assert.Throws(Of InvalidCastException)(Sub() coll.Add(item1, After:=New Object)) ' After not in a string or int
      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll.Add(item1, Before:=5)) ' Before not in range
      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll.Add(item1, After:=5)) ' After not in range
      Assert.Throws(Of ArgumentException)(Sub() coll.Add(item1, Before:="Key5")) ' Before not found
      Assert.Throws(Of ArgumentException)(Sub() coll.Add(item1, After:="Key5")) ' After not found
    End Sub

    <Fact>
    Public Sub Remove()
      Dim coll As IList = CreateCollection(10)
      For i As Integer = 0 To 10 - 1
        Dim value As Foo = CreateValue(i)
        coll.Remove(value)
        Assert.[False](coll.Contains(value))
      Next
      Assert.Equal(0, coll.Count)

      coll.Remove(New Foo) ' No throw for non-existent object
    End Sub

    <Fact>
    Public Sub Insert()
      Dim coll As IList = New Collection
      For i As Integer = 0 To 10 - 1
        Dim value As Foo = CreateValue(i)
        coll.Insert(i, value)
        Assert.[True](coll.Contains(value))
      Next
      Assert.Equal(10, coll.Count)
      For i As Integer = 0 To coll.Count - 1
        Dim expected As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(i)
        Assert.Equal(expected, coll(i))
      Next
    End Sub

    <Fact>
    Public Sub Insert_InvalidIndex_ThrowsArgumentOutOfRangeException()
      Dim coll As IList = CreateCollection(10)
      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll.Insert(-1, New Foo)) ' Index < 0
      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll.Insert(coll.Count + 1, New Foo)) ' Index > coll.Count

      Assert.Equal(10, coll.Count)
    End Sub

    <Fact>
    Public Sub RemoveAt()
      Dim coll As IList = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        coll.RemoveAt(0)
        Assert.[False](coll.Contains(CreateValue(i)))
      Next
    End Sub

    <Fact>
    Public Sub RemoveAt_InvalidIndex_ThrowsArgumentOutOfRangeException()
      Dim coll As IList = CreateCollection(10)

      Dim firstItem As Object = coll(0)
      coll.RemoveAt(-10)    ' Indexing bug when accessing through the IList interface on non-empty collection
      Assert.[False](coll.Contains(firstItem))
      Assert.Equal(9, coll.Count)

      coll = New Collection

      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll.RemoveAt(-1)) ' Index < 0
    End Sub

    <Fact>
    Public Sub Remove_Key()
      Dim coll As Community.VisualBasic.Collection = CreateKeyedCollection(10)

      Assert.[True](coll.Contains("Key3"))
      coll.Remove("Key3")
      Assert.[False](coll.Contains("Key3"))
    End Sub

    <Fact>
    Public Sub Remove_InvalidKey_ThrowsArgumentException()
      Dim coll As Community.VisualBasic.Collection = CreateKeyedCollection(10)

      Assert.Throws(Of ArgumentException)(Sub() coll.Remove("Key10"))
    End Sub

    <Fact>
    Public Sub Remove_Index()
      Dim coll As Collection = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        coll.Remove(1)
        Assert.[False](CType(coll, IList).Contains(CreateValue(i)))
      Next
    End Sub

    <Fact>
    Public Sub Remove_InvalidIndex_ThrowsArgumentException()
      Dim coll As Community.VisualBasic.Collection = CreateCollection(10)

      Assert.Throws(Of IndexOutOfRangeException)(Sub() coll.Remove(20))
    End Sub

    <Fact>
    Public Sub Clear()
      Dim coll As Collection = CreateCollection(10)
      coll.Clear()
      Assert.Equal(0, coll.Count)
    End Sub

    <Fact>
    Public Sub IndexOf()
      Dim coll As IList = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        Dim ndx As Integer = coll.IndexOf(CreateValue(i))
        Assert.Equal(i, ndx)
      Next
    End Sub

    <Fact>
    Public Sub Contains()
      Dim coll As IList = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        Assert.[True](coll.Contains(CreateValue(i)))
      Next
      Assert.[False](coll.Contains(New Foo))
    End Sub

    <Fact>
    Public Sub Contains_ByKey()
      Dim coll As Community.VisualBasic.Collection = CreateKeyedCollection(10)

      Assert.[True](coll.Contains("Key3"))
      Assert.[False](coll.Contains("Key10"))
    End Sub

    <Fact>
    Public Sub Item_Get()
      Dim coll As Collection = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        Assert.Equal(CreateValue(i), coll(i + 1))
      Next

      Assert.Equal(CreateValue(5), coll(CObj(6)))
    End Sub

    <Fact>
    Public Sub Item_Get_InvalidIndex_ThrowsIndexOutOfRangeException()
      Dim coll As Collection = CreateCollection(10)

      Assert.Equal(CType(coll, IList)(-10), coll(1))    ' Indexing bug when accessing through the IList interface on non-empty collection

      Assert.Throws(Of IndexOutOfRangeException)(Function() coll(0)) ' Index <= 0
      Assert.Throws(Of IndexOutOfRangeException)(Function() coll(coll.Count + 1)) ' Index < 0
      Assert.Throws(Of ArgumentException)(Function() coll(CObj(Guid.Empty))) ' Neither string nor int
    End Sub

    <Fact>
    Public Sub Item_GetByKey()
      Dim coll As Collection = CreateKeyedCollection(10)
      For i As Integer = 0 To coll.Count - 1
        Assert.Equal(CreateValue(i), coll("Key" & i.ToString()))
      Next

      Assert.Equal(CreateValue(5), coll(CObj("Key5")))
      Assert.Equal(CreateValue(5), coll(CObj(New Char() {"K"c, "e"c, "y"c, "5"c})))

      coll.Add(CreateValue(11), "X")
      Assert.Equal(CreateValue(11), coll(CObj("X"c)))
    End Sub

    <Fact>
    Public Sub Item_GetByKey_InvalidIndex_ThrowsIndexOutOfRangeException()
      Dim coll As Collection = CreateKeyedCollection(10)

      Assert.Throws(Of ArgumentException)(Function() coll("Key20"))
      Assert.Throws(Of IndexOutOfRangeException)(Function() coll(CStr(Nothing)))
      Assert.Throws(Of IndexOutOfRangeException)(Function() coll(CObj(Nothing)))
    End Sub

    <Fact>
    Public Sub Item_Set()
      Dim coll As IList = CreateCollection(10)
      For i As Integer = 0 To coll.Count - 1
        Dim value As Community.VisualBasic.Tests.CollectionsTests.Foo = CreateValue(coll.Count - i)
        coll(i) = value
        Assert.Equal(value, coll(i))
      Next
    End Sub

    <Fact>
    Public Sub Item_Set_Invalid()
      Dim coll As IList = New Collection

      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll(-1) = New Foo) ' Index < 0
      Assert.Throws(Of ArgumentOutOfRangeException)("Index", Sub() coll(coll.Count + 1) = New Foo) ' Index >= InnerList.Count
    End Sub

    <Fact>
    Public Sub CopyTo()
      Dim coll As Collection = CreateCollection(10)

      ' Basic
      Dim fooArr As Community.VisualBasic.Tests.CollectionsTests.Foo() = New Foo(9) {}
      CType(coll, ICollection).CopyTo(fooArr, 0)

      Assert.Equal(coll.Count, fooArr.Length)
      For i As Integer = 0 To fooArr.Length - 1
        Assert.Equal(coll(i + 1), fooArr.GetValue(i))
      Next

      ' With index
      fooArr = New Foo(coll.Count * 2 - 1) {}
      CType(coll, ICollection).CopyTo(fooArr, coll.Count)

      For i As Integer = coll.Count To fooArr.Length - 1
        Assert.Equal(coll(i - coll.Count + 1), fooArr.GetValue(i))
      Next
    End Sub

    <Fact>
    Public Sub CopyTo_Invalid()
      Dim coll As ICollection = CreateCollection(10)
      Dim fooArr As Community.VisualBasic.Tests.CollectionsTests.Foo() = New Foo(9) {}
      ' Index < 0
      Assert.Throws(Of ArgumentException)(Sub() coll.CopyTo(fooArr, -1))
      ' Index + fooArray.Length > coll.Count
      Assert.Throws(Of ArgumentException)(Sub() coll.CopyTo(fooArr, 5))
    End Sub

    <Fact>
    Public Sub GetEnumerator()
      Dim coll As Collection = CreateCollection(10)
      Dim enumerator As IEnumerator = coll.GetEnumerator()
      Assert.NotNull(enumerator)

      Dim count1 As Integer = 0
      While enumerator.MoveNext()
        Assert.Equal(coll(count1 + 1), enumerator.Current)
        count1 += 1
      End While

      Assert.Equal(coll.Count, count1)
    End Sub

    <Fact>
    Public Sub GetEnumerator_PrePost()
      Dim coll As Collection = CreateCollection(10)
      Dim enumerator As IEnumerator = coll.GetEnumerator()

      ' Index <= 0
      Assert.Null(enumerator.Current)

      ' Index >= dictionary.Count
      While enumerator.MoveNext()
      End While
      Assert.Null(enumerator.Current)
      Assert.[False](enumerator.MoveNext())

      ' Current throws after resetting
      enumerator.Reset()
      Assert.[True](enumerator.MoveNext())

      enumerator.Reset()
      Assert.Null(enumerator.Current)
    End Sub

    <Fact>
    Public Sub SyncRoot()
      Dim coll As ICollection = New Collection
      Assert.Equal(coll.SyncRoot, coll)
    End Sub

    <Fact>
    Public Sub IListProperties()
      Dim coll As IList = CreateCollection(10)
      Assert.[False](coll.IsFixedSize)
      Assert.[False](coll.IsReadOnly)
      Assert.[False](coll.IsSynchronized)
    End Sub

    Private Class Foo

      Public Sub New()
      End Sub

      Public Sub New(intValue As Integer, stringValue As String)
        Me.IntValue = intValue
        Me.StringValue = stringValue
      End Sub

      Public Property IntValue As Integer
      Public Property StringValue As String
      Public Overrides Function Equals(obj As Object) As Boolean
        Dim foo1 As Foo = TryCast(obj, Foo)
        If foo1 Is Nothing Then
          Return False
        End If

        Return foo1.IntValue = IntValue AndAlso foo1.StringValue = StringValue
      End Function
      Public Overrides Function GetHashCode() As Integer
        Return IntValue
      End Function

    End Class

  End Class

End Namespace