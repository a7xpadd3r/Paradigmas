using System.Numerics;
using System;

namespace Game
{
    public abstract class GameObject
    {
        public bool debug = false;
        public bool ready = false;
        public bool active = false;
        // Identification
        public int Id { get; private set; }
        public string owner { get; set; }
        public string tag { get; set; }

        // Position
        public float posX { get; private set; }
        public float posY { get; private set; }
        public Vector2 Position => new Vector2(posX, posY);

        // Collision
        public Collider collider { get; private set; }
        public ColliderProperties colliderProperties { get; set; }
        public float damage { get; set; }
        public bool callsDamageOnCollision { get; set; }

        // Events
        public Action<float> AnyDamage;
        public Action<float> OnDamage;

        // Stats
        public float currentLifes { get; set; }
        public float originalLifes { get; private set; }

        public void Awake()
        {
            this.Id = GameObjectManager.GenerateObjectID();

            // Collision and damage are two different things.
            if (this.collider == null && this.colliderProperties != null)
            {
                this.collider = new Collider(colliderProperties, this.owner, this.tag, this.Id, this.damage);
                this.collider.OnCollision += OnCollision;
            }
            
            GameObjectManager.AddGameObject(this);
            this.AnyDamage += Damage;
            this.ready = true;
            this.active = true;
        }

        public void BeginPlay() { }
        public virtual void Render() { }
        public virtual void Update() { }

        public virtual void OnCollision(Collider instigator) { }
        /*
    {
        Console.WriteLine("'{0}' -> Colisión por parte de '{1}', instigado por '{2}'", this.owner, instigator, instigator.owner);
        if (callsDamageOnCollision) this.AnyDamage?.Invoke(instigator.damage);
    }*/

        public virtual void Damage(float amount) { }/*
        {
            this.currentLifes -= amount;
            this.OnDamage?.Invoke(amount);
            if (this.currentLifes <= 0) Destroy();
        }*/

        public virtual void OnDeactivated()
        {
            // Needs to be changed for Pool stuff
            GameObjectManager.RemoveGameObject(this);
        }

        public virtual void OnDeactivatedHandler() { }

        public virtual void Destroy()
        {
            if (this.collider != null) this.collider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            GameObjectManager.RemoveGameObject(this);
        }

        public virtual void UpdatePosition(Vector2 newPosition, bool bypassColliderPosition = false, Vector2 bypassedPosition = new Vector2())
        {
            this.posX = newPosition.X;
            this.posY = newPosition.Y;
            if (this.collider != null && !bypassColliderPosition) this.collider.UpdateColliderPosition(this.Position);
            if (this.collider != null && bypassColliderPosition) this.collider.UpdateColliderPosition(bypassedPosition);

        }
    }
}
