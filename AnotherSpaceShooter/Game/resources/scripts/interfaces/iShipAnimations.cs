using System.Numerics;

namespace Game
{
    public interface iShipAnimations
    {
        Transform RenderTransform { get; set; }
        Animation ShipAnim { get; set; }
        Animation ShipPropellersAnim { get; set; }
        Animation ShieldAnim { get; set; }
    }
}
