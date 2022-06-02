using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject
    {
        public new bool debug = false;
        // Special stuff
        private ShipConfig ship = null;

        // Position stuff
        private static float posX = 500;
        private static float posY = 900;
        public static Vector2 Position => new Vector2(posX, posY);
        private Vector2 RailPosition => new Vector2(ship.ShipRailPosition().X, ship.ShipRailPosition().Y);
        private bool ready = false;

        // Weapons stuff
        private int currentWeapon = 1;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;

        // Damage stuff
        private float originalLifes;
        private readonly float shieldTime = 0.8f;
        private float currentShieldTime = 0;

        // Events
        public Action OnShipDestroyed;

        public Player(ShipConfig withThisShip, Vector2 newPos, string newOwner = "Player", int newLifes = 10)
        {
            // Basic stuff
            this.spawnPosition = newPos;
            this.life = newLifes;
            originalLifes = this.life;
            posX = spawnPosition.X;
            posY = spawnPosition.Y;

            // Tags
            this.owner = newOwner;
            this.tag = "Ship";

            // Ship configs
            this.ship = withThisShip; // Needs to be deprecated...
            this.ShipConfiguration = ship;

            // ShipObject references
            this.ShipAnim = ShipConfiguration.ShipAnim();
            this.ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            this.SmokeDamageAnim = new Animation("Smoke", 0.15f, Effects.GetEffectTextures(1), false, false);
            this.ShieldAnim = new Animation("PlayerShield", 0.03f, Effects.GetEffectTextures(2));
            this.ShipAnim.ChangeFrame(4); // Intact ship texture
            this.SmokeDamageAnim.OnAnimationFinished += OnSmokeEnded;

            // Update UI
            UI.UpdateUIShippy(ShipAnim.GetFrameTexture(4));

            // Collision
            this.objectCollider = new Collider(Position, this.ship.ShipSize(), this.owner, this.tag, 3);
            //this.objectCollider.OnCollision += AnyDamage; // Needs to be changed?

            // Final set
            Awake(); Console.WriteLine("Jugador inicializado."); this.ready = true;
        }

        public override void Damage(float amount)
        {
            if (!IsShielding && BlinkingEnded)
            {
                this.IsShielding = true;
                this.ShieldAnim.ChangeFrame(0);
                this.SmokeDamageAnim.Play();
                this.currentShieldTime = 0;
                this.life -= amount;
                this.RenderSmoke = true;
                if (life <= 0) Destroy();

                // Ship texture (shows damage)
                if ((life * 100) / originalLifes > 85)
                {
                    ShipAnim.ChangeFrame(4);
                    UI.UpdateUIShippy(ShipAnim.GetFrameTexture(4));
                }
                else if ((life * 100) / originalLifes < 85 && (life * 100) / originalLifes > 50)
                {
                    ShipAnim.ChangeFrame(3);
                    UI.UpdateUIShippy(ShipAnim.GetFrameTexture(3));
                }
                else if ((life * 100) / originalLifes < 50 && (life * 100) / originalLifes > 25)
                {
                    ShipAnim.ChangeFrame(2);
                    UI.UpdateUIShippy(ShipAnim.GetFrameTexture(2));
                }
                else if ((life * 100) / originalLifes < 25 && (life * 100) / originalLifes > 0)
                {
                    ShipAnim.ChangeFrame(1);
                    UI.UpdateUIShippy(ShipAnim.GetFrameTexture(1));
                }
                else if (life <= 0)
                {
                    //ShipAnim.ChangeFrame(0);
                    //UI.UpdateUIShippy(ShipAnim.GetFrameTexture(0));
                }
            }
        }

        private void Fire()
        {
            canShoot = false;
            //bullets.Add(new Proyectile(Position + RailPosition, currentWeapon));
            //ProyectilesManager.AddProyectile(new Proyectile(Position + RailPosition, currentWeapon, owner));
            new Proyectile(Position + RailPosition, currentWeapon, owner);
        }

        public override void Update()
        {
            if (ready)
            {
                // Update stuff
                UpdateShipPosition(Position);
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset());
                callsDamageOnCollision = !IsShielding;

                // Movement controls
                if (Engine.GetKey(Keys.A)) posX -= ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.D)) posX += ShipConfiguration.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.W)) posY -= (ShipConfiguration.ShipSpeed() / 1.1f) * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.S)) posY += (ShipConfiguration.ShipSpeed() / 1.1f) * Program.GetDeltaTime();

                // Weapons managment
                if (Engine.GetKey(Keys.SPACE) && canShoot) { Fire(); }
                if (Engine.GetKey(Keys.Num1)) { currentWeapon = 1; recoilTime = 0.5f; }
                if (Engine.GetKey(Keys.Num2)) { currentWeapon = 2; recoilTime = 0.8f; }
                if (Engine.GetKey(Keys.Num3)) { currentWeapon = 3; recoilTime = 0.25f; }

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

        public override void Destroy()
        {
            new GenericEffect(Position, new Vector2(1.3f, 1.3f), new Vector2(30, 60), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
            this.objectCollider.OnCollision -= this.OnCollision; this.AnyDamage -= Damage; this.SmokeDamageAnim.OnAnimationFinished -= OnSmokeEnded;

            ManagerLevel1.OnPlayerDeath?.Invoke(UI.ShippysLeft); // Maybe needs to be reworked.
            GameObjectManager.RemoveGameObject(this);
        }
    }
}