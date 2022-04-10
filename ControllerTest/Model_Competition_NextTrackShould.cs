using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void Setup()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            var track = new Track("Test track", new LinkedList<Section>());
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            var track = new Track("Test Track", new LinkedList<Section>());
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnNextTrack()
        {
            var track = new Track("Test Track 1", new LinkedList<Section>());
            var track2 = new Track("Test Track 2", new LinkedList<Section>());
            _competition.Tracks.Enqueue(track);
            _competition.Tracks.Enqueue(track2);
            Assert.AreEqual(_competition.NextTrack(), track);
            Assert.AreEqual(_competition.NextTrack(), track2);
        }
    }
}
