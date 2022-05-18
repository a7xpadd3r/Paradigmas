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
        MisilHelicopter,
        MisilEnemyLow,
        PlasmaMini,
        PlasmaTank,
        MisilBoss,
        Fuego,
        MisilPlayer
    }
    public static class BulletFactory
    {
        public static IBullet CreateBullet(Bullet bullet, Vector2 startPosition)
        {
            switch (bullet)
            {
                case Bullet.MisilEnemy:
                    return new BulletController(Bullet.MisilEnemy, "BulletEnemy", "Textures/Animations/Bullet/0.png", 100, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.MisilHelicopter:
                    return new BulletController(Bullet.MisilHelicopter, "BulletEnemy", "Textures/Animations/Bullet/2.png", 100, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.MisilEnemyLow:
                    return new BulletController(Bullet.MisilEnemyLow, "BulletEnemy", "Textures/Animations/Bullet/4.png", 100, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.MisilBoss:
                    return new BulletController(Bullet.MisilBoss, "BulletEnemy", "Textures/Animations/Bullet/6.png", 150, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.PlasmaTank:
                    return new BulletController(Bullet.PlasmaTank, "BulletEnemy", "Textures/Animations/Bullet/7.png", 50, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.PlasmaMini:
                    return new BulletController(Bullet.PlasmaMini, "BulletEnemy", "Textures/Animations/Bullet/1.png", 50, startPosition, 200, Vector2.One, "BulletEnemy");
                case Bullet.MisilPlayer:
                    return new BulletController2(Bullet.MisilPlayer, "BulletPlayer", "Textures/Animations/Bullet/3.png", 100, startPosition, 200, Vector2.One, "BulletPlayer");
                case Bullet.Fuego:
                    return new BulletController2(Bullet.Fuego, "BulletPlayer", "Textures/Animations/Bullet/5.png", 200, startPosition, 200, Vector2.One, "BulletPlayer");
                default:
                    return new BulletController2(Bullet.MisilPlayer, "BulletPlayer", "Textures/Animations/Bullet/3.png", 100, startPosition, 200, Vector2.One, "BulletPlayer");
            }
        }
    }
}
