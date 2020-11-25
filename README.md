# Community.VisualBasic

A very **experimental** alternate to the official Microsoft.VisualBasic runtime initially created to evaluate the support of the many ease-of-use features that makes Visual Basic, well, Visual Basic in .NET 5 development for non-WinForms projects - especially *netstandard2.0*, *netstandard2.1* and, to some degree, .NET 5 Console applications where cross-platform capability is desired.

## Reason

There are actually a "few" Microsoft.VisualBasic runtimes depending on the type of project, type of platform target, etc.  There is a "core" version that has the absolute minimum necessary to compile a VB project.  Then there a is the slightly more robust (minimal) one that was originally utilized in .NET Core 1.x and growing (I believe) slightly for .NET Core 3.1 projects.  When the launch of .NET 5, a pretty complete version is available... if you are building a WinForms project.

However, much of the common functionality one would *expect* there to exist as part of Visual Basic is **missing** if you desire to build a reusable library targeting *netstardard* (at the time of this writing, any level) forcing you have to potentially rewrite a lot of code and missing out on a much of what makes VB approachable/usable.  A lot of this isn't necessarily tied to WinForms and would be nice to have whether you are building a netstandard library, console application for Linux or WinForms.

## Initial Observations

- With the work already done, it is obvious that (a rough estimate of) 85% or more of the code will "just work". 
- There are obviously areas of functionality that will not work *cross-platform* and, as such, will not work in a *netstandard2.0* project.  This includes anything that is WinForms-related related, Registry-related and other areas that are very Windows specific.  Other possibilities is functionality that just doesn't exist in .NET 5 (clearly legacy functionality).  I estimate that this is around 10% (or less).
- The other percentage (5-10%) is functionality that is currently designed for Windows but could easily be adjusted to support cross-platform development.

## Ideas / Thoughts for moving forward...

- Be able to create a new *netstardard2.0* project.  
- Easily build using Visual Studio 16.8+.  
- Easily leverage in any .NET 5 project... including targeting the Blazor platform.
- It is a stated goal from the beginning that the project not be 100% compatible with .NET 5 WinForms VisualBasic library as some things will have to give considering we are attempting to reach cross-platform capability; with that said, where possible compatibility will be strived for and maintained.
- Rather than throw a New PlatformNotSupportedException, will *ifdef* the functionality out for the time being; I suspect that it would be better to tell the consumer of the library that something doesn't work immediately rather than have to test everything for functionality.  With that said, will review this later and possibly leverage attributes.
- Review all code for functionality on, at minimum, Windows and Debian Linux - with primary evaluation of Linux debugging taking place directly in Visual Studio thanks to WSL2.  
- Evaluate using the same namespace as the original.  Currently experimenting with code under the Community.VisualBasic namespace; there is the possibility that could utilize Microsoft.VisualBasic as the namespace and do some substitution magic in the project file.  Need to explore this more.
- During this process, refactoring the code (where possible - without breaking functionality) so that default Visual Studio settings code analysis warnings are fixed. 
- Additional functionality may be added, TBD at a later date.

## Further Discussion

In addition to the tools provided here on GitHub, feel free to join the general conversations taking place over at https://gitter.im/VB-NET/Language.