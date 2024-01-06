# Community.VisualBasic

A very **experimental** alternate to the official [Microsoft.VisualBasic](https://github.com/dotnet/runtime/tree/master/src/libraries/Microsoft.VisualBasic.Core) runtime initially created to evaluate the support of the many ease-of-use features that makes Visual Basic, well, Visual Basic targetting .NET development for non-WinForms projects - especially *netstandard2.0*, *netstandard2.1* and, to some degree, .NET Console applications where cross-platform (Debian Linux / RaspPi) capability is desired.

Much of the common functionality one would *expect* there to exist as part of Visual Basic is **missing** if you desire to build a reusable library targeting *netstandard 2.x* forcing you have to potentially rewrite a lot of code and missing out on a much of what makes VB approachable/usable.  A lot of this isn't necessarily tied to WinForms and it would be nice to have regardless of building a netstandard library, console application (Windows or Linux) or WinForms.

## Why?

~~"We are not accepting feature contributions to Microsoft.VisualBasic.Core. The library is effectively archived.~~

~~The library and supporting language features are mature and no longer evolving, and the risk of code change likely exceeds the benefit. We will consider changes that address significant bugs or regressions, or changes that are necessary to continue shipping the binaries. Other changes will be rejected."~~

Unlike the project(s) that this is based upon, contributions are encouraged!  This isn't meant to be a knock on what Microsoft is or is not doing; to the contrary.  I envision this as an opportunity for the Visual Basic community to accept ownership in their own future.  I understand that Microsoft has limited resources (contrary to what commentors may think) and, ultimately, I believe strongly that the Visual Basic runtime is a great place for us to explore what the future of Visual Basic might mean (beyond core language structure).

## Not a Fork

As you may have noticed, this project is not a direct fork of [Microsoft.VisualBasic](https://github.com/dotnet/runtime/tree/master/src/libraries/Microsoft.VisualBasic.Core); this is on purpose.  This project is going to utilize the latest tools available to improve the code base as time progresses - meaning that some of the code will be "cleaned up" based on the suggestions provided directly in Visual Studio 2022 (and beyond).  Additionally, this project may eventually be split apart in order to better faciliate nuget packaging, cross-platform targeting, etc.  Trying to somehow maintain this codebase with the original source seems, at least to me, be impossible if these sorts of changes are desired in the long term.  Additionally, a different namespace across the project is needed in order to publish this as a nuget package as the namespace has to be something that isn't *reserved* - something else that I think pretty much breaks the possibility of having a fork maintained.

## Goal

The overall goal, at this stage, is to create a pretty complete implementation of the original [Microsoft.VisualBasic](https://github.com/dotnet/runtime/tree/master/src/libraries/Microsoft.VisualBasic.Core) namespace that works in .netstandard 2.x and, as much as possible, within a Console application running on Debian Linux.  I make sure to use the term *namespace* as *assembly*/*project* are probably a bit misleading given that functionality for this *namespace* appears to be implemented in at least three different projects (roslyn, dotnet and winforms).  Where possible, will try to implement everything that isn't on the following list:

- My.Computer.Keyboard.*
- My.Computer.Mouse.*
- My.Computer.Screen.*
- My.Computer.Clipboard.*
- My.Computer.Registry.*
- My.Forms
- MsgBox()

The above list is what I am currently aware of (off the top of my head) regarding functionality that is either very Windows specific (or more appropriately, WinForms specific).  Will evaluate implementing the above functionality where possible as we progress forward.

## Ideas / Thoughts for moving forward...

- Be able to create a new *netstandard2.0* project.  
- Easily build using Visual Studio 2022 (17.4+).  
- Easily leverage in any .NET project... including targeting the Blazor platform.
- It is a stated goal from the beginning that the project not be 100% compatible with .NET WinForms VisualBasic library as some things will have to give considering we are attempting to reach cross-platform capability; with that said, where possible compatibility will be strived for and maintained.
- In addition to throwing a New PlatformNotSupportedException, will leverage *obsolete* attributes for functionality that will not work so that the consumer of the package will know quickly that something will most likely fail.
- Review all code for functionality on, at minimum, Windows and Debian Linux - with primary evaluation of Linux debugging taking place directly in Visual Studio thanks to WSL2.  
- During this process, refactoring the code (where possible - without breaking functionality) so that default Visual Studio settings code analysis warnings are fixed. 
- Additional functionality may be added, TBD at a later date.

## Discord

Please feel to join us over on Discord:

- [Discord Invite](https://discord.gg/Y8EH5fF6WG)

## Future

Once this project is at the "a solid state" stage regarding **existing functionality**, we will be considering new functionality to take things into the future.  So if you have ideas that you think might be considered, please start the conversation in the discussions.

