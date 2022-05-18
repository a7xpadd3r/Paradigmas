using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Bullet
    {
        MisilEnemy,
        Plasma,
        MisilPlayer
    }
    public static class BulletFactory
    {
        public static IBullet CreateBullet(Bullet bullet, Vector2 startPosition)
        {
            switch (bullet)
            {
                case Bullet.MisilEnemy:
                    return new BulletController2("BulletEnemy", "Textures/Animations/Bullet/0.png",1000, startPosition, 200, new Vector2(1, 1), "BulletEnemy");
                case Bullet.MisilPlayer:
                    return new BulletController2("BulletPlayer", "Textures/Animations/Bullet/3.png", 1000, startPosition, 200, new Vector2(1, 1), "BulletPlayer");
                case Bullet.Plasma:
                    return new BulletController2("BulletPlayer", "Textures/Animations/Bullet/1.png", 1000, startPosition, 200, new Vector2(1, 1), "BulletPlayer");
                default:
                    return new BulletController2("BulletPlayer", "Textures/Animations/Bullet/3.png", 1000, startPosition, 200, new Vector2(1, 1), "BulletPlayer");
            }
        }
    }
}
