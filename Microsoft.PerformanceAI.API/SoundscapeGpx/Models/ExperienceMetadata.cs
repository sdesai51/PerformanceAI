using System;
using System.Collections.Generic;

namespace SoundscapeGpx.Models
{
    public class ExperienceMetadata
    {
        public string name;
        public string description;
        public string author;
        public DateTime creationTime;
        public DateTime startTime;
        public DateTime endTime;
        public string identifier;
        public double regionLatitude;
        public double regionLongitude;
        public double regionRadius;
        public string locale;
        public string behaviour;
        public IEnumerable<SoundscapeWayPoint> waypoints;
    }
}
