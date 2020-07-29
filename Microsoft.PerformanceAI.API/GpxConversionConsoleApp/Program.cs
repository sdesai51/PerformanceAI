using MKCoolsoft.GPXLib;
using SoundscapeGpx;
using SoundscapeGpx.Models;
using System;
using System.Collections.Generic;

namespace GpxConversionConsoleApp
{
    class Program
    {
        //private const string Filename = "C:\\soundscape\\PerformanceAI\\Performance AI 2107 91044\\Microsoft.PerformanceAI.API\\GpxConversionConsoleApp\\othersample.gpx";
        private const string Filename = "C:\\soundscape\\PerformanceAI\\Performance AI 2107 91044\\Microsoft.PerformanceAI.API\\GpxConversionConsoleApp\\HowthVanilla.gpx";

        static void Main(string[] args)
        {
            WriteSoundscapeXmlTest();
        }

        private static void WriteSoundscapeXmlTest()
        {
            var point1 = new SoundscapeWayPoint
            {
                Name = "Cemetry Junction",
                Description = "A junction between two large roads in Reading, the name references the large cemetery whose entrance is at this junction",
                Type = "WayPoint",
                Latitude = 51.452833, // lat
                Longitude = -0.948861, // long
                Elevation = 9,
                Street = "Cemetery Junction, Reading"
            };

            var point2 = new SoundscapeWayPoint
            {
                Name = "North end of Donnington Road",
                Description = "North end of Donnington Road",
                Type = "WayPoint",
                Latitude = 51.452405, // lat
                Longitude = -0.952123, // long
                Elevation = 9,
                Street = "North end of Donnington Road"
            };

            SoundscapeWayPoint point3 = new SoundscapeWayPoint
            {
                Name = "Donnington Cars",
                Description = "Donnington Cars",
                Type = "WayPoint",
                Latitude = 51.449737, // lat
                Longitude = -0.950664, // long
                Elevation = 9,
                Street = "Donnington Cars"
            };

            var waypoints = new List<SoundscapeWayPoint>
            {
                point1,
                point2,
                point3
            };

            var metadata = new ExperienceMetadata
            {
                Name = "Frazier's test route in Reading",
                Description = "Route for testing the scavenger hunt in Reading",
                Author = "QA Team",
                StartTime = new DateTime(2019, 11, 01, 11, 52, 51),
                EndTime = new DateTime(2021, 01, 01, 11, 53, 52),
                RegionLatitude = 51.4502463333333333,
                RegionLongitude = -0.94847866666666666667,
                RegionRadius = 2000.00,
                Waypoints = waypoints,
                Locale = "en_us",
                Behaviour = "ScavengerHunt",
                CreationTime = DateTime.UtcNow,
                Identifier = "c713e0b491e74468b5600312291018f8"
            };

            var builder = new SoundscapeGpxBuilder(metadata);
            var doc = builder.ExportToGpx();
            Console.WriteLine(doc.ToString());
        }

        private void TestingGpxLib()
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

                foreach (var segment in track.TrksegList)
                {
                    Console.WriteLine($"Track information for segment.");
                    foreach (var point in segment.TrkptList)
                    {
                        // elevation will default to 0 is not provided
                        Console.WriteLine($"\t{point.Lat}\t{point.Lon}\t{point.Ele}");
                    }
                }
            }
        }
    }
}
