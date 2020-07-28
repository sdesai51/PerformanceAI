using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.PerformanceAI.API.Proxies.Dtos
{
    public class Resource
    {
        public List<int> Elevations { get; set; }

        public int ZoomLevel { get; set; }
    }

    public class ResourceSet
    {
        public int EstimatedTotal { get; set; }

        public List<Resource> Resources { get; set; }
    }

    public class ElevationResponse
    {
        public string AuthenticationResultCode { get; set; }

        public string BrandLogoUri { get; set; }

        public string Copyright { get; set; }

        public List<ResourceSet> ResourceSets { get; set; }

        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string TraceId { get; set; }
    }
}
