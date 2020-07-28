namespace Microsoft.PerformanceAI.API.Types
{
    public class Elevation
    {
        public Coordinates3d Start { get; set; }

        public Coordinates3d End { get; set; }

        /// <summary>
        /// Length in meters.
        /// </summary>
        public int Length;

        public int Angle;

        public bool IsDownHill;
    }
}
