using Microsoft.PerformanceAI.API.Extensions;
using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Services
{
    public class ElvationChangeDetectionService : IElvationChangeDetectionService
    {
        public IEnumerable<Elevation> DetectSteepElevation(IEnumerable<Coordinates3d> coordinates, int threshold)
        {
            var list = coordinates.AsList();
            var elevationList = new List<Elevation>();
            Coordinates3d start, end;

            var index = 0;
            do
            {
                if (index == 0)
                {
                    index++;
                    continue;
                }

                var currentChangeInElevation = list[index].Elevation - list[index - 1].Elevation;
                if (currentChangeInElevation == 0)
                {
                    // It's flat.
                    index++;
                    continue;
                }

                start = list[index];
                var isDownhill = currentChangeInElevation < 0;

                // As long as elevation continues in the same directon we i.e. 6,3,2,1 we increases index.
                while (index < list.Count - 1 && ((list[index].Elevation - list[index - 1].Elevation < 0) == isDownhill))
                {
                    index++;
                }

                end = list[index];

                var elevationLength = this.CalculateDistance(start.Lat, start.Long, end.Lat, end.Long);
                var tempEevation = new Elevation
                {
                    IsDownHill = isDownhill,
                    Start = start,
                    End = end,
                    Length = elevationLength
                };

                if (tempEevation.ElevationChange >= threshold)
                {
                    elevationList.Add(tempEevation);
                }

                index++;
            }
            while (index < list.Count);

            return elevationList;
        }

        /// <summary>
        /// Calculate distances in meters between two coordinates.
        /// </summary>
        /// <remarks>https://www.geodatasource.com/developers/c-sharp</remarks>
        /// <returns>A meter distance between two coordinates.</returns>
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(ConvertDecimalToRadians(lat1)) *
                    Math.Sin(this.ConvertDecimalToRadians(lat2)) +
                    Math.Cos(this.ConvertDecimalToRadians(lat1)) *
                    Math.Cos(this.ConvertDecimalToRadians(lat2)) *
                    Math.Cos(this.ConvertDecimalToRadians(theta));

                dist = Math.Acos(dist);
                dist = this.ConvertRadianToDecimal(dist);
                dist = dist * 60 * 1.1515; // Miles.
                dist = dist * 1.609344; // Converting to KM.
                dist = dist * 1000; // Converting to meters.
                return (dist);
            }
        }

        /// <summary>
        /// This function converts decimal degrees to radians  
        /// </summary>
        private double ConvertDecimalToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// This function converts radians to decimal degrees.
        /// </summary>
        private double ConvertRadianToDecimal(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
