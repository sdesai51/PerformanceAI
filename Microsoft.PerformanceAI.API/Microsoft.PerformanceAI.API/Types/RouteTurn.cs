using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Types
{
    public class RouteTurn
    {
        public Coordinate Start { get; set; }

        public Coordinate End { get; set; }

        public int TurnDirectonAngle { get; set; }

        public double Distance { get; set; }

        public double MaxAngle { get; set; }

        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
