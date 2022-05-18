using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PoolGeneric<T> where T : IPooleable
    {
        private List<T> inUse = new List<T>();
        private List<T> availables = new List<T>();

        public T Get(Bullet bulletType)
        {
            T obj = default(T);

            for (int i = 0; i < availables.Count; i++)
            {
                if (availables[i].Type == bulletType)
                {
                    obj = availables[0];
                    availables.RemoveAt(0);
                    inUse.Add(obj);
                    obj.Reset();
                    return obj;
                }
            }     
            
            obj = (T)BulletFactory.CreateBullet(bulletType, Vector2.Zero);
            obj.OnRecyclePool += RecycleElement;
            inUse.Add(obj);
            obj.Reset();
            return obj;
        }
        public void RecycleElement(IPooleable obj)
        {
            if (inUse.Contains((T)obj))
            {
                inUse.Remove((T)obj);
                availables.Add((T)obj);
            }
        }
    }
}

