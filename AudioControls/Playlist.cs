using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AudioControls
{
    public class Playlist : IEnumerable<PlaylistEntry>
    {
        List<PlaylistEntry> entries;

        public static Playlist LoadFromXml(string xml)
        {
            Playlist playlist = new Playlist();
            XDocument xmlPlaylist = XDocument.Parse(xml);
            playlist.entries = new List<PlaylistEntry>();
            playlist.entries.AddRange(from audioFile in xmlPlaylist.Descendants("audiofile")
                                      select new PlaylistEntry
                                      {
                                          Url = (string)audioFile.Attribute("url"),
                                          Title = (string)audioFile.Attribute("title"),
                                          Artist = (string)audioFile.Attribute("artist")
                                      });
            return playlist;
        }

        public int Count
        {
            get { return entries.Count; }
        }

        public IEnumerator<PlaylistEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)entries).GetEnumerator();
        }

    }
}
