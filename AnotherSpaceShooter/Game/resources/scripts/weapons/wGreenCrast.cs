using System;
using System.Numerics;

namespace Game
{
    class wGreenCrast : iWeapon
    {
        public iGetWeapon Owner { get; private set; }

        public wGreenCrast(float cd)
        {

        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire(Vector2 spawnPosition)
        {
            new Proyectile(spawnPosition, 3, "Player");
            Console.WriteLine("Calling fire on Green Crast weapon");
        }

        public void Update()
        {

        }
    }
}
