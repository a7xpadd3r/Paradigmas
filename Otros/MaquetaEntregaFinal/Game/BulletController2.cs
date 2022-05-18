using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BulletController2 : GameObject, IBullet
    {

        public event Action<IBullet> OnRecycle;

        private Animation bulletAnimation;
        private string texturePath;
        private Collision collision;
        public int Damage { get; }

        public float Speed { get; }

        public Vector2 Direction { get; set; }


        public BulletController2(string tag, string texturePath, int damage, Vector2 starPosition, float speed, Vector2 scale, string name, float angle = 0) : base(name, starPosition, scale, angle)
        {
            collision = new Collision();
            IsTrigger = true;
            Tag = tag;
            this.texturePath = texturePath;
            Damage = damage;
            Speed = speed;
            CreateAnimations();
            currentAnimation = bulletAnimation;
            currentAnimation.Play();
            OnDeactivate += OnDeactivateHandler;
            OnDeactivate += OnDeactivateGenericHandler;
        }

        
        public override void Update()
        {
            base.Update();
            transform.Position += Direction * Speed * Time.DeltaTime;
            CheckCollision();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void Reset()
        {
            SetActive(true);
        }

        private void CheckCollision()
        {
            var listGameObject = GameObjectManager.ActiveGameObjects;
            for (int i = 0; i < listGameObject.Count; i++)
            {
                if (collision.CheckCollision(this, listGameObject[i], "Enemies"))
                {
                    if (transform.Position.X <= -5)
                    {
                        SetActive(false);
                    }
                }
            }
        }

        protected override void CreateAnimations()
        {
                List<Texture> bulletTexture = new List<Texture>();
                Texture texture = Engine.GetTexture(texturePath);
                bulletTexture.Add(texture);
                bulletAnimation = new Animation("bulletTexure", bulletTexture, 0.05f);
        }

        private void OnDeactivateHandler(GameObject gameObject)
        {
            OnRecycle?.Invoke(this);
        }
        private void OnDeactivateGenericHandler(GameObject gameObject)
        {
            //OnRecyclePool?.Invoke(this);
        }
    }
}
