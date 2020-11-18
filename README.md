# Community.VisualBasic

An alternate to the Microsoft.VisualBasic runtime initially created to support ease-of-use VisualBasic .NET 5 development for non-WinForms projects.

## Reason

There are actually a "few" Microsoft.VisualBasic runtimes depending on the type of project, type of platform target, etc.  There is a "core" version that has the absolute minimum necessary to compile a VB project.  Then there a is the slightly more robust (minimal) one that was originally utilized in .NET Core 1.x and growing (I believe) slightly for .NET Core 3.1 projects.  When the launch of .NET 5, a pretty complete version is available... if you are building a WinForms project.

However, much of the common functionality one would *expect* there to exist as part of VisualBasic is **missing** if you desire to build a reusable library targeting *netstardard* (at the time of this writing, any level) forcing you have to potentially rewrite a lot of code and missing out on a much of what makes VB approachable/usable.  A lot of this isn't necessarily tied to WinForms and would be nice to have whether you are building a netstardard library, console appliction for Linux or WinForms.

## General Ideas

- Be able to create a new netstardard (2.1, possibly earlier) project.  Initial project will start with 2.1... after significant work, will attempt to roll-back to test how low we can go.
- Easily build using Visual Studio 16.8+.
- Easily leverage in any .NET 5 project... including targeting the Blazor platform.
- It is a stated goal from the beginning that the project not be 100% compatible with .NET 5 WinForms VisualBasic library as some things will have to give considering we are attempting to reach cross-platform capability; with that said, where possible compatibility will be strived for and maintained.
- Additional functionality may be added where it makes sense specifically with regard to supporting more easily converting code from C# to VB.

## Further Discussions

In addition to the tools provided here on GitHub, feel free to join the general conversations taking place over at https://gitter.im/VB-NET/Language.

## TODO

There's a ton related to this project that is on the TODO list... will evolve as the project evolves.
