using System.Numerics;

namespace Game
{
    public interface iShipAnimations
    {
        Vector2 RenderPosition { get; set; }
        Animation ShipAnim { get; set; }
        Animation ShipPropellersAnim { get; set; }
        Animation SmokeDamageAnim { get; set; }
        Animation ShieldAnim { get; set; }
    }
}
