using System.Numerics;

namespace Game
{
    public class ShipsProperties // This file contains all the ships configurations.
    {
        // ShipData usage: SPEED - DURABILITY (DMG gets divided by this value) - SHIP TEXTURES - SHIELD COLOR - SHIELD OFFSET+SIZE - RAIL POSITION - COLLIDER OFFSET - COLLIDER SIZE - PROPELLERS OFFSET
        // ShipData contains other forms for declaration.
        public static readonly ShipData ElCapitan = new ShipData(350, 1, Textures.GetShipAnimation(ShipsAnimations.ElCapitan), Textures.GetShieldAnimation(ShieldColor.Green), new DoubleVector2(new Vector2(48,40), new Vector2(4,4)), new Vector2(0,45), new Vector2(-14.5f,-55), new Vector2(100,50), new Vector2(0,-55));
    }
}
