using System.Numerics;
using System;

namespace Game
{
    public abstract class GameObject
    {
        public Collider objectCollider = null;
        public bool isActive = true;
        public float life = 1;
        public string Owner => owner;
        public string Tag => tag;
        public string owner = "null";
        public string tag = "null";
        public Vector2 spawnPosition = new Vector2(0,0);
        public Action<float> AnyDamage;
        public Action<float> OnDamage;
        public float damageAmount = 1;
        public bool callsDamageOnCollision = true;
        public bool debug = false;

        public void Awake()
        {
            GameObjectManager.AddGameObject(this);

            // Collision and damage are two different things.
            objectCollider.OnCollision += OnCollision;
            AnyDamage += Damage;
        }

        public void BeginPlay()
        {
        }

        public virtual void Render()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void OnCollision(Collider instigator)
        {
            //Console.WriteLine("'{0}' -> Colisión por parte de '{1}', instigado por '{2}'", Owner, instigator, instigator.GetOwner());
            if (callsDamageOnCollision) AnyDamage?.Invoke(instigator.damage);
        }

        public virtual void Damage(float amount)
        {
            life -= amount;
            OnDamage?.Invoke(amount);
            if (life <= 0) Destroy();
        }

        public virtual void OnDeactivated()
        {
            // Needs to be changed for Pool stuff
            GameObjectManager.RemoveGameObject(this);
        }

        public virtual void OnDeactivatedHandler()
        {

        }

        public virtual void Destroy()
        {
            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            GameObjectManager.RemoveGameObject(this);
        }

        private void UpdateOwner(string newOwner)
        {
            owner = newOwner;
        }
    }
}
