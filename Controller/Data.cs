using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    static class Data
    {
        private static Competition Competition { get; set; }

        public static void Initialize()
        {
            Competition = new Competition(new List<IParticipant>(), new Queue<Track>());
            addParticipants();
            addTracks();
        }

        private static void addParticipants()
        {
            List<IParticipant> participants = new List<IParticipant>();
            participants.Add(new Driver("Driver 1", 0, new Car(70, false), TeamColors.Red));
            participants.Add(new Driver("Driver 2", 0, new Car(80, false), TeamColors.Green));
            participants.Add(new Driver("Driver 3", 0, new Car(76, false), TeamColors.Yellow));
            participants.Add(new Driver("Driver 4", 0, new Car(79, false), TeamColors.Blue));
            Competition.Participants = participants;
        }

        private static void addTracks()
        {
            Queue<Track> trackQueue = new Queue<Track>();

            // Track 1
            LinkedList<Section> sections = new LinkedList<Section>();
            sections.Append(new Section(SectionTypes.Straight));
            sections.Append(new Section(SectionTypes.RightCorner));
            sections.Append(new Section(SectionTypes.Straight));
            sections.Append(new Section(SectionTypes.RightCorner));
            sections.Append(new Section(SectionTypes.Straight));
            sections.Append(new Section(SectionTypes.RightCorner));
            sections.Append(new Section(SectionTypes.Straight));
            sections.Append(new Section(SectionTypes.RightCorner));
            trackQueue.Enqueue(new Track("Track 1", sections));

            // Track 2
            LinkedList<Section> sections2 = new LinkedList<Section>();
            sections2.Append(new Section(SectionTypes.Straight));
            sections2.Append(new Section(SectionTypes.RightCorner));
            sections2.Append(new Section(SectionTypes.Straight));
            sections2.Append(new Section(SectionTypes.RightCorner));
            sections2.Append(new Section(SectionTypes.Straight));
            sections2.Append(new Section(SectionTypes.RightCorner));
            sections2.Append(new Section(SectionTypes.Straight));
            sections2.Append(new Section(SectionTypes.RightCorner));
            trackQueue.Enqueue(new Track("Track 2", sections2));

            Competition.Tracks = trackQueue;
        }
    }
}
