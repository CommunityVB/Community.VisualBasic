' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Imports System
Imports System.Runtime.InteropServices

Namespace Global.Community.VisualBasic.CompilerServices

  <ComVisible(False)>
  Friend NotInheritable Class UnsafeNativeMethods

    ''' <summary>
    ''' Used to determine how much free space is on a disk
    ''' </summary>
    ''' <param name="Directory">Path including drive we're getting information about</param>
    ''' <param name="UserSpaceFree">The amount of free sapce available to the current user</param>
    ''' <param name="TotalUserSpace">The total amount of space on the disk relative to the current user</param>
    ''' <param name="TotalFreeSpace">The amount of free spave on the disk.</param>
    ''' <returns>True if function succeeds in getting info otherwise False</returns>
    <DllImport("Kernel32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, SetLastError:=True)>
    Friend Shared Function GetDiskFreeSpaceEx(Directory As String, ByRef UserSpaceFree As Long, ByRef TotalUserSpace As Long, ByRef TotalFreeSpace As Long) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


    <PreserveSig()>
    Friend Declare Ansi Function LCMapStringA _
            Lib "kernel32" Alias "LCMapStringA" (Locale As Integer, dwMapFlags As Integer,
                <MarshalAs(UnmanagedType.LPArray)> lpSrcStr As Byte(), cchSrc As Integer, <MarshalAs(UnmanagedType.LPArray)> lpDestStr As Byte(), cchDest As Integer) As Integer

    <PreserveSig()>
    Friend Declare Auto Function LCMapString _
            Lib "kernel32" (Locale As Integer, dwMapFlags As Integer,
lpSrcStr As String, cchSrc As Integer, lpDestStr As String, cchDest As Integer) As Integer

    <DllImport("oleaut32", PreserveSig:=True, CharSet:=CharSet.Unicode, EntryPoint:="VarParseNumFromStr")>
    Friend Shared Function VarParseNumFromStr(
            <[In](), MarshalAs(UnmanagedType.LPWStr)> str As String,
lcid As Integer,
dwFlags As Integer,
            <MarshalAs(UnmanagedType.LPArray)> numprsPtr As Byte(),
            <MarshalAs(UnmanagedType.LPArray)> digits As Byte()) As Integer
    End Function

    <DllImport("oleaut32", PreserveSig:=False, CharSet:=CharSet.Unicode, EntryPoint:="VarNumFromParseNum")>
    Friend Shared Function VarNumFromParseNum(
            <MarshalAs(UnmanagedType.LPArray)> numprsPtr As Byte(),
            <MarshalAs(UnmanagedType.LPArray)> DigitArray As Byte(),
dwVtBits As Int32) As Object
    End Function

    <DllImport("oleaut32", PreserveSig:=False, CharSet:=CharSet.Unicode, EntryPoint:="VariantChangeType")>
    Friend Shared Sub VariantChangeType(
        <Out()> ByRef dest As Object,
        <[In]()> ByRef Src As Object,
wFlags As Int16,
vt As Int16)
    End Sub

    <DllImport("user32", PreserveSig:=True, CharSet:=CharSet.Unicode, EntryPoint:="MessageBeep")>
    Friend Shared Function MessageBeep(uType As Integer) As Integer
    End Function

    <DllImport("kernel32", PreserveSig:=True, CharSet:=CharSet.Unicode, EntryPoint:="SetLocalTime", SetLastError:=True)>
    Friend Shared Function SetLocalTime(systime As NativeTypes.SystemTime) As Integer
    End Function

    <DllImport("kernel32", PreserveSig:=True, CharSet:=CharSet.Auto, EntryPoint:="MoveFile", BestFitMapping:=False, ThrowOnUnmappableChar:=True, SetLastError:=True)>
    Friend Shared Function MoveFile(<[In](), MarshalAs(UnmanagedType.LPTStr)> lpExistingFileName As String,
            <[In](), MarshalAs(UnmanagedType.LPTStr)> lpNewFileName As String) As Integer
    End Function

    <DllImport("kernel32", PreserveSig:=True, CharSet:=CharSet.Unicode, EntryPoint:="GetLogicalDrives")>
    Friend Shared Function GetLogicalDrives() As Integer
    End Function

    Public Const LCID_US_ENGLISH As Integer = &H409

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)>
    Public Enum TagSYSKIND
      SYS_WIN16 = 0
      SYS_MAC = 2
    End Enum

    ' REVIEW :  - c# version was class, does it make a difference?
    '    [StructLayout(LayoutKind.Sequential)]
    '    Public class  tagTLIBATTR {
    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)>
    Public Structure TagTLIBATTR
      Public guid As Guid
      Public lcid As Integer
      Public syskind As TagSYSKIND
      <MarshalAs(UnmanagedType.U2)> Public wMajorVerNum As Short
      <MarshalAs(UnmanagedType.U2)> Public wMinorVerNum As Short
      <MarshalAs(UnmanagedType.U2)> Public wLibFlags As Short
    End Structure

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),
     ComImport(),
     Guid("00020403-0000-0000-C000-000000000046"),
     InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface ITypeComp

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteBind(
             <[In](), MarshalAs(UnmanagedType.LPWStr)> szName As String,
             <[In](), MarshalAs(UnmanagedType.U4)> lHashVal As Integer,
             <[In](), MarshalAs(UnmanagedType.U2)> wFlags As Short,
             <Out(), MarshalAs(UnmanagedType.LPArray)> ppTInfo As ITypeInfo(),
             <Out(), MarshalAs(UnmanagedType.LPArray)> pDescKind As ComTypes.DESCKIND(),
             <Out(), MarshalAs(UnmanagedType.LPArray)> ppFuncDesc As ComTypes.FUNCDESC(),
             <Out(), MarshalAs(UnmanagedType.LPArray)> ppVarDesc As ComTypes.VARDESC(),
             <Out(), MarshalAs(UnmanagedType.LPArray)> ppTypeComp As ITypeComp(),
             <Out(), MarshalAs(UnmanagedType.LPArray)> pDummy As Integer())

      Sub RemoteBindType(
             <[In](), MarshalAs(UnmanagedType.LPWStr)> szName As String,
             <[In](), MarshalAs(UnmanagedType.U4)> lHashVal As Integer,
             <Out(), MarshalAs(UnmanagedType.LPArray)> ppTInfo As ITypeInfo())
    End Interface

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),
     ComImport(),
     Guid("00020400-0000-0000-C000-000000000046"),
     InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IDispatch

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function GetTypeInfoCount() As Integer

      <PreserveSig()>
      Function GetTypeInfo(
              <[In]()> index As Integer,
              <[In]()> lcid As Integer,
              <[Out](), MarshalAs(UnmanagedType.Interface)> ByRef pTypeInfo As ITypeInfo) As Integer

      ' WARNING :  - This api NOT COMPLETELY DEFINED, DO NOT CALL!
      <PreserveSig()>
      Function GetIDsOfNames() As Integer

      ' WARNING :  - This api NOT COMPLETELY DEFINED, DO NOT CALL!
      <PreserveSig()>
      Function Invoke() As Integer
    End Interface

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),
     ComImport(),
     Guid("00020401-0000-0000-C000-000000000046"),
     InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface ITypeInfo
      <PreserveSig()>
      Function GetTypeAttr(
              <Out()> ByRef pTypeAttr As IntPtr) As Integer

      <PreserveSig()>
      Function GetTypeComp(
              <Out()> ByRef pTComp As ITypeComp) As Integer


      <PreserveSig()>
      Function GetFuncDesc(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out()> ByRef pFuncDesc As IntPtr) As Integer

      <PreserveSig()>
      Function GetVarDesc(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out()> ByRef pVarDesc As IntPtr) As Integer

      <PreserveSig()>
      Function GetNames(
              <[In]()> memid As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> rgBstrNames As String(),
              <[In](), MarshalAs(UnmanagedType.U4)> cMaxNames As Integer,
              <Out(), MarshalAs(UnmanagedType.U4)> ByRef cNames As Integer) As Integer

      <Obsolete("Bad signature, second param type should be Byref. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function GetRefTypeOfImplType(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out()> ByRef pRefType As Integer) As Integer

      <Obsolete("Bad signature, second param type should be Byref. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function GetImplTypeFlags(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out()> pImplTypeFlags As Integer) As Integer

      <PreserveSig()>
      Function GetIDsOfNames(
              <[In]()> rgszNames As IntPtr,
              <[In](), MarshalAs(UnmanagedType.U4)> cNames As Integer,
              <Out()> ByRef pMemId As IntPtr) As Integer

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function Invoke() As Integer

      <PreserveSig()>
      Function GetDocumentation(
               <[In]()> memid As Integer,
               <Out(), MarshalAs(UnmanagedType.BStr)> ByRef pBstrName As String,
               <Out(), MarshalAs(UnmanagedType.BStr)> ByRef pBstrDocString As String,
               <Out(), MarshalAs(UnmanagedType.U4)> ByRef pdwHelpContext As Integer,
               <Out(), MarshalAs(UnmanagedType.BStr)> ByRef pBstrHelpFile As String) As Integer

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function GetDllEntry(
              <[In]()> memid As Integer,
              <[In]()> invkind As ComTypes.INVOKEKIND,
              <Out(), MarshalAs(UnmanagedType.BStr)> pBstrDllName As String,
              <Out(), MarshalAs(UnmanagedType.BStr)> pBstrName As String,
              <Out(), MarshalAs(UnmanagedType.U2)> pwOrdinal As Short) As Integer

      <PreserveSig()>
      Function GetRefTypeInfo(
               <[In]()> hreftype As IntPtr,
               <Out()> ByRef pTypeInfo As ITypeInfo) As Integer

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function AddressOfMember() As Integer

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function CreateInstance(
              <[In]()> ByRef pUnkOuter As IntPtr,
              <[In]()> ByRef riid As Guid,
              <Out(), MarshalAs(UnmanagedType.IUnknown)> ppvObj As Object) As Integer

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      <PreserveSig()>
      Function GetMops(
              <[In]()> memid As Integer,
              <Out(), MarshalAs(UnmanagedType.BStr)> pBstrMops As String) As Integer

      <PreserveSig()>
      Function GetContainingTypeLib(
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTLib As ITypeLib(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pIndex As Integer()) As Integer

      <PreserveSig()>
      Sub ReleaseTypeAttr(typeAttr As IntPtr)

      <PreserveSig()>
      Sub ReleaseFuncDesc(funcDesc As IntPtr)

      <PreserveSig()>
      Sub ReleaseVarDesc(varDesc As IntPtr)
    End Interface

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),
     ComImport(),
     Guid("B196B283-BAB4-101A-B69C-00AA00341D07"),
     InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IProvideClassInfo
      Function GetClassInfo() As <MarshalAs(UnmanagedType.Interface)> ITypeInfo
    End Interface

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),
     ComImport(),
     Guid("00020402-0000-0000-C000-000000000046"),
     InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface ITypeLib
      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteGetTypeInfoCount(
              <Out(), MarshalAs(UnmanagedType.LPArray)> pcTInfo As Integer())

      Sub GetTypeInfo(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTInfo As ITypeInfo())

      Sub GetTypeInfoType(
              <[In](), MarshalAs(UnmanagedType.U4)> index As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> pTKind As ComTypes.TYPEKIND())

      Sub GetTypeInfoOfGuid(
              <[In]()> ByRef guid As Guid,
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTInfo As ITypeInfo())

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteGetLibAttr(
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTLibAttr As tagTLIBATTR(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pDummy As Integer())

      Sub GetTypeComp(
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTComp As ITypeComp())

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteGetDocumentation(
index As Integer,
              <[In](), MarshalAs(UnmanagedType.U4)> refPtrFlags As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> pBstrName As String(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pBstrDocString As String(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pdwHelpContext As Integer(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pBstrHelpFile As String())

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteIsName(
              <[In](), MarshalAs(UnmanagedType.LPWStr)> szNameBuf As String,
              <[In](), MarshalAs(UnmanagedType.U4)> lHashVal As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> pfName As IntPtr(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pBstrLibName As String())

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub RemoteFindName(
              <[In](), MarshalAs(UnmanagedType.LPWStr)> szNameBuf As String,
              <[In](), MarshalAs(UnmanagedType.U4)> lHashVal As Integer,
              <Out(), MarshalAs(UnmanagedType.LPArray)> ppTInfo As ITypeInfo(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> rgMemId As Integer(),
              <[In](), Out(), MarshalAs(UnmanagedType.LPArray)> pcFound As Short(),
              <Out(), MarshalAs(UnmanagedType.LPArray)> pBstrLibName As String())

      <Obsolete("Bad signature. Fix and verify signature before use.", True)>
      Sub LocalReleaseTLibAttr()
    End Interface

    ''' <summary>
    ''' Frees memory allocated from the local heap. i.e. frees memory allocated
    ''' by LocalAlloc or LocalReAlloc.n
    ''' </summary>
    ''' <param name="LocalHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("kernel32", ExactSpelling:=True, SetLastError:=True)>
    Friend Shared Function LocalFree(LocalHandle As IntPtr) As IntPtr
    End Function
  End Class
End Namespace
