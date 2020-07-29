using System;
using System.Collections.Generic;
using System.ComponentModel;
using MKCoolsoft.GPXLib;
using SoundscapeGpx;
using SoundscapeGpx.Models;

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
            SoundscapeWayPoint point1 = new SoundscapeWayPoint(
                "Cemetry Junction",
                "A junction between two large roads in Reading, the name references the large cemetery whose entrance is at this junction",
                "WayPoint",
                51.452833, // lat
                -0.948861, // long
                9,
                "Cemetery Junction, Reading");

            SoundscapeWayPoint point2 = new SoundscapeWayPoint(
                "North end of Donnington Road",
                "North end of Donnington Road",
                "WayPoint",
                51.452405, // lat
                -0.952123, // long
                9,
                "North end of Donnington Road");

            SoundscapeWayPoint point3 = new SoundscapeWayPoint(
                "Donnington Cars",
                "Donnington Cars",
                "WayPoint",
                51.449737, // lat
                -0.950664, // long
                9,
                "Donnington Cars");

            List<SoundscapeWayPoint> waypoints = new List<SoundscapeWayPoint>()
            {
                point1,
                point2,
                point3
            };

            ExperienceMetadata metadata = new ExperienceMetadata()
            {
                name = "Frazier's test route in Reading",
                description = "Route for testing the scavenger hunt in Reading",
                author = "QA Team",
                startTime = new DateTime(2019, 11, 01, 11, 52, 51),
                endTime = new DateTime(2021, 01, 01, 11, 53, 52),
                regionLatitude = 51.4502463333333333,
                regionLongitude = -0.94847866666666666667,
                regionRadius = 2000.00,
                waypoints = waypoints,
                locale = "en_us",
                behaviour = "ScavengerHunt",
                creationTime = DateTime.UtcNow,
                identifier = "c713e0b491e74468b5600312291018f8"
            };

            SoundscapeGpxBuilder builder = new SoundscapeGpxBuilder(metadata);


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
