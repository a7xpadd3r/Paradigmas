using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class ShipData
    {
        // Basic stuff
        public float MaxSpeed;
        public float Durability;
        public float Damage = 1;

        // GFX
        public Animation ShipAnim;
        public Animation ShieldAnim;
        public List<ShipPropellerData> ShipPropellers;

        // Vectors
        public Vector2 RailPosition;
        public DoubleVector2 ColliderVectors;
        public DoubleVector2 ShieldVectors;

        // Shield
        public bool IsShielding => shielding;
        private bool shielding = false;
        private float currentshieldCD = 0;
        private float maxshieldCD = 0.8f;

        public void ShipUpdate(float delta)
        {
            this.ShipAnim.Update();
            this.ShieldAnim.Update();

            if (shielding) currentshieldCD += delta;
            if (shielding && currentshieldCD >= maxshieldCD) { shielding = false; currentshieldCD = 0; }
        }
        public void UpdateShieldStatus(bool newShieldStatus) { shielding = newShieldStatus; }
        public void UpdateMaxShieldCD(float newMax) { maxshieldCD = newMax; }
        public void ShipDamage(float currentHP, float maxHP)
        {
            // Ship texture (shows damage)
            if ((currentHP * 100) / maxHP > 85) { ShipAnim.ChangeFrame(0); }
            else if ((currentHP * 100) / maxHP < 85 && (currentHP * 100) / maxHP > 50) { ShipAnim.ChangeFrame(1); }
            else if ((currentHP * 100) / maxHP < 50 && (currentHP * 100) / maxHP > 25) { ShipAnim.ChangeFrame(2); }
            else if ((currentHP * 100) / maxHP < 25 && (currentHP * 100) / maxHP > 0) { ShipAnim.ChangeFrame(3); }
        }

        public ShipData(float newMaxSpeed, float newDurability, Vector2 newRailPosition, Animation newShipAnim, ShipShieldData newShieldData, ShipPropellerData newPropeller, DoubleVector2 newColliderVectors)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.RailPosition = newRailPosition;

            this.ShipAnim = newShipAnim;
            this.ShipAnim.ChangeFrame(0);
            this.ShieldAnim = newShieldData.ShieldAnim;
            this.ShieldVectors = newShieldData.ShieldVectors;

            this.ColliderVectors = newColliderVectors;
            this.ShipPropellers = new List<ShipPropellerData>();
            this.ShipPropellers.Add(newPropeller);
        }
        public ShipData(float newMaxSpeed, float newDurability, Vector2 newRailPosition, Animation newShipAnim, ShipShieldData newShieldData, ShipPropellerData newPropeller1, ShipPropellerData newPropeller2, DoubleVector2 newColliderVectors)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.RailPosition = newRailPosition;

            this.ShipAnim = newShipAnim;
            this.ShipAnim.ChangeFrame(0);
            this.ShieldAnim = newShieldData.ShieldAnim;
            this.ShieldVectors = newShieldData.ShieldVectors;

            this.ColliderVectors = newColliderVectors;
            this.ShipPropellers = new List<ShipPropellerData>();
            this.ShipPropellers.Add(newPropeller1);
            this.ShipPropellers.Add(newPropeller2);
        }
        public ShipData(float newMaxSpeed, float newDurability, Vector2 newRailPosition, Animation newShipAnim, ShipShieldData newShieldData, ShipPropellerData newPropeller1, ShipPropellerData newPropeller2, ShipPropellerData newPropeller3, DoubleVector2 newColliderVectors)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.RailPosition = newRailPosition;

            this.ShipAnim = newShipAnim;
            this.ShipAnim.ChangeFrame(0);
            this.ShieldAnim = newShieldData.ShieldAnim;
            this.ShieldVectors = newShieldData.ShieldVectors;

            this.ColliderVectors = newColliderVectors;
            this.ShipPropellers = new List<ShipPropellerData>();
            this.ShipPropellers.Add(newPropeller1);
            this.ShipPropellers.Add(newPropeller2);
            this.ShipPropellers.Add(newPropeller3);
        }

        // Enemy mosquitoe
        public ShipData(float newMaxSpeed, float newDurability, Vector2 newRailPosition, Animation newShipAnim, DoubleVector2 newColliderVectors)
        {
            this.MaxSpeed = newMaxSpeed;
            this.Durability = newDurability;
            this.RailPosition = newRailPosition;

            this.ShipAnim = newShipAnim;
            this.ShipAnim.ChangeFrame(0);

            this.ColliderVectors = newColliderVectors;
            this.Damage = newDurability;
        }
    }
}
