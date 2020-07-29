using Microsoft.PerformanceAI.API.Services;
using Microsoft.PerformanceAI.API.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Microsoft.PerformanceAI.API.Tests
{
    [TestClass]
    public class SharpTurnsDetectionServiceUnitTests
    {
        private SharpTurnsDetectionService sharpTurnsDetectionService;

        [TestInitialize]
        public void Init()
        {
            this.sharpTurnsDetectionService = new SharpTurnsDetectionService();
        }

        [TestMethod]
        public void UnitTest_SharpTurnsDetectionService_Detect()
        {
            var turns = this.sharpTurnsDetectionService.DetectTurns(this.BuildTurn());
            Assert.AreEqual(1, turns.Count);
        }

        private IEnumerable<Coordinate> BuildTurn()
        {
            return new List<Coordinate>
            {
                new Coordinate { Lat = 53.3879 , Long= -6.065},
                new Coordinate { Lat = 53.38819 , Long= -6.06469},
                new Coordinate { Lat = 53.38824, Long= -6.06468},
                new Coordinate { Lat = 53.3883, Long= -6.0647 },
                new Coordinate { Lat = 53.38835 , Long= -6.06472},
                new Coordinate { Lat = 53.38835, Long= -6.06482 },
                new Coordinate { Lat = 53.38835 , Long= -6.06494},
                new Coordinate { Lat = 53.38832 , Long= -6.06506},
                new Coordinate { Lat = 53.38832 , Long= -6.06506},
                new Coordinate { Lat = 53.38835, Long= -6.06494}
            };
        }

    }
}
