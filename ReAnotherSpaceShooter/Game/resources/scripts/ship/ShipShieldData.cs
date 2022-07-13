using System.Numerics;

namespace Game
{
    public class ShipShieldData
    {
        public Animation ShieldAnim;
        public Vector2 RenderOffset;
        public Vector2 RenderSize;
        public DoubleVector2 ShieldVectors => new DoubleVector2(RenderOffset, RenderSize);

        public ShipShieldData(Animation newAnim, Vector2 newRenderOffset, Vector2 newRenderSize)
        {
            this.ShieldAnim = newAnim;
            this.RenderOffset = newRenderOffset;
            this.RenderSize = newRenderSize;
        }
    }
}
