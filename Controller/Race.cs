using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }

        public List<IParticipant> Participants { get; set; }

        public DateTime Starttime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);

        private Dictionary<Section, SectionData> _positions { get; set; }

        private Timer _timer { get; set; }

        public delegate void TimerCallback(object? state);

        public Race(Track Track, List<IParticipant> Participants)
        {
            _positions = new Dictionary<Section, SectionData>();

            this.Track = Track;
            this.Participants = Participants;
            RandomizeEquipment();
            setStartingPositions();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section)) {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        private void RandomizeEquipment()
        {
            foreach (IParticipant Participant in Participants)
            {
                Participant.Equipment.Performance = _random.Next(1, 100);
                Participant.Equipment.Quality = _random.Next(1, 100);
            }
        }

        public void setStartingPositions() 
        {
            var participantStack = new Stack<IParticipant>(Participants);
            foreach (Section section in Track.Sections)
            {
                if (participantStack.Count == 0) return;
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    var sectionData = new SectionData();
                    sectionData.Left = participantStack.Pop();
                    if (participantStack.Count != 0) 
                        sectionData.Right = participantStack.Pop();
                    _positions[section] = sectionData;
                }
            }
            if (participantStack.Count != 0)
            {
                throw new Exception("Not enough starting positions, add more start grid sections or reduce the amount of participants");
            }
        }
    }
}
