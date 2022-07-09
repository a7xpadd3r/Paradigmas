using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameObject
    {
        // Basic values
        public int objectID { get; set; }
        public string objectOwner { get; set; }
        public string objectTag { get; set; }
        public bool objectActive { get; set; }
        public Collider objectCollider { get; set; }
        public Transform objectTransform { get; set; }

        // Animation or sprite
        public Texture objectTexture { get; set; }
        public Animation objectAnimation { get; set; }

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
        public virtual void Damage(float amount) { }
        public virtual void Sleep() { }
        public virtual void Destroy() { }
    }
}
