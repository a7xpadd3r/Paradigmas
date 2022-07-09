using System;
using System.Numerics;

namespace Game
{
    public class ShipData
    {
        // Basic stuff
        public float MaxSpeed ;
        public float Durability;

        // GFX
        public Animation ShipAnim;
        public Animation RedPropeller = Textures.GetPropellerAnimation(ShipPropeller.Red);
        public Animation BluePropeller = Textures.GetPropellerAnimation(ShipPropeller.Red);
        public Animation ShieldAnim;

        // Vectors
        public Vector2 RailPosition;
        public DoubleVector2 ColliderVectors;
        public DoubleVector2 ShieldVectors;

        public Vector2 FirstPropellerPosition;
        public Vector2 SecondPropellerPosition;
        public Vector2 ThirdPropellerPosition;
        public Vector2 FourthPropellerPosition;

        // Full build with DoubleVector2 - no shield parameters
        public ShipData(float newMaxSpeed, float newDurability, Animation newShipAnim, Animation newShieldAnim, DoubleVector2 newColliderVectors, Vector2 newRailPosition, Vector2 propellerpos1, Vector2 propellerpos2 = new Vector2(), Vector2 propellerpos3 = new Vector2(), Vector2 propellerpos4 = new Vector2())
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.ShipAnim = newShipAnim;
            this.ShieldAnim = newShieldAnim;
            this.ColliderVectors = newColliderVectors;
            this.RailPosition = newRailPosition;

            this.FirstPropellerPosition = propellerpos1;
            this.SecondPropellerPosition = propellerpos2;
            this.ThirdPropellerPosition = propellerpos3;
            this.FourthPropellerPosition = propellerpos4;
        }

        // Only 1 propeller with DoubleVector2 and some vectors - no shield parameters
        public ShipData(float newMaxSpeed, float newDurability, Animation newShipAnim, Animation newShieldAnim, DoubleVector2 newColliderVectors, Vector2 newRailPosition, Vector2 propellerpos1)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.ShipAnim = newShipAnim;
            this.ShieldAnim = newShieldAnim;
            this.ColliderVectors = newColliderVectors;
            this.RailPosition = newRailPosition;

            this.FirstPropellerPosition = propellerpos1;
        }

        // Only 1 propeller (default) with Vectors2 - no shield parameters
        public ShipData(float newMaxSpeed, float newDurability, Animation newShipAnim, Animation newShieldAnim, Vector2 newRailPosition, Vector2 newColliderOffset, Vector2 newColliderSize, Vector2 propellerpos1)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.ShipAnim = newShipAnim;
            this.ShieldAnim = newShieldAnim;
            this.RailPosition = newRailPosition;
            this.ColliderVectors = new DoubleVector2(newColliderOffset, newColliderSize);

            this.FirstPropellerPosition = propellerpos1;
        }

        // Only 1 propeller with Vectors2 - shield parameters
        public ShipData(float newMaxSpeed, float newDurability, Animation newShipAnim, Animation newShieldAnim, DoubleVector2 newShieldVectors, Vector2 newRailPosition, Vector2 newColliderOffset, Vector2 newColliderSize, Vector2 propellerpos1)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.ShipAnim = newShipAnim;
            this.ShieldAnim = newShieldAnim;
            this.RailPosition = newRailPosition;
            this.ColliderVectors = new DoubleVector2(newColliderOffset, newColliderSize);
            this.ShieldVectors = newShieldVectors;

            this.FirstPropellerPosition = propellerpos1;
        }
    }
}
