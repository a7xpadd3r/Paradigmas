using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject
    {
        public bool debug = false;
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
        private int shipIntegrity = 4;
        private readonly float shieldTime = 0.8f;
        private float currentShieldTime = 0;

        // Events
        public Action OnShipDestroyed;

        public void InitializePlayer(ShipConfig withThisShip)
        {
            // Set initial position from GameObject
            posX = spawnPosition.X;
            posY = spawnPosition.Y;

            // Tag
            owner = "Player";

            // Ship configs
            ship = withThisShip; // Needs to be deprecated
            ShipConfiguration = ship;
          
            // ShipObject references
            ShipAnim = ShipConfiguration.ShipAnim();
            ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            SmokeDamageAnim = new Animation("Smoke", 0.15f, Effects.GetEffectTextures(1), false);
            ShieldAnim = new Animation("PlayerShield", 0.03f, Effects.GetEffectTextures(2));
            ShipAnim.ChangeFrame(4); // Intact ship texture

            // Collision
            objectCollider = new Collider(Position, ship.ShipSize(), "Player", 3);
            objectCollider.OnCollision += AnyDamage;

            // Final set
            ready = true; Awake(); Console.WriteLine("Jugador inicializado.");
        }

        private void AnyDamage(Collider instigator)
        {
            if ((!IsShielding && shipIntegrity > 1) || (!IsShielding && shipIntegrity == 1))
            {
                if (!IsShielding && shipIntegrity > 1)
                {
                    shipIntegrity--;
                    if (debug) Console.WriteLine("Player --> Evento de daño, integridad " + shipIntegrity + "/4... Instigador --> " + instigator.GetOwner());
                }
                else if (!IsShielding && shipIntegrity == 1)
                {
                    shipIntegrity = 4;
                    OnShipDestroyed?.Invoke();
                    if (debug) Console.WriteLine("Player --> RIP, reiniciando... Instigador --> " + instigator.GetOwner());
                }

                ShipAnim.ChangeFrame(shipIntegrity);
                ShieldAnim.ChangeFrame(0);
                SmokeDamageAnim.Play();
                currentShieldTime = 0;
                IsShielding = true;
            }
        }

        private void Fire()
        {
            canShoot = false;
            //bullets.Add(new Proyectile(Position + RailPosition, currentWeapon));
            ProyectilesManager.AddProyectile(new Proyectile(Position + RailPosition, currentWeapon));
        }

        public override void Update()
        {
            if (ready)
            {
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

                // Update position in ShipObject, needed to render stuff
                UpdateShipPosition(Position);
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset());
            }
        }


        // Needs to be modified for non-static?
        public Texture GetIntegrityTexture()
        {
            Texture integrity = ship.ShipAnim().GetFrameTexture(shipIntegrity);
            return integrity;
        }

        public ShipConfig GetShip()
        {
            ShipConfig shipdata = ship;
            return shipdata;
        }
    }
}