using System.Numerics;

namespace Game
{
    public class ShipPropellerData
    {
        public Animation PropellerAnimation;
        public Vector2 PropellerOffset;
        public Vector2 PropellerSize;

        public ShipPropellerData(Animation newAnim, Vector2 newOffset, Vector2 newSize)
        {
            this.PropellerAnimation = newAnim;
            this.PropellerOffset = newOffset;
            this.PropellerSize = newSize;
        }
    }
}
