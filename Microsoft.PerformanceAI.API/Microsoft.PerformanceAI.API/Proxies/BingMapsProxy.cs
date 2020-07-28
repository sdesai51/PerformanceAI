using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Microsoft.PerformanceAI.API.Proxies
{
    public class BingMapsProxy : BaseProxy, IBingMapsProxy
    {
        private const string URL_TEMPLATE = "http://dev.virtualearth.net/REST/v1/Elevation/List?points={0}&key={1}";
        private const int MAX_PAGE_SIZE = 1024;

        private readonly string AuthKey;

        public BingMapsProxy(IHttpClientFactory httpClientFactory, string authKey) : base(httpClientFactory) =>
            this.AuthKey = authKey ?? throw new ArgumentNullException(nameof(authKey));

        public IEnumerable<Coordinate> GetElevation(IEnumerable<Coordinate> coordinates)
        {
            return new List<Coordinate>();
        }

        private string ConstructUrl(IEnumerable<Coordinate> coordinates)
        {
            if (coordinates.Count() >= MAX_PAGE_SIZE)
            {
                //TODO: ADD Pagination here.
                coordinates = coordinates.TakeLast(MAX_PAGE_SIZE);
            }

            var coorsinatesString = string.Join(",", coordinates);
            return string.Format(URL_TEMPLATE, coordinates, this.AuthKey);
        }
    }
}
