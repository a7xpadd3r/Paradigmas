using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*public enum BulletType
    {
        BulletPlayer,
        BulletEnemy,
    }
    
    public static class FactoryBullet
    {
        public static IBullet CreateObject(BulletType bullet, Vector2 startPosition, float offsetX = 0, float offsetY = 0)
        {
            switch (bullet)
            {
                case BulletType.BulletPlayer:
                    return new BulletController("BulletPlayer", Bullet.Plasma, "Textures/Animations/Bullet/0.png", 20, new Vector2(startPosition.X - offsetX, startPosition.Y - offsetY), 480, new Vector2(1, 1), "BulletPlayer");
                case BulletType.BulletEnemy:
                    return new BulletController("BulletEnemy", Bullet.MachineGun, "Textures/Animations/Bullet/0.png",20, startPosition, 333, new Vector2(1, 1), "BulletEnemy",180);
                default:
                    return new BulletController("BulletPlayer", Bullet.Plasma, "Textures/Animations/Bullet/0.png", 20, startPosition, 480, new Vector2(1, 1), "BulletPlayer",180);
            }
        }

        /*
         public static IPooleable CreateObject(Element typeElement, Vector2 startPosition, float offsetX, float offsetY)
         {
             switch (typeElement)
             {
                case Element.BulletPlayer:
                    return new BulletController("BulletPlayer", typeElement, "Textures/Animations/Bullet/0.png", 20, new Vector2(startPosition.X - offsetX, startPosition.Y - offsetY), 480, new Vector2(1, 1), "BulletPlayer");
                case Element.BulletEnemy:
                    return new BulletController("BulletEnemy", typeElement, "Textures/Animations/Bullet/0.png", 20, startPosition, 333, new Vector2(1, 1), "BulletEnemy", 180);
                default:
                    return new BulletController("BulletPlayer", typeElement, "Textures/Animations/Bullet/0.png", 20, startPosition, 480, new Vector2(1, 1), "BulletPlayer", 180);
                }          
         }
    }*/
}

