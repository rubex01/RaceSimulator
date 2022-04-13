using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulatorGUI
{
    public class DataContext : INotifyPropertyChanged
    {
        public string NameCurrentTrack { get; set; }

        public string Placements { get; set; }

        public string Rounds { get; set; }

        public string ParticipantCount { get; set; }

        public string DriverInfo { get; set; }

        public string MetersDrivenByDriverInfo { get; set; }

        private List<IParticipant> _finished { get; set; } = new List<IParticipant> { };

        public string trackLength { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
        }

        private void GenerateParticipantStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Chauffeurs:");
            foreach (IParticipant driver in Data.CurrentRace.Participants)
            {
                sb.AppendLine($"{driver.Name} rijdt met de kleur {driver.TeamColor}");
            }
            DriverInfo = sb.ToString();

            StringBuilder sb2 = new StringBuilder();
            sb2.AppendLine("Meters gereden:");
            List<IParticipant> participantList = Data.CurrentRace.Participants.ToList();
            participantList.Sort((x, y) => y.Points.CompareTo(x.Points));
            for (int i = 0; i < participantList.Count; i++)
            {
                sb2.AppendLine($"{(i+1)}. {participantList[i].Name} heeft {participantList[i].Points/100} meter gereden");
            }
            MetersDrivenByDriverInfo = sb2.ToString();
        }

        private void generateSatistics()
        {
            Rounds = $"Aantal laps: {Data.CurrentRace.RequiredLaps}";
            ParticipantCount = $"Aantal deelnemers: {Data.CurrentRace.Participants.Count}";
            NameCurrentTrack = $"Naam track: {Data.CurrentRace.Track.Name}";
            trackLength = $"Lengte van de track: {(Data.CurrentRace.Track.Sections.Count * 1000 * Data.CurrentRace.RequiredLaps)/100} meter";
        }

        public void OnDriversChanged(object model, DriversChangedEventArgs e)
        {
            generateSatistics();
            GenerateParticipantStatistics();
            NameCurrentTrack = Data.CurrentRace.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void OnRaceFinished(object model, EventArgs e)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
        }
    }
}
