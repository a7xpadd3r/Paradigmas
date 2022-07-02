using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameObject : Collider
    {
        // Basic values
        public int objectID { get; private set; }
        public string objectOwner { get; private set; }
        public string objectTag { get; private set; }
        public bool objectActive { get; private set; }

        // Position
        public Transform objectTransform { get; private set; }

        // Sprites

        // Public values
        public bool Active => objectActive;
        public int ID => objectID;
        public string Owner => objectOwner;
        public string Tag => objectTag;
        public Vector2 Position => objectTransform.Position;
        public Vector2 Size => objectTransform.Scale;
        public float Rotation => objectTransform.Rotation;

        // Functions
        public virtual void Awake() { }
        public virtual void BeginPlay() { }
        public virtual void Update() { }
        public virtual void OnCollision(Collider instigator) { }
        public virtual void Damage(float amount) { }
        public virtual void Sleep() { }
        public virtual void Destroy() { }

    }
}
