namespace SoundscapeGpx.Models
{
    public class SoundscapeWayPoint
    {
        public string name;
        public string description;
        public string type;
        public double longitude;
        public double latitude;
        public decimal elevation;
        public string street;

        public SoundscapeWayPoint(string name, string description, string type, double latitude, double longitude, decimal elevation, string street)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.longitude = longitude;
            this.latitude = latitude;
            this.elevation = elevation;
            this.street = street;
        }
    }
}