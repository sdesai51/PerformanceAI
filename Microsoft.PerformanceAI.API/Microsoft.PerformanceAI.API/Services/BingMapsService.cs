using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Microsoft.PerformanceAI.API.Services
{
    public class BingMapsService : IBingMapsService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BingMapsService(IHttpClientFactory httpClientFactory) =>
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public IEnumerable<Coordinate> GetElevation(IEnumerable<Coordinate> coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
