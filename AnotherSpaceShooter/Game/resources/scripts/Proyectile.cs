using System;
using System.Media;
using System.Numerics;

namespace Game
{
    public class Proyectile : GameObject
    {
        // Basic stuff
        public int type { get; private set; }
        protected private float speed = 800; // Default speed.
        protected private readonly float lifeTime = 2f;
        protected private float currentLifeTime = 0;
        protected private float damage = 1; // Default damage

        // Special stuff
        private protected readonly Texture gfx = new Texture("resources/gfx/proyectiles/e_proyectile1.png"); // Default texture.

        // Position stuff
        private protected float posX = 0;
        private protected float posY = 0;
        private protected float offsetX = 140;
        private protected float offsetY = 80;
        private protected Vector2 Position => new Vector2(posX, posY);
        private protected Vector2 CollisionOffset => new Vector2(offsetX, offsetY);
        private protected Vector2 Size = new Vector2();

        public Proyectile(Vector2 newPos, int newType, string newOwner)
        {
            this.owner = newOwner;
            this.posX = newPos.X;
            this.posY = newPos.Y;
            this.type = newType;
            this.owner = newOwner;
            this.tag = "Proyectile";

            switch (type)
            {
                    // Player proyectiles
                case 1:
                    this.speed = 1000;
                    this.Size = new Vector2(1,1);
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile1.png");
                    this.damage = 1.8f;
                    break;
                case 2:
                    this.speed = 700;
                    this.Size = new Vector2(1, 1);
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile2.png");
                    this.damage = 3.3f;
                    break;
                case 3:
                    this.speed = 1600;
                    this.Size = new Vector2(1, 1);
                    this.gfx = new Texture("resources/gfx/proyectiles/p_proyectile3.png");
                    this.damage = 0.6f;
                    break;
                    
                    // Enemy proyectiles
                case 4:
                    this.speed = -960;
                    this.Size = new Vector2(1, 1);
                    this.gfx = new Texture("resources/gfx/proyectiles/e_proyectile1.png");
                    this.damage = 1f;
                    break;
            }

            objectCollider = new Collider(Position, Size, newOwner, tag, damage);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            new GenericEffect(Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
            Destroy();
        }

        public override void Update()
        {
            objectCollider.UpdatePos(new Vector2(Position.X + CollisionOffset.X, Position.Y + CollisionOffset.Y));
            currentLifeTime += Program.GetDeltaTime(); 
            posY -= speed * Program.GetDeltaTime(); 
            
            if (currentLifeTime >= lifeTime || posY > 1200 || posY < -100)
            {
                OnDeactivated();
            }
        }

        public override void Render()
        {
            Engine.Draw(gfx, Position.X, Position.Y, 1, 1);
        }
    }
}
