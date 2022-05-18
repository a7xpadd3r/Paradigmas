using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BulletPool
    {
        private List<IBullet> inUse = new List<IBullet>();
        private List<IBullet> availables = new List<IBullet>();

        public IBullet GetBullet(Bullet bullet)
        {
            IBullet bulletToReturn = null;

            for (int i = 0; i < availables.Count; i++)
            {
                if (availables[i].Type == bullet)
                {
                    bulletToReturn = availables[0];                   
                    availables.RemoveAt(0);
                    inUse.Add(bulletToReturn);
                    bulletToReturn.Reset();
                    return bulletToReturn;
                }
            }
            bulletToReturn = BulletFactory.CreateBullet(bullet, Vector2.Zero);
            bulletToReturn.OnRecycle += RecycleBullet;
            inUse.Add(bulletToReturn);
            bulletToReturn.Reset();
            return bulletToReturn;
        }

        public void RecycleBullet(IBullet bullet)
        {
            if (inUse.Contains(bullet))
            {
                inUse.Remove(bullet);
                availables.Add(bullet);
            }
        }
    }
}
