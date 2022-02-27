using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Competition
    {
        public List<IParticipant> Participants { get; set; }

        public Queue<Track> Tracks { get; set; }

        public Competition(List<IParticipant> Participants, Queue<Track> Tracks)
        {
            this.Tracks = Tracks;
            this.Participants = Participants;
        }
    }
}
