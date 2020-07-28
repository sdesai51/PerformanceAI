using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Microsoft.PerformanceAI.API.Proxies
{
    public abstract class BaseProxy
    {
        private readonly IHttpClientFactory httpClientFactory;

        protected readonly ILogger<BaseProxy> Logger;

        protected HttpClient HttpClient
        {
            get
            {
                return this.httpClientFactory != null ? this.httpClientFactory.CreateClient() : null;
            }
        }

        public BaseProxy(IHttpClientFactory httpClientFactory, ILogger<BaseProxy> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
    }
}
