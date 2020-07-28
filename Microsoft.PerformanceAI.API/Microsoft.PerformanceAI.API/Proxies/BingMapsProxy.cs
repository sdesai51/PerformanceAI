using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.PerformanceAI.API.Proxies.Dtos;
using Microsoft.PerformanceAI.API.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Proxies
{
    public class BingMapsProxy : BaseProxy, IMapsProxy
    {
        private const string URL_TEMPLATE = "Elevation/List?points={0}&key={1}";
        private const int MAX_PAGE_SIZE = 1024;

        private readonly BingMapsSettings bingMapsSettings;

        public BingMapsProxy(IHttpClientFactory httpClientFactory, IOptions<BingMapsSettings> settings, ILogger<BingMapsProxy> logger) :
            base(httpClientFactory, logger)
        {
            var optionSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.bingMapsSettings = optionSettings.Value;
        }
        public async Task<IEnumerable<Coordinates3d>> GetElevation(IEnumerable<Coordinate> coordinates)
        {
            this.Logger.LogInformation("Entering Bing Maps Proxy");
            var targetUrl = this.ConstructUrl(coordinates);
            this.Logger.LogInformation("Invocing {0}", targetUrl);
            var response = await this.HttpClient.GetAsync(targetUrl);

            if (!response.IsSuccessStatusCode)
            {
                var msg = $"Error contacting Bing Maps API. Status code {response.StatusCode}. Error: {response.ReasonPhrase}.";
                this.Logger.LogError(msg);
                throw new Exception(msg);
            }

            this.Logger.LogInformation("Response status is OK");

            this.Logger.LogInformation("Desrialization of Bing Maps response.");
            var elevationData = await this.ReadResponse(response);

            var ammendedCoordiantes = this.AmmendCoordinates(
                (List<Coordinate>)coordinates,
                elevationData?.ResourceSets?[0].Resources?[0].Elevations);

            return ammendedCoordiantes;
        }

        private string ConstructUrl(IEnumerable<Coordinate> coordinates)
        {
            const string SEPARATOR = ",";
            if (coordinates.Count() >= MAX_PAGE_SIZE)
            {
                //TODO: ADD Pagination here.
                coordinates = coordinates.TakeLast(MAX_PAGE_SIZE);
            }


            var coorsinatesString = string.Join(SEPARATOR, coordinates.Select(c => c.ToString()));
            return this.bingMapsSettings.BaseUrl + string.Format(URL_TEMPLATE, coorsinatesString, this.bingMapsSettings.Key);
        }

        private async Task<ElevationResponse> ReadResponse(HttpResponseMessage httpResponseMessage)
        {
            return await JsonSerializer.DeserializeAsync<ElevationResponse>(await httpResponseMessage.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                });
        }

        private IEnumerable<Coordinates3d> AmmendCoordinates(
            List<Coordinate> originalCoordinates,
            List<int> elevationData)
        {
            this.Logger.LogInformation($"Ammending coordinates {originalCoordinates.Count()}" +
                $" with {elevationData.Count()} elevation points.");

            if (originalCoordinates.Count() != elevationData.Count())
            {
                throw new Exception("Number of coordinates and elevation points is not the same.");
            }

            var ammendedCoordinates = new List<Coordinates3d>();

            // Copy to arrays to enable access over index.
            var originalCoordinatesArray = originalCoordinates.ToArray();
            var elevationDataArray = elevationData.ToArray();

            Enumerable.Range(0, originalCoordinates.Count()).ToList().ForEach(i =>
            {
                ammendedCoordinates.Add(new Coordinates3d
                {
                    Lat = originalCoordinatesArray[i].Lat,
                    Long = originalCoordinatesArray[i].Long,
                    Elevation = elevationDataArray[i]
                });
            });

            return ammendedCoordinates;
        }
    }
}
