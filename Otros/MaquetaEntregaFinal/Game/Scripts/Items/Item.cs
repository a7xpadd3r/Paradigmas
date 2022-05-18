using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Items : GameObject, IItems
    {                       
        public int Heal { get; }
        public Item Type { get; set; }

        public float Speed { get; }

        private string texturePath;
        private Animation ItemAnimation;
        private Collider BoxCollider;

        public Items(string name, Item type, string texturePath,int heal, Vector2 startPosition, Vector2 scale, float angle = 0, string tag = "Item") : base( name, startPosition, scale, angle)
        {
            Tag = tag;      
            IsTrigger = true;
            Type = type;
            BoxCollider = new Collider(this, ColliderType.Box, true);
            Heal = heal;
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

        public void GetSpeed(float speed)
        {
        }
    }
}

       

