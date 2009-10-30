using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AudioPlayer")]
[assembly: AssemblyDescription("Silverlight Audio Player")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AudioPlayer")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d581151a-3394-4e31-abd0-2df4a67903f3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("0.28.0.0")]
[assembly: AssemblyFileVersion("0.28.0.0")]

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
// v0.16 2 Apr 2008
// A "progress bar" control that can be used to display download progress
// Multi-player shows download progress
// v0.17 3 Apr 2008
// AudioControls and TestHarness merged into one Silverlight app
// v0.18 7 Jun 2008
// Conversion to Silverlight 2 beta 2
// v0.19 8 Jun 2008
// Switching control templating to use new visual state manager
// had to remove the "Element" off the component names to get sliders working again
// Moved styles into App.xaml
// v0.20 11 Oct 2008
// updated to Silverlight 2 RC0
// v0.21 14 Oct 2008
// updated to Silverlight 2 RTW
// v0.22 19 Dec 2008
// Some refactoring of app entry point and changes to MultiPlayer ListBox style
// v0.23 23 Dec 2008
// improvements to MultiPlayer's transport button and volume slider styles. TextScroller better timing logic and scrolls from right hand edge in all cases.
// v0.24 6 Jan 2009
// significant improvements to SimplePlayer's style
// v0.24 16 Jan 2009
// SinglePlayer now sized better and with a transparent background, using Static Resources for brushes on Simple Player
// v0.25 1 Jul 2009
// fixed a minor XAML error
// v0.26 26 Aug 2009
// test website now able to find default playlist location
// v0.27 14 Sep 2009
// Simpler Url, Title and Artist specification for single audio file player
// Demo website now explains how to use the control, will be used as the basis for the help on the Wiki
// making a sample html file for binary release
// cleaning up example html file
// v0.28 30 Oct 2009
// Addition of new Volume Slider control to simple player
// Documentation page uses object tag instead of (now obsolete?) asp:Silverlight control
// Supports an Alternative "Path=" syntax instead of "Url=" to make it work nice with WikiPlex

// Tasks:
// Get everything into one Silverlight assembly
// Improve scrolling text control
// Interface implemented by each player
// Playlist.xml to allow selection & configuration of players
// Factor out MediaElement control logic into a separate class?
// RegisterScriptableObject should be done at Page Level
// Custom template for volume slider
// Control to display track title and author
// Consider fading out volume on pause & fading in on play
// HTML access to scriptable properties
