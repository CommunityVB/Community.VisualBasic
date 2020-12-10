' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Xunit

' Tests that use ProjectData or AssemblyData rely on shared state
' and should not be run in parallel.
<Assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization:=True, MaxParallelThreads:=1)>
