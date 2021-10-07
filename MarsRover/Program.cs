using MarsRover.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        #region Private Functions
        private static string ChangeLocation(string fromLocation, string Rotation)
        {
            string fullRotation = fromLocation + Rotation;
            Dictionary<string, string> locationRotation = new Dictionary<string, string>();
            //South
            locationRotation.Add("SR", Locations.East);
            locationRotation.Add("SL", Locations.North);
            //West
            locationRotation.Add("WR", Locations.North);
            locationRotation.Add("WL", Locations.East);
            //East
            locationRotation.Add("ER", Locations.West);
            locationRotation.Add("EL", Locations.South);
            //North
            locationRotation.Add("NR", Locations.South);
            locationRotation.Add("NL", Locations.West);

            locationRotation.TryGetValue(fullRotation, out string result);
            return result;
        }
        private static AstronautMovementViewModel GetAstronautLocation(AstronautMovementViewModel movement)
        {
            string newRotation = "";
            foreach (var command in movement.MovementCommands)
            {
                if (movement.AstronautLocation.CoordinateX <= movement.MapSize.CoordinateX && movement.AstronautLocation.CoordinateY <= movement.MapSize.CoordinateY)
                {
                    if (command == "L" || command == "R")
                    {
                        newRotation = ChangeLocation(movement.AstronautLocation.Rotation, command);
                        movement.AstronautLocation.Rotation = newRotation;
                    }
                    if (command == "M")
                    {
                        switch (movement.AstronautLocation.Rotation)
                        {
                            case Locations.North:
                                movement.AstronautLocation.CoordinateY += 1;
                                break;
                            case Locations.East:
                                movement.AstronautLocation.CoordinateY -= 1;
                                break;
                            case Locations.South:
                                movement.AstronautLocation.CoordinateX += 1;
                                break;
                            case Locations.West:
                                movement.AstronautLocation.CoordinateX -= 1;
                                break;
                            default:
                                break;
                        }
                    }
                }
              
            }
            return movement;
        }
        #endregion

        static void Main(string[] args)
        {
            var astronautMovement = new AstronautMovementViewModel()
            {
                AstronautLocation = new AstronautLocationViewModel()
                {
                    CoordinateX = 1,
                    CoordinateY = 2,
                    Rotation = "N",
                },
                MapSize = new MapSizeViewModel()
                {
                    CoordinateX = 5,
                    CoordinateY = 5,
                },
                MovementCommands = new string[] {"L","M","L","M","L","M","L","M","M" },
            };
            var res = GetAstronautLocation(astronautMovement);

            Console.ReadLine();
        }

    }

}
