using System;
using System.Numerics;

namespace Game
{
    class wGreenCrast : iWeapon
    {
        public iGetWeapon Owner { get; private set; }

        public wGreenCrast()
        {

        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire()
        {
            new Proyectile(new Vector2(0, 0), 3, "Player");
            Console.WriteLine("Calling fire on Green Crast weapon");
        }

        public void Update(float delta, Vector2 currentPosition)
        {

        }
    }
}
