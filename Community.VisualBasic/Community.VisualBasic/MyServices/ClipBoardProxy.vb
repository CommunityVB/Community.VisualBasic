' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Option Explicit On
Option Strict On

Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
'Imports System.Windows.Forms

Namespace Global.Community.VisualBasic.MyServices

  ''' <summary>
  ''' A class that wraps System.Windows.Forms.Clipboard so that
  ''' a clipboard can be instanced.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Never)>
  Public Class ClipboardProxy

    ''' <summary>
    ''' Only Allows instantiation of the class
    ''' </summary>
    Friend Sub New()
    End Sub

    ''' <summary>
    ''' Gets text from the clipboard
    ''' </summary>
    ''' <returns>The text as a String</returns>
    Public Function GetText() As String
      Throw New NotImplementedException
#If False Then
      Return Clipboard.GetText()
#End If
    End Function

#If False Then

    ''' <summary>
    ''' Gets text from the clipboard saved in the passed in format
    ''' </summary>
    ''' <param name="format">The type of text to get</param>
    ''' <returns>The text as a String</returns>
    Public Function GetText(format As System.Windows.Forms.TextDataFormat) As String
      Return Clipboard.GetText(format)
    End Function

#End If

    ''' <summary>
    ''' Indicates whether or not text is available on the clipboard
    ''' </summary>
    ''' <returns>True if text is available, otherwise False</returns>
    Public Function ContainsText() As Boolean
      Throw New NotImplementedException
#If False Then
      Return Clipboard.ContainsText
#End If
    End Function

#If False Then

    ''' <summary>
    ''' Indicates whether or not text is available on the clipboard in 
    ''' the passed in format
    ''' </summary>
    ''' <param name="format">The type of text being checked for</param>
    ''' <returns>True if text is available, otherwise False</returns>
    Public Function ContainsText(format As System.Windows.Forms.TextDataFormat) As Boolean
      Return Clipboard.ContainsText(format)
    End Function

#End If

    ''' <summary>
    ''' Saves the passed in String to the clipboard
    ''' </summary>
    ''' <param name="text">The String to save</param>
    Public Sub SetText(text As String)
      Throw New NotImplementedException
#If False Then
      Clipboard.SetText(text)
#End If
    End Sub

#If False Then

    ''' <summary>
    ''' Saves the passed in String to the clipboard in the passed in format
    ''' </summary>
    ''' <param name="text">The String to save</param>
    ''' <param name="format">The format in which to save the String</param>
    Public Sub SetText(text As String, format As System.Windows.Forms.TextDataFormat)
      Clipboard.SetText(text, format)
    End Sub

    ''' <summary>
    ''' Gets an Image from the clipboard
    ''' </summary>
    ''' <returns>The image</returns>
    Public Function GetImage() As Image
      Return Clipboard.GetImage()
    End Function
#End If

    ''' <summary>
    ''' Indicate whether or not an image has been saved to the clipboard
    ''' </summary>
    ''' <returns>True if an image is available, otherwise False</returns>
    Public Function ContainsImage() As Boolean
      Throw New NotImplementedException
#If False Then
      Return Clipboard.ContainsImage()
#End If
    End Function

#If False Then
    ''' <summary>
    ''' Saves the passed in image to the clipboard
    ''' </summary>
    ''' <param name="image">The image to be saved</param>
    Public Sub SetImage(image As Image)
      Clipboard.SetImage(image)
    End Sub
#End If

    ''' <summary>
    ''' Gets an audio stream from the clipboard
    ''' </summary>
    ''' <returns>The audio stream as a Stream</returns>
    Public Function GetAudioStream() As Stream
      Throw New NotImplementedException
#If False Then
      Return Clipboard.GetAudioStream()
#End If
    End Function

    ''' <summary>
    ''' Indicates whether or not there's an audio stream saved to the clipboard
    ''' </summary>
    ''' <returns>True if an audio stream is available, otherwise False</returns>
    Public Function ContainsAudio() As Boolean
      Throw New NotImplementedException
