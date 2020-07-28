using System;

namespace Microsoft.PerformanceAI.API.Types
{
    public class Elevation
    {
        public Coordinates3d Start { get; set; }

        public Coordinates3d End { get; set; }

        /// <summary>
        /// Length in meters.
        /// </summary>
        public double Length;

        public bool IsDownHill;

        public int ElevationChange
        {
            get
            {
                var change = 0;
                if (this.Start != null && this.End != null)
                {
                    change = Math.Abs(this.Start.Elevation - this.End.Elevation);
                }

                return change;
            }
        }
    }
}
