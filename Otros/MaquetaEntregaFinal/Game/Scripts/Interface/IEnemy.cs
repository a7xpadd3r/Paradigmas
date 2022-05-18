using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IEnemy
    {
        event Action<GameObject> OnDestroy;
        bool IsDestroyed { get; }

    }
}