#If False Then
      Return Clipboard.ContainsAudio()
#End If
    End Function

    ''' <summary>
    ''' Saves the passed in audio byte array to the clipboard
    ''' </summary>
    ''' <param name="audioBytes">The byte array to be saved</param>
    Public Sub SetAudio(audioBytes As Byte())
      Throw New NotImplementedException
#If False Then
      Clipboard.SetAudio(audioBytes)
#End If
    End Sub

    ''' <summary>
    ''' Saves the passed in audio stream to the clipboard
    ''' </summary>
    ''' <param name="audioStream">The stream to be saved</param>
    Public Sub SetAudio(audioStream As Stream)
      Throw New NotImplementedException
#If False Then
      Clipboard.SetAudio(audioStream)
#End If
    End Sub

    ''' <summary>
    ''' Gets a file drop list from the clipboard
    ''' </summary>
    ''' <returns>The list of file paths as a StringCollection</returns>
    Public Function GetFileDropList() As StringCollection
      Throw New NotImplementedException
#If False Then
      Return Clipboard.GetFileDropList()
#End If
    End Function

    ''' <summary>
    ''' Indicates whether or not a file drop list has been saved to the clipboard
    ''' </summary>
    ''' <returns>True if a file drop list is available, otherwise False</returns>
    Public Function ContainsFileDropList() As Boolean
      Throw New NotImplementedException
#If False Then
      Return Clipboard.ContainsFileDropList()
#End If
    End Function

    ''' <summary>
    ''' Saves the passed in file drop list to the clipboard
    ''' </summary>
    ''' <param name="filePaths">The file drop list as a StringCollection</param>
    Public Sub SetFileDropList(filePaths As StringCollection)
      Throw New NotImplementedException
#If False Then
      Clipboard.SetFileDropList(filePaths)
#End If
    End Sub

    ''' <summary>
    ''' Gets data from the clipboard that's been saved in the passed in format.
    ''' </summary>
    ''' <param name="format">The type of data being sought</param>
    ''' <returns>The data</returns>
    Public Function GetData(format As String) As Object
      Throw New NotImplementedException
#If False Then
      Return Clipboard.GetData(format)
#End If
    End Function

    ''' <summary>
    ''' Indicates whether or not there is data on the clipboard in the passed in format
    ''' </summary>
    ''' <param name="format"></param>
    ''' <returns>True if there's data in the passed in format, otherwise False</returns>
    Public Function ContainsData(format As String) As Boolean
      Throw New NotImplementedException
#If False Then
      Return Clipboard.ContainsData(format)
#End If
    End Function

    ''' <summary>
    ''' Saves the passed in data to the clipboard in the passed in format
    ''' </summary>
    ''' <param name="format">The format in which to save the data</param>
    ''' <param name="data">The data to be saved</param>
    Public Sub SetData(format As String, data As Object)
      Throw New NotImplementedException
#If False Then
      Clipboard.SetData(format, data)
#End If
    End Sub

    ''' <summary>
    ''' Removes everything from the clipboard
    ''' </summary>
    Public Sub Clear()
      Throw New NotImplementedException
#If False Then
      Clipboard.Clear()
#End If
    End Sub

#If False Then

    ''' <summary>
    ''' Gets a Data Object from the clipboard.
    ''' </summary>
    ''' <returns>The data object</returns>
    ''' <remarks>This gives the ability to save an object in multiple formats</remarks>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Function GetDataObject() As System.Windows.Forms.IDataObject
      Return Clipboard.GetDataObject()
    End Function

    ''' <summary>
    ''' Saves a DataObject to the clipboard
    ''' </summary>
    ''' <param name="data">The data object to be saved</param>
    ''' <remarks>This gives the ability to save an object in multiple formats</remarks>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Sub SetDataObject(data As System.Windows.Forms.DataObject)
      Clipboard.SetDataObject(data)
    End Sub

#End If

  End Class

End Namespace