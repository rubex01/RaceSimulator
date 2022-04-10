using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; } = new List<IParticipant>();

        public Queue<Track> Tracks { get; set; } = new Queue<Track>();

        public Track? NextTrack()
        {
            return Tracks.Count > 0 ? Tracks.Dequeue() : null;
        }
    }
}
