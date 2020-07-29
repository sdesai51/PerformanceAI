using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PerformanceAI.API.Services;
using SoundscapeGpx.Models;
using System.Threading.Tasks;
namespace Microsoft.PerformanceAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoundscapeGpxController : PerformanceBaseController<SoundscapeGpxController>
    {
        private readonly ISoundscapeGpxService soundscapeGpxService;

        public SoundscapeGpxController(
            ISoundscapeGpxService soundscapeGpxService,
            ILogger<SoundscapeGpxController> logger) : base(logger)
        {
            this.soundscapeGpxService = soundscapeGpxService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post()
        {
            this.logger.LogInformation("Entering controller method.");

            var postBody = await this.GetPostBodyAsTypeNewtonsoft<ExperienceMetadata>();
            var gpx = this.soundscapeGpxService.BuildSoundscapeGpx(postBody);

            this.logger.LogInformation("Exiting controller method.");
            return this.Ok(gpx);
        }
    }
}
