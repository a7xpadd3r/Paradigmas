using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IBullet
    {
        event System.Action<IBullet> OnRecycle;
        Bullet Type { get; }
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        float Speed { get; }
        int Damage { get; }
        void Reset();
    }
}

