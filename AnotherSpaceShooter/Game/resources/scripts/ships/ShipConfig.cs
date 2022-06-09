using System.Numerics;

namespace Game
{
    public class ShipConfig
    {
        // Private data
        protected private float speed;
        protected private float life;
        protected private Vector2 railPosition;
        protected private Vector2 propellerPosition;
        protected private Vector2 collisionSize;
        protected private Vector2 collisionOffset;
        protected private Vector2 drawOffset;
        protected private Animation shipAnimation;
        protected private Animation propellersAnimation;

        // Readable data
        public float ConfigShipSpeed => speed;
        public Vector2 ConfigShipRailPosition => railPosition;
        public Vector2 ConfigShipPropellersPosition => propellerPosition;
        public Vector2 ConfigShipSize => collisionSize;
        public Vector2 ConfigShipCollisionOffset => collisionOffset;
        public Vector2 ConfigShipDrawOffset => drawOffset;
        public Animation ConfigShipAnim => shipAnimation;
        public Animation ConfigPropellersAnim => propellersAnimation;

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
    }
}
