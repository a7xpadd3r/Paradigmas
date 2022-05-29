using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject
    {
        public static bool debug = false;
        // Special stuff
        private static ShipConfig ship = null;

        // Position stuff
        private static float posX = 500;
        private static float posY = 900;
        public static Vector2 Position => new Vector2(posX, posY);
        private static Vector2 RailPosition => new Vector2(ship.ShipRailPosition().X, ship.ShipRailPosition().Y);
        private static bool ready = false;

        // Weapons stuff
        private static int currentWeapon = 1;
        private static bool canShoot = true;
        private static float recoilTime = 0.4f;
        private static float currentTime = 0;

        // Damage stuff
        private static int shipIntegrity = 4;
        private static readonly float shieldTime = 0.8f;
        private static float currentShieldTime = 0;

        // Events
        public static Action OnShipDestroyed;

        public void InitializePlayer(ShipConfig withThisShip)
        {
            // Tag
            owner = "Player";

            // Ship configs
            ship = withThisShip; // Needs to be deprecated
            ShipConfiguration = ship;
          
            // ShipObject references
            ShipAnim = ShipConfiguration.ShipAnim();
            ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            SmokeDamageAnim = new Animation("Smoke", 0.25f, Effects.GetEffectTextures(1), false);
            ShieldAnim = new Animation("PlayerShield", 0.03f, Effects.GetEffectTextures(2));
            ShipAnim.ChangeFrame(4); // Intact ship texture

            // Collision
            objectCollider = new Collider(Position, ship.ShipSize(), "Player");
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

        private static void Fire()
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
        public static Texture GetIntegrityTexture()
        {
            Texture integrity = ship.ShipAnim().GetFrameTexture(shipIntegrity);
            return integrity;
        }

        public static ShipConfig GetShip()
        {
            ShipConfig shipdata = ship;
            return shipdata;
        }
    }
}