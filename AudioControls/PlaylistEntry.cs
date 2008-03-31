using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AudioControls
{
    public class PlaylistEntry
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }

    public static class Playlist 
    {
        public static IEnumerable<PlaylistEntry> LoadFromXml(string xml)
        {
            XDocument xmlPlaylist = XDocument.Parse(xml);
            var playlist = from audioFile in xmlPlaylist.Descendants("audiofile")
                           select new PlaylistEntry
                           {
                               Url = (string)audioFile.Attribute("url"),
                               Title = (string)audioFile.Attribute("title"),
                               Artist = (string)audioFile.Attribute("artist")
                           };
            return playlist;
        }
    }
}
