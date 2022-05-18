using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class SpeedBoostItem : GameObject, IItems
    {
        
        public Item Type { get; set; }
        public int Heal { get; }
        public Vector2 Direction { get; set; }
        public float CurrentSpeed { get; private set; }
        public float Speed { get; }

        private string texturePath;
        private Animation ItemAnimation;
        public Collider BoxCollider;
        private float maxSpeed;
        
        public event Action<float, float> OnGetSpeed;
        public event Action<float, float> OnGetCurrentSpeed;

        public SpeedBoostItem(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }
        public SpeedBoostItem(string name, Item type, string texturePath, float speed,Vector2 startPosition, Vector2 scale, float angle = 0, string tag = "ItemSpeed") : base(name, startPosition, scale, angle)
        {
            Tag = tag;
            IsTrigger = true;
            CurrentSpeed = speed;
            BoxCollider = new Collider(this, ColliderType.Box, true);
            Type = type;
            this.texturePath = texturePath;
           
            CreateAnimations();
            this.currentAnimation = ItemAnimation;
            this.currentAnimation.Play();
        }
        public override void Update()
        {
            base.Update();
            BoxCollider.CheckCollision();
        }
        public void GetSpeed(float speed)
        {
            CurrentSpeed += speed;

            if (CurrentSpeed > maxSpeed)
            {
                CurrentSpeed = maxSpeed;              
            }

            OnGetSpeed?.Invoke(CurrentSpeed, speed);
            GetCurrentSpeed(speed);
        }     
        public void Reset()
        {
            SetActive(false);   
        }
      
        public void Hit()
        {
            ColliderManager.RemoveCollider(BoxCollider);
            GameObjectManager.RemoveGameObject(this);
        }
        protected override void CreateAnimations()
        {
            List<Texture> idleTexture = new List<Texture>();
            Texture texture = Engine.GetTexture(this.texturePath);
            idleTexture.Add(texture);
            ItemAnimation = new Animation("idle", idleTexture, 0.05f, true);
        }

        public void GetCurrentSpeed(float speed)
        {
            OnGetCurrentSpeed?.Invoke(CurrentSpeed, speed);
        }

    }
}
