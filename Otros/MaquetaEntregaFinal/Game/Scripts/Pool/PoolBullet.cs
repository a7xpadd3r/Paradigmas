using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PoolBullet
    {/*
        private List<IBullet> inUse = new List<IBullet>();
        private List<IBullet> availables = new List<IBullet>();

        public IBullet Get(Bullet element, Vector2 ownerPosition, float offsetX, float offsetY)
        {
            IBullet bulletReturn = null;
            for (int i = 0; i < availables.Count; i++)
            {
                if (availables[i].Type == element)
                {
                    bulletReturn = availables[0];
                    availables.RemoveAt(0);
                    inUse.Add(bulletReturn);
                    bulletReturn.Reset();
                    return bulletReturn;
                }

            }
            //bulletReturn = FactoryGeneric.CreateObject(element, ownerPosition, offsetX, offsetY);
            bulletReturn.OnRecycle += RecycleElement;
            inUse.Add(bulletReturn);
            bulletReturn.Reset();
            return bulletReturn;
        }

        public void RecycleElement(IBullet bullet)
        {
            if (inUse.Contains(bullet))
            {
                inUse.Remove(bullet);
                availables.Add(bullet);
            }
        }*/
    }
}

