using Game.resources.scripts;
using System;
using System.Collections.Generic;

namespace Game
{
    public class PlayerConfig
    {
        protected static int life = 4;
        protected static int shipstatus = 4;
        public static int skin = 1;

        // El Capitan ship configs
        public struct ElCapitan
        {
            public readonly static float speed = 400f;
            public readonly static float[] screenLimits = { 150 , 1930 };
            public readonly static float[] railPosition = { 31 , 30 };
            public readonly static float[] propellerPosition = { 47.5f, 110 };

            public static float[] position = { 960, 1000 };
        }

        public static int GetLifes()
        {
            return life;
        }

        public static int GetShipStatus()
        {
            return shipstatus;
        }

        public static void UpdateShipStatus()
        {
            if (shipstatus != 0)
                --shipstatus;
            else if (shipstatus == 0)
                Console.WriteLine("AVISO: Player UpdateShipStatus llamado pero el estado es 0.");
        }

        public static float GetPos(bool needX)
        {
            float pos = ElCapitan.position[0];
            if (!needX)
                pos = ElCapitan.position[1];
            return pos;
        }
    }

    public class Player
    {
        
        static float _rot = 0;

        private static Animation ShipStatus;
        private static Animation ShipPropellers;

        static float lifeTime = 2f;
        static float timer = 0;

        public static void PlayerAnimStart()
        {
            CreateShipAnimation();
            CreatePropellersAnimation();
        }

        public static void PlayerMovement()
        {
            if (Engine.GetKey(Keys.UP))
            {
                //_posY -= _speed * Program.deltaTime;
                GameplayManager.LifeLost();
            }

            if (Engine.GetKey(Keys.DOWN))
            {
                //_posY += _speed * Program.deltaTime;
            }

            if ((Engine.GetKey(Keys.LEFT) || Engine.GetKey(Keys.A)) && PlayerConfig.ElCapitan.position[0] > PlayerConfig.ElCapitan.screenLimits[0])
                PlayerConfig.ElCapitan.position[0] -= PlayerConfig.ElCapitan.speed * Program.deltaTime;

            if ((Engine.GetKey(Keys.RIGHT) || Engine.GetKey(Keys.D)) && PlayerConfig.ElCapitan.position[0] < PlayerConfig.ElCapitan.screenLimits[1])
                PlayerConfig.ElCapitan.position[0] += PlayerConfig.ElCapitan.speed * Program.deltaTime;

            ShipStatus.Update();
            ShipPropellers.Update();
        }

        // Animations
        private static void CreateShipAnimation()
        {
            var DamageTextures = PlayerTextures.SkinSet();
            ShipStatus = new Animation("shipstatus", 1f, DamageTextures, false, true);
        }
        private static void CreatePropellersAnimation()
        {
            var PropellersTextures = PlayerTextures.PropellerSet();
            ShipPropellers = new Animation("shippropellers", 0.03f, PropellersTextures, true, false);
        }

        public static void PlayerDraw()
        {
            Engine.Draw(ShipPropellers.CurrentTexture,
                PlayerConfig.ElCapitan.position[0] + PlayerConfig.ElCapitan.propellerPosition[0],
                PlayerConfig.ElCapitan.position[1] + PlayerConfig.ElCapitan.propellerPosition[1], 
                1, 1, _rot, 145.5f, 86.5f);
            Engine.Draw(ShipStatus.CurrentTexture, PlayerConfig.ElCapitan.position[0], PlayerConfig.ElCapitan.position[1], 1, 1, _rot, 145.5f, 86.5f);


            timer += Program.deltaTime;

            if (timer >= lifeTime)
            {
                PlayerConfig.UpdateShipStatus();
                timer = 0;
                Console.WriteLine(PlayerConfig.GetShipStatus());
                ShipStatus.ChangeFrame(3);
            }
        }
    }

    public class Weapons
    {
        private static int currentWeapon = 1;
        public static bool canShoot = true;
        static List<GenericBullet> bullets = new List<GenericBullet>();
        private static float recoilTime = 0.4f;
        private static float currentTime = 0;

        public static void Update()
        {
            RecoilManagment();
        }

        // How often player will shot beams
        private static void RecoilManagment()
        {
            currentTime += Program.deltaTime;
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
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].DrawBullet();
            }
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
                    0, currentWeapon));
                GenericBullet.sfx.Play();
                //SFXManagment.PlayFireSFX();
            }
        }
    }
}