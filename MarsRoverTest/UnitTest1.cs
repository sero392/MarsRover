using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover.ViewModels;
using System;
using System.Collections.Generic;

namespace MarsRoverTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            #region Astronot Tanımlaması
            var astronautLocations = new List<AstronautLocationViewModel>();
            astronautLocations.Add(new AstronautLocationViewModel()
            {
                CoordinateX = 4,
                CoordinateY = 2,
                Rotation = "N",
                MovementCommands = new string[] { "L",  "M", "R", "M", "M", "R", "M", "R","M" }
            });
            var astronautMovement = new AstronautMovementViewModel()
            {
                MapSize = new MapSizeViewModel()
                {
                    CoordinateX = 5,
                    CoordinateY = 5,
                },
                AstronautLocation = astronautLocations,
            };
            #endregion


            //Dönüş Yapılması Beklenilen Sonuç
            var expected = new AstronautLocationViewModel()
            {
                CoordinateX = 4,
                CoordinateY = 3,
                Rotation = "S",
            };

            var actual = MarsRover.Program.GetAstronautLocation(astronautMovement).AstronautLocation[0];
            //Sonuçlar Beklenene Eşitmi Kontrol Et
            Assert.AreEqual(expected.CoordinateX, actual.CoordinateX);
            Assert.AreEqual(expected.CoordinateY, actual.CoordinateY);
            Assert.AreEqual(expected.Rotation, actual.Rotation);
        }
    }
}
