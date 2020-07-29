using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Microsoft.PerformanceAI.API.Services
{
    public class SharpTurnsDetectionService : GeoServiceBase
    {
        private const int MIN_ANGLE = 90;
        private const int MAX_METER_DISTANCE = 50;
        private const double CHANGING_THRESHOLD = 0.00003;
        private const double MAX_TURN_ANGLE = 90; // TODO: this wont handle U turns!!
        public List<RouteTurn> DetectTurns(IEnumerable<Coordinate> coordinates)
        {
            var sharpTurns = new List<RouteTurn>();
            var coordinatesArray = coordinates != null ? coordinates.ToArray() : new Coordinate[0];

            for (int i = 0; i < coordinatesArray.Length; i++)
            {
                var start = coordinatesArray[i];
                var end = default(Coordinate);
                var detectedMaxAngle = 0d;
                var directionAngle = 0d;
                var j = i + 1;

                // We check all points for next N meters in the same move direction (<90 degrees).
                while (coordinatesArray[j] != null &&
                      this.IsRouteTurning(coordinatesArray[j - 1], coordinatesArray[j]) &&
                      this.CalculateDistance(start, coordinatesArray[j]) <= MAX_METER_DISTANCE)
                {
                    var newAngle = this.CalculateDirectionAngle(start, coordinatesArray[j]);
                    
                    if (!this.IsTurningSameDirection(directionAngle, newAngle))
                    {
                        i = j;
                        directionAngle = 0d;
                        break;
                    }

                    directionAngle = newAngle;
                    detectedMaxAngle = newAngle;

                    if (detectedMaxAngle >= MIN_ANGLE)
                    {
                        end = coordinatesArray[j];
                        sharpTurns.Add(new RouteTurn
                        {
                            Start = start,
                            End = end,
                            TurnDirectonAngle = Convert.ToInt32(Math.Ceiling(this.CalculateDirectionAngle(start, end))),
                            MaxAngle = detectedMaxAngle,
                            Distance = this.CalculateDistance(start, end)
                        });

                        directionAngle = 0d;
                        detectedMaxAngle = 0d;
                        end = null;
                        i = j;
                        break;
                    }
                    j++;
                }
            }

            return sharpTurns;
        }

        /// <summary>
        /// Detects if there is a minimum difference in a coordinates change.
        /// </summary>
        /// <returns></returns>
        private bool IsRouteTurning(Coordinate source, Coordinate candidate)
        {
            var isChangingEnough = false;

            if (source.Lat == candidate.Lat && source.Long == candidate.Long)
            {
                // This ensure  that identical coordinate specified twice wont cause issues.
                isChangingEnough = true;

            }
            else
            {
                isChangingEnough = Math.Abs(source.Lat - candidate.Lat) > CHANGING_THRESHOLD ||
                                   Math.Abs(source.Long - candidate.Long) > CHANGING_THRESHOLD;
            }

            return isChangingEnough;
        }

        /// <summary>
        /// Calculates a direction of two point on 360 circle.
        /// </summary>
        /// <returns>Direction in degree.</returns>
        private double CalculateDirectionAngle(Coordinate source, Coordinate target)
        {
            double Rad2Deg = 180.0 / Math.PI;
            var result = Math.Abs(Math.Atan2(source.Lat - target.Lat, target.Long - source.Long) * Rad2Deg);
            return result;
            /*var dTeta = Math.Log(Math.Tan((target.Lat / 2) + (Math.PI / 4)) / Math.Tan((source.Lat / 2) + (Math.PI / 4)));
            var dLon = Math.Abs(source.Long - target.Long);
            var teta = Math.Atan2(dLon, dTeta);
            var direction = Math.Round(this.ConvertRadianToDecimal(teta));
            return direction;*/
        }

        /// <summary>
        /// Check is turn continous - angle is increasing.
        /// </summary>
        /// <returns>True if angle increases.</returns>
        private bool IsTurningSameDirection(double oldAngle, double newAngle)
        {
            return oldAngle < newAngle;
        }

        private double CalculateAngle(Coordinate source, Coordinate target)
        {
            var latDiff = target.Lat - source.Lat;
            var lonDiff = target.Long - source.Long;
            return Math.Atan2(lonDiff, latDiff) * 180.0 / Math.PI;
        }
    }
}
