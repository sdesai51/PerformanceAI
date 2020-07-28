namespace Microsoft.PerformanceAI.API.Types
{
    public class Coordinate
    {
        public double Lat { get; set; }
        public double Long { get; set; }

        public override string ToString()
        {
            return $"{this.Lat},{this.Long}";
        }
    }
}
