using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BulletController2 : GameObject, IBullet, IPooleable
    {
        //Campos
        private string texturePath;
        private Collider boxCollider;
        private Animation bulletAnimation;
        public event Action<IBullet> OnRecycle;
        public event Action<IPooleable> OnRecyclePool;


        //Propiedades
        public int Damage { get; }
        public Bullet Type { get; set; }
        public float Speed { get; }
        public Vector2 Direction { get; set; }


        //Constructor
        public BulletController2(Bullet type, string tag, string texturePath, int damage, Vector2 starPosition, float speed, Vector2 scale, string name, float angle = 0) : base(name, starPosition, scale, angle)
        {
            Engine.Debug("Se crea una bala");
            Tag = tag;
            Type = type;
            boxCollider = new Collider(this, ColliderType.Box, true);
            IsTrigger = true;
            this.texturePath = texturePath;
            Damage = damage;
            Speed = speed;
            CreateAnimations();
            currentAnimation = bulletAnimation;
            currentAnimation.Play();
            OnDeactivate += OnDeactivateGenericHandler;
        }


        //Metodos
        public override void Update()
        {
            base.Update();
            Position += Direction * Speed * Time.DeltaTime;

            if (Position.X < -5 || Position.X > Program.WIDTH_SCREEN || Position.Y < -5 || Position.Y > Program.HEIGHT_SCREEN)
            {
                SetActive(false);
            }
          
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void Reset()
        {
            SetActive(true);
        }
        public void Hit()
        {
            ColliderManager.RemoveCollider(boxCollider);
            GameObjectManager.RemoveGameObject(this);
        }
     
        protected override void CreateAnimations()
        {
                List<Texture> bulletTexture = new List<Texture>();
                Texture texture = Engine.GetTexture(texturePath);
                bulletTexture.Add(texture);
                bulletAnimation = new Animation("bulletTexure", bulletTexture, 0.05f);
        }
     
        private void OnDeactivateGenericHandler(GameObject gameObject)
        {
           OnRecyclePool?.Invoke(this);
        }
    }
}
