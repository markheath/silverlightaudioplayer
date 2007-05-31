using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SilverlightAudioPlayer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mark Heath")]
[assembly: AssemblyProduct("SilverlightAudioPlayer")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2007")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3d5900ae-111a-45be-96b3-d9e4606ca793")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("0.4.0.0")]
[assembly: AssemblyFileVersion("0.4.0.0")]

// v0.1 25 May 2007
// very basic version can play and pause
// v0.2 
// created an AudioPositionSlider control
// v0.3 31 May 2007
// finally got collapsing working correctly
// imported the Silverlight Sample Controls project to use as base classes
// Created a progress slider control based on the sample slider
// Added a basic play button icon
// v0.4 
// Got rid of AudioPositionSlider in favour of ProgressSlider
// improved resizing of ProgressSlider
// Modified the Slider control to get our ProgressThumb positioning right, may
// need to revisit later to see if Slider is now broken
// Play and pause icons showing correctly
// Now Play, Pause and Media URL are scriptable

// Tasks:
// Control to display track title and author
// Consider fading out volume on pause & fading in on play
// HTML access to scriptable properties
// Investigate adding a simple volume slider