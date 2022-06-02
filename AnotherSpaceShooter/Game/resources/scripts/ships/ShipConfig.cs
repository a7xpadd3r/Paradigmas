using System.Numerics;

namespace Game
{
    public class ShipConfig
    {
        protected private float speed;
        protected private float life;
        protected private Vector2 railPosition;
        protected private Vector2 propellerPosition;
        protected private Vector2 collisionSize;
        protected private Vector2 collisionOffset;
        protected private Vector2 drawOffset;
        protected private Animation shipAnimation;
        protected private Animation propellersAnimation;
        //protected private GenericEffect destroyedEffect = new GenericEffect(Position, new Vector2(1.3f, 1.3f), new Vector2(138, 170), 0, "Smoke", Effects.GetEffectTextures(1), 0.15f, false, false, true);

        public ShipConfig(float newSpeed, Vector2 newRailPos, Vector2 newPropPos, Vector2 newCollSize, Vector2 newCollOffset, Animation newShipAnim, Animation newPropellersAnim, Vector2 newDrawOffset = new Vector2())
        {
            this.speed = newSpeed;
            this.railPosition = newRailPos;
            this.propellerPosition = newPropPos;
            this.collisionSize = newCollSize;
            this.collisionOffset = newCollOffset;
            this.drawOffset = newDrawOffset;
            this.shipAnimation = newShipAnim;
            this.propellersAnimation = newPropellersAnim;
        }

        public float ShipSpeed()
        {
            return this.speed;
        }

        public Vector2 ShipRailPosition()
        {
            return this.railPosition;
        }

        public Vector2 ShipPropellersPosition()
        {
            return this.propellerPosition;
        }

        public Vector2 ShipSize()
        {
            return this.collisionSize;
        }

        public Vector2 ShipCollisionOffset()
        {
            return this.collisionOffset;
        }
        public Vector2 ShipDrawOffset()
        {
            return this.drawOffset;
        }

        public Animation ShipAnim()
        {
            return this.shipAnimation;
        }
        public Animation PropellersAnim()
        {
            return this.propellersAnimation;
        }
    }
}
