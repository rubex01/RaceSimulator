using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    public class Controller_Race_SetupLapDictionary
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
        public void SetupLapDictionaryShouldBe()
        {
            Driver driver = new Driver("a", 0, new Car(100, false) { Quality = 60, Performance = 100 }, TeamColors.Blue);
            Driver driver2 = new Driver("a", 0, new Car(100, false) { Quality = 60, Performance = 100 }, TeamColors.Blue);
            Driver driver3 = new Driver("a", 0, new Car(100, false) { Quality = 60, Performance = 100 }, TeamColors.Blue);
            _race.Participants = new List<IParticipant> { driver, driver2, driver3 };

            Dictionary<IParticipant, int> expected = new Dictionary<IParticipant, int>();
            expected.Add(driver, 0);
            expected.Add(driver2, 0);
            expected.Add(driver3, 0);

            _race.SetupLapDictionary();
            Assert.AreEqual(expected, _race.ParticipantLaps);
        }
    }
}
