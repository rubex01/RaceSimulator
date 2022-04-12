using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RaceSimulatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Statistics _statistics;

        private StatisticsParticipants _statisticsParticipants;

        public MainWindow()
        {
            Data.Initialize();
            Data.NextRace();
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Visualization.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
            Data.CurrentRace.Start();
        }

        public void OnDriversChanged(object model, DriversChangedEventArgs e)
        {
            this.Screen.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    Screen.Source = null;
                    Screen.Source = Visualization.DrawTrack(e.Track);
                }));
        }

        public void OnRaceFinished(object model, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
            Data.NextRace();
            Init();
        }

        private void MenuItem_Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_StatisticsParticipants_OnClick(object sender, RoutedEventArgs e)
        {
            _statisticsParticipants = new StatisticsParticipants();
            _statisticsParticipants.Show();
        }

        private void MenuItem_Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            _statistics = new Statistics();
            _statistics.Show();
        }
    }
}
