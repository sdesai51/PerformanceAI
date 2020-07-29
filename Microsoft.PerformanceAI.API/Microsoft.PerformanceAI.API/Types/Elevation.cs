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

        public double Angle
        {
            get
            {
                // from Pythagoras law
                var a = this.ElevationChange;
                var b = this.Length;
                var c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                var a2 = Math.Pow(a, 2);
                var b2 = Math.Pow(b, 2);
                var c2 = Math.Pow(c, 2);

                // From Cosine law 
                var alpha = Math.Acos((b2 + c2 - a2) / (2 * b * c));
                var alphaDegee = (alpha * 180 / Math.PI);
                return Math.Round(alphaDegee, 2);
            }
        }

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
