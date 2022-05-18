using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class PoolBulletsNotDynamic
    {
        private List<BulletController> inUse = new List<BulletController>();
        private List<BulletController> availables = new List<BulletController>();

        public PoolBulletsNotDynamic(int count)
        {
            for (int i = 0; i < count; i++)
            {
                BulletController bullet = new BulletController();
                availables.Add(bullet);
            }
        }

        public BulletController GetBullet()
        {
            if (availables.Count > 0)
            {
                BulletController bulletToReturn = availables[0];
                availables.RemoveAt(0);
                inUse.Add(bulletToReturn);
                return bulletToReturn;
            }
            return null;
        }

        private void onDeactivateBulletHandler(BulletController bullet)
        {
            RecycleBullet(bullet);
        }

        public void RecycleBullet(BulletController bullet)
        {
            if (inUse.Contains(bullet))
            {
                inUse.Remove(bullet);
                availables.Add(bullet);
            }
        }
    }
}

