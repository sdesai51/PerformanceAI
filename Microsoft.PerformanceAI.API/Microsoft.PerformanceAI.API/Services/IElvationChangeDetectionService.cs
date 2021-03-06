﻿using System.Collections.Generic;
using Microsoft.PerformanceAI.API.Types;

namespace Microsoft.PerformanceAI.API.Services
{
    public interface IElvationChangeDetectionService
    {
        /// <summary>
        /// Detects evelations larger than a given meter threshold.
        /// </summary>
        /// <param name="coordinates">Coordinates with elevation</param>
        /// <param name="minChange">Minimum threshold for change in meters.</param>
        /// <param name="minAngle">Minimum change angle.</param>
        /// <returns>A collection of detected elevation changes.</returns>
        IEnumerable<Elevation> DetectSteepElevation(IEnumerable<Coordinates3d> coordinates,  int? minChange, int? minAngle);
    }
}