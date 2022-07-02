using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum ShipsList { ElCapitan }
    public class ShipsProperties
    {
        public ShipData ElCapitan = new ShipData(200, 1,Textures.GetShipAnimation(ShipsAnimations.ElCapitan), );
    }
}
