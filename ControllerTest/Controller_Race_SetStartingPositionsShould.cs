using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

namespace ControllerTest
{
    public class Controller_Race_SetStartingPositionsShould
    {
        private Race _race;

        [SetUp]
        public void Setup()
        {
            _race = new Race(
                new Track("track", 
                    new SectionTypes[2] { SectionTypes.StartGrid, SectionTypes.StartGrid 
                }), 
                new List<IParticipant> { new Driver("a", 0, new Car(1, false), 
                TeamColors.Red) 
            });
        }

        [Test]
        public void SetStartingPositionShouldThrowException()
        {
            _race.Track = new Track("track", new SectionTypes[0]);
            Assert.Throws<Exception>(() => _race.setStartingPositions());
        }

        [Test]
        public void SetStartingPositionShouldBe()
        {
            Driver driver = new Driver("name", 0, new Car(1, false), TeamColors.Green);
            _race.Participants = new List<IParticipant> { driver };
            LinkedList<Section> sections = new LinkedList<Section>();
            Section sectionStart = new Section(SectionTypes.StartGrid);
            sections.AddLast(sectionStart);
            _race.Track = new Track("track", sections);
            _race.setStartingPositions();

            Assert.AreSame(driver, _race.GetSectionData(sectionStart).Left);
        }
    }
}
