using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IWeapon
    {
        IOwnWeapon Owner { get; }
        void AssignOwner(IOwnWeapon owner);
        void StartAttack();
        void StopAttack();
        void Update();
    }
}
