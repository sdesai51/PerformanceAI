using Microsoft.PerformanceAI.API.Types;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Services
{
    public interface IVanillaParserService
    {
        /// <summary>
        /// Takes a RAW string representing a 'Vanilla' GPX file and extract coordinates out from it.
        /// </summary>
        /// <param name="xmlDocumentString">XML document as a string.</param>
        /// <returns>List of coornidates</returns>
        IEnumerable<Coordinate> ExtractCoordinates(string xmlDocumentString);
    }
}
