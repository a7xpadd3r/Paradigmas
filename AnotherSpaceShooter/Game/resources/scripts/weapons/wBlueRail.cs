using System;
using System.Numerics;

namespace Game
{
    public class wBlueRail : iWeapon
    {
        public iGetWeapon Owner { get; private set; }

        public wBlueRail(float cd)
        {

        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire(Vector2 spawnPosition)
        {
            new Proyectile(spawnPosition, 1, "Player");
            Console.WriteLine("Calling fire on Blue Rail weapon");
        }

        public void Update()
        {

        }
    }
}
