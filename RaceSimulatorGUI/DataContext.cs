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

        public event PropertyChangedEventHandler? PropertyChanged;

        public DataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        private void getPlacements()
        {

            foreach (Section section in Data.CurrentRace.Track.Sections)
            {
                SectionData data = Data.CurrentRace.GetSectionData(section);
                if (data.Left != null)
                {

                }
                if (data.Right != null)
                {
                    if (data.Left != null && data.DistanceLeft > data.DistanceRight)
                    {

                    }
                }
            }
        }

        public void OnDriversChanged(object model, DriversChangedEventArgs e)
        {

            NameCurrentTrack = Data.CurrentRace.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
