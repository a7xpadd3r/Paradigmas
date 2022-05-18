using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IOwnWeapon
    {
        Vector2 OwnerPosition { get; }
        Vector2 OwnerDirection { get; }
        float OwnerOffsetX { get; }
        IWeapon CurrentWeapon { get; }
        List<IWeapon> Weapons { get; }
        void AddWeapon(IWeapon weapon);

    }
}
