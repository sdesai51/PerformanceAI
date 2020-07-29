using Microsoft.PerformanceAI.API.Extensions;
using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Services
{
    public class ElvationChangeDetectionService : GeoServiceBase, IElvationChangeDetectionService
    {
        public IEnumerable<Elevation> DetectSteepElevation(IEnumerable<Coordinates3d> coordinates, int? minChange, int? minAngle)
        {
            var list = coordinates.AsList();
            var elevationList = new List<Elevation>();
            Coordinates3d start, end;

            var index = 0;
            do
            {
                var currentChangeInElevation = list[index].Elevation - (index == 0 ? list[index].Elevation : list[index - 1].Elevation);
                if (currentChangeInElevation == 0)
                {
                    // It's flat.
                    index++;
                    continue;
                }

                start = list[index - 1];
                var isDownhill = currentChangeInElevation < 0;

                // As long as elevation continues in the same directon we i.e. 6,3,2,1 we increases index.
                while (index < list.Count &&
                    ((list[index].Elevation - list[index - 1].Elevation < 0) == isDownhill))
                {
                    index++;
                }

                end = list[index - 1];

                var elevationLength = this.CalculateDistance(start.Lat, start.Long, end.Lat, end.Long);
                var tempEevation = new Elevation
                {
                    IsDownHill = isDownhill,
                    Start = start,
                    End = end,
                    Length = elevationLength
                };

                if ((!minChange.HasValue || tempEevation.ElevationChange >= minChange.Value) &&
                    (!minAngle.HasValue || tempEevation.Angle >= minAngle.Value))
                {
                    elevationList.Add(tempEevation);
                }

                index++;
            }
            while (index < list.Count);

            return elevationList;
        }
    }
}
