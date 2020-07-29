using System;
using System.Collections.Generic;

namespace SoundscapeGpx.Models
{
    public class ExperienceMetadata
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Identifier { get; set; }

        public double RegionLatitude { get; set; }

        public double RegionLongitude { get; set; }

        public double RegionRadius { get; set; }

        public string Locale { get; set; }

        public string Behaviour { get; set; }

        public IEnumerable<SoundscapeWayPoint> Waypoints { get; set; }
    }
}
