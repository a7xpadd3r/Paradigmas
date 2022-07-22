using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyPoolProyectile : GameObject
    {
        // Basic
        private string owner = "Enemy";
        private int objectid;
        private ProyectileData pdata;
        private Direction proyectiledirection = Direction.Down;
        private WeaponTypes type;
    }
}
