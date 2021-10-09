using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.ViewModels
{
    public class AstronautLocationViewModel
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Rotation { get; set; }
        public string[] MovementCommands { get; set; }

    }
}
