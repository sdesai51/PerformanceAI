namespace Microsoft.PerformanceAI.API.Types
{
    public class Coordinates3d : Coordinate
    {
        public int Elevation { get; set; }

        public override string ToString()
        {
            return $"{this.Elevation}, {base.ToString()}";
        }
    }
}
