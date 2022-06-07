using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class DummyEnemy : ShipObject, iGetWeapon
    {
        private protected bool ready = false;
        private new bool debug = false;

        // Special stuff
        private ShipConfig ship = null;

        // Position stuff
        private protected float posX = 900;
        private protected float posY = 200;
        public Vector2 OwnerRailPosition => Position - ship.ShipRailPosition();
        private protected Vector2 Position => new Vector2(posX, posY);

        // Weapons stuff - iGetWeapon
        private int currentWeapIndex = -1;
        public List<iWeapon> AllWeapons { get; private set; } = new List<iWeapon>();
        public iWeapon CurrentWeapon => AllWeapons[currentWeapIndex];

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
            ShieldAnim = new Animation("EnemyShield", 0.03f, Effects.GetEffectTextures(3));
            Rotation = -180;
            //ShipAnimation.ChangeFrame(4); if more animatios are added, use this

            // Weapons
            AllWeapons.Add(fWeapon.CreateWeapon(WeaponTypes.Enemy1));
            currentWeapIndex++;

            // Collider
            objectCollider = new Collider(Position, ship.ShipSize(), owner, "Ship", 3);
            this.realSize = new Vector2(ShipAnim.CurrentTexture.Width, ShipAnim.CurrentTexture.Height);
            OnDamage += Damage;

            Awake(); Console.WriteLine("Dummy --> Enemigo dummy creado con el ID {0}", this.id); ready = true;
        }

        public override void OnCollision(Collider instigator)
        {
            switch (instigator.owner)
            {
                case "Player":

                    if (callsDamageOnCollision) AnyDamage?.Invoke(instigator.damage);
                    break;
            }
        }

        public void GetWeapon(Item theItem) { /* Nothing goes here... */ }

        public override void Damage(float amount)
        {
            if (!IsShielding)
            {
                IsShielding = true;
                ShieldAnim.ChangeFrame(0);
                currentShieldTime = 0;
                life -= amount;
                new GenericEffect(Position, new Vector2(1.8f, 1.8f), new Vector2(160, 230), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
                if (life <= 0) Destroy();
            }
        }

        public override void Destroy()
        {
            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;

            ManagerLevel1.OnEnemyDeath?.Invoke(Position);
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

                if (IsShielding) { currentShieldTime += Program.GetDeltaTime(); }
                if (currentShieldTime >= shieldTime && IsShielding) IsShielding = false;

                AI();
            }
        }

        private void AI()
        {
            float delta = Program.GetDeltaTime();
            if (posX > 2000)
                movingRight = true;

            else if (posX < -50)
                movingRight = false;

            if (!movingRight)
                posX += ShipConfiguration.ShipSpeed() * delta;

            else if (movingRight)
                posX -= ShipConfiguration.ShipSpeed() * delta;
            
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Update(delta, OwnerRailPosition);
                CurrentWeapon.Fire();
            }
        }
    }
}