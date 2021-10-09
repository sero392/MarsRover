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
        public List<AstronautLocationViewModel> AstronautLocation { get; set; } = new List<AstronautLocationViewModel>();
    }
}
