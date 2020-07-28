using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PerformanceAI.API.Proxies;
using Microsoft.PerformanceAI.API.Services;
using System.Threading.Tasks;
namespace Microsoft.PerformanceAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : PerformanceBaseController<ConversionController>
    {
        private readonly IMapsProxy mapService;
        private readonly IVanillaParserService vanillaParserService;

        public ConversionController(
            IVanillaParserService vanillaParserService,
            IMapsProxy mapService,
            ILogger<ConversionController> logger) : base(logger)
        {
            this.mapService = mapService;
            this.vanillaParserService = vanillaParserService;
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
    }
}
