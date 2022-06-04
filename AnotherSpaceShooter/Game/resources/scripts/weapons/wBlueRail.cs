using System;
using System.Numerics;

namespace Game
{
    public class wBlueRail : iWeapon
    {
        public iGetWeapon Owner { get; private set; }

        public wBlueRail()
        {

        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void Fire()
        {
            new Proyectile(new Vector2(0,0), 1, "Player");
            Console.WriteLine("Calling fire on Blue Rail weapon");
        }

        public void Update(float delta, Vector2 currentPosition)
        {

        }
    }
}
