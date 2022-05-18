using System;
using System.Media;
using System.Numerics;

namespace Game
{
    public class Proyectile
    {
        // Basic stuff
        public string owner { get; private set; }
        public int type { get; private set; }
        protected private float speed = 800; // Default speed.
        protected private readonly float lifeTime = 2f;
        protected private float currentLifeTime = 0;
        protected private bool beamNeeded = false;

        // Special stuff
        private protected readonly Texture gfx = new Texture("resources/gfx/proyectiles/p_genericproyectile.png"); // Default texture.
        private protected readonly Animation beamAnim = new Animation("Beam", 0.08f, Effects.GetEffectTextures(4), false);
        private protected Collider pCollider;

        // Position stuff
        private protected float posX = 0;
        private protected float posY = 0;
        private protected float angle = -180;
        private protected Vector2 Position => new Vector2(posX, posY);
        private protected Vector2 Size = new Vector2();
        private protected Vector2 AngleOffset = new Vector2(33, 25);

        private protected bool draw = true;
        public bool Active => draw;
        public bool Death => beamNeeded;

        public Proyectile(Vector2 newPos, int newType, string newOwner = "Proyectile")
        {
            this.owner = newOwner;
            this.posX = newPos.X;
            this.posY = newPos.Y;
            this.type = newType;

            switch (type)
            {
                    // Player proyectiles
                case 1:
                    this.speed = 1000;
                    this.Size = new Vector2(5,10);
                    this.angle = 0;
                    this.AngleOffset = new Vector2();
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile1.png");
                    break;
                case 2:
                    this.speed = 700;
                    this.Size = new Vector2(5, 10);
                    this.angle = 0;
                    this.AngleOffset = new Vector2();
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile2.png");
                    break;
                case 3:
                    this.speed = 1600;
                    this.Size = new Vector2(5, 10);
                    this.angle = 0;
                    this.AngleOffset = new Vector2();
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile3.png");
                    break;
                    
                    // Enemy proyectiles
                case 4:
                    this.speed = -960;
                    this.Size = new Vector2(5, 10);
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile3.png");
                    break;
            }

            this.pCollider = new Collider(Position, Size, "Proyectile", ColliderType.Box);
            CollisionManager.AddCollider(this.pCollider);
            ProyectilesManager.AddProyectile(this);

            this.pCollider.OnCollision += HitSomething;
            this.beamAnim.OnAnimationFinished += DeathAnimationFinished;
            this.draw = true;
        }

        void DeathAnimationFinished()
        {
            beamNeeded = false;
            ProyectilesManager.RemoveProyectile(this);
        }

        void HitSomething(Collider instigator)
        {
            if (instigator.GetOwner() == "Player")
            {
                draw = false;
                pCollider.SetActive(false);
                CollisionManager.RemoveCollider(this.pCollider);
            }
            if (instigator.GetOwner() == "Proyectile")
            {
                beamAnim.ChangeFrame(0);
                draw = false;
                pCollider.SetActive(false);
                CollisionManager.RemoveCollider(this.pCollider);
            }
            beamNeeded = true;
        }

        public void Update()
        {
            if (draw)
            {
                currentLifeTime += Program.GetDeltaTime(); posY -= speed * Program.GetDeltaTime(); 

                if (pCollider != null) pCollider.UpdatePos(new Vector2(Position.X + 150, Position.Y + 80));

                if (currentLifeTime >= lifeTime || posY > 1200 || posY < -100)
                {
                    draw = false;
                    pCollider.SetActive(false);
                    CollisionManager.RemoveCollider(this.pCollider);
                }
                Engine.Draw(gfx, Position.X, Position.Y, 1, 1, angle, AngleOffset.X, AngleOffset.Y);
            }
        }

        public void AfterHit()
        {
            if (!Active && beamNeeded)
            {
                beamAnim.Update();
                Engine.Draw(beamAnim.CurrentTexture, Position.X, Position.Y, 2, 2, angle, AngleOffset.X, AngleOffset.Y);
            }
        }
    }
}
