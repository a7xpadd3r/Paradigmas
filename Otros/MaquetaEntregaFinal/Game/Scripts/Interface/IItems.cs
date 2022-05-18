using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IItems
    {
        Item Type { get; }
        int Heal { get; }
        float Speed { get; }
        void GetSpeed(float speed);
    }
}
