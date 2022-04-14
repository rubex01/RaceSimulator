using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }

        public int RequiredLaps { get; set; }  

        public delegate void DriversChangedEvent(object sender, DriversChangedEventArgs e);

        public event DriversChangedEvent DriversChanged;

        public delegate void RaceFinishedEvent(object sender, EventArgs e);

        public event RaceFinishedEvent RaceFinished;

        public List<IParticipant> Participants { get; set; }

        public Dictionary<IParticipant, int> ParticipantLaps { get; set; }

        public DateTime Starttime { get; set; }

        private Random _random = new Random(DateTime.Now.Millisecond);

        private Dictionary<Section, SectionData> _positions { get; set; }

        private System.Timers.Timer _timer;

        public Race(Track Track, List<IParticipant> Participants, int laps)
        {
            _positions = new Dictionary<Section, SectionData>();
            ParticipantLaps = new Dictionary<IParticipant, int>();
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += OnTimedEvent;

            this.RequiredLaps = laps;
            this.Track = Track;
            this.Participants = Participants;
            RandomizeEquipment();
            setStartingPositions();
            SetupLapDictionary();
        }

        public Race(Track Track, List<IParticipant> Participants): this(Track, Participants, 2)
        {
        }

        public void Start()
        {
            _timer.Start();
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
                Participant.Equipment.Performance = _random.Next(20, 100);
                Participant.Equipment.Quality = _random.Next(20, 100);
            }
        }

        public void SetupLapDictionary()
        {
            foreach (IParticipant Participant in Participants)
            {
                ParticipantLaps.Add(Participant, 0);
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

        private void IncrementParticipantLap(IParticipant Participant)
        {
            if (!ParticipantLaps.ContainsKey(Participant))
                ParticipantLaps.Add(Participant, 1);
            else
                ParticipantLaps[Participant] += 1;
        }

        private void CalculateNewDistances()
        {
            for (int i = 0; i < Track.Sections.Count; i++)
            {
                SectionData data = GetSectionData(Track.Sections.ElementAt(i));

                if (data.Left != null && !data.Left.Equipment.IsBroken)
                {
                    int distance = (data.Left.Equipment.Performance * (data.Left.Equipment.Speed + data.Left.Equipment.Quality) / 2 / 10);
                    data.Left.Points += distance;
                    data.DistanceLeft += distance;
                }

                if (data.Right != null && !data.Right.Equipment.IsBroken)
                {
                    int distance = (data.Right.Equipment.Performance * (data.Right.Equipment.Speed + data.Right.Equipment.Quality) / 2 / 10);
                    data.Right.Points += distance;
                    data.DistanceRight += distance;
                }
            }
        }

        private bool CalculateDriverSections()
        {
            bool AnyDriverChangedSection = false;
            for (int i = Track.Sections.Count-1; i >= 0; i--)
            {
                int x = (i == 0) ? Track.Sections.Count-1 : i - 1;
                SectionData OldSectionData = GetSectionData(Track.Sections.ElementAt(x));
                SectionData currentSectionData = GetSectionData(Track.Sections.ElementAt(i));

                if (OldSectionData?.DistanceLeft >= 1000)
                {
                    if (currentSectionData.Left == null)
                    {
                        if (ParticipantsShouldContinue(OldSectionData.Left))
                        {
                            currentSectionData.Left = OldSectionData.Left;
                            currentSectionData.DistanceLeft = OldSectionData.DistanceLeft - 1000;
                        }
                        SetOldLeftEmpty();
                    } 
                    else if (currentSectionData.Right == null)
                    {
                        if (ParticipantsShouldContinue(OldSectionData.Left))
                        {
                            currentSectionData.Right = OldSectionData.Left;
                            currentSectionData.DistanceRight = OldSectionData.DistanceLeft - 1000;
                        }
                        SetOldLeftEmpty();
                    }
                }

                if (OldSectionData?.DistanceRight >= 1000)
                {
                    if (currentSectionData.Right == null)
                    {
                        if (ParticipantsShouldContinue(OldSectionData.Right))
                        {
                            currentSectionData.Right = OldSectionData.Right;
                            currentSectionData.DistanceRight = OldSectionData.DistanceRight - 1000;
                        }
                        SetOldRightEmpty();
                    }
                    else if (currentSectionData.Left == null)
                    {
                        if (ParticipantsShouldContinue(OldSectionData.Right))
                        {
                            currentSectionData.Left = OldSectionData.Right;
                            currentSectionData.DistanceLeft = OldSectionData.DistanceRight - 1000;
                        }
                        SetOldRightEmpty();
                    }
                }

                bool ParticipantsShouldContinue(IParticipant Participant)
                {
                    if (Track.Sections.ElementAt(i).SectionType == SectionTypes.Finish)
                    {
                        IncrementParticipantLap(Participant);
                        if (ParticipantLaps[Participant] == RequiredLaps)
                            return false;
                    }
                    return true;
                }

                void SetOldRightEmpty()
                {
                    OldSectionData.Right = null;
                    OldSectionData.DistanceRight = 0;
                    AnyDriverChangedSection = true;
                }

                void SetOldLeftEmpty()
                {                 
                    OldSectionData.Left = null;
                    OldSectionData.DistanceLeft = 0;
                    AnyDriverChangedSection = true;
                }
            }
            return AnyDriverChangedSection;
        }

        public bool MoveDrivers()
        {
            CalculateNewDistances();
            return CalculateDriverSections();
        }

        private bool CheckRaceFinished()
        {
            foreach (int laps in ParticipantLaps.Values)
            {
                if (laps != RequiredLaps)
                    return false;
            }
            return true;
        }

        private void BreakAndFixRandomEquipment()
        {
            foreach (IParticipant Participant in Participants)
            {
                if (Participant.Equipment.IsBroken && (_random.Next(0, 500) == _random.Next(0, 2)))
                {
                    Participant.Equipment.Quality -= 10;
                    Participant.Equipment.IsBroken = false;
                }
                else
                {
                    Participant.Equipment.IsBroken = (_random.Next(0, 100) == _random.Next(0, 2)) ? true : false;
                }
            }
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (MoveDrivers())
            {
                OnDriversChanged(new DriversChangedEventArgs() { Track = this.Track });
            }
            else if (CheckRaceFinished())
            {
                OnRaceFinished(e);
            }
            BreakAndFixRandomEquipment();
        }

        protected virtual void OnDriversChanged(DriversChangedEventArgs e)
        {
            DriversChanged?.Invoke(this, e);
        }

        protected virtual void OnRaceFinished(EventArgs e)
        {
            RaceFinished?.Invoke(this, e);
        }
    }
}
