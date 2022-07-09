using System.Collections.Generic;

namespace Game
{
    public class PoolEntry<T>
    {
        public string Owner;
        public int Id;
        public T Value;
    }

    public class PoolGeneric<T>
    {
        private List<PoolEntry<T>> availables = new List<PoolEntry<T>>();
        private List<PoolEntry<T>> inUse = new List<PoolEntry<T>>();

        public PoolEntry<T> GetOrCreate(string owner)
        {
            if (availables.Count > 0)
            {
                for (var i = 0; i < availables.Count; i++)
                {
                    if (availables[i].Owner == owner)
                    {
                        var obj = availables[i];
                        availables.RemoveAt(i);
                        inUse.Add(obj);
                        return obj;
                    }
                }
            }

            var newObj = new PoolEntry<T> { Owner = owner };

            inUse.Add(newObj);
            return newObj;
        }

        public void InUseToAvailable(PoolEntry<T> poolEntry)
        {
            inUse.Remove(poolEntry);
            availables.Add(poolEntry);
        }
    }
}
