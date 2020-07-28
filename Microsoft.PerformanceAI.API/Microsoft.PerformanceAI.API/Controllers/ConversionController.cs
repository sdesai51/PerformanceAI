using Microsoft.AspNetCore.Mvc;
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
        private readonly IBingMapsProxy bingService;
        private readonly IVanillaParserService vanillaParserService;

        public ConversionController(IVanillaParserService vanillaParserService, IBingMapsProxy bingService)
        {
            this.bingService = bingService;
            this.vanillaParserService = vanillaParserService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(string value)
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
