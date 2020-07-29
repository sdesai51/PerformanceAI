using Microsoft.PerformanceAI.API.Types;
using System;

namespace Microsoft.PerformanceAI.API.Services
{
    public abstract class GeoServiceBase
    {
        public double CalculateDistance(Coordinate start, Coordinate target)
        {
            return this.CalculateDistance(start.Lat, start.Long, target.Lat, target.Long);
        }

        /// <summary>
        /// Calculate distances in meters between two coordinates.
        /// </summary>
        /// <remarks>https://www.geodatasource.com/developers/c-sharp</remarks>
        /// <returns>A meter distance between two coordinates.</returns>
        protected double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
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
                return dist;
            }
        }

        /// <summary>
        /// This function converts decimal degrees to radians  
        /// </summary>
        protected double ConvertDecimalToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// This function converts radians to decimal degrees.
        /// </summary>
        protected double ConvertRadianToDecimal(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
