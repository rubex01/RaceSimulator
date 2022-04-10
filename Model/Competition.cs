using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }

        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            this.Tracks = new Queue<Track>();
            this.Participants = new List<IParticipant>();
        }

        public Track? NextTrack()
        {
            return Tracks.Count > 0 ? Tracks.Dequeue() : null;
        }
    }
}
