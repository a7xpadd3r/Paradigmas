using System;
using System.Numerics;

namespace Game
{
    class wRedDiamond : iWeapon
    {
        public iGetWeapon Owner { get; private set; }
        public wRedDiamond(float cd)
        {

        }
        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire(Vector2 spawnPosition)
        {
            new Proyectile(spawnPosition, 2, "Player");
            Console.WriteLine("Calling fire on Red Diamond weapon");
        }

        public void Update()
        {

        }
    }
}
