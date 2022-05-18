using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
   public class Points
   {
        private static readonly Points instance = new Points();

        private int pointTotal = 0;

        public static Points Instance
        {
            get
            {
                return instance;
            }
        }

        public void ShowScore()
        {
            Engine.Debug($"Total score: {pointTotal}");
        }

        public int ReturnScoreTotal()
        {
            return pointTotal;
        }

        public void ResetScore()
        {
            pointTotal = 0;
        }

        public void LowerScore()
        {
            pointTotal--;
        }

        public void AddScore()
        {
            pointTotal += 10;
        }

        private Points()
        {

        }
    }
}
