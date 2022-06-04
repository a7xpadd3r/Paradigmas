using System;
using System.Numerics;

namespace Game
{
    class wRedDiamond : iWeapon
    {
        public iGetWeapon Owner { get; private set; }
        public wRedDiamond()
        {

        }
        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire()
        {
            new Proyectile(new Vector2(0,0), 2, "Player");
            Console.WriteLine("Calling fire on Red Diamond weapon");
        }

        public void Update(float delta, Vector2 currentPosition)
        {

        }
    }
}
