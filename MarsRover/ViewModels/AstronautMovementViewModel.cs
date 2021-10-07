using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.ViewModels
{
    public class AstronautMovementViewModel
    {
        public MapSizeViewModel MapSize { get; set; }
        public AstronautLocationViewModel AstronautLocation { get; set; }
        public string[] MovementCommands { get; set; }
    }
}
