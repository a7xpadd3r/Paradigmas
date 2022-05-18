using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BulletController : GameObject, IBullet, IPooleable
    {
        //Campos
        private string texturePath;
        public event Action<IBullet> OnRecycle;
        public event Action<IPooleable> OnRecyclePool;
        private Animation bulletAnimation;
        private Collider boxCollider;


        //Propiedades
        public int Damage { get; }
        public Bullet Type { get; set; }
        public float Speed { get; }
        public Vector2 Direction { get; set; }


        //Constructor
        public BulletController(Bullet type, string tag, string texturePath, int damage, Vector2 starPosition, float speed, Vector2 scale, string name, float angle = 0) : base(name, starPosition, scale, angle)
        {           
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
            //OnDeactivate += OnDeactivateHandler;
            OnDeactivate += OnDeactivateGenericHandler;
        }

        //Metodos
        public override void Update()
        {
            base.Update();
            Position += Direction * Speed * Time.DeltaTime;
            boxCollider.CheckCollision();
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