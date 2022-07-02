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
        public Collider objectCollider { get; private set; }
        public Transform objectTransform { get; set; }

        // Animation or sprite
        public Texture objectTexture { get; private set; }
        public Animation objectAnimation { get; private set; }

        // Public values
        public bool Active => objectActive;
        public int ID => objectID;
        public string Owner => objectOwner;
        public string Tag => objectTag;
        public Collider Collider => objectCollider;
        public Vector2 Position => objectTransform.Position;
        public Vector2 Size => objectTransform.Scale;
        public float Rotation => objectTransform.Rotation;
        public Texture Texture => objectTexture;
        public Animation Animation => objectAnimation;

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
