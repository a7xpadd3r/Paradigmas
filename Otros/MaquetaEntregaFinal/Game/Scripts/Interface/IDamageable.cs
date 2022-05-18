using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IDamageable
    {
        HealthController HealthController { get; }
        bool IsDestroyed { get; }

        event System.Action<GameObject> OnDestroy;
        void GetDamage(int damage);
        void GetHeal(int heal);
        void Destroy();
    }
}
