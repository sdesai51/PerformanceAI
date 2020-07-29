namespace SoundscapeGpx.Models
{
    public class SoundscapeWayPoint
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public decimal Elevation { get; set; }

        public string Street { get; set; }
    }
}