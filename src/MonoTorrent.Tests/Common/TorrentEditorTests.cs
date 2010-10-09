using System;

using NUnit.Framework;
using MonoTorrent.BEncoding;

namespace MonoTorrent.Common
{
    [TestFixture]
    public class TorrentEditorTests
    {
        [Test]
        public void EditingCreatesCopy ()
        {
            var d = Create ("comment", "a");
            var editor = new TorrentEditor (d);
            editor.Comment = "b";
            Assert.AreEqual ("a", d ["comment"].ToString (), "#1");
        }

        [Test]
        public void EditComment ()
        {
            var d = Create ("comment", "a");
            var editor = new TorrentEditor (d);
            editor.Comment = "b";
            d = editor.ToDictionary ();
            Assert.AreEqual ("b", d ["comment"].ToString (), "#1");
        }

        BEncodedDictionary Create (string key, string value)
        {
            var d = new BEncodedDictionary ();
            d.Add (key, (BEncodedString) value);
            return d;
        }
    }
}
