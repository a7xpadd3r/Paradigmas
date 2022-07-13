using System;
using System.Numerics;

namespace Game
{
    public class ShipObject : GameObject
    {
        public ShipData Ship;
        public Vector2 RailPosition => this.Position - Ship.RailPosition;
        public ColliderVectors ShipCollidersVectors => new ColliderVectors(new DoubleVector2(this.Position, this.Ship.ShipAnim.TextureSize), this.Ship.ColliderVectors);
        public bool ShieldActive { get; set; }
        public float ShieldDuration { get; set; }
    }
}
