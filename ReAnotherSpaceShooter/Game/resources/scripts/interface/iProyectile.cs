using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface iProyectile
    {
        ProyectileData ProyectileStats { get; }
        void Update();
    }
}
