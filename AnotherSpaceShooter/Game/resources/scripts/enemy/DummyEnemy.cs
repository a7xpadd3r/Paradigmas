using System;
using System.Numerics;

namespace Game
{
    public class DummyEnemy
    {
        public static bool debug = false;
        private static bool ready = false;
        private static int life = 10;
        public static Action OnDead;
        public static int Life => life;

        // Special stuff
        private static ShipConfig ship = null;
        private static Collider collider = null;
        private static Animation shipAnimation = null;
        private static Animation shipPropellers = null;
        private static Animation smokeDamage = new Animation("Smoke", 0.12f, Effects.GetEffectTextures(1), false);
        private static Animation shieldRed = new Animation("Smoke", 0.03f, Effects.GetEffectTextures(3));

        // Position stuff
        private static float posX = 900;
        private static float posY = 200;
        private static Vector2 Position => new Vector2(posX, posY);

        // Weapons stuff
        private static bool canShoot = true;
        private static float recoilTime = 0.4f;
        private static float currentTime = 0;

        // Damage stuff
        private static int shipIntegrity = 4;
        private static bool isShielding = false;
        private static readonly float shieldTime = 0.4f;
        private static float currentShieldTime = 0;

        // "AI"
        private static bool movingRight = true;

        public static void InitializeDummy(ShipConfig withThisShip)
        {
            ship = withThisShip;
            shipAnimation = ship.ShipAnim();
            shipPropellers = ship.PropellersAnim();
            //shipAnimation.ChangeFrame(4);

            collider = new Collider(Position, ship.ShipSize(), "Player");
            collider.OnCollision += AnyDamage;
            CollisionManager.AddCollider(collider);
            ready = true;
            if (debug) Console.WriteLine("Dummy inicializado.");
        }

        private static void AnyDamage(Collider instigator)
        {
            if (instigator.GetOwner() != "EnemyProyectile")
            {
                if (!isShielding && shipIntegrity > 1)
                {
                    life--;
                    shipIntegrity--;
                    //shipAnimation.ChangeFrame(shipIntegrity);
                    shieldRed.ChangeFrame(0);
                    smokeDamage.Play();
                    currentShieldTime = 0;
                    isShielding = true;
                    if (debug) Console.WriteLine("DummyEnemy --> Evento de daño, integridad " + shipIntegrity + "/4... Instigador --> " + instigator.GetOwner());
                }
                else if (!isShielding && shipIntegrity == 1)
                {
                    shipIntegrity = 4;
                    //shipAnimation.ChangeFrame(shipIntegrity);
                    shieldRed.ChangeFrame(0);
                    smokeDamage.Play();
                    currentShieldTime = 0;
                    isShielding = true;
                    if (debug) Console.WriteLine("Dummy --> RIP, reiniciando... Instigador -->" + instigator.GetOwner());
                }
                if (Life == 0) OnDead?.Invoke();           
            }
        }

        public static void Update()
        {
            if (ready)
            {
                collider.UpdatePos(Position + ship.ShipCollisionOffset());
                shipAnimation.Update(); shipPropellers.Update(); smokeDamage.Update(); shieldRed.Update();

                Engine.Draw(shipPropellers.CurrentTexture, Position.X + ship.ShipPropellersPosition().X, Position.Y + ship.ShipPropellersPosition().Y, 1, 1, -180, ship.ShipDrawOffset().X, ship.ShipDrawOffset().Y);
                Engine.Draw(shipAnimation.CurrentTexture, Position.X, Position.Y, 1, 1,-180);
                Engine.Draw(smokeDamage.CurrentTexture, Position.X, Position.Y, 1.7f, 1.7f, -180, 55, 65);

                AI();
            }
        }

        private static void AI()
        {
            if (posX > 2000)
                movingRight = true;

            else if (posX < -50)
                movingRight = false;

            if (!movingRight)
                posX += ship.ShipSpeed() * Program.GetDeltaTime();

            else if (movingRight)
                posX -= ship.ShipSpeed() * Program.GetDeltaTime();

            if (canShoot)
            {
                canShoot = false;
                //bullets.Add(new Proyectile(Position + RailPosition + new Vector2(-60, 0), 1, "Enemy"));
                //bullets.Add(new Proyectile(Position + RailPosition + new Vector2(60, 0), 1, "Enemy")); //55
                //bullets.Add(new Proyectile(Position - ship.ShipRailPosition(), 4, "EnemyProyectile"));
                ProyectilesManager.AddProyectile(new Proyectile(Position - ship.ShipRailPosition(), 4, "EnemyProyectile"));
            }

            if (!canShoot) currentTime += Program.GetDeltaTime();

            if (currentTime >= recoilTime && !canShoot)
            {
                currentTime = 0;
                canShoot = true;
            }

            if (isShielding) { currentShieldTime += Program.GetDeltaTime(); Engine.Draw(shieldRed.CurrentTexture, Position.X, Position.Y, 3.5f, 3.5f, -180, ship.ShipDrawOffset().X - 17, ship.ShipDrawOffset().Y - 28); }
            if (currentShieldTime >= shieldTime && isShielding) isShielding = false;
        }
    }
}