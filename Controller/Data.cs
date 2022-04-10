using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }

        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        private static void AddParticipants()
        {
            List<IParticipant> participants = new List<IParticipant>();
            participants.Add(new Driver("Ferrari", 0, new Car(70, false), TeamColors.Red));
            participants.Add(new Driver("Red Bull", 0, new Car(80, false), TeamColors.Green));
            participants.Add(new Driver("Mercedes", 0, new Car(76, false), TeamColors.Yellow));
            participants.Add(new Driver("Aston Martin", 0, new Car(79, false), TeamColors.Blue));
            Competition.Participants = participants;
        }

        private static void AddTracks()
        {
            Competition.Tracks = new Queue<Track>();

            // Track 1
            LinkedList<Section> sections = new LinkedList<Section>();
            sections.AddLast(new Section(SectionTypes.StartGrid));
            sections.AddLast(new Section(SectionTypes.StartGrid));
            sections.AddLast(new Section(SectionTypes.StartGrid));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.RightCorner));
            sections.AddLast(new Section(SectionTypes.LeftCorner));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.RightCorner));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.RightCorner));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.LeftCorner));
            sections.AddLast(new Section(SectionTypes.RightCorner));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.RightCorner));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Straight));
            sections.AddLast(new Section(SectionTypes.Finish));
            sections.AddLast(new Section(SectionTypes.RightCorner));

            Competition.Tracks.Enqueue(new Track("Track 1", sections));

            // Track 2
            LinkedList<Section> sections2 = new LinkedList<Section>();
            sections2.AddLast(new Section(SectionTypes.StartGrid));
            sections2.AddLast(new Section(SectionTypes.StartGrid));
            sections2.AddLast(new Section(SectionTypes.StartGrid));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.LeftCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.LeftCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.LeftCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.LeftCorner));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.Straight));
            sections2.AddLast(new Section(SectionTypes.LeftCorner));
            sections2.AddLast(new Section(SectionTypes.Finish));
            sections2.AddLast(new Section(SectionTypes.Finish));
            sections2.AddLast(new Section(SectionTypes.RightCorner));
            sections2.AddLast(new Section(SectionTypes.RightCorner));

            Competition.Tracks.Enqueue(new Track("Track 2", sections2));
        }

        public static void NextRace()
        {
            Track? nextTrack = Competition.NextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }
    }
}
