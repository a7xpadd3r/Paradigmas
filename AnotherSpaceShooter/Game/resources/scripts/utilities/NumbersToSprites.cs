using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class NumbersToSprites
    {
        private List<Texture> numbersPack1 = UITextures.GetUITextures(4);
        private float separation = 15f;

        public void RenderNumbers(double what, Vector2 where, Vector2 howBig, float angle = 0, Vector2 offset = new Vector2())
        {
            List<Texture> numbersToDraw = new List<Texture>();
            float currentNumberOffset = 0;
            string what2string = what.ToString();

            foreach (char c in what2string)
            {
                switch (c)
                {
                    case '0':
                        numbersToDraw.Add(numbersPack1[0]);
                        break;
                    case '1':
                        numbersToDraw.Add(numbersPack1[1]);
                        break;
                    case '2':
                        numbersToDraw.Add(numbersPack1[2]);
                        break;
                    case '3':
                        numbersToDraw.Add(numbersPack1[3]);
                        break;
                    case '4':
                        numbersToDraw.Add(numbersPack1[4]);
                        break;
                    case '5':
                        numbersToDraw.Add(numbersPack1[5]);
                        break;
                    case '6':
                        numbersToDraw.Add(numbersPack1[6]);
                        break;
                    case '7':
                        numbersToDraw.Add(numbersPack1[7]);
                        break;
                    case '8':
                        numbersToDraw.Add(numbersPack1[8]);
                        break;
                    case '9':
                        numbersToDraw.Add(numbersPack1[9]);
                        break;
                    case ',':
                        numbersToDraw.Add(numbersPack1[10]);
                        break;
                }
            }

            for (int i = 0; i < numbersToDraw.Count; i++)
            {
                Engine.Draw(numbersToDraw[i], where.X + currentNumberOffset, where.Y, howBig.X, howBig.Y, angle, offset.X, offset.Y);
                currentNumberOffset += separation;
            }

            //string toString = what.ToString();
            //letters[] = toString.Split(); 
        }
    }
}
