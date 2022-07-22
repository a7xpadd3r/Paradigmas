using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Player
    {
        //static Texture ship_elcapitan = "resources/ElCapitan.png";
        public static float _posY = 680;
        public static float _posX = 305;
        static float _rot = 0;
        public static float _speed = 400;
        public static float _limitLeft = 170;
        public static float _limitRight = 1270;

        public static void PlayerMovement()
        {
            if ((Engine.GetKey(Keys.LEFT) || Engine.GetKey(Keys.A)) && _posX > _limitLeft)
            {
                _posX -= _speed * Program.deltaTime;
            }

            if ((Engine.GetKey(Keys.RIGHT) || Engine.GetKey(Keys.D)) && _posX < _limitRight)
            {
                _posX += _speed * Program.deltaTime;
            }
        }

        public static void PlayerDraw()
        {
            Engine.Draw("resources/ElCapitan.png", _posX, _posY, 1, 1, _rot, 145.5f, 86.5f);
        }

        public static float GetPos(bool needX)
        {
            float pos = _posX;
            if (!needX)
                pos = _posY;
            return pos;
        }
    }

    public class Weapons
    {
        static int currentWeapon = 1;
        public static bool canShoot = true;
        static List<GenericBullet> bullets = new List<GenericBullet>();

        public static void BulletsManagment()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].Draw)
                {
                    bullets.RemoveAt(i);
                    Weapons.canShoot = true;
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
            {
                Shoot();
            }
            // Weapons switch
            if (Engine.GetKey(Keys.Num1) && currentWeapon != 1)
            {
                currentWeapon = 1;
                Console.WriteLine("Weapon 1");
            }
            if (Engine.GetKey(Keys.Num2) && currentWeapon != 2)
            {
                currentWeapon = 2;
                Console.WriteLine("Weapon 2");
            }
        }

        static void Shoot()
        {
            if (canShoot)
            {
                float posX = Player._posX + 32;
                float posY = Player._posY - 25;
                bullets.Add(new GenericBullet(posX, posY, 0, currentWeapon));
                Weapons.canShoot = false;
                GenericBullet.sfx.Play();
                Console.WriteLine("Bullet at {0}, {1}.", posX, posY);
            }
        }
    }
}
