using System;
using System.Numerics;

namespace Game
{
    public class DummyEnemy : ShipObject
    {
        private protected bool ready = false;
        private new bool debug = false;

        // Special stuff
        private ShipConfig ship = null;

        // Position stuff
        private protected float posX = 900;
        private protected float posY = 200;
        private protected Vector2 Position => new Vector2(posX, posY);

        // Weapons stuff
        private protected bool canShoot = true;
        private protected float recoilTime = 0.4f;
        private protected float currentTime = 0;

        // Damage stuff
        private protected int shipIntegrity = 4;
        private readonly float shieldTime = 0.4f;
        private protected float currentShieldTime = 0;

        // "AI"
        private bool movingRight = true;

        public DummyEnemy(ShipConfig withThisShip, Vector2 newDirection, string newOwner = "Enemy", int newLifes = 10)
        {
            this.spawnPosition = newDirection;
            this.posX = spawnPosition.X;
            this.posY = spawnPosition.Y;
            this.owner = newOwner;
            this.tag = "Ship";
            this.life = newLifes;

            // Ship configs
            ship = withThisShip;
            ShipConfiguration = ship;

            // ShipObject references
            ShipAnim = ShipConfiguration.ShipAnim();
            ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            SmokeDamageAnim = new Animation("Smoke", 0.13f, Effects.GetEffectTextures(1), false);
            ShieldAnim = new Animation("EnemyShield", 0.03f, Effects.GetEffectTextures(3));
            Rotation = -180;
            //ShipAnimation.ChangeFrame(4); if more animatios are added, use this

            objectCollider = new Collider(Position, ship.ShipSize(), owner, "Ship", 3);
            OnDamage += Damage;
            Awake(); if (debug) Console.WriteLine("Dummy inicializado.");
            ready = true;
        }

        public void InitializeDummy(ShipConfig withThisShip)
        {/*
            // Set initial position from GameObject
            posX = spawnPosition.X;
            posY = spawnPosition.Y;

            // Tag
            this.owner = "Enemy";
            this.tag = "Ship";

            // Ship configs
            ship = withThisShip;
            ShipConfiguration = ship;

            // ShipObject references
            ShipAnim = ShipConfiguration.ShipAnim();
            ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            SmokeDamageAnim = new Animation("Smoke", 0.13f, Effects.GetEffectTextures(1), false);
            ShieldAnim = new Animation("EnemyShield", 0.03f, Effects.GetEffectTextures(3));
            Rotation = -180;
            //ShipAnimation.ChangeFrame(4); if more animatios are added, use this

            objectCollider = new Collider(Position, ship.ShipSize(), owner, "Ship", 3);
            OnDamage += Damage;
            Awake(); if (debug) Console.WriteLine("Dummy inicializado.");
            ready = true; */
        }

        public override void Damage(float amount)
        {
            if (!IsShielding)
            {
                IsShielding = true;
                ShieldAnim.ChangeFrame(0);
                SmokeDamageAnim.Play();
                currentShieldTime = 0;
                this.life -= amount;
                if (life <= 0) Destroy();
            }
        }

        public override void Destroy()
        {
            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            new GenericEffect(Position, new Vector2(1.3f, 1.3f), new Vector2(170, 180), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
            UI.UpdateScore(250);
            GameObjectManager.RemoveGameObject(this);
        }

        public override void Update()
        {
            if (ready)
            {
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset());
                UpdateShipPosition(Position);
                callsDamageOnCollision = !IsShielding;
                AI();
            }
        }

        private void AI()
        {
            if (posX > 2000)
                movingRight = true;

            else if (posX < -50)
                movingRight = false;

            if (!movingRight)
                posX += ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();

            else if (movingRight)
                posX -= ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();

            if (canShoot)
            {
                canShoot = false;
                new Proyectile(new Vector2(Position.X - 50, Position.Y)- ship.ShipRailPosition(), 4, "Enemy");
                new Proyectile(new Vector2(Position.X + 50, Position.Y) - ship.ShipRailPosition(), 4, "Enemy");
                new Proyectile(Position - ship.ShipRailPosition(), 4, "Enemy");
            }

            if (!canShoot) currentTime += Program.GetDeltaTime();

            if (currentTime >= recoilTime && !canShoot)
            {
                currentTime = 0;
                canShoot = true;
            }

            if (IsShielding) { currentShieldTime += Program.GetDeltaTime(); }
            if (currentShieldTime >= shieldTime && IsShielding) IsShielding = false;
        }
    }
}