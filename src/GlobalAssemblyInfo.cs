using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("MutoMark")]
[assembly: AssemblyDescription("A MarkDown Viewer for Windows")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("David Muto")]
[assembly: AssemblyProduct("MutoMark")]
[assembly: AssemblyCopyright("Copyright © David Muto 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(Version.NUMBER + ".*")]
[assembly: AssemblyInformationalVersion(Version.NUMBER)]
[assembly: AssemblyFileVersion(Version.NUMBER + ".0")]

class Version
{
    public const string NUMBER = "0.1.1";
}