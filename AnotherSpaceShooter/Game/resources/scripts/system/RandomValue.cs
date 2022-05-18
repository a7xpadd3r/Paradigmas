using System;

namespace Game
{
    public class RandomValue
    {
        protected Random rnd = new Random();

        public int RandomInteger(int from, int to)
        {
            rnd = new Random();
            int randomreturn = rnd.Next(from, to);
            Console.WriteLine("RandomValue -> nuevo valor entero aleatorio generado: '{0}'.", randomreturn);
            ResetRnd();
            return randomreturn;
        }

        public float RandomFloat(float from, float to)
        {
            rnd = new Random();
            double randomreturn = (rnd.NextDouble() * (to - from) + from);
            ResetRnd();
            Console.WriteLine("RandomValue -> nuevo valor flotante aleatorio generado: '{0}'.", randomreturn);
            return (float)randomreturn;
        }

        private void ResetRnd()
        {
            rnd = null;
        }
    }
}
