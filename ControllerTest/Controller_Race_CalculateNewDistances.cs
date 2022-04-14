using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    public class Controller_Race_CalculateNewDistances
    {
        private Race _race;

        [SetUp]
        public void Setup()
        {
            _race = new Race(
                new Track("track", new SectionTypes[0]),
                new List<IParticipant>()
            );
        }

        [Test]
        public void CalculateNewDistancesShouldBe()
        {
            Driver driver = new Driver("a", 0, new Car(100, false) { Quality = 60, Performance = 100 }, TeamColors.Blue);
            _race.Participants = new List<IParticipant> { driver };
            LinkedList<Section> sections = new LinkedList<Section>();
            sections.AddLast(new Section(SectionTypes.StartGrid));
            Section a = new Section(SectionTypes.Straight);
            sections.AddLast(a);
            sections.AddLast(new Section(SectionTypes.Finish));
            _race.Track = new Track("track", sections);

            _race.setStartingPositions();
            _race.MoveDrivers();
            _race.MoveDrivers();
            Assert.AreSame(driver, _race.GetSectionData(a).Left);
            Assert.AreEqual(600, _race.GetSectionData(a).DistanceLeft);
        }
    }
}
