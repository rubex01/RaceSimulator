// See https://aka.ms/new-console-template for more information
using Controller;
using RaceSimulator;

Data.Initialize();
Data.NextRace();

Visualization.Initialize();
Visualization.DrawTrack(Data.CurrentRace.Track);

for (; ; )
{
    Thread.Sleep(100);
}