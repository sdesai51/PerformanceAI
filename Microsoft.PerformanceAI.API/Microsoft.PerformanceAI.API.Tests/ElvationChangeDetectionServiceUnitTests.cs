using Microsoft.PerformanceAI.API.Services;
using Microsoft.PerformanceAI.API.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PerformanceAI.API.Tests
{
    [TestClass]
    public class ElvationChangeDetectionServiceUnitTests
    {
        private ElvationChangeDetectionService elvationDetectionService;

        [TestInitialize]
        public void Init()
        {
            this.elvationDetectionService = new ElvationChangeDetectionService();
        }

        [TestMethod]
        public void UnitTest_ElvationChangeDetectionService_One_Uphill_Elevation()
        {
            var coordintes = new List<Coordinates3d> {

                new Coordinates3d{
                    Lat = 53.35862,
                    Long = -6.18993,
                    Elevation = 2,
                },
                new Coordinates3d{
                     Lat = 53.3587,
                     Long = -6.18954,
                     Elevation = 2
                },
                new Coordinates3d {
                    Lat = 53.35902,
                    Long =-6.18864,
                    Elevation = 4
                },
                new Coordinates3d {
                    Lat = 53.35946,
                    Long =-6.18719,
                    Elevation = 2
                }
            };

            var elevations = this.elvationDetectionService.DetectSteepElevation(coordintes, 1,0);
            Assert.AreEqual(1, elevations.Count());

            var elevation = elevations.First();
            Assert.AreEqual(2, elevation.Start.Elevation);
            Assert.AreEqual(4, elevation.End.Elevation);
            Assert.AreNotEqual(0, elevation.Length);
            Assert.IsFalse(elevation.IsDownHill);
            Assert.AreEqual(1.65, elevation.Angle);
        }

        [TestMethod]
        public void UnitTest_ElvationChangeDetectionService_One_Long_Uphill_Elevation()
        {
            var coordintes = new List<Coordinates3d>
            {
                new Coordinates3d{
                    Lat = 53.36739,
                    Long = -6.17216,
                    Elevation = 1,
                },
                new Coordinates3d{
                     Lat = 53.36866,
                     Long = -6.17031,
                     Elevation = 4
                },
                new Coordinates3d {
                    Lat = 53.37012,
                    Long =-6.16819,
                    Elevation = 6
                },
            };

            var elevations = this.elvationDetectionService.DetectSteepElevation(coordintes, 1,0);
            Assert.AreEqual(1, elevations.Count());

            var fist = elevations.First();
            Assert.AreEqual(5, fist.ElevationChange);
            Assert.AreEqual(0.71, fist.Angle);
        }

        [TestMethod]
        public void UnitTest_ElvationChangeDetectionService_One_Climb()
        {
            var coordintes = new List<Coordinates3d>
            {
                new Coordinates3d
                {
                    Lat = 53.38349,
                    Long = -6.09966,
                    Elevation = 10
                },
                new Coordinates3d
                {
                    Lat = 53.383160,
                    Long = -6.099228,
                    Elevation = 1000
                }
            };

            var elevations = this.elvationDetectionService.DetectSteepElevation(coordintes, 1,0);
            var firstElevation = elevations.First();
            Assert.AreEqual(87.31, firstElevation.Angle);
        }

        [TestMethod]
        public void UnitTest_ElvationChangeDetectionService_Angle()
        {
            var elevation = new Elevation
            {
                Start = new Coordinates3d
                {
                    Lat = 53.38349,
                    Long = -6.09966,
                    Elevation = 0
                },
                End = new Coordinates3d
                {
                    Lat = 53.383160,
                    Long = -6.099228,
                    Elevation = 100
                },
                Length = 5
            };

            Assert.AreEqual(87.14, elevation.Angle);
        }
    }
}