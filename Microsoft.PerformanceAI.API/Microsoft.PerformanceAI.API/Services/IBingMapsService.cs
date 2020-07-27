using Microsoft.PerformanceAI.API.Types;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Services
{
    public interface IBingMapsService
    {
        IEnumerable<Coordinate> GetElevation(IEnumerable<Coordinate> coordinates);
    }
}
