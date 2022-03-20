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

        private Dictionary<Section, SectionData> _positions;

        public Race(Track Track, List<IParticipant> Participants)
        {
            this.Track = Track;
            this.Participants = Participants;   
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
    }
}
