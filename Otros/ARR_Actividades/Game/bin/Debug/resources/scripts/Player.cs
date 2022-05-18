using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class PlayerConfig
    {
        protected static string gTag = "Player";
        protected static int life = 4;
        protected static int shipstatus = 4;
        protected static GameObject gObject;
        protected static ColliderType cType = ColliderType.Box;
        public static int skin = 1;

        public static Collider pCollider;

        protected static Vector2 collisionVectorPos => new Vector2(GetPos() + 23f, GetPos(false));
        public static Transform collisionTransform => new Transform(collisionVectorPos, new Vector2(160f, 2f), 0);

        public static void InitializePlayer()
        {
            gObject = new GameObject(gTag, "Player", collisionTransform, Player.GetAnim(), cType);
            pCollider = new Collider(gObject, collisionTransform, ColliderType.Box);
            CollisionManager.AddCollider(pCollider);
            pCollider.OnCollision += Player.TakeDamage;
        }

        // El Capitan ship configs
        public struct ElCapitan
        {
            public readonly static float speed = 400f;
            public readonly static float[] screenLimits = { 150, 1930 };
            public readonly static float[] railPosition = { 31.8f, 30 };
            public readonly static float[] propellerPosition = { 47.5f, 110 };
            public readonly static float[] shipSize = { 10f, 10f };

            public static float[] position = { 960, 1000 };
        }

        public static int GetLifes => life;
        public static int GetShipStatus()
        {
            return shipstatus;
        }

        public static void AnyDamage()
        {
            if (shipstatus != 0)
            {
                --shipstatus;

                Console.WriteLine("AnyDamage llamado.");
            }

            else if (shipstatus == 0)
            {
                //gObject.SetActive(false);
                //Console.WriteLine("Player: ShipStatus objeto desactivado Tag: {0}.", gObject.objTag);
                shipstatus = 4;
                GameplayManager.LifeLost();
            }
        }

        public static float GetPos(bool needX = true)
        {
            float pos = ElCapitan.position[0];
            if (!needX)
                pos = ElCapitan.position[1];
            return pos;
        }
    }

    public class Player
    {
        private static float _rot = 0;
        private static bool canTakeDamage = true;
        private static float damageTimer = 1f;  // Invulnerability time after taking damage.
        private static float damageCurrentTimer = 0;

        // Animations
        private static Animation ShipStatus;
        private static Animation ShipPropellers;
        public static Animation smokeFX = new Animation("smoke1", 0.5f, GFX.GetTextures("switch1"), true, true);
        private static List<Texture> t_smoke1 = new List<Texture>();

        public static Animation GetAnim()
        {
            return ShipStatus;
        }
        public static void TakeDamage(Collider collider)
        {
            if (canTakeDamage && PlayerConfig.GetShipStatus() != 0)
            {
                PlayerConfig.AnyDamage();
                ShipStatus.ChangeFrame(PlayerConfig.GetShipStatus());
                canTakeDamage = false;
                smokeFX.Play();
            }
            else if(canTakeDamage && PlayerConfig.GetShipStatus() == 0) 
            {
                GameplayManager.LifeLost();
            }
        }
        public static void PlayerAnimStart()
        {
            CreateShipAnimation();
            CreatePropellersAnimation();
        }
        public static void PlayerMovement()
        {
            if (PlayerConfig.GetShipStatus() == 0)
            {
                PlayerConfig.AnyDamage();
                ShipStatus.ChangeFrame(PlayerConfig.GetShipStatus());
            }

            // Update collision position
            PlayerConfig.pCollider.UpdatePos(PlayerConfig.collisionTransform);

            if (Engine.GetKey(Keys.UP))
            {
                //PlayerConfig.ElCapitan.position[1] -= PlayerConfig.ElCapitan.speed * Program.GetDeltaTime();
                GameplayManager.LifeLost();
            }

            if (Engine.GetKey(Keys.DOWN))
            {
                //PlayerConfig.ElCapitan.position[1] += PlayerConfig.ElCapitan.speed * Program.GetDeltaTime();
            }

            if ((Engine.GetKey(Keys.LEFT) || Engine.GetKey(Keys.A)) && PlayerConfig.ElCapitan.position[0] > PlayerConfig.ElCapitan.screenLimits[0])
                PlayerConfig.ElCapitan.position[0] -= PlayerConfig.ElCapitan.speed * Program.GetDeltaTime();

            if ((Engine.GetKey(Keys.RIGHT) || Engine.GetKey(Keys.D)) && PlayerConfig.ElCapitan.position[0] < PlayerConfig.ElCapitan.screenLimits[1])
                PlayerConfig.ElCapitan.position[0] += PlayerConfig.ElCapitan.speed * Program.GetDeltaTime();

            ShipStatus.Update();    ShipPropellers.Update();    smokeFX.Update();
        }

        // Animations
        private static void CreateShipAnimation()
        {
            var DamageTextures = PlayerTextures.SkinSet();
            ShipStatus = new Animation("shipstatus", 0, DamageTextures, false, true);
            ShipStatus.ChangeFrame(4);

            for (int i = 0; i < 5; i++)
            {
                t_smoke1.Add(new Texture("resources/gfx/effect/smoke1-" + i + ".png"));
                Console.WriteLine("resources/gfx/effect/smoke1-" + i + ".png");
            }
            t_smoke1.Add(new Texture("resources/gfx/blank.png"));
            smokeFX = new Animation("smoke1", 0.1f, t_smoke1, false);
        }
        private static void CreatePropellersAnimation()
        {
            var PropellersTextures = PlayerTextures.PropellerSet();
            ShipPropellers = new Animation("shippropellers", 0.03f, PropellersTextures, true, false);
        }
        public static void PlayerDraw()
        {
            // Propellers animation
            Engine.Draw(ShipPropellers.CurrentTexture,
                PlayerConfig.ElCapitan.position[0] + PlayerConfig.ElCapitan.propellerPosition[0],
                PlayerConfig.ElCapitan.position[1] + PlayerConfig.ElCapitan.propellerPosition[1], 
                1, 1, _rot, 145.5f, 86.5f);

            // Ship draw
            Engine.Draw(ShipStatus.CurrentTexture, PlayerConfig.ElCapitan.position[0], PlayerConfig.ElCapitan.position[1], 1, 1, _rot, 145.5f, 86.5f);
            Engine.Draw(smokeFX.CurrentTexture,
                PlayerConfig.ElCapitan.position[0] - 220,
                PlayerConfig.ElCapitan.position[1] - 180, 2, 2);

            if (!canTakeDamage)
            {
                damageCurrentTimer += Program.GetDeltaTime();
            }
            

            if (damageCurrentTimer >= damageTimer && !canTakeDamage)
            {
                Console.WriteLine("Player TakeDamage timer reseted.");
                canTakeDamage = true;
                damageCurrentTimer = 0;
                //ShipStatus.ChangeFrame(PlayerConfig.GetShipStatus());
                //PlayerConfig.UpdateShipStatus();
            }
        }

    }

    public class Weapons
    {
        private static int currentWeapon = 1;
        private static bool canShoot = true;
        private static List<GenericBullet> bullets = new List<GenericBullet>();
        private static float recoilTime = 0.4f;
        private static float currentTime = 0;

        public static void Update()
        {
            RecoilManagment();
        }

        // How often player will shot beams
        private static void RecoilManagment()
        {
            if (!Weapons.canShoot)  currentTime += Program.GetDeltaTime();

            if (currentTime >= recoilTime && !Weapons.canShoot)
            {
                currentTime = 0;
                Weapons.canShoot = true;
            }
        }

        // Bullets entities
        public static void BulletsManagment()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].Draw)
                {
                    bullets.RemoveAt(i);
                }
            }
            for (int i = 0; i < bullets.Count; i++) bullets[i].Update();

            for (int i = 0; i < bullets.Count; i++) bullets[i].DrawBullet();
        }

        public static void PlayerWeaponsManagment()
        {
            if (Engine.GetKey(Keys.SPACE) && canShoot)
                Shoot();

            // Weapons switch & prevent spam when pressing keys at the same time.
            if ((Engine.GetKey(Keys.Num1) && !Engine.GetKey(Keys.Num2) && !Engine.GetKey(Keys.Num3)) && currentWeapon != 1)
            {
                recoilTime = 0.4f;
                currentWeapon = 1;
                Console.WriteLine("Switch to Weapon 1 - new RecoilTime {0}", recoilTime);
            }

            if ((Engine.GetKey(Keys.Num2) && !Engine.GetKey(Keys.Num1) && !Engine.GetKey(Keys.Num3)) && currentWeapon != 2)
            {
                recoilTime = 0.6f;
                currentWeapon = 2;
                Console.WriteLine("Switch to Weapon 2 - new RecoilTime {0}", recoilTime);
            }

            if ((Engine.GetKey(Keys.Num3) && !Engine.GetKey(Keys.Num1) && !Engine.GetKey(Keys.Num2)) && currentWeapon != 3)
            {
                recoilTime = 0.28f;
                currentWeapon = 3;
                Console.WriteLine("Switch to Weapon 3 - new RecoilTime {0}", recoilTime);
            }
        }

        static void Shoot()
        {
            if (canShoot)
            {
                Weapons.canShoot = false;
                bullets.Add(new GenericBullet(
                    PlayerConfig.ElCapitan.position[0] + PlayerConfig.ElCapitan.railPosition[0],
                    PlayerConfig.ElCapitan.position[1] - PlayerConfig.ElCapitan.railPosition[1], 
                    0, currentWeapon, "Player"));
                GenericBullet.sfx.Play();
                //SFXManagment.PlayFireSFX();
            }
        }
    }
}