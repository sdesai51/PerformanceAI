using Microsoft.PerformanceAI.API.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Proxies
{
    /// <summary>
    /// Provide an abstraction layer for contacting maps provider API.
    /// </summary>
    public interface IMapsProxy
    {
        /// <summary>
        /// Gets information about elevation for each coordinate.
        /// </summary>
        /// <param name="coordinates">Ordered collection of coordinates.</param>
        /// <returns>Ammended collection of coordinates with added elevation.</returns>
        Task<IEnumerable<Coordinates3d>> GetElevation(IEnumerable<Coordinate> coordinates);
    }
}
