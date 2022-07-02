namespace Game
{
    public class ShipData
    {
        // Basic stuff
        public float MaxSpeed ;
        public float Durability;

        // GFX
        public Animation ShipAnim;
        public Animation ShipPropeller;
        public int PropellersAmount;
        public Animation ShieldAnim;

        public ShipData(float newMaxSpeed, float newDurability, Animation newShipAnim, Animation newShipPropAnim, int newPropAmount, Animation newShieldAnim)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.ShipAnim = newShipAnim;
            this.ShipPropeller = newShipPropAnim;
            this.PropellersAmount = newPropAmount;
            this.ShieldAnim = newShieldAnim;
        }
    }
}
