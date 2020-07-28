using System;
using System.Net.Http;

namespace Microsoft.PerformanceAI.API.Proxies
{
    public abstract class BaseProxy
    {
        protected readonly IHttpClientFactory httpClientFactory;

        public BaseProxy(IHttpClientFactory httpClientFactory) => 
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }
}
