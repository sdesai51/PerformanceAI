using Microsoft.PerformanceAI.API.Types;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Proxies
{
    public interface IBingMapsProxy
    {
        IEnumerable<Coordinate> GetElevation(IEnumerable<Coordinate> coordinates);
    }
}
