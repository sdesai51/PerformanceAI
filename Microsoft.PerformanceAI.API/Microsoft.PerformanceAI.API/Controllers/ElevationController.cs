using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.PerformanceAI.API.Services;
using Microsoft.PerformanceAI.API.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevationController : PerformanceBaseController<ElevationController>
    {
        private readonly IElvationChangeDetectionService elevationService;

        public ElevationController(
            IElvationChangeDetectionService elevationService,
            ILogger<ElevationController> logger) : base(logger)
        {
            this.elevationService = elevationService;
        }

        /// <summary>
        /// Detects elevation changes based on coordinates.
        /// </summary>
        /// <param name="threshold">Filter based on elevation change (in meters of change).</param>
        /// <param name="angle">Filter based on elevation angle (in degrees).</param>
        /// <returns>A filtered collection of elevation changes.</returns>
        [HttpPost]
        public async Task<ActionResult> DetectElevationChanges(int? threshold = 2, int? angle = 1)
        {
            this.logger.LogInformation("Entering elevation controller.");
            var coordinates3d = await this.GetPostBodyAsType<List<Coordinates3d>>();
            var elevationChanges = this.elevationService.DetectSteepElevation(coordinates3d, threshold, angle);

            return this.Ok(elevationChanges);
        }
    }
}