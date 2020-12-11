' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Imports System.Collections.Generic
Imports System.Runtime.InteropServices

Namespace Global.Community.VisualBasic.CompilerServices

  ' Implements a MRU collection for caching dynamic methods used in IDO late binding.
  Friend Class CacheDict(Of TKey, TValue)
    ' The Dictionary to quickly access cached data
    Private ReadOnly _dict As Dictionary(Of TKey, KeyInfo)
    ' MRU sorted linked list
    Private ReadOnly _list As LinkedList(Of TKey)
    ' Maximum size
    Private ReadOnly _maxSize As Integer

    Friend Sub New(maxSize As Integer)
      _dict = New Dictionary(Of TKey, KeyInfo)
      _list = New LinkedList(Of TKey)
      _maxSize = maxSize
    End Sub

    Friend Sub Add(key As TKey, value As TValue)
      Dim info As New KeyInfo
      If _dict.TryGetValue(key, info) Then
        ' If the key is already in the collection, remove it
        _list.Remove(info.List)
      ElseIf (_list.Count = _maxSize) Then
        ' Age out the last element if we hit the max size
        Dim last As LinkedListNode(Of TKey) = _list.Last
        _list.RemoveLast()
        _dict.Remove(last.Value)
      End If

      ' Add the new element
      Dim node As New LinkedListNode(Of TKey)(key)
      _list.AddFirst(node)
      _dict.Item(key) = New KeyInfo(value, node)
    End Sub

    Friend Function TryGetValue(key As TKey, <Out()> ByRef value As TValue) As Boolean
      Dim info As New KeyInfo
      If _dict.TryGetValue(key, info) Then
        Dim list As LinkedListNode(Of TKey) = info.List
        If (list.Previous IsNot Nothing) Then
          _list.Remove(list)
          _list.AddFirst(list)
        End If
        value = info.Value
        Return True
      End If
      value = Nothing
      Return False
    End Function

    ' KeyInfo to store in the dictionary
    Private Structure KeyInfo
      Friend ReadOnly Value As TValue
      Friend ReadOnly List As LinkedListNode(Of TKey)

      Friend Sub New(v As TValue, l As LinkedListNode(Of TKey))
        Value = v
        List = l
      End Sub
    End Structure
  End Class

End Namespace