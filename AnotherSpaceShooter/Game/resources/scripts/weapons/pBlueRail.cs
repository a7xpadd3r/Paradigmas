using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class pBlueRail : GameObject, iProyectile
    {
        // iProyectile basics
        public Transform transform { get; set; }
        public Animation animation { get; set; }
        public float speed { get; set; }
        public float damage { get; set; }
        public bool active { get; set; }

        // iProyectile custom
        private float posX = 0;
        private float posY = 0;
        private Transform newTransform => new Transform(new Vector2(posX, posY), new Vector2(),0);


        public pBlueRail(Vector2 spawnPos)
        {
            this.speed = 1000;
            this.animation = ProyectilesTextures.GetProyectileAnim(0);
            this.damage = 1.8f;
            this.posX = spawnPos.X;
            this.posX = spawnPos.Y;
            this.active = true;
            this.objectCollider = new Collider(newTransform.position, newTransform.scale, "Player", "Proyectile", damage);
            Awake();
        }

        public override void Update()
        {
            if (active)
            {
                animation.Update();
                posX -= speed * Program.GetDeltaTime();
                Render();
            }
        }

        public void Render()
        {
            Engine.DrawTransform(animation.CurrentTexture, newTransform);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
