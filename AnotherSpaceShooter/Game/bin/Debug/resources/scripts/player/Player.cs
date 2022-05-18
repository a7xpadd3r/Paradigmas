using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player
    {
        public static bool debug = false;
        // Special stuff
        private static ShipConfig ship = null;
        private static Collider collider = null;
        private static Animation shipAnimation = null;
        private static Animation shipPropellers = null;
        private static Animation smokeDamage = new Animation("Smoke", 0.12f, Effects.GetEffectTextures(1), false);
        private static Animation shieldGreen = new Animation("Smoke", 0.03f, Effects.GetEffectTextures(2));

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
        private static bool isShielding = false;
        private static readonly float shieldTime = 0.8f;
        private static float currentShieldTime = 0;

        // Events
        public static Action OnShipDestroyed;

        public static void InitializePlayer(ShipConfig withThisShip)
        {
            ship = withThisShip;
            shipAnimation = ship.ShipAnim();
            shipPropellers = ship.PropellersAnim();
            shipAnimation.ChangeFrame(4);

            collider = new Collider(Position, ship.ShipSize(), "Player");
            collider.OnCollision += AnyDamage;
            CollisionManager.AddCollider(collider);
            ready = true;
            Console.WriteLine("Jugador inicializado.");
        }

        private static void AnyDamage(Collider instigator)
        {
            if (!isShielding && shipIntegrity > 1)
            {
                shipIntegrity--;
                shipAnimation.ChangeFrame(shipIntegrity);
                shieldGreen.ChangeFrame(0);
                smokeDamage.Play();
                currentShieldTime = 0;
                isShielding = true;
                if (debug) Console.WriteLine("Player --> Evento de daño, integridad " + shipIntegrity + "/4... Instigador --> " + instigator.GetOwner());
            }
            else if (!isShielding && shipIntegrity == 1)
            {
                shipIntegrity = 4;
                shipAnimation.ChangeFrame(shipIntegrity);
                shieldGreen.ChangeFrame(0);
                smokeDamage.Play();
                currentShieldTime = 0;
                isShielding = true;
                OnShipDestroyed?.Invoke();
                if (debug) Console.WriteLine("Player --> RIP, reiniciando... Instigador --> " + instigator.GetOwner());
            }
        }

        private static void Fire()
        {
            canShoot = false;
            //bullets.Add(new Proyectile(Position + RailPosition, currentWeapon));
            ProyectilesManager.AddProyectile(new Proyectile(Position + RailPosition, currentWeapon));
        }

        public static void Update()
        {
            if (ready)
            {
                // Movement controls
                if (Engine.GetKey(Keys.A)) posX -= ship.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.D)) posX += ship.ShipSpeed() * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.W)) posY -= (ship.ShipSpeed() / 1.1f) * Program.GetDeltaTime();
                if (Engine.GetKey(Keys.S)) posY += (ship.ShipSpeed() / 1.1f) * Program.GetDeltaTime();

                // Update stuff
                collider.UpdatePos(Position + ship.ShipCollisionOffset());
                shipAnimation.Update(); shipPropellers.Update(); smokeDamage.Update(); shieldGreen.Update();
                

                // Draw stuff
                Engine.Draw(shipAnimation.CurrentTexture, Position.X, Position.Y);
                Engine.Draw(shipPropellers.CurrentTexture, Position.X + ship.ShipPropellersPosition().X, Position.Y + ship.ShipPropellersPosition().Y);
                Engine.Draw(smokeDamage.CurrentTexture, Position.X, Position.Y, 1.7f, 1.7f,0, 55, 65);

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

                if (isShielding) { currentShieldTime += Program.GetDeltaTime(); Engine.Draw(shieldGreen.CurrentTexture, Position.X, Position.Y, 4, 4, 0, 0, -10); }
                if (currentShieldTime >= shieldTime && isShielding) isShielding = false;
            }
        }

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