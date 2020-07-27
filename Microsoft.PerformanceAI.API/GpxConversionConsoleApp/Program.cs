using System;
using MKCoolsoft.GPXLib;

namespace GpxConversionConsoleApp
{
    class Program
    {
        //private const string Filename = "C:\\soundscape\\PerformanceAI\\Performance AI 2107 91044\\Microsoft.PerformanceAI.API\\GpxConversionConsoleApp\\othersample.gpx";
        private const string Filename = "C:\\soundscape\\PerformanceAI\\Performance AI 2107 91044\\Microsoft.PerformanceAI.API\\GpxConversionConsoleApp\\HowthVanilla.gpx";

        static void Main(string[] args)
        {
            var gpxLib = new GPXLib();

            gpxLib.LoadFromFile(Filename);
            Console.WriteLine("Loaded File");

            Console.WriteLine("===== Route Data =====");

            foreach (var route in gpxLib.RteList)
            {
                // ges all route data
                Console.WriteLine($"Route information for {route.Name}");
            }

            Console.WriteLine("===== Waypoint Data =====");
            foreach (var waypoint in gpxLib.WptList)
            {
                Console.WriteLine($"WAypoint information for {waypoint.Name}");
            }

            // Sample file only has Trackinformation
            Console.WriteLine("===== Track Data =====");
            foreach (var track in gpxLib.TrkList)
            {
                Console.WriteLine($"Track information for {track.Name}");
                
                foreach(var segment in track.TrksegList)
                {
                    Console.WriteLine($"Track information for segment.");
                    foreach(var point in segment.TrkptList)
                    {
                        // elevation will default to 0 is not provided
                        Console.WriteLine($"\t{point.Lat}\t{point.Lon}\t{point.Ele}");
                    }
                }
            }

        }
    }
}
