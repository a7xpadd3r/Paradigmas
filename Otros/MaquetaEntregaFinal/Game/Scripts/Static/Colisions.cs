using System;
namespace Game
{
   public static class CollisionUtilities
   {
        //Metodo
        public static bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            //Calculamos la distancia absoluta en X e Y las dos cajas
            float distanceX = Math.Abs(positionA.X - positionB.X);
            float distanceY  = Math.Abs(positionA.Y - positionB.Y);

            //Sumamos la mitad de los anchos y altos de ambas cajas
            float sumHalfWidths = sizeA.X / 2 + sizeB.X / 2;
            float sumHalfHeight = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeight;
        }
   }
}
