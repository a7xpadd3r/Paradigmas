using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IPooleable
    {
        Bullet Type { get; set; }
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        void Reset();
        event Action<IPooleable> OnRecyclePool;
    }
}
