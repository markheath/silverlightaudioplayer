using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AudioControls")]
[assembly: AssemblyDescription("Silverlight Audio Controls")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mark Heath")]
[assembly: AssemblyProduct("AudioControls")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("612da658-6241-43be-8491-35e8131584d0")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("0.15.0.0")]
[assembly: AssemblyFileVersion("0.15.0.0")]

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
// v0.5 4 June 2007
// Can script play and URL from HTML test page now
// More error handling
// v0.6 30 July 2007
// Updated to work with Silverlight 1.1 Refresh
// Made the overall size of the control smaller
// v0.7 10 Mar 2008
// Updated to work with Silverlight 2.0 Beta
// v0.8 11 Mar 2008
// Animated Speaker is now a user control
// v0.9 18 Mar 2008
// Improvements to size and positioning
// Beginnings of MultiPlayer
// v0.10 20 Mar 2008
// Custom button template with icons for MultiPlayer
// v0.11 28 Mar 2008
// Can use initialise parameters to choose a player to play
// Added a website to enable us to load playlists via XML (not sure if I really needed to do this)
// v0.12 31 Mar 2008
// Multi-player using data-template for 
// Multi-player has media element and can play selected item
// Simple player can work off a playlist too
// v0.13 31 Mar 2008
// Simple and multi-player now both can work with Playlists
// Root visual not set until we have a playlist
// "Oops" control for playlist not found
// Looks for default playlist if not specified in initParams
// v0.14 1 Apr 2008
// Beginnings of a TextScoller control
// Now looks in same folder for default playlist to allow use on Silverlight Streaming
// v0.15 2 Apr 2008
// Style for a volume slider
// Volume slider now controls volume 
// Multi-player uses text scroller

// Tasks:
// Get everything into one Silverlight assembly
// Scrolling text user control
// Interface implemented by each player
// Playlist.xml to allow selection & configuration of players
// Factor out MediaElement control logic into a separate class?
// RegisterScriptableObject should be done at Page Level
// Custom template for volume slider
// Control to display track title and author
// Consider fading out volume on pause & fading in on play
// HTML access to scriptable properties