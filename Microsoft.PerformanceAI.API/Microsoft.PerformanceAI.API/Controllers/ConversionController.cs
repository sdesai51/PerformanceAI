using Microsoft.AspNetCore.Mvc;
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
        private readonly IBingMapsService bingService;

        public ConversionController(IBingMapsService bingService) => this.bingService = bingService;

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
