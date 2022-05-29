using System;
using System.Numerics;

namespace Game
{
    public class DummyEnemy : ShipObject
    {
        public static bool debug = true;
        private static bool ready = false;
        private static int life = 10;
        public static Action OnDead;
        public static int Life => life;

        // Special stuff
        private static ShipConfig ship = null;

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
        private static readonly float shieldTime = 0.4f;
        private static float currentShieldTime = 0;

        // "AI"
        private static bool movingRight = true;

        public void InitializeDummy(ShipConfig withThisShip)
        {
            // Tag
            owner = "Enemy";

            // Ship configs
            ship = withThisShip;
            ShipConfiguration = ship;

            // ShipObject references
            ShipAnim = ShipConfiguration.ShipAnim();
            ShipPropellersAnim = ShipConfiguration.PropellersAnim();
            SmokeDamageAnim = new Animation("Smoke", 0.13f, Effects.GetEffectTextures(1), false);
            ShieldAnim = new Animation("EnemyShield", 0.03f, Effects.GetEffectTextures(3));
            Rotation = -180;
            //ShipAnimation.ChangeFrame(4);

            objectCollider = new Collider(Position, ship.ShipSize(), "Player");
            objectCollider.OnCollision += AnyDamage;
            ready = true; Awake(); if (debug) Console.WriteLine("Dummy inicializado.");
        }

        private void AnyDamage(Collider instigator)
        {
            if (instigator.GetOwner() != "EnemyProyectile")
            {
                if (!IsShielding && shipIntegrity > 1)
                {
                    life--;
                    shipIntegrity--;
                    //shipAnimation.ChangeFrame(shipIntegrity);
                    ShieldAnim.ChangeFrame(0);
                    SmokeDamageAnim.Play();
                    currentShieldTime = 0;
                    IsShielding = true;
                    if (debug) Console.WriteLine("DummyEnemy --> Evento de daño, integridad " + shipIntegrity + "/4... Instigador --> " + instigator.GetOwner());
                }
                else if (!IsShielding && shipIntegrity == 1)
                {
                    shipIntegrity = 4;
                    //shipAnimation.ChangeFrame(shipIntegrity);
                    ShieldAnim.ChangeFrame(0);
                    SmokeDamageAnim.Play();
                    currentShieldTime = 0;
                    IsShielding = true;
                    if (debug) Console.WriteLine("Dummy --> RIP, reiniciando... Instigador -->" + instigator.GetOwner());
                }
                if (Life == 0) OnDead?.Invoke();           
            }
        }

        public override void Update()
        {
            if (ready)
            {
                UpdateShipPosition(Position);
                objectCollider.UpdatePos(Position + ShipConfiguration.ShipCollisionOffset());

                //ShipAnim.Update(); ShipPropellersAnim.Update(); SmokeDamageAnim.Update(); ShieldAnim.Update();
                //Engine.Draw(shipPropellers.CurrentTexture, Position.X + ship.ShipPropellersPosition().X, Position.Y + ship.ShipPropellersPosition().Y, 1, 1, -180, ship.ShipDrawOffset().X, ship.ShipDrawOffset().Y);
                //Engine.Draw(shipAnimation.CurrentTexture, Position.X, Position.Y, 1, 1,-180);
                //Engine.Draw(smokeDamage.CurrentTexture, Position.X, Position.Y, 1.7f, 1.7f, -180, 55, 65);
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

            if (IsShielding) { currentShieldTime += Program.GetDeltaTime(); }
            if (currentShieldTime >= shieldTime && IsShielding) IsShielding = false;
        }
    }
}