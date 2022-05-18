using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MovementController
    {
        //Metodos
        public void StartMovementX(GameObject gameObject, float speed, float dirX)
        {
            LimitGameObject(gameObject, speed, dirX);
            gameObject.Position += new Vector2(dirX * speed * Time.DeltaTime, 0);
        }
        public void StartMovementY(GameObject gameObject, float speed, float dirY)
        {
            LimitGameObject(gameObject, speed, dirY);
            gameObject.Position += new Vector2(0, dirY * speed * Time.DeltaTime);
        }
        public void LimitGameObject(GameObject gameObject, float speed, float dirX1 = -1, float dirX2 = 1, float dirY1 = -1, float dirY2 = 1)
        {
            if (gameObject.Position.X > Program.WIDTH_SCREEN - 0)
            {
                gameObject.Position += new Vector2(dirX1 * speed * Time.DeltaTime, 0);
            }
            if (gameObject.Position.X < 10)
            {
                gameObject.Position += new Vector2(dirX2 * speed * Time.DeltaTime, 0);
            }
            if (gameObject.Position.Y > Program.HEIGHT_SCREEN - 0)
            {
                gameObject.Position += new Vector2(0, dirY1 * speed * Time.DeltaTime);
            }
            if (gameObject.Position.Y < 10)
            {
                gameObject.Position += new Vector2(0, dirY2 * speed * Time.DeltaTime);
            }
        }
    }
}
