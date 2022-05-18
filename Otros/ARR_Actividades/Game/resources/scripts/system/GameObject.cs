using System;
using System.Numerics;

namespace Game
{
    public class GameObject
    {
        // Data
        public string objTag;           // Identifier for the objects list.
        public Animation objAnimation;
        public Collider objCollider;
        public Transform objTransform;
        public float objRadius;
        public bool objIsActive;
        public Vector2 testPos;
        private string owner;

        // Events
        public event Action<GameObject> objOnActivation;
        public event Action<GameObject> objOnDeactivation;

        public GameObject(string newTag, string newOwner, Transform newTransform, Animation newAnimation, ColliderType newColliderType)
        {
            this.objTag = newTag;
            this.objTransform = newTransform;
            this.objAnimation = newAnimation;
            this.objIsActive = true;
            this.owner = newOwner;
            GameObjectManager.AddGameObject(this);
            objCollider = new Collider(this, new Transform(new Vector2(newTransform.position.X, newTransform.position.Y), new Vector2(10, 10), 0), newColliderType, true);
            //CollisionManager.AddCollider(this.objCollider);
            //Console.WriteLine("GameObject --> '{0}' con el tag '{1}' ha sido inicializado.", this, newTag);
        }

        public Collider GetCollider()
        {
            return this.objCollider;
        }

        public void SetActive(bool newStatus)
        {
            if (objIsActive == newStatus) return;
            objIsActive = newStatus;
            if (objIsActive) objOnActivation?.Invoke(this); else objOnDeactivation?.Invoke(this);
        }

        public void Update()
        {
        }

        public string GetOwner()
        {
            return owner;
        }

        public void Render()
        {
            //TheRendererer.RenderDraw(objAnimation.CurrentTexture, objTransform, objRadius);
        }
    }
}