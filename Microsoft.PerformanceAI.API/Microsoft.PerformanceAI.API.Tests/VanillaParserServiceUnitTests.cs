using Microsoft.PerformanceAI.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Microsoft.PerformanceAI.API.Tests
{
    [TestClass]
    public class VanillaParserServiceUnitTests
    {
        private string ValidString = "<?xml version=\"1.0\" ?><gpx xmlns=\"http://www.topografix.com/GPX/1/1\"><trk><name>howth route</name><trkseg><trkpt lat=\"53.36053\" lon=\"-6.21076\"/><trkpt lat=\"53.36041\" lon=\"-6.2108\"/><trkpt lat=\"53.3604\" lon=\"-6.2106\"/></trkseg></trk></gpx>";
        private IVanillaParserService vanillaParserService;

        [TestInitialize]
        public void Init()
        {
            this.vanillaParserService = new VanillaParserService();
        }

        [TestMethod]
        public void UnitTest_VanillaParserService_ExtractCoordinates()
        {
            var list = this.vanillaParserService.ExtractCoordinates(this.ValidString);
            Assert.IsTrue(list.Any());
        }
    }
}
