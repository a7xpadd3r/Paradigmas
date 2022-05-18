using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PoolDynamic
    {
        private List<BulletController> inUse = new List<BulletController>();
        private List<BulletController> available = new List<BulletController>();
        private Transform transform;

        public BulletController GetBullet()
        {
            BulletController bulletToReturn = null;
            if (available.Count > 0)
            {
                bulletToReturn = available[0];
                available.RemoveAt(0);
                inUse.Add(bulletToReturn);
            }
            else
            {

                bulletToReturn = new BulletController("Bullet", Element.BulletEnemy, "Textures/Animations/Bullet/0.png", 100, transform.Position, 100, new Vector2(1, 1), "Bullet");
                bulletToReturn.OnDeactivate += OnDeactivateBulletHandler;
            }
            inUse.Add(bulletToReturn);
            return bulletToReturn;
        }

        private void OnDeactivateBulletHandler(BulletController bullet)
        {
            RecycleBullet(bullet);
        }
        public void RecycleBullet(BulletController bullet)
        {
            if (inUse.Contains(bullet))
            {
                inUse.Remove(bullet);
                available.Add(bullet);
            }
        }
    }
}
