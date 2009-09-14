<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls" TagPrefix="asp" %>    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Silverlight Audio Player Demo Page</title>
    <link href="style.css" rel="stylesheet" type="text/css">

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    <h2>Silverlight Audio Player</h2>
    <p>Silverlight Audio Player is a simple audio player that can be used for playing back one or more
    audio files. It currently supports two styles of player. The first plays a single file and is based
    on the visual design of the <a href="http://wordpress.org/extend/plugins/audio-player/">Wordpress audio player</a>.
    The second allows you to play multiple files.</p>
    
    <h3>Installation</h3>
    <p>You need the AudioPlayer.xap file, and optionally an XML playlist file. Once you have those, you can
    load the Silverlight application in the same way you would any other. Remember to set up the size appropriately,
    and pass in the MP3 file Url or Playlist Url in the Initparams (see below).  By convention, Visual Studio likes
    XAP files to be stored in a ClientBin folder. You do not need to do this, and if you don't, you will need to adjust the
    relative paths from the examples shown below (i.e. no need for ..\ on your Urls).</p>
    
    <h3>Single Player Demo</h3>
    <p>To use the single player, simply pass init params to your silverlight object. The single player should be sized
    295 pixels wide and 30 pixels high.</p>
    
    <p><b>InitParams:</b> <i>Url=http://www.wordandspirit.co.uk/blog/audio/dont-look-at-me.mp3,Artist=Mark Heath,Title=Don't look at me</i></p>
    <div style="width:295px; height: 30px;">

        <asp:Silverlight ID="Demo1" runat="server" 
            InitParams="Url=http://www.wordandspirit.co.uk/blog/audio/dont-look-at-me.mp3,Artist=Mark Heath,Title=Don't look at me"
            Source="~/ClientBin/AudioPlayer.xap" 
            Version="2.0" Width="100%" Height="100%" />
    </div>
    
    <h3>Single Player Demo 2</h3>
    
    <p>Unfortunately, Silverlight cannot read the ID3 tags from MP3 files, so if you omit the Title and Artist tags, just
    the URL will be displayed.</p>
    
    <p><b>InitParams:</b> <i>Url=http://www.wordandspirit.co.uk/blog/audio/you-have-always-given.mp3</i></p>
    <div style="width:295px; height: 30px;">

        <asp:Silverlight ID="Demo2" runat="server" 
            InitParams="Url=http://www.wordandspirit.co.uk/blog/audio/you-have-always-given.mp3"
            Source="~/ClientBin/AudioPlayer.xap" 
            Version="2.0" Width="100%" Height="100%" />
    </div>
    
    <h3>Multiple File Player Demo</h3>
    
    <p>To launch the player with multiple files, you need to point it at a playlist. A playlist is
    an XML file containing the Url, Artist and Title for each track you wish to play.</p>
    
    <p><i>n.b. if your playlist only contains a single file, the single player will load.</i></p>
    
    <div class="xml">
        &lt;?xml version="1.0" encoding="utf-8" ?&gt; <br />
        &lt;playlist&gt; <br />
&nbsp;&nbsp;    &lt;audiofile url="http://www.markheath.me.uk/music/markheath+oppositionandjoy.mp3" title="Opposition and Joy" artist="Mark Heath" /&gt; <br />
&nbsp;&nbsp;    &lt;audiofile url="http://www.markheath.me.uk/music/markheath+youhavealwaysgiven.mp3" title="You have always given" artist="Mark Heath" /&gt; <br />
&nbsp;&nbsp;    &lt;audiofile url="http://www.markheath.me.uk/music/markheath+dontlookatme.mp3" title="Don't Look at Me" artist="Mark Heath" /&gt; <br />
&nbsp;&nbsp;    &lt;audiofile url="http://www.markheath.me.uk/music/markheath+holyspiritwillyoube.mp3" title="Holy Spirit Will You Be" artist="Mark Heath" /&gt; <br />
        &lt;/playlist&gt; 
    </div>
        
    <p>The GUI is still in an experimental state. You can adjust volume and move between the songs, but you
    cannot currently reposition within the current audio track.</p>
    
    <p><b>InitParams:</b> <i>Url=Playlist=../Playlist.xml</i></p>
        
    <div style="width:400px; height:150px; margin:5px;">

        <asp:Silverlight ID="Demo3" runat="server" 
            InitParams="Playlist=../Playlist.xml"
            Source="~/ClientBin/AudioPlayer.xap" 
            Version="2.0" Width="100%" Height="100%" />
    </div>
    
    <h3>Many Audio Tracks</h3>
    
    <p>A scroll bar will appear if there are too many audio tracks to display:</p>

    <p><b>InitParams:</b> <i>Url=Playlist=../12ItemPlaylist.xml</i></p>
        
        <div style="width:400px; height:150px; margin:5px;">

        <asp:Silverlight ID="Silverlight1" runat="server" 
            InitParams="Playlist=../12ItemPlaylist.xml"
            Source="~/ClientBin/AudioPlayer.xap" 
            Version="2.0" Width="100%" Height="100%" />
    </div>

    
    <h3>No Parameters Demo</h3>
    
    <p>If you don't pass any parameters in, it will look for a Playlist.xml file in the folder underneath 
    the one containing the xap file (i.e. don't put it in ClientBin).</p>
    
    </div>
    </form>
</body>
</html>
