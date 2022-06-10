using System;
using System.Numerics;

namespace Game
{
    public enum MosquitoeDirection
    {
        Up, Right, Down, Left
    }
    public class eMosquitoe : ShipObject, iEnemy
    {
        private protected bool ready = false;

        // Special stuff
        private ShipConfig ship = ShipsData.GetShipConfig(4);

        // Position stuff
        private protected new float posX = 900;
        private protected new float posY = 200;
        public Vector2 OwnerRailPosition => Position - ship.ShipRailPosition;
        private protected new Vector2 Position => new Vector2(posX, posY);
        private Vector2 startingPos;
        private Vector2 boundaries;

        public eMosquitoe(Vector2 newPosition = new Vector2(), Vector2 newBoundaries = new Vector2(), float newLifes = 0.2f)
        {
            startingPos = newPosition;
            this.boundaries = newBoundaries;
            this.ShipConfiguration = this.ship;
            this.ShipAnim = new Animation("Mosquitoe Ship Anim", 0.01f, ShipsTextures.GetShipTextures(5)); 
            this.owner = "Enemy";
            this.tag = "Ship";
            this.life = newLifes;
            this.spawnPosition = newPosition;
            this.posX = this.spawnPosition.X;
            this.posY = this.spawnPosition.Y;
            this.objectCollider = new Collider(Position, this.ShipConfiguration.ShipSize, this.owner, this.tag, 3);

            Awake(); Console.WriteLine("Enemigo --> Enemigo 'Mosquitoe' creado con el ID {0}.", this.id); ready = true;
        }

        public override void Update()
        {
            if (ready)
            {
                objectCollider.UpdatePos(Position - this.ShipConfiguration.ShipCollisionOffset);
                UpdateShipPosition(Position);
                callsDamageOnCollision = !IsShielding;
                AI();
            }
        }
        public override void Destroy()
        {
            new GenericEffect(Position, new Vector2(1, 1), new Vector2(40, 40), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;

            ManagerLevel1.OnEnemyDeath?.Invoke(Position);
            UI.UpdateScore(50);
            GameObjectManager.RemoveGameObject(this);
        }

        public void AI()
        {
            float delta = Program.GetDeltaTime();

            if (posX > boundaries.X)
                posX -= ShipConfiguration.ShipSpeed * delta;

            else if (posX < boundaries.X)
                posX += ShipConfiguration.ShipSpeed * delta;

            if (posY < boundaries.Y + 1080)
                posY += ShipConfiguration.ShipSpeed * delta;

            if (posY > Engine.ScreenSize.Y + 50)
            {
                posX = startingPos.X;
                posY = startingPos.Y;
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool FloatInRange(float input, float min, float max)
        {
            bool value = false;
            if (input > min && input < min) value = true;
            return value;
        }
    }
}
