using Controller;
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
        public event PropertyChangedEventHandler? PropertyChanged;

        public DataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        public void OnDriversChanged(object model, DriversChangedEventArgs e)
        {
            NameCurrentTrack = Data.CurrentRace.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
