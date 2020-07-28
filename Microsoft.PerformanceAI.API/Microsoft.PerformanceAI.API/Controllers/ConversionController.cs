using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PerformanceAI.API.Proxies;
using Microsoft.PerformanceAI.API.Services;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace Microsoft.PerformanceAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly ILogger<ConversionController> logger;
        private readonly IMapsProxy mapService;
        private readonly IVanillaParserService vanillaParserService;

        public ConversionController(IVanillaParserService vanillaParserService,
            IMapsProxy mapService,
            ILogger<ConversionController> logger)
        {
            this.mapService = mapService;
            this.vanillaParserService = vanillaParserService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post()
        {
            this.logger.LogInformation("Entering controller method.");
            var postBody = await this.GetPostBody();

            var vanillaCoordinates = this.vanillaParserService.ExtractCoordinates(postBody);
            var ammendedCoordinates = await this.mapService.GetElevation(vanillaCoordinates);

            this.logger.LogInformation("Exiting controller method.");
            return this.Ok(ammendedCoordinates);
        }

        private async Task<string> GetPostBody()
        {
            var bodyString = string.Empty;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            return bodyString;
        }
    }
}
