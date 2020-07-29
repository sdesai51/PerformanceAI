using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Controllers
{
    public abstract class PerformanceBaseController<T> : ControllerBase
        where T : ControllerBase
    {
        protected readonly ILogger<T> logger;

        public PerformanceBaseController(ILogger<T> logger) => this.logger = logger;
        protected async Task<string> GetPostBody()
        {
            var bodyString = string.Empty;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            return bodyString;
        }

        protected async Task<K> GetPostBodyAsType<K>()
        {
            return await JsonSerializer.DeserializeAsync<K>(
                Request.Body,
                new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true,

                });
        }
    }
}
