using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.resources.scripts.utilities
{
    class PoolEntry<T>
    {
        public int Id;
        public T Data;
    }


    class PoolGenerics<T>
    {

        private List<PoolEntry<T>> _avaliables = new List<PoolEntry<T>>();
        private List<PoolEntry<T>> _inUse = new List<PoolEntry<T>>();

        public PoolEntry<T> GetPool(int id)
        {
            for (int i = 0; i < _avaliables.Count; i++)
            {
                var data = _avaliables[i];
                if(data.Id == id)
                {
                    _inUse.Add(data);
                    _avaliables.RemoveAt(i);
                    return data;
                }
            }

            var entry = new PoolEntry<T>();

            entry.Id = id;
            _inUse.Add(entry);

            return entry;
        }

        public void Reset(PoolEntry<T> entry)
        {
            _inUse.Remove(entry);
            _avaliables.Add(entry);
        }
    }
}
